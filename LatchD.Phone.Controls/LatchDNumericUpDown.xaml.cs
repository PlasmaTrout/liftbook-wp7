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
using System.Globalization;
using System.Windows.Media.Imaging;

namespace LatchD.Phone.Controls
{
    public partial class LatchDNumericUpDown : UserControl
    {
        public delegate void ValueChangedEventHandler(object sender, RoutedEventArgs e);
        public event ValueChangedEventHandler OnValueChanged;

        public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached(
            "ValueProperty", typeof(double), typeof(LatchDNumericUpDown),
            new PropertyMetadata(0.0, new PropertyChangedCallback(ValueChanged)));

        private string formatString = "##0";
        private double increment = 1.0;
        private double maximum = double.MaxValue;
                
        
        public string FormatString
        {
            get { return formatString; }
            set { formatString = value; }
        }

        public double Increment
        {
            get { return increment; }
            set { increment = value; }
        }

        public bool AllowNegativeValues
        {
            get;
            set;
        }

        public double Minimum
        {
            get;
            set;
        }

        public double Maximum
        {
            get { return maximum; }
            set { maximum = value; }
        }

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public LatchDNumericUpDown()
        {
            InitializeComponent();
            ImageBrush image = new ImageBrush();
            image.ImageSource = new BitmapImage(new Uri("/Images/addbar.minus.rest.png", UriKind.RelativeOrAbsolute));

        }

        private static void ValueChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            LatchDNumericUpDown control = o as LatchDNumericUpDown;

            if (control != null)
            {
                double value = 0.0;
                double.TryParse(e.NewValue.ToString(), out value);
                control.ValueTextBox.Text = value.ToString(control.FormatString, CultureInfo.CurrentCulture);

                if (control.OnValueChanged != null)
                {
                    control.OnValueChanged(control, new RoutedEventArgs());
                }
            }
        }

        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Value > this.Minimum)
            {
                this.Value = this.Value - this.Increment;
            }
            else
            {
                if (this.AllowNegativeValues)
                {
                    this.Value = this.Value - this.Increment;
                }
            }
        }

        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Value < this.Maximum)
            {
                this.Value = this.Value + this.Increment;
            }
        }

        private void ValueTextBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ValueTextBox.SelectAll();
        }

        private void ValueTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ValueTextBox != null && !string.IsNullOrEmpty(ValueTextBox.Text))
            {
                try
                {
                    double test = double.Parse(ValueTextBox.Text);
                    this.Value = test;
                }
                catch (FormatException)
                {
                    MessageBox.Show("Only numbers may be entered in this box.");
                    UIHelper.WashTextField(ValueTextBox);
                }
            }
        }

        private void ValueTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            Key[] allow = { Key.D0, Key.D1, Key.D2, Key.D3, Key.D4, Key.D5, Key.D6, Key.D7, Key.D8, Key.D9 };
            if (!allow.Contains(e.Key) || (e.PlatformKeyCode < 48 && e.PlatformKeyCode > 57))
            {
                e.Handled = true;
            }
        }
    }
}
