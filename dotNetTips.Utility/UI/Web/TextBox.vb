' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 12-04-2013
'
' Last Modified By : David McCarter
' Last Modified On : 12-04-2013
' ***********************************************************************
' <copyright file="TextBox.vb" company="David McCarter Consulting">
'     David McCarter Consulting. All rights reserved.
' </copyright>
' <summary></summary>
' ***********************************************************************
Namespace UI.Web
    ''' <summary>
    ''' TextBox for ASP.NET
    ''' </summary>
    ''' <remarks>Fixes the Max length issue.</remarks>
    Public Class TextBox
        Inherits System.Web.UI.WebControls.TextBox

        ''' <summary>
        ''' Processes the post back data for the <see cref="T:System.Web.UI.WebControls.TextBox" /> control.
        ''' </summary>
        ''' <param name="postDataKey">The index within the posted collection that references the content to load.</param>
        ''' <param name="postCollection">The collection posted to the server.</param>
        ''' <returns>
        ''' true if the posted content is different from the last posting; otherwise, false.
        ''' </returns>
        Protected Overrides Function LoadPostData(ByVal postDataKey As String, ByVal postCollection As Specialized.NameValueCollection) As Boolean
            'Encapsulation.TryValidateParam(Of ArgumentNullException)(String.IsNullOrWhiteSpace(postDataKey) = False)
            'Encapsulation.TryValidateParam(Of ArgumentNullException)(postCollection IsNot Nothing)

            Page.ClientScript.ValidateEvent(Me.UniqueID, String.Empty)

            Dim oldText As String = Me.Text
            Dim newText As String = postCollection.Item(postDataKey)

            If (newText.Length > Me.MaxLength) Then
                newText = newText.Substring(0, Me.MaxLength)
            End If

            If (Not Me.ReadOnly AndAlso Not oldText.Equals(newText, StringComparison.Ordinal)) Then
                Me.Text = newText
                Return True
            End If

            Return False

        End Function

    End Class
End Namespace