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
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace WPLiftsLibrary
{
    public class CardioRunCollection : Collection<CardioRun>
    {
        #region Queries

        public IEnumerable<string> GetWorkOutsForDate(DateTime date)
        {
            var results = from x in this.Items
                          where x.Date.Date == date.Date
                          select x.CardioType;

            foreach (var result in results.Distinct())
            {
                yield return result;
            }
        }

        public int GetCardioCountForDate(DateTime date)
        {
            var results = from x in this.Items
                          where x.Date.Date == date.Date
                          select x;

            return results.Count();
        }

        public IEnumerable<CardioRun> GetScheduledCardio(DateTime date)
        {
            var results = from x in this.Items
                         where x.Date.Date == date.Date
                         select x;

            foreach (var result in results)
            {
                yield return result;
            }
        }

        public IEnumerable<CardioRun> GetScheduledCardio(DateTime date,string cardioType)
        {
            var results = from x in this.Items
                          where x.Date.Date == date.Date && x.CardioType.Equals(cardioType)
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

        public bool CheckForCompletion(DateTime date, string cardioType)
        {
            bool result = true;

            var results = from x in this.Items
                          where x.Date.Date == date.Date && x.CardioType.Equals(cardioType)
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

        public IEnumerable<CardioRun> GetAllLifts(string cardioType)
        {
            var results = from x in this.Items
                          where x.CardioType.Equals(cardioType)
                          select x;

            foreach (var result in results)
            {
                yield return result;
            }
        }

        public bool IsPersonalRecord(CardioRun run)
        {
            bool isPr = false;

            double result = GetAllLifts(run.CardioType).Max(n => n.MinuteMilePace);
            if (run.MinuteMilePace == result)
            {
                isPr = true;
            }

            return isPr;
        }

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

        public double GetMaxDistance(DateTime date, string run)
        {
            double result = 0.0;

            var results = from x in this.Items
                          where x.Date.Date == date.Date && x.CardioType.Equals(run)
                          select x;

            if (results.Count() > 0)
            {
                result = results.Max(n => n.Distance);
            }

            return result;
        }

        public double GetMaxDistance()
        {
            double result = 0.0;

            var results = from x in this.Items
                          select x;

            if (results.Count() > 0)
            {
                result = results.Max(n => n.Distance);
            }

            return result;
        }

        public double GetMaxPace(DateTime date, string run)
        {
            double result = 0.0;

            var results = from x in this.Items
                          where x.Date.Date == date.Date && x.CardioType.Equals(run)
                          select x;
            if (results.Count() > 0)
            {
                result = results.Max(n => n.MinuteMilePace);
            }

            return result;
        }

        public double GetMaxPace()
        {
            double result = 0.0;

            var results = from x in this.Items
                          select x;

            if (results.Count() > 0)
            {
                result = results.Max(n => n.MinuteMilePace);
            }

            return result;
        }

        public double GetMaxSpeed(DateTime date, string run)
        {
            double result = 0.0;

            var results = from x in this.Items
                          where x.Date.Date == date.Date && x.CardioType.Equals(run)
                          select x;
            if (results.Count() > 0)
            {
                result = results.Max(n => n.MilesPerHour);
            }

            return result;
        }

        public double GetMaxSpeed()
        {
            double result = 0.0;

            var results = from x in this.Items
                          select x;

            if (results.Count() > 0)
            {
                result = results.Max(n => n.MilesPerHour);
            }

            return result;
        }

        #endregion
    }
}
