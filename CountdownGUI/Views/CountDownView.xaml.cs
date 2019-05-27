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
            DataContext = new CountdownViewModel();
        }
    }
}
