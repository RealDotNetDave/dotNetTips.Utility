'***********************************************************************
' Assembly         : dotNetTips
' Author           : David McCarter
' Created          : 12-10-2008
'
' Last Modified By : David McCarter
' Last Modified On : 06-16-2009
' Description      :
'
' Copyright        : (c) dotNetTips.com. All rights reserved.
'***********************************************************************
Namespace Encryption
    ''' <summary>
    ''' Use local machine or user to encrypt and decrypt the data
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EncryptionStorage
        ''' <summary>
        ''' Uses machine key to encrypt and decrypt data.
        ''' </summary>
        ''' <remarks></remarks>
        Machine
        ''' <summary>
        ''' Uses user key to encrypt and decrypt data.
        ''' </summary>
        ''' <remarks></remarks>
        User
    End Enum
End Namespace