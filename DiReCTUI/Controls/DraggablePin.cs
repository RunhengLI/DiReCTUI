using Microsoft.Maps.MapControl.WPF;
using System.Windows.Input;

namespace DiReCTUI.Controls
{
    /// <summary>
    /// A class that creates draggable pushpins on the map, rather than the default static ones
    /// This file is copied from https://code.msdn.microsoft.com/bing/Draggable-Pushpins-in-Bing-48cb2683, related license can be found in the folder
    /// There's no need for further modifications on this file
    /// </summary>
    public class DraggablePin : Pushpin
    {
        private Map _map;
        private bool isDragging = false;
        Location _center;

        public DraggablePin(Map map)
        {
            _map = map;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (_map != null)
            {
                _center = _map.Center;

                _map.ViewChangeOnFrame += _map_ViewChangeOnFrame;
                _map.MouseUp += ParentMap_MouseLeftButtonUp;
                _map.MouseMove += ParentMap_MouseMove;
                _map.TouchMove += _map_TouchMove;
            }

            // Enable Dragging
            this.isDragging = true;

            base.OnMouseLeftButtonDown(e);
        }

        void _map_TouchMove(object sender, TouchEventArgs e)
        {
            var map = sender as Microsoft.Maps.MapControl.WPF.Map;
            // Check if the user is currently dragging the Pushpin
            if (this.isDragging)
            {
                // If so, the Move the Pushpin to where the Mouse is.
                var mouseMapPosition = e.GetTouchPoint(map);
                var mouseGeocode = map.ViewportPointToLocation(mouseMapPosition.Position);
                this.Location = mouseGeocode;
            }
        }

        void _map_ViewChangeOnFrame(object sender, MapEventArgs e)
        {
            if (isDragging)
            {
                _map.Center = _center;
            }
        }

        #region "Mouse Event Handler Methods"

        void ParentMap_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // Left Mouse Button released, stop dragging the Pushpin
            if (_map != null)
            {
                _map.ViewChangeOnFrame -= _map_ViewChangeOnFrame;
                _map.MouseUp -= ParentMap_MouseLeftButtonUp;
                _map.MouseMove -= ParentMap_MouseMove;
                _map.TouchMove -= _map_TouchMove;
            }

            this.isDragging = false;
        }

        void ParentMap_MouseMove(object sender, MouseEventArgs e)
        {
            var map = sender as Microsoft.Maps.MapControl.WPF.Map;
            // Check if the user is currently dragging the Pushpin
            if (this.isDragging)
            {
                // If so, the Move the Pushpin to where the Mouse is.
                var mouseMapPosition = e.GetPosition(map);
                var mouseGeocode = map.ViewportPointToLocation(mouseMapPosition);
                this.Location = mouseGeocode;
            }
        }
        #endregion
    }
}
