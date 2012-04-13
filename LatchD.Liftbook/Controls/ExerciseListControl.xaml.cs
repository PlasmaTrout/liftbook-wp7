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
using System.Globalization;
using System.Windows.Media.Imaging;

namespace WPLifts.Controls
{
    public partial class ExerciseListControl : UserControl
    {
        public delegate void ItemDeletedHandler(object sender, RoutedEventArgs e);
        public event ItemDeletedHandler ItemDeleted;

        private LiftCollection coll;
        private CardioRunCollection runs;

        public LiftCollection Lifts
        {
            get { return coll; }
        }

        public string Key
        {
            get;
            set;
        }

        public CardioRunCollection CardioRuns
        {
            get { return runs; }
        }

        public ExerciseListControl()
        {
            InitializeComponent();
            coll = new LiftCollection();
            runs = new CardioRunCollection();
        }

        public ExerciseListControl(IEnumerable<Lift> lifts,double allTimePR)
        {
            InitializeComponent();
            coll = new LiftCollection();
            runs = new CardioRunCollection();
            foreach (var lift in lifts)
            {
                coll.Add(lift);
            }

            RecordOneRepLabel.Text = "BEST "+allTimePR.ToString("###", CultureInfo.CurrentCulture) + " lbs";

            BindLiftDetails();
        }

        public ExerciseListControl(IEnumerable<CardioRun> newRuns)
        {
            InitializeComponent();
            coll = new LiftCollection();
            runs = new CardioRunCollection();
            foreach (var item in newRuns)
            {
                this.runs.Add(item);
            }

            LogoImageBrush.ImageSource = new BitmapImage(new Uri("/Images/appbarjog2.png", UriKind.RelativeOrAbsolute));
            
            BindRunDetails();
        }

        public void BindRunDetails()
        {

            TonnageLabel.Text = this.CardioRuns.GetMaxSpeed().ToString("###.##", CultureInfo.CurrentCulture) + " mph"; 
            OneRepMaxLabel.Text = this.CardioRuns.GetMaxPace().ToString("###.##",CultureInfo.CurrentCulture)+" pace";
            RecordOneRepLabel.Text = this.CardioRuns.GetMaxDistance().ToString("###.##", CultureInfo.CurrentCulture) + " miles";
        }

        public void BindLiftDetails()
        {
            TonnageLabel.Text = string.Format(CultureInfo.CurrentCulture, "{0} {1} {2}", UITextResources.TONNAGE_LBL,
                    this.Lifts.GetTonnage().ToString(CultureInfo.CurrentCulture),
                    UITextResources.POUNDS_ABBR);
            OneRepMaxLabel.Text = string.Format(CultureInfo.CurrentCulture, "{0} {1} {2}",
                UITextResources.BEST_1RM,
                this.Lifts.GetBestOneRep().ToString("###", CultureInfo.CurrentCulture),
                UITextResources.POUNDS_ABBR);

                       
        }

        private void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            WpLiftsDataFile file = IsolatedStorageHelper.LoadData();
            
            foreach (var lift in coll)
            {
                file.DeleteItem(lift.ID);
            }

            foreach (var item in this.CardioRuns)
            {
                file.DeleteItem(item.ID);
            }

            IsolatedStorageHelper.SaveData(file);

            if (ItemDeleted != null)
            {
                ItemDeleted(this, new RoutedEventArgs());
            }
        }
    }
}
