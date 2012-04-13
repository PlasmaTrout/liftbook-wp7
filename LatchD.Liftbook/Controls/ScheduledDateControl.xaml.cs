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
using System.Windows.Media.Imaging;
using System.Globalization;

namespace WPLifts.Controls
{
    public partial class ScheduledDateControl : UserControl
    {
        public static readonly DependencyProperty DateProperty = DependencyProperty.RegisterAttached(
            "DateProperty", typeof(string), typeof(ScheduledDateControl),
            new PropertyMetadata(DateTime.MinValue.ToShortDateString(), new PropertyChangedCallback(DateChanged)));

        public static readonly DependencyProperty CompletedProperty = DependencyProperty.RegisterAttached(
            "CompletedProperty", typeof(bool), typeof(ScheduledDateControl),
            new PropertyMetadata(false, new PropertyChangedCallback(CompletedChanged)));

        public string Date
        {
            get { return GetValue(DateProperty).ToString(); }
            set { SetValue(DateProperty, value); }
        }

        public bool IsCompleted
        {
            get { return (bool)GetValue(CompletedProperty); }
            set { SetValue(CompletedProperty, value); }
        }

        public ScheduledDateControl()
        {
            InitializeComponent();
        }

        private static void DateChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ScheduledDateControl control = o as ScheduledDateControl;

            if (control != null)
            {
                DateTime newTime = DateTime.Parse(e.NewValue.ToString(),CultureInfo.CurrentCulture);

                AppSettings settings = new AppSettings();

                control.DateTextBlock.Text = newTime.ToString(settings.ScheduleDateFormatString,CultureInfo.CurrentCulture);
            }
        }

        private static void CompletedChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ScheduledDateControl control = o as ScheduledDateControl;

            if (control != null)
            {
                bool result = bool.Parse(e.NewValue.ToString());
                control.CompletedCheck.IsChecked = result;
            }
        }
    }
}
