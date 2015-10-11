using System.Xml;

namespace MyAnimeGuide
{
    class AnimeData
    {
        public string PID { get; set; }
        public string TID { get; set; }
        public string StTime { get; set; }
        public string EdTime { get; set; }
        public AnimeDateTime AnimeTime { get; set; }
        public string ChName { get; set; }
        public string ChID { get; set; }
        public string Count { get; set; }
        public string SubTitle { get; set; }
        public string Title { get; set; }

        public AnimeData(XmlElement xmlElement) {
            this.PID = xmlElement.Attributes["PID"].Value;
            this.TID = xmlElement.Attributes["TID"].Value;
            this.StTime = xmlElement.Attributes["StTime"].Value;
            this.EdTime = xmlElement.Attributes["EdTime"].Value;
            this.ChName = xmlElement.Attributes["ChName"].Value;
            this.ChID = xmlElement.Attributes["ChID"].Value;
            this.Count = xmlElement.Attributes["Count"].Value;
            this.SubTitle = xmlElement.Attributes["SubTitle"].Value;
            this.Title = xmlElement.Attributes["Title"].Value;
            this.AnimeTime = new AnimeDateTime(this.StTime, this.EdTime);
        }



    }
}
