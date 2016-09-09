using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondIINoneMC.Core.Helpers
{
    /// <summary>
    /// Contains common methods for dictionaries
    /// </summary>
    public static class DictionaryHelper
    {
        /// <summary>
        /// Gets the dictionary value that exists of the <paramref name="key"/> or returns
        /// the <paramref name="defaultValue"/> whe there is no value.
        /// </summary>
        /// <param name="dict">The dictionary to search.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>
        /// the value when the <paramref name="key"/> exists or the <paramref name="defaultValue"/>
        /// </returns>
        public static string GetString(Dictionary<string, string> dict, string key, string defaultValue)
        {
            string output = defaultValue;

            if (IsNullOrEmpty(dict, key) == false)
            {
                output = dict[key];
            }

            return output;
        }

        /// <summary>
        /// Gets the dictionary value that exists of the <paramref name="key"/> or returns
        /// the <paramref name="defaultValue"/> whe there is no value.
        /// </summary>
        /// <param name="dict">The dictionary to search.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>
        /// the value when the <paramref name="key"/> exists or the <paramref name="defaultValue"/>
        /// </returns>
        public static bool GetBool(Dictionary<string, string> dict, string key, bool defaultValue)
        {
            bool output = defaultValue;

            if (IsNullOrEmpty(dict, key) == false)
            {
                var tempStr = dict[key].ToUpper();

                output = tempStr == "TRUE" ||
                         tempStr == "T" ||
                         tempStr == "1";
            }

            return output;
        }

        /// <summary>
        /// Gets the dictionary value that exists of the <paramref name="key"/> or returns
        /// the <paramref name="defaultValue"/> whe there is no value.
        /// </summary>
        /// <param name="dict">The dictionary to search.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>
        /// the value when the <paramref name="key"/> exists or the <paramref name="defaultValue"/>
        /// </returns>
        public static int GetInt32(Dictionary<string, string> dict, string key, int defaultValue)
        {
            int output = defaultValue;

            if (IsNullOrEmpty(dict, key) == false)
            {
                var tempStr = dict[key];

                output = Int32.Parse(tempStr);
            }

            return output;
        }

        /// <summary>
        /// Gets the dictionary value that exists of the <paramref name="key" /> or returns
        /// the <paramref name="defaultValue" /> whe there is no value.
        /// </summary>
        /// <param name="dict">The dictionary to search.</param>
        /// <param name="key">The key.</param>
        /// <param name="enumType">Type of the enum.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>
        /// the value when the <paramref name="key" /> exists or the <paramref name="defaultValue" />
        /// </returns>
        public static object GetEnum(Dictionary<string, string> dict, string key, Type enumType, object defaultValue)
        {
            object output = defaultValue;

            if (IsNullOrEmpty(dict, key) == false)
            {
                var tempStr = dict[key];

                output = Enum.Parse(enumType, tempStr, true);
            }

            return output;
        }

        /// <summary>
        /// Determines whether the element is <c>null</c> or empty in the specified dictionary.
        /// </summary>
        /// <param name="dict">The dictionary.</param>
        /// <param name="key">The key.</param>
        /// <returns>
        ///   <c>true</c> if the element is <c>null</c> or empty in the specified dictionary; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmpty(Dictionary<string, string> dict, string key)
        {
            return !dict.ContainsKey(key) ||
                   String.IsNullOrEmpty(dict[key]);
        }
    }
}
