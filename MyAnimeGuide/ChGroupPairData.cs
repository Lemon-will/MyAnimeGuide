using System.Collections.ObjectModel;

namespace MyAnimeGuide
{
    class ChGroupPairData
    {
        public ChGroupData ChGroupData { get; set; }
        public ObservableCollection<ChData> ChildChDataList { get; set; }

        public ChGroupPairData(ChGroupData _groupdata) {
            ChildChDataList = new ObservableCollection<ChData>();
            ChGroupData = _groupdata;
        }
    }
}
