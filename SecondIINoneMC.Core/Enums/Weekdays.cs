using System;
using System.Collections.Generic;
using System.Text;

namespace SecondIINoneMC.Core.Enums
{
	public enum Weekday
	{
        None = 0,
        Sunday = 1,
        Monday = 2,
        Tuesday = 3,
        Wednesday = 4,
        Thursday = 5,
        Friday = 6,
        Saturday = 7
    }

    public class Weekdays
    {
        private static List<String> weekdays;

        public static List<String> WeekdayCollection
        {
            get
            {
                if (weekdays == null)
                {
                    weekdays = new List<String>(Enum.GetNames(typeof(Weekday)));
                }
                return weekdays;
            }
        }
    }
}
