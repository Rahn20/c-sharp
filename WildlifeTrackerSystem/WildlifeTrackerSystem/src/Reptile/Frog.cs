namespace WildlifeTrackerSystem.src.Reptile
{
    /// <summary>
    ///   A concrete class inheriting from Reptile
    /// </summary>
    public class Frog : Reptile
    {
        private string diet;       // The diet of the frog, example: Most frogs eat insects and spiders.
        private FoodSchedule foodSchedule = new FoodSchedule();


        public Frog() : base()
        {
            ReptileType = ReptileType.Frog;
            diet = string.Empty;
            SetFoodSchedule();
        }

        /// <summary>
        ///   Copy constructor
        /// </summary>
        /// <param name="frog"> The frog instance (object) to copy </param>
        public Frog(Frog frog)
        {
            // Set common properties (Animal attributes)
            Category = CategoryType.Reptile;
            Name = frog.Name;
            Age = frog.Age;
            Gender = frog.Gender;
            ImagePath = frog.ImagePath;
            Id = frog.Id;

            // Set Reptile properties
            ReptileType = ReptileType.Frog;
            Habitat = frog.Habitat;
            NumOfLegs = frog.NumOfLegs;

            // Set frog properties
            diet = frog.diet;
        }

        public string Diet
        {
            get { return diet; }
            set { diet = value; }
        }

        public override string ToString()
        {
            string output = string.Format("Id: {0}, name: {1}, Number of legs: {2}, frog diet: {3}",
                Id, Name, NumOfLegs, diet);

            return output;
        }


        /// <summary>
        ///   Gets the data of the frog as a dictionary. (Contains Animal + reptile + frog data)
        /// </summary>
        /// <returns> A dictionary containing the frog's data </returns>
        public override Dictionary<string, string> GetAnimalData()
        {
            Dictionary<string, string> keyValuePairs = base.GetAnimalData();
            keyValuePairs.Add("Frog diet", diet);

            return keyValuePairs;
        }

        /// <summary>
        ///   Sets the food schedule for the Frog.
        /// </summary>
        private void SetFoodSchedule()
        {
            foodSchedule.EaterType = EaterType.Carnivore;
            foodSchedule.Food.AddToList("Morning: insects (snails, spiders and crickets), mealworms and waxworms");
            foodSchedule.Food.AddToList("Lunch: caterpillars or worms");
            foodSchedule.Food.AddToList("Evening: mice");
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
        /// <returns> Frog object with all data </returns>
        public override object CopyAnimal()
        {
            return new Frog
            {
                Category = Category,
                Name = Name,
                Age = Age,
                Gender = Gender,
                ImagePath = ImagePath,
                Id = Id,
                ReptileType = ReptileType,
                Habitat = Habitat,
                NumOfLegs = NumOfLegs,
                Diet = diet,
            };
        }
    }
}
