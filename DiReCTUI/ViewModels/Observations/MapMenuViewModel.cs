using DiReCTUI.Controls;
using DiReCTUI.Models;
using DiReCTUI.Views.Components;
using DiReCTUI.Views.Observations;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml;

namespace DiReCTUI.ViewModels
{
    /// <summary>
    /// View Model for MapMenuView.xaml
    /// </summary>
    class MapMenuViewModel:ViewModelBase
    {
        /// <summary>
        /// ContentControl for RecordView instances
        /// </summary>
        #region "RecordContent"
        public Object RecordContent
        {
            get
            {
                return _RecordContent;
            }
            set
            {
                _RecordContent=value;
                OnPropertyChanged("RecordContent");
            }
        }
        private Object _RecordContent = new Object();
        #endregion

        /// <summary>
        /// Visibility of the ContentControl for records
        /// </summary>
        #region "RecordContentVisibility"
        public Visibility RecordContentVisibility
        {
            get
            {
                return _RecordContentVisibility;
            }
            set
            {
                _RecordContentVisibility=value;
                OnPropertyChanged("RecordContentVisibility");
            }
        }
        private Visibility _RecordContentVisibility = Visibility.Collapsed;
        #endregion

        /// <summary>
        /// ContentControl for MapView instances
        /// </summary>
        #region "MapContent"
        public Object MapContent
        {
            get
            {
                return _MapContent;
            }
            set
            {
                _MapContent=value;
                OnPropertyChanged("MapContent");
            }
        }
        private Object _MapContent = new Object();
        #endregion

        /// <summary>
        /// Visibility of everything about the map, including the button and the menu
        /// </summary>
        #region "MapContentVisibility"
        public Visibility MapContentVisibility
        {
            get
            {
                return _MapContentVisibility;
            }
            set
            {
                _MapContentVisibility=value;
                OnPropertyChanged("MapContentVisibility");
            }
        }
        private Visibility _MapContentVisibility;
        #endregion

        /// <summary>
        /// Command for toggling menu visibility
        /// </summary>
        #region "ToggleMenuCommand"
        public ICommand ToggleMenuCommand
        {
            get
            {
                if(_ToggleMenuCommand==null)
                    _ToggleMenuCommand=new RelayCommand(p => this.ToggleMenu());
                return _ToggleMenuCommand;
            }
        }
        private RelayCommand _ToggleMenuCommand;
        #endregion

        /// <summary>
        /// Grid for the menu
        /// WIP: Hard-coded grid
        /// </summary>
        #region "MenuGrid"
        public Grid MenuGrid
        {
            get
            {
                return _MenuGrid;
            }
        }
        public void MenuGridClear()
        {
            _MenuGrid.Children.Clear();
            _MenuGrid.ColumnDefinitions.Clear();
            _MenuGrid.RowDefinitions.Clear();
            OnPropertyChanged("MenuGrid");
        }
        public void MenuGridAdd(TextBlockView item)
        {
            _MenuGrid.Children.Add(item);
            OnPropertyChanged("MenuGrid");
        }
        public void MenuGridAdd(ColumnDefinition item)
        {
            _MenuGrid.ColumnDefinitions.Add(item);
            OnPropertyChanged("MenuGrid");
        }
        public void MenuGridAdd(RowDefinition item)
        {
            _MenuGrid.RowDefinitions.Add(item);
            OnPropertyChanged("MenuGrid");
        }
        private Grid _MenuGrid = new Grid();
        #endregion

        /// <summary>
        /// Visibility of the menu grid
        /// </summary>
        #region "MenuGridVisibility"
        public Visibility MenuGridVisibility
        {
            get
            {
                return _MenuGridVisibility;
            }
            set
            {
                _MenuGridVisibility=value;
                OnPropertyChanged("MenuGridVisibility");
            }
        }
        private Visibility _MenuGridVisibility=Visibility.Collapsed;
        #endregion

        ///<summary>
        /// Stores a reference of SOP from the parent
        /// </summary>
        private SOP SOP;

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="location">User location</param>
        /// <param name="s">Selected SOP</param>
        public MapMenuViewModel(Location location,SOP s)
        {
            SOP=s;

            ///Generates the map
            MapContent=new MapView(location,s);
            DisplayMenu();
        }

        /// <summary>
        /// Triggered each time MenuButton is clicked, toggles menu visibility
        /// </summary>
        private void ToggleMenu()
        {
            MenuGridVisibility=(MenuGridVisibility==Visibility.Visible) ? Visibility.Collapsed : Visibility.Visible;
        }

        /// <summary>
        /// Displays task menu
        /// </summary>
        private void DisplayMenu()
        {
            List<String> taskList = new List<String>();
            foreach(XmlNode taskNode in SOP.TaskNodeList)
                taskList.Add(taskNode.Name);
            int taskNum = taskList.Count;

            MenuGrid.HorizontalAlignment=HorizontalAlignment.Right;

            for(int i = 0;i<taskNum;i++)
                MenuGridAdd(new RowDefinition());

            ///WIP: Create new view and view model for setting these preperties in MVVM style
            for(int i = 0;i<taskNum;i++)
            {
                TextBlockView textBlockView = new TextBlockView();
                textBlockView.FontSize=22;
                textBlockView.Block.Height=30;
                textBlockView.Block.Width=double.NaN;
                textBlockView.DataContext=new TextBlockViewModel(Brushes.Transparent,taskList[i],CreateRecord,i);
                MenuGridAdd(textBlockView);
                Grid.SetRow(textBlockView,i);
            }
            _MenuGrid.HorizontalAlignment=HorizontalAlignment.Right;
            _MenuGrid.VerticalAlignment=VerticalAlignment.Bottom;
            _MenuGrid.Margin=new Thickness(0,0,5,95);
        }

        /// <summary>
        /// Creates a RecordView instance of the selected task
        /// </summary>
        /// <param name="index">Index of the selected task</param>
        private void CreateRecord(int index)
        {
            Location location = (MapContent as MapView).GetLocation();
            RecordView recordView = new RecordView();
            recordView.DataContext=new RecordViewModel(SOP.TaskNodeList[index],location,SOP.Name,this);
            RecordContent=recordView;
            MapContentVisibility=Visibility.Collapsed;
            RecordContentVisibility=Visibility.Visible;
        }
    }
}
