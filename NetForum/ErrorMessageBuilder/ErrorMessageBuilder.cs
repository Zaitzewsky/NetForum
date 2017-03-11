using System.Collections.Generic;
using System.Text;
using System.Data.Entity.Validation;

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

        public static string BuildErrorMessage(string errorMessage, IEnumerable<DbEntityValidationResult> errors)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append(errorMessage);

            foreach (var error in errors)
            {
                foreach (var validationError in error.ValidationErrors)
                {
                    stringBuilder.Append(validationError.ErrorMessage);
                    stringBuilder.AppendLine();
                }
            }

            return stringBuilder.ToString();
        }
    }
}
