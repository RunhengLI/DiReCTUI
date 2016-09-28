using System;
using System.Collections.Generic;
using System.Xml;

namespace DiReCTUI.Controls
{
    /// <summary>
    /// A class that reads a list of SOP names and stores related SOP files 
    /// </summary>
    public class SOPReader
    {
        /// <summary>
        /// List of all SOP names
        /// </summary>
        public List<String> SOPNameList { get; protected set; }

        /// <summary>
        /// Dictionary of SOP names and their Xml files
        /// </summary>
        public Dictionary<String,XmlDocument> SOPXmlList { get; protected set; }

        /// <summary>
        /// Constructor method
        /// Reads a list of SOP names and parses related SOP files 
        /// </summary>
        public SOPReader()
        {
            SOPNameList=new List<string>();
            SOPXmlList=new Dictionary<String,XmlDocument>();
            SOPLoad();
        }

        /// <summary>
        /// Reads SOP names from the list, then attempts to read and store all related SOP files
        /// WIP (optional): multithreading
        /// </summary>
        private void SOPLoad()
        {
            try
            {
                XmlDocument xmlDoc=new XmlDocument();
                xmlDoc.Load("Documents/SOPName.xml");
                System.Xml.XmlNode inNode=xmlDoc.DocumentElement.FirstChild;
                while(inNode!=null)
                {
                    SOPNameList.Add(inNode.InnerText);
                    inNode=inNode.NextSibling;
                }
            }
            catch(System.IO.DirectoryNotFoundException)
            {
            }
            catch(System.IO.FileNotFoundException)
            {
            }
            catch(System.Xml.XmlException)
            {
            }

            foreach(string name in SOPNameList)
            {
                SOPFileLoad(name);
            }
        }

        /// <summary>
        /// Attempts to read and parse a SOP file of the specific name
        /// </summary>
        /// <param name="SOPName">Name of SOP</param>
        public void SOPFileLoad(string SOPName)
        {
            try
            {
                XmlDocument xmlDoc=new XmlDocument();
                xmlDoc.Load("Documents/SOP/"+SOPName+".xml");

                ///A SOP file could be big, leaving it fto be parsed future when needed
                SOPXmlList.Add(SOPName,xmlDoc);
            }

            ///When a file doesn't exist, just skip it to the next
            catch(System.IO.DirectoryNotFoundException)
            {
            }
            catch(System.IO.FileNotFoundException)
            {
            }
            catch(System.Xml.XmlException)
            {
            }
            catch(System.ArgumentException)
            {
            }
        }

        /// <summary>
        /// Returns the existence of the SOP file by the index
        /// </summary>
        /// <param name="index">index of SOP in the list</param>
        /// <returns>
        /// True if the file exists
        /// False if the file doesn't exist
        /// </returns>
        public bool HasXml(int index)
        {
            try
            {
                return (SOPXmlList[SOPNameList[index]]!=null);
            }
            catch(System.Collections.Generic.KeyNotFoundException)
            {
                return false;
            }
        }
    }
}
