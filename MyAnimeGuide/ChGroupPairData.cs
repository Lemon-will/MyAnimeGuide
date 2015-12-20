using System;
using System.Collections.ObjectModel;

namespace MyAnimeGuide
{
    class ChGroupPairData
    {
        public ChGroupData ChGroupData { get; set; }
        public ObservableCollection<ChData> ChildChDataList { get; set; }
        private Nullable<bool> _isChecked = false;
        public Nullable<bool> IsChecked
        {
            get
            {
                return _isChecked;
            }
            set
            {
                _isChecked = value;
                foreach (ChData chData in ChildChDataList)
                {
                    if (value != null) 
                    {
                        chData.IsChecked = value;
                    }
                }
                System.Console.WriteLine(ChGroupData.ChGroupName + value);
            }
        }

        public ChGroupPairData(ChGroupData _groupdata) {
            ChildChDataList = new ObservableCollection<ChData>();
            ChGroupData = _groupdata;
        }
    }
}
