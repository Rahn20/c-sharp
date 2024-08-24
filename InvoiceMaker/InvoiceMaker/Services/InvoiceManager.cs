using System.Globalization;
using System.IO;
using InvoiceMaker.Models;

namespace InvoiceMaker.Services
{
    /// <summary>
    ///   Manages invoices with company and product data, reading invoice information from a file.
    /// </summary>
    public class InvoiceManager
    {
        // An array of strings containing datafile. 
        private string[] lines;

        private Invoice _invoice;
        public Invoice Invoice 
        { 
            get => _invoice;
            private set
            {
                _invoice = value;
                InvoiceUpdated?.Invoke(this, _invoice);
            }
        }

        public InvoiceManager()
        { 
            lines = [];
        }


        // Event raised when the Invoice is updated.
        public event EventHandler<Invoice?>? InvoiceUpdated;


        #region Methods

        /// <summary>
        ///   Opens a text file, reads all lines of the file, and closes the file. Adds the file content to an Invoice object.
        /// </summary>
        /// <param name="filepath"> The file path </param>
        /// <exception cref="FileNotFoundException"></exception>
        public void MakeInvoice(string filepath)
        {
            if (!File.Exists(filepath)) throw new FileNotFoundException("File not Found");

            lines = File.ReadAllLines(filepath);

            var (addressReceiver, addressSender) = AddAddress();
            var (companyReceiver, companySender) = AddCompany(addressReceiver, addressSender);
            List<Product> products = AddProducts();

            Invoice = new Invoice(
                invoiceNumber: lines[0],
                invoiceDate: DateOnly.Parse(lines[1]),
                dueDate: DateOnly.Parse(lines[2]),
                person: lines[4],
                receiver: companyReceiver,
                sender: companySender,
                products: products
            );
        }


        /// <summary>
        ///   Creates and returns addresses for the receiver- and sender-companies based on the provided lines in the file.
        /// </summary>
        /// <returns> A tuple containing addresses for the receiver and sender </returns>
        private (Address addressReceiver, Address addressSender) AddAddress()
        {
            Address receiver = new Address(
                Street: lines[5],
                City: lines[7],
                Country: lines[8],
                ZipCode: int.Parse(lines[6])
            );

            Address sender = new Address(
                Street: lines[^6],      // lines.Length - 6
                City: lines[^4],
                Country: lines[^3],
                ZipCode: int.Parse(lines[^5])
            );

            return (receiver, sender);
        }


        /// <summary>
        ///    Creates and returns company information for the receiver- and sender-companies based on the provided lines in the file.
        /// </summary>
        /// <param name="addressReceiver"> The address of the receiver company </param>
        /// <param name="addressSender"> The address of the Sender company </param>
        /// <returns> A tuple containing companies information for the receiver and sender </returns>
        private (Company companyReceiver, Company companySender) AddCompany(Address addressReceiver, Address addressSender)
        {
            // The receiver company does not have a URL or phone number.
            Company receiver = new Company(
                Name: lines[3],
                Address: addressReceiver
            );

            // Removes all spaces, lines.Length - 2
            string phoneNr = lines[^2].Replace(" ", "");

            Company sender = new Company(
                Name: lines[^7],
                Address: addressSender,
                PhoneNumber: long.Parse(phoneNr),
                URL: lines[^1]
            );

            return (receiver, sender);
        }


        /// <summary>
        ///   Creates and returns a list of products based on the provided lines in the file.
        /// </summary>
        /// <returns> A list of products </returns>
        private List<Product> AddProducts()
        {
            List<Product> products = new List<Product>();

            // Index 9 in the lines-variable represents the number of items. After index 9 comes the product data.
            int NumberOfItems = short.Parse(lines[9]);
            int index = 9;
            int count = 0;

            while (count < NumberOfItems)
            {
                // The CultureInfo.InvariantCulture ensures that the double separator is always a dot (.)
                Product productObj = new Product(
                    name: lines[index + 1],
                    quantity: int.Parse(lines[index + 2]),
                    price: double.Parse(lines[index + 3], CultureInfo.InvariantCulture),
                    tax: double.Parse(lines[index + 4], CultureInfo.InvariantCulture)
                );

                products.Add(productObj);

                count++;
                index += 4;
            }

            return products;
        }


        /// <summary>
        ///   Calculates and returns the total price of all products in the list, including tax.
        /// </summary>
        /// <returns> The total price of all products including tax, truncated to two decimal places </returns>
        public double GetTotalPrice()
        {
            if (_invoice == null) return 0;

            // Total price including tax
            double totalPrice = 0;

            foreach (Product product in _invoice.Products)
            {
                totalPrice += product.TotalPrice;
            }

            return Math.Truncate(totalPrice * 100) / 100;
        }

        #endregion
    }
}
