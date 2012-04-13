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
using System.Windows.Media.Imaging;

namespace WPLifts.Controls
{
    public partial class ExerciseLiftControl : UserControl
    {
        public delegate void SaveNeededHandler(object sender, SaveNeededRoutedEventArgs e);
        public event SaveNeededHandler SaveNeeded;

        public delegate void ItemDeletedHandler(object sender, RoutedEventArgs e);
        public event ItemDeletedHandler ItemDeleted;

        public static readonly DependencyProperty LiftProperty = DependencyProperty.RegisterAttached(
            "LiftProperty", typeof(Lift), typeof(ExerciseLiftControl),
            new PropertyMetadata(null, new PropertyChangedCallback(LiftChanged)));

        private int EventFires
        {
            get;
            set;
        }

        public bool AfterFirstLoad
        {
            get { return EventFires > 1; }
        }

        public Lift Lift
        {
            get { return GetValue(LiftProperty) as Lift; }
            set { SetValue(LiftProperty, value); }
        }

        public ExerciseLiftControl()
        {
            InitializeComponent();
        }

        private static void LiftChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ExerciseLiftControl control = o as ExerciseLiftControl;

            if (control != null)
            {
                Lift result = e.NewValue as Lift;

                if (result != null)
                {
                    BindFields(control, result);
                }
            }
        }

        private static void BindFields(ExerciseLiftControl control, Lift result)
        {
            control.WeightTextBox.Text = result.Weight.ToString("###",CultureInfo.CurrentCulture);
            control.RepsTextBox.Text = result.Repetitions.ToString(CultureInfo.CurrentCulture);
            
            control.IsCompletedCheck.IsChecked = result.IsCompleted;
            
            if (!result.IsCompleted)
            {
                control.EventFires++;
            }
            //if (result.LiftName.ToUpper().Contains("WARMUP"))
            //{
            //    //control.GridImageBrush.ImageSource = new BitmapImage(new Uri("/Images/appbarwarmups.png", UriKind.Relative));
            //    //control.GridImageBrush.Opacity = 0.1;
            //    //control.GridImageBrush.Stretch = Stretch.UniformToFill;
            //}
        }

        private static void BindMaxData(ExerciseLiftControl control, Lift result)
        {
            control.OneRepTextBox.Text = result.CalculationOneRepMaximum().ToString("###", CultureInfo.CurrentCulture);
            control.DescriptionBlock.Text = result.ToShortString();
        }

        private void IsCompletedCheck_Checked(object sender, RoutedEventArgs e)
        {
            this.Lift.IsCompleted = IsCompletedCheck.IsChecked.Value;
            this.Lift.CompletedDate = DateTime.Now.ToString();

            

            //EventFires++;
          
            FireSave();
            
        }

        private void IsCompletedCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            this.Lift.IsCompleted = IsCompletedCheck.IsChecked.Value;
            this.Lift.CompletedDate = string.Empty;

            BindMaxData(this, this.Lift);

            FireSave();


        }

        private void FireSave()
        {
            if (AfterFirstLoad)
            {
                if (SaveNeeded != null)
                {
                    SaveNeeded(this, new SaveNeededRoutedEventArgs() { CauseNavigationReset = false });
                }
            }

            EventFires++;
        }

        private void RepsTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            Key[] allow = { Key.D0, Key.D1, Key.D2,Key.D3, Key.D4,Key.D5, Key.D6, Key.D7, Key.D8, Key.D9 };
            if (!allow.Contains(e.Key) || (e.PlatformKeyCode < 48 && e.PlatformKeyCode > 57))
            {
                e.Handled = true;
            }
        }

        private void RepsTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (!this.Lift.Repetitions.ToString(CultureInfo.CurrentCulture).Equals(RepsTextBox.Text))
            {
                try
                {
                    if (RepsTextBox != null)
                    {
                        if (!string.IsNullOrEmpty(RepsTextBox.Text))
                        {
                            this.Lift.Repetitions = int.Parse(RepsTextBox.Text, CultureInfo.CurrentCulture);
                        }

                        BindMaxData(this, this.Lift);

                        FireSave();
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show(UITextResources.NUMBER_ENTRY_MESSAGE);
                    UIHelper.WashTextField(RepsTextBox);
                }

                
            }
        }

        private void WeightTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                if (WeightTextBox != null)
                {
                   
                    if (!string.IsNullOrEmpty(WeightTextBox.Text))
                    {
                        this.Lift.Weight = double.Parse(WeightTextBox.Text, CultureInfo.CurrentCulture);
                    }

                    BindMaxData(this, this.Lift);

                    FireSave();
                }

                
            }
            catch (FormatException)
            {
                MessageBox.Show(UITextResources.NUMBER_ENTRY_MESSAGE);
                UIHelper.WashTextField(WeightTextBox);
            }
        }

       

        private void RepsTextBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RepsTextBox.SelectAll();
        }

        private void WeightTextBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            WeightTextBox.SelectAll();
        }

        private void OneRepTextBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OneRepTextBox.SelectAll();
        }

        private void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            WpLiftsDataFile file = IsolatedStorageHelper.LoadData();

            file.Lifts.DeleteItem(this.Lift.ID);

            IsolatedStorageHelper.SaveData(file);

            //this.Visibility = System.Windows.Visibility.Collapsed;

            if (ItemDeleted != null)
            {
                ItemDeleted(this, new RoutedEventArgs());
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        
    }

    public class SaveNeededRoutedEventArgs : RoutedEventArgs
    {
        public bool CauseNavigationReset
        {
            get;
            set;
        }
    }

    
}
