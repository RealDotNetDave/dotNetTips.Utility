' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 08-17-2013
'
' Last Modified By : David McCarter
' Last Modified On : 08-24-2013
' ***********************************************************************
' <copyright file="CollectionExtensions.vb" company="David McCarter Consulting">
'     David McCarter Consulting. All rights reserved.
' </copyright>
' <summary></summary>
' ***********************************************************************
Namespace Extensions
    ''' <summary>
    '''
    ''' </summary>
    Public Module CollectionExtensions

        ''' <summary>
        ''' Splits the specified list.
        ''' </summary>
        ''' <param name="list">The list.</param>
        ''' <param name="groupCount">The group count.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <System.Runtime.CompilerServices.Extension()>
        Public Function Split(list As IList, groupCount As Integer) As IEnumerable
            Contract.Requires(Of ArgumentNullException)(list IsNot Nothing)

            Return SplitList(Of IList)(CType(list, Global.System.Collections.Generic.List(Of Global.System.Collections.IList)), groupCount)
        End Function

        ''' <summary>
        ''' Determines whether the specified list is valid.
        ''' </summary>
        ''' <param name="list">The list.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <System.Runtime.CompilerServices.Extension()>
        Public Function IsValid(list As IList) As Boolean
            Return list IsNot Nothing AndAlso list.Count > 0
        End Function

        ''' <summary>
        ''' Determines whether the specified list is valid.
        ''' </summary>
        ''' <param name="list">The list.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <System.Runtime.CompilerServices.Extension()>
        Public Function IsValid(list As IEnumerable) As Boolean
            Return list.IsValid()
        End Function

        Private Function SplitList(Of T)(items As List(Of T), groupCount As Integer) As IEnumerable(Of IEnumerable(Of T))
            Contract.Requires(Of ArgumentNullException)(items IsNot Nothing)

            Dim allGroups As New List(Of List(Of T))()

            'split the list into equal groups
            Dim startIndex As Integer = 0
            Dim groupLength As Integer = CInt(System.Math.Round(CDbl(items.Count) / CDbl(groupCount), 0))
            While startIndex < items.Count
                Dim group As New List(Of T)()
                group.AddRange(items.GetRange(startIndex, groupLength))
                startIndex += groupLength

                'adjust group-length for last group
                If startIndex + groupLength > items.Count Then
                    groupLength = items.Count - startIndex
                End If

                allGroups.Add(group)
            End While

            'merge last two groups, if more than required groups are formed
            If allGroups.Count > groupCount AndAlso allGroups.Count > 2 Then
                allGroups(allGroups.Count - 2).AddRange(allGroups.Last())
                allGroups.RemoveAt(allGroups.Count - 1)
            End If

            Return (allGroups.AsEnumerable())
        End Function
    End Module
End Namespace