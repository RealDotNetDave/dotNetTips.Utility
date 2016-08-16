' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 03-29-2016
'
' Last Modified By : David McCarter
' Last Modified On : 01-26-2016
' ***********************************************************************
' <copyright file="EntityExtensions.vb" company="NicheWare - David McCarter">
'     NicheWare - David McCarter
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Runtime.CompilerServices
Imports System.Data.Objects.DataClasses

Namespace Extensions

    ''' <summary>
    ''' Extensions for the Entity Framework
    ''' </summary>
    Public Module EntityExtensions
        ''' <summary>
        ''' Retrieves the foreign key.
        ''' </summary>
        ''' <param name="ref">The Entity reference.</param>
        ''' <returns>System.Int32.</returns>
        <Extension> _
        Public Function ForeignKey(ByVal ref As EntityReference) As Integer
            Dim key As Integer = 0

            Return If(Integer.TryParse(ref.EntityKey.EntityKeyValues(0).Value.ToString(), key), key, 0)
        End Function

        ''' <summary>
        ''' Loads the entity collection if it hasn't already been loaded
        ''' </summary>
        ''' <typeparam name="T">Type of entity collection</typeparam>
        ''' <param name="entityCollection">Entity collection to potentially load entities into</param>
        ''' <param name="entitySource">The source entity which has the entity collection relationship (modified or unchanged only)</param>
        <Extension>
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
        ''' <typeparam name="T"></typeparam>
        ''' <param name="entityReference">The entity reference.</param>
        ''' <returns><c>true</c> if [has entity key first value] [the specified entity reference]; otherwise, <c>false</c>.</returns>
        <Extension>
        Public Function HasEntityKeyFirstValue(Of T As {Class, IEntityWithRelationships})(entityReference As EntityReference(Of T)) As Boolean
            Return entityReference IsNot Nothing AndAlso entityReference.EntityKey.HasFirstValue(Of Integer)()
        End Function

        ''' <summary>
        ''' Get entity key with a value present
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="entityReference">The entity reference.</param>
        ''' <returns>System.Int32.</returns>
        <Extension>
        Public Function GetEntityKeyFirstValue(Of T As {Class, IEntityWithRelationships})(entityReference As EntityReference(Of T)) As Integer
            If entityReference IsNot Nothing Then
                Return entityReference.EntityKey.GetFirstValue(Of Integer)()
            End If
            Return 0
        End Function

        ''' <summary>
        ''' Gets the first entity key value
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="entityKey">The entity key.</param>
        ''' <returns>the first entity key value</returns>
        <Extension> _
        Public Function GetFirstValue(Of T)(entityKey As EntityKey) As T
            If entityKey IsNot Nothing AndAlso entityKey.EntityKeyValues IsNot Nothing AndAlso entityKey.EntityKeyValues.Length > 0 Then
                Return DirectCast(entityKey.EntityKeyValues.First().Value, T)
            End If
            Return Nothing
        End Function

        ''' <summary>
        ''' Sets the first entity key value
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="entityKey">The entity key.</param>
        ''' <param name="value">The value.</param>
        <Extension>
        Public Sub SetFirstValue(Of T)(entityKey As EntityKey, value As T)
            If entityKey IsNot Nothing AndAlso entityKey.EntityKeyValues IsNot Nothing AndAlso entityKey.EntityKeyValues.Length > 0 Then
                entityKey.EntityKeyValues.First().Value = value
            End If
            Return
        End Sub

        ''' <summary>
        ''' Whether or not the entity key has a first value
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="entityKey">The entity key.</param>
        ''' <returns><c>true</c> if [has first value] [the specified entity key]; otherwise, <c>false</c>.</returns>
        <Extension>
        Public Function HasFirstValue(Of T)(entityKey As EntityKey) As Boolean
            Return (Not GetFirstValue(Of T)(entityKey).Equals(Nothing))
        End Function

    End Module

End Namespace