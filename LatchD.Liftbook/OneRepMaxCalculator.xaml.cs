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
    public partial class OneRepMaxCalculator : PhoneApplicationPage
    {
        BaseLift lift;

        public OneRepMaxCalculator()
        {
            InitializeComponent();
            lift = new BaseLift(5, 5, OneRepMaxCalcMethod.Epley);
            BindResult();
        }

        private void BindResult()
        {
            if (ResultTextBlock != null)
            {
                double result = lift.CalculationOneRepMaximum();
                ResultTextBlock.Text = result.ToString("###.##", CultureInfo.CurrentCulture) + " lbs";

                int weightResult = (int)(Math.Round(result));
                weightResult = weightResult / 5 * 5;
                Barweight.Text = "Closest Bar Weight is " + weightResult.ToString(CultureInfo.CurrentCulture) + " lbs";
            }
        }

        private void RepsBox_OnValueChanged(object sender, RoutedEventArgs e)
        {
            if (RepsBox != null)
            {
                this.lift.Repetitions = (int)RepsBox.Value;
                BindResult();
            }
        }

        private void WeightBox_OnValueChanged(object sender, RoutedEventArgs e)
        {
            if (WeightBox != null)
            {
                this.lift.Weight = WeightBox.Value;
                BindResult();
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (this.lift != null)
            {
                RadioButton button = sender as RadioButton;

                if (button != null)
                {
                    switch (button.Content.ToString())
                    {
                        case "Brzycki":
                            this.lift.CalcMethod = OneRepMaxCalcMethod.Brzycki;
                            break;
                        case "Lander":
                            this.lift.CalcMethod = OneRepMaxCalcMethod.Lander;
                            break;
                        default:
                            this.lift.CalcMethod = OneRepMaxCalcMethod.Epley;
                            break;
                    }
                }

                BindResult();
            }
        }
    }
}