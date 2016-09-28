using DiReCTUI.Controls;
using DiReCTUI.Models;
using DiReCTUI.Views.Observations;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Xml;

namespace DiReCTUI.ViewModels
{
    /// <summary>
    /// View Model for ObservationsView.xaml
    /// </summary>
    class ObservationsViewModel:ViewModelBase
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
        private int _SelectedItem = -1;
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
        /// Command for returning to SOP selection
        /// </summary>
        #region "ReturnCommand"
        public ICommand ReturnCommand
        {
            get
            {
                if(_ReturnCommand==null)
                    _ReturnCommand=new RelayCommand(p => this.Return());
                return _ReturnCommand;
            }
        }
        private RelayCommand _ReturnCommand;
        #endregion

        ///<summary>
        /// A SOP instance for parsing Xml file of the selected SOP
        /// </summary>
        private SOP SOP;

        /// <summary>
        /// Stores a reference of the parent model
        /// </summary>
        private SelectionViewModel Model;

        /// <summary>
        /// Construction method
        /// </summary>
        /// <param name="XmlDoc">Xml document containing the selected SOP</param>
        /// <param name="model">A reference of the parent model</param>
        public ObservationsViewModel(XmlDocument XmlDoc,SelectionViewModel model)
        {
            SOP=new SOP(XmlDoc.FirstChild);
            Model=model;

            ///Adds items to be shown as headers
            HeaderListAdd("Map");
            HeaderListAdd("Record list");
            HeaderListAdd("Background");
            HeaderListAdd("Task view");

            ///Adds null to the list to prevent nullpointer
            ContentListAdd(null);
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
                ///Map selected, generates new instance at the first time loaded                
                if(ContentList[0]==null)
                {
                    ///WIP: Fixed location for demo only
                    ///Should fetch user GPS position on a real tablet
                    Location location = new Location(25.04133,121.6133);
                    MapMenuView mapMenuView = new MapMenuView();
                    mapMenuView.DataContext=new MapMenuViewModel(location,SOP);
                    ContentListSet(mapMenuView,0);
                }
                break;
            case 1:
                ///Record list selected, generates new instance EVERY TIME loaded, as there may exist newly-generated records
                RecordListView recordListView = new RecordListView();
                recordListView.DataContext=new RecordListViewModel(SOP.Name);
                ContentListSet(recordListView,1);
                break;
            case 2:
                ///Background selected
                ///WIP
                break;
            case 3:
                ///Task view selected
                ///WIP
                break;
            }
        }

        /// <summary>
        /// Triggered each time the ReturnButton is clicked, return to SOP selection
        /// </summary>
        private void Return()
        {
            if(MessageBox.Show("Return to SOP Selection?\nAll unsaved changes will be lost!","",MessageBoxButton.OKCancel)==MessageBoxResult.OK)
            {
                Model.SelectionGridVisibility=Visibility.Visible;
                Model.ContentVisibility=Visibility.Collapsed;
                Model.Content=null;
            }
        }
    }
}
