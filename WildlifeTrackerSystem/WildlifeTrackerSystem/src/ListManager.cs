using System.Reflection;

namespace WildlifeTrackerSystem.src
{
    /// <summary>
    ///   Generic class implementing IListManager
    /// </summary>
    /// <typeparam name="T"> List of T where it can be an Animal or a FoodItem or any other object </typeparam>
    public class ListManager<T> : IListManager<T>
    {
        private List<T> m_list;


        public ListManager()
        {
            m_list = new List<T>();
        }


        /// <summary>
        ///    Returns the number of items in the collection. Count starts from 1
        /// </summary>
        public int Count
        {
            get { return m_list.Count; }
        }


        /// <summary>
        ///   Retrieve the last element in the collection
        /// </summary>
        public T? GetLastElement
        {
            get { return (m_list.Count != 0) ? m_list.Last() : default; }
        }


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
        public bool CheckIndex(int index)
        {
            return (index >= 0 && index < m_list.Count);
        }

        /// <summary>
        ///   Removes everything from the collection.
        /// </summary>
        public void DeleteAll()
        {
            m_list.Clear();
        }

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

            if (m_list[index] is Animal animal)
            {
                // Return a deep copy of the element, preventing any later change to the returned object, thus not affecting the list.
                return (T)animal.CopyAnimal();
            }
            else
            {
                return m_list[index];
            }
        }


        /// <summary>
        ///   Returns an array of strings where every string is represents the object (calling the ToString() of the object).
        /// </summary>
        /// <returns> An Array of strings containing data </returns>
        public string[] ToStringArray()
        {
            List<string> listInfo = new List<string>();

            foreach (T element in m_list)
            {
                string? stringValues = element?.ToString(); // calling the ToString() of the object

                if (stringValues != null) listInfo.Add(stringValues);
            }

            return listInfo.ToArray();
        }


        /// <summary>
        ///   Search for the specific object in the collection, add it to a new list, and return the new list.
        /// </summary>
        /// <param name="searchStr"> The object as a string to search for </param>
        /// <returns> An array of objects that match the specified object (searchStr) </returns>
        public T[] SearchSpecificObject(string searchStr)
        {
            List<T> newList = new List<T>();

            foreach (T item in m_list)
            {
                // Get the name of the type of the current object and Check if it matches the specified object (searchStr).
                if (item?.GetType().Name == searchStr)
                {
                    newList.Add(item);
                }
            }
            return newList.ToArray();

        }


        /// <summary>
        ///   Gets the first item in the collection that matches the specified Id property.
        /// </summary>
        /// <param name="id"> The ID property of the object to be retrieved </param>
        /// <returns> The index of the object that matches the ID property, otherwise -1. </returns>
        public int GetIndexById(string id)
        {
            return m_list.FindIndex(item =>
            {
                // search for the first item that matches the specified id
                PropertyInfo? idProperty = typeof(T).GetProperty("Id");
                object? value = idProperty?.GetValue(item);
                return value != null && value.ToString() == id; // Return its index, otherwise -1
            });
        }



        public void JsonSerialize(string filename)
        {

        } 


        public void JsonDeserialize(string filename)
        {

        }


    }
}
