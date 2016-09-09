using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecondIINoneMC.Core.Enums
{
    public enum PluralDay
    {
        First = 1, 
        Second, 
        Third,
        Fourth,
        Fifth,
        Sixth,
        Seventh,
        Eigth,
        Ninth,
        Tenth,
        Eleventh,
        Twelfth,
        Thirteen,
        Fourteen,
        Fifteen,
        Sixteen,
        Seventeen,
        Eighteen,
        Ninteen,
        Twentieth,
        TwentieFirst,
        TwentieSecond,
        TwentieThird,
        TwentieFourth,
        TwentieFifth,
        TwentieSixth,
        TwentieSeventh,
        TwentieEigth,
        TwentieNinth,
        Thirtieth,
        ThirtieFirst
    }

    public class PluralDays
    {
        private static List<String> pluralDays;

        public static List<String> PluralDayCollection
        {
            get
            {
                if (pluralDays == null)
                {
                    pluralDays = new List<String>(Enum.GetNames(typeof(PluralDay)));
                }
                return pluralDays;
            }
        }

        #region Full Plural Day Name

        public static String GetFullPluralDayName(String pluralDayName)
        {

            switch (pluralDayName)
            {
                case "First":
                    return "First";
                case "Second":
                    return "Second";
                case "Third":
                    return "Third";
                case "Fourth":
                    return "Fourth";
                case "Fifth":
                    return "Fifth";
                case "Sixth":
                    return "Sixth";
                case "Seventh":
                    return "Seventh";
                case "Eigth":
                    return "Eigth";
                case "Ninth":
                    return "Ninth";
                case "Tenth":
                    return "Tenth";
                case "Eleventh":
                    return "Eleventh";
                case "Twelfth":
                    return "Twelfth";
                case "Thirteenth":
                    return "Thirteenth";
                case "Fourteenth":
                    return "Fourteenth";
                case "Fifteenth":
                    return "Fifteenth";
                case "Sixteenth":
                    return "Sixteenth";
                case "Seventeenth":
                    return "Seventeenth";
                case "Eighteenth":
                    return "Eighteenth";
                case "Ninteenth":
                    return "Ninteenth";
                case "Twentieth":
                    return "Twentieth";
                case "TwentieFirst":
                    return "Twentie First";
                case "TwentieSecond":
                    return "Twentie Second";
                case "TwentieThird":
                    return "Twentie Third";
                case "TwentieFourth":
                    return "Twentie Fourth";
                case "TwentieFifth":
                    return "Twentie Fifth";
                case "TwentieSixth":
                    return "Twentie Sixth";
                case "TwentieSeventh":
                    return "Twentie Seventh";
                case "TwentieEigth":
                    return "Twentie Eigth";
                case "TwentieNinth":
                    return "Twentie Ninth";
                case "Thirtieth":
                    return "Thirtieth";
                case "ThirtieFirst":
                    return "Thirtie First";
                default:
                    return "First";
            }
        }

        #endregion
    
    }
}
