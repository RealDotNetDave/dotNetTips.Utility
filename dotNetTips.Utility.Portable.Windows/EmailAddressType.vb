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