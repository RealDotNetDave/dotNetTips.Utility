' ***********************************************************************
' Assembly         : dotNetTips.Utility.Portable
' Author           : David McCarter
' Created          : 12-04-2013
'
' Last Modified By : David McCarter
' Last Modified On : 12-04-2013
' ***********************************************************************
' <copyright file="MathHelper.vb" company="David McCarter Consulting">
'     David McCarter Consulting. All rights reserved.
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Globalization
Namespace Math
    ''' <summary>
    ''' Math helper class.
    ''' </summary>
    ''' <remarks></remarks>
    Public Module MathHelper

        ''' <summary>
        ''' Converts decimal and returns places after decimal point.
        ''' </summary>
        ''' <param name="d">Decimal value to convert.</param>
        ''' <param name="digits">Places after the decimal to return.</param>
        ''' <returns>Converted decimal.</returns>
        ''' <remarks></remarks>
        Public Function ConvertDecimal(ByVal d As Decimal, ByVal digits As Int32) As Decimal
            Dim convertedValue As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim tempValue As String = Convert.ToString(d, CultureInfo.CurrentCulture)
            Dim tempSplit() As String = tempValue.Split(Convert.ToChar(".", CultureInfo.CurrentCulture))

            If (tempSplit.GetLowerBound(0) = 0) Then
                convertedValue.Append(tempSplit(0))
            End If

            If (tempSplit.GetUpperBound(0) = 1) Then
                convertedValue.Append(".")
                convertedValue.Append(tempSplit(1).Substring(0, digits))
            End If

            Return Convert.ToDecimal(convertedValue.ToString(), CultureInfo.CurrentCulture)

        End Function

        ''' <summary>
        ''' Calculates the kilobytes.
        ''' </summary>
        ''' <param name="d">Decimal number to be calculated.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function CalculateKilobytes(ByVal d As Decimal) As Decimal
            Return CalculateKilobytes(d, 0)
        End Function

        ''' <summary>
        ''' Calculates the kilobytes.
        ''' </summary>
        ''' <param name="d">Decimal number to be calculated.</param>
        ''' <param name="digits">The number of decimal places in the return value.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function CalculateKilobytes(ByVal d As Decimal, ByVal digits As Int32) As Decimal
            Contract.Requires(Of ArgumentOutOfRangeException)(digits >= 0 AndAlso digits <= 15, "Must be in range 0-15")

            Return CDec(System.Math.Round(d / 1024.0, digits))
        End Function
    End Module
End Namespace