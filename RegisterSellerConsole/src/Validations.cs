using System;

namespace RegisterSellerConsole.src
{
    public class Validations
    {
        /// <summary>
        ///  Validates name, person number, district, and number of sold items. They should contain data; otherwise, a ValidatorException will be raised
        /// </summary>
        /// 
        /// <param name="id"> Seller’s personnumber. </param>
        /// <param name="name"> Seller's name </param>
        /// <param name="items"> Number of items the seller has sold during the period </param>
        /// <param name="district"> District where the seller works </param>
        /// 
        /// <exception cref="ValidatorsException"> Thrown when one of the name, district, or items parameters does not contain data </exception>
        /// 
        /// <returns> True if the data passed the validation </returns>
        public static bool ValidateData(string name, string id, string district, string items)
        {
            if (name.Length > 0 && district.Length > 0 && items.Length > 0)
            {
                return ValidatePersonNr(id);
            }
            else
            {
                throw new ValidatorsException("\nDet bör inte finnas tomma fält. Fyll i namn, personnummer, distrikt och antal sålda artiklar.\nFörsök igen!");
            }
        }


        /// <summary>
        ///  Checks that the personnumber contains 10 digits.
        /// </summary>
        /// 
        /// <param name="personNr"> Seller’s personnumber </param>
        /// <exception cref="ValidatorsException"> Thrown when the length of the person number is not 10 </exception>
        /// <returns> True if the personnumber passes the validation </returns>
        public static bool ValidatePersonNr(string personNr)
        {
            if (personNr.Length == 10)
            {
                Int64.Parse(personNr);     // Raise FormatException if the conversion fails.
                return true;
            }
            else throw new ValidatorsException("\nPersonnummer bör bestå av 10 siffror, Försök igen!");
        }
    }
}