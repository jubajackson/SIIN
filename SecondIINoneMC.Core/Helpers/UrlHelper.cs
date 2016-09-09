using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondIINoneMC.Core.Helpers
{
    /// <summary>
    /// Contains methods to help in validating a scrubbing Urls
    /// </summary>
    public class UrlHelper
    {
        /// <summary>
        /// Determines whether the specified value is a valid <see cref="Uri"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValid(string value)
        {
            Uri uri;

            if (Uri.TryCreate(value, UriKind.RelativeOrAbsolute, out uri))
            {
                return true;
            }

            return false;
        }

        // Note: the default pages included
        //       doesn't contain default.html and default.aspx
        //       because default.htm and default.asp is enough 
        //       to tackle default.html and default.aspx
        static string[] _defaultPages = new string[] { "default.htm", "index.htm", "default.asp", "index.asp" };

        /// <summary>
        /// Scrubs a website Url and making into a valid http address.
        /// </summary>
        /// <param name="webSite">The web url.</param>
        /// <returns>A valid http address</returns>
        public static string ScrubWebSiteName(string webSite)
        {
            if (string.IsNullOrEmpty(webSite))
            {
                return null;
            }

            const string HTTP = "http://";

            string cleanedWebSite = webSite.ToLower();

            Uri uri;
            if (Uri.TryCreate(cleanedWebSite, UriKind.RelativeOrAbsolute, out uri))
            {
                if (!uri.IsAbsoluteUri)
                {
                    cleanedWebSite = HTTP + cleanedWebSite;
                }
            }

            foreach (string defaultpage in _defaultPages)
            {
                cleanedWebSite = StringHelper.RemoveFromEnd(cleanedWebSite, defaultpage);
            }

            if (cleanedWebSite.EndsWith("/"))
            {
                cleanedWebSite = cleanedWebSite.Substring(0, cleanedWebSite.Length - 1);
            }

            return cleanedWebSite;
        }
    }
}
