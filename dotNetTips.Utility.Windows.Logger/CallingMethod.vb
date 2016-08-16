' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 08-10-2013
'
' Last Modified By : David McCarter
' Last Modified On : 08-24-2013
' ***********************************************************************
' <copyright file="CallingMethod.vb" company="David McCarter Consulting">
'     David McCarter Consulting. All rights reserved.
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Globalization
Imports System.IO
Imports System.Reflection

''' <summary>
''' Retrieves Method Info
''' </summary>
''' <remarks>
''' Original code: http://www.csharp411.com/c-get-calling-method/
''' </remarks>
Public Class CallingMethod

    ''' <summary>
    ''' Gets the calling method.
    ''' </summary>
    Public Sub New()
        Me.New(Nothing)
    End Sub

    ''' <summary>
    ''' Gets the calling method, ignoring calls from the specified type.
    ''' </summary>
    ''' <param name="ignoreType">All calls made from this type will be ignored.
    ''' Use this when wrapping this class in another class. OK if null.</param>
    Public Sub New(ignoreType As Type)
        Me._ignoreType = ignoreType
        Me.Initialize()
    End Sub

    Private _fileName As String
    ''' <summary>
    ''' Gets the name of the file that contained the method.
    ''' </summary>
    Public ReadOnly Property FileName() As String
        Get
            Return Me._fileName
        End Get
    End Property

    Private _filePath As String
    ''' <summary>
    ''' Gets the path of the file that contained the method.
    ''' </summary>
    Public ReadOnly Property FilePath() As String
        Get
            Return Me._filePath
        End Get
    End Property

    Private ReadOnly _ignoreType As Type
    ''' <summary>
    ''' Gets the type that will be ignored.
    ''' </summary>
    Public ReadOnly Property IgnoreType() As Type
        Get
            Return Me._ignoreType
        End Get
    End Property

    ''' <summary>
    ''' Initializes the calling method information.
    ''' </summary>
    Private Sub Initialize()
        Dim method As MethodBase = Nothing
        Dim ignoreName As String = If(Me._ignoreType Is Nothing, Nothing, Me._ignoreType.Name)

        Dim stackFrame As StackFrame = Nothing
        Dim stackTrace As New StackTrace(True)

        For i As Integer = 0 To stackTrace.FrameCount - 1
            Dim sf As StackFrame = stackTrace.GetFrame(i)
            method = sf.GetMethod()
            Dim typeName As String = method.ReflectedType.Name
            If [String].Compare(typeName, NameOf(CallingMethod), StringComparison.CurrentCulture) <> 0 AndAlso (ignoreName Is Nothing OrElse [String].Compare(typeName, ignoreName, StringComparison.CurrentCulture) <> 0) Then
                stackFrame = sf
                Exit For
            End If
        Next

        method = stackFrame.GetMethod()
        Me._method = method
        Dim methodString As String = method.ToString()

        Dim returnName As String = Nothing
        Dim methodSignature As String = methodString

        Dim splitIndex As Integer = methodString.IndexOf(" "c)
        If splitIndex > 0 Then
            returnName = methodString.Substring(0, splitIndex)
            methodSignature = methodString.Substring(splitIndex + 1, methodString.Length - splitIndex - 1)
        End If
        Me._returnName = returnName
        Me._methodSignature = methodSignature

        Me._type = method.ReflectedType
        Me._typeName = Me._type.Name
        Me._typeNameFull = Me._type.FullName

        Me._methodName = method.Name
        Me._methodNameFull = [String].Concat(Me._typeNameFull, ".", Me._methodName)

        Me._lineNumber = stackFrame.GetFileLineNumber()

        Dim fileLine As String = Nothing
        Me._filePath = stackFrame.GetFileName()
        If Not [String].IsNullOrEmpty(Me._filePath) Then
            Me._fileName = Path.GetFileName(Me._filePath)
            fileLine = [String].Format(CultureInfo.CurrentCulture, "File={0}, Line={1}", Me._fileName, Me._lineNumber)
        End If

        Me._methodSignatureFull = [String].Format(CultureInfo.CurrentCulture, "{0} {1}.{2}", returnName, Me._typeNameFull, Me._methodSignature)
        Me._text = [String].Format(CultureInfo.CurrentCulture, "{0} [{1}]", Me._methodSignatureFull, fileLine)

    End Sub

    Private _lineNumber As Integer
    ''' <summary>
    ''' Gets the line number in the file that called the method.
    ''' </summary>
    Public ReadOnly Property LineNumber() As Integer
        Get
            Return Me._lineNumber
        End Get
    End Property

    Private _methodNameFull As String
    ''' <summary>
    ''' Gets the full name of this method, with namespace.
    ''' </summary>
    Public ReadOnly Property MethodNameFull() As String
        Get
            Return Me._methodNameFull
        End Get
    End Property

    Private _method As MethodBase
    ''' <summary>
    ''' Gets the calling method.
    ''' </summary>
    Public ReadOnly Property Method() As MethodBase
        Get
            Return Me._method
        End Get
    End Property

    Private _methodName As String
    ''' <summary>
    ''' Gets the name of this method.
    ''' </summary>
    Public ReadOnly Property MethodName() As String
        Get
            Return Me._methodName
        End Get
    End Property

    Private _methodSignatureFull As String
    ''' <summary>
    ''' Gets the complete method signature
    ''' with return type, full method name, and arguments.
    ''' </summary>
    Public ReadOnly Property MethodSignatureFull() As String
        Get
            Return Me._methodSignatureFull
        End Get
    End Property

    Private _methodSignature As String
    ''' <summary>
    ''' Gets the method name and arguments.
    ''' </summary>
    Public ReadOnly Property MethodSignature() As String
        Get
            Return Me._methodSignature
        End Get
    End Property

    ''' <summary>
    ''' Gets the namespace containing the object containing this method.
    ''' </summary>
    Public ReadOnly Property [Namespace]() As String
        Get
            Dim type As Type = Me.Type
            Return If(type Is Nothing, Nothing, type.[Namespace])
        End Get
    End Property

    Private _returnName As String
    ''' <summary>
    ''' Gets the name of the return type.
    ''' </summary>
    Public ReadOnly Property ReturnName() As String
        Get
            Return Me._returnName
        End Get
    End Property

    Private _text As String
    ''' <summary>
    ''' Gets the full method signature, file and line number.
    ''' </summary>
    Public ReadOnly Property Text() As String
        Get
            Return Me._text
        End Get
    End Property

    ''' <summary>
    ''' Gets the full method signature, file and line number.
    ''' </summary>
    Public Overrides Function ToString() As String
        Return Me.Text
    End Function

    Private _typeNameFull As String
    ''' <summary>
    ''' Gets the full name of the type that contains this method,
    ''' including the namespace.
    ''' </summary>
    Public ReadOnly Property TypeNameFull() As String
        Get
            Return Me._typeNameFull
        End Get
    End Property

    Private _typeName As String
    ''' <summary>
    ''' Gets the name of the type that contains this method,
    ''' not including the namespace.
    ''' </summary>
    Public ReadOnly Property TypeName() As String
        Get
            Return Me._typeName
        End Get
    End Property

    Private _type As Type
    ''' <summary>
    ''' Gets the type that contains this method.
    ''' </summary>
    Public ReadOnly Property Type() As Type
        Get
            Return Me._type
        End Get
    End Property

End Class