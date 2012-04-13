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
    public partial class Add531Lifts : PhoneApplicationPage
    {
        WpLiftsDataFile file;
        DateTime selectedDate;

        string[] mainLIfts = { "Military Press","Conventional Deadlift","Sumo Deadlift","Bench Press",
                                       "Back Squat","Front Squat" };

        string[] assistanceLifts = { "Military Press","Deadlift","Bench Press",
                                       "Back Squat","Dips","Chins","Pushups",
                                       "Dumbell Rows","Kroc Rows","Barbell Rows",
                                       "Barbell Shrugs","Dumbell Bench","Dumbell Military Press",
                                       "Dumbell Incline Press","Barbell Incline Press","Lunges",
                                       "Step-ups","Leg Press","Leg Curl","Back Raise","Good Morning",
                                       "Glute Ham Raise","Sit-Ups","Dumbell Side Bends","Hanging Leg Raises",
                                       "Abdominal Wheel" };


        public Add531Lifts()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            string selectedDate = string.Empty;
            string protocol = string.Empty;

            if (NavigationContext.QueryString.TryGetValue("selectedDate", out selectedDate))
            {
                this.selectedDate = DateTime.Parse(selectedDate,CultureInfo.CurrentCulture);
            }
            else
            {
                this.selectedDate = DateTime.Now;
            }

            MainListPicker.ItemsSource = mainLIfts;
            AssistancePicker1.ItemsSource = assistanceLifts;
            AssistancePicker2.ItemsSource = assistanceLifts;

            file = IsolatedStorageHelper.LoadData();

            base.OnNavigatedTo(e);
        }

        private void WeightSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (WeightControl != null)
            {
                int weight = ((int)Math.Round(WeightControl.Value)) / 5 * 5;
                MaxWeightLabel.Text = "Max Weight (" + weight.ToString(CultureInfo.CurrentCulture) + " lbs)";
            }
        }

        //private void WeekSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    if (WeekSlider != null)
        //    {
        //        WeekLabel.Text = "Week " + WeekSlider.Value.ToString(CultureInfo.CurrentCulture);
        //    }
        //}

        //private void Assistance1WeightSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    if (Assistance1WeightSlider != null)
        //    {
        //        int weight = ((int)Math.Round(Assistance1WeightSlider.Value)) / 5 * 5;
        //        Assistance1WeightLabel.Text = "Weight " + weight.ToString(CultureInfo.CurrentCulture) + " lbs";
        //    }
        //}

        //private void Assistance2WeightSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    if (Assistance2WeightSlider != null)
        //    {
        //        int weight = ((int)Math.Round(Assistance2WeightSlider.Value)) / 5 * 5;
        //        Assistance2WeightLabel.Text = "Weight " + weight.ToString(CultureInfo.CurrentCulture) + " lbs";
        //    }
        //}

        private void Save2Button_Click(object sender, EventArgs e)
        {
            int weight = ((int)Math.Round(WeightControl.Value)) / 5 * 5;
            int aweight1 = ((int)Math.Round(Assistance1Control.Value)) / 5 * 5;
            int aweight2 = ((int)Math.Round(Assitance2Control.Value)) / 5 * 5;

            int week = 4;

            if (Week1Radio.IsChecked.Value)
            {
                week = 1;
            }
            else if (Week2Radio.IsChecked.Value)
            {
                week = 2;
            }
            else if (Week3Radio.IsChecked.Value)
            {
                week = 3;
            }

            file.Lifts.Add531Lift(selectedDate, MainListPicker.SelectedItem.ToString(),
                weight, AssistancePicker1.SelectedItem.ToString(), aweight1,
                AssistancePicker2.SelectedItem.ToString(), aweight2, week);

            IsolatedStorageHelper.SaveData(file);

            string query = string.Format(CultureInfo.CurrentCulture, "/ViewWorkout.xaml?selectedDate={0}", selectedDate);
            Uri uri = new Uri(query, UriKind.RelativeOrAbsolute);
            NavigationService.Navigate(uri);
        }

        private void WeightControl_OnValueChanged(object sender, RoutedEventArgs e)
        {
            if (WeightControl != null)
            {
                int weight = ((int)Math.Round(WeightControl.Value)) / 5 * 5;
                MaxWeightLabel.Text = "Max Weight (" + weight.ToString(CultureInfo.CurrentCulture) + " lbs)";
            }
        }

        private void Assistance1Control_OnValueChanged(object sender, RoutedEventArgs e)
        {
            if (Assistance1Control != null)
            {
                int weight = ((int)Math.Round(Assistance1Control.Value)) / 5 * 5;
                Assistance1WeightLabel.Text = "Weight " + weight.ToString(CultureInfo.CurrentCulture) + " lbs";
                
            }
        }

        private void Assitance2Control_OnValueChanged(object sender, RoutedEventArgs e)
        {
            if (Assitance2Control != null)
            {
                int weight = ((int)Math.Round(Assitance2Control.Value)) / 5 * 5;
                Assistance2WeightLabel.Text = "Weight " + weight.ToString(CultureInfo.CurrentCulture) + " lbs";

            }
        }

        private void Week1Radio_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton button = sender as RadioButton;

            if (button.Name.Equals("Week4Radio"))
            {
                if (Assistance1Control != null && AssistancePicker1 != null)
                {
                    Assistance1Control.IsEnabled = false;
                    AssistancePicker1.IsEnabled = false;
                    AssistancePicker1.Opacity = 0.3;
                    Assistance1WeightLabel.Opacity = 0.3;
                    Assistance1.Opacity = 0.3;
                }

                if (Assitance2Control != null && AssistancePicker2 != null)
                {
                    Assitance2Control.IsEnabled = false;
                    AssistancePicker2.IsEnabled = false;
                    AssistancePicker2.Opacity = 0.3;
                    Assistance2WeightLabel.Opacity = 0.3;
                    Assistance2.Opacity = 0.3;
                }
            }
            else
            {
                if (Assistance1Control != null && AssistancePicker1 != null)
                {
                    Assistance1Control.IsEnabled = true;
                    AssistancePicker1.IsEnabled = true;
                    AssistancePicker1.Opacity = 1.0;
                    Assistance1WeightLabel.Opacity = 1.0;
                    Assistance1.Opacity = 1.0;
                }

                if (Assitance2Control != null && AssistancePicker2 != null)
                {
                    Assitance2Control.IsEnabled = true;
                    AssistancePicker2.IsEnabled = true;
                    AssistancePicker2.Opacity = 1.0;
                    Assistance2WeightLabel.Opacity = 1.0;
                    Assistance2.Opacity = 1.0;
                }
            }
        }
    }
}