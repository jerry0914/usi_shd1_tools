using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;

namespace ExtensionMethods
{
    public static class XElement_Extension
    {
        public static XElement ElementByAttribute(this XElement Root, String Attribute, String Value)
        {
            return Root.ElementByAttribute("", Attribute, Value);
        }

        public static XElement ElementByAttribute(this XElement Root, String ElementName, String Attribute, String Value)
        {
            XElement xeReturn = null;
            if (ElementName != null && ElementName.Length > 0)
            {
                IEnumerable<XElement> elements =
                                    from element in Root.Elements(ElementName)
                                    where element.Attribute(Attribute).Value.Equals(Value)
                                    select element;
                if (elements.Count() > 0)
                {
                    xeReturn = elements.ElementAt(0);
                }
            }
            else
            {
                IEnumerable<XElement> elements =
                                   from element in Root.Elements()
                                   where element.Attribute(Attribute).Value.Equals(Value)
                                   select element;
                if (elements.Count() > 0)
                {
                    xeReturn = elements.ElementAt(0);
                }
            }
            return xeReturn;
        }
    }
}
