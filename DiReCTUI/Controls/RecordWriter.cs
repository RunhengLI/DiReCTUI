using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Maps.MapControl.WPF;
using DiReCTUI.Models;

namespace DiReCTUI.Controls
{
    /// <summary>
    /// A class that writes responses to a SOP into a Xml file
    /// </summary>
    class RecordWriter
    {
        /// <summary>
        /// Constructor method
        /// Creates a Xml file with the obtained data for future use 
        /// WIP: validation of data (probably according to its SOPtask)
        /// WIP (optional): multithreading
        /// </summary>
        /// <param name="SOPName">Name of the SOP</param>
        /// <param name="task">Task on which the record is based</param>
        /// <param name="responseList">List of responses to all questions of the task</param>
        /// <param name="location">Recording location</param>
        public RecordWriter(string SOPName,SOPTask task,List<string> responseList,Location location)
        {

            ///Get current time, type "yyyyMMddHHmmss"
            string time=DateTime.Now.ToString(("yyyyMMddHHmmss"));

            System.IO.Directory.CreateDirectory("Documents/Records/"+SOPName);
            XmlTextWriter writer=new XmlTextWriter("Documents/Records/"+SOPName+"/"+time+".xml",null);

            writer.WriteStartElement(SOPName);
            writer.WriteElementString("Latitude",location.Latitude.ToString(System.Globalization.CultureInfo.InvariantCulture));
            writer.WriteElementString("Longitude",location.Longitude.ToString(System.Globalization.CultureInfo.InvariantCulture));
                writer.WriteElementString("RecordTime",time);
                writer.WriteStartElement(task.Name);
                    for(int i=0;i<responseList.Count;i++)
                    {
                        ///In order to shorten the Xml file, values equal to default will not be written into the Xml
                        if(responseList[i]!=task.SubTaskList[i].defaultList[0])
                            writer.WriteElementString(task.SubTaskList[i].Name,responseList[i]);
                    }
                writer.WriteEndElement();
            writer.WriteEndElement();

            writer.Close();
        }
    }
}
