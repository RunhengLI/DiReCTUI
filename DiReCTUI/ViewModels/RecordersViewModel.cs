using DiReCTUI.Views.Components;
using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace DiReCTUI.ViewModels
{
    /// <summary>
    /// View Model for RecordersView.xaml
    /// WIP: make the grid content dynamic by reading an Xml file
    /// </summary>
    class RecordersViewModel:ViewModelBase
    {
        /// <summary>
        /// The main grid
        /// WIP:Hard-coded grid
        /// </summary>
        #region "RecordersGrid"
        public Grid RecordersGrid
        {
            get
            {
                return _RecordersGrid;
            }
        }
        public void RecordersGridClear()
        {
            _RecordersGrid.Children.Clear();
            _RecordersGrid.ColumnDefinitions.Clear();
            _RecordersGrid.RowDefinitions.Clear();
            OnPropertyChanged("RecordersGrid");
        }
        public void RecordersGridAdd(TextBlockView item)
        {
            _RecordersGrid.Children.Add(item);
            OnPropertyChanged("RecordersGrid");
        }
        public void RecordersGridAdd(ColumnDefinition item)
        {
            _RecordersGrid.ColumnDefinitions.Add(item);
            OnPropertyChanged("RecordersGrid");
        }
        public void RecordersGridAdd(RowDefinition item)
        {
            _RecordersGrid.RowDefinitions.Add(item);
            OnPropertyChanged("RecordersGrid");
        }
        private Grid _RecordersGrid = new Grid();
        #endregion

        /// <summary>
        /// Constructor method
        /// </summary>
        public RecordersViewModel()
        {
            Refresh();
        }

        /// <summary>
        /// Displays a list of recorders
        /// </summary>
        private void Refresh()
        {
            ///Constant number of columns in the grid
            const int columnNum = 3;

            ///Number of recorders read
            ///WIP: Hard-coded for demo only, should be based on input data on a real tablet
            const int recorderNum = 14;

            ///Number of rows needed
            int rowNum = (int)Math.Ceiling((double)recorderNum/(double)columnNum);

            for(int i = 0;i<columnNum;i++)
                RecordersGridAdd(new ColumnDefinition());

            for(int i = 0;i<rowNum;i++)
                RecordersGridAdd(new RowDefinition());

            for(int i = 0;i<recorderNum;i++)
            {
                ///Adds a textBlockView to the grid
                TextBlockView textBlockView = new TextBlockView();
                string text = "\nRecorder Name: "+i+"\n\nPhone Number:\n"+"0000000000"+"\n\n";
                textBlockView.DataContext=new TextBlockViewModel(Brushes.LightYellow,text,null,i);
                RecordersGridAdd(textBlockView);
                Grid.SetRow(textBlockView,(i-i%columnNum)/columnNum);
                Grid.SetColumn(textBlockView,i%columnNum);
            }
        }
    }
}
