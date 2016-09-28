using DiReCTUI.Controls;
using DiReCTUI.Models;
using Microsoft.Maps.MapControl.WPF;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DiReCTUI.Views.Components
{
    /// <summary>
    /// Interaction logic for MapView.xaml
    /// An user control that displays a Bing map and pushpins on it
    /// A lot of map properties are unbindable, MVVM is not recommended for this view
    /// Programming directly in xaml.cs rather than in a view model
    /// </summary>
    public partial class MapView:UserControl
    {
        /// <summary>
        /// Stores SOP
        /// </summary>
        private SOP SOP;

        /// <summary>
        /// Constructor method
        /// Generates a map and a menu according to the selected SOP
        /// </summary>
        /// <param name="location">User location</param>
        /// <param name="s">Selected SOP</param>
        public MapView(Location location,SOP s)
        {

            InitializeComponent();
            SOP=s;
            ClearPin();
            MainMap.Center=location;

            foreach(LocationTask l in SOP.LocationTaskList)
                AddStaticPin(l);
            AddDraggablePin(location);
        }

        /// <summary>
        /// Clears all pushpins on the map
        /// </summary>
        public void ClearPin()
        {
            MainMap.Children.Clear();
        }

        /// <summary>
        /// Adds a static pushpin on the map
        /// </summary>
        /// <param name="locationTask">A location and its list of recommended tasks</param>
        public void AddStaticPin(LocationTask locationTask)
        {
            Pushpin newPin = new Pushpin();
            newPin.Location=locationTask.Location;
            newPin.Background=Brushes.Gray;

            string task = "Please Record:\n";
            foreach(string s in locationTask.TaskList)
                task=task+s+"\n";
            newPin.ToolTip=task;

            ///Lambda function which is invoked each time a static pin is clicked
            newPin.PreviewMouseDown+=(senderPin,clickEvent) =>
            {
                MessageBox.Show(task,"",MessageBoxButton.OK);
            };

            MainMap.Children.Add(newPin);
        }

        /// <summary>
        /// Adds a draggable pushpin on the map
        /// WIP: Draggable for demo only, should not be draggable on a real tablet
        /// </summary>
        /// <param name="location">Pushpin Location</param>
        public void AddDraggablePin(Location location)
        {
            DraggablePin newPin = new DraggablePin(MainMap);
            newPin.Location=location;
            MainMap.Children.Add(newPin);
        }

        /// <summary>
        /// Updates DraggablePin location
        /// Currently Unused, should be called regularly to update user position on a real tablet
        /// </summary>
        /// <param name="location"></param>
        public void PinMove(Location location)
        {
            ///The user pushpin is the last children
            (MainMap.Children[MainMap.Children.Count-1] as DraggablePin).Location=location;
        }

        /// <summary>
        /// Returns current user location for recording to use
        /// </summary>
        /// <returns>Current draggable pin lcoation</returns>
        public Location GetLocation()
        {
            ///The user pushpin is the last children,
            return (MainMap.Children[MainMap.Children.Count-1] as DraggablePin).Location;
        }
    }
}
