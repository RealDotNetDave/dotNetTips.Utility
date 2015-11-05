Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary

Public Module ObjectExtensions
  ''' <summary>
  ''' Ins the specified object compare from.
  ''' </summary>
  ''' <typeparam name="T"></typeparam>
  ''' <param name="objCompareFrom">The object compare from.</param>
  ''' <param name="objCompareTo">The object compare to.</param>
  ''' <returns></returns>
  ''' <remarks>Code by: Brian Wigfield</remarks>
  <System.Runtime.CompilerServices.Extension()>
  Public Function [In](Of T)(ByVal objCompareFrom As T, ByVal ParamArray objCompareTo As T()) As Boolean
    For Each compareToSingle As T In objCompareTo.AsParallel
      If objCompareFrom.Equals(compareToSingle) Then
        Return True
      End If
    Next
    Return False
  End Function

  ''' <summary>
  ''' Makes a copy from the object.
  ''' Doesn't copy the reference memory, only data.
  ''' </summary>
  ''' <typeparam name="T">Type of the return object.</typeparam>
  ''' <param name="item">Object to be copied.</param>
  ''' <returns>Returns the copied object.</returns>
  ''' <remarks>Original code by: Carlos Alessandro Ribeiro</remarks>
  <System.Runtime.CompilerServices.Extension>
  Public Function Clone(Of T)(item As Object) As T
    If item IsNot Nothing Then
      Dim formatter As New BinaryFormatter()
      Using stream As New MemoryStream()

        formatter.Serialize(stream, item)
        stream.Seek(0, SeekOrigin.Begin)

        Dim result As T = DirectCast(formatter.Deserialize(stream), T)

        Return result
      End Using
    End If

    Return Nothing
  End Function
End Module