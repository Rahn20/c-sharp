namespace ChangeMoney.src
{
    public class ChangeMoneySystem
    {
        // the difference between the amount and the price
        private readonly int difference;

        // The change the user receives, keys represent coins/notes and values indicate the frequency of each denomination
        private readonly Dictionary<string, int> result = new() {
            { "femhundralapp", 0 },
            { "tvåhundralapp", 0 },
            { "hundralapp", 0 },
            { "femtiolapp", 0 },
            { "tjugolapp", 0 },
            { "tiokrona", 0 },
            { "femkrona", 0 },
            { "enkrona", 0 }
        };



        /// <summary>
        ///  Calculates the change by subtracting the current price from the amount paid by the user.
        /// </summary>
        /// 
        /// <param name="amount"> The amount paid by the user. </param>
        /// <param name="price"> The current price </param>
        /// <exception cref="ArgumentException">  Thrown if the difference between the price and amount is less than 0 </exception>
        public ChangeMoneySystem(int price, int amount)
        {
            if (amount - price < 0)
            {
                throw new ArgumentException("FEL! - beloppet bör vara mer än priset.");
            }

            difference = amount - price;
        }


        /// <summary>
        ///  Returns the user's change and the corresponding coins/notes in a dictionary
        /// </summary>
        /// 
        /// <returns> A dictionary representing the user's change </returns>
        public Dictionary<string, int> GetChangeResult()
        {
            string[] stringValues = { "femhundralapp", "tvåhundralapp", "hundralapp", "femtiolapp", "tjugolapp", "tiokrona", "femkrona", "enkrona" };
            int[] intValues = { 500, 200, 100, 50, 20, 10, 5, 1 };

            GetChange(difference, stringValues, intValues);

            return result;
        }


        /// <summary>
        ///  Recursive method to calculate the user's change and store it in the private dictionary "result."
        /// </summary>
        /// 
        /// <param name="counter"> Counter starting from 0, incremented with each method execution </param>
        /// <param name="diff"> Difference between the price and the amount. </param>
        /// <param name="intValues"> Array of coins/notes (strings) </param>
        /// <param name="stringValues"> Array of possible change values (integers) </param>
        private void GetChange(int diff, string[] stringValues, int[] intValues, int counter = 0)
        {
            // base case
            if (diff == 0) { return; }


            // Loop continues while the same amount can be used for exchange.
            while (diff >= intValues[counter])
            {
                diff -= intValues[counter];
                result[stringValues[counter]] += 1;
            }

            GetChange(diff, stringValues, intValues, counter + 1);
        }
    }
}