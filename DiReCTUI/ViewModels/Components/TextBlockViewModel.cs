using DiReCTUI.Controls;
using System;
using System.Windows.Input;
using System.Windows.Media;

namespace DiReCTUI.ViewModels
{
    /// <summary>
    /// View Model for TextBlockView.xaml
    /// </summary>
    class TextBlockViewModel:ViewModelBase
    {
        /// <summary>
        /// Backcolor of the TextBlock
        /// </summary>
        #region "BlockColor"
        public Brush BlockColor
        {
            get
            {
                return _BlockColor;
            }
            set
            {
                _BlockColor=value;
                OnPropertyChanged("BlockColor");
            }
        }
        private Brush _BlockColor;
        #endregion

        /// <summary>
        /// Text in the TextBlock
        /// </summary>
        #region "BlockText"
        public string BlockText
        {
            get
            {
                return _BlockText;
            }
            set
            {
                _BlockText=value;
                OnPropertyChanged("BlockText");
            }
        }
        private String _BlockText;
        #endregion

        /// <summary>
        /// Command performed when clicked
        /// </summary>
        #region "BlockClick"
        public ICommand BlockClick
        {
            get
            {
                if(_BlockClick==null)
                    _BlockClick=new RelayCommand(p => this.ClickCommand(Index));
                return _BlockClick;
            }
        }
        private RelayCommand _BlockClick;
        #endregion

        /// <summary>
        /// Stores the command to be performed
        /// </summary>
        Action<int> ClickCommand;

        /// <summary>
        /// Stores the parameter to be passed to the command
        /// </summary>
        int Index;

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="blockColor">Backcolor of the TextBlock</param>
        /// <param name="blockText">Text in the TextBlock</param>
        /// <param name="clickCommand">Command performed when clicked, has an input of type int and no return value</param>
        /// <param name="index">Parameter to be passed to the command</param>
        public TextBlockViewModel(Brush blockColor,string blockText,Action<int> clickCommand,int index)
        {
            BlockColor=blockColor;
            BlockText=blockText;
            if(clickCommand!=null)
                ///Command found, relay to that command
                ClickCommand=clickCommand;
            else
                ///Command not found, relay to a dummy command
                ClickCommand=DummyCommand;
            Index=index;
        }

        /// <summary>
        /// A dummy command which does nothing
        /// Must not be deleted, for the clickCommand must hold a command or it will throw exception
        /// </summary>
        /// <param name="index"></param>
        private void DummyCommand(int index)
        { }
    }
}
