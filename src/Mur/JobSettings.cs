using System;
using System.Xml.Serialization;

namespace Mur
{
    [Serializable]
    public class JobSettings
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }

        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }

        public Schedule[] Schedules { get; set; }
    }
}
