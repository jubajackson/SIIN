using System;
using System.Collections.Generic;
using System.Text;

namespace SecondIINoneMC.Core.Enums
{
	public enum State
	{
        None,
        AL,
        AK,
		AZ,
		AR,
		CA,
		CO,
		CT,
		DE,
		DC,
		FL,
		GA,
		HI,
		ID,
		IL,
		IN,
		IA,
		KS,
		KY,
		LA,
		ME,
		MD,
		MA,
		MI,
		MN,
		MS,
		MO,
		MT,
		NE,
		NV,
		NH,
		NJ,
		NM,
		NY,
		NC,
		ND,
		MP,
		OH,
		OK,
		OR,
		PA,
		RI,
		SC,
		SD,
		TN,
		TX,
		UT,
		VT,
		VA,
		WA,
		WV,
		WI,
		WY,
	}

    public class States
    {
        private static List<String> states;

        public static List<String> StateCollection
        {
            get
            {
                if (states == null)
                {
                    states = new List<String>(Enum.GetNames(typeof(State)));
                }
                return states;
            }
        }

        #region Full State Name
        public static String GetFullStateName(string stateAbbreviation)
        {

            switch (stateAbbreviation)
            {
                case "AK":
                    return "Alaska";

                case "AL":
                    return "Alabama";

                case "AR":
                    return "Arkansas";

                case "AZ":
                    return "Arizona";

                case "CA":
                    return "California";

                case "CO":
                    return "Colorado";

                case "CT":
                    return "Connecticut";

                case "DC":
                    return "District Of Columbia";

                case "DE":
                    return "Delaware";

                case "FG":
                    return "Foreign";

                case "FL":
                    return "Florida";

                case "GA":
                    return "Georgia";

                case "HI":
                    return "Hawaii";

                case "IA":
                    return "Iowa";

                case "ID":
                    return "Idaho";

                case "IL":
                    return "Illinois";

                case "IN":
                    return "Indiana";

                case "KS":
                    return "Kansas";

                case "KY":
                    return "Kentucky";

                case "LA":
                    return "Louisiana";

                case "MA":
                    return "Massachusetts";

                case "MD":
                    return "Maryland";

                case "ME":
                    return "Maine";

                case "MI":
                    return "Michigan";

                case "MN":
                    return "Minnesota";

                case "MO":
                    return "Missouri";

                case "MS":
                    return "Mississippi";

                case "MT":
                    return "Montana";

                case "NC":
                    return "North Carolina";

                case "ND":
                    return "North Dakota";

                case "NE":
                    return "Nebraska";

                case "NH":
                    return "New Hampshire";

                case "NJ":
                    return "New Jersey";

                case "NM":
                    return "New Mexico";

                case "NV":
                    return "Nevada";

                case "NY":
                    return "New York";

                case "OH":
                    return "Ohio";

                case "OK":
                    return "Oklahoma";

                case "OR":
                    return "Oregon";

                case "PA":
                    return "Pennsylvania";

                case "RI":
                    return "Rhode Island";

                case "SC":
                    return "South Carolina";

                case "SD":
                    return "South Dakota";

                case "TN":
                    return "Tennessee";

                case "TX":
                    return "Texas";

                case "UT":
                    return "Utah";

                case "VA":
                    return "Virginia";

                case "VT":
                    return "Vermont";

                case "WA":
                    return "Washington";

                case "WI":
                    return "Wisconsin";

                case "WV":
                    return "West Virginia";

                case "WY":
                    return "Wyoming";

                #region Territories

                case "AQ":
                    return "American Samoa";

                case "GU":
                    return "Guam";

                case "RM":
                    return "Marshall Islands";

                case "FM":
                    return "Federated States of Micronesia";

                case "CQ":
                    return "Northern Mariana Islands";

                case "PW":
                    return "Palau";

                case "PR":
                    return "Puerto Rico";

                case "VI":
                    return "Virgin Islands";

                #endregion

                default:
                    return "Unknown";
            }
        }
        #endregion
    }
}
