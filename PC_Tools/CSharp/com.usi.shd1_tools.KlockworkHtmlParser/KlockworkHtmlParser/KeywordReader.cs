using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace com.usi.shd1_tools.KlockworkHtmlParser
{
    class KeywordReader
    {
        public readonly String KeywordConfigPath="";
        
        public List<KeywordCollection> KeywordCollections
        {
            get{
                return keywordCollections;
            }
        }
        private List<KeywordCollection> keywordCollections;

        public KeywordReader(String keywordConfigPath)
        {
            KeywordConfigPath = keywordConfigPath;
            keywordCollections = readConfig();
        }

        public void RefreshKeywordConfiguration()
        {
            keywordCollections = readConfig();
        }

        private List<KeywordCollection> readConfig()
        {
            List<KeywordCollection> collections = new List<KeywordCollection>();
            try
            {
                XElement xeRoot = XElement.Load(KeywordConfigPath);//.Element("root");
                KeywordCollection keyCol = null;
                foreach (XElement xeCategory in xeRoot.Elements("Category"))
                {
                    try
                    {
                        String name = xeCategory.Attribute("name").Value;
                        keyCol = new KeywordCollection(name);
                        bool enable = true;
                        try
                        {
                            enable = Convert.ToBoolean(xeCategory.Attribute("enable").Value);
                        }
                        catch
                        {
                            // Read enable attribute fail, default = enable
                            enable = true;
                        }
                        keyCol.Enable = enable;
                        foreach (XElement xeKeyword in xeCategory.Elements("Keyword"))
                        {
                            Keyword keyword = new Keyword(xeKeyword.Value);
                            if (xeKeyword.Attribute("remark") != null)
                            {
                                keyword.Remark = xeKeyword.Attribute("remark").Value;
                            }
                            keyCol.Keys.Add(keyword);
                        }
                        collections.Add(keyCol);
                    }
                    catch
                    {
                        //read Category config fail, skip this one and go to nexe.
                    }
                }
            }
            catch
            {
                //Config read fail, do nothing
            }
            return collections;
        }
    }

    public class KeywordCollection
    {
        public readonly String Category = "";
        public bool Enable = true;
        public List<Keyword> Keys = new List<Keyword>();
        public KeywordCollection(String category)
        {
            Category = category;
        }
    }
    public class Keyword
    {
        public String Remark = "";
        public readonly String Value = "";
        public Keyword(String _value)
        {
            Value = _value;
        }
    }
}
