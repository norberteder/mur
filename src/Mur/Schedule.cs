using System;
using System.Xml.Serialization;

namespace Mur
{
    [Serializable]
    public class Schedule
    {
        [XmlAttribute(AttributeName = "start")]
        public DateTime Start { get; set; }

        [XmlAttribute(AttributeName = "interval")]
        public string Interval { get; set; }
    }
}
