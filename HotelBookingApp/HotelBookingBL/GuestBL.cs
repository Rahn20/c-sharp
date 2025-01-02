using HotelBookingDAL;
using HotelBookingDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HotelBookingBL
{

    /// <summary>
    ///   Business Logic Layer (BL) for managing guest operations. 
    ///   Provides methods for retrieving, adding, updating, and deleting guests.
    /// </summary>
    public class GuestBL
    {
        private readonly IRepository<Guest> _guest;


        public GuestBL(IRepository<Guest> repository)
        {
            _guest = repository;
        }


        /// <summary>
        ///   Retrieves all guests from the repository.
        /// </summary>
        /// <returns> A task that represents the asynchronous operation, containing the list of all guests </returns>
        public async Task<IEnumerable<Guest>> GetAllGuests() => await _guest.GetAllAsync();


        /// <summary>
        ///  Adds a new guest to the repository.
        /// </summary>
        /// <param name="guest"> The guest entity to add </param>
        /// <returns> A task that represents the asynchronous operation. </returns>
        public async Task AddGuest(Guest guest) => await _guest.AddAsync(guest);


        /// <summary>
        ///  Checks email address, the email is considered valid if it matches a specific email pattern
        ///  and is not already associated with an existing guest.
        /// </summary>
        /// <param name="email"> The email address to be validated </param>
        /// <returns> A task, boolean indicating whether the email is valid (true) or invalid (false) </returns>
        public async Task<bool> IsEmailValid(string email)
        {
            string pattern = @"^[a-zA-Z0-9._]+@[a-zA-Z0-9]+\.[a-zA-Z]{2,}$";
            
            if (!Regex.IsMatch(email, pattern)) return false;

            var allGuests = await _guest.GetAllAsync();
            return !allGuests.Any(g => g.Email == email);
        }

        /// <summary>
        ///   Validates the provided phone number by checking if it matches the required pattern.
        /// </summary>
        /// <param name="phone"> The phone number to be validated </param>
        /// <returns> A boolean indicating whether the phone number is valid (true) or invalid (false) </returns>
        public static bool IsPhoneNumberValid(string phone)
        {
            string pattern = @"^0\d{9}$";
            return Regex.IsMatch(phone, pattern);
        }


        /// <summary>
        ///   Updates the details of an existing guest in the repository.
        /// </summary>
        /// <param name="guest"> The guest entity with updated details </param>
        /// <returns> A task that represents the asynchronous operation. </returns>
        public async Task UpdateGuest(Guest guest) => await _guest.UpdateAsync(guest);


        /// <summary>
        ///   Deletes a guest from the repository.
        /// </summary>
        /// <param name="guest"> The guest entity to delete </param>
        /// <returns> A task that represents the asynchronous operation. </returns>
        public async Task DeleteGuest(Guest guest) => await _guest.DeleteAsync(guest);


        /// <summary>
        ///  Filters guests based on the provided search text, searching for guest fullname
        /// </summary>
        /// <param name="searchTxt"> The search text used to filter guests. </param>
        /// <returns> A result of an enumerable collection of filtered guests </returns>
        public async Task<IEnumerable<Guest>> FilterByGuestName(string searchTxt) => await _guest.FindAsync(searchTxt);
    }
}
