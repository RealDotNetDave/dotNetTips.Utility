' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 07-07-2017
'
' Last Modified By : David McCarter
' Last Modified On : 07-07-2017
' ***********************************************************************
' <copyright file="DataContext.vb" company="McCarter Consulting - David McCarter">
'     David McCarter - dotNetTips.com © 2017
' </copyright>
' <summary></summary>
' *************************************************************************
Imports dotNetTips.Utility.Portable.Data
Imports System.Data.Entity
Imports System.Reflection
Imports System.Threading
Imports System.Threading.Tasks

Namespace Data
    ''' <summary>
    ''' Class DataContext.
    ''' </summary>
    ''' <seealso cref="System.Data.Entity.DbContext" />
    Public MustInherit Class DataContext
        Inherits DbContext

        ''' <summary>
        ''' Constructs a new context instance using the given string as the name or connection string for the
        ''' database to which a connection will be made.
        ''' See the class remarks for how this is used to create a connection.
        ''' </summary>
        ''' <param name="nameOrConnection">Either the database name or a connection string.</param>
        Protected Sub New(nameOrConnection As String)
            MyBase.New(nameOrConnection)
            Configuration.LazyLoadingEnabled = False
            Configuration.ValidateOnSaveEnabled = True

#If DEBUG Then
        Me.Database.Log = Sub(s) System.Diagnostics.Debug.WriteLine(s)
#End If
        End Sub

        ''' <summary>
        ''' Updates the entities.
        ''' </summary>
        Private Sub UpdateEntities()

            If ChangeTracker.HasChanges = False Then
                Return
            End If

            For Each tempEntity In ChangeTracker.Entries(Of IDataEntity)().Where(Function(p) p.State <> EntityState.Unchanged).AsParallel()
                If tempEntity.State = EntityState.Added Then
                    tempEntity.Entity.CreatedAt = DateTime.Now
                    tempEntity.Entity.PublicKey = Guid.NewGuid
                Else
                    tempEntity.Entity.UpdatedAt = DateTime.Now
                End If
            Next
        End Sub

        ''' <summary>
        ''' This method is called when the model for a derived context has been initialized, but
        ''' before the model has been locked down and used to initialize the context.  The default
        ''' implementation of this method does nothing, but it can be overridden in a derived class
        ''' such that the model can be further configured before it is locked down.
        ''' </summary>
        ''' <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        ''' <remarks>Typically, this method is called only once when the first instance of a derived context
        ''' is created.  The model for that context is then cached and is for all further instances of
        ''' the context in the app domain.  This caching can be disabled by setting the ModelCaching
        ''' property on the given ModelBuilder, but note that this can seriously degrade performance.
        ''' More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        ''' classes directly.</remarks>
        Protected Overrides Sub OnModelCreating(modelBuilder As DbModelBuilder)
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly())

            MyBase.OnModelCreating(modelBuilder)
        End Sub

        ''' <summary>
        ''' Saves all changes made in this context to the underlying database.
        ''' </summary>
        ''' <returns>The number of state entries written to the underlying database. This can include
        ''' state entries for entities and/or relationships. Relationship state entries are created for
        ''' many-to-many relationships and relationships where there is no foreign key property
        ''' included in the entity class (often referred to as independent associations).</returns>
        Public Overrides Function SaveChanges() As Integer
            UpdateEntities()

            Return MyBase.SaveChanges()
        End Function
        ''' <summary>
        ''' Asynchronously saves all changes made in this context to the underlying database.
        ''' </summary>
        ''' <returns>A task that represents the asynchronous save operation.
        ''' The task result contains the number of state entries written to the underlying database. This can include
        ''' state entries for entities and/or relationships. Relationship state entries are created for
        ''' many-to-many relationships and relationships where there is no foreign key property
        ''' included in the entity class (often referred to as independent associations).</returns>
        ''' <remarks>Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ''' that any asynchronous operations have completed before calling another method on this context.</remarks>
        Public Overrides Function SaveChangesAsync() As Task(Of Integer)
            UpdateEntities()
            Return MyBase.SaveChangesAsync()
        End Function

        ''' <summary>
        ''' Asynchronously saves all changes made in this context to the underlying database.
        ''' </summary>
        ''' <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
        ''' <returns>A task that represents the asynchronous save operation.
        ''' The task result contains the number of state entries written to the underlying database. This can include
        ''' state entries for entities and/or relationships. Relationship state entries are created for
        ''' many-to-many relationships and relationships where there is no foreign key property
        ''' included in the entity class (often referred to as independent associations).</returns>
        ''' <remarks>Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ''' that any asynchronous operations have completed before calling another method on this context.</remarks>
        Public Overrides Function SaveChangesAsync(cancellationToken As CancellationToken) As Task(Of Integer)
            UpdateEntities()
            Return MyBase.SaveChangesAsync(cancellationToken)
        End Function
    End Class
End Namespace