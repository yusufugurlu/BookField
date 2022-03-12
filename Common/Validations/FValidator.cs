using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Validations
{
    public static class FValidator
    {
        public static string Errors(List<ValidationFailure> errors)
        {
            string strErrors = "";
            foreach (var error in errors)
            {
                strErrors += error.ErrorMessage+"\n";
            }
            return strErrors;
        }
    }
}
