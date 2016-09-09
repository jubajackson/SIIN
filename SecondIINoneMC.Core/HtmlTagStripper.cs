using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using SecondIINoneMC.Core.Helpers;

namespace SecondIINoneMC.Core
{
    /// <summary>
    /// Contains functions to help safely remove html tags from strings.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Thanks to Martijn for the insperation and sample:
    /// http://www.dijksterhuis.org/safely-cleaning-html-with-strip_tags-in-csharp/
    /// http://derekslager.com/blog/posts/2007/09/a-better-dotnet-regular-expression-tester.ashx
    /// </para>
    /// </remarks>
    public class HtmlTagStripper
    {
        private readonly static Dictionary<string, string[]> NoneAllowed = new Dictionary<string, string[]>();

        /// <summary>
        /// The Regex pattern used to find HTML tags.
        /// </summary>
        private const string HtmlTagPattern = @"(<\/?[^>]+>)";
        private readonly static Regex __htmlTagRegex = new Regex(HtmlTagPattern, RegexOptions.Compiled);

        /// <summary>
        /// A 4 group pattern for a single attribute of a tag element.
        /// </summary>
        private const string HtmlAttribPattern = @"[-_0-9a-zA-Z]+=(('[^']*')|(""[^""]*"")|([^ ]+(?=[ >])))";
        private readonly static Regex __htmlAttribRegex = new Regex(HtmlAttribPattern, RegexOptions.Compiled);

        /// <summary>
        /// The comment HTML pattern
        /// </summary>
        private const string HtmlCommentPattern = @"(<![-][-])+(.*)+([-][-]>)";
        private readonly static Regex __htmlCommentRegex = new Regex(HtmlCommentPattern, RegexOptions.Compiled);

        /// <summary>
        /// Strips all tags from the <paramref name="input"/>.
        /// </summary>
        /// <param name="input">The input to check.</param>
        /// <returns>the cleaned string</returns>
        /// <remarks>
        /// when the passed <paramref name="input"/> is <c>null</c> or empty, the passed value is returned.
        /// </remarks>
        public static string StripTags(string input)
        {
            return StripTags(input, NoneAllowed);
        }

        /// <summary>
        /// Strips the tags from the <paramref name="input"/> but leaves any specified tags.
        /// </summary>
        /// <param name="input">The input to check.</param>
        /// <param name="allowedTags">The tags allowed to remain.</param>
        /// <returns>the cleaned string</returns>
        /// <remarks>
        /// when the passed <paramref name="input"/> is <c>null</c> or empty, the passed value is returned.
        /// </remarks>
        public static string StripTags(string input, string[] allowedTags)
        {
            if (String.IsNullOrWhiteSpace(input))
            {
                return input;
            }

            Dictionary<string, string[]> tags = new Dictionary<string, string[]>();
            foreach (var tag in allowedTags)
            {
                tags.Add(tag, null);
            }
            return StripTags(input, tags);
        }

        /// <summary>
        /// Strips the tags from the <paramref name="input"/> but leaves any specified tags and attributes.
        /// </summary>
        /// <param name="input">The input to check.</param>
        /// <param name="allowedTags">The tags allowed plus thier allowed attributes to remain.</param>
        /// <returns>the cleaned string</returns>
        /// <exception cref="ArgumentNullException">
        /// when the passed <paramref name="allowedTags"/> is <c>null</c>.
        /// </exception>
        /// <remarks>
        /// when the passed <paramref name="input"/> is <c>null</c> or empty, the passed value is returned.
        /// </remarks>
        public static string StripTags(string input, Dictionary<string, string[]> allowedTags)
        {
            if (String.IsNullOrWhiteSpace(input))
            {
                return input;
            }

            ArgumentValidator.ThrowOnNull(input, "allowedTags");

            string output = input;
            bool updateTag, hadHtmlElememts;
            int offset;
            string htmlTag, cleanTag, allowedTag;

            hadHtmlElememts = false;

            output = RemoveHtmlComments(output);

            foreach (Match tag in __htmlTagRegex.Matches(output))
            {
                htmlTag = tag.Value;
                updateTag = true;
                cleanTag = String.Empty;
                hadHtmlElememts = true;

                foreach (var allowedTagKvp in allowedTags)
                {
                    allowedTag = allowedTagKvp.Key;
                    offset = -1;

                    if (offset != 0)
                        offset = htmlTag.IndexOf("<" + allowedTag + ">", StringComparison.InvariantCultureIgnoreCase);
                    if (offset != 0)
                        offset = htmlTag.IndexOf("<" + allowedTag + " ", StringComparison.InvariantCultureIgnoreCase);
                    if (offset != 0)
                        offset = htmlTag.IndexOf("<" + allowedTag + "/>", StringComparison.InvariantCultureIgnoreCase);
                    if (offset != 0)
                        offset = htmlTag.IndexOf("</" + allowedTag, StringComparison.InvariantCultureIgnoreCase);

                    // Does it matched any of the above the tag is allowed
                    if (offset == 0)
                    {
                        updateTag = true;

                        cleanTag = StripTagAttributes(htmlTag, allowedTag, allowedTagKvp.Value);

                        break;
                    }
                }

                if (updateTag)
                {
                    output = ReplaceFirst(output, htmlTag, cleanTag);
                }
            }

            if (!hadHtmlElememts)
            {
                output = System.Web.HttpUtility.HtmlEncode(output);
            }

            return output;
        }

        private static string RemoveHtmlComments(string input)
        {
            string output = input;

            int offsetS, offsetE;

            // Max attempts
            for (int i = 0; i < 100; i++)
            {
                offsetS = output.IndexOf("<!--");

                if (offsetS >= 0)
                {
                    offsetE = output.IndexOf("-->", offsetS);

                    if (offsetE < 0)
                    {
                        // Remove every thing after
                        output = output.Substring(0, offsetS);
                        break;
                    }
                    else
                    {
                        string htmlComment = output.Substring(offsetS, offsetE - offsetS + 3);

                        // Remove just this one
                        output = output.Replace(htmlComment, String.Empty);
                    }
                }
                else
                {
                    break;
                }
            }

            return output;
        }

        private static string StripTagAttributes(string htmlTag, string tag, string[] allowedAttribs)
        {
            if (htmlTag.StartsWith("</"))
            {
                return "</" + tag + ">";
            }

            bool isSelfClosing = htmlTag.EndsWith("/>");

            string output = htmlTag.Substring(0, htmlTag.IndexOf(tag, 0, StringComparison.InvariantCultureIgnoreCase) + tag.Length);

            if (allowedAttribs != null && allowedAttribs.Length > 0)
            {
                string attrib, attribName;

                foreach (Match uTag in __htmlAttribRegex.Matches(htmlTag))
                {
                    attrib = uTag.Value;

                    attribName = attrib.Substring(0, attrib.IndexOf("="));

                    for (int i = 0; i < allowedAttribs.Length; i++)
                    {
                        if (String.Compare(allowedAttribs[i], attribName, true) == 0)
                        {
                            output += " " + attrib;
                        }
                    }
                }
            }

            if (isSelfClosing)
            {
                output += "/>";
            }
            else
            {
                output += ">";
            }

            return output;
        }

        private static string ReplaceFirst(string haystack, string needle, string replacement)
        {
            int pos = haystack.IndexOf(needle, StringComparison.InvariantCultureIgnoreCase);
            if (pos < 0)
                return haystack;
            return haystack.Substring(0, pos) + replacement + haystack.Substring(pos + needle.Length);
        }
    }
}
