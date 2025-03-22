using System;
using System.ComponentModel;
using System.Globalization;
using UnityEngine;

namespace PostGhost.IniForUnity
{
    public class ColorTypeConverter : TypeConverter
    {
        public static readonly ColorTypeConverter Instance = new();

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(Color))
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
                    return (Color)color32Value;
                }
                case Color colorValue:
                {
                    return colorValue;
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
                return color;
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}