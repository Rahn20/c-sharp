
namespace InvoiceMaker.src
{
    public class Product
    {
        // The name of the product
        public string Name { get; init; }

        // The quantity of the product.
        public int Quantity { get; init; }

        // The product price
        public double Price { get; init; }

        // The product Tax
        public double Tax { get; init; }


        public Product(string name, int quantity, double price, double tax)
        {
            Name = name;
            Quantity = quantity;
            Price = price;
            Tax = tax;
        }


        /// <summary>
        ///   Calculates the total tax based on the tax rate, price, and quantity.
        /// </summary>
        public double TotalTax
        {
            get
            {
                double calculation = (Tax / 100) * Price * Quantity;
                return Math.Truncate(calculation * 100) / 100; // Returns the result rounded to two decimal.
            }
        }


        /// <summary>
        ///   Calculates the total price based on the total tax, price, and quantity.
        /// </summary>
        public double TotalPrice
        {
            get
            {
                double calculation = (Price * Quantity) + TotalTax;
                return Math.Truncate(calculation * 100) / 100; 
            }
        }
    }
}
