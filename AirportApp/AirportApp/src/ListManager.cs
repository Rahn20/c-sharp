
namespace AirportApp.src
{
    /// <summary>
    ///   Generic class implementing IListManager inetrface
    /// </summary>
    /// <typeparam name="T"> List of T where it can be an Airplane or any other object.  </typeparam>
    public class ListManager<T> : IListManager<T>
    {

        private List<T> m_list = new List<T>();

        public ListManager() { }


        /// <summary>
        ///    Retrieves the number of items in the collection. Count starts from 1.
        /// </summary>
        public int Count => m_list.Count;


        /// <summary>
        ///   Adds a new object to the end of the collection
        /// </summary>
        /// <param name="type"> the object to be added </param>
        /// <returns> True if the object was successfully added, otheriwse false. </returns>
        public bool AddToList(T type)
        {
            if (type == null) return false;

            m_list.Add(type);
            return true;
        }


        /// <summary>
        ///   Replaces an object in a given position (index) with a new object.
        /// </summary>
        /// <param name="index"> Index to object that is to be replaced </param>
        /// <param name="type"> The object to be replaced </param>
        /// <returns> True if successful, otheriwse false </returns>
        public bool ChangeAt(int index, T type)
        {
            if (type == null || CheckIndex(index) == false) return false;

            m_list[index] = type;
            return true;
        }

        /// <summary>
        ///   Checks if the index is within the range of the collection.
        /// </summary>
        /// <param name="index"> The index to be checked </param>
        /// <returns> True if the given index is within the range of the collection, otherwise false </returns>
        public bool CheckIndex(int index) => (index >= 0 && index < m_list.Count);


        /// <summary>
        ///   Removes an object from the collection at a given index.
        /// </summary>
        /// <param name="index"> Index to object that is to be removed </param>
        /// <returns> True if successful deleted, otheriwse false </returns>
        public bool DeleteAt(int index)
        {
            if (CheckIndex(index) == false) return false;

            m_list.RemoveAt(index);
            return true;
        }

        /// <summary>
        ///   Returns an object from a certain position (index) in the collection.
        /// </summary>
        /// <param name="index"> Index to object that is to be retrieved </param>
        /// <returns> The T at the specified index, or null if the index is out of range. </returns>
        public T? GetAt(int index)
        {
            if (CheckIndex(index) == false) return default;

            if (m_list[index] is Airplane flight)
            {
                // Return a deep copy of the element, preventing any later change to the returned object,
                // thus not affecting the list.
                return (T)(object)flight.CopyAirplane();
            }
            else
            {
                return m_list[index];
            }
        }


        /// <summary>
        ///   Returns an array of strings where every string is represents the object (calling the ToString() of the object)
        /// </summary>
        /// <returns> An Array of strings containing data </returns>
        public string[] ToStringArray()
        {
            List<string> listInfo = new List<string>();

            foreach (T element in m_list)
            {
                string? stringValues = element?.ToString();

                if (stringValues != null) listInfo.Add(stringValues);
            }

            return listInfo.ToArray();
        }
    }
}
