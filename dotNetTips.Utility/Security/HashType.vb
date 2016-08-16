' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : dotnetdave
' Created          : 05-16-2016
'
' Last Modified By : dotnetdave
' Last Modified On : 05-16-2016
' ***********************************************************************
' <copyright file="HashType.vb" company="">
'     . All rights reserved.
' </copyright>
' <summary></summary>
' ***********************************************************************
Namespace Security
    ''' <summary>
    ''' Supported hash algorithms
    ''' </summary>
    Public Enum HashType
        ''' <summary>
        ''' The hmac
        ''' </summary>
        HMAC
        ''' <summary>
        ''' The hmacm d5
        ''' </summary>
        HMACMD5
        ''' <summary>
        ''' The hmacsh a1
        ''' </summary>
        HMACSHA1
        ''' <summary>
        ''' The hmacsh a256
        ''' </summary>
        HMACSHA256
        ''' <summary>
        ''' The hmacsh a384
        ''' </summary>
        HMACSHA384
        ''' <summary>
        ''' The hmacsh a512
        ''' </summary>
        HMACSHA512
        ''' <summary>
        ''' The mac triple DES
        ''' </summary>
        MACTripleDES
        ''' <summary>
        ''' The m d5
        ''' </summary>
        MD5
        ''' <summary>
        ''' The ripem D160
        ''' </summary>
        RIPEMD160
        ''' <summary>
        ''' The sh a1
        ''' </summary>
        SHA1
        ''' <summary>
        ''' The sh a256
        ''' </summary>
        SHA256
        ''' <summary>
        ''' The sh a384
        ''' </summary>
        SHA384
        ''' <summary>
        ''' The sh a512
        ''' </summary>
        SHA512
    End Enum
End Namespace