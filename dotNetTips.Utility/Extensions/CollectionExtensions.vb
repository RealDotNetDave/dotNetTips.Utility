' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 05-14-2014
'
' Last Modified By : David McCarter
' Last Modified On : 03-10-2017
' ***********************************************************************
' <copyright file="CollectionExtensions.vb" company="David McCarter Consulting">
'     '     David McCarter Consulting. All rights reserved.
'
' </copyright>
' <summary></summary>
' *************************************************************************
Imports System.Drawing
Imports System.IO
Imports System.Runtime.CompilerServices

Namespace Extensions
    ''' <summary>
    ''' Class CollectionExtensions.
    ''' </summary>
    Public Module CollectionExtensions

        ''' <summary>
        ''' To the image.
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <returns>Image.</returns>
        <Extension>
        Public Function ToImage(value As Byte()) As Image

            Dim image As Image

            Using stream As New MemoryStream(value)
                image = Image.FromStream(stream)
            End Using

            Return image

        End Function

    End Module
End Namespace