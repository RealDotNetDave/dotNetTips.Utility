' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : david
' Created          : 03-22-2017
'
' Last Modified By : david
' Last Modified On : 05-10-2017
' ***********************************************************************
' <copyright file="Config.vb" company="McCarter Consulting - David McCarter">
'     David McCarter - dotNetTips.com © 2017
' </copyright>
' <summary></summary>
' ***********************************************************************

Imports System.Environment
Imports System.IO
Imports dotNetTips.Utility.Portable
Imports dotNetTips.Utility.Xml

''' <summary>
''' Class Config.
''' </summary>
''' <typeparam name="T"></typeparam>
Public Class Config(Of T As Class)
    Implements ISingleton(Of T)

    ''' <summary>
    ''' Initializes a new instance of the class.
    ''' </summary>
    Protected Sub New()
        Dim fileName = My.Application.Info.ProductName.Trim() + ".config.xml"
        Dim folder = Path.Combine(Environment.GetFolderPath(SpecialFolder.LocalApplicationData), My.Application.Info.CompanyName.Trim())

        Me.ConfigFileName = Path.Combine(folder, fileName)
    End Sub

    ''' <summary>
    ''' Saves this instance.
    ''' </summary>
    ''' <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    Public Overridable Function Save() As Boolean
        If File.Exists(ConfigFileName) Then
            File.Delete(ConfigFileName)
        End If

        XmlHelper.SerializeToXmlFile(_instance, ConfigFileName)

        Return True
    End Function

    ''' <summary>
    ''' Loads this instance.
    ''' </summary>
    ''' <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    Public Overridable Function Load() As Boolean
        If File.Exists(ConfigFileName) Then
            _instance = XmlHelper.DeserializeFromXmlFile(Of T)(ConfigFileName)
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
            Return m_ConfigFileName
        End Get
        Protected Set
            m_ConfigFileName = Value
        End Set
    End Property
    ''' <summary>
    ''' The m configuration file name
    ''' </summary>
    Private m_ConfigFileName As String

    ''' <summary>
    ''' The instance
    ''' </summary>
    Private _instance As T

    ''' <summary>
    ''' Instances this instance.
    ''' </summary>
    ''' <returns>T.</returns>
    Public Function Instance() As T

        If _instance Is Nothing Then
            _instance = TypeHelper.Create(Of T)()
        End If

        Return _instance

    End Function
End Class