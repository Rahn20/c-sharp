namespace InvoiceMaker.Models
{
    /// <summary>
    ///    Represents a record address with street, city, country, and zip code.
    /// </summary>
    /// <param name="Street"> The street name of the address </param>
    /// <param name="City"> The city name of the address </param>
    /// <param name="Country"> The country name of the address. </param>
    /// <param name="ZipCode"> The ZIP code of the address </param>
    public record Address(string Street, string City, string Country, int ZipCode);


    /// <summary>
    ///  Represents a record company with a name, address, phone number, and URL.
    /// </summary>
    /// <param name="Name"> The name of the company.</param>
    /// <param name="Address"> The address of the company.</param>
    /// <param name="PhoneNumber"> The phone number of the company (optional).</param>
    /// <param name="URL"> The URL of the company's website (optional).</param>
    public record Company(string Name, Address Address, long? PhoneNumber = null, string? URL = null);
}