' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : dotnetdave
' Created          : 04-15-2016
'
' Last Modified By : dotnetdave
' Last Modified On : 04-08-2016
' ***********************************************************************
' <copyright file="EmailAddressType.vb" company="">
'     . All rights reserved.
' </copyright>
' <summary></summary>
' ***********************************************************************
Namespace Net
    ''' <summary>
    ''' Type of the email address (where it gets used).
    ''' </summary>
    Public Enum EmailAddressType
        ''' <summary>
        ''' Used as Sent To address.
        ''' </summary>
        SendTo
        ''' <summary>
        ''' Used as Send From address.
        ''' </summary>
        SendFrom
        ''' <summary>
        ''' Used as Send Carbon Copy address.
        ''' </summary>
        SendCC
        ''' <summary>
        ''' Used as Send Bind Carbon Copy address.
        ''' </summary>
        SendBcc
    End Enum
End Namespace