using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CPDataxtendCommands
{
    [XmlRoot("Command")]
    public class Command
    {
        [XmlElement(ElementName ="MySiteID")]
        public string MySiteID;

        [XmlElement(ElementName = "MySiteName")]
        public string MySiteName;

        [XmlElement(ElementName = "MainSiteID")]
        public string MainSiteID;
        [XmlElement(ElementName = "SecondSiteID")]
        public string SecondSiteID;

    }
}
