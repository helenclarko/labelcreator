using System;
using System.Collections.Generic;
using System.Text;

namespace DVDScribe
{
    class Utils
    {
        public static string getEnumText(Enum e)
        {
            System.Reflection.FieldInfo EnumInfo = e.GetType().GetField(e.ToString());
            System.ComponentModel.DescriptionAttribute[] EnumAttributes =
                (System.ComponentModel.DescriptionAttribute[])
                EnumInfo.GetCustomAttributes
                    (typeof(System.ComponentModel.DescriptionAttribute), false);
            if (EnumAttributes.Length > 0)
            {
                return EnumAttributes[0].Description;
            }
            return e.ToString();

        }
    }
}
