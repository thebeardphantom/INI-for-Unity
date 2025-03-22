using IniParser.Model;
using IniParser.Model.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace PostGhost.IniForUnity
{
    /// <summary>
    /// Extension methods for working with <see cref="IniData" />.
    /// </summary>
    public static class IniDataUtility
    {
        public static readonly IniParserConfiguration DefaultIniParserConfiguration = new();

        private static readonly Dictionary<Type, TypeConverter> s_typeToTypeConverter = new()
        {
            { typeof(Color), ColorTypeConverter.Instance },
            { typeof(Color32), Color32TypeConverter.Instance },
            { typeof(bool), IniBoolTypeConverter.Instance },
        };

        public static Func<IniParserConfiguration> ParserConfigurationFactory = () => DefaultIniParserConfiguration;

        public static bool TryGetValue<T>(this IniData iniData, string sectionName, string keyName, out T value)
        {
            SectionData section = iniData.Sections.GetSectionData(sectionName);
            if (section == null)
            {
                value = default;
                return false;
            }

            return TryGetValue(section.Keys, keyName, out value);
        }

        public static bool TryGetValue<T>(this KeyDataCollection section, string keyName, out T value)
        {
            string valueString = section[keyName];
            if (valueString == null)
            {
                value = default;
                return false;
            }

            return TryConvertFromString(valueString, out value);
        }

        public static T GetValue<T>(this IniData iniData, string sectionName, string keyName, T defaultValue = default)
        {
            return TryGetValue(iniData, sectionName, keyName, out T value) ? value : defaultValue;
        }

        public static T GetValue<T>(this KeyDataCollection section, string keyName, T defaultValue = default)
        {
            return TryGetValue(section, keyName, out T value) ? value : defaultValue;
        }

        public static bool TryConvertFromString<T>(string valueString, out T value)
        {
            if (!s_typeToTypeConverter.TryGetValue(typeof(T), out TypeConverter typeConverter))
            {
                typeConverter = TypeDescriptor.GetConverter(typeof(T));
            }

            object valueObject = typeConverter.ConvertFromString(valueString);

            if (valueObject == null)
            {
                value = default;
                return false;
            }

            value = (T)valueObject;
            return true;
        }
    }
}