'***********************************************************************
' Assembly         : dotNetTips
' Author           : David McCarter
' Created          : 10-10-2008
'
' Last Modified By : David McCarter
' Last Modified On : 10-26-2008
' Description      :
'
' Copyright        : (c) dotNetTips.com. All rights reserved.
'***********************************************************************
Namespace Web
    ''' <summary>
    ''' Determines what action to take on the web server.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum WebServiceAction
        ''' <summary>
        ''' Gets the current mode.
        ''' </summary>
        GetCurrentMode
        ''' <summary>
        ''' Pauses the service.
        ''' </summary>
        PauseService
        ''' <summary>
        ''' Resumes the service
        ''' </summary>
        ResumeService
        ''' <summary>
        ''' Starts the service.
        ''' </summary>
        StartService
        ''' <summary>
        ''' Stops the service.
        ''' </summary>
        StopService
    End Enum
End Namespace