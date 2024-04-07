
namespace WildlifeTrackerSystem.src
{
    /// <summary>
    ///   Class serves as schedule for feeding of the animals
    /// </summary>
    public class FoodSchedule
    {
        #region Fields
        private EaterType eaterType;
        private List<string> foodList;  // ex: morning: ***, lunch: ***, evening: ***
        #endregion

        public FoodSchedule()
        {
            foodList = new List<string>();
        }

        #region Properties
        public int Count
        {
            get { return foodList.Count; } 
        }

        public EaterType EaterType
        {
            get { return eaterType; }
            set { eaterType = value; }
        }
        #endregion

        #region Methods


        /// <summary>
        ///  Adds an item to the food list if it is not null.
        /// </summary>
        /// <param name="item"> The item to add to the foodlist </param>
        public void AddToFoodList(string item) 
        {
            if (item != null) foodList.Add(item);
        }


        /// <summary>
        ///   Changes the item at the specified index in the foodlist with the provided item.
        /// </summary>
        /// <param name="index"> The index of the item to change </param>
        /// <param name="item"> The new item to replace the existing item. </param>
        /// <returns> True if the item was successfully changed, otherwise false </returns>
        public bool ChangeAt(int index, string item)
        {
            bool changed = false;

            if (CheckIndex(index) && item != null)
            {
                foodList[index] = item;
                changed = true;
            }

            return changed;
        }


        /// <summary>
        ///   Checks if the index is within the range of the collection
        /// </summary>
        /// <param name="index"></param>
        /// <returns> False if the given index is not within the range of the collection; otherwise True </returns>
        public bool CheckIndex(int index)
        {
            return (index <= Count - 1);
        }


        /// <summary>
        ///    Removes the item at the specified index in the foodlist.
        /// </summary>
        /// <param name="index"> The index of the item to remove </param>
        /// <returns></returns>
        public bool DeleteAt(int index)
        {
            bool removed = false;

            if (CheckIndex(index))
            {
                foodList.RemoveAt(index);
                removed = true;
            }

            return removed;
        }


        /// <summary>
        ///   Retrieves the foodlist as an array of strings.
        /// </summary>
        /// <returns> An array containing the items in the foodlist </returns>
        public string[] GetFoodListInfoString()
        {
            return foodList.ToArray();
        }

        /// <summary>
        ///   
        /// </summary>
        /// <returns></returns>
        public string Title()
        {
            // Have to idea what the title should be here, it was not mentioned in the requirement description
            // I leave it empty for now
            return "";
        }

        public new string ToString()
        {
            string output = string.Format("{0, -20} {1, 10}", "Eastertype:", eaterType);
            return output;
        }

        #endregion

    }
}
