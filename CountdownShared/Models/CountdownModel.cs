using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountdownShared.Models
{
    public class CountdownModel : INotifyPropertyChanged
    {
        public CountdownModel()
        {

        }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        private int hours;
        public int Hours
        {
            get { return hours; }
            set
            {
                hours = value;
                OnPropertyChanged("Hours");
            }
        }

        private int minutes;
        public int Minutes
        {
            get { return minutes; }
            set
            {
                minutes = value;
                OnPropertyChanged("Minutes");
            }
        }

        private int seconds;

        public int Seconds
        {
            get { return seconds; }
            set
            {
                seconds = value;
                OnPropertyChanged("Seconds");
            }
        }

        #region INotifyMember
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string PropertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        #endregion
    }
}
