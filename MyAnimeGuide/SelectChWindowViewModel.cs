using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Xml;

namespace MyAnimeGuide.ViewModel
{
    class SelectChWindowViewModel : INotifyPropertyChanged
    {
        readonly string CONFIG_PATH = @"config.xml";
        public ObservableCollection<string> MyChNameList { get; set; }
        public ObservableCollection<ChData> AllChList { get; set; }
        public Dictionary<string, ChData> AllChHash { get; set; }

        public SelectChWindowViewModel()
        {
            ChXmlData chXmlData = new ChXmlData();

            AllChHash = ChXmlData.AllChData;
            AllChList = new ObservableCollection<ChData>();
            foreach (string key in AllChHash.Keys)
            {
                AllChList.Add(AllChHash[key]);
            }
            MyChNameList = new ObservableCollection<string>();
            InitMyChList();
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

        private void InitMyChList()
        {
            if (!File.Exists(CONFIG_PATH))
            {
                MyChNameList = new ObservableCollection<string>();
            }
            else
            {
                XmlDocument configXmlDoc = new XmlDocument();
                configXmlDoc.Load(CONFIG_PATH);
                XmlNodeList allChNameXmlNodes = configXmlDoc.SelectNodes(@"//myChName");
                foreach(XmlNode chNameNode in allChNameXmlNodes)
                {
                    MyChNameList.Add(chNameNode.InnerText);
                }
            }
        }

        private void saveConfigFile()
        {
            XmlDocument configXmlDoc = new XmlDocument();
            XmlDeclaration declaration = configXmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement configRoot = configXmlDoc.CreateElement("config");

            configXmlDoc.AppendChild(declaration);
            configXmlDoc.AppendChild(configRoot);

            XmlElement myChNamesElement = configXmlDoc.CreateElement("myChNames");
            configRoot.AppendChild(myChNamesElement);

            foreach(string chName in MyChNameList)
            {
                XmlElement myChNameElement = configXmlDoc.CreateElement("myChName");
                myChNameElement.InnerText = chName;
                myChNamesElement.AppendChild(myChNameElement);
            }
            configXmlDoc.Save(CONFIG_PATH);
        }

    }
}
