' ***********************************************************************
' Assembly         : dotNetTips.Utility.Portable.Windows
' Author           : David McCarter
' Created          : 04-15-2016
'
' Last Modified By : David McCarter
' Last Modified On : 01-23-2016
' ***********************************************************************
' <copyright file="WorkItemEventArgs.vb" company="dotNetTips.com">
'     David McCarter
' </copyright>
' <summary></summary>
' ***********************************************************************
Namespace Threading
    ''' <summary>
    ''' Class WorkItemEventArgs. This class cannot be inherited.
    ''' </summary>
    ''' <seealso cref="System.EventArgs" />
    Public NotInheritable Class WorkItemEventArgs
        Inherits EventArgs
        ''' <summary>
        ''' Gets or sets the identifier.
        ''' </summary>
        ''' <value>The identifier.</value>
        Public Property Id As Guid
    End Class
End Namespace