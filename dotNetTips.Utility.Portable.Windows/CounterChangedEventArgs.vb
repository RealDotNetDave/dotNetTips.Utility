'***********************************************************************
' Assembly         : dotNetTips
' Author           : David McCarter
' Created          : 06-09-2009
'
' Last Modified By : David McCarter
' Last Modified On : 06-09-2009
' Description      :
'
' Copyright        : (c) dotNetTips.com. All rights reserved.
'***********************************************************************

''' <summary>
''' Arguments for when the counter value is changed.
''' </summary>
Public Class CounterChangedEventArgs
    Inherits System.EventArgs

    Private _newValue As Int32

    ''' <summary>
    ''' Initializes a new instance of the <see cref="CounterChangedEventArgs" /> class.
    ''' </summary>
    Public Sub New()
    End Sub

    ''' <summary>
    ''' New Value of the Counter.
    ''' </summary>
    ''' <value><see cref="int32"/></value>
    ''' <returns><see cref="int32"/></returns>
    ''' <remarks></remarks>
    Public Property NewValue() As Int32

        Get
            Return Me._newValue
        End Get

        Friend Set(ByVal value As Int32)
            Me._newValue = value
        End Set

    End Property

    Friend Sub New(ByVal value As Int32)
        Me.NewValue = value
    End Sub

End Class