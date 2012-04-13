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
    public class CardioRun
    {
        #region Properties

        [XmlAttribute]
        public string ID
        {
            get;
            set;
        }

        /// <summary>
        /// The date the run was scheduled.
        /// </summary>
        [XmlAttribute]
        public DateTime Date
        {
            get;
            set;
        }

        /// <summary>
        /// The type of cardio being performed.
        /// </summary>
        [XmlAttribute]
        public string CardioType
        {
            get;
            set;
        }

        /// <summary>
        /// How long it took to go the distance specified.
        /// </summary>
        
        public TimeSpan Duration
        {
            get { return new TimeSpan(this.DurationHours,this.DurationMinutes,this.DurationSeconds); }
        }

        [XmlAttribute]
        public int DurationHours
        {
            get;
            set;
        }

        [XmlAttribute]
        public int DurationMinutes
        {
            get;
            set;
        }

        [XmlAttribute]
        public int DurationSeconds
        {
            get;
            set;
        }

        /// <summary>
        /// The distance in miles.
        /// </summary>
        [XmlAttribute]
        public double Distance
        {
            get;
            set;
        }

        /// <summary>
        /// This flag is set if this cardio bout is the current Personal Record.
        /// </summary>
        [XmlAttribute]
        public bool IsPR
        {
            get;
            set;
        }

        /// <summary>
        /// The resistance setting used.
        /// </summary>
        [XmlAttribute]
        public double Resistance
        {
            get;
            set;
        }

        /// <summary>
        /// The geo-location snapshot of where the user was when the cardio was completed.
        /// </summary>
        [XmlAttribute]
        public string CompletedLocation
        {
            get;
            set;
        }

        /// <summary>
        /// Returns the minute mile pace of the current activity based on the entire run.
        /// </summary>
        public double MinuteMilePace
        {
            get { return CalculateMinuteMile(this.Distance, this.Duration.TotalMinutes); }
        }

        /// <summary>
        /// The miles per hour that was travelled on average for the entire run.
        /// </summary>
        public double MilesPerHour
        {
            get { return CalculateMilesPerHour(this.Distance, this.Duration.TotalHours); }
        }

        [XmlAttribute]
        public bool IsCompleted
        {
            get;
            set;
        }

        #endregion

        #region Calculations

        /// <summary>
        /// Calculates the minute mile pace of a specific run.
        /// </summary>
        /// <param name="miles">The number of miles ran.</param>
        /// <param name="minutes">The number of minutes it took to complete the activity.</param>
        /// <returns>The minute mile pace of the run as a double.</returns>
        public static double CalculateMinuteMile(double miles, double minutes)
        {
            double result = 0.0;

            if (miles > 0.0 && minutes > 0.0)
            {
                result = minutes / miles;
            }

            return result;
        }

        /// <summary>
        /// Calculates the miles per hour of a specific run.
        /// </summary>
        /// <param name="miles">The number of miles ran.</param>
        /// <param name="hours">How long the run took in hours.</param>
        /// <returns>The miles per hour that was ran to complete the run in the timeframe.</returns>
        public static double CalculateMilesPerHour(double miles, double hours)
        {
            double result = 0.0;

            if (miles > 0.0 && hours > 0.0)
            {
                result =  miles / hours;
            }

            return result; 
        }

        #endregion

        #region Override

        public override string ToString()
        {
            string result = string.Format(CultureInfo.CurrentCulture,"{0}: {1} mile/miles at {2}:{3}:{4} is a pace of {5} minute miles ( {6} mph )",
                this.CardioType.ToString(),
                this.Distance.ToString("###.##", CultureInfo.CurrentCulture),
                this.Duration.Hours.ToString(CultureInfo.CurrentCulture),
                this.Duration.Minutes.ToString(CultureInfo.CurrentCulture),
                this.Duration.Seconds.ToString(CultureInfo.CurrentCulture),
                this.MinuteMilePace.ToString("##.##", CultureInfo.CurrentCulture),
                this.MilesPerHour.ToString("##.##", CultureInfo.CurrentCulture));
            return result;
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public CardioRun()
        {
            this.ID = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Initializes a new run with a distance and a time to complete.
        /// </summary>
        /// <param name="distance">The distance in miles for this run.</param>
        /// <param name="duration">The time it took to complete it.</param>
        public CardioRun(double distance, TimeSpan duration)
        {
            this.Distance = distance;
            this.DurationHours = duration.Hours;
            this.DurationMinutes = duration.Minutes;
            this.DurationSeconds = duration.Seconds;
            this.ID = Guid.NewGuid().ToString();
        }

        #endregion
    }
}
