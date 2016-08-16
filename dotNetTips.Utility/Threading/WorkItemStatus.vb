' ***********************************************************************
' Assembly         : dotNetTips.Utility.Portable.Windows
' Author           : David McCarter
' Created          : 04-15-2016
'
' Last Modified By : David McCarter
' Last Modified On : 01-23-2016
' ***********************************************************************
' <copyright file="WorkItemStatus.vb" company="dotNetTips.com">
'     David McCarter
' </copyright>
' <summary></summary>
' ***********************************************************************
Namespace Threading
    ''' <summary>
    ''' Enum WorkItemStatus
    ''' </summary>
    Public Enum WorkItemStatus
        ''' <summary>
        ''' Undetermined
        ''' </summary>
        Undetermined
        ''' <summary>
        ''' Completed
        ''' </summary>
        Completed
        ''' <summary>
        ''' Queued
        ''' </summary>
        Queued
        ''' <summary>
        ''' Executing
        ''' </summary>
        Executing
        ''' <summary>
        ''' Aborted
        ''' </summary>
        Aborted
    End Enum

End Namespace