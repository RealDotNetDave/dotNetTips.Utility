'***********************************************************************
' Assembly         : dotNetTips
' Author           : David McCarter
' Created          : 12-10-2008
'
' Last Modified By : David McCarter
' Last Modified On : 06-09-2009
' Description      :
'
' Copyright        : (c) dotNetTips.com. All rights reserved.
'***********************************************************************
Imports System.Diagnostics.Contracts
''' <summary>
''' Validation helper class.
''' </summary>
''' <remarks></remarks>
Public Module ValidationHelper

    ''' <summary>
    ''' Validates value is a number.
    ''' </summary>
    ''' <param name="input">Text to validate.</param>
    ''' <returns>True if valid.</returns>
    ''' <remarks></remarks>
    Public Function IsValidNumber(ByVal input As String) As Boolean
        Contract.Requires(Of ArgumentNullException)(String.IsNullOrEmpty(input) = False)

        Return IsNumeric(input)

    End Function

End Module