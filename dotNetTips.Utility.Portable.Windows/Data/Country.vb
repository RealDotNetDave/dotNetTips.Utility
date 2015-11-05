' ***********************************************************************
' Assembly         : dotNetTips.Utility.Portable
' Author           : David McCarter
' Created          : 12-04-2013
'
' Last Modified By : David McCarter
' Last Modified On : 02-01-2014
' ***********************************************************************
' <copyright file="Country.vb" company="David McCarter Consulting">
'     David McCarter Consulting. All rights reserved.
' </copyright>
' <summary></summary>
' ***********************************************************************
Namespace Data

    ''' <summary>
    ''' Country class.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Country

        ''' <summary>
        ''' Name of country.
        ''' </summary>
        ''' <value>Country <see cref="String">name</see>.</value>
        ''' <returns>Country <see cref="String">name</see>.</returns>
        ''' <remarks></remarks>
        Public Property Name() As String

        ''' <summary>
        ''' Country code.
        ''' </summary>
        ''' <value>Country <see cref="String">code</see>.</value>
        ''' <returns>Country <see cref="String">code</see>.</returns>
        ''' <remarks></remarks>
        Public Property Code() As String

    End Class
End Namespace