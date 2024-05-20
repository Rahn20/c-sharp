namespace WildlifeTrackerSystem.src
{
    /// <summary>
    ///   A generic list manager interface. The IListManager can be implemented by different classes passing any type <T> at declaration
    ///   but then <T> must have the same type in all methods included in this interface.
    /// </summary>
    /// <typeparam name="T"> List of T where it can be an Animal or a FoodItem or any other object </typeparam>
    public interface IListManager<T>
    {
        /// <summary>
        ///   Returns the number of elements in the collection
        /// </summary>
        int Count { get; }


        /// <summary>
        ///  Retrieve the last element in the collection
        /// </summary>
        T? GetLastElement { get; }

        /// <summary>
        ///   Adds a new object to the end of the collection
        /// </summary>
        /// <param name="type"> the object to be added </param>
        /// <returns> True if the object was successfully added, otheriwse false. </returns>
        bool AddToList(T type);


        /// <summary>
        ///   Replaces an object in a given position with a new object
        /// </summary>
        /// <param name="index"> index to object that is to be replaced </param>
        /// <param name="type"> The object to be replaced </param>
        /// <returns> True if successful otheriwse false </returns>
        bool ChangeAt(int index, T type);


        /// <summary>
        ///   Checks if the index is within the range of the collection
        /// </summary>
        /// <param name="index"> The index to be checked </param>
        /// <returns> True if the given index is within the range of the collection, otherwise false </returns>
        bool CheckIndex(int index);

        /// <summary>
        ///   Removes everything from the collection, empties it.
        /// </summary>
        void DeleteAll();

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
        /// <returns> The T at the specified index, or null if the index is out of range </returns>
        T? GetAt(int index);


        /// <summary>
        ///  Returns an array of strings where every string is represents the object (calling the ToString() of the object)
        /// </summary>
        /// <returns> An Array of strings containing data </returns>
        string[] ToStringArray();


        /// <summary>
        ///    Search for the specific object in the collection, add it to a new list, and return the new list.
        /// </summary>
        /// <param name="searchStr"> The object as a string to search for </param>
        /// <returns> An array of objects that match the specified object </returns>
        T[] SearchSpecificObject(string searchStr);

        /// <summary>
        ///   Retrieves all the items in the collection.
        /// </summary>
        /// <returns> An array of type (T) containing data. </returns>
        public T[] GetAllItems();

        /// <summary>
        ///   Adds/ writes the data in the m_list to a file in JSON format.
        /// </summary>
        /// <param name="filename"> The full path of the file or filename to be written the data into it. </param>
        void JsonSerialize(string filename);


        /// <summary>
        ///   Reads the JSON data from a file and adds it to the m_list.
        /// </summary>
        /// <param name="filename"> The full path of the file or filename to be written the data into it </param>
        void JsonDeserialize(string filename);

    }
}
