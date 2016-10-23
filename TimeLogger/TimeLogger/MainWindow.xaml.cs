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
using System.IO;

namespace TimeLogger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static DateTime StartTime;
        public static String WeekDay;
        public String LogPath = "C:\\Users\\" + Environment.UserName + "\\Documents\\TimeLog.csv";

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
            DateTime StartingTime = GetStartTime();

            // write out a bunch of strings to the various text boxes
            StartTimeText.AppendText(StartingTime.ToString("HH: mm:ss"));
            WeekDay = StartingTime.DayOfWeek.ToString();
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

            String PCode = ProjectCodeText.Text;
            String TaskName = TaskNameText.Text;

            String Result = ExpiredHours + ":" + ExpiredMinutes;
            String LogResult = WeekDay + "," + TaskName + "," + PCode + "," + ExpiredHours + ":" + ExpiredMinutes;
            ElapsedTimeText.AppendText(Result);

            

            Logger(LogResult);
        }

        private void Logger(String Text)
        {

            // TODO - Check if today is Monday, and last write time on log file 
            // was Friday.  If it was, delete it so we can start again.  If not,
            // carry on appending the text

            // check the csv exists

            

            if (!File.Exists(LogPath))
            {
                try
                {
                    // TODO: Not working as expected, doesn't log until after second stop is initiated.

                    File.Create(LogPath);
                    File.AppendAllText(LogPath, "Day,TaskName,Code,Time" + Environment.NewLine);
                    File.AppendAllText(LogPath, Text + Environment.NewLine);
                    
                }
                catch (Exception e)
                {
                    String CaughtException = e.Message.ToString();
                    
                    // TODO: Throw the error to console or window output.
                    // decide what options to give user
                }


            }
            else if (File.Exists(LogPath))
            {
                File.AppendAllText(LogPath, Text + Environment.NewLine);
            }


        }


        private void ClearAll(object sender, RoutedEventArgs e)
        {
            StartTimeText.Clear();
            StopTimeText.Clear();
            DayOfWeekText.Clear();
            ElapsedTimeText.Clear();
        }



        private void DayOfWeekText_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void StopTimeText_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

       

       

        
    }

}
