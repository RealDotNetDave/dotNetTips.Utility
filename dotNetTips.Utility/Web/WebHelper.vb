' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 04-15-2016
'
' Last Modified By : David McCarter
' Last Modified On : 07-29-2016
' ***********************************************************************
' <copyright file="WebHelper.vb" company="NicheWare - David McCarter">
'     NicheWare - David McCarter
' </copyright>
' <summary></summary>
' ***********************************************************************

Imports System.CodeDom
Imports System.CodeDom.Compiler
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Data.Services.Client
Imports System.Diagnostics.Contracts
Imports System.Drawing
Imports System.IO
Imports System.Reflection
Imports System.Runtime.Serialization.Json
Imports System.ServiceModel.Web
Imports System.Text
Imports System.Web.Services.Description
Imports System.Xml
Imports dotNetTips.Utility.Portable.Extensions
Imports dotNetTips.Utility.Security

Namespace Web

    ''' <summary>
    ''' Web helper class.
    ''' </summary>
    Public Module WebHelper

        ''' <summary>
        ''' The HTTP header names
        ''' </summary>
        Private httpHeaderNames As String() = [Enum].GetNames(GetType(System.Net.HttpRequestHeader))

        ''' <summary>
        ''' Calls the web service.
        ''' </summary>
        ''' <param name="webServiceAsmxUrl">The web service asmx URL.</param>
        ''' <param name="serviceName">Name of the service.</param>
        ''' <param name="methodName">Name of the method.</param>
        ''' <param name="args">The args.</param>
        ''' <returns>System.Object.</returns>
        ''' <exception cref="InvalidDataException">Compile Error Occurred calling web service.</exception>
        ''' <remarks>Original Code By: Aslam.Iqbal@electoralreform.co.uk
        ''' Modified By: David McCarter</remarks>
        Public Function CallWebService(ByVal webServiceAsmxUrl As Uri, ByVal serviceName As String, ByVal methodName As String, ByVal args() As Object) As Object
            Contract.Requires(Of ArgumentNullException)(String.IsNullOrWhiteSpace(webServiceAsmxUrl.ToString()) = False)
            Contract.Requires(Of ArgumentNullException)(String.IsNullOrWhiteSpace(serviceName) = False)
            Contract.Requires(Of ArgumentNullException)(String.IsNullOrWhiteSpace(methodName) = False)
            Contract.Ensures(Contract.Result(Of Object)() IsNot Nothing)

            Static wsdlCache As Dictionary(Of String, String)

            Try
                If wsdlCache Is Nothing Then
                    wsdlCache = New Dictionary(Of String, String)()
                End If

                Dim wsdl As String
                If (wsdlCache.Count > 0 AndAlso wsdlCache.ContainsKey(webServiceAsmxUrl.ToString)) Then
                    wsdl = wsdlCache.Item(webServiceAsmxUrl.ToString)
                Else
                    wsdl = dotNetTips.Utility.Web.DownloadString(String.Format(CultureInfo.CurrentCulture, "{0}?wsdl", webServiceAsmxUrl))
                    wsdlCache.Add(webServiceAsmxUrl.ToString, wsdl)
                End If

                'Read the WSDL file describing a service.
                Dim description As ServiceDescription = ServiceDescription.Read(XmlReader.Create(New StringReader(wsdl)))

                'LOAD THE DOM'''''''''''''''''''''''''''

                '--Initialize a service description importer.
                ' Use SOAP 1.2.
                Dim importer As New ServiceDescriptionImporter() With {.ProtocolName = "Soap12"}
                importer.AddServiceDescription(description, Nothing, Nothing)

                '--Generate a proxy client.

                importer.Style = ServiceDescriptionImportStyle.Client
                '--Generate properties to represent primitive values.
                importer.CodeGenerationOptions = System.Xml.Serialization.CodeGenerationOptions.GenerateProperties

                'Initialize a Code-DOM tree into which we will import the service.
                Dim ns As CodeNamespace = New CodeNamespace()
                Dim unit As CodeCompileUnit = New CodeCompileUnit()
                unit.Namespaces.Add(ns)

                'Import the service into the Code-DOM tree.
                'This creates proxy code that uses the service.

                Dim warning As ServiceDescriptionImportWarnings = importer.Import(ns, unit)

                If warning = 0 Then

                    '--Generate the proxy code
                    Dim provider1 As CodeDomProvider = CodeDomProvider.CreateProvider("VB")
                    '--Compile the assembly proxy with the // appropriate references
                    Dim assemblyReferences() As String
                    assemblyReferences = New String() {"System.dll", "System.Web.Services.dll", "System.Web.dll", "System.Xml.dll", "System.Data.dll"}
                    '(Thanks for this line nikolas)
                    Dim parameters As CompilerParameters = New CompilerParameters(assemblyReferences) With {.GenerateInMemory = True}
                    Dim results As CompilerResults = provider1.CompileAssemblyFromDom(parameters, unit)

                    '-Check For Errors
                    If results.Errors.Count > 0 Then
                        Dim oops As CompilerError
                        For Each oops In results.Errors
                            System.Diagnostics.Debug.WriteLine("========Compiler error============")
                            System.Diagnostics.Debug.WriteLine(oops.ErrorText)
                        Next
                        Throw New InvalidDataException("Compile Error Occurred calling web service.")
                    End If

                    '--Finally, Invoke the web service method
                    Dim instance As Object = results.CompiledAssembly.CreateInstance(serviceName)
                    Dim mi As MethodInfo = instance.GetType().GetMethod(methodName)
                    Return mi.Invoke(instance, args)

                Else
                    Return Nothing
                End If

            Catch ex As Exception
                Throw
            End Try
        End Function

        ''' <summary>
        ''' Currents the users IP.
        ''' </summary>
        ''' <param name="currentContext">The current context.</param>
        ''' <returns>System.String.</returns>
        ''' <remarks>Original code by: Jason Robert Karns</remarks>
        Public Function CurrentUsersIP(ByVal currentContext As System.Web.HttpContext) As String
            Contract.Requires(Of ArgumentNullException)(currentContext Is Nothing)
            Contract.Ensures(String.IsNullOrWhiteSpace(Contract.Result(Of String)()) = False)

            Dim address As String = currentContext?.Request.ServerVariables("HTTP_CLIENT_IP")

            If address Is Nothing OrElse address.Length = 0 OrElse address = "unknown" Then
                address = currentContext?.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
            End If

            If address Is Nothing OrElse address.Length = 0 OrElse address = "unknown" Then
                address = currentContext?.Request.ServerVariables("REMOTE_ADDR")
            End If

            If address Is Nothing OrElse address.Length = 0 Then
                address = "unknown"
            End If

            Return address
        End Function

        ''' <summary>
        ''' Downloads the file.
        ''' </summary>
        ''' <param name="address">The HTTP address.</param>
        ''' <param name="fileName">Name of the local file.</param>
        Public Sub DownloadFile(ByVal address As Uri, ByVal fileName As String)
            Contract.Requires(Of ArgumentNullException)(String.IsNullOrWhiteSpace(address.ToString) = False)
            Contract.Requires(Of ArgumentNullException)(String.IsNullOrWhiteSpace(fileName) = False)

            'Try
            Using request As New System.Net.WebClient()
                'Download the data
                request.DownloadFile(address, fileName)
            End Using
            'Catch ex As System.Net.WebException When

            'End Try

        End Sub

        ''' <summary>
        ''' Downloads the file.
        ''' </summary>
        ''' <param name="address">The HTTP address.</param>
        ''' <param name="fileName">Name of the local file.</param>
        Public Sub DownloadFile(ByVal address As String, ByVal fileName As String)
            Contract.Requires(Of ArgumentNullException)(String.IsNullOrWhiteSpace(address) = False)
            Contract.Requires(Of ArgumentNullException)(String.IsNullOrWhiteSpace(fileName) = False)

            DownloadFile(New Uri(address), fileName)

        End Sub

        ''' <summary>
        ''' Downloads an online image.
        ''' </summary>
        ''' <param name="address">The address.</param>
        ''' <returns>Image.</returns>
        ''' <exception cref="ArgumentNullException"></exception>
        ''' <exception cref="System.ArgumentNullException"></exception>
        Public Function DownloadOnlineImage(ByVal address As Uri) As Image

            If IsNothing(address) Then
                Throw New ArgumentNullException(NameOf(address))
            End If

            Dim onlineImage As Image = Nothing

            Using request As New System.Net.WebClient
                Dim imageDownload = request.DownloadData(address)
                onlineImage = Image.FromStream(New MemoryStream(imageDownload))
            End Using

            Return onlineImage

        End Function

        ''' <summary>
        ''' Downloads an online image.
        ''' </summary>
        ''' <param name="address">The address.</param>
        ''' <returns>Image.</returns>
        Public Function DownloadOnlineImage(ByVal address As String) As Image
            Contract.Requires(Of ArgumentNullException)(String.IsNullOrWhiteSpace(address) = False)
            Contract.Ensures(Contract.Result(Of Image)() IsNot Nothing)

            Return DownloadOnlineImage(New Uri(address))

        End Function

        ''' <summary>
        ''' Downloads the string.
        ''' </summary>
        ''' <param name="address">The HTTP address.</param>
        ''' <returns>System.String.</returns>
        Public Function DownloadString(ByVal address As Uri) As String
            Contract.Requires(Of ArgumentNullException)(String.IsNullOrWhiteSpace(address.ToString) = False)
            Contract.Ensures(String.IsNullOrWhiteSpace(Contract.Result(Of String)()) = False)

            Using request As New System.Net.WebClient()
                'Download the data
                Return request.DownloadString(address)
            End Using

        End Function

        ''' <summary>
        ''' Downloads the string.
        ''' </summary>
        ''' <param name="address">The HTTP address.</param>
        ''' <returns>System.String.</returns>
        Public Function DownloadString(ByVal address As String) As String
            Return DownloadString(New Uri(address))
        End Function

        ''' <summary>
        ''' Downloads a web page.
        ''' </summary>
        ''' <param name="address">The URL of the page to download.</param>
        ''' <returns>Web page as <seealso cref="String" /></returns>
        Public Function DownloadWebpage(ByVal address As Uri) As String
            Contract.Requires(Of ArgumentNullException)(String.IsNullOrWhiteSpace(address.ToString) = False)
            Contract.Ensures(String.IsNullOrWhiteSpace(Contract.Result(Of String)()) = False)

            Using request As New System.Net.WebClient()
                Dim requestData As Byte()

                'Download the data
                requestData = request.DownloadData(address)

                'Return the data by encoding it back to text!
                Return Encoding.ASCII.GetString(requestData)

            End Using

        End Function

        ''' <summary>
        ''' Downloads a web page.
        ''' </summary>
        ''' <param name="address">The URL of the page to download.</param>
        ''' <returns>Web page as <seealso cref="String" /></returns>
        Public Function DownloadWebPage(ByVal address As String) As String
            Contract.Requires(Of ArgumentNullException)(String.IsNullOrWhiteSpace(address) = False)
            Contract.Ensures(String.IsNullOrWhiteSpace(Contract.Result(Of String)()) = False)

            Return DownloadWebpage(New Uri(address))
        End Function

        '' TODO: Move to new version
        ''' <summary>
        ''' Executes the odata query full.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="requestUrl">The request URL.</param>
        ''' <param name="items">The items.</param>
        ''' <param name="context">The context.</param>
        Public Sub ExecuteOdataQueryFull(Of T)(requestUrl As Uri, items As Collection(Of T), context As DataServiceContext)

            Dim results = context.Execute(Of T)(requestUrl)
            If results IsNot Nothing Then
                For Each result In results.ToList()
                    items.Add(result)
                Next
            End If

            Dim response = DirectCast(results, QueryOperationResponse(Of T))
            Dim continuation = response.GetContinuation()

            If continuation IsNot Nothing Then
                ExecuteOdataQueryFull(continuation.NextLinkUri, items, context)
            Else
            End If

        End Sub

        ''' <summary>
        ''' Extracts the HTTP header value.
        ''' </summary>
        ''' <param name="key">The key.</param>
        ''' <returns>System.String.</returns>
        Public Function ExtractHttpHeaderValue(key As String) As String
            Contract.Requires(Of ArgumentNullException)(String.IsNullOrWhiteSpace(key) = False)

            Dim request = System.Web.HttpContext.Current.Request

            Return If(request.Headers.AllKeys.Contains(key), request.Headers(key), [String].Empty)
        End Function

        ''' <summary>
        ''' Extracts the HTTP query value.
        ''' </summary>
        ''' <param name="key">The key.</param>
        ''' <returns>System.String.</returns>
        Public Function ExtractHttpQueryValue(key As String) As String
            Contract.Requires(Of ArgumentNullException)(String.IsNullOrEmpty(key) = False)

            Dim request = System.Web.HttpContext.Current.Request

            Return request.Item(key)
        End Function

        ''' <summary>
        ''' HTML encodes an text for an attribute.
        ''' </summary>
        ''' <param name="input">The text to encode.</param>
        ''' <returns>Encoded text as <seealso cref="String" /></returns>
        Public Function HtmlAttributeEncode(ByVal input As String) As String
            Contract.Requires(Of ArgumentNullException)(String.IsNullOrWhiteSpace(input) = False)
            Contract.Ensures(String.IsNullOrWhiteSpace(Contract.Result(Of String)()) = False)

            Return SecurityHelper.EncodeHtmlAttribute(input)

        End Function

        ''' <summary>
        ''' HTML decodes text.
        ''' </summary>
        ''' <param name="input">The HTML to decode.</param>
        ''' <returns>Decoded text as <seealso cref="String" /></returns>
        Public Function HtmlDecode(ByVal input As String) As String
            Contract.Requires(Of ArgumentNullException)(String.IsNullOrWhiteSpace(input) = False)
            Contract.Ensures(String.IsNullOrWhiteSpace(Contract.Result(Of String)()) = False)

            Return System.Web.HttpUtility.HtmlDecode(input)

        End Function

        ''' <summary>
        ''' HTML encodes the data in a DataTable.
        ''' </summary>
        ''' <param name="table">The DataTable to encode.</param>
        ''' <returns>Cloned <seealso cref="DataTable" /> with it's data encoded.</returns>
        Public Function HtmlEncode(ByVal table As DataTable) As DataTable
            Contract.Requires(Of ArgumentNullException)(table IsNot Nothing = False)
            Contract.Ensures(Contract.Result(Of DataTable)() IsNot Nothing)

            Dim tempTable As DataTable = table.Clone
            Dim columnCount As Int32 = tempTable.Columns.Count()
            Dim columnCounter As Int32

            'Get the Column count
            For Each tempRow As DataRow In tempTable.Rows
                tempRow.BeginEdit()
                For columnCounter = 0 To columnCount - 1
                    If tempTable.Columns(columnCounter).DataType Is System.Type.GetType("System.String") Then
                        If Not IsDBNull(tempRow.Item(columnCounter)) Then
                            tempRow.Item(columnCounter) = HtmlEncode(CStr(tempRow.Item(columnCounter)))
                        End If
                    End If
                Next
                tempRow.EndEdit()
            Next

            Return tempTable

        End Function

        ''' <summary>
        ''' HTML encodes the data in a DataSet.
        ''' </summary>
        ''' <param name="data">The DataSet to encode.</param>
        ''' <returns>Cloned <seealso cref="DataSet" /> with it's data encoded.</returns>
        Public Function HtmlEncode(ByVal data As DataSet) As DataSet
            Contract.Requires(Of ArgumentNullException)(data IsNot Nothing = False)
            Contract.Ensures(Contract.Result(Of DataSet)() IsNot Nothing)

            Dim tempDataSet As DataSet = data.Clone

            For Each tempTable As DataTable In tempDataSet.Tables
                HtmlEncode(tempTable)
            Next

            Return tempDataSet

        End Function

        ''' <summary>
        ''' HTML encodes text.
        ''' </summary>
        ''' <param name="input">The text to encode.</param>
        ''' <returns>Encoded text as <seealso cref="String" /></returns>
        Public Function HtmlEncode(ByVal input As String) As String

            Return SecurityHelper.EncodeHtml(input)

        End Function

        ''' <summary>
        ''' Java Script encodes text.
        ''' </summary>
        ''' <param name="input">The text to encode.</param>
        ''' <returns>Encoded text as <seealso cref="String" /></returns>
        Public Function JavaScriptEncode(ByVal input As String) As String
            Contract.Requires(Of ArgumentNullException)(String.IsNullOrWhiteSpace(input) = False)
            Contract.Ensures(String.IsNullOrWhiteSpace(Contract.Result(Of String)()) = False)

            Return SecurityHelper.EncodeJS(input)

        End Function

        ''' <summary>
        ''' JSON decodes a string to an object.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="input">The input.</param>
        ''' <returns>T.</returns>
        ''' <remarks><example>
        ''' Dim temp = dotNetTips.Utility.Web.WebHelper.JsonDecode(Of ReadOnlyCollection(Of System.IO.DriveInfo))(Me.TextBox1.Text)
        ''' </example></remarks>
        Public Function JsonDecode(Of T)(ByVal input As String) As T
            Contract.Requires(Of ArgumentNullException)(String.IsNullOrWhiteSpace(input) = False)
            Contract.Ensures(Contract.Result(Of T)() IsNot Nothing)

            Using ms = New MemoryStream(Encoding.Unicode.GetBytes(input))
                Dim serilizer = New DataContractJsonSerializer(GetType(T))
                Return DirectCast(serilizer.ReadObject(ms), T)
            End Using

        End Function

        ''' <summary>
        ''' JSON encodes an object.
        ''' </summary>
        ''' <param name="input">The input.</param>
        ''' <returns>System.String.</returns>
        Public Function JsonEncode(ByVal input As Object) As String
            Contract.Requires(Of ArgumentNullException)(input Is Nothing = False)
            Contract.Ensures(Contract.Result(Of String)() IsNot Nothing)

            Dim serilizer = New DataContractJsonSerializer(input.GetType)

            Using ms = New MemoryStream()
                serilizer.WriteObject(ms, input)
                Return Encoding.Default.GetString(ms.ToArray())
            End Using

        End Function

        '' <summary>
        '' Gets the geo location info. @@@
        '' </summary>
        '' <param name="ipAddress">The ip address.</param>
        '' <returns></returns>
        '' <remarks>Original code by: Viper.</remarks>
        'Public Function LookupGeoLocationInfo(ByVal ipAddress As String) As GeoLocationInfo
        '    Contract.Requires(Of ArgumentNullException)(String.IsNullOrWhiteSpace(ipAddress) = False)

        '    Dim result As GeoLocationInfo = Nothing

        '    Dim geoInfo = DownloadString(New Uri(String.Format(CultureInfo.InvariantCulture, My.Resources.GeoLookup, System.Net.IPAddress.Parse(ipAddress).ToString)))

        '    If String.IsNullOrWhiteSpace(geoInfo) = False OrElse geoInfo.ToUpper.Contains("NOT FOUND") Then
        '        Return result
        '    End If

        '    Dim xmlResponse = XDocument.Parse(geoInfo)
        '    Dim callStatus = xmlResponse.Descendants("Status").Single.Value

        '    If String.Compare(callStatus, "OK", True, CultureInfo.CurrentCulture) <> 0 Then
        '        Return result
        '    End If

        '    result = New GeoLocationInfo()
        '    result.City = xmlResponse.Descendants("City").Single.Value
        '    result.CountryName = xmlResponse.Descendants("CountryName").Single.Value
        '    result.CountryCode = xmlResponse.Descendants("CountryCode").Single.Value
        '    result.RegionCode = xmlResponse.Descendants("RegionCode").Single.Value
        '    result.RegionName = xmlResponse.Descendants("RegionName").Single.Value
        '    result.PostalCode = xmlResponse.Descendants("ZipPostalCode").Single.Value
        '    result.Latitude = CDec(xmlResponse.Descendants("Latitude").Single.Value)
        '    result.Longitude = CDec(xmlResponse.Descendants("Longitude").Single.Value)

        '    Return result

        'End Function

        ''' <summary>
        ''' URL decodes text.
        ''' </summary>
        ''' <param name="input">The URL text to decode.</param>
        ''' <returns>Decoded text as <seealso cref="String" /></returns>
        Public Function UrlDecode(ByVal input As String) As String
            Contract.Requires(Of ArgumentNullException)(String.IsNullOrWhiteSpace(input) = False)
            Contract.Ensures(String.IsNullOrWhiteSpace(Contract.Result(Of String)()) = False)

            Return System.Web.HttpUtility.UrlDecode(input)

        End Function

        ''' <summary>
        ''' URL encodes text.
        ''' </summary>
        ''' <param name="input">The URL text to encode.</param>
        ''' <returns>Encoded URL as <seealso cref="String" /></returns>
        Public Function UrlEncode(ByVal input As String) As String
            Contract.Requires(Of ArgumentNullException)(String.IsNullOrWhiteSpace(input) = False)
            Contract.Ensures(String.IsNullOrWhiteSpace(Contract.Result(Of String)()) = False)

            Return SecurityHelper.EncodeUrl(input)

        End Function

        ''' <summary>
        ''' Visual Basic Script encodes text.
        ''' </summary>
        ''' <param name="input">The text to encode.</param>
        ''' <returns>Encoded text as <seealso cref="String" /></returns>
        Public Function VisualBasicScriptEncode(ByVal input As String) As String
            Contract.Requires(Of ArgumentNullException)(String.IsNullOrWhiteSpace(input) = False)
            Contract.Ensures(String.IsNullOrWhiteSpace(Contract.Result(Of String)()) = False)

            Return Security.SecurityHelper.EncodeVbs(input)

        End Function

        ''' <summary>
        ''' XML attribute encodes text.
        ''' </summary>
        ''' <param name="input">The text to encode.</param>
        ''' <returns>Encoded text as <seealso cref="String" /></returns>
        Public Function XmlAttributeEncode(ByVal input As String) As String
            Contract.Requires(Of ArgumentNullException)(String.IsNullOrWhiteSpace(input) = False)
            Contract.Ensures(String.IsNullOrWhiteSpace(Contract.Result(Of String)()) = False)

            Return SecurityHelper.EncodeHtmlAttribute(input)

        End Function

        ''' <summary>
        ''' XML encodes text.
        ''' </summary>
        ''' <param name="input">The string to encode.</param>
        ''' <returns>Encoded text as <seealso cref="String" /></returns>
        Public Function XmlEncode(ByVal input As String) As String
            Contract.Requires(Of ArgumentNullException)(String.IsNullOrWhiteSpace(input) = False)
            Contract.Ensures(String.IsNullOrWhiteSpace(Contract.Result(Of String)()) = False)

            Return Security.SecurityHelper.EncodeHtml(input)

        End Function

        ''' <summary>
        ''' Extracts the incoming request custom header values from a WebOperationContext.
        ''' </summary>
        ''' <returns>IReadOnlyDictionary(Of System.String, System.String).</returns>
        ''' <remarks>Strips out "sensitive" values stored in the HttpHeadersToIgnore.</remarks>
        Friend Function ExtractIncomingRequestCustomHeaderValues() As IReadOnlyDictionary(Of String, String)
            Return ExtractIncomingRequestCustomHeaderValues(False)
        End Function

        ''' <summary>
        ''' Extracts the incoming request custom header values from a WebOperationContext.
        ''' </summary>
        ''' <param name="includeSensitiveValues">if set to <c>true</c> [include sensitive values].</param>
        ''' <returns>IReadOnlyDictionary(Of System.String, System.String).</returns>
        ''' <remarks>Strips out "sensitive" values stored in the HttpHeadersToIgnore unless includeSensitiveValues is true.</remarks>
        Friend Function ExtractIncomingRequestCustomHeaderValues(includeSensitiveValues As Boolean) As IReadOnlyDictionary(Of String, String)
            Dim currentRequest = WebOperationContext.Current.IncomingRequest()

            If currentRequest Is Nothing Then
                Return Nothing
            End If

            Dim items = From
                           h In currentRequest.Headers.AllKeys
                        Where
                           httpHeaderNames.Any(Function(p) p.Equals(h)) = False
                        Select
                           h

            Dim headerValues = New Dictionary(Of String, String)()

            For Each key In items.AsParallel()
                Dim value = String.Empty

                value = If(includeSensitiveValues = False AndAlso key.ToLower().ContainsAny(My.Resources.HttpHeadersToIgnore.Split(dotNetTips.Utility.Portable.ControlChars.Comma)), If(String.IsNullOrWhiteSpace(currentRequest.Headers(key).ToString(CultureInfo.InvariantCulture)), False.ToString(), True.ToString()), currentRequest.Headers(key).ToString(CultureInfo.InvariantCulture))

                headerValues.Add(key.ToString, value)
            Next

            Return New ReadOnlyDictionary(Of String, String)(headerValues)
        End Function

    End Module
End Namespace