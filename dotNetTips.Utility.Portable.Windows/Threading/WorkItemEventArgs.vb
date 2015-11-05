Namespace Collections
    Public NotInheritable Class WorkItemEventArgs
        Inherits EventArgs
        Public Property Id() As Guid
            Get
                Return m_Id
            End Get
            Set(value As Guid)
                m_Id = value
            End Set
        End Property
        Private m_Id As Guid
    End Class
End Namespace