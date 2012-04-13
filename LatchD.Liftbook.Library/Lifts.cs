using System;
using System.Net;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace WPLiftsLibrary
{
   
    public class LiftCollection : Collection<Lift>
    {
       
        #region Adds

        public void AddLift(DateTime date, string exercise, LiftProtocol protocol, int reps ,double weight, int sets)
        {
            for (int i = 0; i < sets; i++)
            {
                this.Add(new Lift() { Date = date, LiftName = exercise, Repetitions = reps, Weight = weight, Protocol=protocol });
            }
        }

        public void Add531Lift(DateTime date, string mainLift, double weight, string assistance1, double assistanceWeight1,
            string assistance2, double assistanceWeight2, int week)
        {
            Add531Lifts(date, mainLift, weight, week);

            if (week != 4)
            {
                AddLift(date, assistance1+" Assistance", LiftProtocol.Wendler531, 10, assistanceWeight1, 5);
                AddLift(date, assistance2+" Assistance", LiftProtocol.Wendler531, 10, assistanceWeight2, 5);
            }
        }

        #endregion

        
        #region 5-3-1

        private void Add531Lifts(DateTime date,string exercise,double previousWeight,int week)
        {
            this.AddWarmupLifts(date, exercise, previousWeight, 40, 50, 60);

            if (week != 4)
            {
                
                double[] weekonepct = { 0.65, 0.75, 0.85 };
                double[] weektwopct = { 0.70, 0.80, 0.90 };
                double[] weekthrpct = { 0.75, 0.85, 0.95 };

                double[][] weeks = { weekonepct, weektwopct, weekthrpct };

                int[] reps = { 5, 5, 5 };

                switch (week)
                {
                    case 1:
                        reps = new int[] { 5, 5, 5 };
                        break;
                    case 2:
                        reps = new int[] { 3, 3, 3 };
                        break;
                    case 3:
                        reps = new int[] { 5, 3, 1 };
                        break;
                    default:
                        break;
                }


                this.Add(new Lift()
                {
                    Date = date,
                    LiftName = exercise,
                    Weight = (double)((int)Math.Round((previousWeight * weeks[week - 1][0]) / 5.0)) * 5,
                    Repetitions = reps[0]
                });

                this.Add(new Lift()
                {
                    Date = date,
                    LiftName = exercise,
                    Weight = (double)((int)Math.Round((previousWeight * weeks[week - 1][1]) / 5.0)) * 5,
                    Repetitions = reps[1]
                });

                this.Add(new Lift()
                {
                    Date = date,
                    LiftName = exercise,
                    Weight = (double)((int)Math.Round((previousWeight * weeks[week - 1][2]) / 5.0)) * 5,
                    Repetitions = reps[2]
                });
            }
        }

        #endregion

        #region Warmup Stuff

        public void AddWarmupLift(DateTime date, string exercise, LiftProtocol protocol, double totalWeight, int percentage, int reps)
        {
            double value = totalWeight * ((double)percentage / 100.00);
            double adjustedWeight = (double)((int)Math.Round(value / 5.0)) * 5;

            this.AddLift(date, exercise, protocol, reps, adjustedWeight, 1);
        }

        private void AddWarmupLifts(DateTime date, string exercise, double previousWeight, params int[] percentages)
        {
            int i = 0;

            foreach (var percentage in percentages)
            {
                i++;

                double value = previousWeight * ((double)percentage/100.00);

                double adjustedWeight = (double)((int)Math.Round(value / 5.0)) * 5;


                this.Add(new Lift()
                {
                    LiftName=exercise+" Warmup",
                    Date = date,
                    Weight = adjustedWeight,
                    Repetitions = i == 3 ? 3 : 5,
                    Protocol = LiftProtocol.Wendler531
                });

                
            }
        }

        #endregion

        #region Queries

        public IEnumerable<DateTime> GetScheduledDates(DateTime previousDate, DateTime futureDate)
        {
           
            var results = from x in this.Items
                          where x.Date.Date >= previousDate.Date && x.Date.Date <= futureDate.Date
                          orderby x.Date
                          select x.Date;
            foreach (var result in results.Distinct())
            {
                yield return result;
            }
        }

        public IEnumerable<string> GetWorkoutsForDate(DateTime date)
        {
            var results = from x in this.Items
                          where x.Date.Date == date.Date
                          select x.LiftName;

            foreach (var result in results.Distinct())
            {
                yield return result;
            }
        }

        public int GetLiftCountForDate(DateTime date)
        {
            var results = from x in this.Items
                          where x.Date.Date == date.Date
                          select x.LiftName;

            return results.Count();
        }

        public IEnumerable<Lift> GetScheduledLifts(DateTime date, string lift)
        {
            var results = from x in this.Items
                          where x.Date.Date == date.Date && x.LiftName.Equals(lift)
                          select x;

            foreach (var result in results)
            {
                yield return result;
            }
        }

        public IEnumerable<Lift> GetAllLifts(string lift)
        {
            var results = from x in this.Items
                          where x.LiftName.Equals(lift)
                          select x;

            foreach (var result in results)
            {
                yield return result;
            }
        }

        public bool CheckForCompletion(DateTime date)
        {
            bool result = true;

            var results = from x in this.Items
                          where x.Date.Date == date.Date
                          select x.IsCompleted;

            int count = results.Count();

            if (count > 0)
            {
                int switches = results.Distinct().Count();

                if (switches == 1)
                {
                    result = results.FirstOrDefault();
                }
                else
                {
                    result = false;
                }
            }

            return result;
        }

        public bool CheckForCompletion(DateTime date,string exerciseName)
        {
            bool result = true;

            var results = from x in this.Items
                          where x.Date.Date == date.Date && x.LiftName.Equals(exerciseName)
                          select x.IsCompleted;

            int count = results.Count();

            if (count > 0)
            {
                int switches = results.Distinct().Count();

                if (switches == 1)
                {
                    result = results.FirstOrDefault();
                }
                else
                {
                    result = false;
                }
            }

            return result;
        }

        public int GetTonnage(DateTime date, string lift)
        {
            int total = 0;

            total = (int) GetScheduledLifts(date, lift).Sum(n => n.GetTonnage());

            return total;
        }

        public int GetTonnage()
        {
            int total = 0;

            total = (int)this.Items.Sum(n => n.GetTonnage());

            return total;
        }

        public double GetBestOneRep(DateTime date, string lift)
        {
            double bestRep = 0;

            bestRep = GetScheduledLifts(date, lift).Max(n => n.CalculationOneRepMaximum());

            return bestRep;
        }

        public double GetBestOneRep()
        {
            double bestRep = 0;

            bestRep = this.Items.Max(n => n.CalculationOneRepMaximum());

            return bestRep;
        }

        public double GetBestOneRepEver(string lift)
        {
            double bestRep = 0;

            var results = from x in this.Items
                          where x.LiftName.Equals(lift)
                          select x.CalculationOneRepMaximum();

            bestRep = results.Max();

            return bestRep;
        }

        public bool IsPersonalRecord(Lift lift)
        {
            bool isPr = false;

            double result = GetAllLifts(lift.LiftName).Max(n => n.CalculationOneRepMaximum());
            if (lift.CalculationOneRepMaximum() == result)
            {
                isPr = true;
            }

            return isPr;
        }

        public string GetBestProtocolGuess(DateTime date)
        {
            var results = from x in this.Items
                          where x.Date.Date == date.Date
                          group x by x.Protocol into g
                          select new { Protocol = g.Key, Count = g.Count() };

            var bestGroup = results.OrderBy(n => n.Count);

            if (bestGroup.Count() > 0)
            {
                return bestGroup.Last().Protocol.ToString();
            }
            else
            {
                return "None";
            }
        }

        #endregion

        #region Updates

       

        #endregion

        #region Deletes

        public void DeleteItem(string id)
        {
            var results = from x in this.Items
                          where x.ID.Equals(id)
                          select this.Items.IndexOf(x);

            List<int> indexes = new List<int>(results);

            foreach (var result in indexes)
            {
                this.Items.RemoveAt(result);
            }
        }

        #endregion
    }
}
