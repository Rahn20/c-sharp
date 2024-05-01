using System.Collections;

namespace WildlifeTrackerSystem.src
{
    /// <summary>
    ///    Manage animals
    /// </summary>
    public class AnimalManager : ListManager<Animal>
    {
        #region Fields
        private int mammalCurrentID = 0;
        private int birdCurrentID = 0;
        private int fishCurrentID = 0;
        private int reptileCurrentID = 0;
        #endregion

        public AnimalManager() { }


        #region Methods

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
        #endregion
    }
}
