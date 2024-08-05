
namespace InvoiceMaker.src
{
    public class Invoice
    {
        public string InvoiceNumber { get; init; }
        public string Person { get; init; }
        public List<Product> Products { get; init; }
        public Company CompanyReceiver { get; init; }
        public Company CompanySender { get; init; }

        public float Discount { get; set; }
        public DateOnly InvoiceDate { get; set; }
        public DateOnly DueDate { get; set; }


        public Invoice(
            string invoiceNumber, DateOnly invoiceDate, DateOnly dueDate, string person,
            Company receiver, Company sender, List<Product> products)
        {
            InvoiceNumber = invoiceNumber;
            InvoiceDate = invoiceDate;
            DueDate = dueDate;
            Person = person;

            CompanyReceiver = receiver;
            CompanySender = sender;
            Products = products;
        }


        /// <summary>
        ///   Calculates the discounted price based on the total price and discount percentage
        /// </summary>
        /// <param name="totalPrice"> The total price of all products in the list, including thier tax </param>
        /// <returns> The discounted price after applying the discount </returns>
        public double CalculateDiscount(double totalPrice)
        {
            double amount = (Discount / 100) * totalPrice;
            double calculation = totalPrice - amount;

            // returns result with two decimal.
            return Math.Truncate(calculation * 100) / 100;
        }
    }
}