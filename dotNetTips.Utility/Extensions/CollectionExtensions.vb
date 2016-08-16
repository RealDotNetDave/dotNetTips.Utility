' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 05-14-2014
'
' Last Modified By : David McCarter
' Last Modified On : 05-14-2014
' ***********************************************************************
' <copyright file="CollectionExtensions.vb" company="David McCarter Consulting">
'     David McCarter Consulting. All rights reserved.
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Collections.Generic
Imports System.Diagnostics.Contracts
Imports System.Drawing
Imports System.IO
Imports System.Reflection
Imports System.Runtime.CompilerServices
Namespace Extensions
    ''' <summary>
    '''
    ''' </summary>
    Public Module CollectionExtensions

        ''' <summary>
        ''' To the image.
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension>
        Public Function ToImage(value As Byte()) As Image
            Contracts.Contract.Requires(Of ArgumentNullException)(value.Length > 0)

            Dim image As Image

            Using stream As New MemoryStream(value)
                image = Image.FromStream(stream)
            End Using

            Return image

        End Function

    End Module
End Namespace