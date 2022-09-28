using System;


namespace PersonRegistration.src
{
    internal class Validations
    {

        /**
         * <summary>
         *      Validates first name, last name and personnumber. They should contain data otherwise raise ValidatorsExeption.
         * </summary>
         * 
         * <param name="name"> first name </param>
         * <param name="lastname"> last name</param>
         * <param name="personnr"> personnumber</param>
         * 
         * <exception cref="ValidatorsException"> Thrown when first name and last name parameters do not contain data, personnumber 
         *  does not contain 10 letters/numbers and personnumber sixth element does not contain '-' </exception>
         * 
         * <returns> returns true if the data passes the validation </returns>
         */
        public static bool ValidateData(string name, string lastname, string personnr)
        {
            if (name.Length > 0 && lastname.Length > 0 && personnr.Length == 11 && personnr[6] == '-')
            {
                string[] splitId = personnr.Split('-');

                Int32.Parse(splitId[0]);        // throws FormatException if conversion fails
                Int32.Parse(splitId[1]);        // throws FormatException if conversion fails

                return true;
            }
            else
            {
                throw new ValidatorsException("Fälten bör innehålla data och personnummer måste skrivas i formaten yymmdd-1111. Försök igen!");
            }
        }
    }
}
