// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard.Tester
// Author           : David McCarter
// Created          : 01-19-2019
//
// Last Modified By : David McCarter
// Last Modified On : 05-31-2019
// ***********************************************************************
// <copyright file="RandomData.cs" company="dotNetTips.Utility.Standard.Tester">
//     Copyright (c) dotNetTips.com - McCarter Consulting. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Text;

namespace dotNetTips.Utility.Standard.Tester.Data
{
    /// <summary>
    /// Class RandomData.
    /// </summary>
    /// <remarks>Original code from: https://github.com/andrewseward/Any-.Net </remarks>
    public static class RandomData
    {
        /// <summary>
        /// The default maximum character
        /// </summary>
        private const char DefaultMaxCharacter = 'z';

        /// <summary>
        /// The default minimum character
        /// </summary>
        private const char DefaultMinCharacter = 'A';

        /// <summary>
        /// The domain extensions
        /// </summary>
        private static readonly string[] DomainExtensions = new string[] { ".com", ".co.uk", ".org", ".org.uk", ".net", ".us", ".com.au", ".es", ".fr", ".de", ".ly", ".gov", ".gov.uk", ".ac.uk" };

        /// <summary>
        /// The random
        /// </summary>
        private static readonly Random Random = new Random();

        /// <summary>
        /// The synchronize lock
        /// </summary>
        private static readonly object SyncLock = new object();

        /// <summary>
        /// Characters this instance.
        /// </summary>
        /// <returns>System.Char.</returns>
        public static char Character() => Character(DefaultMinCharacter, DefaultMaxCharacter);

        /// <summary>
        /// Characters the specified minimum value.
        /// </summary>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <returns>System.Char.</returns>
        public static char Character(char minValue, char maxValue) => (char)Integer(minValue, maxValue);

        /// <summary>
        /// Decimals the specified minimum value.
        /// </summary>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <param name="decimalPlaces">The decimal places.</param>
        /// <returns>System.Decimal.</returns>
        public static decimal Decimal(decimal minValue, decimal maxValue, int decimalPlaces)
        {
            var multiplier = ((decimal)decimalPlaces) * 10;

            var result = Integer((int)(minValue * multiplier), (int)(maxValue * multiplier)) / multiplier;

            return result;
        }

        /// <summary>
        /// Domains the extension.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string DomainExtension() => Of(DomainExtensions);

        /// <summary>
        /// Emails the address.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string EmailAddress()
        {
            var emailAddress = new StringBuilder();

            emailAddress.Append(Word(1, 25, 'a', 'z'));
            emailAddress.Append("@");
            emailAddress.Append(Word(1, 25, 'a', 'z'));
            emailAddress.Append(DomainExtension());

            return emailAddress.ToString();
        }

        /// <summary>
        /// Integers the specified minimum int.
        /// </summary>
        /// <param name="min">The minimum int.</param>
        /// <param name="max">The maximum int.</param>
        /// <returns>System.Int32.</returns>
        public static int Integer(int min, int max)
        {
            lock (SyncLock)
            {
                return Random.Next(min, max);
            }
        }

        /// <summary>
        /// Keys this instance.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string Key()
        {
            return Guid.NewGuid().ToString().Replace("-", string.Empty);
        }

        /// <summary>
        /// Ofs the specified words.
        /// </summary>
        /// <param name="words">The words.</param>
        /// <returns>System.String.</returns>
        public static string Of(params string[] words) => words[Integer(0, words.Length - 1)];

        /// <summary>
        /// Relatives the URL.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string RelativeUrl()
        {
            var url = new StringBuilder();

            for (var i = 0; i < Integer(1, 10); i++)
            {
                url.Append(UrlPart());
            }

            url.Append("/");

            return url.ToString();
        }

        /// <summary>
        /// URLs this instance.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string Url()
        {
            var url = new StringBuilder();

            url.Append(UrlHostName());
            url.Append(RelativeUrl());

            return url.ToString();
        }

        /// <summary>
        /// URLs the hostname.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string UrlHostName()
        {
            var url = new StringBuilder();

            url.Append("https://");

            url.Append(UrlHostnameWithNoProtocol());

            return url.ToString();
        }

        /// <summary>
        /// URLs the hostname with no protocol.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string UrlHostnameWithNoProtocol()
        {
            var url = new StringBuilder();

            url.Append(Of("www.", Word(1, 25, 'a', 'z') + ".", string.Empty));

            url.Append(UrlHostnameWithoutSubdomain());

            return url.ToString();
        }

        /// <summary>
        /// URLs the hostname without subdomain.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string UrlHostnameWithoutSubdomain()
        {
            var url = new StringBuilder();

            url.Append(Word(3, 25, 'a', 'z'));

            url.Append(DomainExtension());

            return url.ToString();
        }

        /// <summary>
        /// URLs the part.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string UrlPart()
        {
            var url = new StringBuilder();

            url.Append("/");

            url.Append(Word(1, 25, 'a', 'z'));

            return url.ToString();
        }

        /// <summary>
        /// Words the specified length.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns>System.String.</returns>
        public static string Word(int length) => Word(length, DefaultMinCharacter, DefaultMaxCharacter);

        /// <summary>
        /// Words the specified minimum length.
        /// </summary>
        /// <param name="minLength">The minimum length.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <returns>System.String.</returns>
        public static string Word(int minLength, int maxLength) => Word(minLength, maxLength, DefaultMinCharacter, DefaultMaxCharacter);

        /// <summary>
        /// Words the specified length.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <param name="minCharacter">The minimum character.</param>
        /// <param name="maxCharacter">The maximum character.</param>
        /// <returns>System.String.</returns>
        public static string Word(int length, char minCharacter, char maxCharacter)
        {
            var word = new StringBuilder(length);

            for (var i = 0; i < length; i++)
            {
                word.Append(Character(minCharacter, maxCharacter));
            }

            return word.ToString();
        }

        /// <summary>
        /// Words the specified minimum length.
        /// </summary>
        /// <param name="minLength">The minimum length.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <param name="minCharacter">The minimum character.</param>
        /// <param name="maxCharacter">The maximum character.</param>
        /// <returns>System.String.</returns>
        public static string Word(int minLength, int maxLength, char minCharacter, char maxCharacter) => Word(Integer(minLength, maxLength), minCharacter, maxCharacter);
    }
}
