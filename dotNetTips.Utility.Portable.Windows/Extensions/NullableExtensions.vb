' ***********************************************************************
' Assembly         : dotNetTips.Utility.Portable
' Author           : David McCarter
' Created          : 12-04-2013
'
' Last Modified By : David McCarter
' Last Modified On : 05-12-2014
' ***********************************************************************
' <copyright file="NullableExtensions.vb" company="David McCarter Consulting">
'     David McCarter Consulting. All rights reserved.
' </copyright>
' <summary></summary>
' ***********************************************************************

Namespace Extensions
    ''' <summary>
    '''
    ''' </summary>
    Public Module NullableExtensions
        ''' <summary>
        ''' Determines whether [has value and equals] [the specified source].
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="source">The source.</param>
        ''' <param name="target">The target.</param>
        ''' <returns>
        ''' <c>true</c> if [has value and equals] [the specified source]; otherwise, <c>false</c>.
        ''' </returns>
        ''' <remarks>Code By: MatteoSp </remarks>
        <System.Runtime.CompilerServices.Extension()> _
        Public Function HasValueAndEquals(Of T As Structure)(ByVal source As Nullable(Of T), ByVal target As T) As Boolean
            Return source.HasValue AndAlso source.Value.Equals(target)
        End Function

        ''' <summary>
        ''' Determines whether [has value and equals] [the specified source].
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="source">The source.</param>
        ''' <param name="target">The target.</param>
        ''' <returns>
        ''' <c>true</c> if [has value and equals] [the specified source]; otherwise, <c>false</c>.
        ''' </returns>
        ''' <remarks>Code By: MatteoSp </remarks>
        <System.Runtime.CompilerServices.Extension()> _
        Public Function HasValueAndEquals(Of T As Structure)(ByVal source As Nullable(Of T), ByVal target As Nullable(Of T)) As Boolean
            Return source.HasValue AndAlso source.Value.Equals(target)
        End Function

        ''' <summary>
        ''' Returns the nullable string.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="param">The param.</param>
        ''' <returns></returns>
        ''' <remarks>Code By: Chris Rock</remarks>
        <System.Runtime.CompilerServices.Extension()> _
        Public Function ToNullableString(Of T As Structure)(ByVal param As System.Nullable(Of T)) As String
            If param.HasValue Then
                Return param.Value.ToString()
            Else
                Return String.Empty
            End If
        End Function

    End Module
End Namespace