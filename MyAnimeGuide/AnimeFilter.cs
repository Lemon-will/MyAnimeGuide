using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAnimeGuide
{
    class AnimeFilter
    {
        ObservableCollection<AnimeData> allAnimes = null;

        public AnimeFilter(ObservableCollection<AnimeData> allAnimes)
        {
            this.allAnimes = allAnimes;
        }

        public ObservableCollection<AnimeData> FilterByDate(AnimeDateTime date)
        {

            ObservableCollection<AnimeData> outputCollection = new ObservableCollection<AnimeData>();
            foreach (AnimeData animeData in allAnimes)
            {
                if (date.StDateTime.Date == animeData.AnimeTime.StDateTime.Date)
                    outputCollection.Add(animeData);
            }
            return outputCollection;
        }

        public ObservableCollection<AnimeData> FilterByCh(List<string> chNameDataList)
        {
            ObservableCollection<AnimeData> outputCollection = new ObservableCollection<AnimeData>();
            foreach (AnimeData animeData in allAnimes)
            {
                foreach (string chName in chNameDataList)
                {
                    if (chName == animeData.ChName)
                    {
                        outputCollection.Add(animeData);
                        break;
                    }
                }
            }
            return outputCollection;
        }

    }
}
