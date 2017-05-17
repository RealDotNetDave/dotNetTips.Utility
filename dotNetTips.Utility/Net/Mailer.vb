' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 01-20-2017
'
' Last Modified By : David McCarter
' Last Modified On : 05-11-2017
' ***********************************************************************
' <copyright file="Mailer.vb" company="McCarter Consulting - David McCarter">
'     David McCarter - dotNetTips.com © 2017
' </copyright>
' <summary></summary>
' ***********************************************************************

Imports System.Net.Mail
Imports dotNetTips.Utility.Portable.Extensions

Namespace Net
    ''' <summary>
    ''' Mail helper class.
    ''' </summary>
    ''' <seealso cref="System.IDisposable" />
    Public Class Mailer
        Implements IDisposable

        ''' <summary>
        ''' Event fired when Async mail send completed.
        ''' </summary>
        Public Event SendMailAsyncCompleted As EventHandler(Of System.ComponentModel.AsyncCompletedEventArgs)

        ''' <summary>
        ''' The mail server
        ''' </summary>
        Private _mailServer As SmtpClient

#Region "Public Methods"

        ''' <summary>
        ''' Creates list of email addresses.
        ''' </summary>
        ''' <param name="emailAddressType">Type of the email address.</param>
        ''' <param name="emailAddresses">Array of email addresses.</param>
        ''' <returns>Collection of <see cref="System.Collections.ObjectModel.ReadOnlyCollection(Of EmailAddress)"></see></returns>
        Public Shared Function CreateEmailAddressList(ByVal emailAddressType As EmailAddressType, ByVal ParamArray emailAddresses() As String) As System.Collections.ObjectModel.Collection(Of EmailAddress)
            Dim addresses As New System.Collections.ObjectModel.Collection(Of EmailAddress)

            For Each address As String In emailAddresses
                addresses.Add(New EmailAddress() With {.Address = address, .EmailAddressType = emailAddressType})
            Next

            Return addresses

        End Function
        ''' <summary>
        ''' Sends mail.
        ''' </summary>
        ''' <param name="fromAddress">From email address.</param>
        ''' <param name="subject">Subject.</param>
        ''' <param name="message">Message.</param>
        ''' <param name="bodyHtml">Sets message is HTML.</param>
        ''' <param name="sendAddresses">Send email addresses.</param>
        Public Sub SendMail(ByVal fromAddress As EmailAddress, ByVal subject As String, ByVal message As String, ByVal bodyHtml As Boolean, ByVal sendAddresses As System.Collections.ObjectModel.Collection(Of EmailAddress))

            ''Set default types, just in case.
            fromAddress.EmailAddressType = EmailAddressType.SendFrom

            Using tempMessage As New MailMessage()
                Net.Mailer.CreateMailMessage(fromAddress, subject, message, bodyHtml, sendAddresses, tempMessage)
                Me.SendMailMessage(tempMessage, Nothing)
            End Using

        End Sub

        ''' <summary>
        ''' Sends mail.
        ''' </summary>
        ''' <param name="fromAddress">From email address.</param>
        ''' <param name="subject">Subject.</param>
        ''' <param name="message">Message.</param>
        ''' <param name="bodyHtml">Sets message is HTML.</param>
        ''' <param name="sendToAddress">Send to email address.</param>
        Public Sub SendMail(ByVal fromAddress As EmailAddress, ByVal subject As String, ByVal message As String, ByVal bodyHtml As Boolean, ByVal sendToAddress As EmailAddress)

            ''Set default types, just in case.
            sendToAddress.EmailAddressType = EmailAddressType.SendTo
            fromAddress.EmailAddressType = EmailAddressType.SendFrom

            Dim toAddresses As New ObjectModel.Collection(Of EmailAddress)
            toAddresses.Add(sendToAddress)

            Me.SendMail(fromAddress, subject, message, bodyHtml, toAddresses)

        End Sub

        ''' <summary>
        ''' Sends mail.
        ''' </summary>
        ''' <param name="message">Message object.</param>
        Public Sub SendMail(ByVal message As MailMessage)

            Me.SendMailMessage(message, Nothing)

        End Sub

        ''' <summary>
        ''' Sends mail async.
        ''' </summary>
        ''' <param name="fromAddress">From email address.</param>
        ''' <param name="subject">Subject.</param>
        ''' <param name="message">Message.</param>
        ''' <param name="bodyHtml">Sets message is HTML.</param>
        ''' <param name="userToken">User token for async.</param>
        ''' <param name="sendAddresses">Send to email addresses.</param>
        Public Sub SendMailAsync(ByVal fromAddress As EmailAddress, ByVal subject As String, ByVal message As String, ByVal bodyHtml As Boolean, ByVal userToken As Object, ByVal sendAddresses As System.Collections.ObjectModel.Collection(Of EmailAddress))

            ''Set default types, just in case.
            fromAddress.EmailAddressType = EmailAddressType.SendFrom

            Dim tempMessage As New MailMessage()
            Net.Mailer.CreateMailMessage(fromAddress, subject, message, bodyHtml, sendAddresses, tempMessage)
            Me.SendMailMessage(tempMessage, userToken)

        End Sub

        ''' <summary>
        ''' Sends mail async.
        ''' </summary>
        ''' <param name="fromAddress">From email address.</param>
        ''' <param name="subject">Subject.</param>
        ''' <param name="message">Message.</param>
        ''' <param name="bodyHtml">Sets message is HTML.</param>
        ''' <param name="userToken">User token for async.</param>
        ''' <param name="sendToAddress">Send to email address.</param>
        Public Sub SendMailAsync(ByVal fromAddress As EmailAddress, ByVal subject As String, ByVal message As String, ByVal bodyHtml As Boolean, ByVal userToken As Object, ByVal sendToAddress As EmailAddress)

            ''Set default types, just in case.
            sendToAddress.EmailAddressType = EmailAddressType.SendTo

            Dim address As New ObjectModel.Collection(Of EmailAddress)
            address.Add(sendToAddress)

            Me.SendMailAsync(fromAddress, subject, message, bodyHtml, userToken, New ObjectModel.Collection(Of EmailAddress)(address))

        End Sub

        ''' <summary>
        ''' Sends mail async.
        ''' </summary>
        ''' <param name="message">Message object.</param>
        ''' <param name="userToken">User token for async.</param>
        Public Sub SendMailAsync(ByVal message As MailMessage, ByVal userToken As Object)
            Me.SendMailMessage(message, userToken)
        End Sub

        ''' <summary>
        ''' Sends email with the users default email application.
        ''' </summary>
        ''' <param name="subject">Subject of the email.</param>
        ''' <param name="message">Email message</param>
        ''' <param name="sendToAddress">Email address to send to.</param>
        ''' <exception cref="System.ArgumentException">sendToAddress</exception>
        Public Shared Sub SendMailWithDefaultProgram(ByVal subject As String, ByVal message As String, ByVal sendToAddress As EmailAddress)

            If Not sendToAddress.IsAddressValid Then
                Throw New ArgumentException(NameOf(sendToAddress))
            Else
                Using mailProcess As New Process()
                    Dim processInfo As New ProcessStartInfo() With {
                        .FileName = String.Format(CultureInfo.InvariantCulture, "mailto:{0}?subject={1}&body={2}", sendToAddress.Address, subject, message),
                        .UseShellExecute = True,
                        .WindowStyle = ProcessWindowStyle.Normal
                    }
                    mailProcess.StartInfo = processInfo
                    Process.Start(processInfo)
                End Using
            End If

        End Sub
