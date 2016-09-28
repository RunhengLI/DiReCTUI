using System.Windows;
using DiReCTUI.Views;

namespace DiReCTUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// The main window for this DiReCT User Interface
    /// </summary>
    public partial class MainWindow:Window
    {
        public MainWindow()
        {
            InitializeComponent();   
        }
       
        /// <summary>
        /// Event which is raised each time the sign out button is clicked 
        /// It's very hard to transfer a Close() command to the view model, hardly worth the effort
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        private void UserSignoutMenu_Click(object sender,RoutedEventArgs e)
        {
            if(MessageBox.Show("Are you sure to sign out?\nAll unsaved changes will be lost!","",MessageBoxButton.OKCancel)==MessageBoxResult.OK)
            {
                ///Close the main window and switch back to login window
                new SigninWindow(UserMenu.Header as string).Show();     
                this.Close();
            }
        }
    }
}