using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecondIINoneMC.Core.Enums
{
    public enum WeekNumber
    {
        First = 1, 
        Second, 
        Third,
        Fourth,
        Fifth
    }

    public class WeekNumbers
    {
        private static List<String> weekNumbers;

        public static List<String> WeekNumberCollection
        {
            get
            {
                if (weekNumbers == null)
                {
                    weekNumbers = new List<String>(Enum.GetNames(typeof(WeekNumber)));
                }
                return weekNumbers;
            }
        }
    }
}