#End Region

#Region "Private Methods"

        ''' <summary>
        ''' Creates the mail message.
        ''' </summary>
        ''' <param name="fromAddress">From address.</param>
        ''' <param name="subject">The subject.</param>
        ''' <param name="message">The message.</param>
        ''' <param name="bodyHtml">if set to <c>true</c> [body HTML].</param>
        ''' <param name="sendAddresses">The send addresses.</param>
        ''' <param name="tempMessage">The temporary message.</param>
        Private Shared Sub CreateMailMessage(ByVal fromAddress As EmailAddress, ByVal subject As String, ByVal message As String, ByVal bodyHtml As Boolean, ByVal sendAddresses As System.Collections.ObjectModel.Collection(Of EmailAddress), ByVal tempMessage As MailMessage)

            For Each tempSendTo As EmailAddress In From a In sendAddresses Where a.IsAddressValid
                Select Case tempSendTo.EmailAddressType
                    Case EmailAddressType.SendBcc
                        tempMessage.Bcc.Add(New MailAddress(tempSendTo.Address, tempSendTo.Name))
                    Case EmailAddressType.SendCC
                        tempMessage.CC.Add(New MailAddress(tempSendTo.Address, tempSendTo.Name))
                    Case Else
                        tempMessage.To.Add(New MailAddress(tempSendTo.Address, tempSendTo.Name))
                End Select
            Next

            tempMessage.From = New MailAddress(fromAddress.Address, fromAddress.Name)
            tempMessage.Subject = subject
            tempMessage.Body = message
            tempMessage.IsBodyHtml = bodyHtml

        End Sub

        Private Sub SendAsyncCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)

            RaiseEvent SendMailAsyncCompleted(Me, e)
            RemoveHandler _mailServer.SendCompleted, AddressOf SendAsyncCompleted

        End Sub
        Private Sub SendMailMessage(ByVal tempMessage As MailMessage, ByVal userToken As Object)

            If Not IsNothing(userToken) Then
                AddHandler _mailServer.SendCompleted, AddressOf SendAsyncCompleted
                Me._mailServer.SendAsync(tempMessage, userToken)
                Return
            End If

            Me._mailServer.Send(tempMessage)

        End Sub
#End Region

#Region "Public Properties"
        ''' <summary>
        ''' Timeout for the SMTP server.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Timeout() As Int32
            Get
                Return Me._mailServer.Timeout
            End Get
            Set(ByVal value As Int32)
                Me._mailServer.Timeout = value
            End Set
        End Property
#End Region

#Region "Constructors"
        ''' <summary>
        ''' Constructor
        ''' </summary>
        ''' <param name="host">SMTP host address (required)</param>
        ''' <param name="port">SMTP host port (required)</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal host As String, ByVal port As Int32)
            Me._mailServer = New SmtpClient(host, port)
        End Sub

        ''' <summary>
        ''' Constructor
        ''' </summary>
        ''' <param name="host">SMTP host address (required).</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal host As String)
            Me._mailServer = New SmtpClient(host)
        End Sub

#End Region

        Public Overloads Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        Private _disposed As Boolean

        ''' <summary>
        ''' Releases unmanaged and - optionally - managed resources
        ''' </summary>
        ''' <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
            If Not (Me._disposed) Then
                ' If disposing equals true, dispose all managed
                ' and unmanaged resources.
                If (disposing) Then
                    ' Dispose managed resources.
                    Me.DisposeFields()
                End If
            End If
            Me._disposed = True
        End Sub

        ''' <summary>
        ''' Allows an <see cref="T:System.Object" /> to attempt to free resources and perform other cleanup operations before the <see cref="T:System.Object" /> is reclaimed by garbage collection.
        ''' </summary>
        Protected Overrides Sub Finalize()
            Try
                Dispose(False)
            Finally
                MyBase.Finalize()
            End Try

        End Sub

    End Class
End Namespace