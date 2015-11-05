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
Imports System.Runtime.CompilerServices
Imports System.Data.Objects.DataClasses
Imports System.Diagnostics.Contracts

Namespace Extensions

    ''' <summary>
    ''' Extensions for the Entity Framework
    ''' </summary>
    ''' <remarks></remarks>
    Public Module EntityExtensions
        ''' <summary>
        ''' Retrieves the foreign key.
        ''' </summary>
        ''' <param name="ref">The Entity reference.</param>
        ''' <returns></returns>
        <Extension()> _
        Public Function ForeignKey(ByVal ref As EntityReference) As Integer
            Contract.Requires(Of ArgumentNullException)(ref IsNot Nothing)

            Dim key As Integer = 0

            If Integer.TryParse(ref.EntityKey.EntityKeyValues(0).Value.ToString(), key) Then
                Return key
            Else
                Return 0
            End If
        End Function

        ''' <summary>
        ''' Loads the entity collection if it hasn't already been loaded
        ''' </summary>
        ''' <typeparam name="T">Type of entity collection</typeparam>
        ''' <param name="entityCollection">Entity collection to potentially load entities into</param>
        ''' <param name="entitySource">The source entity which has the entity collection relationship (modified or unchanged only)</param>
        <System.Runtime.CompilerServices.Extension> _
        Public Sub EnsureLoaded(Of T As {Class, IEntityWithRelationships})(entityCollection As EntityCollection(Of T), entitySource As EntityObject)
            If entitySource IsNot Nothing AndAlso entityCollection IsNot Nothing AndAlso Not entityCollection.IsLoaded Then
                If entitySource.EntityState = System.Data.EntityState.Modified OrElse entitySource.EntityState = System.Data.EntityState.Unchanged Then
                    entityCollection.Load()
                End If
            End If
        End Sub

        ''' <summary>
        ''' Whether or not the entity reference has an entity key with a value present
        ''' </summary>
        <System.Runtime.CompilerServices.Extension> _
        Public Function HasEntityKeyFirstValue(Of T As {Class, IEntityWithRelationships})(entityReference As EntityReference(Of T)) As Boolean
            Return entityReference IsNot Nothing AndAlso entityReference.EntityKey.HasFirstValue(Of Integer)()
        End Function

        ''' <summary>
        ''' Get entity key with a value present
        ''' </summary>
        <System.Runtime.CompilerServices.Extension> _
        Public Function GetEntityKeyFirstValue(Of T As {Class, IEntityWithRelationships})(entityReference As EntityReference(Of T)) As Integer
            If entityReference IsNot Nothing Then
                Return entityReference.EntityKey.GetFirstValue(Of Integer)()
            End If
            Return 0
        End Function

        ''' <summary>
        ''' Gets the first entity key value
        ''' </summary>
        ''' <returns>the first entity key value</returns>
        <System.Runtime.CompilerServices.Extension> _
        Public Function GetFirstValue(Of T)(entityKey As EntityKey) As T
            If entityKey IsNot Nothing AndAlso entityKey.EntityKeyValues IsNot Nothing AndAlso entityKey.EntityKeyValues.Length > 0 Then
                Return DirectCast(entityKey.EntityKeyValues.First().Value, T)
            End If
            Return Nothing
        End Function

        ''' <summary>
        ''' Sets the first entity key value
        ''' </summary>
        <System.Runtime.CompilerServices.Extension> _
        Public Sub SetFirstValue(Of T)(entityKey As EntityKey, value As T)
            If entityKey IsNot Nothing AndAlso entityKey.EntityKeyValues IsNot Nothing AndAlso entityKey.EntityKeyValues.Length > 0 Then
                entityKey.EntityKeyValues.First().Value = value
            End If
            Return
        End Sub

        ''' <summary>
        ''' Whether or not the entity key has a first value
        ''' </summary>
        <System.Runtime.CompilerServices.Extension> _
        Public Function HasFirstValue(Of T)(entityKey As EntityKey) As Boolean
            Return (Not GetFirstValue(Of T)(entityKey).Equals(Nothing))
        End Function

    End Module

End Namespace