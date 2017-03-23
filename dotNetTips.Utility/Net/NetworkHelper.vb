' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : dotnetdave
' Created          : 04-15-2016
'
' Last Modified By : dotnetdave
' Last Modified On : 04-15-2016
' ***********************************************************************
' <copyright file="NetworkHelper.vb" company="">
'     . All rights reserved.
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Net.NetworkInformation

Namespace Net
    ''' <summary>
    ''' Various network helper methods.
    ''' </summary>
    Public Module NetworkHelper

        ''' <summary>
        ''' Determines there is an internet connection available.
        ''' </summary>
        ''' <returns>True if a internet site can be reached.</returns>
        Public Function IsInternetConnectionAvailable() As Boolean
            Dim success = False

            If My.Computer.Network.IsAvailable = False Then
                Return success
            End If

            Dim sitesList As String() = {"www.google.com", "www.microsoft.com", "www.yahoo.com"}

            For Each url In sitesList.AsParallel()
                If My.Computer.Network.Ping(url, 300) Then
                    success = True
                    Exit For
                End If
            Next

            Return success

        End Function

    End Module
End Namespace