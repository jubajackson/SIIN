using System;
using System.Data.SqlClient;
using System.Text;

namespace Multiview.Data
{
    internal class ExceptionMessage
    {
        internal static string Format(Exception ex)
        {
            StringBuilder errorMessages = new StringBuilder();

            errorMessages.AppendLine(ex.Message);

            FormatInner(ex.InnerException, errorMessages);

            errorMessages.AppendLine("Stack: " + ex.StackTrace);

            return errorMessages.ToString();
        }

        private static void FormatInner(Exception ex, StringBuilder errorMessages)
        {
            if (ex == null)
                return;

            if (ex is SqlException)
            {
                SqlException se = ex as SqlException;

                if (se != null)
                {
                    for (int i = 0; i < se.Errors.Count; i++)
                    {
                        //Msg 102, Level 15, State 1, Line 66
                        //Incorrect syntax near 'x'.
                        errorMessages.Append("Msg " + se.Errors[i].Number);
                        errorMessages.Append(", Level " + se.Errors[i].Class);
                        errorMessages.Append(", State " + se.Errors[i].State);
                        errorMessages.AppendLine(", line " + se.Errors[i].LineNumber);
                        errorMessages.AppendLine("Message: " + se.Errors[i].Message);
                    }
                }
            }

            FormatInner(ex.InnerException, errorMessages);
        }
    }
}
