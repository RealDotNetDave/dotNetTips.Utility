' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 12-04-2013
'
' Last Modified By : David McCarter
' Last Modified On : 05-15-2014
' ***********************************************************************
' <copyright file="DataServicesExtensions.vb" company="David McCarter Consulting">
'     David McCarter Consulting. All rights reserved.
' </copyright>
' <summary></summary>
' ***********************************************************************

Imports System.Collections.Generic
Imports System.Data.Services
Namespace Extensions
    ''' <summary>
    '''
    ''' </summary>
    Public Module DataServicesExtensions
        ''' <summary>
        ''' Copies the request header values.
        ''' </summary>
        ''' <param name="args">The args.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <System.Runtime.CompilerServices.Extension()> _
        Public Function CopyRequestHeaderValues(args As ProcessRequestArgs) As SortedDictionary(Of String, String)
            Contracts.Contract.Requires(Of ArgumentNullException)(args IsNot Nothing)

            Dim requestHeaderValues As New SortedDictionary(Of String, String)

            If args.OperationContext.RequestHeaders IsNot Nothing AndAlso args.OperationContext.RequestHeaders.Count > 0 Then
                For Each key In args.OperationContext.RequestHeaders.AllKeys
                    requestHeaderValues.Add(key, args.OperationContext.RequestHeaders.[Get](key))
                Next
            End If

            Return requestHeaderValues

        End Function
    End Module
End Namespace