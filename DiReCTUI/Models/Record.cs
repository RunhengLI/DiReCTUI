using Microsoft.Maps.MapControl.WPF;
using System;
using System.Xml;

namespace DiReCTUI.Models
{
    /// <summary>
    /// A class that parses and stores an existing record
    /// WIP (optional): multithreading
    /// </summary>
    public class Record
    {
        /// <summary>
        /// Record location
        /// </summary>
        public Location Location;

        /// <summary>
        /// Record Time type "yyMMddHHmmss"
        /// </summary>
        public string Time { get; protected set; }

        /// <summary>
        /// Name of the task recorded
        /// </summary>
        public string TaskName { get; protected set; }

        /// <summary>
        /// A XmlNode containing its responses
        /// </summary>
        public XmlNode TaskResponse { get; protected set; }

        /// <summary>
        /// Constructor Method
        /// Parses a XmlNode to obtain its record data
        /// </summary>
        /// <param name="XmlDoc">Xml document of a record file</param>
        public Record(XmlDocument XmlDoc)
        {
            Location=new Location();
            foreach(XmlNode inNode in XmlDoc.FirstChild.ChildNodes)
                switch(inNode.Name)
                {
                case "Latitude":
                    Location.Latitude=Double.Parse(inNode.InnerText,System.Globalization.CultureInfo.InvariantCulture);
                    break;
                case "Longitude":
                    Location.Longitude=Double.Parse(inNode.InnerText,System.Globalization.CultureInfo.InvariantCulture);
                    break;
                case "RecordTime":
                    Time=inNode.InnerText;
                    break;
                default:
                    TaskName=inNode.Name;
                    TaskResponse=inNode;
                    break;
                }
        }
    }
}
