﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MyAnimeGuide.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {


        public MainWindowViewModel()
        {
            XmlData xmlData = new XmlData();
            AnimeListforView = xmlData.Animes;
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



    }
}
