using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.IO;
using System.Linq.Expressions;

using SecondIINoneMC.Helpers;
using SecondIINoneMC.Core.Enums;

namespace SecondIINoneMC.Helpers
{
    public static class SecondIINoneMCHelper
    {
        #region PUBLIC METHODS

        public static List<KeyValuePair<int, string>> GetStates()
        {
            var statesList = new List<KeyValuePair<int, string>>();

            foreach (String state in States.StateCollection)
            {
                var enumKey = (int)Enum.Parse(typeof(State), state);
                var enumValue = States.GetFullStateName(state);
                statesList.Add(new KeyValuePair<int, string>(enumKey, enumValue));
            }

            return statesList;
        }

        public static void AddErrorsToModelState(ModelStateDictionary modelState)
        {
            var errorList = new List<string>();

            foreach (var obj in modelState.Values)
            {
                foreach (var error in obj.Errors)
                {
                    if (!string.IsNullOrEmpty(error.ErrorMessage))
                        errorList.Add(error.ErrorMessage);
                }
            }

            foreach (var message in errorList)
                modelState.AddModelError("", message);
        }

        public static List<KeyValuePair<string, int>> GetSortOrderOptions()
        {
            var keyValuePairList = new List<KeyValuePair<string, int>>();

            keyValuePairList.Add(new KeyValuePair<string, int>("Ascending", 1));
            keyValuePairList.Add(new KeyValuePair<string, int>("Descending", 2));

            return keyValuePairList;
        }

        #endregion
    }
}
