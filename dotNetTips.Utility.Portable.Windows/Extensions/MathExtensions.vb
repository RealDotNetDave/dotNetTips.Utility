Imports System
Imports System.Runtime.CompilerServices

Namespace Extensions
    ''' <summary>
    ''' Extensions for math functions.
    ''' </summary>
    ''' <remarks></remarks>
    Public Module MathExtensions
        ''' <summary>
        ''' Rounds the specified value.
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <returns></returns>
        ''' <remarks>
        ''' Code by: Lucas
        ''' http://code.msdn.microsoft.com/LucasExtensions
        ''' </remarks>
        <Extension> _
        Public Function Round(ByVal value As Double) As Integer
            Return CInt(System.Math.Round(value))
        End Function

        ''' <summary>
        ''' Rounds the specified value.
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <param name="digits">The digits.</param>
        ''' <returns></returns>
        ''' <remarks>
        ''' Code by: Lucas
        ''' http://code.msdn.microsoft.com/LucasExtensions
        ''' </remarks>
        <Extension> _
        Public Function Round(ByVal value As Double, ByVal digits As Integer) As Integer
            Return CInt(System.Math.Round(value, digits))
        End Function

        ''' <summary>
        ''' Rounds the specified value.
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <returns></returns>
        ''' <remarks>
        ''' Code by: Lucas
        ''' http://code.msdn.microsoft.com/LucasExtensions
        ''' </remarks>
        <Extension> _
        Public Function Round(ByVal value As Decimal) As Integer
            Return CInt(System.Math.Round(value))
        End Function

        ''' <summary>
        ''' Rounds the specified value.
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <param name="digits">The digits.</param>
        ''' <returns></returns>
        ''' <remarks>
        ''' Code by: Lucas
        ''' http://code.msdn.microsoft.com/LucasExtensions
        ''' </remarks>
        <Extension> _
        Public Function Round(ByVal value As Decimal, ByVal digits As Integer) As Integer
            Return CInt(System.Math.Round(value, digits))
        End Function

    End Module
End Namespace