
namespace WildlifeTrackerSystem.src
{
    /// <summary>
    ///   Class with name and ingredients as fields.
    /// </summary>
    public class FoodItem
    {
        private string name;
        private ListManager<string> ingredients;


        public FoodItem()
        { 
            name = string.Empty;
            ingredients = new ListManager<string>();
        }
        

        public string Name
        { 
            get { return name; }
            set { name = value; }
        }

        public ListManager<string> Ingredients
        {
            get { return ingredients; }
        }


        /// <summary>
        /// </summary>
        /// <returns> A string with name and ingredients. </returns>
        public new string ToString()
        {
            // Separate elements in the list with comma.
            string strIngredients = string.Join(", ", ingredients.ToStringArray());
            string output = string.Format("Name: {0}, ingredients {1}", name, strIngredients);

            return output;
        }
    }
}
