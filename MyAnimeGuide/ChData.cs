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
        bool _isChecked;
        public bool IsChecked
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
            ChID = chItem.SelectSingleNode(@"ChID").InnerText;
            ChName = chItem.SelectSingleNode(@"ChName").InnerText;
            ChGroupID = chItem.SelectSingleNode(@"ChGID").InnerText;
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
