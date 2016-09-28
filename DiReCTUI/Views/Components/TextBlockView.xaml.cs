using System.Windows.Controls;

namespace DiReCTUI.Views.Components
{
    /// <summary>
    /// Interaction logic for TextBlockView.xaml
    /// An user control that contains a single TextBlock
    /// Its only purpose is to be added to the dynamic grids, in order to avoid too many property settings in view models
    /// </summary>
    public partial class TextBlockView:UserControl
    {
        public TextBlockView()
        {
            InitializeComponent();
        }
    }
}
