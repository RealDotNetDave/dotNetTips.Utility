' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 03-22-2017
'
' Last Modified By : David McCarter
' Last Modified On : 06-24-2017
' ***********************************************************************
' <copyright file="Config.vb" company="NicheWare - David McCarter">
'     NicheWare - David McCarter
' </copyright>
' <summary></summary>
' *************************************************************************
Imports dotNetTips.Utility.IO
Imports dotNetTips.Utility.Portable
Imports dotNetTips.Utility.Xml
Imports System.IO
''' <summary>
''' Class Config.
''' </summary>
''' <typeparam name="T"></typeparam>
''' <seealso cref="dotNetTips.Utility.Portable.ISingleton(Of T)" />
Public Class Config(Of T As Class)
    Implements ISingleton(Of T)
    ''' <summary>
    ''' Initializes a new instance of the class.
    ''' </summary>
    Protected Sub New()
        Me.ConfigFileName = Path.Combine(DirectoryHelper.AppApplicationDataFolder, "dotNetTips.Dev.Cleaner.config")
    End Sub

    ''' <summary>
    ''' Saves this instance.
    ''' </summary>
    ''' <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    Public Overridable Function Save() As Boolean
        If File.Exists(Me.ConfigFileName) Then
            File.Delete(Me.ConfigFileName)
        End If

        XmlHelper.SerializeToXmlFile(_instance, Me.ConfigFileName)

        Return True
    End Function

    ''' <summary>
    ''' Loads this instance.
    ''' </summary>
    ''' <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    Public Overridable Function Load() As Boolean
        If File.Exists(Me.ConfigFileName) Then
            _instance = dotNetTips.Utility.Xml.DeserializeFromXmlFile(Of T)(Me.ConfigFileName)
            Return True
        End If

        Return False
    End Function

    ''' <summary>
    ''' Gets or sets the name of the configuration file.
    ''' </summary>
    ''' <value>The name of the configuration file.</value>
    Public Property ConfigFileName() As String
        Get
            Return _configFileName
        End Get
        Protected Set
            If _configFileName Is Value Then
                Return
            End If
            _configFileName = Value
        End Set
    End Property


    ''' <summary>
    ''' Gets the instance.
    ''' </summary>
    ''' <returns></returns>
    Public Function Instance() As T Implements ISingleton(Of T).Instance
        If _instance Is Nothing Then
            _instance = TypeHelper.Create(Of T)()
        End If

        Return _instance
    End Function

    ''' <summary>
    ''' The instance
    ''' </summary>
    Private _instance As T

    ''' <summary>
    ''' The configuration file name
    ''' </summary>
    Private _configFileName As String


End Class