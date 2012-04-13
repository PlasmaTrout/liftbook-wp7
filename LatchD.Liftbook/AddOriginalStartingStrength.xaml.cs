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
    public partial class AddOriginalStartingStrength : PhoneApplicationPage
    {
        WpLiftsDataFile file;
        DateTime selectedDate;

        private const string FIRST_LIFT_A = "Back Squat";
        private const string SECOND_LIFT_A = "Bench Press";
        private const string THIRD_LIFT_A = "Deadlift";

        private const string FIRST_LIFT_B = "Back Squat";
        private const string SECOND_LIFT_B = "Overhead Press";
        private const string THIRD_LIFT_B = "Power Clean";

        public AddOriginalStartingStrength()
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
                FirstLabel.Text = FIRST_LIFT_A + " Weight (3x5):";
                SecondLabel.Text = SECOND_LIFT_A + " Weight (3x5):";
                ThirdLabel.Text = THIRD_LIFT_A + " Weight (1x5):";
            }

        }

        private void WorkoutBRadio_Checked(object sender, RoutedEventArgs e)
        {
            if (FirstLabel != null)
            {
                FirstLabel.Text = FIRST_LIFT_B + " Weight (3x5):";
                SecondLabel.Text = SECOND_LIFT_B + " Weight (3x5):";
                ThirdLabel.Text = THIRD_LIFT_B + " Weight (5x3):";
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
                    file.Lifts.AddLift(this.selectedDate, FIRST_LIFT_A + " Warmup", LiftProtocol.StartingStrength, 5, 45.0, 2);
                    file.Lifts.AddWarmupLift(this.selectedDate, FIRST_LIFT_A + " Warmup", LiftProtocol.StartingStrength, weight1, 40, 5);
                    file.Lifts.AddWarmupLift(this.selectedDate, FIRST_LIFT_A + " Warmup", LiftProtocol.StartingStrength, weight1, 60, 3);
                    file.Lifts.AddWarmupLift(this.selectedDate, FIRST_LIFT_A + " Warmup", LiftProtocol.StartingStrength, weight1, 80, 2);
                }

                file.Lifts.AddLift(this.selectedDate, FIRST_LIFT_A, LiftProtocol.StartingStrength, 5, weight1, 3);

                if (WarmupsCheckBox.IsChecked.Value)
                {
                    file.Lifts.AddLift(this.selectedDate, SECOND_LIFT_A + " Warmup", LiftProtocol.StartingStrength, 5, 45.0, 2);
                    file.Lifts.AddWarmupLift(this.selectedDate, SECOND_LIFT_A + " Warmup", LiftProtocol.StartingStrength, weight2, 50, 3);
                    file.Lifts.AddWarmupLift(this.selectedDate, SECOND_LIFT_A + " Warmup", LiftProtocol.StartingStrength, weight2, 70, 3);
                    file.Lifts.AddWarmupLift(this.selectedDate, SECOND_LIFT_A + " Warmup", LiftProtocol.StartingStrength, weight2, 90, 2);
                }

                file.Lifts.AddLift(this.selectedDate, SECOND_LIFT_A, LiftProtocol.StartingStrength, 5, weight2, 3);

                if (WarmupsCheckBox.IsChecked.Value)
                {
                    //file.Lifts.AddLift(this.selectedDate, THIRD_LIFT_A + " Warmup", LiftProtocol.Stronglifts5x5, 5, 45.0, 2);
                    file.Lifts.AddWarmupLift(this.selectedDate, THIRD_LIFT_A + " Warmup", LiftProtocol.StartingStrength, weight3, 40, 5);
                    file.Lifts.AddWarmupLift(this.selectedDate, THIRD_LIFT_A + " Warmup", LiftProtocol.StartingStrength, weight3, 40, 5);
                    file.Lifts.AddWarmupLift(this.selectedDate, THIRD_LIFT_A + " Warmup", LiftProtocol.StartingStrength, weight3, 60, 3);
                    file.Lifts.AddWarmupLift(this.selectedDate, THIRD_LIFT_A + " Warmup", LiftProtocol.StartingStrength, weight3, 85, 2);
                }

                file.Lifts.AddLift(this.selectedDate, THIRD_LIFT_A, LiftProtocol.StartingStrength, 5, weight3, 1);
            }
            else
            {
                if (WarmupsCheckBox.IsChecked.Value)
                {
                    file.Lifts.AddLift(this.selectedDate, FIRST_LIFT_B + " Warmup", LiftProtocol.StartingStrength, 5, 45.0, 2);
                    file.Lifts.AddWarmupLift(this.selectedDate, FIRST_LIFT_B + " Warmup", LiftProtocol.StartingStrength, weight1, 40, 5);
                    file.Lifts.AddWarmupLift(this.selectedDate, FIRST_LIFT_B + " Warmup", LiftProtocol.StartingStrength, weight1, 60, 3);
                    file.Lifts.AddWarmupLift(this.selectedDate, FIRST_LIFT_B + " Warmup", LiftProtocol.StartingStrength, weight1, 80, 2);
                }
                
                file.Lifts.AddLift(this.selectedDate, FIRST_LIFT_B, LiftProtocol.StartingStrength, 5, weight1, 3);
                
                if (WarmupsCheckBox.IsChecked.Value)
                {
                    file.Lifts.AddLift(this.selectedDate, SECOND_LIFT_B + " Warmup", LiftProtocol.StartingStrength, 5, 45.0, 2);
                    file.Lifts.AddWarmupLift(this.selectedDate, SECOND_LIFT_B + " Warmup", LiftProtocol.StartingStrength, weight2, 55, 5);
                    file.Lifts.AddWarmupLift(this.selectedDate, SECOND_LIFT_B + " Warmup", LiftProtocol.StartingStrength, weight2, 70, 3);
                    file.Lifts.AddWarmupLift(this.selectedDate, SECOND_LIFT_B + " Warmup", LiftProtocol.StartingStrength, weight2, 85, 2);
                }
                
                file.Lifts.AddLift(this.selectedDate, SECOND_LIFT_B, LiftProtocol.StartingStrength, 5, weight2, 3);
                
                if (WarmupsCheckBox.IsChecked.Value)
                {
                    file.Lifts.AddLift(this.selectedDate, THIRD_LIFT_B + " Warmup", LiftProtocol.StartingStrength, 5, 45.0, 2);
                    file.Lifts.AddWarmupLift(this.selectedDate, THIRD_LIFT_B + " Warmup", LiftProtocol.StartingStrength, weight2, 55, 5);
                    file.Lifts.AddWarmupLift(this.selectedDate, THIRD_LIFT_B + " Warmup", LiftProtocol.StartingStrength, weight2, 70, 3);
                    file.Lifts.AddWarmupLift(this.selectedDate, THIRD_LIFT_B + " Warmup", LiftProtocol.StartingStrength, weight2, 85, 2);
                }

                
                file.Lifts.AddLift(this.selectedDate, THIRD_LIFT_B, LiftProtocol.StartingStrength, 3, weight3, 5);
            }

            IsolatedStorageHelper.SaveData(file);

            string query = string.Format(CultureInfo.CurrentCulture, "/ViewWorkout.xaml?selectedDate={0}", selectedDate);
            Uri uri = new Uri(query, UriKind.RelativeOrAbsolute);
            NavigationService.Navigate(uri);
        }
    }
}