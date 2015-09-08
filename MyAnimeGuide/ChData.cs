using System.Xml;

namespace MyAnimeGuide
{
    class ChData
    {
        public string ChID { get; set; }
        public string ChName { get; set; }
        public string ChGroupID { get; set; }
        public string ChGroupName { get; set; }

        public ChData(XmlElement chItem) 
        {
            ChID = chItem.SelectSingleNode(@"ChID").InnerText;
            ChName = chItem.SelectSingleNode(@"ChName").InnerText;
            ChGroupID = chItem.SelectSingleNode(@"ChGID").InnerText;
            ChGroupName = ChXmlData.GroupID_NameHash[ChGroupID];
        }
    }
}
