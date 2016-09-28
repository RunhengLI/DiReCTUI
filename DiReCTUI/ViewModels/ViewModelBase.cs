using System;
using System.Diagnostics;
using System.ComponentModel;

namespace DiReCTUI.ViewModels
{
    /// <summary>
    /// A class that collects and sends Icommands to update front-end binded objects
    /// Can only transmit objects, not commands
    /// This class is copied from Daniel Yhung's code, github address https://github.com/yhung119/DiReCTUI
    /// There's no need for further modifications on this file
    /// </summary>
    public class ViewModelBase:INotifyPropertyChanged
    {
        ///<summary>
        ///Warns the developers when the property is not
        ///included in the specific object
        /// </summary>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            //Verify that the property is real, public,instance property on this object
            if(TypeDescriptor.GetProperties(this)[propertyName]==null)
            {
                string msg = "INvalid property name: "+propertyName;

                if(this.ThrowOnInvalidPropertyName)
                {
                    throw new Exception(msg);
                }
                else
                {
                    Debug.Fail(msg);
                }
            }
        }

        protected virtual bool ThrowOnInvalidPropertyName { get; private set; }

        ///<summary>
        ///Raised when a property on this object has a new value
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        ///<summary>
        ///Raises this object's PropertyChange Event
        /// </summary>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.VerifyPropertyName(propertyName);

            PropertyChangedEventHandler handler = this.PropertyChanged;
            if(handler!=null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this,e);
            }

        }
    }
}
