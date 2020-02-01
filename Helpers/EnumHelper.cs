using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MapTest.Helpers
{
    /// <summary>
    /// Shared constants used by Flags and Enums.
    /// </summary>
    internal static class EnumInternals<T> where T : struct, IEnumConstraint
    {
        internal static readonly bool IsFlags;
        internal static readonly Func<T, T, T> Or;
        internal static readonly Func<T, T, T> And;
        internal static readonly Func<T, T> Not;
        internal static readonly T UsedBits;
        internal static readonly T AllBits;
        internal static readonly T UnusedBits;
        internal static Func<T, T, bool> Equality;
        internal static readonly Func<T, bool> IsEmpty;
        internal static readonly IList<T> Values;
        internal static readonly IList<string> Names;
        internal static readonly Type UnderlyingType;
        internal static readonly Dictionary<T, string> ValueToDescriptionMap;
        internal static readonly Dictionary<string, T> DescriptionToValueMap;

        static EnumInternals()
        {
            Values = new ReadOnlyCollection<T>((T[])Enum.GetValues(typeof(T)));
            Names = new ReadOnlyCollection<string>(Enum.GetNames(typeof(T)));
            ValueToDescriptionMap = new Dictionary<T, string>();
            DescriptionToValueMap = new Dictionary<string, T>();
            foreach (T value in Values)
            {
                string description = GetDescription(value);
                ValueToDescriptionMap[value] = description;
                if (description != null && !DescriptionToValueMap.ContainsKey(description))
                {
                    DescriptionToValueMap[description] = value;
                }
            }
            UnderlyingType = Enum.GetUnderlyingType(typeof(T));
            IsFlags = typeof(T).IsDefined(typeof(FlagsAttribute), false);
            // Parameters for various expression trees
            ParameterExpression param1 = Expression.Parameter(typeof(T), "x");
            ParameterExpression param2 = Expression.Parameter(typeof(T), "y");
            Expression convertedParam1 = Expression.Convert(param1, UnderlyingType);
            Expression convertedParam2 = Expression.Convert(param2, UnderlyingType);
            Equality = Expression.Lambda<Func<T, T, bool>>(Expression.Equal(convertedParam1, convertedParam2), param1, param2).Compile();
            Or = Expression.Lambda<Func<T, T, T>>(Expression.Convert(Expression.Or(convertedParam1, convertedParam2), typeof(T)), param1, param2).Compile();
            And = Expression.Lambda<Func<T, T, T>>(Expression.Convert(Expression.And(convertedParam1, convertedParam2), typeof(T)), param1, param2).Compile();
            Not = Expression.Lambda<Func<T, T>>(Expression.Convert(Expression.Not(convertedParam1), typeof(T)), param1).Compile();
            IsEmpty = Expression.Lambda<Func<T, bool>>(Expression.Equal(convertedParam1,
                Expression.Constant(Activator.CreateInstance(UnderlyingType))), param1).Compile();

            UsedBits = default(T);
            foreach (T value in Enums.GetValues<T>())
            {
                UsedBits = Or(UsedBits, value);
            }
            AllBits = Not(default(T));
            UnusedBits = And(AllBits, (Not(UsedBits)));
        }

        private static string GetDescription(T value)
        {
            FieldInfo field = typeof(T).GetField(value.ToString());
            return field.GetCustomAttributes(typeof(DescriptionAttribute), false)
                        .Cast<DescriptionAttribute>()
                        .Select(x => x.Description)
                        .FirstOrDefault();
        }
    }

    /// <summary>
    /// Dummy interface only used to represent an enum constraint.
    /// </summary>
    public interface IEnumConstraint
    {
    }

    /// <summary>
    /// Provides a set of static methods for use with enum types. Much of
    /// what's available here is already in System.Enum, but this class
    /// provides a strongly typed API.
    /// </summary>
    public static class Enums
    {
        /// <summary>
        /// Returns an array of values in the enum.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns>An array of values in the enum</returns>
        public static T[] GetValuesArray<T>() where T : struct, IEnumConstraint
        {
            return (T[])Enum.GetValues(typeof(T));
        }

        /// <summary>
        /// Returns the values for the given enum as an immutable list.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        public static IList<T> GetValues<T>() where T : struct, IEnumConstraint
        {
            return EnumInternals<T>.Values;
        }

        /// <summary>
        /// Returns an array of names in the enum.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns>An array of names in the enum</returns>
        public static string[] GetNamesArray<T>() where T : struct, IEnumConstraint
        {
            return Enum.GetNames(typeof(T));
        }

        /// <summary>
        /// Returns the names for the given enum as an immutable list.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns>An array of names in the enum</returns>
        public static IList<string> GetNames<T>() where T : struct, IEnumConstraint
        {
            return EnumInternals<T>.Names;
        }

        /// <summary>
        /// Checks whether the value is a named value for the type.
        /// </summary>
        /// <remarks>
        /// For flags enums, it is possible for a value to be a valid
        /// combination of other values without being a named value
        /// in itself. To test for this possibility, use IsValidCombination.
        /// </remarks>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="value">Value to test</param>
        /// <returns>True if this value has a name, False otherwise.</returns>
        public static bool IsNamedValue<T>(this T value) where T : struct, IEnumConstraint
        {
            // TODO: Speed this up for big enums
            return GetValues<T>().Contains(value);
        }

        /// <summary>
        /// Returns the description for the given value, 
        /// as specified by DescriptionAttribute, or null
        /// if no description is present.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="item">Value to fetch description for</param>
        /// <returns>The description of the value, or null if no description
        /// has been specified (but the value is a named value).</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="item"/>
        /// is not a named member of the enum</exception>
        public static string GetDescription<T>(this T item) where T : struct, IEnumConstraint
        {
            string description;
            if (EnumInternals<T>.ValueToDescriptionMap.TryGetValue(item, out description))
            {
                return description;
            }
            throw new ArgumentOutOfRangeException("item");
        }

        public static string GetDisplayName(this Enum enumValue)
        {
            var enumMember = enumValue.GetType().GetMember(enumValue.ToString()).First();
            return enumMember.GetCustomAttribute<DisplayAttribute>() != null ? enumMember.GetCustomAttribute<DisplayAttribute>().Name : enumMember.Name;
        }

        /// <summary>
        /// Attempts to find a value with the given description.
        /// </summary>
        /// <remarks>
        /// More than one value may have the same description. In this unlikely
        /// situation, the first value with the specified description is returned.
        /// </remarks>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="description">Description to find</param>
        /// <param name="value">Enum value corresponding to given description (on return)</param>
        /// <returns>True if a value with the given description was found,
        /// false otherwise.</returns>
        public static bool TryParseDescription<T>(string description, out T value)
            where T : struct, IEnumConstraint
        {
            return EnumInternals<T>.DescriptionToValueMap.TryGetValue(description, out value);
        }

        /// <summary>
        /// Parses the name of an enum value.
        /// </summary>
        /// <remarks>
        /// This method only considers named values: it does not parse comma-separated
        /// combinations of flags enums.
        /// </remarks>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns>The parsed value</returns>
        /// <exception cref="ArgumentException">The name could not be parsed.</exception>
        public static T ParseName<T>(string name) where T : struct, IEnumConstraint
        {
            T value;
            if (!TryParseName(name, out value))
            {
                throw new ArgumentException("Unknown name", "name");
            }
            return value;
        }

        /// <summary>
        /// Attempts to find a value for the specified name.
        /// Only names are considered - not numeric values.
        /// </summary>
        /// <remarks>
        /// If the name is not parsed, <paramref name="value"/> will
        /// be set to the zero value of the enum. This method only
        /// considers named values: it does not parse comma-separated
        /// combinations of flags enums.
        /// </remarks>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="name">Name to parse</param>
        /// <param name="value">Enum value corresponding to given name (on return)</param>
        /// <returns>Whether the parse attempt was successful or not</returns>
        public static bool TryParseName<T>(string name, out T value) where T : struct, IEnumConstraint
        {
            // TODO: Speed this up for big enums
            int index = EnumInternals<T>.Names.IndexOf(name);
            if (index == -1)
            {
                value = default(T);
                return false;
            }
            value = EnumInternals<T>.Values[index];
            return true;
        }

        /// <summary>
        /// Returns the underlying type for the enum
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns>The underlying type (Byte, Int32 etc) for the enum</returns>
        public static Type GetUnderlyingType<T>() where T : struct, IEnumConstraint
        {
            return EnumInternals<T>.UnderlyingType;
        }

        public static string DescriptionAttr<T>(this T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0) return attributes[0].Description;
            else return source.ToString();
        }
    }
}
