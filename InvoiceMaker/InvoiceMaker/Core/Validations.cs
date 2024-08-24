using System;
using System.Collections;
using System.ComponentModel;

namespace InvoiceMaker.Core
{
    /// <summary>
    ///    Provides functionality for managing and reporting validation errors for properties.
    /// </summary>
    public class Validations : INotifyDataErrorInfo
    {
        // Dictionary to store error messages associated with each property name.
        private readonly Dictionary<string, List<string>> propertyErrors;


        public Validations()
        {
            propertyErrors = new Dictionary<string, List<string>>();
        }


        // Occurs when the errors for a property change.
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;


        //  Gets a value indicating whether there are any errors present.
        public bool HasErrors => propertyErrors.Count > 0;



        /// <summary>
        ///   Retrieves the list of error messages for a specific property.
        /// </summary>
        /// <param name="propertyName"> The name of the property </param>
        /// <returns> An enumerable collection of error messages for the specified property </returns>
        public IEnumerable GetErrors(string? propertyName)
        {
            if (propertyName == null) return Enumerable.Empty<string>();

            return propertyErrors.GetValueOrDefault(propertyName, new List<string>());
        }


        /// <summary>
        ///   Adds an error message to the list of errors for a specific property.
        /// </summary>
        /// <param name="propertyName"> The name of the property </param>
        /// <param name="errorMessage"> The error message to add </param>
        public void AddError(string propertyName, string errorMessage)
        {
            if (!propertyErrors.ContainsKey(propertyName))
            {
                propertyErrors.Add(propertyName, new List<string>());
            }

            propertyErrors[propertyName].Add(errorMessage);
            OnErrorsChanged(propertyName);
        }



        // Clears all error messages for a specific property.
        public void ClearErrors(string propertyName)
        {
            if (propertyErrors.Remove(propertyName))
            {
                OnErrorsChanged(propertyName);
            }
        }

        //  Raises the ErrorsChanged event.
        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

    }
}
