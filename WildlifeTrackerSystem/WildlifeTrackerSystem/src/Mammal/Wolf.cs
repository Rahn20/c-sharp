namespace WildlifeTrackerSystem.src.Mammal
{
    public class Wolf : Mammal
    {
        #region Fields
        private WolfSpecie wolfSpecie;
        private string eyeColor = "";       // The eye color of the wolf
        private FoodSchedule foodSchedule = new FoodSchedule();
        #endregion


        public Wolf() : base()
        {
            MammalType = MammalType.Wolf;
            SetFoodSchedule();
        }

        /// <summary>
        ///   Copy constructor
        /// </summary>
        /// <param name="wolf"> The Wolf instance (object) to copy </param>
        public Wolf(Wolf wolf)
        {
            // Set common properties (Animal attributes)
            Category = CategoryType.Mammal;
            Name = wolf.Name;
            Age = wolf.Age;
            Gender = wolf.Gender;
            ImagePath = wolf.ImagePath;
            Id = wolf.Id;

            // Set mammal properties
            MammalType = MammalType.Wolf;
            Color = wolf.Color;
            Lifespan = wolf.Lifespan;
            NumOfTeeth = wolf.NumOfTeeth;
            Weight = wolf.Weight;
            Speed = wolf.Speed;
            Height = wolf.Height;

            // Set wolf properties
            wolfSpecie = wolf.wolfSpecie;
            eyeColor = wolf.eyeColor;
        }


        #region Properties
        public WolfSpecie WolfSpecie
        {
            get { return wolfSpecie; }
            set { wolfSpecie = value; }
        }

        public string EyeColor
        {
            get { return eyeColor; }
            set { eyeColor = value; }
        }
        #endregion

        public override string ToString()
        {
            string output = string.Format("Id: {0}, name: {1}, number of teeth: {2}, height: {3}, weight: {4}, wolf eyecolor. {5}",
                Id, Name, NumOfTeeth, Height, Weight, eyeColor);

            return output;
        }

        /// <summary>
        ///   Gets the data of the wolf as a dictionary. (Contains Animal + mammal + wolf data)
        /// </summary>
        /// <returns> A dictionary containing the wolf's data </returns>
        public override Dictionary<string, string> GetAnimalData()
        {
            Dictionary<string, string> keyValuePairs = base.GetAnimalData();

            keyValuePairs.Add("Wolf specie", wolfSpecie.ToString());
            keyValuePairs.Add("Eye color", eyeColor);

            return keyValuePairs;
        }


        /// <summary>
        ///   Gets the animal information as a string with values.
        /// </summary>
        /// <returns> A string containing the animal's data </returns>
        public override string GetExtraInfo()
        {
            string info = base.GetExtraInfo();

            info += string.Format("{0, -15} {1, 10}\n", "Wolf specie:", wolfSpecie);
            info += string.Format("{0, -15} {1, 10}\n", "Eye color:", eyeColor);
            return info;
        }

        /// <summary>
        ///   Sets the food schedule for the Wolf.
        /// </summary>
        private void SetFoodSchedule()
        {
            foodSchedule.EaterType = EaterType.Carnivore;
            foodSchedule.Food.AddToList("Morning: fish (salmon) and smaller mammals like hares, rabbits, beavers, and raccoons");
            foodSchedule.Food.AddToList("Lunch: birds and rodents");
            foodSchedule.Food.AddToList("Evening: ungulates (deer, bison, elk, and moose)");
        }


        /// <summary>
        ///   Retrieves the foodschedule for the animal.
        /// </summary>
        /// <returns> The food schedule for the animal. </returns>
        public override FoodSchedule GetFoodSchedule()
        {
            return foodSchedule;
        }


        /// <summary>
        ///   Copies the animal object, used to prevent unintended modifications to the original objects.
        /// </summary>
        /// <returns> Wolf object with all data </returns>
        public override object CopyAnimal()
        {
            return new Wolf
            {
                Category = Category,
                Name = Name,
                Age = Age,
                Gender = Gender,
                ImagePath = ImagePath,
                Id = Id,
                MammalType = MammalType,
                Color = Color,
                Lifespan = Lifespan,
                NumOfTeeth = NumOfTeeth,
                Weight = Weight,
                Speed = Speed,
                Height = Height,
                WolfSpecie = wolfSpecie,
                EyeColor = eyeColor,
            };
        }
    }
}
