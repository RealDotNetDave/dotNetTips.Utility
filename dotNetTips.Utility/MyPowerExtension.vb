'***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : dm11086
' Created          : 05-17-2010
'
' Last Modified By : dm11086
' Last Modified On : 05-17-2010
' Description      :
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Windows.Forms

Namespace My

    ' This is the My.Computer.Shutdown extension, which contains methods that allows the end user to
    ' Shutdown, restart, and logoff a user's computer, as well as put it to sleep or in hibernation.
    ' This extension also gets power status information for the user's computer.
    ' The icon used for this item template can be grabbed here: http://www.visualpharm.com/must_have_icon_set/
    ' It is licensed under a Creative Commons Attribution-No Derivative Works 3.0
    ' Un-ported License and was created by Visual Pharm.

    ''' <summary>
    ''' Class MyPowerExtension.
    ''' </summary>
    Public Module MyPowerExtension

        ''' <summary>
        ''' Shuts the off.
        ''' </summary>
        ''' <remarks></remarks>
        Sub Shutoff()
            'This code calls the system shutdown procedure.
            Shell("shutdown -s -t 00")
        End Sub

        ''' <summary>
        ''' Logoff this instance.
        ''' </summary>
        ''' <remarks></remarks>
        Sub LogOff()
            'This code calls the LogOff procedure above.
            Shell("shutdown -l -t 00")
        End Sub

        ''' <summary>
        ''' Restarts this instance.
        ''' </summary>
        ''' <remarks></remarks>
        Sub Restart()
            'This code calls the system Restart procedure.
            Shell("shutdown -r -t 00")
        End Sub

        ''' <summary>
        ''' Hibernates this instance.
        ''' </summary>
        ''' <remarks></remarks>
        Sub Hibernate()
            'This code calls the system hibernation procedure.
            Shell("shutdown -h -t 00")
        End Sub

        ''' <summary>
        ''' Gets the battery percent.
        ''' </summary>
        ''' <returns></returns>
        ''' <value>The battery percent.</value>
        ''' <remarks></remarks>
        <CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")>
        <CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode")>
        Public ReadOnly Property BatteryPercent() As String
            ' This code will retrieve the BatteryLifePercent property and convert it to a percent.
            Get
                Select Case SystemInformation.PowerStatus.BatteryLifePercent.ToString
                    Case "1"
                        Return "100%"
                    Case "0.99"
                        Return "99%"
                    Case "0.98"
                        Return "98%"
                    Case "0.97"
                        Return "97%"
                    Case "0.96"
                        Return "96%"
                    Case "0.95"
                        Return "95%"
                    Case "0.94"
                        Return "94%"
                    Case "0.93"
                        Return "93%"
                    Case "0.92"
                        Return "92%"
                    Case "0.91"
                        Return "91%"
                    Case "0.9"
                        Return "90%"
                    Case "0.89"
                        Return "89%"
                    Case "0.88"
                        Return "88%"
                    Case "0.87"
                        Return "87%"
                    Case "0.86"
                        Return "86%"
                    Case "0.85"
                        Return "85%"
                    Case "0.84"
                        Return "84%"
                    Case "0.83"
                        Return "83%"
                    Case "0.82"
                        Return "82%"
                    Case "0.81"
                        Return "81%"
                    Case "0.8"
                        Return "80%"
                    Case "0.79"
                        Return "79%"
                    Case "0.78"
                        Return "78%"
                    Case "0.77"
                        Return "77%"
                    Case "0.76"
                        Return "76%"
                    Case "0.75"
                        Return "75%"
                    Case "0.74"
                        Return "74%"
                    Case "0.73"
                        Return "73%"
                    Case "0.72"
                        Return "72%"
                    Case "0.71"
                        Return "71%"
                    Case "0.7"
                        Return "70%"
                    Case "0.69"
                        Return "69%"
                    Case "0.68"
                        Return "68%"
                    Case "0.67"
                        Return "67%"
                    Case "0.66"
                        Return "66%"
                    Case "0.65"
                        Return "65%"
                    Case "0.64"
                        Return "64%"
                    Case "0.63"
                        Return "63%"
                    Case "0.62"
                        Return "62%"
                    Case "0.61"
                        Return "61%"
                    Case "0.6"
                        Return "60%"
                    Case "0.59"
                        Return "59%"
                    Case "0.58"
                        Return "58%"
                    Case "0.57"
                        Return "57%"
                    Case "0.56"
                        Return "56%"
                    Case "0.55"
                        Return "55%"
                    Case "0.54"
                        Return "54%"
                    Case "0.53"
                        Return "53%"
                    Case "0.52"
                        Return "52%"
                    Case "0.51"
                        Return "51%"
                    Case "0.5"
                        Return "50%"
                    Case "0.49"
                        Return "49%"
                    Case "0.48"
                        Return "48%"
                    Case "0.47"
                        Return "47%"
                    Case "0.46"
                        Return "46%"
                    Case "0.45"
                        Return "45%"
                    Case "0.44"
                        Return "44%"
                    Case "0.43"
                        Return "43%"
                    Case "0.42"
                        Return "42%"
                    Case "0.41"
                        Return "41%"
                    Case "0.4"
                        Return "40%"
                    Case "0.39"
                        Return "39%"
                    Case "0.38"
                        Return "38%"
                    Case "0.37"
                        Return "37%"
                    Case "0.36"
                        Return "36%"
                    Case "0.35"
                        Return "35%"
                    Case "0.34"
                        Return "34%"
                    Case "0.33"
                        Return "33%"
                    Case "0.32"
                        Return "32%"
                    Case "0.31"
                        Return "31%"
                    Case "0.3"
                        Return "30%"
                    Case "0.29"
                        Return "29%"
                    Case "0.28"
                        Return "28%"
                    Case "0.27"
                        Return "27%"
                    Case "0.26"
                        Return "26%"
                    Case "0.25"
                        Return "25%"
                    Case "0.24"
                        Return "24%"
                    Case "0.23"
                        Return "23%"
                    Case "0.22"
                        Return "22%"
                    Case "0.21"
                        Return "21%"
                    Case "0.2"
                        Return "20%"
                    Case "0.19"
                        Return "19%"
                    Case "0.18"
                        Return "18%"
                    Case "0.17"
                        Return "17%"
                    Case "0.16"
                        Return "16%"
                    Case "0.15"
                        Return "15%"
                    Case "0.14"
                        Return "14%"
                    Case "0.13"
                        Return "13%"
                    Case "0.12"
                        Return "12%"
                    Case "0.11"
                        Return "11%"
                    Case "0.1"
                        Return "10%"
                    Case "0.09"
                        Return "9%"
                    Case "0.08"
                        Return "8%"
                    Case "0.07"
                        Return "7%"
                    Case "0.06"
                        Return "6%"
                    Case "0.05"
                        Return "5%"
                    Case "0.04"
                        Return "4%"
                    Case "0.03"
                        Return "3%"
                    Case "0.02"
                        Return "2%"
                    Case "0.01"
                        Return "1%"
                    Case Else
                        Return "NA"
                End Select
            End Get
        End Property

        ''' <summary>
        ''' Gets the power line status.
        ''' </summary>
        ''' <returns></returns>
        ''' <value>The power line status.</value>
        ''' <remarks></remarks>
        Public ReadOnly Property PowerLineStatus() As String
            Get
                Return If(SystemInformation.PowerStatus.PowerLineStatus = 1, "Plugged In", "On Battery")
            End Get
        End Property

        ''' <summary>
        ''' Locks the system.
        ''' </summary>
        ''' <remarks></remarks>
        Sub LockSystem()
            'This code runs the shell program that acts just like the Switch User icon in Windows XP.
            Shell("rundll32 user32.dll,LockWorkStation", AppWinStyle.NormalFocus)
        End Sub

        ''' <summary>
        ''' Aborts the shutdown.
        ''' </summary>
        ''' <remarks></remarks>
        Sub AbortShutdown()
            'Name says it all.
            Shell("shutdown -a -t 00")
        End Sub
    End Module
End Namespace