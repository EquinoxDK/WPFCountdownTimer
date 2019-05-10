using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Countdown
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Classes.AppTimer Countdown;
        private string Filename;
        private static readonly Regex onlyNumbers = new Regex("[^0-9]+");

        public MainWindow()
        {
            InitializeComponent();
            Countdown = new Classes.AppTimer(200);
            Countdown.Tick += SaveList;
            Filename = "timer.txt";
            System.IO.File.Create(Filename);
        }

        private void SaveList(ElapsedEventArgs e)
        {
            string content = "";
            Dispatcher.Invoke((Action)(() =>
            {
                StringBuilder sb = new StringBuilder();
                foreach (TimerSheet item in TimerPanel.Children)
                {
                    sb.AppendLine($"{item.ContentText} {item.Time.Text}");
                }
                content = sb.ToString();
            }));

            try
            {
                string oldContent = System.IO.File.ReadAllText(Filename);
                if (!content.Equals(oldContent))
                {
                    System.IO.File.WriteAllText(Filename, content);
                }
            }
            catch (Exception)
            {

            }
        }

        private string CreateTime(TimeSpan timeLeft)
        {
            if (timeLeft > TimeSpan.Zero)
            {
                return timeLeft.ToString("hh\\:mm\\:ss");
            }
            else
            {
                return "Done";
            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(HoursTxt.Text, out int hour);
            int.TryParse(MinuteTxt.Text, out int min);
            int.TryParse(SecondsTxt.Text, out int sec);
            TimeSpan countdownTime = new TimeSpan(hour, min, sec);
            TimerSheet n = new TimerSheet(DateTime.Now, countdownTime, CreateTime);
            Countdown.Tick += n.Tickhandler;

            n.Time.Text = CreateTime(countdownTime);
            n.Text.Text = TextTxt.Text;
            n.ContentText = TextTxt.Text;


            TimerPanel.Children.Add(n);
            //TimerPanel.Children.Add(Countdown.Start(new TimeSpan(hour, min, sec), TextTxt.Text));
            HoursTxt.Text = "";
            MinuteTxt.Text = "";
            SecondsTxt.Text = "";
            TextTxt.Text = "";
            //HoursTxt.IsEnabled = false;
            //MinuteTxt.IsEnabled = false;
            //SecondsTxt.IsEnabled = false;
        }

        private void HoursTxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = onlyNumbers.IsMatch(e.Text);

        }

        private void HoursTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!(sender as TextBox).Text.Equals("") && int.Parse((sender as TextBox).Text) > 23)
            {
                (sender as TextBox).Text = "23";
            }
        }

        private void MinuteTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!(sender as TextBox).Text.Equals("") && int.Parse((sender as TextBox).Text) > 59)
            {
                (sender as TextBox).Text = "59";
            }
        }

        private void MinuteTxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = onlyNumbers.IsMatch(e.Text);
        }

        private void SecondsTxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = onlyNumbers.IsMatch(e.Text);
        }

        private void SecondsTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!(sender as TextBox).Text.Equals("") && int.Parse((sender as TextBox).Text) > 59)
            {
                (sender as TextBox).Text = "59";
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //TimerPanel.Children.Add(new TimerSheet());
        }
    }
}
