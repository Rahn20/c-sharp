using System;
using System.Collections.Generic;




namespace ChangeMoneyForm.src
{
    internal class ChangeMoney
    {
        // the difference between the amount and the price
        private readonly int difference;

        // the change the user gets back, the key is coins/notes and the value is number of times it has been received.
        private readonly Dictionary<string, int> result = new Dictionary<string, int>
        {
            { "femhundralapp", 0 },
            { "tvåhundralapp", 0 },
            { "hundralapp", 0 },
            { "femtiolapp", 0 },
            { "tjugolapp", 0 },
            { "tiokrona", 0 },
            { "femkrona", 0 },
            { "enkrona", 0 }
        };



        /**
         * <summary>
         *      takes the difference between the amount the user pays with and the current price
         * </summary>
         * 
         * <param name="price"> the current price </param>
         * <param name="amount"> the amount user pays with </param>
         * 
         * <exception cref="ArgumentException"> Thrown when the difference between the price and amount is less than 0</exception>
         */
        public ChangeMoney(int price, int amount)
        {
            if (amount - price < 0)
            {
                throw new ArgumentException("FEL! - det betalda beloppet bör vara mer än priset.");
            }

            difference = amount - price;
        }



        /**
         * <summary>
         *       Returns how much and in which coins/notes the user receives his change.
         * </summary>
         * 
         * <returns> A dictionary represents change the user gets back </returns>
         */
        public Dictionary<string, int> GetChangeResult()
        {
            string[] stringValues = { "femhundralapp", "tvåhundralapp", "hundralapp", "femtiolapp", "tjugolapp", "tiokrona", "femkrona", "enkrona" };
            int[] intValues = { 500, 200, 100, 50, 20, 10, 5, 1 };

            GetChange(difference, stringValues, intValues);

            return result;
        }


        /**
         * <summary>
         *      A recursive method for calculating change the user gets back, the result saves in the private dictionary "result".
         * </summary>
         * 
         * <param name="diff"> the difference between the price and the amount</param>
         * <param name="stValues"> an array with all the coins/notes (string)</param>
         * <param name="itValues"> an array of all possible change user can get (integer)</param>
         * <param name="counter"> starts from 0 and increases by 1 each time the method is executed </param>
         * */
        private void GetChange(int diff, string[] stValues, int[] itValues, int counter = 0)
        {
            // base case
            if (diff == 0)
            {
                return;
            }


            // The loop runs as long as the same amount can be used for exchange.
            while (diff >= itValues[counter])
            {
                diff -= itValues[counter];
                result[stValues[counter]] += 1;  // add the result to the dictionary 
            }


            GetChange(diff, stValues, itValues, counter + 1);       // recursion call
        }
    }
}
