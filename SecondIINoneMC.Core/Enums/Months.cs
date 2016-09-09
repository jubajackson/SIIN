using System;
using System.Collections.Generic;
using System.Text;

namespace SecondIINoneMC.Core.Enums
{
	public enum Month
	{
        None = 0,
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12
    }

    public class Months
    {
        private static List<String> months;

        public static List<String> MonthCollection
        {
            get
            {
                if (months == null)
                {
                    months = new List<String>(Enum.GetNames(typeof(Month)));
                }
                return months;
            }
        }
    }
}
