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
using System.Globalization;
using WPLiftsLibrary;

namespace WPLifts
{
    public partial class AddFiveByFiveBeginner : PhoneApplicationPage
    {

        WpLiftsDataFile file;

        private const string FIRST_LIFT_A = "Back Squat";
        private const string SECOND_LIFT_A = "Bench Press";
        private const string THIRD_LIFT_A = "Barbell Row";

        private const string FIRST_LIFT_B = "Back Squat";
        private const string SECOND_LIFT_B = "Overhead Press";
        private const string THIRD_LIFT_B = "Deadlift";

        private DateTime selectedDate;

        public AddFiveByFiveBeginner()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            string selectedDate = string.Empty;
            string protocol = string.Empty;

            if (NavigationContext.QueryString.TryGetValue("selectedDate", out selectedDate))
            {
                this.selectedDate = DateTime.Parse(selectedDate, CultureInfo.CurrentCulture);
            }
            else
            {
                this.selectedDate = DateTime.Now;
            }

            file = IsolatedStorageHelper.LoadData();

            base.OnNavigatedTo(e);
        }

        private void WorkoutARadio_Checked(object sender, RoutedEventArgs e)
        {
            if (FirstLabel != null)
            {
                FirstLabel.Text = FIRST_LIFT_A + " Weight (5x5):";
                SecondLabel.Text = SECOND_LIFT_A + " Weight (5x5):";
                ThirdLabel.Text = THIRD_LIFT_A + " Weight (5x5):";
            }

        }

        private void WorkoutBRadio_Checked(object sender, RoutedEventArgs e)
        {
            if (FirstLabel != null)
            {
                FirstLabel.Text = FIRST_LIFT_B + " Weight (5x5):";
                SecondLabel.Text = SECOND_LIFT_B + " Weight (5x5):";
                ThirdLabel.Text = THIRD_LIFT_B + " Weight (1x5):";
            }
        }

