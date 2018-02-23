// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard
// Author           : David McCarter
// Created          : 08-09-2017
//
// Last Modified By : David McCarter
// Last Modified On : 02-22-2018
// ***********************************************************************
// <copyright file="TypeHelper.cs" company="dotNetTips.com - David McCarter">
//     dotNetTips.com - David McCarter
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using dotNetTips.Utility.Standard.OOP;

namespace dotNetTips.Utility.Standard
{
    /// <summary>
    /// Class TypeHelper.
    /// </summary>
    public static class TypeHelper
    {
        /// <summary>
        /// Creates type instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>T.</returns>
        /// <remarks>Original code by: Jeremy Clark</remarks>
        public static T Create<T>()
            where T : class
        {
            var instance = Activator.CreateInstance<T>();

            return instance is T ? instance : null;
        }
        /// <summary>
        /// Does the object equal instance.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="instance">The instance.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool DoesObjectEqualInstance(object value, object instance)
        {
            var result = object.ReferenceEquals(value, instance);

            return result;

        }
        /// <summary>
        /// Gets the default.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>T.</returns>
        public static T GetDefault<T>()
        {
            var result = default(T);

            return result;
        }
        /// <summary>
        /// Gets the instance hash code.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>Int32.</returns>
        public static int GetInstanceHashCode(object instance)
        {
            var hash = -1;

            foreach (var prop in instance.GetType().GetRuntimeProperties().Where(p => p != null))
            {
                var value = prop.GetValue(instance);

                if (value != null)
                {
                    hash = hash ^ value.GetHashCode();
                }
            }

            return hash;
        }

        /// <summary>
        /// Finds the derived types.
        /// </summary>
        /// <param name="baseType">Type of the base.</param>
        /// <param name="classOnly">if set to <c>true</c> [class only].</param>
        /// <returns>IEnumerable&lt;Type&gt;.</returns>
        public static IEnumerable<Type> FindDerivedTypes(Type baseType, bool classOnly)
        {
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            return FindDerivedTypes(path, SearchOption.TopDirectoryOnly, baseType, classOnly);
        }

        /// <summary>
        /// Finds the derived types.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="baseType">Type of the base.</param>
        /// <param name="classOnly">if set to <c>true</c> [class only].</param>
        /// <returns>IEnumerable&lt;Type&gt;.</returns>
        /// <exception cref="FileNotFoundException">Could not find assembly.</exception>
        /// <exception cref="ArgumentNullException">Could not find assembly.</exception>
        public static IEnumerable<Type> FindDerivedTypes(string path, SearchOption searchOption, Type baseType, bool classOnly)
        {
            Encapsulation.TryValidateParam(path, nameof(path), "Must pass in path and file name to the assembly.");

            Encapsulation.TryValidateParam<ArgumentNullException>(baseType != null, nameof(baseType), "Parent Type must be defined");

            var returnValue = new List<Type>();

            if (Directory.Exists(path) == false)
            {
                throw new FileNotFoundException("Could not find assembly.", path);
            }

            var files = Directory.EnumerateFiles(path, "*.dll", searchOption);

            foreach (var file in files)
            {
                try
                {
                    var assy = Assembly.LoadFile(file);

                    var containsBaseType = assy.ExportedTypes.Any(p => (p.BaseType != null && p.BaseType.FullName == baseType.FullName));

                    if (containsBaseType)
                    {
                        returnValue.AddRange(LoadDerivedTypes(assy.DefinedTypes, baseType, classOnly));
                    }
                }
                catch (Exception ex)
                {
                    var test = ex.Message;
                }
            }

            return returnValue;
        }


        /// <summary>
        /// Finds the derived types.
        /// </summary>
        /// <param name="currentDomain">The current domain.</param>
        /// <param name="baseType">Type of the base.</param>
        /// <param name="classOnly">if set to <c>true</c> [class only].</param>
        /// <returns>IEnumerable&lt;Type&gt;.</returns>
        public static IEnumerable<Type> FindDerivedTypes(AppDomain currentDomain, Type baseType, bool classOnly)
        {
            Encapsulation.TryValidateParam<ArgumentNullException>(currentDomain != null, nameof(currentDomain));

            Encapsulation.TryValidateParam<ArgumentNullException>(baseType != null, nameof(baseType));

            List<Type> types = null;

            foreach (var assy in currentDomain.GetAssemblies())
            {
                try
                {
                    var tempTypes = LoadDerivedTypes(assy.DefinedTypes, baseType, classOnly).ToList();

                    if (tempTypes.Count() > 0)
                    {
                        if (types == null)
                        {
                            types = tempTypes;
                        }
                        else
                        {
                            types.AddRange(tempTypes);
                        }
                    }
                }
                catch (Exception ex)
                {
                    var test = ex.Message;
                }
            }

            return types;
        }

        /// <summary>
        /// Loads the derived types.
        /// </summary>
        /// <param name="types">The types.</param>
        /// <param name="baseType">Type of the base.</param>
        /// <param name="classOnly">if set to <c>true</c> [class only].</param>
        /// <returns>IEnumerable&lt;Type&gt;.</returns>
        private static IEnumerable<Type> LoadDerivedTypes(IEnumerable<TypeInfo> types, Type baseType, bool classOnly)
        {
            // works out the derived types
            foreach (var type in types)
            {
                // if classOnly, it must be a class
                // useful when you want to create instance
                if (classOnly && !type.IsClass)
                {
                    continue;
                }

                if (baseType.IsInterface)
                {
                    if (type.GetInterface(baseType.FullName) != null)
                    {
                        // add it to result list
                        yield return type;
                    }
                }
                else if (type.IsSubclassOf(baseType))
                {
                    // add it to result list
                    yield return type;
                }
            }
        }
    }
}