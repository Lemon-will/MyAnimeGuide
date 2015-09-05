using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MyAnimeGuide
{
    class XmlData
    {
        readonly string SHOBO_URL = "http://cal.syoboi.jp/cal_chk.php?days=1";
        readonly string XML_PATH = "ch.xml";
        WebRequest webReqObj;
        WebResponse webResObj;
        XmlDocument xmlDocObj = new XmlDocument();
        ObservableCollection<AnimeData> animes = new ObservableCollection<AnimeData>();

        public XmlDocument XmlDocObj { get; }
        public ObservableCollection<AnimeData> Animes
        {
            get
            {
                return animes;
            }
            set
            {
                animes = value;
            }
        }

        public XmlData()
        {
            LoadXml();
        }

        void LoadXml()
        {
            DateTime nowDateTime = DateTime.Now;
            // ファイルがないか、現存しているXMLファイルが今日ダウンロードしたものでない場合新たにDLする
            if (!File.Exists(XML_PATH) || File.GetLastWriteTime(XML_PATH).Date.CompareTo(nowDateTime.Date) < 0)
            {
                webReqObj = WebRequest.Create(SHOBO_URL);
                webResObj = webReqObj.GetResponse();
                xmlDocObj = new XmlDocument();
                xmlDocObj.Load(webResObj.GetResponseStream());
                xmlDocObj.Save(XML_PATH);
            }
            else
            {
                xmlDocObj.Load(XML_PATH);
            }
            XmlToCollection();
        }

        /// <summary>
        /// XMLデータをコレクションanimesに突っ込む
        /// </summary>
        void XmlToCollection()
        {
            animes.Clear();
            XmlElement rootElement = xmlDocObj.DocumentElement;
            XmlElement progItemsElement = (XmlElement)rootElement.FirstChild;
            foreach (XmlElement progItem in progItemsElement.ChildNodes)
            {
                AnimeData anime = new AnimeData(progItem);
                animes.Add(anime);
            }
        }
    }
}
