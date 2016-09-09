using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondIINoneMC.Core.Helpers
{
    /// <summary>
    /// Contains common actions performed on strings
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// Compares the specified strings for equality ignoring case.
        /// </summary>
        /// <param name="a">The first string to compare</param>
        /// <param name="b">The second string to compare</param>
        /// <returns><c>true</c> when the strings match; otherwise <c>false</c></returns>
        public static bool Match(string a, string b)
        {
            return String.Compare(a, b, true) == 0;
        }

        /// <summary>
        /// Determines whether the specified string is numeric.
        /// </summary>
        /// <param name="input">The string to check.</param>
        /// <returns>
        ///   <c>true</c> if string contains only numeric values; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidNumeric(string input)
        {
            bool isNumeric = true;
            int max = input.Length;

            for (int i = 0; i < max; i++)
            {
                switch (input[i])
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        // Is valid
                        break;
                    default:
                        isNumeric = false;
                        break;
                }
            }

            return isNumeric;
        }

        /// <summary>
        /// Removes the specified text from the end of the string.
        /// </summary>
        /// <param name="text">The text to be searched.</param>
        /// <param name="endingText">The text that needs to be removed.</param>
        /// <returns>A new string instance if removed; otherwise the same instance.</returns>
        /// <example>
        ///  RemoveFromEnd("HelloWorld", "World")
        ///  return "Hello" 
        /// </example>
        /// <remarks>
        /// When a <c>null</c> is passed for <paramref name="text"/> or <paramref name="endingText"/>
        /// the method simply returns <paramref name="text"/> 
        /// </remarks>
        public static string RemoveFromEnd(string text, string endingText)
        {
            if (text == null || endingText == null)
            {
                return text;
            }

            int matchIndex = 0;

            matchIndex = text.LastIndexOf(endingText, StringComparison.InvariantCultureIgnoreCase);

            if (matchIndex > 0)
            {
                return text.Substring(0, text.Length - (text.Length - matchIndex));
            }

            return text;
        }

        /// <summary>
        /// Tries to find the block of text that begins with and ends with the specified.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="startValue">The start value to search for.</param>
        /// <param name="endValue">The end value after the <paramref name="startValue"/> to search for.</param>
        /// <param name="textBlock">The text block object that indicates where it was found, or <c>null</c>.</param>
        /// <returns>
        /// When found <c>true</c> is returned and <paramref name="textBlock"/> is filled;
        /// otherwise <c>false</c> is returned and <paramref name="textBlock"/> is <c>null</c>.
        /// </returns>
        public static bool TryFindTextBlock(string text, string startValue, string endValue, out TextBlock textBlock)
        {
            textBlock = null;

            if (text != null)
            {
                int startIndex = text.IndexOf(startValue);

                if (startIndex >= 0)
                {
                    int endIndex = text.IndexOf(endValue, startIndex + startValue.Length);

                    if (endIndex >= startIndex)
                    {
                        textBlock = new TextBlock(
                            text,
                            startIndex,
                            startValue,
                            endIndex,
                            endValue,
                            text.Substring(startIndex + startValue.Length, endIndex - startIndex - startValue.Length));

                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Replaces the found text block.
        /// </summary>
        /// <param name="textBlock">The text block.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns></returns>
        public static string ReplaceTextBlock(TextBlock textBlock, string newValue)
        {
            return textBlock.OriginalText.Replace(
                textBlock.StartToken + textBlock.InnerText + textBlock.EndToken, newValue);
        }

        /// <summary>
        /// Represents a block of found text in the original string.
        /// </summary>
        public class TextBlock
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="TextBlock" /> class.
            /// </summary>
            /// <param name="originalText">The original text.</param>
            /// <param name="startTokenStartIndex">The start index of the block text.</param>
            /// <param name="startToken">The start token.</param>
            /// <param name="endTokenStartIndex">The end index of the block text.</param>
            /// <param name="endToken">The end token.</param>
            /// <param name="innerText">The block of text.</param>
            /// <exception cref="ArgumentNullException">thrown when <paramref name="originalText" /> or <paramref name="innerText" /> are <c>null</c>.</exception>
            public TextBlock(string originalText, int startTokenStartIndex, string startToken, int endTokenStartIndex, string endToken, string innerText)
            {
                ArgumentValidator.ThrowOnNull("originalText", originalText);
                ArgumentValidator.ThrowOnNull("innerText", innerText);

                OriginalText = originalText;
                StartTokenStartIndex = startTokenStartIndex;
                StartToken = startToken;
                EndTokenStartIndex = endTokenStartIndex;
                EndToken = endToken;
                InnerText = innerText;
            }

            /// <summary>
            /// Gets or sets the original text the block of text was found in.
            /// </summary>
            /// <value>
            /// The original text.
            /// </value>
            public string OriginalText
            {
                get;
                set;
            }

            /// <summary>
            /// Gets the start token used to find the text block.
            /// </summary>
            /// <value>
            /// The start token used to find the text block.
            /// </value>
            public string StartToken
            {
                get;
                private set;
            }

            /// <summary>
            /// Gets the start tokens start index in the original text.
            /// </summary>
            /// <value>
            /// The start tokens start index in the original text.
            /// This value will be greater than zero.
            /// </value>
            public int StartTokenStartIndex
            {
                get;
                private set;
            }

            /// <summary>
            /// Gets the end token used to find the text block.
            /// </summary>
            /// <value>
            /// The end token used to find the text block.
            /// </value>
            public string EndToken
            {
                get;
                private set;
            }

            /// <summary>
            /// Gets the end tokens start index in the original text.
            /// </summary>
            /// <value>
            /// The end tokens start index in the original text.
            /// This value will be greater than <see cref="StartTokenStartIndex"/> plus the
            /// start tokens length.
            /// </value>
            public int EndTokenStartIndex
            {
                get;
                private set;
            }

            /// <summary>
            /// Gets the text found between the start and end tokens.
            /// </summary>
            /// <value>
            /// The text block of the block text.
            /// </value>
            public string InnerText
            {
                get;
                private set;
            }
        }
    }
}
