using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Malinas_Marian_Proiect
{
    public class StringNotEmpty : ValidationRule
    { 
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureinfo)
        {
            string aString = value.ToString();
            if (aString == "")
                return new ValidationResult(false, "Inregistrarea nu poate fi goala!");
            return new ValidationResult(true, null);
        }
    }
    public class StringMinLengthValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureinfo)
        {
            string aString = value.ToString();
            if (aString.Length < 3)
                return new ValidationResult(false, "Introduceti mai mult de 3 caractere!");
        return new ValidationResult(true, null);
        }
    }
}
