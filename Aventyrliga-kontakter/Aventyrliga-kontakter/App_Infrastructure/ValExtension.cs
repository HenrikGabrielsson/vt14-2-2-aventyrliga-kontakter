using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aventyrliga_kontakter
{
    public static class ValExtension
    {
        public static bool Validate<T>(this T instance, out ICollection<ValidationResult> valResults)
        {
            var valContext = new ValidationContext(instance);
            valResults = new List<ValidationResult>();
            return Validator.TryValidateObject(instance, valContext, valResults, true);
        }
    }
}