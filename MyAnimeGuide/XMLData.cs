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
    class XMLData
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

        public XMLData()
        {
            LoadXml();
        }

        void LoadXml()
        {
            if (!File.Exists(XML_PATH))
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

        void XmlToCollection()
        {
            animes.Clear();
            XmlElement rootElement = xmlDocObj.DocumentElement;
            XmlElement progItemsElement = (XmlElement)rootElement.FirstChild;
            foreach (XmlElement progItem in progItemsElement.ChildNodes)
            {
                AnimeData anime = new AnimeData();
                anime = MakeAnimeData(progItem);
                animes.Add(anime);
                Console.WriteLine(anime.Title);
            }
        }

        AnimeData MakeAnimeData(XmlElement e)
        {
            AnimeData data = new AnimeData();

            data.PID = e.Attributes["PID"].Value;
            data.TID = e.Attributes["TID"].Value;
            data.StTime = e.Attributes["StTime"].Value;
            data.EdTime = e.Attributes["EdTime"].Value;
            data.ChName = e.Attributes["ChName"].Value;
            data.ChID = e.Attributes["ChID"].Value;
            data.Count = e.Attributes["Count"].Value;
            data.SubTitle = e.Attributes["SubTitle"].Value;
            data.Title = e.Attributes["Title"].Value;

            return data;
        }
    }
}
