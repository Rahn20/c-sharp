namespace UtilitiesLib
{
    /// <summary>
    ///   Generic class implementing IListManager inetrface to manage list operation.
    /// </summary>
    /// <typeparam name="T"> T where it can be any type (ex: object, string etc.)  </typeparam>
    public class ListManager<T> : IListManager<T>
    {
        private List<T> _list = new List<T>();


        public ListManager() {}


        public int Count => _list.Count;

        public bool AddToList(T type)
        {
            bool result = false;

            if (type != null)
            {
                _list.Add(type);
                result = true;
            }

            return result;
        }

        public bool ChangeAt(int index, T type)
        {
            bool result = false;

            if (type != null && CheckIndex(index) == true)
            {
                _list[index] = type;
                result = true;
            }
            return result;
        }

        public bool CheckIndex(int index) => index >= 0 && index < _list.Count;

        public bool DeleteAt(int index)
        {
            bool result = false;

            if (CheckIndex(index) == true)
            {
                _list.RemoveAt(index);
                result = true;
            }
            return result;
        }

        public T? GetAt(int index)
        {
            if (CheckIndex(index) == false) return default;
            return _list[index];
        }
    }
}
