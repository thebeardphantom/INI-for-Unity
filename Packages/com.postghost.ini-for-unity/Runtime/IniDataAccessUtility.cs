namespace PostGhost.IniForUnity
{
    /// <summary>
    /// Extension methods for working with <see cref="IIniDataAccess" />.
    /// </summary>
    public static class IniDataAccessUtility
    {
        public static T GetValue<T>(this IIniDataAccess iniDataAccess, string keyName, T defaultValue = default)
        {
            return iniDataAccess.TryGetValue(keyName, out T value) ? value : defaultValue;
        }

        public static T GetValue<T>(this IIniDataAccess iniDataAccess, string sectionName, string keyName, T defaultValue = default)
        {
            return iniDataAccess.TryGetValue(sectionName, keyName, out T value) ? value : defaultValue;
        }

        public static T ConvertFromString<T>(this IIniDataAccess iniDataAccess, string valueString, T defaultValue = default)
        {
            return iniDataAccess.TryConvertFromString(valueString, out T value) ? value : defaultValue;
        }
    }
}