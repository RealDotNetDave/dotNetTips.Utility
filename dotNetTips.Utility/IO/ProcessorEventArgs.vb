'***********************************************************************
' Assembly         : dotNetTips
' Author           : David McCarter
' Created          : 06-09-2009
'
' Last Modified By : David McCarter
' Last Modified On : 06-16-2009
' Description      :
'
' Copyright        : (c) dotNetTips.com. All rights reserved.
'***********************************************************************
Namespace IO
    ''' <summary>
    ''' Event arguments for file process.
    ''' </summary>
    Public Class ProcessorEventArgs
        Inherits System.EventArgs

        ''' <summary>
        ''' Initializes a new instance of the <see cref="ProcessorEventArgs" /> class.
        ''' </summary>
        Public Sub New()
        End Sub

        Private _totalLength As Long
        ''' <summary>
        ''' Total length of file.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property TotalLength() As Long
            Get
                Return _totalLength
            End Get
            Set(ByVal value As Long)
                _totalLength = value
            End Set
        End Property
        Private _lengthCopied As Long
        ''' <summary>
        ''' Total length copied.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property LengthCopied() As Long
            Get
                Return _lengthCopied
            End Get
            Set(ByVal value As Long)
                _lengthCopied = value
            End Set
        End Property
        Private _percent As Double
        ''' <summary>
        ''' Percent complete.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Percent() As Double
            Get
                Return _percent
            End Get
            Set(ByVal value As Double)
                _percent = value
            End Set
        End Property
        Private _cancel As Boolean

        ''' <summary>
        ''' Cancels process.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Cancel() As Boolean
            Get
                Return _cancel
            End Get
            Set(ByVal value As Boolean)
                _cancel = value
            End Set
        End Property

    End Class
End Namespace