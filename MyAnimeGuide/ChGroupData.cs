using System;
using System.ComponentModel;
using System.Xml;

namespace MyAnimeGuide
{
    class ChGroupData
    {
        public string ChGroupName { get; set; }
        public string ChGroupID { get; set; }

        public ChGroupData(XmlElement chGroupItem)
        {
            ChGroupName = chGroupItem.SelectSingleNode(@"ChGroupName").InnerText;
            ChGroupID = chGroupItem.SelectSingleNode(@"ChGID").InnerText;
        }
    }
}
