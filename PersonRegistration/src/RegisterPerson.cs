using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace PersonRegistration.src
{
    internal class RegisterPerson
    {
        // the path to the csv file
        private string path;

        // a list containing all personal information contained in the csv file.
        private List<Person> persons = new List<Person>();


        /**
         * <summary>
         *      sets the path variable to the csv path
         * </summary>
         */
        public RegisterPerson()
        {
            // returns the url to the main directory containing "/bin/Debug"
            var currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            // replaces bin\debug with an empty string and appends the csv file name to the path
            path = currentDirectory.Replace("\\bin\\Debug", "") + "\\persons.csv";
        }


        /**
         * <summary>
         *      Takes the person's data and puts it in the csv file and in the list.
         * </summary>
         * 
         * <param name="name"> first name</param> 
         * <param name="lastname"> lastname</param>
         * <param name="personnr">  personnumber</param>
         */
        public void AddPerson(string name, string lastname, string personnr)
        {
            if (!File.Exists(path))
            {
                // Creates the file if it does not exist. Writes the first line in the life.
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


        /**
         * <return> a list of all people information present in the csv file</return>
         */
        public List<Person> Persons
        {
            get { return persons; }
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

                    persons.Add(new Person
                    {
                        Name = data[0],
                        Lastname = data[1],
                        Personnr = data[2],
                    });
                }
            }
        }


        /**
         * <summary>
         *      21 Algorithm, check of personnumber which is calculated by multiplying each digit in the personnumber by alternating 2 and 1,
         *      the sum of digits for each product is calculated and summed up.The control number must be divisible by 10 for the personnumber to be correct.
         * </summary>
         * 
         * <param name="personnr"> personnumber to be checked </param>
         * 
         * <returns> returns true if the result of the algorithm is divisible by 10</returns>
         */
        public bool CheckPersonnr(string personnr)
        {
            string[] splitId = personnr.Split('-');

            char[] charArrPart1 = splitId[0].ToCharArray();
            char[] charArrPart2 = splitId[1].ToCharArray();

            int[] part1 = charArrPart1.Select(i => int.Parse(i.ToString())).ToArray();
            int[] part2 = charArrPart2.Select(i => int.Parse(i.ToString())).ToArray();

            int resultPart1 = VerifyPersonnr(part1);
            int finalResult = VerifyPersonnr(part2, resultPart1);

            if ((finalResult % 10) == 0) return true;
            return false;
        }



        /**
         * <summary>
         *      A recursive method for computing the 21 algorithm. If the i-variable is odd, then I multiply the personnumber digit by 1
         *      and if it is even, then the perosnnummer digit is multiplied by 2 instead.
         * </summary>
         * 
         * <param name="id"> array containing personnumbers to be checked </param>
         * <param name="res"> an integer that is incremented by the sum of digits each time if-else is executed </param>
         * <param name="i"> an integer that is incremented by 1 each time the method is executed.It is used as indexnumber to find out element value</param>
         * 
         * <returns> returnerar heltalen res som är en summering av produkträkning </returns>
         */
        private int VerifyPersonnr(int[] id, int res = 0, int i = 0)
        {
            if (i == id.Length) return res; // base case

            if (i % 2 == 0)
            {
                int getRes = id[i] * 2;
                res += (getRes / 10) + (getRes % 10);
            } 
            else //if i is an odd number then id[i] * 1 which always gives id[i]
            {
                res += id[i];
            }

            return VerifyPersonnr(id, res, i + 1);  //recursive call
        }


        /**
         * <summary>
         *      Check if the personnumber is in the personlist.
         * </summary>
         * 
         * <param name="personnr"> personnumber to be checked </param>
         * 
         * <returns> True if personnumber exists (the person exists), otherwise false </returns>
         */
        public bool CheckRepeatedPerson(string personnr)
        {
            foreach (Person person in persons)
            {
                if (person.Personnr == personnr) return true;
            }

            return false;
        }



        /**
         * <summary>
         *      Get the person's gender by looking at the ninth digit of the personnumber.
         *      Where the number is odd for men and even for women.
         * </summary>
         * 
         * <param name="personnr"> personnumber </param>
         * 
         * <returns> a string whether the person is female or male </returns>
         */
        public string GetGender(string personnr)
        {
            // char 9 is the third digit of the birth number (in the last 4 digits)
            int getThirdNr = int.Parse(personnr[9].ToString());

            if (getThirdNr % 2 == 0) return "Kvinna";
            else return "Man";
        }

    }
}