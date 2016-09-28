using System.Collections.Generic;
using System.Xml;
using DiReCTUI.Models;

namespace DiReCTUI.Controls
{
    /// <summary>
    /// A class that reads and parses existing records
    /// </summary>
    class RecordReader
    {
        /// <summary>
        /// A list of parsed records
        ///</summary>
        public List<Record> RecordList { get; protected set; }

        /// <summary>
        /// Stores the name of SOP
        /// </summary>
        private string SOPName;

        /// <summary>
        /// Constructor method
        /// Reads and parses all previous records of a specific SOP
        /// </summary>
        /// <param name="s">Name of the SOP whose records are to be read</param>
        public RecordReader(string s)
        {
            RecordList=new List<Record>();
            SOPName=s;
            RecordFileLoad();
        }

        /// <summary>
        /// Send all related SOP to be parsed
        /// </summary>
        private void RecordFileLoad()
        {
            try
            {
                foreach(string s in System.IO.Directory.GetFiles("Documents/Records/"+SOPName))
                {
                    XmlDocument xmlDoc=new XmlDocument();
                    xmlDoc.Load(s);

                    ///Double-check that it has the correct SOP name
                    if(xmlDoc.FirstChild.Name==SOPName)
                    {
                        ///There won't exist a large number of records on a tablet, so parse them directly
                        RecordList.Add(new Record(xmlDoc));                        
                    }
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
        }
    }
}
