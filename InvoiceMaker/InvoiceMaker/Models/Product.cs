namespace InvoiceMaker.Models
{
    /// <summary>
    ///     Product Information, with the necessary properties, (init-only properties)
    /// </summary>
    /// <param name="name">  The name of the product </param>
    /// <param name="quantity"> The quantity of the product. </param>
    /// <param name="price"> The product price </param>
    /// <param name="tax">  The product Tax </param>
    public class Product(string name, int quantity, double price, double tax)
    {
        public string Name { get; init; } = name;
        public int Quantity { get; init; } = quantity;
        public double Price { get; init; } = price;
        public double Tax { get; init; } = tax;


        /// <summary>
        ///   Calculates the total tax based on the tax rate, price, and quantity.
        /// </summary>
        public double TotalTax
        {
            get
            {
                double calculation = Tax / 100 * Price * Quantity;
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
                double calculation = Price * Quantity + TotalTax;
                return Math.Truncate(calculation * 100) / 100;
            }
        }
    }
}
