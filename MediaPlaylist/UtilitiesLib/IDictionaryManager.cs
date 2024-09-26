namespace UtilitiesLib
{
    /// <summary>
    ///   Defines methods and properties for managing a collection of items of <T> type/> 
    ///   in a dictionary structure, allowing for basic CRUD operations, where the Id is the (KEY).
    /// </summary>
    /// <typeparam name="T"> The type of items to be managed in the dictionary </typeparam>
    public interface IDictionaryManager<T>
    {
        /// <summary>
        ///   Gets the total number of items currently stored in the dictionary.
        /// </summary>
        int Count { get; }


        /// <summary>
        ///   Adds a new item of type <T> to the dictionary.
        /// </summary>
        /// <param name="type"> The item to be added. </param>
        /// <returns> True if the item was added successfully, otherwise false. </returns>
        bool AddToDictionary(T type);


        /// <summary>
        ///   Updates an existing item in the dictionary by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the item to be updated.</param>
        /// <param name="type"> The type of item to be updated.</param>
        /// <returns> True if the item was updated successfully; otherwise, false. </returns>
        bool ChangeById(int id, T type);


        /// <summary>
        ///   Deletes an item from the dictionary by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the item to be deleted.</param>
        /// <returns> True if the item was deleted successfully; otherwise false.</returns>
        bool DeleteById(int id);

        /// <summary>
        ///  Retrieves an item from the dictionary by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the item to retrieve. </param>
        /// <returns> True if the item was deleted successfully; otherwise false </returns>
        T? GetById(int id);


        /// <summary>
        ///   Retrieves all items in the dictionary.
        /// </summary>
        /// <returns> IEnumerable containing all the items (The Values) in the dictionary. </returns>
        IEnumerable<T> GetAll();
    }
}
