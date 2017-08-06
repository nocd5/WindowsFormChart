using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Common
{
    public class ViewModelBase : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        protected void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Dictionary<string, string> ErrorMessages = new Dictionary<string, string>();

        protected void SetError(string errorMessage, [CallerMemberName]string propertyName = "")
        {
            ErrorMessages[propertyName] = errorMessage;
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        protected void ClearError([CallerMemberName]string propertyName = "")
        {
            if (ErrorMessages.ContainsKey(propertyName))
            {
                ErrorMessages.Remove(propertyName);
            }
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public bool FindError([CallerMemberName]string propertyName = "") => ErrorMessages.ContainsKey(propertyName ?? "");

        bool INotifyDataErrorInfo.HasErrors => ErrorMessages.Count != 0;

        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) ||
                !ErrorMessages.ContainsKey(propertyName))
            {
                return null;
            }

            return ErrorMessages[propertyName];
        }
    }
}
