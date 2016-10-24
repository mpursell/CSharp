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
        public const String CsvHeaders = "Day,TaskName,ProjectCode,Time";

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

            if (!File.Exists(LogPath))
            {
                // create the log file
                CreateLog(LogPath);

            }

            
    
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

            // convert the minutes to int so we can turn them into 
            // decimal fractions of hours
            double Minutes = int.Parse(ExpiredMinutes);
            double MinutesAsDecimal = Minutes / 60 * 10;
            MinutesAsDecimal = Math.Round(MinutesAsDecimal, 0);
            String MinutesAsString = MinutesAsDecimal.ToString(); 
            


            String ExpiredSeconds = ExpiredTime.Seconds.ToString();

            String PCode = ProjectCodeText.Text;
            String TaskName = TaskNameText.Text;

            String Result = ExpiredHours + ":" + ExpiredMinutes;
            String LogResult = WeekDay + "," + TaskName + "," + PCode + "," + ExpiredHours + "." + MinutesAsString;
            ElapsedTimeText.AppendText(Result);

            

            Logger(LogResult);
        }

        private void CreateLog(String LogPath)
        {
            // Create the log file if it doesn't already exist

            if (!File.Exists(LogPath))
            {
                try
                {

                    // make sure to close the file after creation so we can immediately
                    // append the csv headers to it
                    File.Create(LogPath).Close();


                }
                catch (Exception e)
                {
                    String CaughtException = e.Message.ToString();
                    

                    // TODO: Throw the error to console or window output.
                    // decide what options to give user
                }


                File.WriteAllText(LogPath, CsvHeaders + Environment.NewLine);


            }
        }

        private void Logger(String Text)
        {

            // TODO - Check if today is Monday, and last write time on log file 
            // was Friday.  If it was, delete it so we can start again.  If not,
            // carry on appending the text


            File.AppendAllText(LogPath, Text + Environment.NewLine);
            
        }


        private void ClearAll(object sender, RoutedEventArgs e)
        {
            StartTimeText.Clear();
            StopTimeText.Clear();
            DayOfWeekText.Clear();
            ElapsedTimeText.Clear();
        }

      
    }

    // TODO: simplify the logger into a single class with Create and Write Methods
    public class Logger
    {
        public void CreateLog(String LogPath)
        {

        }

        public void WriteLog(String LogPath)
        {

        }
    }

}
