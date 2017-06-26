' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 03-29-2016
'
' Last Modified By : David McCarter
' Last Modified On : 01-23-2016
' ***********************************************************************
' <copyright file="ResponseExtensions.vb" company="NicheWare - David McCarter">
'     NicheWare - David McCarter
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Runtime.CompilerServices
Imports System.Web
Imports dotNetTips.Utility.Portable.OOP

Namespace Extensions
    ''' <summary>
    ''' Extensions for HttpResponse objects.
    ''' </summary>
    Public Module ResponseExtensions
        ''' <summary>
        ''' Redirects the specified ref.
        ''' </summary>
        ''' <param name="value">The ref.</param>
        ''' <param name="page">The page.</param>
        <Extension>
        Public Sub Redirect(ByVal value As System.Web.HttpResponse, ByVal page As String)
            Encapsulation.TryValidateParam(Of ArgumentNullException)(String.IsNullOrWhiteSpace(page) = False)

            value.Redirect(page, False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Sub
    End Module
End Namespace