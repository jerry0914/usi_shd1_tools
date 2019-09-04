using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using System.Threading;
using com.usi.shd1_tools.CommonLibrary;
using System.Text.RegularExpressions;
using System.Net;

namespace com.usi.shd1_tools.KlockworkHtmlParser
{
    public class KlockworkHtmlProcessor
    {
        public List<KeyValuePair<String, List<KlockworkParsedMessage>>> AllParsedMessagesList
        {
            get
            {
                List<KeyValuePair<String, List<KlockworkParsedMessage>>> lst = _doNotUseParsedMessageDictionary.ToList<KeyValuePair<String, List<KlockworkParsedMessage>>>();
                return lst;
            }
        }
        private Dictionary<String, List<KlockworkParsedMessage>> _doNotUseParsedMessageDictionary = new Dictionary<String, List<KlockworkParsedMessage>>();
        private List<KlockworkParsedMessage> getClockParsedMessages(String key)
        {
            List<KlockworkParsedMessage> lstMessages = new List<KlockworkParsedMessage>();
            if (_doNotUseParsedMessageDictionary.ContainsKey(key))
            {
                lstMessages = _doNotUseParsedMessageDictionary[key];
            }
            return lstMessages;
        }
        private void addKlockworkParsedMessage(String key, KlockworkParsedMessage message)
        {
            List<KlockworkParsedMessage> lstMessages = null;
            if (_doNotUseParsedMessageDictionary.ContainsKey(key))
            {
                lstMessages = _doNotUseParsedMessageDictionary[key];
                lstMessages.Add(message);
                _doNotUseParsedMessageDictionary[key] = lstMessages;
            }
            else
            {
                lstMessages = new List<KlockworkParsedMessage>();
                lstMessages.Add(message);
                _doNotUseParsedMessageDictionary.Add(key, lstMessages);
            }

        }
        private KlockworkHtmlProcessor me = null;
        private HtmlDocument htmlDoc = null;
        private String htmlFilePath = "";
        public String HtmlFilePath
        {
            get
            {
                return htmlFilePath;
            }
        }
        public EventHandler<UpdateProcessingProgessEventArgs> UpdateProcessingProgessEventHandler;
        private bool runFlag = false;
        public KlockworkHtmlProcessor(String htmlPath)
        {
            htmlFilePath = htmlPath;
        }

        public void Stop()
        {
            runFlag = false;
        }

        private void clear()
        {
            htmlDoc = null;
            _doNotUseParsedMessageDictionary.Clear();
        }

        public void Start(List<KeywordCollection> keyCollections)
        {
            HtmlWeb client = new HtmlWeb();
            clear();
            try
            {
                htmlDoc = client.Load(htmlFilePath);
                long count = (htmlDoc.DocumentNode.SelectSingleNode("/html[1]/body[1]/table[1]/tbody[1]")).ChildNodes.Count;
                HtmlNodeCollection nodes= htmlDoc.DocumentNode.SelectNodes("/html[1]/body[1]/table[1]/tbody[1]/tr");
                if (htmlDoc != null && keyCollections != null)
                {
                    runFlag = true;
                    filterByKeywords(keyCollections);
                }
                //htmlDoc = null;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void filterByKeywords(object param)
        {
            try
            {
                List<KeywordCollection> keyCols = param as List<KeywordCollection>;
                HtmlNodeCollection trNodeCollection = htmlDoc.DocumentNode.SelectNodes("/html[1]/body[1]/table[1]/tbody[1]/tr");
                int counter = 0;
                int max = trNodeCollection.Count;
                while (counter < max && runFlag)
                {
                    HtmlNode trNode = trNodeCollection[counter];
                    HtmlNode tdNode = trNode.SelectSingleNode("td");
                    HtmlNodeCollection divNodes = tdNode.SelectNodes("div");
                    int index = -1;
                    String header = "";
                    String pathAndLocation = "";
                    String description = "";
                    if (divNodes.Count == 3)
                    {
                        Regex regHeader = new Regex(@"#(?<index>\d+):\s*(?<header>(\S|\s)+)");
                        Match m = regHeader.Match(divNodes[0].InnerText);
                        if (m.Success)
                        {
                            index = Convert.ToInt32(m.Groups["index"].Value);
                            header = WebUtility.HtmlDecode(m.Groups["header"].Value);
                        }
                        else
                        {
                            header = WebUtility.HtmlDecode(divNodes[0].InnerText);
                        }
                        pathAndLocation = divNodes[1].InnerText;
                        description = WebUtility.HtmlDecode(divNodes[2].InnerText);
                    }
                    addKlockworkParsedMessage("ALL", new KlockworkParsedMessage(index,header, pathAndLocation, description));
                    foreach (KeywordCollection keyCol in keyCols)
                    {
                        if (keyCol.Enable)
                        {
                            foreach (Keyword keyword in keyCol.Keys)
                            {
                                if (header.ToLower().Contains(keyword.Value) || pathAndLocation.ToLower().Contains(keyword.Value) || description.ToLower().Contains(keyword.Value))
                                {
                                    addKlockworkParsedMessage(keyCol.Category, new KlockworkParsedMessage(index,header, pathAndLocation, description));
                                    break;
                                }
                            }
                        }
                    }
                    counter++;
                    if (UpdateProcessingProgessEventHandler != null)
                    {
                        UpdateProcessingProgessEventHandler.Invoke(this,new UpdateProcessingProgessEventArgs(htmlFilePath,max,counter));
                    }
                    tdNode = null;
                    divNodes = null;
                }
                trNodeCollection = null;
            }
            catch (ThreadInterruptedException tie)
            {
                //Do nothing
            }
        }
    }

    public class UpdateProcessingProgessEventArgs : EventArgs
    {
        public readonly String ProcessingItme = "";
        public readonly int Maximum = -1;
        public readonly int Progress = -1;
        public UpdateProcessingProgessEventArgs(String processingItem, int maximum, int progress)
        {
            ProcessingItme = processingItem;
            Maximum = maximum;
            Progress = progress;
        }
    }

    public class KlockworkParsedMessage
    {
        public readonly int Index =-1;
        public readonly String Header = "";
        public readonly String PathAndLocation = "";
        public readonly String ErrorDescription="";
        public KlockworkParsedMessage(int index,String header,String pathAndLocation, String errorDescription)
        //public KlockworkParsedMessage(String header,String pathAndLocation, String errorDescription)
        {
            Index = index;
            Header = header;
            PathAndLocation = pathAndLocation;
            ErrorDescription = errorDescription;
        }
    }    
}
