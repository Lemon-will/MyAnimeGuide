using System.Collections.ObjectModel;
using System.Xml;

namespace MyAnimeGuide
{
    class AnimeXmlData
    {
        readonly string SHOBO_URL = "http://cal.syoboi.jp/cal_chk.php?days=7";
        readonly string XML_PATH = "data/ch.xml";
        XmlDocument xmlDocObj = new XmlDocument();
        ObservableCollection<AnimeData> _animes = new ObservableCollection<AnimeData>();

        public XmlDocument XmlDocObj { get; set; }
        public ObservableCollection<AnimeData> Animes
        {
            get
            {
                return _animes;
            }
            set
            {
                _animes = value;
            }
        }

        public AnimeXmlData()
        {
            XmlDocObj = XmlUtil.ReadSaveXml(XML_PATH, SHOBO_URL, true);
            AnimeXmlToCollection();
        }

        /// <summary>
        /// XMLデータをコレクションanimesに突っ込む
        /// </summary>
        void AnimeXmlToCollection()
        {
            _animes.Clear();
            XmlElement rootElement = XmlDocObj.DocumentElement;
            XmlElement progItemsElement = (XmlElement)rootElement.FirstChild;
            foreach (XmlElement progItem in progItemsElement.ChildNodes)
            {
                AnimeData anime = new AnimeData(progItem);
                _animes.Add(anime);
            }
        }
    }
}
