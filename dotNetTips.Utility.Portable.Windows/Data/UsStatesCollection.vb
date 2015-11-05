' ***********************************************************************
' Assembly         : dotNetTips.Utility.Portable
' Author           : David McCarter
' Created          : 12-04-2013
'
' Last Modified By : David McCarter
' Last Modified On : 02-01-2014
' ***********************************************************************
' <copyright file="UsStates.vb" company="David McCarter Consulting">
'     David McCarter Consulting. All rights reserved.
' </copyright>
' <summary></summary>
' ***********************************************************************
Namespace Data

    ''' <summary>
    ''' List of US States
    ''' </summary>
    ''' <remarks></remarks>
    Public NotInheritable Class UsStatesCollection
        Inherits EnumerableQuery(Of State)

        ''' <summary>
        ''' Converts a state name to state code or state code to state name.
        ''' </summary>
        ''' <param name="nameOrCode">State name or code.</param>
        ''' <returns>State name or code.</returns>
        ''' <remarks></remarks>
        Public Shared Function ConvertNameOrCode(ByVal nameOrCode As String) As String
            Contracts.Contract.Requires(String.IsNullOrEmpty(nameOrCode) = False, "Argument cannot be null.")

            nameOrCode = nameOrCode.Trim

            Dim countryResult As String = String.Empty

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
        ''' Constructor that auto loads the states list.
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            MyBase.New(DataLists.StatesList)
        End Sub

        ''' <summary>
        ''' Constructor that loads the states
        ''' </summary>
        ''' <param name="list">List of <see cref="State">states</see>.</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal list As IEnumerable(Of State))
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