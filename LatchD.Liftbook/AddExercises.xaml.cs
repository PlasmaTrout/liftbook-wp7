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
using System.Text;
using System.Globalization;

namespace WPLifts
{
    public partial class AddExercises : PhoneApplicationPage
    {
        WpLiftsDataFile file;
        DateTime currentDate;

       
        public AddExercises()
        {
            InitializeComponent();
           
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            string selectedDate = string.Empty;
            string protocol = string.Empty;

            if (NavigationContext.QueryString.TryGetValue("selectedDate", out selectedDate))
            {
                currentDate = DateTime.Parse(selectedDate);
            }

            //if (NavigationContext.QueryString.TryGetValue("protocol", out protocol))
            //{
            //    ProtocolBlock.Text = protocol;
            //}
            LiftCombo.ItemsSource = WpLiftsDataFile.ListExercises().Select(n => n.Name);
            file = IsolatedStorageHelper.LoadData();

            base.OnNavigatedTo(e);
        }

        //private void SetsSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    UpdateSelection();
        //}

        //private void WeightSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    UpdateSelection();
        //}

        private void UpdateSelection()
        {
            if (SetsSlider != null && WeightSlider != null)
            {
                int result = ((int)Math.Round(SetsSlider.Value));
                int weight = ((int)Math.Round(WeightSlider.Value)) / 5 * 5;
                int reps = ((int)Math.Round(RepsSlider.Value));

                SetsLabel.Text = result.ToString(CultureInfo.CurrentCulture) + " " + (result == 1 ? "set" : "sets");
                WeightLabel.Text = weight.ToString(CultureInfo.CurrentCulture) + " lbs";
                RepsLabel.Text = reps.ToString(CultureInfo.CurrentCulture) + " " + (reps == 1 ? "rep" : "reps");
            }
        }

        private void Save2Button_Click(object sender, EventArgs e)
        {
            
            int result = ((int)Math.Round(SetsSlider.Value));
            int weight = ((int)Math.Round(WeightSlider.Value)) / 5 * 5;
            int reps = ((int)Math.Round(RepsSlider.Value));

            StringBuilder sb = new StringBuilder();
            sb.Append(LiftCombo.SelectedItem.ToString());

            if (RadioWarmup.IsChecked.Value)
            {
                sb.Append(" Warmup");
            }

            if (RadioAssist.IsChecked.Value)
            {
                sb.Append(" Assistance");
            }


            file.Lifts.AddLift(currentDate.Date, sb.ToString(), LiftProtocol.Open, reps, weight, result);
            IsolatedStorageHelper.SaveData(file);

            string query = string.Format(CultureInfo.CurrentCulture,"/ViewWorkout.xaml?selectedDate={0}", currentDate.ToShortDateString());
            Uri uri = new Uri(query, UriKind.RelativeOrAbsolute);
            NavigationService.Navigate(uri);
        }

        private void SetsSlider_OnValueChanged(object sender, RoutedEventArgs e)
        {
            UpdateSelection();
        }

        private void WeightSlider_OnValueChanged(object sender, RoutedEventArgs e)
        {
            UpdateSelection();
        }

        private void RepsSlider_OnValueChanged(object sender, RoutedEventArgs e)
        {
            UpdateSelection();
        }
    }
}