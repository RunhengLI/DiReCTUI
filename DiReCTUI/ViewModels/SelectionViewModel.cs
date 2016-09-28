using DiReCTUI.Controls;
using DiReCTUI.Views.Components;
using DiReCTUI.Views.Observations;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DiReCTUI.ViewModels
{
    /// <summary>
    /// View model for SelectionView.xaml
    /// </summary>
    class SelectionViewModel:ViewModelBase
    {
        /// <summary>
        /// ContentControl for ObservationView instances
        /// </summary>
        #region "Content"
        public Object Content
        {
            get
            {
                return _Content;
            }
            set
            {
                _Content=value;
                OnPropertyChanged("Content");
            }
        }
        private Object _Content = new Object();
        #endregion

        /// <summary>
        /// Visibility of the ContentControl for observations
        /// </summary>
        #region "ContentVisibility"
        public Visibility ContentVisibility
        {
            get
            {
                return _ContentVisibility;
            }
            set
            {
                _ContentVisibility=value;
                OnPropertyChanged("ContentVisibility");
            }
        }
        private Visibility _ContentVisibility=Visibility.Collapsed;
        #endregion

        /// <summary>
        /// The selection grid
        /// WIP:Hard-coded grid
        /// </summary>
        #region "SelectionGrid"
        public Grid SelectionGrid
        {
            get
            {
                return _SelectionGrid;
            }
        }
        public void SelectionGridClear()
        {
            _SelectionGrid.Children.Clear();
            _SelectionGrid.ColumnDefinitions.Clear();
            _SelectionGrid.RowDefinitions.Clear();
            OnPropertyChanged("SelectionGrid");
        }
        public void SelectionGridAdd(TextBlockView item)
        {
            _SelectionGrid.Children.Add(item);
            OnPropertyChanged("SelectionGrid");
        }
        public void SelectionGridAdd(ColumnDefinition item)
        {
            _SelectionGrid.ColumnDefinitions.Add(item);
            OnPropertyChanged("SelectionGrid");
        }
        public void SelectionGridAdd(RowDefinition item)
        {
            _SelectionGrid.RowDefinitions.Add(item);
            OnPropertyChanged("SelectionGrid");
        }
        private Grid _SelectionGrid = new Grid();
        #endregion

        /// <summary>
        /// Visibility of the selection grid
        /// </summary>
        #region "SelectionGridVisibility"
        public Visibility SelectionGridVisibility
        {
            get
            {
                return _SelectionGridVisibility;
            }
            set
            {
                _SelectionGridVisibility=value;
                OnPropertyChanged("SelectionGridVisibility");
            }
        }
        private Visibility _SelectionGridVisibility;
        #endregion

        /// <summary>
        /// Stores a reference of SOPReader from the parent
        /// </summary>
        private SOPReader SOPReader;

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="s">SOPReader instance containing Xml files of every SOP</param>
        public SelectionViewModel(SOPReader s)
        {
            SOPReader=s;
            Refresh();
        }

        /// <summary>
        /// Displays TextBlockViews of SOPs for selection
        /// </summary>
        public void Refresh()
        {
            ///Constant number of columns in the grid
            const int columnNum = 3;
            int SOPNum = SOPReader.SOPNameList.Count();

            ///Number of rows needed
            int rowNum = (int)Math.Ceiling((double)SOPNum/(double)columnNum);

            SelectionGridClear();

            for(int i = 0;i<columnNum;i++)
                SelectionGridAdd(new ColumnDefinition());

            for(int i = 0;i<rowNum;i++)
                SelectionGridAdd(new RowDefinition());

            for(int i = 0;i<SOPNum;i++)
            {
                ///Adds a textBlockView to the grid
                TextBlockView textBlockView = new TextBlockView();
                string text = "\n\n\n"+SOPReader.SOPNameList[i]+"\n\n\n";
                if(SOPReader.HasXml(i))
                    ///If SOP of the current is found, enable selection
                    textBlockView.DataContext=new TextBlockViewModel(Brushes.AliceBlue,text,Select,i);
                else
                    ///If SOP of the current is not found, disable selection
                    textBlockView.DataContext=new TextBlockViewModel(Brushes.LightGray,text,null,i);
                SelectionGridAdd(textBlockView);
                Grid.SetRow(textBlockView,(i-i%columnNum)/columnNum);
                Grid.SetColumn(textBlockView,i%columnNum);
            }
        }

        /// <summary>
        /// Triggered each time a TextBlockView is clicked, generates a new ObservationView instance of the selected SOP
        /// </summary>
        /// <param name="index">index held by the TextBlockView</param>
        private void Select(int index)
        {
            ObservationsView observationsView = new ObservationsView();
            observationsView.DataContext=new ObservationsViewModel(SOPReader.SOPXmlList[SOPReader.SOPNameList[index]],this);
            Content=observationsView;
            SelectionGridVisibility=Visibility.Collapsed;
            ContentVisibility=Visibility.Visible;
        }
    }
}
