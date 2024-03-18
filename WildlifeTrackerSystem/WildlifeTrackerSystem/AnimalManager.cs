using System.Collections;

namespace WildlifeTrackerSystem
{
    internal class AnimalManager
    {
        private List<Animal> animalList;
        private int mammalCurrentID = 0;
        private int birdCurrentID = 0;
        private int fishCurrentID = 0;
        private int reptileCurrentID = 0;

        /// <summary>
        ///   Default constructor, creates the animal list.
        /// </summary>
        public AnimalManager() 
        { 
            animalList = new List<Animal>();
        }

        #region Properties
        public List<Animal> AnimalList 
        {
            get { return animalList; } 
        }

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
                return (ElementCount > 0) ? animalList[ElementCount - 1] : null;
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
                    return "B" + (birdCurrentID).ToString("D3");
                case CategoryType.Fish:
                    fishCurrentID += 1;
                    return "F" + (fishCurrentID).ToString("D3");
                case CategoryType.Mammal:
                    mammalCurrentID += 1;
                    return "M" + (mammalCurrentID).ToString("D3");
                case CategoryType.Reptile:
                    reptileCurrentID += 1;
                    return "R" + (reptileCurrentID).ToString("D3");
                default:
                    throw new ArgumentException("Invalid categorytype", nameof(category));
            }
        }

        #endregion
    }
}
