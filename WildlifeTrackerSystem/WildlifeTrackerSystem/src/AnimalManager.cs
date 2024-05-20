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

        #region Delegates
        private delegate IEnumerable GetAnimalTypeValuesDelegate();
        private delegate string GetNewIDDelegate();
        private delegate Animal CreateAnimalDelegate(object animalType);
        private delegate void UpdateCurrentIDsDelegate(string animalID);
        #endregion

        #region Methods
        /// <summary>
        ///  Retrieves the delegates for Retrieving the animal type values, generating animal IDs and creating animals based on the specified category type.
        ///  Using Delegates, a type that represents references to methods with a specific signature.
        ///  Are used to pass methods as arguments to other methods, which allows for flexible and dynamic method execution.
        /// </summary>
        /// <param name="category"> The category type of the animal (Bird, Fish, Mammal, Reptile) </param>
        /// <returns> A tuple containing a delegate </returns>
        /// <exception cref="ArgumentException"> Thrown when an invalid category type is provided </exception>
        private (GetAnimalTypeValuesDelegate, GetNewIDDelegate, CreateAnimalDelegate, UpdateCurrentIDsDelegate) GetHandlers(CategoryType category)
        {
            switch (category)
            {
                case CategoryType.Bird:
                    return (
                        () => Enum.GetValues(typeof(BirdType)).Cast<BirdType>(),
                        () => {
                            birdCurrentID += 1;
                            return "B" + birdCurrentID.ToString("D3");
                        },
                        animalType => Bird.Bird.CreateBird((BirdType)animalType),
                        animalID => birdCurrentID = int.Parse((animalID).Substring(1))
                        );

                case CategoryType.Fish:
                    return (
                        () => Enum.GetValues(typeof(FishType)).Cast<FishType>(),
                        () => {
                            fishCurrentID += 1;
                            return "F" + fishCurrentID.ToString("D3");
                        },
                        animalType => Fish.Fish.CreateFish((FishType)animalType),
                        animalID => fishCurrentID = int.Parse((animalID).Substring(1))
                        );

                case CategoryType.Mammal:
                    return (
                        () => Enum.GetValues(typeof(MammalType)).Cast<MammalType>(),
                        () => {
                            mammalCurrentID += 1;
                            return "M" + mammalCurrentID.ToString("D3");
                        },
                        animalType => Mammal.Mammal.CreateMammal((MammalType)animalType),
                        animalID => mammalCurrentID = int.Parse((animalID).Substring(1))
                        );

                case CategoryType.Reptile:
                    return (
                        () => Enum.GetValues(typeof(ReptileType)).Cast<ReptileType>(),
                        () => {
                            reptileCurrentID += 1;
                            return "R" + reptileCurrentID.ToString("D3");
                        },
                         animalType => Reptile.Reptile.CreateReptile((ReptileType)animalType),
                         animalID => reptileCurrentID = int.Parse((animalID).Substring(1))
                        );
                default:
                    throw new ArgumentException("Invalid categorytype", nameof(category));
            }
        }


        /// <summary>
        ///    Gets the values of the specified animal type enumeration based on the provided category type.
        /// </summary>
        /// <param name="category"> The category type of animal </param>
        /// <returns> An enumerable collection of animal type values </returns>
        /// <exception cref="ArgumentException"> Throw an exception for invalid category type </exception>
        public IEnumerable GetAnimalTypeValues(CategoryType category)
        {
            var (getAnimalTypeValuesHandler, _, _, _) = GetHandlers(category);
            return getAnimalTypeValuesHandler();
        }


        /// <summary>
        ///    Generates a new ID based on the provided category type.
        /// </summary>
        /// <param name="category">  The category type of animal </param>
        /// <returns> A new ID string based on the provided category type </returns>
        /// <exception cref="ArgumentException"> Throw an exception for invalid category type </exception>
        public string GetNewID(CategoryType category)
        {
            var (_, GetNewIDHandler, _, _) = GetHandlers(category);
            return GetNewIDHandler();
        }


        /// <summary>
        ///   Creates a new instance based on the animal category and animal species.
        /// </summary>
        /// <param name="category"> Animal category type </param>
        /// <param name="animalType"> animal specie </param>
        /// <returns> Instance of animal based on the animal specie</returns>
        /// <exception cref="ArgumentException"> Throw an exception for invalid category type </exception>
        public Animal CreateAnimal(CategoryType category, object animalType)
        {
            var (_, _, CreateAnimalHandler, _) = GetHandlers(category);
            return CreateAnimalHandler(animalType);
        }


        /// <summary>
        ///   Sets values to the four current animal IDs by looping through each animal in the list.
        /// </summary>
        /// <exception cref="ArgumentException"> Throw an exception for invalid category type</exception>
        public void UpdateCurrentIDs()
        {
            for (int count = 0; count < this.Count; count++)
            {
                Animal? animalObj = this.GetAt(index: count);

                if (animalObj != null)
                {
                    var (_, _, _, UpdateCurrentIDsHandler) = GetHandlers(animalObj.Category);
                    UpdateCurrentIDsHandler(animalObj.Id);
                }
            }
        }


        /// <summary>
        ///   Sets the animal's current IDs to 0.
        /// </summary>
        public void ResetAnimalsIDs()
        {
            birdCurrentID = 0;
            fishCurrentID = 0;
            mammalCurrentID = 0;
            reptileCurrentID = 0;
        }

        #endregion
    }
}
