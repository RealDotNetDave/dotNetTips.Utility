' ***********************************************************************
' Assembly         : dotNetTips.Utility.Windows.Logger
' Author           : David McCarter
' Created          : 04-15-2016
'
' Last Modified By : David McCarter
' Last Modified On : 04-08-2016
' ***********************************************************************
' <copyright file="LoggingTraceListener.vb" company="NicheWare - dotNetTips.com">
'     NicheWare - dotNetTips.com. All rights reserved.
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Globalization
''' <summary>
''' Base class for trace listeners.
''' </summary>
''' <seealso cref="System.Diagnostics.TraceListener" />
Public MustInherit Class LoggingTraceListener
    Inherits TraceListener

    ''' <summary>
    ''' Initializes a new instance of the <see cref="LoggingTraceListener" /> class.
    ''' </summary>
    Protected Sub New()
        MyBase.New()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="LoggingTraceListener" /> class.
    ''' </summary>
    ''' <param name="name">The name.</param>
    Protected Sub New(ByVal name As String)
        MyBase.New(name)
    End Sub

    ''' <summary>
    ''' Determines whether the specified opts is enabled.
    ''' </summary>
    ''' <param name="opts">The opts.</param>
    ''' <returns><c>true</c> if the specified opts is enabled; otherwise, <c>false</c>.</returns>
    Protected Function IsEnabled(ByVal opts As TraceOptions) As Boolean
        Return (opts And MyBase.TraceOutputOptions) <> TraceOptions.None
    End Function

    ''' <summary>
    ''' Retrieves the property values.
    ''' </summary>
    Protected Sub RetrievePropertyValues()
        Dim listenerAttributes = Me.GetSupportedAttributes()

        If listenerAttributes Is Nothing Then
            Exit Sub
        End If

        For Each attribute In listenerAttributes.AsParallel
            Dim prop = Me.[GetType]().GetProperty(attribute)

            If prop IsNot Nothing AndAlso Me.Attributes.ContainsKey(attribute) Then
                Dim ic As IConvertible = Me.Attributes(attribute)
                Dim targetType As Type = prop.PropertyType
                prop.SetValue(Me, (ic.ToType(targetType, CultureInfo.CurrentCulture)), Nothing)
            End If
        Next
    End Sub

End Class