using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;


namespace RegisterSellerConsole.src
{
    public class RegisterSeller
    {
        // the path to the csv file
        private string path;

        // a list containing all seller information contained in the csv file.
        private List<Seller> sellers = new List<Seller>();


        /**
         * <summary>
         *      Sets the path variable to the path of the csv file.
         * </summary>
         */
        public RegisterSeller(string filename)
        {
            // returns the url to the main directory containing "/bin/Debug"
            //var currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string currentDirectory = Directory.GetCurrentDirectory();

            // replace bin\debug with empty string and add the csv filename to the path.
            path = currentDirectory.Replace("\\bin\\Debug", "") + $"\\{filename}";
        }


        /**
         * <summary>
         *      Takes the seller's information and puts them in the csv file and in the list.
         * </summary>
         * 
         * <param name="name"> seller's name</param> 
         * <param name="personNr"> seller's person id </param>
         * <param name="district">  seller's district </param>
         * <param name="soldItems"> seller's number of sold items</param>
         */
        public void AddSeller(string name, string personNr, string district, int soldItems)
        {
            if (!File.Exists(path))
            {
                // Create the file if it is not exists. writes the first line of the file containing the headers.
                string createText = "Namn,Personnummer,Distrikt,Antal" + Environment.NewLine;
                File.WriteAllText(path, createText);
            }


            // creates the line to be added to the file
            string appendSeller = $"{name},{personNr},{district},{soldItems}" + Environment.NewLine;
            File.AppendAllText(path, appendSeller);

            sellers.Add(new Seller
            {
                Name = name,
                PersonNr = personNr,
                District = district,
                SoldItems = soldItems
            });
        }


        /**
         * <return> list of all sellers that are in the csv file. </return>
         */
        public List<Seller> Sellers
        {
            get { return sellers; }
        }


        /**
         * <summary>
         *      Reads all lines in the file and puts them in a list if the file exists.
         * </summary>
         */
        public void AddToList()
        {
            if (File.Exists(path))
            {
                string[] lines = File.ReadAllLines(path);
                string[] data;


                for (int i = 1; i < lines.Length; i++)
                {
                    data = lines[i].Split(',');

                    sellers.Add(new Seller { 
                        Name = data[0],
                        PersonNr = data[1],
                        District = data[2],
                        SoldItems = Int16.Parse(data[3])
                    });
                }
            }
        }


        /**
         * <summary>
         *    Check if the personnumber is in the seller's list.
         * </summary>
         * 
         * <returns> True if personnumber exists (seller exists), otherwise false</returns>
         */
        public bool CheckPersonNr(string personNr)
        {
            foreach (Seller seller in sellers)
            {
                if (seller.PersonNr == personNr) return true;
            }

            return false;
        }


        /**
         * <summary>
         *      A recursive method of Bubble Sort, sorts the list by the number of sold items.
         * </summary>
         * 
         * <param name="list"> list of all the seller's data </param>
         * <param name="length"> the length of the list </param>
         */
        public void SortBySolditems(List<Seller> list, int length)
        {
            if (length == 0 || length == 1)
            {
                return;
            }

            for (int i = 0; i < length - 1; i++)
            {
                if (list[i].SoldItems > list[i + 1].SoldItems)
                {
                    // swap the elements
                    (list[i + 1], list[i]) = (list[i], list[i + 1]);
                }
            }

            SortBySolditems(list, length - 1); // recursion call
        }

        /**
        * <summary>
        *      Looking at the number of sold items that the sellers have to determine
        *      what level each seller has reached. There are 4 levels on articles.
        *      Level 1 below 50 items
        *      Level 2 50-99 items.
        *      Level 3 100-199 items.
        *      Level 4 over 199 items.
        * </summary>
        * 
        * <returns> a dictionary with the number of sellers who have reached a certain level</returns>
        */
        public Dictionary<string, int> GetItemLevels()
        {
            Dictionary<string, int> levels = new Dictionary<string, int>
            {
                { "under 50 artiklar", 0 },
                { "50-99 artiklar", 0 },
                { "100-199 artiklar", 0 },
                { "över 199 artiklar", 0 }
            };


            foreach (Seller seller in sellers)
            {
                // increases the number of sellers by 1 every time the value of the item matches the level requirement.
                if (seller.SoldItems < 50) levels["under 50 artiklar"] += 1;                                            // level 1
                else if (seller.SoldItems >= 50 && seller.SoldItems <= 99) levels["50-99 artiklar"] += 1;               // level 2
                else if (seller.SoldItems >= 100 && seller.SoldItems <= 199) levels["100-199 artiklar"] += 1;           // level 3
                else levels["över 199 artiklar"] += 1;                                                                  // level 4
            }

            return levels;
        }
    }
}
