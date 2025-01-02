using HotelBookingDTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBookingDAL
{
    /// <summary>
    ///   Defines a set of asynchronous database operations (CRUD) for T type in the database.
    /// </summary>
    /// <typeparam name="T"> any type of class </typeparam>
    public interface IRepository<T> where T : class
    {

        /// <summary>
        ///   Asynchronously retrieves all entities of T type from the database.
        /// </summary>
        /// <returns> IEnumerable list of all attributes in the entity </returns>
        Task<IEnumerable<T>> GetAllAsync();


        /// <summary>
        ///    Asynchronously retrieves an entity of type T from the database by its unique identifier.
        /// </summary>
        /// <param name="id"> The unique identifier of the entity. </param>
        /// <returns> The entity with the specified ID and all its attributes/columns </returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        ///   Asynchronously adds a new entity of type T to the database.
        /// </summary>
        /// <param name="entity"> The entity to be added. </param>
        /// <param name="payment"> The payment to be added, if any. </param>
        /// <returns> A task that represents the asynchronous operation </returns>
        Task AddAsync(T entity, Payment? payment = null);

        /// <summary>
        ///   Asynchronously updates an existing entity of type T in the database.
        /// </summary>
        /// <param name="entity">  The entity to be updated </param>
        /// <returns> A task that represents the asynchronous operation </returns>
        Task UpdateAsync(T entity, Payment? payment = null);

        /// <summary>
        ///   Asynchronously deletes an existing entity of type T from the database.
        /// </summary>
        /// <param name="id"> The entity to delete </param>
        /// <returns> A task that represents the asynchronous operation </returns>
        Task DeleteAsync(T entity);


        /// <summary>
        ///   Asynchronously searches for and retrieves a collection of items of type T based on the provided search string.
        /// </summary>
        /// <param name="searchStr"> The search string used to filter the items. </param>
        /// <returns> A task that represents the asynchronous operation, with a result of an enumerable collection of items. </returns>
        Task<IEnumerable<T>> FindAsync(string searchStr);
    }
}
