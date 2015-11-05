' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 05-15-2014
'
' Last Modified By : David McCarter
' Last Modified On : 05-15-2014
' ***********************************************************************
' <copyright file="TriState.vb" company="David McCarter Consulting">
'     David McCarter Consulting. All rights reserved.
' </copyright>
' <summary></summary>
' ***********************************************************************
''' <summary>
'''
''' </summary>
Public Enum Tristate
    ''' <summary>
    ''' The false
    ''' </summary>
    ''' <remarks></remarks>
    [False] = 0
    ''' <summary>
    ''' The true
    ''' </summary>
    ''' <remarks></remarks>
    [True] = -1
    ''' <summary>
    ''' The use default
    ''' </summary>
    ''' <remarks></remarks>
    UseDefault = -2
End Enum