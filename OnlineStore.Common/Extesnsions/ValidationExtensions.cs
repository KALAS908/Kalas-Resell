using OnlineStore.Common.Exceptions;
using FluentValidation.Results;
using System;


namespace OnlineStore.Common.Extesnsions
{
        public static class ValidationExtensions
        {
            public static void ThenThrow(this ValidationResult result)
            {
                if (!result.IsValid)
                {
                    throw new ValidationErrorException(result);
                }
            }
        }
   
}
