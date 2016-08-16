' ***********************************************************************
' Assembly         : dotNetTips.Utility.Portable.Windows
' Author           : David McCarter
' Created          : 04-15-2016
'
' Last Modified By : David McCarter
' Last Modified On : 07-30-2016
' ***********************************************************************
' <copyright file="Counter.vb" company="dotNetTips.com">
'     David McCarter
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Globalization
''' <summary>
''' Counter class.
''' </summary>
''' <remarks></remarks>
Public Class Counter

    ''' <summary>
    ''' Value
    ''' </summary>
    Private _value As Int32
    ''' <summary>
    ''' Seed
    ''' </summary>
    Private _seed As Int32
    ''' <summary>
    ''' Minimum
    ''' </summary>
    Private _minimum As Int32 = Int32.MinValue
    ''' <summary>
    ''' Maximum
    ''' </summary>
    Private _maximum As Int32 = Int32.MaxValue

    ''' <summary>
    ''' Event that is fired when ever the Value of the counter is changed.
    ''' </summary>
    Public Shared Event ValueChanged As EventHandler(Of CounterChangedEventArgs)

    ''' <summary>
    ''' Counter value.
    ''' </summary>
    ''' <value>The value.</value>
    Public Property [Value]() As Int32
        Get
            Return _value
        End Get

        Private Set(ByVal value As Int32)

            If Me.Value <> value Then

                If value <= Me.Maximum And value >= Me.Minimum Then

                    Me._value = value

                    Me.OnValueChanged()

                End If

            End If

        End Set
    End Property

    ''' <summary>
    ''' Fires <see cref="Valuechanged">event</see>.
    ''' </summary>
    Protected Overridable Sub OnValueChanged()
        RaiseEvent ValueChanged(Me, New CounterChangedEventArgs(Me.Value))

    End Sub

    ''' <summary>
    ''' Minimum value of the counter.
    ''' </summary>
    ''' <value>The minimum.</value>
    Public Property Minimum() As Int32

        Get
            Return Me._minimum
        End Get

        Set(ByVal value As Int32)

            If value = Me._minimum Then
                Return
            End If

            Me._minimum = value

            If Me.Minimum > Me.Value Then
                Me.Value = Me.Minimum
            End If

            If Me.Minimum <> Me.Seed Then
                Me.Seed = Me.Minimum
            End If

        End Set
    End Property

    ''' <summary>
    ''' Maximum value of the counter.
    ''' </summary>
    ''' <value>The maximum.</value>
    Public Property Maximum() As Int32

        Get
            Return Me._maximum
        End Get

        Set(ByVal value As Int32)

            If value = Me._maximum Then
                Return
            End If

            Me._maximum = value

            If Me.Maximum < Me.Value Then
                Me.Value = Me.Maximum
            End If

        End Set
    End Property

    ''' <summary>
    ''' Counter seed (default) value
    ''' </summary>
    ''' <value>The seed.</value>
    Public Property Seed() As Int32

        Get
            Return Me._seed
        End Get

        Private Set(ByVal value As Int32)

            If value = Me._seed Then Return

            If Me.Seed <> Me.Minimum Then Me.Minimum = Me.Seed

        End Set

    End Property

    ''' <summary>
    ''' Gets or sets the amount by which to increase the current value of the counter.
    ''' </summary>
    ''' <value><see cref="int32" /></value>
    Public Property [Step]() As Int32 = 1

    ''' <summary>
    ''' Increments the counter by the <see cref="[Step]">Step</see> value.
    ''' </summary>
    Public Sub Increment()
        Me.Value += Me.Step
    End Sub

    ''' <summary>
    ''' Increments the counter by the given value.
    ''' </summary>
    ''' <param name="value"><see cref="int32">Number</see> to increment the counter.</param>
    Public Sub Increment(ByVal value As Int32)
        Me.Value += value
    End Sub

    ''' <summary>
    ''' Decrements the count.
    ''' </summary>
    Public Sub Decrement()
        Me.Value -= Me.Step
    End Sub

    ''' <summary>
    ''' Decrements the counter by the given value.
    ''' </summary>
    ''' <param name="value"><see cref="int32">Number </see> to decrement the counter.</param>
    Public Sub Decrement(ByVal value As Int32)
        Me.Value -= value
    End Sub

    ''' <summary>
    ''' Resets the count to the <see cref="Seed">Seed</see> value.
    ''' </summary>
    Public Sub Reset()
        Me.Value = Me.Seed
    End Sub

    ''' <summary>
    ''' Returns the value of the counter.
    ''' </summary>
    ''' <returns>Value of the counter as <see cref="String" /></returns>
    Public Overrides Function ToString() As String
        Return Me.Value.ToString(CultureInfo.CurrentCulture)
    End Function

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="step">Sets the <see cref="[Step]">Step</see> value.</param>
    Public Sub New(ByVal [step] As Int32)
        Me.Step = [step]
    End Sub

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="step">Sets the <see cref="[Step]">Step</see> value.</param>
    ''' <param name="seed">Sets the <see cref="Seed">Seed</see> value.</param>
    Public Sub New(ByVal [step] As Int32, ByVal seed As Int32)
        Me.Step = [step]
        Me._seed = seed
        Me._value = Me._seed
    End Sub

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="step">Sets the <see cref="[Step]">Step</see> value.</param>
    ''' <param name="seed">Sets the <see cref="Seed">Seed</see> value.</param>
    ''' <param name="minimum">Sets the <see cref="Minimum">Minimum</see> value.</param>
    ''' <param name="maximum">Sets the <see cref="Maximum">Maximum</see> value.</param>
    Public Sub New(ByVal [step] As Int32, ByVal seed As Int32, ByVal minimum As Int32, ByVal maximum As Int32)
        Me.Step = [step]
        Me._seed = seed
        Me._minimum = minimum
        Me._maximum = maximum
        Me._value = Me._seed
    End Sub

    ''' <summary>
    ''' Constructor.
    ''' </summary>
    Public Sub New()
    End Sub

End Class