using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace WPLiftsLibrary
{
    public class BaseLift
    {
        #region Properties
        /// <summary>
        /// The number of reps that were performed for this lift.
        /// </summary>
        [XmlAttribute]
        public int Repetitions
        {
            get;
            set;
        }

        /// <summary>
        /// The weight in pounds that was used to perform the lift.
        /// </summary>
        [XmlAttribute]
        public double Weight
        {
            get;
            set;
        }

        /// <summary>
        /// The conversion to kilograms of the current weight being used to perform the lift. If you
        /// set this field the Weight will be set as well, so be carefull.
        /// </summary>
        [XmlAttribute]
        public double Kilograms
        {
            get { return GetKilos(this.Weight); }
            set { this.Weight = GetPounds(value); }
        }

        /// <summary>
        /// The method used to calculate the one rep max or 1RM of the current lift.
        /// </summary>
        [XmlAttribute]
        public OneRepMaxCalcMethod CalcMethod
        {
            get;
            set;
        }

        #endregion

        #region Calculations

        /// <summary>
        /// Calculates the tonnage of the current weight.
        /// </summary>
        /// <returns>A double of the actual weight lifted.</returns>
        public double GetTonnage()
        {
            return (double)this.Repetitions * this.Weight;
        }

        /// <summary>
        /// Converts pounds to kilograms.
        /// </summary>
        /// <param name="pounds">The pounds you wish to convert.</param>
        /// <returns>A double result of kilograms after conversion.</returns>
        public static double GetKilos(double pounds)
        {
            return pounds * 0.45359237;
        }

        /// <summary>
        /// Converts kilos back into pounds.
        /// </summary>
        /// <param name="kilos">The kilograms you wish to convert to pounds.</param>
        /// <returns>A double result of pounds after conversion.</returns>
        public static double GetPounds(double kilos)
        {
            return kilos / 0.45359237;
        }

        /// <summary>
        /// Calculates the One Rep Max or 1RM of the current weight and reps.
        /// </summary>
        /// <returns>The 1RM Value based on the CalcMethod of this class.</returns>
        public double CalculationOneRepMaximum()
        {
            double result = 0.0;

            switch (this.CalcMethod)
            {
                case OneRepMaxCalcMethod.Brzycki:
                    result = CalculateBrzyckiOneRepMax();
                    break;
                case OneRepMaxCalcMethod.Lander:
                    result = CalculateLanderOneRepMax();
                    break;
                default:
                    result = CalculateRoughOneRepMax();
                    break;
            }

            return result;
        }

        /// <summary>
        /// Calculates the 1RM using the Epley formula for 1RM.
        /// </summary>
        /// <returns>The 1RM for the current lift.</returns>
        private double CalculateRoughOneRepMax()
        {
            double result = 0.0;

            result = (this.Weight * (double)this.Repetitions) * 0.03333 + this.Weight;

            return result;
        }

        /// <summary>
        /// Calculates the 1RM using the Brzycki Formula for 1RM.
        /// </summary>
        /// <returns>The 1RM for the current lift.</returns>
        private double CalculateBrzyckiOneRepMax()
        {
            double result = 0.0;

            result = this.Weight * (36.0 / (37.0 - (double)this.Repetitions));

            return result;
        }

        /// <summary>
        /// Calculates the 1RM using the Lander formula for 1RM.
        /// </summary>
        /// <returns>The 1RM for the current lift.</returns>
        private double CalculateLanderOneRepMax()
        {
            double result = 0.0;

            result = ((double)(100.0 * this.Weight) / (101.3 - 2.67123 * (double)this.Repetitions));

            return result;
        }

        #endregion

        #region Constructors

        public BaseLift()
        {
        }

        public BaseLift(int reps, double weight)
        {
            this.Repetitions = reps;
            this.Weight = weight;
            this.CalcMethod = OneRepMaxCalcMethod.Epley;
        }

        public BaseLift(int reps, double weight, OneRepMaxCalcMethod method)
        {
            this.Repetitions = reps;
            this.Weight = weight;
            this.CalcMethod = method;
        }

        #endregion
    }
}
