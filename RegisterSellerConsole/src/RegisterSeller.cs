using System;
using System.Collections.Generic;
using System.IO;

namespace RegisterSellerConsole.src
{
    public class RegisterSeller
    {
        private string path;                                // Path to the CSV file.
        private List<Seller> sellers = new List<Seller>();  // List containing all seller information from the CSV file.


        public RegisterSeller(string filename)
        {
            // Return the URL to the main directory containing "/bin/Debug."
            //var currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string currentDirectory = Directory.GetCurrentDirectory();

            path = currentDirectory.Replace("\\bin\\Debug", "") + $"\\{filename}";
        }


        /// <summary>
        ///  Takes the seller's information and adds them to the CSV file and the list.
        /// </summary>
        /// 
        /// <param name="name">Seller's name.</param>
        /// <param name="personNr">Seller's person ID (personnummer).</param>
        /// <param name = "district" > Seller's district.</param>
        /// <param name = "soldItems" > Seller's number of sold items.</param>
        public void AddSeller(string name, string personNr, string district, int soldItems)
        {
            if (!File.Exists(path))
            {
                // Creates the file if it does not exist and write the first line containing the headers.
                string createText = "Namn,Personnummer,Distrikt,Antal" + Environment.NewLine;
                File.WriteAllText(path, createText);
            }

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


        /// <returns> List of all sellers present in the CSV file.</returns>
        public List<Seller> Sellers
        {
            get { return sellers; }
        }


        /// <summary>
        ///  Reads all lines in the file and add them to a list if the file exists.
        /// </summary>
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


        /// <summary>
        ///  Checks if the personnumber is in the seller's list.
        /// </summary>
        /// 
        /// <returns> True if the person number exists (seller exists), otherwise false </returns>
        public bool CheckPersonNr(string personNr)
        {
            foreach (Seller seller in sellers)
            {
                if (seller.PersonNr == personNr) return true;
            }

            return false;
        }


        /// <summary>
        ///  Recursive method using Bubble Sort to sort the list by the number of sold items.
        /// </summary>
        /// 
        /// <param name="list">List of all seller's data.</param>
        /// <param name="length">The length of the list.</param>
        public void SortBySolditems(List<Seller> list, int length)
        {
            if (length == 0 || length == 1) { return; }

            for (int i = 0; i < length - 1; i++)
            {
                if (list[i].SoldItems > list[i + 1].SoldItems)
                {
                    (list[i + 1], list[i]) = (list[i], list[i + 1]);    // Swap the elements
                }
            }

            SortBySolditems(list, length - 1);
        }


        /// <summary>
        ///  Determines the level each seller has reached based on the number of sold items. There are 4 levels:
        ///  - Level 1: Below 50 items
        ///  - Level 2: 50-99 items
        ///  - Level 3: 100-199 items
        ///  - Level 4: Over 199 items
        /// </summary>
        /// 
        /// <returns> A dictionary with the number of sellers who have reached each level </returns>
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
                // Increment the number of sellers by 1 every time the value of the item matches the level requirement.
                if (seller.SoldItems < 50) levels["under 50 artiklar"] += 1;                                            // level 1
                else if (seller.SoldItems >= 50 && seller.SoldItems <= 99) levels["50-99 artiklar"] += 1;               // level 2
                else if (seller.SoldItems >= 100 && seller.SoldItems <= 199) levels["100-199 artiklar"] += 1;           // level 3
                else levels["över 199 artiklar"] += 1;                                                                  // level 4
            }

            return levels;
        }
    }
}
