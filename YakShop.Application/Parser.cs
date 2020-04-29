using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using YakShop.Domain;

namespace YakShop.Application
{
    public class Parser
    {
        /// <summary>
        /// Parse an XML string to List<Yak>
        /// </summary>
        /// <param name="inputXml">string of the XML</param>
        /// <returns></returns>
        public List<Yak> Parse(string inputXml)
        {
            var ret = new List<Yak>();

            XmlDocument xDoc = new XmlDocument();
            
            xDoc.Load(new StringReader(inputXml));

            XmlNodeList labyaks = xDoc.GetElementsByTagName("labyak");
            foreach (XmlNode labyak in labyaks)
            {
                ret.Add(new Yak(Convert.ToDouble(labyak.Attributes.GetNamedItem("age").InnerText), labyak.Attributes.GetNamedItem("sex").InnerText.Equals("m") ? Sex.male : Sex.female, labyak.Attributes.GetNamedItem("name").InnerText));
            }
            return ret;
        }

    }
}
