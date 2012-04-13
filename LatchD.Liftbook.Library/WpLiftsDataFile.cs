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
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;
using System.Xml;
using System.Linq;
using System.Globalization;

namespace WPLiftsLibrary
{
    public class WpLiftsDataFile
    {
        #region Fields

        private LiftCollection coll;
        private CardioRunCollection collr;
        //private ExerciseCollection colle;

        #endregion

        #region Properties

        public LiftCollection Lifts
        {
            get { return coll; }
        }

        public CardioRunCollection CardioRuns
        {
            get { return collr; }
        }

        //public ExerciseCollection Exercises
        //{
        //    get { return colle; }
        //}

        #endregion

        #region Constructor

        public WpLiftsDataFile()
        {
            coll = new LiftCollection();
            collr = new CardioRunCollection();
            //5 = new ExerciseCollection();
        }

        #endregion

        #region Static

        public static WpLiftsDataFile CreateTestFile()
        {
            return MakeTestingFile();
        }

        private static WpLiftsDataFile MakeTestingFile()
        {
            WpLiftsDataFile file = new WpLiftsDataFile();

            Lift lift = new Lift()
            {
                Date = DateTime.Now.AddDays(-3.0).Date,
                LiftName = "Bench Press",
                Weight = 160,
                Repetitions = 6,
                IsCompleted = true
            };

            file.Lifts.Add(lift);

            file.Lifts.Add531Lift(DateTime.Now.AddDays(-2.0), "Conventional Deadlift", 315.00, "Dumbell Row", 95.00, "Shrug", 125.00, 1);
            file.Lifts.Add531Lift(DateTime.Now.AddDays(-1.0), "Conventional Deadlift", 315.00, "Dumbell Row", 95.00, "Shrug", 125.00, 2);
            file.Lifts.Add531Lift(DateTime.Now, "Conventional Deadlift", 315.00, "Dumbell Row", 95.00, "Shrug", 125.00, 3);
            file.Lifts.Add531Lift(DateTime.Now.AddDays(1.0), "Conventional Deadlift", 315.00, "Dumbell Row", 95.00, "Shrug", 125.00, 4);
            //file.Lifts.AddPlaceholderLift(DateTime.Now.AddDays(-2.0).Date, "Military Press", LiftProtocol.Open, 135.00, true);
            //file.Lifts.AddPlaceholderLift(DateTime.Now.Date, "Deadlift", LiftProtocol.Open, 315.00, true);
            //file.Lifts.AddPlaceholderLift(DateTime.Now.AddDays(2.0).Date, "Back Squat", LiftProtocol.Open, 250.00, true);

            //file.PopulateExercises();

            return file;
        }

