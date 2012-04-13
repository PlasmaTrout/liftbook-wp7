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
using Microsoft.Phone.Tasks;
using WPLiftsLibrary;
using System.Globalization;
using System.Text;

namespace WPLifts
{
    public partial class ShareData : PhoneApplicationPage
    {
        public ShareData()
        {
            InitializeComponent();
        }

        private void EmailExportHyper_Click(object sender, RoutedEventArgs e)
        {
            EmailComposeTask task = new EmailComposeTask();
            WpLiftsDataFile file = IsolatedStorageHelper.LoadData();

            StringBuilder sb = new StringBuilder();

            sb.Append("Workout Export");
            sb.Append(System.Environment.NewLine);
            sb.Append("------------------------------------------------------------------------------------");

            var results = from x in file.Lifts
                          orderby x.Date ascending
                          select x;

            sb.Append("Date/tReps/t/Weight");
            sb.Append(System.Environment.NewLine);

            foreach (var result in results)
            {
                sb.Append(string.Format(CultureInfo.CurrentCulture,"{0}/t{1}/t{2}",
                    result.Date.ToShortDateString(),
                    result.Repetitions.ToString(CultureInfo.CurrentCulture),
                    result.Weight.ToString(CultureInfo.CurrentCulture)));
                sb.Append(System.Environment.NewLine);
            }

            sb.Append("-------------------------------------------------------------------------------------");

            task.Body = sb.ToString();

            task.Show();
        }
    }
}