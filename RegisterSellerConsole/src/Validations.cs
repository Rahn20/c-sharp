using System;


namespace RegisterSellerConsole.src
{
    public class Validations
    {

        /**
         * <summary>
         *      Validates name, personnumber, district and number of sold items. they should contain data, otherwise ValidatorException will be raised.
         * </summary>
         * 
         * <param name="name"> Seller's name</param>
         * <param name="id"> Sellers’s personnumber </param>
         * <param name="district"> in which district the seller works </param>
         * <param name="items"> how many items the seller has sold during the period </param>
         * 
         * <exception cref="ValidatorsException"> Thrown when one of the name, district or article parameters does not contain data </exception>
         * 
         * <returns> returns true if the data passed the validation </returns>
         */
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


        /**
         * <summary>
         *     Check that personnumber contains 10 digits.
         * </summary>
         * 
         * <param name="personNr"> Seller’s personnumber </param>
         * <exception cref="ValidatorsException"> Thrown when the length of the personnumber is not 10 </exception>
         * 
         * <returns> returns true if personnumber passes the validation </returns>
         */
        public static bool ValidatePersonNr(string personNr)
        {
            if (personNr.Length == 10)
            {
                Int64.Parse(personNr);     // raises FormatException if the conversion failed

                return true;
            }
            else throw new ValidatorsException("\nPersonnummer bör bestå av 10 siffror, Försök igen!");
        }
    }
}

