using System.Xml.Serialization;

namespace CPDataxtendScheduler
{
    [XmlRoot("Options")]
    public class Options
    {
        [XmlElement("DefaultSites2Replicate")]
        public int DefaultSites2Replicate = 0;

        [XmlElement("DefaultReplicateDetails")]
        public bool DefaultReplicateDetails = false;

        public Options() { }
    }
}
