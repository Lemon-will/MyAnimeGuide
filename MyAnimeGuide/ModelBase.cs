using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.TeamFoundation.MVVM;
namespace MyAnimeGuide
{
    class ModelBase : ViewModelBase, IDataErrorInfo
    {
        private readonly Dictionary<string, string> _errors = new Dictionary<string, string>();

        public string this[string proprtyName]
        {
            get
            {
                return _errors.ContainsKey(proprtyName) ? _errors[proprtyName] : "";
            }
        }

        public string Error
        {
            get
            {
                return _errors.Count > 0 ? "項目の値が不正です．" : "";
            }
        }

        protected void UpdateErrors(string propertyName, string errorMessage)
        {
            if (string.IsNullOrEmpty(errorMessage))
            {
                _errors.Remove(propertyName);
            }
            else
            {
                _errors[propertyName] = errorMessage;
            }
        }
    }
}
