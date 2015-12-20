using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Windows.Documents;
using System.Windows.Input;

namespace MyAnimeGuide.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {

        // 常にすべてのAnimeData情報を保持するリスト
        public static ObservableCollection<AnimeData> allAnimes = null;
        AnimeFilter animeFilter = null;
        public ChXmlData ChDataInstance { get; set; }
        public static List<string> MyChNameList { get; set; }

        public MainWindowViewModel()
        {
            AnimeXmlData xmlData = new AnimeXmlData();
            allAnimes = xmlData.Animes;
            AnimeListforView = new ObservableCollection<AnimeData>(allAnimes);
            MyChNameList = new List<string>();
            animeFilter = new AnimeFilter(allAnimes);

            if (!File.Exists(Path.CONFIG_PATH))
            {
                SelectChWindow selWin = new SelectChWindow();
                selWin.ShowDialog();
            }
            LoadMyChName();
            AnimeListforView = animeFilter.FilterByCh(MyChNameList);
        }

        // 実際に表示するAnimeDataのリスト
        ObservableCollection<AnimeData> _animes = new ObservableCollection<AnimeData>();
        public ObservableCollection<AnimeData> AnimeListforView
        {
            get { return _animes; }
            set
            {
                _animes = value;
                OnPropertyChanged("AnimeListforView");
            }
        }

        /// <summary>
        /// config.xmlからChNameを読み込みます
        /// </summary>
        private void LoadMyChName()
        {
            XmlDocument configXmlDoc = new XmlDocument();
            configXmlDoc.Load(Path.CONFIG_PATH);
            XmlNodeList allChNameXmlNodes = configXmlDoc.SelectNodes(@"//myChName");
            if (allChNameXmlNodes != null)
                foreach (XmlNode chNameNode in allChNameXmlNodes)
                {
                    MyChNameList.Add(chNameNode.InnerText);
                }
        }

        private void ExecuteSelectChCommand()
        {
            SelectChWindow selWin = new SelectChWindow();
            selWin.ShowDialog();
            AnimeListforView = animeFilter.FilterByCh(MyChNameList);
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

        //RelayCommand
        RelayCommand _selectChCommand;
        public ICommand SelectChCommand
        {
            get
            {
                if (_selectChCommand == null)
                {
                    _selectChCommand = new RelayCommand(param => this.ExecuteSelectChCommand());
                }
                return _selectChCommand;
            }
        }


    }
}
