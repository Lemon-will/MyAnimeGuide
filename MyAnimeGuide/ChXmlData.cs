using System.Collections.Generic;
using System.Xml;

namespace MyAnimeGuide
{
    class ChXmlData
    {
        readonly string SYOBO_CH_URL = "http://cal.syoboi.jp/db.php?Command=ChLookup";
        readonly string SYOBO_GROUP_CH_URL = "http://cal.syoboi.jp/db.php?Command=ChGroupLookup";
        readonly string CURRENT_FOLDER_PATH = System.IO.Directory.GetCurrentDirectory();
        readonly string CH_XML_PATH = System.IO.Directory.GetCurrentDirectory() + @"\data\ch_id.xml";
        readonly string CH_GROUP_XML_PATH = System.IO.Directory.GetCurrentDirectory() + @"\data\ch_group_id.xml";

        XmlDocument ChXmlDoc = new XmlDocument();
        XmlDocument ChGroupXmlDoc = new XmlDocument();

        public static List<ChGroupData> AllChGroupData { get; set; }
        public static List<ChData> AllChData { get; set; }

        public ChXmlData()
        {
            System.IO.Directory.SetCurrentDirectory(CURRENT_FOLDER_PATH);
            ChXmlDoc = XmlUtil.ReadSaveXml(CH_XML_PATH, SYOBO_CH_URL, false);
            ChGroupXmlDoc = XmlUtil.ReadSaveXml(CH_GROUP_XML_PATH, SYOBO_GROUP_CH_URL, false);

            XmlElement chRootElement = ChXmlDoc.DocumentElement;
            XmlElement chItemsElement = (XmlElement)chRootElement.LastChild;
            XmlElement chGroupRootElement = ChGroupXmlDoc.DocumentElement;
            XmlElement chGroupItemsElement = (XmlElement)chGroupRootElement.LastChild;


            AllChGroupData = new List<ChGroupData>();
            AllChData = new List<ChData>();


            foreach(XmlElement chGroupItem in chGroupItemsElement.ChildNodes)
            {
                ChGroupData chGroupData = new ChGroupData(chGroupItem);
                AllChGroupData.Add(chGroupData);
            }

            foreach (XmlElement chItem in chItemsElement.ChildNodes)
            {
                ChData chData = new ChData(chItem);
                AllChData.Add(chData);
            }


        }

    }
}
