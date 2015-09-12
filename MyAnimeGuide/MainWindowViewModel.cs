using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MyAnimeGuide.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {

        // 常にすべてのAnimeData情報を保持するリスト
        ObservableCollection<AnimeData> allAnimes = null;
        public ChXmlData ChDataInstance { get; }

        public MainWindowViewModel()
        {
            AnimeXmlData xmlData = new AnimeXmlData();
            allAnimes = xmlData.Animes;
            AnimeListforView = new ObservableCollection<AnimeData>(allAnimes);

            SelectChWindow selWin = new SelectChWindow();
            selWin.ShowDialog();
        }

        // 実際に表示するAnimeDataのリスト
        ObservableCollection<AnimeData> _animes = new ObservableCollection<AnimeData>();
        public ObservableCollection<AnimeData> AnimeListforView
        {
            get { return _animes; }
            set
            {
                _animes = value;
                OnPropertyChanged("AnimeList");
            }
        }

        //override
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
