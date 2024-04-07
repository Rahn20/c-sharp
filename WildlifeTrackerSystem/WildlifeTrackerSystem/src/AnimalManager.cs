using System.Collections;

namespace WildlifeTrackerSystem.src
{
    /// <summary>
    ///  Manages animals
    /// </summary>
    public class AnimalManager
    {
        #region Fields
        private List<Animal> animalList;
        private int mammalCurrentID = 0;
        private int birdCurrentID = 0;
        private int fishCurrentID = 0;
        private int reptileCurrentID = 0;
        #endregion

        /// <summary>
        ///   Default constructor, creates the animal list.
        /// </summary>
        public AnimalManager()
        {
            animalList = new List<Animal>();
        }

        #region Properties

        /// <summary>
        ///   Gets the number of elements in the animal list.
        /// </summary>
        public int ElementCount
        {
            get { return animalList.Count; }
        }

        /// <summary>
        ///   Gets the last animal in the list if the list is not empty; otherwise, returns null
        /// </summary>
        public Animal? GetLastAnimal
        {
            get
            {
                // Check if there are any animals in the list
                return ElementCount > 0 ? animalList[ElementCount - 1] : null;
            }
        }
        #endregion

        #region Methods

        /// <summary>
        ///   Adds an animal to the list if animal object is not null.
        /// </summary>
        /// <param name="animalObj"> The animal object to add. </param>
        public void AddToList(Animal animalObj)
        {
            if (animalObj != null) animalList.Add(animalObj);
        }


        /// <summary>
        ///    Gets the values of the specified animal type enumeration based on the provided category type.
        /// </summary>
        /// <param name="category"> The category type of animal </param>
        /// <returns> An enumerable collection of animal type values </returns>
        /// <exception cref="ArgumentException"> Throw an exception for invalid category type </exception>
        public static IEnumerable GetAnimalTypeValues(CategoryType category)
        {
            switch (category)
            {
                case CategoryType.Bird:
                    return Enum.GetValues(typeof(BirdType)).Cast<BirdType>();
                case CategoryType.Fish:
                    return Enum.GetValues(typeof(FishType)).Cast<FishType>();
                case CategoryType.Mammal:
                    return Enum.GetValues(typeof(MammalType)).Cast<MammalType>();
                case CategoryType.Reptile:
                    return Enum.GetValues(typeof(ReptileType)).Cast<ReptileType>();
                default:
                    throw new ArgumentException("Invalid categorytype", nameof(category));
            }
        }


        /// <summary>
        ///    Generates a new ID based on the provided category type.
        /// </summary>
        /// <param name="category">  The category type of animal </param>
        /// <returns> A new ID string based on the provided category type </returns>
        /// <exception cref="ArgumentException"> Throw an exception for invalid category type </exception>
        public string GetNewID(CategoryType category)
        {
            switch (category)
            {
                case CategoryType.Bird:
                    birdCurrentID += 1;
                    return "B" + birdCurrentID.ToString("D3");
                case CategoryType.Fish:
                    fishCurrentID += 1;
                    return "F" + fishCurrentID.ToString("D3");
                case CategoryType.Mammal:
                    mammalCurrentID += 1;
                    return "M" + mammalCurrentID.ToString("D3");
                case CategoryType.Reptile:
                    reptileCurrentID += 1;
                    return "R" + reptileCurrentID.ToString("D3");
                default:
                    throw new ArgumentException("Invalid categorytype", nameof(category));
            }
        }


        /// <summary>
        ///   Creates a new instance based on the animal category and animal species.
        /// </summary>
        /// <param name="category"> Animal category type </param>
        /// <param name="animalType"> animal specie </param>
        /// <returns> Instance of animal based on the animal specie</returns>
        /// <exception cref="ArgumentException"></exception>
        public static Animal CreateAnimal(CategoryType category, object animalType)
        {
            switch (category)
            {
                case CategoryType.Bird:
                    return Bird.Bird.CreateBird((BirdType)animalType); 
                case CategoryType.Fish:
                    return Fish.Fish.CreateFish((FishType)animalType);
                case CategoryType.Mammal:
                    return Mammal.Mammal.CreateMammal((MammalType)animalType);
                case CategoryType.Reptile:
                    return Reptile.Reptile.CreateReptile((ReptileType)animalType);
                default:
                    throw new ArgumentException("Invalid categorytype", nameof(category));
            }
        }


        /// <summary>
        ///   Searches for animals by species.
        /// </summary>
        /// <param name="specie"> The species to search for. </param>
        /// <returns> An array of animals matching the specified species </returns>
        public Animal[] FindAnimalsBySpecies(string specie)
        {
            List<Animal> newList = new List<Animal>();

            foreach (Animal animal in animalList)
            {
                string typeName = animal.GetType().Name;    // Get the name of the type of the current animal

                // Check if the type name matches the specified species
                if (typeName == specie) 
                {
                    newList.Add(animal);
                }
            }
            return newList.ToArray();

        }

        /// <summary>
        ///   Gets animal information by calling the ToString() method of every element of the list.
        /// </summary>
        /// <returns> An array of animal information </returns>
        public string[] GetAnimalInfoStrings()
        {
            List<string> info = new List<string>();

            foreach (Animal animal in animalList)
            {
                info.Add(animal.ToString());
            }
            return info.ToArray();
        }


        /// <summary>
        ///   Retrieves the animal at the specified index.
        /// </summary>
        /// <param name="index"> The index of the animal to retrieve </param>
        /// <returns> The animal at the specified index, or null if the index is out of range </returns>
        public Animal? GetAnimalAt(int index)
        {
            return ((index >= 0) && (index < ElementCount)) ? animalList[index]: null;
        }


        /// <summary>
        ///    Retrieves a specific animal from the provided array of animals based on the index.
        /// </summary>
        /// <param name="index"> The index of the animal to retrieve. </param>
        /// <param name="species"> The array of animals to search within </param>
        /// <returns> The animal at the specified index, or null if the index is out of range. </returns>
        public static Animal? GetSpecificAnimalBy(int index, Animal[] species)
        {
            return ((index >= 0) && (index < species.Length)) ? species[index] : null;
        }

        #endregion
    }
}
