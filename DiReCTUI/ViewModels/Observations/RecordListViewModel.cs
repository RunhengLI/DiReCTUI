using DiReCTUI.Controls;
using DiReCTUI.Models;
using System.Collections.Generic;
using System.Windows.Controls;

namespace DiReCTUI.ViewModels
{
    /// <summary>
    /// View model for RecordListView.xaml
    /// </summary>
    class RecordListViewModel:ViewModelBase
    {
        /// <summary>
        /// List of TabItems to be displayed
        /// </summary>
        #region "TabItemList"
        public List<TabItem> TabItemList
        {
            get
            {
                return _TabItemList;
            }
        }
        public void TabItemListClear()
        {
            _TabItemList.Clear();
            OnPropertyChanged("TabItemList");
        }
        public void TabItemListAdd(TabItem item)
        {
            _TabItemList.Add(item);
            OnPropertyChanged("TabItemList");
        }
        public void TabItemListSet(TabItem item,int index)
        {
            if(_TabItemList[index]!=item)
            {
                _TabItemList[index]=item;
                OnPropertyChanged("TabItemList");
            }
        }
        private List<TabItem> _TabItemList = new List<TabItem>();
        #endregion

        /// <summary>
        /// A RecordReader instance for loading and parsing record files
        /// </summary>
        private RecordReader RecordReader;

        /// <summary>
        /// Constructor method
        /// Reads record files of a SOP
        /// </summary>
        /// <param name="SOPName">Name of the SOP whose records are to be read</param>
        public RecordListViewModel(string SOPName)
        {
            RecordReader=new RecordReader(SOPName);

            ///Add records to the TabControl
            TabItemListClear();
            foreach(Record record in RecordReader.RecordList)
            {
                TabItem tabItem = new TabItem();
                tabItem.Header=record.TaskName+"\n"+record.Time;
                TextBlock textBlock = new TextBlock();
                textBlock.Text="Latitude: "+record.Location.Latitude+"\nLongitude: "+record.Location.Longitude+"\n\nRecord:\n";
                textBlock.Text+=record.TaskResponse.InnerXml;
                tabItem.Content=textBlock;
                TabItemListAdd(tabItem);
            }
        }
    }
}
