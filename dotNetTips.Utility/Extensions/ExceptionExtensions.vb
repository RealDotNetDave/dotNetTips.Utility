Imports System.Runtime.CompilerServices

Public Module ExceptionExtensions
    ''' <summary>
    ''' Returns the first inner exception you are looking for or null if not found.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="ex"></param>
    ''' <returns></returns>
    ''' <remarks>Code by: Brian Wigfield</remarks>
    <Extension()>
    Public Function TraverseFor(Of T As Class)(ByVal ex As Exception) As T
        If ex Is Nothing Then
            Return Nothing
        End If
        If ex.GetType Is GetType(T) Then
            Return TryCast(ex, T)
        End If

        Return TryCast(ex.InnerException.TraverseFor(Of T)(), T)

    End Function
End Module