using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Syroot.BinaryData.Core
{
    /// <summary>
    /// Represents a collection of extension methods for the <see cref="Type"/> class.
    /// </summary>
    internal static class TypeExtensions
    {
        // ---- FIELDS -------------------------------------------------------------------------------------------------
        
        private static readonly TypeInfo _iEnumerableTypeInfo = typeof(IEnumerable).GetTypeInfo();

        // ---- METHODS (INTERNAL) -------------------------------------------------------------------------------------
        
        /// <summary>
        /// Returns a value indicating whether the given <paramref name="type"/> is enumerable. Returns <c>false</c> for
        /// non-enumerable objects and strings.
        /// </summary>
        /// <param name="type">The type which should be checked.</param>
        /// <returns><c>true</c> if the type is enumerable and not a string; otherwise <c>false</c>.</returns>
        internal static bool IsEnumerable(this Type type)
        {
            return type != typeof(String) && (type.IsArray || _iEnumerableTypeInfo.IsAssignableFrom(type));
        }

        /// <summary>
        /// Gets the element type of <see cref="IEnumerable"/> instances. Returns <c>null</c> for non-enumerable objects
        /// and strings.
        /// </summary>
        /// <param name="type">The type which element type should be returned.</param>
        /// <returns>The type of the elements, or <c>null</c>.</returns>
        internal static Type GetEnumerableElementType(this Type type)
        {
            // Do not handle strings as enumerables, they are stored differently.
            if (type == typeof(String))
            {
                return null;
            }

            // Check for array instances.
            if (type.IsArray)
            {
                Type elementType;
                if (type.GetArrayRank() > 1 || (elementType = type.GetElementType()).IsArray)
                {
                    throw new NotImplementedException(
                        $"Type {type} is a multidimensional array and not supported at the moment.");
                }
                return elementType;
            }

            // Check for IEnumerable instances. Only the first implementation of IEnumerable<> is returned.
            if (_iEnumerableTypeInfo.IsAssignableFrom(type))
            {
                foreach (Type interfaceType in type.GetTypeInfo().GetInterfaces())
                {
                    TypeInfo interfaceTypeInfo = interfaceType.GetTypeInfo();
                    if (interfaceTypeInfo.IsGenericType
                        && interfaceTypeInfo.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                    {
                        return interfaceTypeInfo.GetGenericArguments()[0];
                    }
                }
            }

            return null;
        }

        internal static bool TryGetEnumerableElementType(this Type type, out Type elementType)
        {
            // Do not handle strings as enumerables, they are stored differently.
            if (type != typeof(String))
            {
                // Check for array instances.
                if (type.IsArray)
                {
                    Type elemType;
                    if (type.GetArrayRank() > 1 || (elemType = type.GetElementType()).IsArray)
                    {
                        throw new NotImplementedException(
                            $"Type {type} is a multidimensional array and not supported at the moment.");
                    }
                    elementType = elemType;
                    return true;
                }

                // Check for IEnumerable instances. Only the first implementation of IEnumerable<> is returned.
                if (_iEnumerableTypeInfo.IsAssignableFrom(type))
                {
                    foreach (Type interfaceType in type.GetTypeInfo().GetInterfaces())
                    {
                        TypeInfo interfaceTypeInfo = interfaceType.GetTypeInfo();
                        if (interfaceTypeInfo.IsGenericType
                            && interfaceTypeInfo.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                        {
                            elementType = interfaceTypeInfo.GetGenericArguments()[0];
                            return true;
                        }
                    }
                }
            }

            elementType = null;
            return false;
        }
    }
}
