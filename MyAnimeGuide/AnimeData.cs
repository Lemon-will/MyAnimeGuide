using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAnimeGuide
{
    class AnimeData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// イベントの通知
        /// </summary>
        /// <param name="propertyName">変化したプロパティ名</param>
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public AnimeData()
        {

        }

        public string PID { get; set; }
        public string TID { get; set; }
        public string StTime { get; set; }
        public string EdTime { get; set; }
        public string ChName { get; set; }
        public string ChID { get; set; }
        public string Count { get; set; }
        public string SubTitle { get; set; }
        public string Title { get; set; }

    }
}
