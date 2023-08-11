using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace OnlineStore.Common.Exceptions
{
    public class ValidationErrorException : Exception
    {
        public readonly ValidationResult ValidationResult;

        public ValidationErrorException(ValidationResult result)
        {
            ValidationResult = result;
        }
    }
}
