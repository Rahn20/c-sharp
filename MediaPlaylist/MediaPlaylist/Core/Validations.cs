using System;
using System.Collections;
using System.ComponentModel;

namespace MediaPlaylist.Core
{
    /// <summary>
    ///   Provides functionality for managing and reporting validation errors for properties.
    /// </summary>
    public class Validations : INotifyDataErrorInfo
    {
        // Dictionary to store error messages associated with each property name.
        private readonly Dictionary<string, List<string>> _propertyErrors;


        public Validations()
        {
            _propertyErrors = new Dictionary<string, List<string>>();
        }


        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public bool HasErrors => _propertyErrors.Count > 0;



        /// <summary>
        ///   Retrieves the list of error messages for a specific property.
        /// </summary>
        /// <param name="propertyName"> The name of the property to retrieve errors for </param>
        /// <returns> IEnumerable containing error messages for the specified property,or an empty list if no errors exist. </returns>
        public IEnumerable GetErrors(string? propertyName)
        {
            if (propertyName == null) return Enumerable.Empty<string>();
            return _propertyErrors.GetValueOrDefault(propertyName, new List<string>());
        }



        /// <summary>
        ///    Adds an error message to the list for a specific property.
        /// </summary>
        /// <param name="propertyName"> The name of the property to add errors to</param>
        /// <param name="errorMessage"> The error message to add </param>
        public void AddError(string propertyName, string errorMessage)
        {
            if (!_propertyErrors.ContainsKey(propertyName))
            {
                _propertyErrors.Add(propertyName, new List<string>());
            }

            _propertyErrors[propertyName].Add(errorMessage);
            OnErrorsChanged(propertyName);
        }


        /// <summary>
        ///   Clears all error messages for a specific property.
        /// </summary>
        /// <param name="propertyName"> The name of the property to clear errors for </param>
        public void ClearErrors(string propertyName)
        {
            if (_propertyErrors.Remove(propertyName))
            {
                OnErrorsChanged(propertyName);
            }
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}
