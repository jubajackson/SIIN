using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SecondIINoneMC
{
    public class HTMLEmail
    {
        public HTMLEmail(string fullPathToTemplateFile)
        {
            LoadTemplate(fullPathToTemplateFile);
        }

        public bool LoadTemplate(string fullPathToTemplateFile)
        {
            bool rv = false;

            try
            {
                if (File.Exists(fullPathToTemplateFile))
                {
                    HtmlTemplate = File.ReadAllText(fullPathToTemplateFile);
                }
                else
                {

                }
            }
            catch (Exception)
            {

                throw;
            }


            return rv;
        }

        private string _title = "";
        public string Title
        {
            get { return _title ?? ""; }
            set { _title = value; }
        }

        private string _htmlTemplate = "";
        public string HtmlTemplate
        {
            get { return _htmlTemplate ?? ""; }
            set { _htmlTemplate = value; }
        }

        private string _body = "";
        /// <summary>
        /// The Html body of the message. This will get merged with the Html template.
        /// </summary>
        public string Body
        {
            get { return _body ?? ""; }
            set { _body = value; }
        }

        private string _teaserMessage = "";
        /// <summary>
        /// Appears near the top of the e-mail. Enables clients like Microsoft Outlook 
        /// to preview a short summary of the message.
        /// </summary>
        public string TeaserMessage
        {
            get { return _teaserMessage ?? ""; }
            set { _teaserMessage = value; }
        }


        // Returns the HTML with the body parsed to include the current
        // class properties or a blank string.
        public string GetHtml()
        {
            string rv = HtmlTemplate;

            rv = rv.Replace("@ViewBag.Title", Title);
            rv = rv.Replace("@ViewBag.TeaserMessage", TeaserMessage);
            rv = rv.Replace("@RenderBody()", Body);

            return rv;
        }

    }
}
