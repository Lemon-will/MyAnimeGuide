using System;
using System.IO;
using System.Net;
using System.Xml;

namespace MyAnimeGuide
{
    class XmlUtil
    {
        /// <summary>
        /// 指定したパスにXMLファイルがない場合、XMLファイルを指定したURLから読み込み、指定したパスに保存するメソッド。
        /// </summary>
        /// <param name="XML_PATH">XMLのパス</param>
        /// <param name="URL_PATH">ダウンロードするURL</param>
        /// <param name="isLatest">現存しているXMLファイルが今日ダウンロードしたものでない場合ときに新たにDLするかどうか</param>
        /// <returns></returns>
        public static XmlDocument ReadSaveXml(string XML_PATH, string URL_PATH, bool isLatest)
        {
            WebRequest webReqObj;
            WebResponse webResObj;
            XmlDocument xmlDocObj = new XmlDocument();

            DateTime nowDateTime = DateTime.Now;
            bool isDownLoad = false;

            if (File.Exists(XML_PATH))
            {
                if (isLatest && File.GetLastWriteTime(XML_PATH).Date.CompareTo(nowDateTime.Date) < 0)
                {
                    isDownLoad = true;
                }
            }

            if (!File.Exists(XML_PATH)) isDownLoad = true;

            if (isDownLoad)
            {
                webReqObj = WebRequest.Create(URL_PATH);
                webResObj = webReqObj.GetResponse();
                xmlDocObj = new XmlDocument();
                xmlDocObj.Load(webResObj.GetResponseStream());
                xmlDocObj.Save(XML_PATH);
            }
            else
            {
                xmlDocObj.Load(XML_PATH);
            }
            return xmlDocObj;
        }
    }
}
