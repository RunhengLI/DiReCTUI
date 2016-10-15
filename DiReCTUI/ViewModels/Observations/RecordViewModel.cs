using DiReCTUI.Controls;
using DiReCTUI.Models;
using Microsoft.Maps.MapControl.WPF;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;

namespace DiReCTUI.ViewModels
{
    /// <summary>
    /// View model for RecordView.xaml
    /// </summary>
    class RecordViewModel:ViewModelBase
    {
        /// <summary>
        /// The main grid
        /// WIP:Hard-coded grid
        /// </summary>
        #region "RecordGrid"
        public Grid RecordGrid
        {
            get
            {
                return _RecordGrid;
            }
        }
        public void RecordGridClear()
        {
            _RecordGrid.Children.Clear();
            _RecordGrid.ColumnDefinitions.Clear();
            _RecordGrid.RowDefinitions.Clear();
            OnPropertyChanged("RecordGrid");
        }
        public void RecordGridAdd(TextBlock item)
        {
            _RecordGrid.Children.Add(item);
            OnPropertyChanged("RecordGrid");
        }
        public void RecordGridAdd(TextBox item)
        {
            _RecordGrid.Children.Add(item);
            OnPropertyChanged("RecordGrid");
        }
        public void RecordGridAdd(ListBox item)
        {
            _RecordGrid.Children.Add(item);
            OnPropertyChanged("RecordGrid");
        }
        public void RecordGridAdd(ColumnDefinition item)
        {
            _RecordGrid.ColumnDefinitions.Add(item);
            OnPropertyChanged("RecordGrid");
        }
        public void RecordGridAdd(RowDefinition item)
        {
            _RecordGrid.RowDefinitions.Add(item);
            OnPropertyChanged("RecordGrid");
        }
        private Grid _RecordGrid = new Grid();
        #endregion

        /// <summary>
        /// Command for saving the record
        /// </summary>
        #region "SaveCommand"
        public ICommand SaveCommand
        {
            get
            {
                if(_SaveCommand==null)
                    _SaveCommand=new RelayCommand(p => this.Save());
                return _SaveCommand;
            }
        }
        private RelayCommand _SaveCommand;
        #endregion

        /// <summary>
        /// Command for aborting and returning to map menu
        /// </summary>
        #region "AbortCommand"
        public ICommand AbortCommand
        {
            get
            {
                if(_AbortCommand==null)
                    _AbortCommand=new RelayCommand(p => this.Abort());
                return _AbortCommand;
            }
        }
        private RelayCommand _AbortCommand;
        #endregion

        /// <summary>
        /// Current SOP task
        /// </summary>
        private SOPTask SOPTask;

        /// <summary>
        /// Stores user location
        /// </summary>
        private Location Location;

        /// <summary>
        /// Stores SOP name
        /// </summary>
        private string SOPName;

        /// <summary>
        /// Stores a reference of the parent model
        /// </summary>
        MapMenuViewModel Model;

        /// <summary>
        /// Construction method
        /// </summary>
        /// <param name="XmlNode">Xml node containing the selected SOP task</param>
        /// <param name="l">User location</param>
        /// <param name="name">SOP name</param>
        /// <param name="model">A reference of the parent model</param>
        public RecordViewModel(XmlNode XmlNode,Location l,string name,MapMenuViewModel model)
        {
            SOPTask=new SOPTask(XmlNode);
            Model=model;
            Location=l;
            SOPName=name;
            Refresh();
        }

        /// <summary>
        /// Triggered each time SaveButton is clicked, generates a RecordWriter instance to write record file
        /// </summary>
        private void Save()
        {
            ///A list to collect all responses in order
            List<string> responseList = new List<string>();

            for(int i = 0;i<SOPTask.SubTaskList.Count;i++)
            {
                switch(SOPTask.SubTaskList[i].Type)
                {
                case "enum":
                    responseList.Add(SOPTask.SubTaskList[i].defaultList[(RecordGrid.Children[2*i+1] as ListBox).SelectedIndex]);
                    break;
                default:
                    responseList.Add((RecordGrid.Children[2*i+1] as TextBox).Text);
                    break;
                }
            }
            RecordWriter recordWriter = new RecordWriter(SOPName,SOPTask,responseList,Location);
            MessageBox.Show("New record saved","",MessageBoxButton.OK);
            Return();
        }

        /// <summary>
        /// Triggered each time AbortButton is clicked, asks if the user would like to leave
        /// WIP: save unfinished survey when leaving
        /// </summary>
        private void Abort()
        {
            if(MessageBox.Show("Abort recording?\nAll unsaved changes will be lost!","",MessageBoxButton.OKCancel)==MessageBoxResult.OK)
            {
                Return();
            }
        }

        /// <summary>
        /// Return to the map menu
        /// </summary>
        private void Return()
        {
            Model.MapContentVisibility=Visibility.Visible;
            Model.RecordContentVisibility=Visibility.Collapsed;
            Model.MapControlVisibility=Visibility.Visible;
            Model.MenuGridVisibility=Visibility.Collapsed;
            Model.RecordContent=null;
        }

        /// <summary>
        /// Generates and displays the survey
        /// WIP: more type options (map polygons, charts, photos...), by invoking new view models
        /// </summary>
        private void Refresh()
        {
            int subTaskNum = SOPTask.SubTaskList.Count;

            RecordGridClear();
            _RecordGrid.VerticalAlignment=VerticalAlignment.Top;

            ///Sets row definitions, for both questions and responses
            for(int i = 0;i<2*subTaskNum;i++)
                RecordGridAdd(new RowDefinition());

            for(int i = 0;i<subTaskNum;i++)
            {
                ///Question labels
                ///WIP: Create new view and view model for setting these preperties in MVVM style
                TextBlock textBlock = new TextBlock();
                textBlock.Text=SOPTask.SubTaskList[i].Name;
                textBlock.Height=40;
                RecordGridAdd(textBlock);
                Grid.SetRow(textBlock,2*i);

                ///Creates questions according to the subTask type
                switch(SOPTask.SubTaskList[i].Type)
                {               
                case "enum":
                    ///for enum type questions, create a ListBox for choosing
                    ListBox listBox = new ListBox();
                    foreach(string s in SOPTask.SubTaskList[i].defaultList)
                        listBox.Items.Add(s);
                    listBox.SelectedIndex=0;
                    listBox.Width=450;
                    RecordGridAdd(listBox);
                    Grid.SetRow(listBox,2*i+1);
                    break;               
                default:
                    ///for other types, just create a TextBox (or a button to invoke other UserControls in the future)
                    TextBox textBox = new TextBox();
                    textBox.Text=SOPTask.SubTaskList[i].defaultList[0];
                    textBox.Width=450;
                    RecordGridAdd(textBox);
                    Grid.SetRow(textBox,2*i+1);
                    break;
                }
            }
        }
    }
}
