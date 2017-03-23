' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 01-23-2016
'
' Last Modified By : David McCarter
' Last Modified On : 01-23-2016
' ***********************************************************************
' <copyright file="XmlHelper.vb" company="dotNetTips.com">
'     dotNetTips.com. All rights reserved.
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.IO
Imports System.Xml
Imports System.Xml.Serialization

Namespace Xml
    ''' <summary>
    ''' Helper functions for XML.
    ''' </summary>
    ''' <remarks></remarks>
    Public Module XmlHelper

        ''' <summary>
        ''' Gets the node value.
        ''' </summary>
        ''' <param name="xml">The XML.</param>
        ''' <param name="path">The path.</param>
        ''' <returns></returns>
        Public Function GetNodeValue(ByVal xml As System.Xml.XPath.IXPathNavigable, ByVal path As String) As String

            If IsNothing(xml) Then
                Throw New ArgumentNullException(NameOf(xml))
            End If

            If String.IsNullOrEmpty(path) Then
                Throw New ArgumentNullException(NameOf(path))
            End If

            Try
                Return xml.CreateNavigator.SelectSingleNode(path).Value
            Catch exNull As NullReferenceException
                Return String.Empty
            End Try

        End Function

        ''' <summary>
        ''' Deserializes from XML file.
        ''' </summary>
        ''' <typeparam name="T">Type</typeparam>
        ''' <param name="fileName">Name of the file.</param>
        ''' <returns></returns>
        Public Function DeserializeFromXmlFile(Of T)(ByVal fileName As String) As T

            Return Deserialize(Of T)(My.Computer.FileSystem.ReadAllText(fileName))

        End Function

        ''' <summary>
        ''' Deserializes the specified XML.
        ''' </summary>
        ''' <typeparam name="T">Type</typeparam>
        ''' <param name="xml">The XML.</param>
        ''' <returns>T.</returns>
        ''' <exception cref="System.ArgumentNullException">xml</exception>
        Public Function Deserialize(Of T)(ByVal xml As String) As T
            If String.IsNullOrEmpty(xml) Then
                Throw New ArgumentNullException(NameOf(xml))
            End If

            Using reader = New StringReader(xml)
                Dim xmlReader = New XmlTextReader(reader)
                Return DirectCast(New XmlSerializer(GetType(T)).Deserialize(xmlReader), T)
            End Using
        End Function

        ''' <summary>
        ''' Serializes obj to XML file.
        ''' </summary>
        ''' <param name="obj">The obj.</param>
        ''' <param name="fileName">Name of the file.</param>
        Public Sub SerializeToXmlFile(ByVal obj As Object, ByVal fileName As String)

            My.Computer.FileSystem.WriteAllText(fileName, Serialize(obj), False)

        End Sub

        ''' <summary>
        ''' Serializes the specified obj to xml.
        ''' </summary>
        ''' <param name="obj">The obj.</param>
        ''' <returns></returns>
        Public Function Serialize(ByVal obj As Object) As String
            If obj Is Nothing Then
                Throw New ArgumentNullException(NameOf(obj))
            End If

            Using writer = New StringWriter()
                Using xmlWriter = New XmlTextWriter(writer)
                    Dim serilizer As New XmlSerializer(obj.[GetType]())
                    serilizer.Serialize(xmlWriter, obj)
                    Return writer.ToString()
                End Using
            End Using

        End Function

    End Module
End Namespace