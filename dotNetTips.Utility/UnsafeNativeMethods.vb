'***********************************************************************
' Assembly         : dotNetTips
' Author           : David McCarter
' Created          : 12-10-2008
'
' Last Modified By : David McCarter
' Last Modified On : 03-24-2008
' Description      :
'
' Copyright        : (c) dotNetTips.com. All rights reserved.
'***********************************************************************
Imports System.Runtime.InteropServices

Friend Module UnsafeNativeMethods

    Friend Const HwndTopmost As Int32 = -1
    Friend Const SwpNoactivate As Int32 = &H10
    Friend Const SwpShowwindow As Int32 = &H40
    Friend Const SWShownoactivate As Int32 = 4

#Region "Task Bar Location Support Routines"

    <System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)> _
    Friend Structure RECT
        Public Left As Int32
        Public Top As Int32
        Public Right As Int32
        Public Bottom As Int32

        Public Overloads Overrides Function GetHashCode() As Integer
            Throw New NotImplementedException()
        End Function

        Public Overloads Overrides Function Equals(obj As [Object]) As Boolean
            Throw New NotImplementedException()
        End Function
    End Structure

    Friend Const AbmGettaskbarpos As Long = &H5
    Friend Const AbmGetstate As Long = &H4
    Friend Const AbeTop As Long = 1
    Friend Const AbeLeft As Long = 0
    Friend Const AbeRight As Long = 2
    Friend Const AbeBottom As Long = 3

#End Region

#Region "DPAPI Encryption"

    Friend Const CryptprotectUIForbidden As Integer = &H1
    Friend Const CryptprotectLocalMachine As Integer = &H4

    <StructLayout(LayoutKind.Sequential)> _
    Friend Structure DataBlob
        Public CbData As Integer
        Public PBData As IntPtr

        Public Overloads Overrides Function GetHashCode() As Integer
            '  dotNetTips.Utility.Portable.Windows.General.GetInstanceHashCode(Me)
        End Function

        Public Overloads Overrides Function Equals(obj As [Object]) As Boolean
            '  dotNetTips.Utility.Portable.Windows.General.DoesObjectEqualInstance(obj, Me)
        End Function
    End Structure

    Friend Const SwpNosize As Integer = 1
    Friend Const WMCtlcolorlistbox As UInt32 = 308

#End Region

    <DllImport("kernel32.dll")> _
    Friend Function GetCurrentThreadId() As Integer
    End Function

    <DllImport("kernel32.dll", CharSet:=CharSet.Unicode)> _
    Friend Function GetModuleHandle(ByVal moduleName As String) As IntPtr
    End Function

    <DllImport("kernel32.dll", CharSet:=CharSet.Unicode, SetLastError:=True)>
    Friend Function GetModuleFileName(<[In]()> ByVal hModule As IntPtr, <Out()> ByVal lpFilename As System.Text.StringBuilder, <[In](), MarshalAs(UnmanagedType.U4)> ByVal nSize As Integer) As Integer
    End Function

    <DllImport("kernel32.dll", BestFitMapping:=False, ThrowOnUnmappableChar:=True)>
    Friend Function GetShortPathName(ByVal lpszLongPath As String, ByVal lpszShortPath As System.Text.StringBuilder, ByVal cchBuffer As Integer) As Integer
    End Function

End Module