using System;


namespace PersonRegistration.src
{
    internal class Validations
    {
        /// <summary>
        ///  Validates first name, last name, and person number. They should contain data, otherwise raise ValidatorsException
        /// </summary>
        /// 
        /// <param name="name"> First name </param>
        /// <param name="lastname"> Last name. </param>
        /// <param name="personnr"> Personnumber </param>
        /// 
        /// <exception cref="ValidatorsException">
        ///  Thrown when first name and last name parameters do not contain data, person number does not contain 10 letters/numbers, or personnumber sixth element does not contain '-'
        /// </exception>
        /// 
        /// <returns></returns>
        public static bool ValidateData(string name, string lastname, string personnr)
        {
            if (name.Length > 0 && lastname.Length > 0 && personnr.Length == 11 && personnr[6] == '-')
            {
                string[] splitId = personnr.Split('-');

                Int32.Parse(splitId[0]);        // Throws FormatException if conversion fails.
                Int32.Parse(splitId[1]);        // Throws FormatException if conversion fails.
                return true;
            }
            else
            {
                throw new ValidatorsException("Fälten bör innehålla data och personnummer måste skrivas i formaten yymmdd-1111. Försök igen!");
            }
        }
    }
}
