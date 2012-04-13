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
using Microsoft.Phone.Tasks;

namespace WPLifts
{
    public partial class AddNewSchedule : PhoneApplicationPage
    {
        #region Constructor

        public AddNewSchedule()
        {
            InitializeComponent();
            ProtocolCombo.SelectedIndex = 0;
            //DateTextBlock.Text = SelectedDatePicker.Value.Value.Date.ToShortDateString();
        }

        #endregion

        #region Events

        private void ProtocolCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProtocolCombo != null)
            {
                ComboBoxItem item = ProtocolCombo.SelectedItem as ComboBoxItem;

                if (item != null)
                {
                    DescriptionBlock.Text = item.Tag.ToString();

                    if (ProtocolCombo.SelectedIndex != 0)
                    {
                        switch (item.Name)
                        {
                            case "Wendler531Item":
                                HyperInfoLink.IsEnabled = true;
                                HyperInfoLink.Tag = "http://www.elitefts.com/view/?sp=2976";
                                break;
                            case "Beginning5x5Item":
                                HyperInfoLink.IsEnabled = true;
                                HyperInfoLink.Tag = "http://stronglifts.com/stronglifts-5x5-beginner-strength-training-program/";
                                break;
                            case "BeginningOSSItem":
                                HyperInfoLink.IsEnabled = true;
                                HyperInfoLink.Tag = "http://startingstrength.wikia.com/wiki/The_Starting_Strength_Novice/Beginner_Programs";
                                break;
                            default:
                                HyperInfoLink.IsEnabled = false;
                                break;
                        }
                    }
                    else
                    {
                        HyperInfoLink.IsEnabled = false;
                    }
                }

            }
        }

        #endregion

        //private void NextButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if (SelectedDatePicker.Value.HasValue && ProtocolCombo.SelectedIndex > -1)
        //    {
        //        ComboBoxItem item = ProtocolCombo.SelectedItem as ComboBoxItem;

        //        string parms = string.Format("?selectedDate={0}&protocol={1}", SelectedDatePicker.Value.Value.ToShortDateString(),
        //            item.Name.Replace("Item",string.Empty));
        //        Uri next = new Uri("/AddExercises.xaml"+parms, UriKind.RelativeOrAbsolute);
        //        NavigationService.Navigate(next);
        //    }
        //}

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }

        private void SelectedDatePicker_ValueChanged(object sender, DateTimeValueChangedEventArgs e)
        {
            //DateTextBlock.Text = e.NewDateTime.Value.ToShortDateString();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            if (SelectedDatePicker.Value.HasValue && ProtocolCombo.SelectedIndex > -1)
            {
                ComboBoxItem item = ProtocolCombo.SelectedItem as ComboBoxItem;

                if (item.Name.Equals("Wendler531Item"))
                {
                    string parms = string.Format(CultureInfo.CurrentCulture, "?selectedDate={0}&protocol={1}", SelectedDatePicker.Value.Value.ToShortDateString(),
                        item.Name.Replace("Item", string.Empty));
                    Uri next = new Uri("/Add531Lifts.xaml" + parms, UriKind.RelativeOrAbsolute);
                    NavigationService.Navigate(next);
                }
                else if (item.Name.Equals("Beginning5x5Item"))
                {
                    string parms = string.Format(CultureInfo.CurrentCulture, "?selectedDate={0}&protocol={1}", SelectedDatePicker.Value.Value.ToShortDateString(),
                       item.Name.Replace("Item", string.Empty));
                    Uri next = new Uri("/AddFiveByFiveBeginner.xaml" + parms, UriKind.RelativeOrAbsolute);
                    NavigationService.Navigate(next);
                }
                else if (item.Name.Equals("BeginningOSSItem"))
                {
                    string parms = string.Format(CultureInfo.CurrentCulture, "?selectedDate={0}&protocol={1}", SelectedDatePicker.Value.Value.ToShortDateString(),
                      item.Name.Replace("Item", string.Empty));
                    Uri next = new Uri("/AddOriginalStartingStrength.xaml" + parms, UriKind.RelativeOrAbsolute);
                    NavigationService.Navigate(next);
                }
                else
                {

                    string parms = string.Format(CultureInfo.CurrentCulture, "?selectedDate={0}&protocol={1}", SelectedDatePicker.Value.Value.ToShortDateString(),
                        item.Name.Replace("Item", string.Empty));
                    Uri next = new Uri("/AddExercises.xaml" + parms, UriKind.RelativeOrAbsolute);
                    NavigationService.Navigate(next);

                }
            }
        }

        private void HyperInfoLink_Click(object sender, RoutedEventArgs e)
        {
            WebBrowserTask task = new WebBrowserTask();
            task.URL = HyperInfoLink.Tag.ToString();
            task.Show();
        }
    }
}