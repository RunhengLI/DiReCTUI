using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Maps.MapControl.WPF;

namespace DiReCTUI.Models
{
    /// <summary>
    /// A class that parses and stores a location and tasks at this location
    /// </summary>
    public class LocationTask
    {
        /// <summary>
        /// Location
        /// </summary>
        public Location Location { get; protected set; }

        /// <summary>
        /// List of tasks at this location
        /// </summary>
        public List<string> TaskList { get; protected set; }

        /// <summary>
        /// Constructor method
        /// Parses a XmlNode to obtain data of the location and its tasks
        /// </summary>
        /// <param name="locationTaskNode">XmlNode containing the location and its tasks</param>
        public LocationTask(XmlNode locationTaskNode)
        {
            Location=new Location();
            TaskList=new List<string>();

             foreach(XmlNode inNode in locationTaskNode.ChildNodes)
                switch(inNode.Name)
                {
                    case "Latitude":
                        Location.Latitude=(Double.Parse(inNode.InnerText,System.Globalization.CultureInfo.InvariantCulture));
                        break;
                    case "Longitude":
                        Location.Longitude=(Double.Parse(inNode.InnerText,System.Globalization.CultureInfo.InvariantCulture));
                        break;
                    case "Task":
                        TaskList.Add(inNode.InnerText);
                        break;
                }
        }
    }

    /// <summary>
    /// A class that parses and stores a SOP SubTask (e.g. "Rock Type")
    /// WIP: Add more possible types
    /// </summary>
    public class SubTask
    {
        /// <summary>
        /// Name of the SubTask
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Type of response
        /// </summary>
        public string Type { get; protected set; }

        /// Default value
        /// There may be multiple values for type "enum", in this case the first value will be used as default
        /// </summary>
        public List<string> defaultList { get; protected set; }

        /// <summary>
        /// Constructor method
        /// Parses a XmlNode to obtain the SubTask data
        /// </summary>
        /// <param name="subTaskNode">XmlNode containing the SubTask</param>
        public SubTask(XmlNode subTaskNode)
        {
            defaultList=new List<string>();

            Name=subTaskNode.Name;
            Type=subTaskNode.Attributes[0].Value;
            switch(Type)
            {
            case "enum":
                foreach(XmlNode inNode in subTaskNode.ChildNodes)
                    defaultList.Add(inNode.InnerText);
                break;
            default:
                defaultList.Add(subTaskNode.InnerText);
                break;
            }
        }
    }

    /// <summary>
    /// A class that parses and stores a SOP task (e.g. "Rock")
    /// </summary>
    class SOPTask
    {
        /// <summary>
        /// Name of the task
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// List of its SubTasks
        /// </summary>
        public List<SubTask> SubTaskList { get; protected set; }

        /// <summary>
        /// Constructor method
        /// Parses a XmlNode to obtain the Task data
        /// </summary>
        /// <param name="taskNode">XmlNode containing the Task</param>
        public SOPTask(XmlNode taskNode)
        {
            SubTaskList=new List<SubTask>();
            Name=taskNode.Name;
            foreach(XmlNode inNode in taskNode.ChildNodes)
                SubTaskList.Add(new SubTask(inNode));
        }
    }

    /// <summary>
    /// A class that parses and stores a SOP (e.g. "DebrisFlow")
    /// WIP (optional): multithreading
    /// </summary>
    public class SOP
    {
        /// <summary>
        /// List of its LocationTasks
        /// </summary>
        public List<LocationTask> LocationTaskList { get; protected set; }

        /// <summary>
        /// List of its Tasks
        /// </summary>
        public List<XmlNode> TaskNodeList { get; protected set; }

        /// <summary>
        /// Name of SOP
        /// </summary>
        public string Name { get; protected set; }
        
        /// <summary>
        /// Constructor method
        /// Parses a XmlNode to obtain SOP data
        /// </summary>
        /// <param name="SOPXml">XmlNode containing the SOP</param>
        public SOP(XmlNode SOPXml)
        {
            LocationTaskList=new List<LocationTask>();
            TaskNodeList=new List<XmlNode>();

            Name=SOPXml.Name;
            foreach(XmlNode inNode in SOPXml.ChildNodes)
                switch(inNode.Name)
                {
                    case "Location":
                        LocationTaskList.Add(new LocationTask(inNode));
                        break;
                    default:
                        TaskNodeList.Add(inNode);
                        break;
                }
        }
    }
}
