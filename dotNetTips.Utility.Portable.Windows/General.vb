' ***********************************************************************
' Assembly         : dotNetTips.Utility.Portable
' Author           : David McCarter
' Created          : 12-04-2013
'
' Last Modified By : David McCarter
' Last Modified On : 02-03-2014
' ***********************************************************************
' <copyright file="General.vb" company="David McCarter Consulting">
'     David McCarter Consulting. All rights reserved.
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Reflection
Imports System.IO

''' <summary>
''' General helper functions.
''' </summary>
Public Module General

    ''' <summary>
    ''' Does the object equal instance.
    ''' </summary>
    ''' <param name="value">The object.</param>
    ''' <param name="instance">The instance.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DoesObjectEqualInstance(ByVal value As [Object], ByVal instance As Object) As Boolean
        Contract.Requires(Of ArgumentNullException)(value IsNot Nothing)
        Contract.Requires(Of ArgumentNullException)(instance IsNot Nothing)

        Dim result = Object.ReferenceEquals(value, instance)

        Return result

    End Function

    ''' <summary>
    ''' Gets the instance hash code.
    ''' </summary>
    ''' <param name="instance">The instance.</param>
    ''' <returns>Int32.</returns>
    Public Function GetInstanceHashCode(ByVal instance As Object) As Int32
        Contract.Requires(Of ArgumentNullException)(instance IsNot Nothing)

        Dim hash As Int32

        For Each prop As PropertyInfo In instance.GetType.GetRuntimeProperties().Where(Function(p) p IsNot Nothing).AsParallel
            Dim value = prop.GetValue(instance)
            If value IsNot Nothing Then
                hash = hash Xor value.GetHashCode
            End If
        Next

        Return hash
    End Function

    ''' <summary>
    ''' Modified IIf function that works properly!
    ''' </summary>
    ''' <typeparam name="T">Type to return.</typeparam>
    ''' <param name="expression">if set to <c>true</c> [expression].</param>
    ''' <param name="truePart">The true part.</param>
    ''' <param name="falsePart">The false part.</param>
    ''' <returns>Defined type.</returns>
    Public Function IIf(Of T)(ByVal expression As Boolean, ByVal truePart As T, ByVal falsePart As T) As T
        Return If(expression, truePart, falsePart)
    End Function

    ''' <summary>
    ''' To the four digit year.
    ''' </summary>
    ''' <param name="year">The year.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ToFourDigitYear(year As Int32) As Int32
        Contract.Requires(Of ArgumentNullException)(year >= 0)
        Contract.Ensures(Contract.Result(Of Int32)() >= 0)

        Dim result As Int32 = If(year >= 100, year, (2 \ 100 - (If(year > 2 Mod 100, 1, 0))) * 100 + year)

        Return result

    End Function
End Module