using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Xml;

namespace MyAnimeGuide.ViewModel
{
    class SelectChWindowViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ChGroupPairData> AllChGroupPairs { get; set; }
        //View側に結びつく画面を閉じるためのプロパティ
        public Action CloseAction { get; set; }

        public SelectChWindowViewModel()
        {
            //プロパティの初期化
            ChXmlData chXmlData = new ChXmlData();
            AllChGroupPairs = new ObservableCollection<ChGroupPairData>();
            MainWindowViewModel.MyChNameList = new List<string>();

            InitMyChList();
            InitAllChGroupPairs();
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
            if (!File.Exists(Path.CONFIG_PATH))
            {
                MessageBox.Show("設定ファイル(config.xml)が存在しません。新たに設定しなおしてください。", "MyAnimeGuide Information", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindowViewModel.MyChNameList = new List<string>();
                SaveConfigFile();
            }
            else
            {
                XmlDocument configXmlDoc = new XmlDocument();
                configXmlDoc.Load(Path.CONFIG_PATH);
                XmlNodeList allChNameXmlNodes = configXmlDoc.SelectNodes(@"//myChName");
                if (allChNameXmlNodes != null)
                    foreach (XmlNode chNameNode in allChNameXmlNodes)
                    {
                        MainWindowViewModel.MyChNameList.Add(chNameNode.InnerText);
                    }
            }
        }

        private void InitAllChGroupPairs()
        {
            //子ChDataリストを初期化済みのChGroupPairDataをChGroupの数だけ用意
            foreach (ChGroupData chGroupData in ChXmlData.AllChGroupData)
            {
                if (chGroupData.ChGroupName != "")  //ChGroupID:23が空のグループを作っているので除外
                {
                    AllChGroupPairs.Add(new ChGroupPairData(chGroupData));
                }
            }
            //ChDataをすべて該当のグループのChGroupPairDataの子リストに代入
            foreach (ChData chData in ChXmlData.AllChData)
            {
                foreach (ChGroupPairData pairData in AllChGroupPairs)
                {
                    if (chData.ChGroupID == pairData.ChGroupData.ChGroupID)
                    {
                        chData.ChGroupName = pairData.ChGroupData.ChGroupName;
                        //チェックボックスの初期化(Ischecked)
                        foreach (String myChName in MainWindowViewModel.MyChNameList)
                        {
                            if (myChName == chData.ChName)
                            {
                                chData.IsChecked = true;
                                if (pairData.IsChecked == false)
                                    pairData.IsChecked = null;
                            }
                        }
                        pairData.ChildChDataList.Add(chData);
                    }
                }
            }
        }


        private void SaveConfigFile()
        {
            XmlDocument configXmlDoc = new XmlDocument();
            XmlDeclaration declaration = configXmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement configRoot = configXmlDoc.CreateElement("config");

            configXmlDoc.AppendChild(declaration);
            configXmlDoc.AppendChild(configRoot);

            XmlElement myChNamesElement = configXmlDoc.CreateElement("myChNames");
            configRoot.AppendChild(myChNamesElement);

            foreach (string chName in MainWindowViewModel.MyChNameList)
            {
                XmlElement myChNameElement = configXmlDoc.CreateElement("myChName");
                myChNameElement.InnerText = chName;
                myChNamesElement.AppendChild(myChNameElement);
            }
            configXmlDoc.Save(Path.CONFIG_PATH);
        }

        private void ExecuteRegisterCommand()
        {
            MainWindowViewModel.MyChNameList = new List<string>();
            foreach (ChGroupPairData pairData in AllChGroupPairs)
            {
                foreach (ChData chData in pairData.ChildChDataList)
                {
                    if (chData.IsChecked == true)
                    {
                        MainWindowViewModel.MyChNameList.Add(chData.ChName);
                    }
                }
            }
            SaveConfigFile();
            CloseAction();
        }

        //以下Command関係
        RelayCommand _registerCommand;
        public ICommand RegisterCommand
        {
            get
            {
                if (_registerCommand == null)
                {
                    _registerCommand = new RelayCommand(param => this.ExecuteRegisterCommand());
                }
                return _registerCommand;
            }
        }
    }
}