        public static IEnumerable<Exercise> ListExercises()
        {

            List<Exercise> exercises = new List<Exercise>();

            exercises.Add(new Exercise() { Name = "Back Squat", Protocol = LiftProtocol.Open });
            exercises.Add(new Exercise() { Name = "Bench Press", Protocol = LiftProtocol.Open });
            exercises.Add(new Exercise() { Name = "Bent Over Row", Protocol = LiftProtocol.Open });
            exercises.Add(new Exercise() { Name = "Clean", Protocol = LiftProtocol.Open });
            exercises.Add(new Exercise() { Name = "Clean And Jerk", Protocol = LiftProtocol.Open });
            exercises.Add(new Exercise() { Name = "Conventional Deadlift", Protocol = LiftProtocol.Open });
            exercises.Add(new Exercise() { Name = "Decline Press", Protocol = LiftProtocol.Open });
            exercises.Add(new Exercise() { Name = "Fly", Protocol = LiftProtocol.Open });
            exercises.Add(new Exercise() { Name = "Front Squat", Protocol = LiftProtocol.Open });
            exercises.Add(new Exercise() { Name = "Good Morning", Protocol = LiftProtocol.Open });
            exercises.Add(new Exercise() { Name = "Hang Clean", Protocol = LiftProtocol.Open });
            exercises.Add(new Exercise() { Name = "Incline Press", Protocol = LiftProtocol.Open });
            exercises.Add(new Exercise() { Name = "Military Press", Protocol = LiftProtocol.Open });
            exercises.Add(new Exercise() { Name = "Overhead Squat", Protocol = LiftProtocol.Open });
            exercises.Add(new Exercise() { Name = "Pendlay Row", Protocol = LiftProtocol.Open });
            exercises.Add(new Exercise() { Name = "Power Clean", Protocol = LiftProtocol.Open });
            exercises.Add(new Exercise() { Name = "Power Snatch", Protocol = LiftProtocol.Open });
            exercises.Add(new Exercise() { Name = "Push Press", Protocol = LiftProtocol.Open });
            exercises.Add(new Exercise() { Name = "Romanian Deadlift", Protocol = LiftProtocol.Open });
            exercises.Add(new Exercise() { Name = "Snatch", Protocol = LiftProtocol.Open });
            exercises.Add(new Exercise() { Name = "Stiff Leg Deadlift", Protocol = LiftProtocol.Open });
            exercises.Add(new Exercise() { Name = "Sumo Deadlift", Protocol = LiftProtocol.Open });
            exercises.Add(new Exercise() { Name = "Zercher Squat", Protocol = LiftProtocol.Open });

            exercises.Add(new Exercise() { Name = "Barbell Curl", Protocol = LiftProtocol.Open });
            exercises.Add(new Exercise() { Name = "Dumbell Concentration Curl", Protocol = LiftProtocol.Open });
            exercises.Add(new Exercise() { Name = "Dumbell Hammer Curl", Protocol = LiftProtocol.Open });

            exercises.Add(new Exercise() { Name = "Dumbell Tricep Kickbacks", Protocol = LiftProtocol.Open });
            exercises.Add(new Exercise() { Name = "Dumbell Tricep Extensions", Protocol = LiftProtocol.Open });
            exercises.Add(new Exercise() { Name = "Cable Tricep Pushdowns", Protocol = LiftProtocol.Open });

            exercises.Add(new Exercise() { Name = "Dips", Protocol = LiftProtocol.Open });
            exercises.Add(new Exercise() { Name = "Bench Dips", Protocol = LiftProtocol.Open });
            exercises.Add(new Exercise() { Name = "Chin Ups", Protocol = LiftProtocol.Open });
            exercises.Add(new Exercise() { Name = "Pull Ups", Protocol = LiftProtocol.Open });

            exercises.Add(new Exercise() { Name = "Leg Curl", Protocol = LiftProtocol.Open });
            exercises.Add(new Exercise() { Name = "Leg Extensions", Protocol = LiftProtocol.Open });
            exercises.Add(new Exercise() { Name = "Calf Raises", Protocol = LiftProtocol.Open });
            exercises.Add(new Exercise() { Name = "Barbell Shrugs", Protocol = LiftProtocol.Open });

            exercises.Add(new Exercise() { Name = "Cable Crossovers", Protocol = LiftProtocol.Open });
            exercises.Add(new Exercise() { Name = "Dumbell Bench Press", Protocol = LiftProtocol.Open });
            exercises.Add(new Exercise() { Name = "Dumbell Incline Press", Protocol = LiftProtocol.Open });
            exercises.Add(new Exercise() { Name = "Dumbell Decline Press", Protocol = LiftProtocol.Open });
            exercises.Add(new Exercise() { Name = "Dumbell Flys", Protocol = LiftProtocol.Open });


            return exercises.OrderBy(n => n.Name);
        }

        public string ToXml()
        {
            StringBuilder sb = new StringBuilder();

            using (XmlWriter writer = XmlWriter.Create(sb))
            {
                XmlSerializer s = new XmlSerializer(this.GetType());
                s.Serialize(writer, this);

            }

            return sb.ToString();
        }

        #endregion

        #region Queries

        public IEnumerable<string> GetScheduledDates(int range)
        {
            double half = range / 2;

            DateTime previousDate = DateTime.Now.AddDays((0 - half));
            DateTime futureDate = DateTime.Now.AddDays(half);

            List<DateTime> lifts = new List<DateTime>(this.Lifts.GetScheduledDates(previousDate, futureDate));
            List<DateTime> runs = new List<DateTime>(this.CardioRuns.GetScheduledDates(previousDate, futureDate));

            lifts.AddRange(runs);

            var results = from x in lifts
                          orderby x
                          select x.ToShortDateString();
           
            foreach (var result in results.Distinct())
            {
                yield return result;
            }

            
        }

        public void DeleteItem(string id)
        {
            this.Lifts.DeleteItem(id);
            this.CardioRuns.DeleteItem(id);
        }

        public string GetWorkoutTextSummary(DateTime date)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Date: ");
            sb.Append(date.ToShortDateString());
            sb.Append(System.Environment.NewLine);
            sb.Append(System.Environment.NewLine);

            sb.Append("Scheduled Lifts");
            sb.Append("------------------------------------------------------");
            sb.Append(System.Environment.NewLine);
            sb.Append(System.Environment.NewLine);

            var results = from x in this.Lifts
                          where x.Date.Date == date.Date
                          group x by x.LiftName into g
                          select new { Key = g.Key, Lifts = g };

            foreach (var result in results)
            {
                
                foreach (var lift in result.Lifts)
                {
                    sb.Append(lift.ToString());
                    sb.Append(System.Environment.NewLine);
                }

                sb.Append(System.Environment.NewLine);
            }

            sb.Append(System.Environment.NewLine);
            sb.Append(System.Environment.NewLine);


            sb.Append("Scheduled Cardio");
            sb.Append("------------------------------------------------------");
            sb.Append(System.Environment.NewLine);

            var cresults = from x in this.CardioRuns
                           where x.Date.Date == date.Date
                          group x by x.CardioType into g
                          select new { Key = g.Key, Runs = g };

            foreach (var cresult in cresults)
            {
               
                foreach (var run in cresult.Runs)
                {
                    sb.Append(run.ToString());
                    sb.Append(System.Environment.NewLine);
                }
            }

            return sb.ToString();
        }

        #endregion

    }
}
