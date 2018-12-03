using System.Data.Entity;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;

namespace dotNetTips.Utility.EntityFramework.Extensions
{


    namespace Extensions
    {

        /// <summary>
        ///     ''' Extensions for the Entity Framework
        ///     ''' </summary>
        public static class EntityExtensions
        {
            /// <summary>
            ///         ''' Retrieves the foreign key.
            ///         ''' </summary>
            ///         ''' <param name="ref">The Entity reference.</param>
            ///         ''' <returns>System.Int32.</returns>
            public static int ForeignKey(this EntityReference @ref)
            {
                int key = 0;

                return int.TryParse(@ref.EntityKey.EntityKeyValues(0).Value.ToString(), ref key) ? key : 0;
            }

            /// <summary>
            ///         ''' Loads the entity collection if it hasn't already been loaded
            ///         ''' </summary>
            ///         ''' <typeparam name="T">Type of entity collection</typeparam>
            ///         ''' <param name="entityCollection">Entity collection to potentially load entities into</param>
            ///         ''' <param name="entitySource">The source entity which has the entity collection relationship (modified or unchanged only)</param>
            public static void EnsureLoaded<T>(this EntityCollection<T> entityCollection, EntityObject entitySource) where T : class, IEntityWithRelationships
            {
                if (entitySource != null && entityCollection != null && !entityCollection.IsLoaded)
                {
                    if (entitySource.EntityState == EntityState.Modified || entitySource.EntityState == EntityState.Unchanged)
                        entityCollection.Load();
                }
            }

            // <Extension>
            // Public Function HasEntityKeyFirstValue(Of T As {Class, IEntityWithRelationships})(entityReference As EntityReference(Of T)) As Boolean
            // Return entityReference IsNot Nothing AndAlso entityReference.EntityKey.HasFirstValue(Of Integer)()
            // End Function

            // <Extension>
            // Public Function GetEntityKeyFirstValue(Of T As {Class, IEntityWithRelationships})(entityReference As EntityReference(Of T)) As Integer
            // If entityReference IsNot Nothing Then
            // Return entityReference.EntityKey.EntityKeyValues(GetEntityKeyFirstValue(Of Integer)()
            // End If
            // Return 0
            // End Function

            /// <summary>
            ///         ''' Gets the first entity key value
            ///         ''' </summary>
            ///         ''' <typeparam name="T"></typeparam>
            ///         ''' <param name="entityKey">The entity key.</param>
            ///         ''' <returns>the first entity key value</returns>
            public static T GetFirstValue<T>(this System.Data.Entity.Core.EntityKey entityKey)
            {
                if (entityKey != null && entityKey.EntityKeyValues != null && entityKey.EntityKeyValues.Length > 0)
                    return (T)entityKey.EntityKeyValues.First().Value;
                return default(T);
            }

            /// <summary>
            ///         ''' Sets the first entity key value
            ///         ''' </summary>
            ///         ''' <typeparam name="T"></typeparam>
            ///         ''' <param name="entityKey">The entity key.</param>
            ///         ''' <param name="value">The value.</param>
            public static void SetFirstValue<T>(this System.Data.Entity.Core.EntityKey entityKey, T value)
            {
                if (entityKey != null && entityKey.EntityKeyValues != null && entityKey.EntityKeyValues.Length > 0)
                    entityKey.EntityKeyValues.First().Value = value;
                return;
            }

            /// <summary>
            ///         ''' Whether or not the entity key has a first value
            ///         ''' </summary>
            ///         ''' <typeparam name="T"></typeparam>
            ///         ''' <param name="entityKey">The entity key.</param>
            ///         ''' <returns><c>true</c> if [has first value] [the specified entity key]; otherwise, <c>false</c>.</returns>
            public static bool HasFirstValue<T>(this System.Data.Entity.Core.EntityKey entityKey)
            {
                return (!GetFirstValue<T>(entityKey).Equals(null));
            }
        }
    }
}
