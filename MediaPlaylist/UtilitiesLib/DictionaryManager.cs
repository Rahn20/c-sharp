namespace UtilitiesLib
{
    /// <summary>
    ///   Generic class implementing IDictionaryManager inetrface to manage Dictionary (CRUD) operation.
    /// </summary>
    /// <typeparam name="T"> The type of items to be managed in the dictionary </typeparam>
    public class DictionaryManager<T> : IDictionaryManager<T>
    {
        private readonly Dictionary<int, T> _dict = new Dictionary<int, T>();
        private int _lastCreatedID = 0;


        public int Count => _dict.Count;

        public bool AddToDictionary(T type)
        {
            bool result = false;

            if (type != null)
            {
                // Increases ID with 1
                _lastCreatedID++;
                _dict.Add(_lastCreatedID, type);
                result = true;
            }

            return result;
        }

        public bool ChangeById(int id, T type)
        {
            bool result = false;

            if (type == null) return result;

            if (_dict.ContainsKey(id))
            {
                // O(1)
                _dict[id] = type;
                result = true;
            }

            return result;
        }

        public bool DeleteById(int id)
        {
            bool result = false;

            if (_dict.Remove(id)) result = true;

            return result;
        }

        public T? GetById(int id)
        {
            T? result = default;

            if (_dict.TryGetValue(id, out T value))
            {
                result = value;
            }
            return result;
        }

        public IEnumerable<T> GetAll()
        {
            return _dict.Values;
        }

        public T GetLastValue()
        {
            return _dict.Last().Value;
        }
    }
}
