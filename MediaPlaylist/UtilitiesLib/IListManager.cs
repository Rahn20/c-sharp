namespace UtilitiesLib
{
    /// <summary>
    ///  A generic list manager interface provides methods to get, add, change, and remove items from the collection.
    ///  The IListManager can be implemented by different classes passing any type <T> at declaration.
    /// </summary>
    /// <typeparam name="T"> List of T where it can be a MediaPlaylist or any other type </typeparam>
    public interface IListManager<T>
    {
        /// <summary>
        ///  Retrieves the number of elements in the collection.
        /// </summary>
        int Count { get; }

        /// <summary>
        ///   Adds a new object to the end of the collection.
        /// </summary>
        /// <param name="type"> The object to be added </param>
        /// <returns> True if the object was successfully added, otheriwse false. </returns>
        bool AddToList(T type);


        /// <summary>
        ///   Replaces an object in a given position with a new object.
        /// </summary>
        /// <param name="index"> Index to object that is to be replaced </param>
        /// <param name="type"> The object to be replaced </param>
        /// <returns> True if successful, otheriwse false </returns>
        bool ChangeAt(int index, T type);


        /// <summary>
        ///   Checks if the index is within the range of the collection
        /// </summary>
        /// <param name="index"> The index to be checked </param>
        /// <returns> True if the given index is within the range of the collection, otherwise false </returns>
        bool CheckIndex(int index);

        /// <summary>
        ///   Removes an object from the collection at a given index.
        /// </summary>
        /// <param name="index"> Index to object that is to be removed </param>
        /// <returns> True if successful deleted, otheriwse false </returns>
        bool DeleteAt(int index);

        /// <summary>
        ///   Returns an object from a certain position in the collection.
        /// </summary>
        /// <param name="index"> The index to object that is to be retrieved </param>
        /// <returns> The Type at the specified index, or null if the index is out of range </returns>
        T? GetAt(int index);
    }
}
