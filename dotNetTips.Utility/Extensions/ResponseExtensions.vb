'***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : dm11086
' Created          : 10-13-2010
'
' Last Modified By : dm11086
' Last Modified On : 10-13-2010
' Description      :
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports System.Runtime.CompilerServices
Imports System.Web

Namespace Extensions
    ''' <summary>
    ''' Extensions for HttpResponse objects.
    ''' </summary>
    ''' <remarks></remarks>
    Public Module ResponseExtensions
        ''' <summary>
        ''' Redirects the specified ref.
        ''' </summary>
        ''' <param name="value">The ref.</param>
        ''' <param name="page">The page.</param>
        ''' <remarks></remarks>
        <Extension()> _
        Public Sub Redirect(ByVal value As System.Web.HttpResponse, ByVal page As String)
            Contracts.Contract.Requires(Of ArgumentNullException)(value IsNot Nothing)
            Contracts.Contract.Requires(Of ArgumentNullException)(String.IsNullOrWhiteSpace(page) = False)

            value.Redirect(page, False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Sub
    End Module
End Namespace