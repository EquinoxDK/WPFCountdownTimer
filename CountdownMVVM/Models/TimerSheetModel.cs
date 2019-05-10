using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountdownMVVM.Models
{
    class TimerSheetModel :INotifyPropertyChanged
    {
        private string text;
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                OnPropertyChanged("Text");
            }
        }

        private string time;
        public string Time
        {
            get { return time; }
            set
            {
                time = value;
                OnPropertyChanged("Time");
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
