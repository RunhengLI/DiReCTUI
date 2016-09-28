using DiReCTUI.Controls;
using DiReCTUI.Views;
using System;
using System.Collections.Generic;

namespace DiReCTUI.ViewModels
{
    /// <summary>
    /// View Model for RecordersView.xaml
    /// WIP: make the grid content dynamic by reading an Xml file
    /// </summary>
    class MainWindowViewModel:ViewModelBase
    {
        /// <summary>
        /// Selected index of the TabControl
        /// </summary>
        #region "SelectedItem"
        public int SelectedItem
        {
            get
            {
                return _SelectedItem;
            }
            set
            {
                if(_SelectedItem==value)
                    return;
                else
                {
                    _SelectedItem=value;
                    SelectionChange(value);
                }
            }
        }
        private int _SelectedItem=-1;
        #endregion

        /// <summary>
        /// List of contents of the TabControl
        /// </summary>
        #region "ContentList"
        public List<Object> ContentList
        {
            get
            {
                return _ContentList;
            }
        }
        public void ContentListClear()
        {
            _ContentList.Clear();
            OnPropertyChanged("ContentList");
        }
        public void ContentListAdd(Object item)
        {
            _ContentList.Add(item);
            OnPropertyChanged("ContentList");
        }
        public void ContentListSet(Object item,int index)
        {
            if(_ContentList[index]!=item)
            {
                _ContentList[index]=item;
                OnPropertyChanged("ContentList");
            }
        }
        private List<Object> _ContentList = new List<Object>();
        #endregion

        /// <summary>
        /// List of headers of the TabControl
        /// </summary>
        #region "HeaderList"
        public List<string> HeaderList
        {
            get
            {
                return _HeaderList;
            }
        }
        public void HeaderListClear()
        {
            _HeaderList.Clear();
            OnPropertyChanged("HeaderList");
        }
        public void HeaderListAdd(string item)
        {
            _HeaderList.Add(item);
            OnPropertyChanged("HeaderList");
        }
        private List<string> _HeaderList = new List<string>();
        #endregion

        /// <summary>
        /// User name, displayed as menu header
        /// </summary>
        #region "UserName"
        public string UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                _UserName=value;
                OnPropertyChanged("UserName");
            }
        }
        private string _UserName;
        #endregion

        ///<summary>
        /// A SOPReader instance to read the list and SOP files 
        /// </summary>
        private SOPReader SOPReader;

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="name">User name used during login</param>
        public MainWindowViewModel(string name)
        {
            SOPReader=new SOPReader();
            UserName=name;

            ///Adds items to be shown as headers
            HeaderListAdd("Observations");
            HeaderListAdd("Recorders");
            HeaderListAdd("SOP");

            ///Adds null to the list to prevent nullpointer
            ContentListAdd(null);
            ContentListAdd(null);
            ContentListAdd(null);
            SelectedItem=0;
        }

        /// <summary>
        /// Triggered each time the TabControl selection is changed
        /// </summary>
        /// <param name="index">Tabcontrol index after the change</param>
        private void SelectionChange(int index)
        {
            switch(index)
            {
            case 0:
                ///Observations selected, generates new instance at the first time loaded, then refresh it EVERY TIME loaded, as there may exist newly-downloaded SOPs
                if(ContentList[0]==null)
                {
                    SelectionView selectionView = new SelectionView();
                    selectionView.DataContext=new SelectionViewModel(SOPReader);
                    ContentListSet(selectionView,0);
                }
                else
                    ((ContentList[0] as SelectionView).DataContext as SelectionViewModel).Refresh();
                break;
            case 1:
                ///Recorders list selected, generates new instance at the first time loaded
                if(ContentList[1]==null)
                {
                    RecordersView recordersView = new RecordersView();
                    recordersView.DataContext=new RecordersViewModel();
                    ContentListSet(recordersView,1);
                }                  
                break;
            case 2:
                ///SOP list selected, generates new instance at the first time loaded
                if(ContentList[2]==null)
                {
                    SOPListView SOPListView = new SOPListView();
                    SOPListView.DataContext=new SOPListViewModel(SOPReader);
                    ContentListSet(SOPListView,2);
                }
                break;
            }
        }
    }
}
