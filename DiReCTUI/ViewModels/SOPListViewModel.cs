using DiReCTUI.Controls;
using DiReCTUI.Views.Components;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DiReCTUI.ViewModels
{
    /// <summary>
    /// View model for SOPListView.xaml
    /// </summary>
    class SOPListViewModel:ViewModelBase
    {
        /// <summary>
        /// The main grid
        /// WIP:Hard-coded grid
        /// </summary>
        #region "SOPGrid"
        public Grid SOPGrid
        {
            get
            {
                return _SOPGrid;
            }
        }
        public void SOPGridClear()
        {
            _SOPGrid.Children.Clear();
            _SOPGrid.ColumnDefinitions.Clear();
            _SOPGrid.RowDefinitions.Clear();
            OnPropertyChanged("SOPGrid");
        }
        public void SOPGridAdd(TextBlockView item)
        {
            _SOPGrid.Children.Add(item);
            OnPropertyChanged("SOPGrid");
        }
        public void SOPGridAdd(ColumnDefinition item)
        {
            _SOPGrid.ColumnDefinitions.Add(item);
            OnPropertyChanged("SOPGrid");
        }
        public void SOPGridAdd(RowDefinition item)
        {
            _SOPGrid.RowDefinitions.Add(item);
            OnPropertyChanged("SOPGrid");
        }
        private Grid _SOPGrid =new Grid();
        #endregion

        /// <summary>
        /// Stores a reference of SOPReader from the parent
        /// </summary>
        private SOPReader SOPReader;

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="s">SOPReader to be displayed and updated</param>
        public SOPListViewModel(SOPReader s)
        {
            SOPReader=s;
            Refresh();
        }

        /// <summary>
        /// Displays status of local SOPs and download option
        /// </summary>
        private void Refresh()
        {
            ///Constant number of columns in the grid
            const int columnNum = 3;
            int SOPNum = SOPReader.SOPNameList.Count;

            ///Number of rows needed
            int rowNum = (int)Math.Ceiling((double)SOPNum/(double)columnNum);

            SOPGridClear();

            for(int i = 0;i<columnNum;i++)
                SOPGridAdd(new ColumnDefinition());

            for(int i = 0;i<rowNum;i++)
                SOPGridAdd(new RowDefinition());

            for(int i = 0;i<SOPNum;i++)
            {
                ///Number of rows needed
                TextBlockView textBlockView = new TextBlockView();
                string text = "\n\n\n"+SOPReader.SOPNameList[i]+"\n\n\n";
                if(SOPReader.HasXml(i))
                    ///If SOP of the current is found, disable download option
                    textBlockView.DataContext=new TextBlockViewModel(Brushes.LightGreen,text,null,i);
                else
                    ///If SOP of the current is not found, enable download option
                    textBlockView.DataContext=new TextBlockViewModel(Brushes.LightGray,text,Download,i);
                SOPGridAdd(textBlockView);
                Grid.SetRow(textBlockView,(i-i%columnNum)/columnNum);
                Grid.SetColumn(textBlockView,i%columnNum);
            }
        }

        /// <summary>
        /// Triggered each time a TextBlockView is clicked, download the related document and refresh
        /// WIP: Real download system
        /// </summary>
        /// <param name="index">index held by the TextBlockView</param>
        private void Download(int index)
        {
            if(MessageBox.Show("DownLoad data of SOP ?\n\n"+SOPReader.SOPNameList[index],"",MessageBoxButton.OKCancel)==MessageBoxResult.OK)
            {
                ///Update SOPReader
                SOPReader.SOPFileLoad(SOPReader.SOPNameList[index]);

                ///Update this UserControl
                Refresh();
            }
        }
    }
}
