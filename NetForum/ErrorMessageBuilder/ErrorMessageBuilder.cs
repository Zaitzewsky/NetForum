using System;
using System.Collections.Generic;
using System.Text;

namespace MessageBuilder
{
    public static class ErrorMessageBuilder
    {
        public static string BuildErrorMessage(string errorMessage, IEnumerable<string> errors)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append(errorMessage);
            stringBuilder.AppendLine();

            foreach (var error in errors)
            {
                stringBuilder.AppendLine();
                stringBuilder.Append(error);
            }

            return stringBuilder.ToString();
        }
    }
}
