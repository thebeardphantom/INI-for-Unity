using System;
using System.ComponentModel;
using System.Globalization;

namespace PostGhost.IniForUnity
{
    public class IniBoolTypeConverter : BooleanConverter
    {
        public static readonly IniBoolTypeConverter Instance = new();

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(bool))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is int intValue)
            {
                return intValue != 0;
            }

            if (value is bool boolValue)
            {
                return boolValue;
            }

            if (value is not string stringValue)
            {
                return base.ConvertFrom(context, culture, value);
            }

            if (string.IsNullOrEmpty(stringValue))
            {
                return base.ConvertFrom(context, culture, value);
            }

            if (int.TryParse(stringValue, out intValue))
            {
                return intValue != 0;
            }

            if (bool.TryParse(stringValue, out boolValue))
            {
                return boolValue;
            }

            stringValue = stringValue.Trim();
            if (stringValue.Equals("yes", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (stringValue.Equals("no", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            
            if (stringValue.Equals("on", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (stringValue.Equals("off", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}