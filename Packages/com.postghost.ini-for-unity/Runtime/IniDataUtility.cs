using IniParser.Model;
using System.ComponentModel;

namespace PostGhost.IniForUnity
{
    /// <summary>
    /// Extension methods for working with <see cref="IniData" />.
    /// </summary>
    public static class IniDataUtility
    {
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

            TypeConverter typeConverter = TypeDescriptor.GetConverter(typeof(T));
            object valueObject = typeConverter.ConvertFromString(valueString);

            if (valueObject == null)
            {
                value = default;
                return false;
            }

            value = (T)valueObject;
            return true;
        }

        public static T GetValue<T>(this IniData iniData, string sectionName, string keyName, T defaultValue = default)
        {
            return iniData.TryGetValue(sectionName, keyName, out T value) ? value : defaultValue;
        }

        public static T GetValue<T>(this KeyDataCollection section, string keyName, T defaultValue = default)
        {
            return section.TryGetValue(keyName, out T value) ? value : defaultValue;
        }
    }
}