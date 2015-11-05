'***********************************************************************
' Assembly         : dotNetTips
' Author           : David McCarter
' Created          : 12-10-2008
'
' Last Modified By : David McCarter
' Last Modified On : 09-27-2008
' Description      :
'
' Copyright        : (c) dotNetTips.com. All rights reserved.
'***********************************************************************
Imports System.Runtime.CompilerServices

Namespace Extensions

    ''' <summary>
    ''' Extensions for the Decimal type.
    ''' </summary>
    ''' <remarks></remarks>
    Public Module DecimalExtensions

        ''' <summary>
        ''' Converts the decimal.
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <param name="placesAfter">The places after.</param>
        ''' <returns></returns>
        <Extension()> _
        Public Function ConvertDecimal(ByVal value As Decimal, ByVal placesAfter As Int32) As Decimal
            Return Math.MathHelper.ConvertDecimal(value, placesAfter)
        End Function

    End Module

End Namespace