namespace WildlifeTrackerSystem.src.Fish
{
    public class Shark : Fish
    {
        #region Fields
        private SharkSpecie specie;
        private float swimmingSpeed;    // The shark swimming capabilities in km/h
        private int length;             // the shark length in meter
        private float weight;           // the shark weight in kg

        private FoodSchedule foodSchedule = new FoodSchedule();
        #endregion


        public Shark() : base()
        {
            FishType = FishType.Shark;
            SetFoodSchedule();
        }


        /// <summary>
        ///    Copy constructor
        /// </summary>
        /// <param name="shark"> The shark instance (object) to copy </param>
        public Shark(Shark shark)
        {
            // Set common properties (Animal attributes)
            Category = CategoryType.Fish;
            Name = shark.Name;
            Age = shark.Age;
            Gender = shark.Gender;
            ImagePath = shark.ImagePath;
            Id = shark.Id;

            // Set Fish properties
            Habitat = shark.Habitat;
            WaterTemperature = shark.WaterTemperature;
            FishType = FishType.Shark;

            // Set Shark properties
            specie = shark.specie;
            swimmingSpeed = shark.swimmingSpeed;
            length = shark.length;
            weight = shark.weight;
        }

        #region Properties
        public SharkSpecie Specie
        {
            get { return specie; }
            set { specie = value; }
        }

        public float SwimmingSpeed
        {
            get { return swimmingSpeed; }
            set { swimmingSpeed = value; }
        }

        public float Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        public int Length
        {
            get { return length; }
            set { length = value; }
        }

        #endregion
        public override string ToString()
        {
            string output = string.Format("Id: {0}, name: {1}, Fishtype: {2}, watertemperature {3}, Swimmingspeed: {4}, shark lenth {5}",
                Id, Name, FishType, WaterTemperature, swimmingSpeed, length);

            return output;
        }


        /// <summary>
        ///   Gets the data of the Shark as a dictionary. (Contains Animal + Fish + Shark data)
        /// </summary>
        /// <returns> A dictionary containing the Shark's data </returns>
        public override Dictionary<string, string> GetAnimalData()
        {
            Dictionary<string, string> keyValuePairs = base.GetAnimalData();

            keyValuePairs.Add("Shark specie", specie.ToString());
            keyValuePairs.Add("Swimming speed", swimmingSpeed.ToString());
            keyValuePairs.Add("Weight", weight.ToString());
            keyValuePairs.Add("Length", length.ToString());

            return keyValuePairs;
        }


        /// <summary>
        ///   Gets the animal information as a string with values.
        /// </summary>
        /// <returns> A string containing the animal's data </returns>
        public override string GetExtraInfo()
        {
            string info = base.GetExtraInfo();

            info += string.Format("{0, -15} {1, 10}\n", "Shark specie:", specie);
            info += string.Format("{0, -15} {1, 10}\n", "Swimming speed:", swimmingSpeed);
            info += string.Format("{0, -15} {1, 10}\n", "Weight:", weight);
            info += string.Format("{0, -15} {1, 10}\n", "Length:", length);
            return info;
        }


        /// <summary>
        ///   Sets the food schedule for the Shark.
        /// </summary>
        private void SetFoodSchedule()
        {
            foodSchedule.EaterType = EaterType.Omnivorous;
            foodSchedule.Food.AddToList("Morning: fish (tuna, salmon, bass, rays) ");
            foodSchedule.Food.AddToList("Lunch: seals, sea lions and mollusks");
            foodSchedule.Food.AddToList("Evening: continuation of fish and seals");
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
        /// <returns> Shark object with all data </returns>
        public override object CopyAnimal()
        {
            return new Shark
            {
                Category = Category,
                Name = Name,
                Age = Age,
                Gender = Gender,
                ImagePath = ImagePath,
                Id = Id,
                Habitat = Habitat,
                WaterTemperature = WaterTemperature,
                FishType = FishType,
                Specie = specie,
                SwimmingSpeed = swimmingSpeed,
                Length = length,
                Weight = weight,
            };
        }
    }
}
