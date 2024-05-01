using System.Windows;
using System.Windows.Controls;

using WildlifeTrackerSystem.src;

namespace WildlifeTrackerSystem
{
    /// <summary>
    /// Interaction logic for FoodItemForm.xaml
    /// </summary>
    public partial class FoodItemForm : Window
    {
        public FoodItem items = new FoodItem();

        public FoodItemForm()
        {
            InitializeComponent();
        }


        private bool CheckInputs(string btnName)
        {
            if (btnName == "Add")
            {
                if (string.IsNullOrEmpty(BoxIngredients.Text))
                {
                    MessageBox.Show("Ingredient cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }
            else if (btnName == "Change")
            {
                if (BoxViewFoodItems.SelectedIndex == -1 || string.IsNullOrEmpty(BoxIngredients.Text))
                {
                    MessageBox.Show("Please select an ingredient to change; the ingredient cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }
            else if (btnName == "Delete" && BoxViewFoodItems.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an ingredient to delete", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private void BtnACD_Click(object sender, RoutedEventArgs e)
        {
            string? button = ((Button)sender).Content.ToString();

            if (CheckInputs(button))
            {
                switch (button)
                {
                    case "Add":
                        if (!string.IsNullOrEmpty(BoxItemName.Text))
                        {
                            items.Name = BoxItemName.Text;
                        }
 
                        items.Ingredients.AddToList(BoxIngredients.Text);
                        break;
                    case "Change":
                        items.Ingredients.ChangeAt(BoxViewFoodItems.SelectedIndex, BoxIngredients.Text.ToString());
                        break;
                    case "Delete":
                        items.Ingredients.DeleteAt(BoxViewFoodItems.SelectedIndex);
                        break;
                }

                // clear input after adding
                BoxIngredients.Clear();

                // view Ingredients
                BoxViewFoodItems.ItemsSource = null;
                BoxViewFoodItems.ItemsSource = items.Ingredients.ToStringArray();
            }
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;    // indicate OK button click
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
