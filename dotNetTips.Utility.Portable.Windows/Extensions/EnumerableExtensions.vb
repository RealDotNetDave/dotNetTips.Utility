'***********************************************************************
' Assembly         : dotNetTips
' Author           : David McCarter
' Created          : 12-10-2008
'
' Last Modified By : David McCarter
' Last Modified On : 06-09-2009
' Description      :
'
' Copyright        : (c) dotNetTips.com. All rights reserved.
'***********************************************************************
Imports System
Imports System.Collections.Generic
Imports System.Runtime.CompilerServices

Namespace Extensions
    ''' <summary>
    ''' Extensions for enums.
    ''' </summary>
    ''' <remarks></remarks>
    Public Module EnumerableExtensions

        ''' <summary>
        ''' Loops over the enum.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="values">The values.</param>
        ''' <param name="action">The action.</param>
        ''' <remarks>
        ''' Code by: Lucas
        ''' http://code.msdn.microsoft.com/LucasExtensions
        ''' </remarks>
        <Extension> _
        Public Sub ForEach(Of T)(ByVal values As IEnumerable(Of T), ByVal action As Action(Of T))
            Contract.Requires(Of ArgumentNullException)(action IsNot Nothing)

            For Each value As T In values
                action(value)
            Next
        End Sub

    End Module
End Namespace