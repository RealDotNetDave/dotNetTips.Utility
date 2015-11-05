Imports System.Runtime.CompilerServices

Public Module IntegerExtensions
    ''' <summary>
    ''' Noes the duplicates.
    ''' </summary>
    ''' <param name="values">The values.</param>
    ''' <returns></returns>
    ''' <remarks>Code by: Kevin S Gallagher</remarks>
    <Extension()>
    Public Function NoDuplicates(ByVal values As Integer()) As Integer()
        Return (From value In values Select value Distinct Order By value).ToArray
    End Function

    ''' <summary>
    ''' Determines if the Integer is of the specified interval.  E.g. if the interval is 100 and the integer is 400, it would return true.
    ''' This function uses the Mod operator, for the above example: (300 Mod 100 = 0)
    ''' </summary>
    ''' <param name="num"></param>
    ''' <param name="interval"></param>
    ''' <returns></returns>
    ''' <remarks>Code by: Blake Pell</remarks>
    <Extension()>
    Public Function IsInterval(ByVal num As Integer, ByVal interval As Integer) As Boolean
        If num Mod interval = 0 Then
            Return True
        Else
            Return False
        End If
    End Function
End Module