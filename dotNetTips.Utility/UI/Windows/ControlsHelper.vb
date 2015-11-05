'***********************************************************************
' Assembly         : dotNetTips
' Author           : David McCarter
' Created          : 12-10-2008
'
' Last Modified By : David McCarter
' Last Modified On : 05-20-2009
' Description      :
'
' Copyright        : (c) dotNetTips.com. All rights reserved.
'***********************************************************************
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Reflection
Imports System.Collections.ObjectModel

Namespace UI.Windows
    ''' <summary>
    ''' Control helper class.
    ''' </summary>
    Public Module ControlsHelper

        ''' <summary>
        ''' Clears the controls.
        ''' </summary>
        ''' <param name="parent">The parent Control.</param>
        Public Sub ClearControls(ByVal parent As Control)

            ClearControls(parent, String.Empty, False)

        End Sub

        ''' <summary>
        ''' Clears the controls.
        ''' </summary>
        ''' <param name="parent">The parent Control.</param>
        ''' <param name="tag">The tag name to use when searching for controls to clear.</param>
        Public Sub ClearControls(ByVal parent As Control, ByVal tag As String)

            ClearControls(parent, tag, False)

        End Sub

        ''' <summary>
        ''' Clears controls.
        ''' </summary>
        ''' <param name="parent">The parent Control.</param>
        ''' <param name="clearChildren">if set to <c>true</c> [clear children].</param>
        Public Sub ClearControls(ByVal parent As Control, ByVal clearChildren As Boolean)

            ClearControls(parent, String.Empty, clearChildren)

        End Sub

        ''' <summary>
        ''' Clears the controls.
        ''' </summary>
        ''' <param name="parent">The parent Control.</param>
        ''' <param name="tag">The tag name to use when searching for controls to clear.</param>
        ''' <param name="clearChildren">if set to <c>true</c> [clear children].</param>
        Public Sub ClearControls(ByVal parent As Control, ByVal tag As String, ByVal clearChildren As Boolean)

            If parent Is Nothing Then
                Throw New ArgumentNullException("parent")
            End If

            parent.SuspendLayout()

            Try
                ClearKnownControls(parent, tag, clearChildren)
            Finally
                parent.ResumeLayout()
            End Try

        End Sub

        ''' <summary>
        ''' Selects the first empty control.
        ''' </summary>
        ''' <param name="parent">The parent Control.</param>
        ''' <param name="tag">The tag name to use when searching for controls.</param>
        ''' <param name="includeChildren">if set to <c>true</c> [include children].</param>
        ''' <remarks>Searches by tab order.</remarks>
        Public Sub SelectFirstEmptyControl(ByVal parent As Control, ByVal tag As String, ByVal includeChildren As Boolean)

            If parent Is Nothing Then
                Throw New ArgumentNullException("parent")
            End If

            parent.SuspendLayout()

            FindEditableControls(parent, tag, includeChildren).Where(Function(ctrl) ctrl.Text.Trim.Length = 0).First.Select()

            parent.ResumeLayout()

        End Sub

        ''' <summary>
        ''' Finds all controls with tab stop within a parent control.
        ''' </summary>
        ''' <param name="parent">Parent <see cref="Control">Control</see>.</param>
        ''' <param name="tag">Narrow search by tag <see cref="String">name</see>.</param>
        ''' <param name="includeChildren">Include child <see cref="Control">Controls</see>.</param>
        ''' <returns><see cref="System.Collections.Generic.SortedDictionary(Of String, Control)">List</see> of controls.</returns>
        ''' <remarks></remarks>
        Public Function FindEditableControls(ByVal parent As Control, ByVal tag As String, ByVal includeChildren As Boolean) As IReadOnlyCollection(Of Control)

            If parent Is Nothing Then
                Throw New ArgumentNullException("parent")
            End If

            Dim controls As New List(Of Control)(From c In parent.Controls.Cast(Of Control)() Where c.TabStop = True And (String.IsNullOrEmpty(tag) OrElse (CStr(c.Tag) = tag)))

            For Each ctrl In From c In parent.Controls.Cast(Of Control)() Where c.HasChildren And includeChildren
                controls.AddRange(FindEditableControls(ctrl, tag, includeChildren))
            Next

            Return New ObjectModel.ReadOnlyCollection(Of Control)((From c In controls Order By GetTabIndex(c)).ToList())

        End Function

        ''' <summary>
        ''' Finds controls.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="parent">The parent.</param>
        ''' <param name="includeChildren">if set to <c>true</c> [include children].</param>
        ''' <returns></returns>
        Public Function FindControls(Of T As Control)(ByVal parent As Control, ByVal includeChildren As Boolean) As IReadOnlyCollection(Of T)

            If parent Is Nothing Then
                Throw New ArgumentNullException("parent")
            End If

            Dim controls As New List(Of T)(From c In parent.Controls.OfType(Of T)())

            For Each ctrl In From c In parent.Controls.Cast(Of Control)() Where c.HasChildren And includeChildren
                controls.AddRange(FindControls(Of T)(ctrl, includeChildren))
            Next

            Return New ReadOnlyCollection(Of T)(controls)

        End Function

        ''' <summary>
        ''' Clears the known controls.
        ''' </summary>
        ''' <param name="parent">The parent.</param>
        ''' <param name="tag">The tag.</param>
        ''' <param name="clearChildren">The clear children.</param>
        ''' <remarks></remarks>
        Public Sub ClearKnownControls(ByVal parent As Control, ByVal tag As String, ByVal clearChildren As Boolean)

            For Each tempControl As Control In FindEditableControls(parent, tag, clearChildren)
                ClearControl(tempControl)
            Next

        End Sub

        Private Sub CheckControl(ByVal controlToClear As Control, ByVal controlType As Type, ByRef shouldReturn As Boolean)
            shouldReturn = False
            'Check to see if we need to ignore the control
            Static ignore As String() = My.Resources.ClearControlsToIgnore.ToUpper(CultureInfo.CurrentCulture).Split(New Char() {Char.Parse(";")})
            If (ignore IsNot Nothing AndAlso ignore.Length > 0) Then
                If (ignore.Contains(controlType.Name.ToUpper(CultureInfo.CurrentCulture))) Then
                    shouldReturn = True
                    Exit Sub
                End If
            End If

            'Check for Read-Only control
            Dim readOnlyProp As PropertyInfo = controlType.GetProperty("ReadOnly")

            If readOnlyProp IsNot Nothing AndAlso CBool(readOnlyProp.GetValue(controlToClear, Nothing)) Then
                shouldReturn = True
                Exit Sub
            End If
        End Sub

        Private Sub ClearCheckBox(ByVal controlToClear As Control)
            Dim tempCheckedListBox As CheckedListBox = DirectCast(controlToClear, CheckedListBox)

            tempCheckedListBox.ClearSelected()

            For Each checkedItem As Integer In tempCheckedListBox.CheckedIndices
                tempCheckedListBox.SetItemCheckState(checkedItem, CheckState.Unchecked)
            Next
        End Sub
        Private Sub ClearDateTimeControl(ByVal controlToClear As Control)
            Dim picker As DateTimePicker = DirectCast(controlToClear, System.Windows.Forms.DateTimePicker)
            picker.Value = Now
            If picker.ShowCheckBox Then
                picker.Checked = False
            End If
        End Sub
        Private Sub ClearListControl(ByVal controlToClear As Control)
            Dim tempListView As ListView = DirectCast(controlToClear, ListView)

            For Each checkedItem As ListViewItem In tempListView.CheckedItems
                checkedItem.Checked = False
            Next

            For Each selectedItem As ListViewItem In tempListView.SelectedItems
                selectedItem.Selected = False
            Next
        End Sub
        Private Sub ClearControl(ByVal controlToClear As Control)

            Dim controlType As Type = controlToClear.[GetType]()

            Dim shouldReturn As Boolean

            CheckControl(controlToClear, controlType, shouldReturn)

            If shouldReturn Then
                Return
            End If

            Select Case controlType.Name
                Case Is = "CheckedListBox"
                    ClearCheckBox(controlToClear)

                Case Is = "ComboBox"
                    DirectCast(controlToClear, System.Windows.Forms.ComboBox).SelectedIndex = -1

                Case Is = "DateTimePicker"
                    ClearDateTimeControl(controlToClear)

                Case Is = "CheckBox"
                    DirectCast(controlToClear, System.Windows.Forms.CheckBox).Checked = False

                Case Is = "RadioButton"
                    DirectCast(controlToClear, System.Windows.Forms.RadioButton).Checked = False

                Case Is = "ListBox"
                    DirectCast(controlToClear, System.Windows.Forms.ListBox).SelectedIndex = -1

                Case Is = "ListView"
                    ClearListControl(controlToClear)

                Case Is = "MonthCalendar"
                    DirectCast(controlToClear, MonthCalendar).SetDate(Now)

                Case Is = "NumericUpDown"
                    Dim tempNumericUpDown As NumericUpDown = DirectCast(controlToClear, NumericUpDown)
                    tempNumericUpDown.Value = tempNumericUpDown.Minimum

                Case Is = "ProgressBar"
                    Dim tempProgress As ProgressBar = DirectCast(controlToClear, ProgressBar)
                    tempProgress.Value = tempProgress.Minimum

                Case Else
                    controlToClear.Text = String.Empty

            End Select

        End Sub

        Private Function GetTabIndex(ByVal inputControl As Control) As String
            Dim tabIndex As String = inputControl.TabIndex.ToString(My.Resources.TabIndexFormat, CultureInfo.InvariantCulture)

            If Not IsNothing(inputControl.Parent) Then
                tabIndex = String.Format(CultureInfo.InvariantCulture, My.Resources.TabIndexFormatValue, GetTabIndex(inputControl.Parent), tabIndex)
            End If

            Return tabIndex

        End Function

    End Module
End Namespace