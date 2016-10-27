' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 08-03-2015
'
' Last Modified By : David McCarter
' Last Modified On : 07-14-2016
' ***********************************************************************
' <copyright file="ObjectExtensions.vb" company="dotNetTips.com">
'     '     '     dotNetTips.com. All rights reserved.
'
'
' </copyright>
' <summary></summary>
' *************************************************************************
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Runtime.Serialization.Formatters.Binary
''' <summary>
''' Class ObjectExtensions.
''' </summary>
Public Module ObjectExtensions

    ''' <summary>
    ''' Makes a copy from the object.
    ''' Doesn't copy the reference memory, only data.
    ''' </summary>
    ''' <typeparam name="T">Type of the return object.</typeparam>
    ''' <param name="item">Object to be copied.</param>
    ''' <returns>Returns the copied object.</returns>
    ''' <exception cref="System.InvalidOperationException">The provided object is not serializable.</exception>
    ''' <remarks>Original code by: Carlos Alessandro Ribeiro. Do not use this for collections. Instead use .Copy().</remarks>
    <Extension>
    Public Function Clone(Of T As New)(item As T) As T

        If Object.ReferenceEquals(item, Nothing) Then
            Return Nothing
        End If

        Dim attributes = item.[GetType]().GetCustomAttributes(GetType(SerializableAttribute), False)
        If attributes Is Nothing Then
            Throw New InvalidOperationException("The provided object is not serializable.")
        End If

        Using stream As New MemoryStream()
            Dim formatter As New BinaryFormatter()
            formatter.Serialize(stream, item)
            stream.Seek(0, SeekOrigin.Begin)

            Dim result As T = DirectCast(formatter.Deserialize(stream), T)

            Return result
        End Using

        Return Nothing
    End Function

End Module