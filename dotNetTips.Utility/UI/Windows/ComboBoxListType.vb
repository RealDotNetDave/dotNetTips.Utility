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
Imports System.Drawing
Imports System.Windows.Forms

Namespace UI.Windows
    ''' <summary>
    ''' Types for ComboBox list.
    ''' </summary>
    Public Enum ComboBoxListType
        ''' <summary>
        ''' Do not auto load date.
        ''' </summary>
        ''' <remarks></remarks>
        None
        ''' <summary>
        ''' Auto load US States.
        ''' </summary>
        ''' <remarks></remarks>
        UsStates
        ''' <summary>
        ''' Auto load Countries.
        ''' </summary>
        ''' <remarks></remarks>
        Countries
    End Enum
End Namespace