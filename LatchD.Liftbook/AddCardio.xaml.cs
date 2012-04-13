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
using System.Globalization;

namespace WPLifts
{
    public partial class AddCardio : PhoneApplicationPage
    {
        private double miles;
        private int hours;
        private int minutes;
        private int seconds;

        public AddCardio()
        {
            InitializeComponent();
        }

        private void BindExercises()
        {
            IEnumerable<string> types = new List<string>(new string[] { "Run", "Jog", "Walk", "Sprint", "Treadmill","Eliptical","Cybex","Other" });
            CardioTypePicker.ItemsSource = types;
        }

        private void HoursTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            try
            {
                miles = double.Parse(MilesTextBox.Text, CultureInfo.CurrentCulture);
                hours = int.Parse(HoursTextBox.Text, CultureInfo.CurrentCulture);
                minutes = int.Parse(MinutesTextBox.Text, CultureInfo.CurrentCulture);
                seconds = int.Parse(SecondsTextBox.Text, CultureInfo.CurrentCulture);
            }
            catch (FormatException)
            {
                MessageBox.Show(UITextResources.NUMBER_ENTRY_MESSAGE);
                UIHelper.WashTextField(MilesTextBox);
                UIHelper.WashTextField(HoursTextBox);
                UIHelper.WashTextField(MinutesTextBox);
                UIHelper.WashTextField(SecondsTextBox);
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            string selectedDate = string.Empty;
           
            if (NavigationContext.QueryString.TryGetValue("selectedDate", out selectedDate))
            {
                DatePicker.Value = DateTime.Parse(selectedDate, CultureInfo.CurrentCulture);
            }

            BindExercises();

            this.MilesTextBox.Text = this.miles.ToString(CultureInfo.CurrentCulture);
            this.HoursTextBox.Text = this.hours.ToString(CultureInfo.CurrentCulture);
            this.MinutesTextBox.Text = this.minutes.ToString(CultureInfo.CurrentCulture);
            this.SecondsTextBox.Text = this.seconds.ToString(CultureInfo.CurrentCulture);

            base.OnNavigatedTo(e);
        }

        private void CardioSaveButton_Click(object sender, EventArgs e)
        {
            WpLiftsDataFile file = IsolatedStorageHelper.LoadData();

            CardioRun run = new CardioRun()
            {
                Date = DatePicker.Value.Value,
                Distance = miles,
                DurationHours = hours,
                DurationMinutes = minutes,
                DurationSeconds = seconds,
                CardioType = CardioTypePicker.SelectedItem.ToString()
            };

            file.CardioRuns.Add(run);
            IsolatedStorageHelper.SaveData(file);

            string selectedDate = DatePicker.Value.Value.ToShortDateString();
            string query = string.Format(CultureInfo.CurrentCulture, "/ViewWorkout.xaml?selectedDate={0}", selectedDate);
            Uri uri = new Uri(query, UriKind.RelativeOrAbsolute);
            NavigationService.Navigate(uri);
        }

        private void MilesTextBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MilesTextBox.SelectAll();
        }

        private void HoursTextBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            HoursTextBox.SelectAll();
        }

        private void MinutesTextBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MinutesTextBox.SelectAll();
        }

        private void SecondsTextBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SecondsTextBox.SelectAll();
        }

        
    }
}