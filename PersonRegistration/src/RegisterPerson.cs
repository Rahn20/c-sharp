using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace PersonRegistration.src
{
    internal class RegisterPerson
    {
        private readonly string path;                       // Specify the path to the CSV file.
        private List<Person> persons = new List<Person>();  // List containing all personal information from the CSV file.


        public RegisterPerson()
        {
            // Return the URL to the main directory containing "/bin/Debug."
            var currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            // Replace "bin\Debug" with an empty string and append the CSV file name to the path.
            path = currentDirectory.Replace("\\bin\\Debug", "") + "\\persons.csv";
        }


        /// <summary>
        ///  Takes the person's data and adds it to the CSV file and the list
        /// </summary>
        /// 
        /// <param name="name"> First name </param>
        /// <param name="lastname"> Last name </param>
        /// <param name="personnr"> Personnumber </param>
        public void AddPerson(string name, string lastname, string personnr)
        {
            if (!File.Exists(path))
            {
                // Create the file if it does not exist and write the first line.
                string createText = "Förnamn,Efternamn,Personnummer" + Environment.NewLine;
                File.WriteAllText(path, createText);
            }

            string appendSeller = $"{name},{lastname},{personnr}" + Environment.NewLine;
            File.AppendAllText(path, appendSeller);

            persons.Add(new Person
            {
                Name = name,
                Lastname= lastname,
                Personnr = personnr,
            });
        }


        /// <returns> A list of all people's information present in the CSV file </returns>
        public List<Person> Persons
        {
            get { return persons; }
        }


        /// <summary>
        ///  Reads all lines in the file and add them to a list if the file exists
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

                    persons.Add(new Person
                    {
                        Name = data[0],
                        Lastname = data[1],
                        Personnr = data[2],
                    });
                }
            }
        }


        /// <summary>
        ///  21 Algorithm, checks a personsnumber by multiplying each digit by alternating 2 and 1, calculates the sum of digits for each product, and sums up the results
        ///  The control number must be divisible by 10 for the personsnumber to be correct.
        /// </summary>
        /// 
        /// <param name="personnr"> personnumber to be checked  </param>
        /// 
        /// <returns> True if the result of the algorithm is divisible by 10 </returns>
        public bool CheckPersonnr(string personnr)
        {
            string[] splitId = personnr.Split('-');

            char[] charArrPart1 = splitId[0].ToCharArray();
            char[] charArrPart2 = splitId[1].ToCharArray();

            int[] part1 = charArrPart1.Select(i => int.Parse(i.ToString())).ToArray();
            int[] part2 = charArrPart2.Select(i => int.Parse(i.ToString())).ToArray();

            int resultPart1 = VerifyPersonnr(part1);
            int finalResult = VerifyPersonnr(part2, resultPart1);

            return (finalResult % 10) == 0;
        }



        /// <summary>
        ///  Recursive method for computing the 21 algorithm. If the 'i' variable is odd, the personnumber digit is multiplied by 1;
        ///  if it is even, the personnumber digit is multiplied by 2.
        /// </summary>
        /// 
        /// <param name="id"> Array containing personnumbers to be checked.</param>
        /// <param name="res"> An integer that is incremented by the sum of digits each time the if-else is executed </param>
        /// <param name="i"> An integer that is incremented by 1 each time the method is executed; it is used as an index number to find the element value </param>
        private int VerifyPersonnr(int[] id, int res = 0, int i = 0)
        {
            if (i == id.Length) return res; // base case

            if (i % 2 == 0)
            {
                int getRes = id[i] * 2;
                res += (getRes / 10) + (getRes % 10);
            }
            else
            {
                res += id[i];
            }

            return VerifyPersonnr(id, res, i + 1);
        }


        /// <summary>
        ///  Checks if the personnumber is in the person list.
        /// </summary>
        /// 
        /// <param name="personnr"> Personnumber to be checked</param>
        /// 
        /// <returns> True if the person number exists (the person exists), otherwise false. </returns>
        public bool CheckRepeatedPerson(string personnr)
        {
            foreach (Person person in persons)
            {
                if (person.Personnr == personnr) return true;
            }

            return false;
        }


        /// <summary>
        ///  Gets the person's gender by examining the ninth digit of the person number. The number is odd for men and even for women.
        /// </summary>
        /// 
        /// <param name="personnr"> Personnumber </param>
        /// 
        /// <returns> A string indicating whether the person is female or male </returns>
        public string GetGender(string personnr)
        {
            // Char 9 is the third digit of the birth number (in the last 4 digits).
            int getThirdNr = int.Parse(personnr[9].ToString());

            if (getThirdNr % 2 == 0) return "Kvinna";
            else return "Man";
        }
    }
}