        private void Save2Button_Click(object sender, EventArgs e)
        {
            int weight1 = ((int)Math.Round(FirstWeight.Value)) / 5 * 5;
            int weight2 = ((int)Math.Round(SecondWeight.Value)) / 5 * 5;
            int weight3 = ((int)Math.Round(ThirdWeight.Value)) / 5 * 5;

            if (WorkoutARadio.IsChecked.Value)
            {
                if (WarmupsCheckBox.IsChecked.Value)
                {
                    file.Lifts.AddLift(this.selectedDate, FIRST_LIFT_A + " Warmup", LiftProtocol.Stronglifts5x5, 5, 45.0, 2);
                    file.Lifts.AddWarmupLift(this.selectedDate, FIRST_LIFT_A + " Warmup", LiftProtocol.Stronglifts5x5, weight1, 50, 3);
                    file.Lifts.AddWarmupLift(this.selectedDate, FIRST_LIFT_A + " Warmup", LiftProtocol.Stronglifts5x5, weight1, 60, 3);
                    file.Lifts.AddWarmupLift(this.selectedDate, FIRST_LIFT_A + " Warmup", LiftProtocol.Stronglifts5x5, weight1, 70, 2);
                }

                file.Lifts.AddLift(this.selectedDate, FIRST_LIFT_A, LiftProtocol.Stronglifts5x5, 5, weight1, 5);

                if (WarmupsCheckBox.IsChecked.Value)
                {   
                    file.Lifts.AddLift(this.selectedDate, SECOND_LIFT_A + " Warmup", LiftProtocol.Stronglifts5x5, 5, 45.0, 2);
                    file.Lifts.AddWarmupLift(this.selectedDate, SECOND_LIFT_A + " Warmup", LiftProtocol.Stronglifts5x5, weight2, 50, 3);
                    file.Lifts.AddWarmupLift(this.selectedDate, SECOND_LIFT_A + " Warmup", LiftProtocol.Stronglifts5x5, weight2, 60, 3);
                    file.Lifts.AddWarmupLift(this.selectedDate, SECOND_LIFT_A + " Warmup", LiftProtocol.Stronglifts5x5, weight2, 70, 2);
                }

                file.Lifts.AddLift(this.selectedDate, SECOND_LIFT_A, LiftProtocol.Stronglifts5x5, 5, weight2, 5);

                if (WarmupsCheckBox.IsChecked.Value)
                {
                    file.Lifts.AddLift(this.selectedDate, THIRD_LIFT_A + " Warmup", LiftProtocol.Stronglifts5x5, 5, 45.0, 2);
                    file.Lifts.AddWarmupLift(this.selectedDate, THIRD_LIFT_A + " Warmup", LiftProtocol.Stronglifts5x5, weight3, 50, 3);
                    file.Lifts.AddWarmupLift(this.selectedDate, THIRD_LIFT_A + " Warmup", LiftProtocol.Stronglifts5x5, weight3, 60, 3);
                    file.Lifts.AddWarmupLift(this.selectedDate, THIRD_LIFT_A + " Warmup", LiftProtocol.Stronglifts5x5, weight3, 70, 2);
                }
               
                file.Lifts.AddLift(this.selectedDate, THIRD_LIFT_A, LiftProtocol.Stronglifts5x5, 5, weight3, 5);
            }
            else
            {
                if (WarmupsCheckBox.IsChecked.Value)
                {
                    file.Lifts.AddLift(this.selectedDate, FIRST_LIFT_B + " Warmup", LiftProtocol.Stronglifts5x5, 5, 45.0, 2);
                    file.Lifts.AddWarmupLift(this.selectedDate, FIRST_LIFT_B + " Warmup", LiftProtocol.Stronglifts5x5, weight1, 50, 3);
                    file.Lifts.AddWarmupLift(this.selectedDate, FIRST_LIFT_B + " Warmup", LiftProtocol.Stronglifts5x5, weight1, 60, 3);
                    file.Lifts.AddWarmupLift(this.selectedDate, FIRST_LIFT_B + " Warmup", LiftProtocol.Stronglifts5x5, weight1, 70, 2);
                }

                file.Lifts.AddLift(this.selectedDate, FIRST_LIFT_B, LiftProtocol.Stronglifts5x5, 5, weight1, 5);

                if (WarmupsCheckBox.IsChecked.Value)
                {
                    file.Lifts.AddLift(this.selectedDate, SECOND_LIFT_B + " Warmup", LiftProtocol.Stronglifts5x5, 5, 45.0, 2);
                    file.Lifts.AddWarmupLift(this.selectedDate, SECOND_LIFT_B + " Warmup", LiftProtocol.Stronglifts5x5, weight2, 50, 3);
                    file.Lifts.AddWarmupLift(this.selectedDate, SECOND_LIFT_B + " Warmup", LiftProtocol.Stronglifts5x5, weight2, 60, 3);
                    file.Lifts.AddWarmupLift(this.selectedDate, SECOND_LIFT_B + " Warmup", LiftProtocol.Stronglifts5x5, weight2, 70, 2);
                }

                file.Lifts.AddLift(this.selectedDate, SECOND_LIFT_B, LiftProtocol.Stronglifts5x5, 5, weight2, 5);             

                if (WarmupsCheckBox.IsChecked.Value)
                {
                    //file.Lifts.AddLift(this.selectedDate, THIRD_LIFT_A + " Warmup", LiftProtocol.Stronglifts5x5, 5, 45.0, 2);
                    file.Lifts.AddWarmupLift(this.selectedDate, THIRD_LIFT_B + " Warmup", LiftProtocol.Stronglifts5x5, weight2, 60, 3);
                    file.Lifts.AddWarmupLift(this.selectedDate, THIRD_LIFT_B + " Warmup", LiftProtocol.Stronglifts5x5, weight2, 70, 3);
                    file.Lifts.AddWarmupLift(this.selectedDate, THIRD_LIFT_B + " Warmup", LiftProtocol.Stronglifts5x5, weight2, 80, 2);

                }

                file.Lifts.AddLift(this.selectedDate, THIRD_LIFT_B, LiftProtocol.Stronglifts5x5, 5, weight3, 1);
            }

            IsolatedStorageHelper.SaveData(file);

            string query = string.Format(CultureInfo.CurrentCulture, "/ViewWorkout.xaml?selectedDate={0}", selectedDate);
            Uri uri = new Uri(query, UriKind.RelativeOrAbsolute);
            NavigationService.Navigate(uri);
        }

        
    }
}