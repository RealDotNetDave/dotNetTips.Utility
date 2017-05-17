' ***********************************************************************
' Assembly         : dotNetTips.Utility.Portable
' Author           : David McCarter
' Created          : 12-04-2013
'
' Last Modified By : David McCarter
' Last Modified On : 03-16-2017
' ***********************************************************************
' <copyright file="Countries.vb" company="David McCarter Consulting">
'     David McCarter Consulting. All rights reserved.
' </copyright>
' <summary></summary>
' *************************************************************************

Imports dotNetTips.Utility.Portable.OOP

Namespace Data
    ''' <summary>
    ''' List of countries.
    ''' </summary>
    ''' <seealso cref="System.Linq.EnumerableQuery(Of dotNetTips.Utility.Portable.Windows.Data.Country)" />
    ''' <remarks>Data bindable.</remarks>
    Public NotInheritable Class CountriesCollection
        Inherits EnumerableQuery(Of Country)

        ''' <summary>
        ''' Converts a country name to country code or country code to country name.
        ''' </summary>
        ''' <param name="nameOrCode">Value to convert.</param>
        ''' <returns><see cref="String">Country name or code</see>.</returns>
        Public Shared Function ConvertNameOrCode(ByVal nameOrCode As String) As String
            Encapsulation.TryValidateParam(Of ArgumentNullException)(String.IsNullOrEmpty(nameOrCode) = False, "Argument cannot be null.")

            nameOrCode = nameOrCode.Trim

            Dim countryResult As String = String.Empty

            countryResult = ConvertCountries(nameOrCode, countryResult)

            Return countryResult

        End Function

        ''' <summary>
        ''' Converts the countries.
        ''' </summary>
        ''' <param name="nameOrCode">The name or code.</param>
        ''' <param name="countryResult">The country result.</param>
        ''' <returns>System.String.</returns>
        Private Shared Function ConvertCountries(nameOrCode As String, countryResult As String) As String
            For Each country In DataLists.CountriesList
                If nameOrCode.Length = 2 Then
                    If (country.Code.ToUpper = nameOrCode.ToUpper) Then
                        countryResult = country.Name
                        Exit For
                    End If
                Else
                    If country.Name.ToUpper = nameOrCode.ToUpper Then
                        countryResult = country.Code
                        Exit For
                    End If
                End If
            Next

            Return countryResult
        End Function

        ''' <summary>
        ''' Constructor that auto loads countries list.
        ''' </summary>
        Public Sub New()
            MyBase.New(DataLists.CountriesList)
        End Sub

        ''' <summary>
        ''' Constructor that loads list of countries.
        ''' </summary>
        ''' <param name="list">List of <see cref="Country">Countries</see>.</param>
        Public Sub New(ByVal list As IEnumerable(Of Country))
            MyBase.New(list)
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="T:System.Linq.EnumerableQuery`1" /> class and associates the instance with an expression tree.
        ''' </summary>
        ''' <param name="expression">An expression tree to associate with the new instance.</param>
        Public Sub New(ByVal expression As System.Linq.Expressions.Expression)
            MyBase.New(expression)
        End Sub

    End Class
End Namespace