using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TimeLogger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static DateTime StartTime;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartTimer(object sender, RoutedEventArgs e)
        {
            // clear all our text boxes except Project Code on timer start
            StartTimeText.Clear();
            StopTimeText.Clear();
            DayOfWeekText.Clear();
            ElapsedTimeText.Clear();

            // get the starting time as DateTime object
           DateTime  StartingTime = GetStartTime();

            // write out a bunch of strings to the various text boxes
            StartTimeText.AppendText(StartingTime.ToString("HH: mm:ss"));
            String WeekDay = StartingTime.DayOfWeek.ToString();
            DayOfWeekText.AppendText(WeekDay);
        }

    

        public DateTime GetStartTime()
        {
            // get a start time as a DateTime object
            StartTime = DateTime.Now;
            return StartTime;
            
        }


        public void StopTimer(object sender, RoutedEventArgs e)
        {


            GetStopTime(StartTime);
            

        }

        private void GetStopTime(DateTime StartTime)
        {
            // get a finish time
            DateTime FinishTime = DateTime.Now;
            StopTimeText.AppendText(FinishTime.ToString("HH: mm:ss"));

            // get a TimeSpan object by subtracting finish from start time
            // cast the TimeSpan properties to strings and write out the result
            TimeSpan ExpiredTime = (FinishTime - StartTime);
            String ExpiredHours = ExpiredTime.Hours.ToString();
            String ExpiredMinutes = ExpiredTime.Minutes.ToString();
            String ExpiredSeconds = ExpiredTime.Seconds.ToString();

            String Result = ExpiredHours + ":" + ExpiredMinutes + ":" + ExpiredSeconds;
            ElapsedTimeText.AppendText(Result);
        }

        private void DayOfWeekText_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void StopTimeText_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }


}
