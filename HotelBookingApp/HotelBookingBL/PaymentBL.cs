using HotelBookingDAL;
using HotelBookingDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingBL
{
    /// <summary>
    ///    Business Logic Layer (BL) for managing payment operations. 
    ///    Provides methods for retrieving, adding, updating, deleting and filtering payments.
    /// </summary>
    public class PaymentBL
    {
        private readonly IRepository<Payment> _payment;

        public PaymentBL(IRepository<Payment> repository)
        {
            _payment = repository;
        }

        /// <summary>
        ///   Retrieves all payments from the repository.
        /// </summary>
        /// <returns> A task that represents the asynchronous operation, containing the list of all payments </returns>
        public async Task<IEnumerable<Payment>> GetAllPayments() => await _payment.GetAllAsync();


        /// <summary>
        ///  Filters payments based on the provided search text, searching for either
        ///  the guest's full name or the exact room number matching the provided text.
        /// </summary>
        /// <param name="searchTxt"> The search text used to filter payments. </param>
        /// <returns> A result of an enumerable collection of filtered payments </returns>
        public async Task<IEnumerable<Payment>> FilterByGuestOrRoom(string searchTxt) => await _payment.FindAsync(searchTxt);
    }
}
