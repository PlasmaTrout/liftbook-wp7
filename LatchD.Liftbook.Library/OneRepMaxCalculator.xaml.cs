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

namespace WPLiftsLibrary
{
    public partial class OneRepMaxCalculator : UserControl
    {
        Lift lift;

        public OneRepMaxCalculator()
        {
            InitializeComponent();
            lift = new Lift() { Repetitions = 5, Weight = 5.0, CalcMethod = OneRepMaxCalcMethod.Epley };
            
        }

        private void BindResult()
        {
            if (ResultTextBlock != null)
            {
                ResultTextBlock.Text = lift.CalculationOneRepMaximum().ToString("###.##", CultureInfo.CurrentCulture);
            }
        }
    }
}
