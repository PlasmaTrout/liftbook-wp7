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
using System.Globalization;

namespace WPLiftsLibrary
{
    public class Lift : BaseLift
    {
        #region Properties

        [XmlAttribute]
        public string ID
        {
            get;
            set;
        }

        /// <summary>
        /// The date the lift is scheduled to occur. The completed flag controls
        /// whether or not it actually occured.
        /// </summary>
        [XmlAttribute]
        public DateTime Date
        {
            get;
            set;
        }

        /// <summary>
        /// The protocol the lift was scheduled for.
        /// </summary>
        [XmlAttribute]
        public LiftProtocol Protocol
        {
            get;
            set;
        }

        /// <summary>
        /// The name of the lift.
        /// </summary>
        [XmlAttribute]
        public string LiftName
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not the lift was completed.
        /// </summary>
        [XmlAttribute]
        public bool IsCompleted
        {
            get;
            set;
        }

        /// <summary>
        /// This flag should be set on the current PR for this exercise name.
        /// </summary>
        [XmlAttribute]
        public bool IsCurrentPR
        {
            get;
            set;
        }

        /// <summary>
        /// The geographical coordinates that were captured from the phone when the IsCompleted
        /// flag was set.
        /// </summary>
        [XmlAttribute]
        public string CompletedLocation
        {
            get;
            set;
        }

        /// <summary>
        /// The date the lift was completed
        /// </summary>
        public string CompletedDate
        {
            get;
            set;
        }

        //[XmlAttribute]
        //public string SetName
        //{
        //    get;
        //    set;
        //}

        #endregion

        #region Overrides

        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture,"{0}: {1} x {2}lbs ({3}kg) = 1RM of {4}lbs ({5}kg) using {6} Formula",
                this.LiftName,
                this.Repetitions.ToString(CultureInfo.CurrentCulture),
                this.Weight.ToString(CultureInfo.CurrentCulture), this.Kilograms.ToString("###.00", CultureInfo.CurrentCulture),
                this.CalculationOneRepMaximum().ToString("###.##",CultureInfo.CurrentCulture),
                GetKilos(this.CalculationOneRepMaximum()).ToString("###.##",CultureInfo.CurrentCulture),
                this.CalcMethod.ToString());
        }

        public string ToShortString()
        {
            return string.Format(CultureInfo.CurrentCulture, "1RM of {0}lbs ({1}kg) using {2} Formula",
                this.CalculationOneRepMaximum().ToString("###.##", CultureInfo.CurrentCulture),
                GetKilos(this.CalculationOneRepMaximum()).ToString("###.##", CultureInfo.CurrentCulture),
                this.CalcMethod.ToString());
        }

        #endregion

        #region Constructor
        
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Lift()
        {
            this.ID = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Creates a new lift with a name, number of repetitions and a weight.
        /// </summary>
        /// <param name="name">The name of the exercise being performed.</param>
        /// <param name="reps">The number of repetitions being performed.</param>
        /// <param name="weight">The total weight being lifted per rep.</param>
        public Lift(string name, int reps, double weight)
        {
            this.LiftName = name;
            this.Repetitions = reps;
            this.Weight = weight;
            this.ID = Guid.NewGuid().ToString();
        }

        #endregion
    }
}
