'***********************************************************************
' Assembly         : dotNetTips
' Author           : David McCarter
' Created          : 12-10-2008
'
' Last Modified By : David McCarter
' Last Modified On : 02-26-2008
' Description      :
'
' Copyright        : (c) dotNetTips.com. All rights reserved.
'***********************************************************************
Imports System.Windows.Forms
Imports dotNetTips.Utility.Portable.OOP

Namespace UI.Windows

    ''' <summary>
    ''' Form helper class.
    ''' </summary>
    Public NotInheritable Class FormHelper

        ''' <summary>
        ''' Clears the text boxes.
        ''' </summary>
        ''' <param name="parent">The parent Control.</param>
        Public Shared Sub ClearTextBoxes(ByVal parent As Control)
            Encapsulation.TryValidateParam(Of ArgumentNullException)(parent IsNot Nothing)

            For Each tempControl As Control In parent.Controls
                If tempControl.Controls.Count > 0 Then
                    ClearTextBoxes(tempControl)
                ElseIf tempControl.GetType Is GetType(TextBox) Then
                    DirectCast(tempControl, TextBox).Text = String.Empty
                End If
            Next
        End Sub

        ''' <summary>
        ''' Selects the text of a control.
        ''' </summary>
        ''' <param name="control">The control to select.</param>
        Public Shared Sub SelectControlText(ByVal control As System.Windows.Forms.TextBox)
            Encapsulation.TryValidateParam(Of ArgumentNullException)(control IsNot Nothing)

            If control.Text.Length > 0 Then
                control.Select(0, control.Text.Length)
            End If

        End Sub

        Private Sub New()
        End Sub

    End Class

End Namespace