using ChangeMoneyConsole.src;
using System;
using System.Collections.Generic;


namespace ChangeMoneyConsole
{
    internal class Program
    {
        // Start the program.
        static void Main(string[] args)
        {
            new Start();
            //Console.ReadKey();
        }
    }


    public class Start
    {
        // save the price user enter in the start method
        private readonly int convertPriceToInt;

        // save paid amount user enter in start method
        private readonly int convertPaidToInt;


        /**
         * <summary>
         *      The user is allowed to enter the price he is going to pay and the paid amount he is paying with,
         *      the program checks the input data and prints out to the user how much and in which value (coins, notes) he receives his change.
         * </summary>
         */
        public Start()
        {
            Console.Write("Ange pris: ");
            string readPrice = Console.ReadLine();

            Console.Write("Betalt: ");
            string readPaid = Console.ReadLine();

            try     // Check the data type
            {
                // Convert string to integer
                convertPriceToInt = int.Parse(readPrice);
                convertPaidToInt = int.Parse(readPaid);

                PrintFinalResult();
                Console.WriteLine("-------------------");
            }
            catch
            {
                Console.WriteLine("Du kan endast skriva in heltal.");
                return;
            }
        }

        /**
         * <summary>
         *      loop the result (coins/notes and change) and print it.
         * </summary>
         */
        private void PrintFinalResult()
        {
            try     // Check the amount paid, if it is less than the price raise the error.
            {
                ChangeMoney changeMoney = new ChangeMoney(convertPriceToInt, convertPaidToInt);
                var result = changeMoney.GetChangeResult();
                Console.Write("\nVäxel tillbaka:\n");

 
                foreach (KeyValuePair<string, int> kv in result)
                {
                    char getLastChar = kv.Key[kv.Key.Length - 1];


                    if (kv.Value > 1 && getLastChar == 'p')
                    {
                        Console.WriteLine(kv.Value.ToString() + " " + kv.Key + "ar");

                    }
                    else if (kv.Value > 1 && getLastChar == 'a')
                    {
                        string singleToPlural = kv.Key.Substring(0, kv.Key.Length - 1) + "or";
                        Console.WriteLine(kv.Value.ToString() + " " + singleToPlural);
                    }
                    else if (kv.Value == 1)
                    {
                        Console.WriteLine(kv.Value.ToString() + " " + kv.Key);
                    }

                }
            }
            catch
            {
                Console.WriteLine("Du betalar för lite.");
                return;
            }
        }
    }
}