using System;
using System.ComponentModel;
using System.Xml;

namespace MyAnimeGuide
{
    class ChData : INotifyPropertyChanged
    {
        public string ChID { get; set; }
        public string ChName { get; set; }
        public string ChGroupID { get; set; }
        public string ChGroupName { get; set; }
        Nullable<bool> _isChecked = false;
        public Nullable<bool> IsChecked
        {
            get
            {
                return _isChecked;
            }
            set
            {
                _isChecked = value;
                OnPropertyChanged("IsChecked");
            }
        }

        public ChData(XmlElement chItem)
        {
            this.ChID = chItem.SelectSingleNode(@"ChID").InnerText;
            this.ChName = chItem.SelectSingleNode(@"ChName").InnerText;
            this.ChGroupID = chItem.SelectSingleNode(@"ChGID").InnerText;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

    }
}
