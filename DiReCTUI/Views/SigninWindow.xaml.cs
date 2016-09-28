using DiReCTUI.Controls;
using DiReCTUI.ViewModels;
using System.Windows;

namespace DiReCTUI.Views
{
    /// <summary>
    /// Interaction logic for SigninWindow.xaml
    /// A window for signing in to the system
    /// This is a simple and independent window, programming in xaml.cs
    /// </summary>
    public partial class SigninWindow:Window
    {
        /// <summary>
        /// Constructor method
        /// No user name, for the first login
        /// </summary>
        public SigninWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor method
        /// Displays login window with previously used login name automatically filled
        /// </summary>
        /// <param name="signoutName">previously used user name</param>
        public SigninWindow(string signoutName)
        {
            InitializeComponent();
            NameTextbox.Text=signoutName;
            MessageBox.Show("Sign out successful","",MessageBoxButton.OK);
        }

        /// <summary>
        /// Event which is invoked each time the Signin button is clicked, performs security challenge
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SigninButton_Click(object sender,RoutedEventArgs e)
        {
            ///If passed
            if(new SigninChallenge(NameTextbox.Text,PasswordTextbox.Password).Verify())
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.DataContext=new MainWindowViewModel(NameTextbox.Text);
                mainWindow.Show();
                this.Close();
            }
            ///If failed
            else
            {
                MessageBox.Show("Wrong Password","Error",MessageBoxButton.OK,MessageBoxImage.Information);
                PasswordTextbox.Password="";
            }
        }
    }
}
