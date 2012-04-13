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
using WPLiftsLibrary;
using System.Globalization;

namespace WPLifts.Controls
{
    public partial class ExerciseCardioControl : UserControl
    {

        public delegate void SaveNeededHandler(object sender, SaveNeededRoutedEventArgs e);
        public event SaveNeededHandler SaveNeeded;

        public static readonly DependencyProperty CardioRunProperty = DependencyProperty.RegisterAttached(
            "CardioRunProperty", typeof(CardioRun), typeof(ExerciseCardioControl),
            new PropertyMetadata(null,CardioRunChanged));

        public bool AfterFirstLoad
        {
            get;
            set;
        }

        public CardioRun CardioRun
        {
            get { return GetValue(CardioRunProperty) as CardioRun; }
            set { SetValue(CardioRunProperty, value); }
        }

        public ExerciseCardioControl()
        {
            InitializeComponent();
        }

        private static void CardioRunChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ExerciseCardioControl control = o as ExerciseCardioControl;

            if (control != null)
            {
                CardioRun run = e.NewValue as CardioRun;

                if (run != null)
                {
                    control.MilesTextBox.Text = run.Distance.ToString(CultureInfo.CurrentCulture);
                    control.HoursTextBox.Text = run.DurationHours.ToString(CultureInfo.CurrentCulture);
                    control.MinutesTextBox.Text = run.DurationMinutes.ToString(CultureInfo.CurrentCulture);
                    control.SecondsTextBox.Text = run.DurationSeconds.ToString(CultureInfo.CurrentCulture);
                    control.IsCompletedCheckBox.IsChecked = run.IsCompleted;
                    control.BindFields();
                }

            }
        }

        public void BindFields()
        {
            PaceRun.Text = " " + this.CardioRun.MinuteMilePace.ToString("###.##", CultureInfo.CurrentCulture) + " ";
            MPHRun.Text = " " + this.CardioRun.MilesPerHour.ToString("###.##", CultureInfo.CurrentCulture) + " ";
            AfterFirstLoad = true;
        }

        private void IsCompletedCheck_Checked(object sender, RoutedEventArgs e)
        {
           
                this.CardioRun.IsCompleted = IsCompletedCheckBox.IsChecked.Value;
                FireSaveNeeded(false);
            
        }

        private void FireSaveNeeded(bool reset)
        {
            if (AfterFirstLoad)
            {
                if (SaveNeeded != null)
                {
                    SaveNeeded(this, new SaveNeededRoutedEventArgs() { CauseNavigationReset = reset });
                }
            }
        }

        private void IsCompletedCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            this.CardioRun.IsCompleted = IsCompletedCheckBox.IsChecked.Value;
            FireSaveNeeded(false);
        }

        private void HoursTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (!this.CardioRun.DurationHours.ToString(CultureInfo.CurrentCulture).Equals(HoursTextBox.Text))
            {
                try
                {
                    if (!string.IsNullOrEmpty(HoursTextBox.Text))
                    {
                        this.CardioRun.DurationHours = int.Parse(HoursTextBox.Text,CultureInfo.CurrentCulture);
                    }
                    else
                    {
                        HoursTextBox.Text = "0";
                    }

                    BindFields();
                }
                catch (FormatException)
                {
                    MessageBox.Show(UITextResources.NUMBER_ENTRY_MESSAGE);
                    UIHelper.WashTextField(HoursTextBox);
                }

                FireSaveNeeded(false);
            }
        }

        private void MinutesTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (!this.CardioRun.DurationMinutes.ToString(CultureInfo.CurrentCulture).Equals(MinutesTextBox.Text))
            {
                try
                {
                    if (!string.IsNullOrEmpty(MinutesTextBox.Text))
                    {
                        this.CardioRun.DurationMinutes = int.Parse(MinutesTextBox.Text,CultureInfo.CurrentCulture);
                    }
                    else
                    {
                        MinutesTextBox.Text = "0";
                    }
                    BindFields();
                }
                catch (FormatException)
                {
                    MessageBox.Show(UITextResources.NUMBER_ENTRY_MESSAGE);
                    UIHelper.WashTextField(MinutesTextBox);
                }

                FireSaveNeeded(false);
            }
        }

        private void SecondsTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (!this.CardioRun.DurationSeconds.ToString(CultureInfo.CurrentCulture).Equals(SecondsTextBox.Text))
            {
                try
                {
                    if (!string.IsNullOrEmpty(SecondsTextBox.Text))
                    {
                        this.CardioRun.DurationSeconds = int.Parse(SecondsTextBox.Text,CultureInfo.CurrentCulture);
                    }
                    else
                    {
                        SecondsTextBox.Text = "0";
                    }
                    BindFields();
                }
                catch (FormatException)
                {
                    MessageBox.Show(UITextResources.NUMBER_ENTRY_MESSAGE);
                    UIHelper.WashTextField(SecondsTextBox);
                }

                FireSaveNeeded(false);
            }
        }

        private void MilesTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (!this.CardioRun.Distance.ToString(CultureInfo.CurrentCulture).Equals(MilesTextBox.Text))
            {
                try
                {
                    if (!string.IsNullOrEmpty(MilesTextBox.Text))
                    {
                        this.CardioRun.Distance = double.Parse(MilesTextBox.Text,CultureInfo.CurrentCulture);
                    }
                    else
                    {
                        MilesTextBox.Text = "0";
                    }
                    BindFields();
                }
                catch (FormatException)
                {
                    MessageBox.Show(UITextResources.NUMBER_ENTRY_MESSAGE);
                    UIHelper.WashTextField(MilesTextBox);
                }

                FireSaveNeeded(false);
            }
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

        private void MilesTextBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MilesTextBox.SelectAll();
        }
    }
}
