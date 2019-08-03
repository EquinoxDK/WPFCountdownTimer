using CountdownShared.ViewModels;
using System.Windows;

namespace CountdownGUI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CountdownView : Window
    {
        public CountdownView()
        {
            InitializeComponent();
            DataContext = new CountdownViewModel(Helpers.SaveHelper.SaveList);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = MessageBox.Show("Want to exit app?", "Exit", MessageBoxButton.YesNo) != MessageBoxResult.Yes;
            if (!e.Cancel)
            {
                (DataContext as CountdownViewModel).ClearOutputFile();
            }
        }
    }
}
