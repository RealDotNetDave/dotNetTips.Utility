' ***********************************************************************
' Assembly         : dotNetTips.Utility.Portable
' Author           : David McCarter
' Created          : 12-04-2013
'
' Last Modified By : David McCarter
' Last Modified On : 02-01-2014
' ***********************************************************************
' <copyright file="DataLists.vb" company="David McCarter Consulting">
'     David McCarter Consulting. All rights reserved.
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Xml.Linq

Namespace Data

    ''' <summary>
    ''' Various data lists.
    ''' </summary>
    ''' <remarks></remarks>
    Public Module DataLists

        ''' <summary>
        ''' List of states.
        ''' </summary>
        ''' <returns>UsStates.</returns>
        Public Function StatesList() As UsStatesCollection

            Dim reader = XElement.Parse(My.Resources.States)

            Static list As New System.Collections.Generic.List(Of State)

            Dim data = From s In reader.Elements(XName.Get("state"))
                       Order By s.Attribute(XName.Get("name")).Value
                       Select New State With {.Name = s.Attribute(XName.Get("name")).Value.ToUpper,
                      .Code = s.Attribute(XName.Get("abbreviation")).Value.ToUpper}

            list = data.ToList

            Return New UsStatesCollection(list.AsEnumerable)

        End Function

        ''' <summary>
        ''' Loads countries.
        ''' </summary>
        ''' <returns>Countries.</returns>
        Public Function CountriesList() As CountriesCollection

            Dim reader = XElement.Parse(My.Resources.Countries)

            Static list As New System.Collections.Generic.List(Of Country)

            Dim data = From c In reader.Elements(XName.Get("ISO_3166-1_Entry"))
                       Order By c.Element(XName.Get("ISO_3166-1_Country_name")).Value
                       Select New Country With {.Name = c.Element(XName.Get("ISO_3166-1_Country_name")).Value.ToUpper,
                                        .Code = c.Element(XName.Get("ISO_3166-1_Alpha-2_Code_element")).Value.ToUpper}

            list = data.ToList

            Return New CountriesCollection(list.AsEnumerable)

        End Function

    End Module

End Namespace