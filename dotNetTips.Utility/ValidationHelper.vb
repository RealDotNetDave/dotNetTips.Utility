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

Imports dotNetTips.Utility.Portable.OOP
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
        Encapsulation.TryValidateParam(Of ArgumentNullException)(String.IsNullOrEmpty(input) = False)

        Return IsNumeric(input)

    End Function

End Module