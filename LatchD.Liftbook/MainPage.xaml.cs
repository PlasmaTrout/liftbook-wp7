using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using WPLiftsLibrary;
using System.Reflection;
using WPLifts.Controls;
using System.Globalization;

namespace WPLifts
{
    
    public partial class MainPage : PhoneApplicationPage
    {
        private WpLiftsDataFile file;
        private int currentRange = 7;

        #region Constructor

        public MainPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Binders

        public void BindSchedule(WpLiftsDataFile file)
        {
          
                ContentPanel.Items.Clear();

                foreach (var date in file.GetScheduledDates(currentRange))
                {
                    ScheduledDateControl control = new ScheduledDateControl();
                    control.Width = 460.00;
                    control.Margin = new Thickness(5.0, 5.0, 5.0, 5.0);
                    control.Date = date;
                    control.Height = 200.0;
                    control.Opacity = 0.8;

                    DateTime pdate = DateTime.Parse(date, CultureInfo.CurrentCulture);

                    bool result = file.Lifts.CheckForCompletion(pdate) && file.CardioRuns.CheckForCompletion(pdate);

                    control.LiftNumberRun.Text = file.Lifts.GetLiftCountForDate(pdate).ToString();
                    control.RunNumberRun.Text = file.CardioRuns.GetCardioCountForDate(pdate).ToString();
                    control.ProtocolTextBlock.Text = file.Lifts.GetBestProtocolGuess(pdate);

                    control.IsCompleted = result;

                    ContentPanel.Items.Add(control);
                }

            
        }

        private void SelectToday()
        {
           
            foreach (var child in ContentPanel.Items)
            {
                ScheduledDateControl childc = child as ScheduledDateControl;
                AppSettings settings = new AppSettings();

                if (childc.DateTextBlock.Text.Equals(DateTime.Now.ToString(settings.ScheduleDateFormatString,CultureInfo.CurrentCulture)))
                {
                    ContentPanel.ScrollIntoView(childc);
                    childc.Opacity = 1;
                    ContentPanel.SelectedIndex = ContentPanel.Items.IndexOf(childc);
                }
            }
        }

        private void ScrollToday()
        {
            foreach (var child in ContentPanel.Items)
            {
                ScheduledDateControl childc = child as ScheduledDateControl;
                AppSettings settings = new AppSettings();

                if (childc.DateTextBlock.Text.Equals(DateTime.Now.ToString(settings.ScheduleDateFormatString, CultureInfo.CurrentCulture)))
                {
                    ContentPanel.ScrollIntoView(childc);
                    childc.Opacity = 1;
                    //ContentPanel.SelectedIndex = ContentPanel.Items.IndexOf(childc);
                }
            }
        }

        #endregion

        #region Events

        private void ContentPanel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ScheduledDateControl control = ContentPanel.SelectedItem as ScheduledDateControl;

            if (control != null)
            {
                foreach (var child in ContentPanel.Items)
                {
                    ScheduledDateControl childc = child as ScheduledDateControl;

                    if (childc != null)
                    {
                        childc.Opacity = 0.6;
                    }
                }

                control.Opacity = 1.0;

                string query = string.Format(CultureInfo.CurrentCulture,"/ViewWorkout.xaml?selectedDate={0}", control.Date);
                Uri uri = new Uri(query, UriKind.RelativeOrAbsolute);
                NavigationService.Navigate(uri);

                ContentPanel.SelectedIndex = -1;
            }
        }

        private void TodayHyperlink_Click(object sender, RoutedEventArgs e)
        {
            SelectToday();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            file = IsolatedStorageHelper.LoadData();

            if (file == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                BindSchedule(file);
            }

            base.OnNavigatedTo(e);
        }

        private void AddWorkoutButton_Click(object sender, EventArgs e)
        {
            Uri uri = new Uri("/AddNewSchedule.xaml", UriKind.RelativeOrAbsolute);
            NavigationService.Navigate(uri);
        }

        private void ShareButton_Click(object sender, EventArgs e)
        {
            Uri uri = new Uri("/ShareData.xaml", UriKind.RelativeOrAbsolute);
            NavigationService.Navigate(uri);
        }

        private void TodaySelect_Click(object sender, EventArgs e)
        {
            SelectToday();
        }

        private void AddJogButton_Click(object sender, EventArgs e)
        {
            Uri uri = new Uri("/AddCardio.xaml", UriKind.RelativeOrAbsolute);
            NavigationService.Navigate(uri);
        }

        private void AllDaysSelect_Click(object sender, EventArgs e)
        {
            this.currentRange = 365;
            RangeLabel.Text = "Range: Full Year (182 Prior + Today + 182 Future)";
            BindSchedule(file);
        }

        private void SevenDaySelect_Click(object sender, EventArgs e)
        {
            this.currentRange = 7;
            RangeLabel.Text = "Range: 7 Days (3 Prior + Today + 3 Future)";
            BindSchedule(file);
        }

        private void Calc_Click(object sender, EventArgs e)
        {
            Uri uri = new Uri("/OneRepMaxCalculator.xaml", UriKind.RelativeOrAbsolute);
            NavigationService.Navigate(uri);
        }

        #endregion

        private void TodayScroll_Click(object sender, EventArgs e)
        {
            ScrollToday();
        }

        

       
      

    }
}