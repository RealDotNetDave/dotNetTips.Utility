' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 12-04-2013
'
' Last Modified By : David McCarter
' Last Modified On : 12-04-2013
' ***********************************************************************
' <copyright file="ValidationHelper.vb" company="David McCarter Consulting">
'     David McCarter Consulting. All rights reserved.
' </copyright>
' <summary></summary>
' ***********************************************************************
Namespace Extensions
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

            Return If(String.IsNullOrEmpty(input), False, IsNumeric(input))

        End Function
    End Module
End Namespace