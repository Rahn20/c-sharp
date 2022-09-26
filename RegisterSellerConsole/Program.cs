using RegisterSellerConsole.src;
using System;
using System.Collections.Generic;


namespace RegisterSellerConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            new Start();

            //Console.ReadKey();
        }
    }


    internal class Start
    {
        private string name;   
        private string personNr; 
        private string district;
        private string soldItems;

        RegisterSeller register = new RegisterSeller();

        /**
         * <summary>
         *      the user enters the seller's information.
         *      Prints the result of the sellers who are registered plus how many sellers have reached a certain level.
         * </summary>
         */
        public Start()
        {
            Console.WriteLine("\nVälkommen till säljare register mini program. Skriv in namn, personnummer (endast 10 sifror), distrikt och antal sålda artiklar.\n");
            Console.Write("Hur många säljare vill du registrera: ");

            string readNrOfSellers = Console.ReadLine();
            int nrSeller = Int16.Parse(readNrOfSellers);

            register.AddToList();       // reads the file's data and adds it to a list

            while (nrSeller > 0)
            {
                Console.Write("Namn: ");
                name = Console.ReadLine();

                Console.Write("Personnummer: ");
                personNr = Console.ReadLine();

                Console.Write("Distrikt: ");
                district = Console.ReadLine();

                Console.Write("Antal sålda artiklar: ");
                soldItems = Console.ReadLine();
                
                // validate and register seller
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(Register());
                Console.ResetColor();
                Console.WriteLine("________________________________________________________________________");


                nrSeller--;
            }

            // sort the result 
            register.SortBySolditems(register.Sellers, (register.Sellers).Count);

            // prints the result
            PrintResult();

            // shows item level results
            ItemLevels();

        }


        /**
         * <summary>
         *      Prints a table of all registered sellers.
         * </summary>
         */
        private void PrintResult()
        {
            Console.WriteLine("\n--------------------------------------------------------------------------");
            Console.WriteLine(String.Format("{0,-3}|{1,-24}|{2,-14}|{3,-19}|{4,-9}|", "#", "Namn", "Personnummer", "Distrikt", "Antal"));
            Console.WriteLine("--------------------------------------------------------------------------");

            int nr = 1;

            foreach (Seller seller in register.Sellers)
            {
                string data = String.Format("{0,-3}|{1,-24}|{2,-14}|{3,-19}|{4,-9}|", nr, seller.Name , seller.PersonNr, seller.District, seller.SoldItems);
                Console.WriteLine(data);

                nr++;
            }

            Console.WriteLine("--------------------------------------------------------------------------\n");
        }


        /**
         * <summary>
         *     Validates the seller's data and checks if the seller is already registered,
         *     if the data passes the validation and the seller is not registered then the seller will be registered/created.
         * </summary>
         * 
         * <returns> a string with information if the seller has been registered or if something went wrong with validation</returns>
         */
        private string Register()
        {
            try // check the validation
            {
                bool validateOk = Validations.ValidateData(name, personNr, district, soldItems); //raises exception if something goes wrong with the validation
                bool searchPersonnr = register.CheckPersonNr(personNr);      // false if the seller is not registered

                if (validateOk && searchPersonnr is false)
                {
                    register.AddSeller(name, personNr, district, Int16.Parse(soldItems));
                    return $"\nSäljaren {name} har skapats.";
                }
                else return "\nSäljaren finns redan registrerad.";
            }
            catch (FormatException)
            {
                return "\nOgiltig personnummer, Se till att personnummer innehåller 10 siffror och försök igen.";
            }
            catch (ValidatorsException ve)
            {
                return ve.Message;
            }
        }


        /**
         * <summary>
         *      Prints number of sellers who have reached a certain level on items, showing only levels that registered sellers have reached.
         * </summary>
         */
        private void ItemLevels()
        {
            var items = register.GetItemLevels();
            int i = 1;

            Console.WriteLine("\nNivå:");

            foreach (KeyValuePair<string, int> kv in items)
            {
                // only shows levels that sellers have reached.
                if (kv.Value > 0)
                {
                    Console.WriteLine($"{kv.Value} säljare har nått nivå {i}: {kv.Key}");
                }

                i++;
            }

            Console.WriteLine("________________________________________________________________________");
        }
    }
}
