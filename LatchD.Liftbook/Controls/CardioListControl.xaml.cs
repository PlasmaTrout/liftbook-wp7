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

namespace WPLifts.Controls
{
    public partial class CardioListControl : UserControl
    {
        public delegate void ItemDeletedHandler(object sender, RoutedEventArgs e);
        public event ItemDeletedHandler ItemDeleted;

        private CardioRunCollection coll;

        public CardioRunCollection Lifts
        {
            get { return coll; }
        }

        public CardioListControl()
        {
            InitializeComponent();
            coll = new CardioRunCollection();
        }

        public CardioListControl(IEnumerable<CardioRun> runs)
        {
            InitializeComponent();
            coll = new CardioRunCollection();

            foreach (var run in runs)
            {
                coll.Add(run);
            }
        }

       
        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            WpLiftsDataFile file = IsolatedStorageHelper.LoadData();

            foreach (var run in coll)
            {
                file.DeleteItem(run.ID);
            }

            IsolatedStorageHelper.SaveData(file);

            if (ItemDeleted != null)
            {
                ItemDeleted(this, new RoutedEventArgs());
            }
        }
    }
}
