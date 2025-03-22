using System;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine;

namespace PostGhost.IniForUnity
{
    public class Color32TypeConverter : TypeConverter
    {
        public static readonly Color32TypeConverter Instance = new();

        private static readonly Regex s_color32Regex = new(@"\((\d{1,3}),\s*(\d{1,3}),\s*(\d{1,3}),?\s*(\d{1,3})\)");

        private static bool TryGetColor32FromMatch(Match match, out Color32 color32)
        {
            string r = match.Groups[1].Value;
            if (!byte.TryParse(r, out byte rByte))
            {
                color32 = default;
                return false;
            }

            string g = match.Groups[2].Value;
            if (!byte.TryParse(g, out byte gByte))
            {
                color32 = default;
                return false;
            }

            string b = match.Groups[3].Value;
            if (!byte.TryParse(b, out byte bByte))
            {
                color32 = default;
                return false;
            }

            var aByte = byte.MaxValue;
            if (match.Groups.Count > 4)
            {
                string a = match.Groups[4].Value;
                if (!byte.TryParse(a, out aByte))
                {
                    color32 = default;
                    return false;
                }
            }

            color32 = new Color32(rByte, gByte, bByte, aByte);
            return true;
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(Color32))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || sourceType == typeof(Color) || sourceType == typeof(Color32);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            switch (value)
            {
                case Color32 color32Value:
                {
                    return color32Value;
                }
                case Color colorValue:
                {
                    return (Color32)colorValue;
                }
            }

            if (value is not string stringValue)
            {
                return base.ConvertFrom(context, culture, value);
            }

            if (string.IsNullOrEmpty(stringValue))
            {
                return base.ConvertFrom(context, culture, value);
            }

            if (ColorUtility.TryParseHtmlString(stringValue, out Color color))
            {
                return (Color32)color;
            }

            Match match = s_color32Regex.Match(stringValue);
            if (match.Success && TryGetColor32FromMatch(match, out Color32 color32))
            {
                return color32;
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}