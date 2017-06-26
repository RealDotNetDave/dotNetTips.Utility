' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : david
' Created          : 02-12-2017
'
' Last Modified By : david
' Last Modified On : 03-04-2017
' ***********************************************************************
' <copyright file="ValidationHelper.vb" company="McCarter Consulting - David McCarter">
'     David McCarter - dotNetTips.com © 2017
' </copyright>
' <summary></summary>
' ***********************************************************************

Imports dotNetTips.Utility.Portable.OOP
''' <summary>
''' Validation helper class.
''' </summary>
Public Module ValidationHelper

    ''' <summary>
    ''' Validates value is a number.
    ''' </summary>
    ''' <param name="input">Text to validate.</param>
    ''' <returns>True if valid.</returns>
    Public Function IsValidNumber(ByVal input As String) As Boolean
        Encapsulation.TryValidateParam(Of ArgumentNullException)(String.IsNullOrEmpty(input) = False)

        Return IsNumeric(input)

    End Function

End Module