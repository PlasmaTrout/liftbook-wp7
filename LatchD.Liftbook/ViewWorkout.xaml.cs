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
using WPLifts.Controls;
using System.Globalization;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Tasks;
using Microsoft.Phone.Shell;
using System.Threading;

namespace WPLifts
{
    public partial class ViewWorkout : PhoneApplicationPage
    {
        #region Fields

        WpLiftsDataFile file;
        DateTime selectedDate;
        //int mouseDownTick;

        #endregion

        #region Constructor

        public ViewWorkout()
        {
            InitializeComponent();
            file = IsolatedStorageHelper.LoadData();
            
        }

        #endregion

        #region Binders

        private void BindWorkouts(DateTime date)
        {
            //ProgressBar.IsIndeterminate = true;
            //ProgressBar.Visibility = System.Windows.Visibility.Visible;
            //ExerciseList.Visibility = Visibility.Collapsed;

            IEnumerable<string> workouts = file.Lifts.GetWorkoutsForDate(date);
            IEnumerable<string> runs = file.CardioRuns.GetWorkOutsForDate(date);

            ExerciseList.Items.Clear();

            int finalCount = MasterPivot.Items.Count()-1;

            for (int i = finalCount; i > 0; --i)
            {
                MasterPivot.Items.RemoveAt(i);
            }

            foreach (var group in workouts)
            {

                ExerciseListControl control = new ExerciseListControl(file.Lifts.GetScheduledLifts(date, group),file.Lifts.GetBestOneRepEver(group));

                control.WorkoutTextBlock.Text = group + " X " + control.Lifts.Count().ToString(CultureInfo.CurrentCulture);
                control.Key = group;
                control.Width = 440.00;
                control.Height = 200.00;
                control.Margin = new Thickness(5.0, 5.0, 5.0, 5.0);

                control.CompletedCheckBox.IsChecked = file.Lifts.CheckForCompletion(date, group);
                control.ItemDeleted += new ExerciseListControl.ItemDeletedHandler(control_ItemDeleted);
                //control.MouseLeftButtonUp += new MouseButtonEventHandler(ListControl_MouseLeftButtonUp);
                //control.MouseLeftButtonDown += new MouseButtonEventHandler(ListControl_MouseLeftButtonDown);
                ExerciseList.Items.Add(control);
                
                PivotItem item = SetupPivotItem(group, date);

                MasterPivot.Items.Add(item);
                control.Tag = MasterPivot.Items.IndexOf(item);

                if (control.WorkoutTextBlock.Text.ToUpper().Contains("WARMUP"))
                {
                    //control.LogoImageBrush.ImageSource = new BitmapImage(new Uri("/Images/appbarwarmups.png", UriKind.Relative));
                    //control.MainBorder.Background = new SolidColorBrush(Colors.Gray);
                }

            }

            foreach (var rungroup in runs)
            {
                ExerciseListControl control = new ExerciseListControl(file.CardioRuns.GetScheduledCardio(date, rungroup));
                control.Key = rungroup;
                control.WorkoutTextBlock.Text = rungroup;
                control.Width = 440.00;
                control.Height = 200.00;
                control.Margin = new Thickness(5.0, 5.0, 5.0, 5.0);
                control.CompletedCheckBox.IsChecked = file.CardioRuns.CheckForCompletion(date, rungroup);
                control.ItemDeleted += new ExerciseListControl.ItemDeletedHandler(control_ItemDeleted);

                ExerciseList.Items.Add(control);

                PivotItem item = SetupRunPivotItem(rungroup, date);

                MasterPivot.Items.Add(item);
                control.Tag = MasterPivot.Items.IndexOf(item);
            }

        }

        #region Lifts

        private PivotItem SetupPivotItem(string exerciseName, DateTime date)
        {
            PivotItem item = new PivotItem();
            item.Header = exerciseName;
            item.Name = exerciseName;

            ScrollViewer viewer = new ScrollViewer();
            StackPanel panel = new StackPanel();
            viewer.Content = panel;
            //panel.Width = 470.00;
            //panel.Height = 1024.00;

            //ListBox box = new ListBox();
            //item.Content = box;

            ProcessLiftData(exerciseName, date, panel);
            item.Content = viewer;
            return item;
        }

        private void ProcessLiftData(string exerciseName, DateTime date, StackPanel box)
        {
            var results = file.Lifts.GetScheduledLifts(date, exerciseName);
            int i = 1;

            foreach (var result in results)
            {
                ExerciseLiftControl control = new ExerciseLiftControl();
                //control.Name = result.LiftName;
                control.Lift = result;
                control.Width = 440.00;
                control.Height = 200.00;
                control.Margin = new Thickness(5.0, 5.0, 5.0, 5.0);
                control.SaveNeeded += new ExerciseLiftControl.SaveNeededHandler(Control_SaveNeeded);
                control.ItemDeleted +=new ExerciseLiftControl.ItemDeletedHandler(Control_LiftItemDeleted);
                control.SetLabel.Text = "Set " + i.ToString(CultureInfo.CurrentCulture);
                i++;
                //control.IsCompletedCheck.IsChecked = result.IsCompleted;

                box.Children.Add(control);

                if (file.Lifts.IsPersonalRecord(result))
                {
                    control.PRFlagLabel.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }

        private ExerciseListControl FindLift(string group)
        {
            PivotItem mainPivotItem = MasterPivot.Items[0] as PivotItem;

            if (mainPivotItem != null)
            {
                    ListBox box = mainPivotItem.Content as ListBox;
              
                    //StackPanel box = view.Content as StackPanel;

                    if (box != null)
                    {
                        foreach (var control in box.Items)
                        {
                            ExerciseListControl c = control as ExerciseListControl;

                            if (c != null)
                            {
                                if (c.Key.Equals(group))
                                {
                                    return c;
                                }
                            }
                        }


                    }
            }

            return null;
        }

        private bool CheckAllLiftsForChecked(string liftName)
        {
            bool allChecked = true;

            PivotItem item = MasterPivot.FindName(liftName) as PivotItem;

            if (item != null)
            {

                ScrollViewer view = item.Content as ScrollViewer;

                if (view != null)
                {

                    StackPanel box = view.Content as StackPanel;

                    if (box != null)
                    {
                        foreach (var boxItem in box.Children)
                        {
                            ExerciseLiftControl boxc = boxItem as ExerciseLiftControl;

                            if (boxc != null)
                            {
                                if (!boxc.IsCompletedCheck.IsChecked.Value)
                                {
                                    allChecked = false;
                                }
                            }

                        }
                    }
                }

            }
            return allChecked;
        }

        private void ResetAllLiftsPR(string liftName)
        {
            PivotItem item = MasterPivot.FindName(liftName) as PivotItem;

            if (item != null)
            {
                //ListBox box = item.Content as ListBox;
                ScrollViewer viewer = item.Content as ScrollViewer;

                if (viewer != null)
                {
                    StackPanel box = viewer.Content as StackPanel;

                    if (box != null)
                    {
                        foreach (var boxItem in box.Children)
                        {
                            ExerciseLiftControl boxc = boxItem as ExerciseLiftControl;

                            if (boxc != null)
                            {
                                boxc.PRFlagLabel.Visibility = System.Windows.Visibility.Collapsed;
                            }


                        }
                    }
                }

            }
        }

        #endregion

        #region Runs

        private PivotItem SetupRunPivotItem(string cardioType, DateTime date)
        {
            PivotItem item = new PivotItem();
            item.Header = cardioType;
            item.Name = cardioType;

            ScrollViewer viewer = new ScrollViewer();
            StackPanel panel = new StackPanel();
            viewer.Content = panel;

            ProcessCardioData(cardioType, date, panel);
            item.Content = viewer;

            return item;
        }

        private void ProcessCardioData(string exerciseName, DateTime date, StackPanel box)
        {
            var results = file.CardioRuns.GetScheduledCardio(date, exerciseName);

            foreach (var result in results)
            {
                ExerciseCardioControl control = new ExerciseCardioControl();
                control.Width = 440.00;
                control.Height = 200.00;
                control.Margin = new Thickness(5.0, 5.0, 5.0, 5.0);
                //control.IsCompletedCheckBox.IsChecked = result.IsCompleted;
                
                control.CardioRun = result;

                control.SaveNeeded += new ExerciseCardioControl.SaveNeededHandler(Control2_SaveNeeded);
                box.Children.Add(control);

            }
        }

        private bool CheckAllCardioRunsForChecked(string liftName)
        {
            bool allChecked = true;

            PivotItem item = MasterPivot.FindName(liftName) as PivotItem;

            if (item != null)
            {

                ScrollViewer view = item.Content as ScrollViewer;

                if (view != null)
                {
                    StackPanel box = view.Content as StackPanel;

                    if (box != null)
                    {
                        foreach (var boxItem in box.Children)
                        {
                            ExerciseCardioControl boxc = boxItem as ExerciseCardioControl;

                            if (boxc != null)
                            {
                                if (!boxc.IsCompletedCheckBox.IsChecked.Value)
                                {
                                    allChecked = false;
                                }
                            }
                        }
                    }
                }

            }
            return allChecked;
        }

        #endregion      

        #endregion

        #region Events

        void Control_SaveNeeded(object o, SaveNeededRoutedEventArgs e)
        {
           
                bool allChecked = true;

                ExerciseLiftControl control = o as ExerciseLiftControl;
                ExerciseListControl parentControl = FindLift(control.Lift.LiftName);

                if (control != null)
                {
                    allChecked = CheckAllLiftsForChecked(control.Lift.LiftName);
                    parentControl.CompletedCheckBox.IsChecked = allChecked;

                    if (file.Lifts.IsPersonalRecord(control.Lift))
                    {
                        ResetAllLiftsPR(control.Lift.LiftName);
                        control.PRFlagLabel.Visibility = System.Windows.Visibility.Visible;
                    }
                    else
                    {
                        control.PRFlagLabel.Visibility = System.Windows.Visibility.Collapsed;
                    }
                }

                parentControl.BindLiftDetails();

                //parentControl.TonnageLabel.Text = string.Format(CultureInfo.CurrentCulture, "{0}: {1} {2}", UITextResources.TONNAGE_LBL,
                //       file.Lifts.GetTonnage(this.selectedDate, control.Lift.LiftName).ToString(CultureInfo.CurrentCulture),
                //       UITextResources.POUNDS_ABBR);
                //parentControl.OneRepMaxLabel.Text = string.Format(CultureInfo.CurrentCulture, "{0}: {1} {2}",
                //    UITextResources.BEST_1RM,
                //    file.Lifts.GetBestOneRep(this.selectedDate, control.Lift.LiftName).ToString("###", CultureInfo.CurrentCulture),
                //    UITextResources.POUNDS_ABBR);
                parentControl.RecordOneRepLabel.Text = file.Lifts.GetBestOneRepEver(control.Lift.LiftName).ToString("###", CultureInfo.CurrentCulture) + " lbs";

                if (MasterPivot.SelectedIndex != 0)
                {
                    IsolatedStorageHelper.SaveData(file);
                }

                if (e.CauseNavigationReset)
                {
                    MasterPivot.SelectedIndex = 0;
                }

           

        }

        void Control2_SaveNeeded(object sender, SaveNeededRoutedEventArgs e)
        {
            //Dispatcher.BeginInvoke(delegate()
            //{
            bool allChecked = true;

            ExerciseCardioControl control = sender as ExerciseCardioControl;
            ExerciseListControl parent = FindLift(control.CardioRun.CardioType);

            if (control != null)
            {
                allChecked = CheckAllCardioRunsForChecked(control.CardioRun.CardioType);
                parent.CompletedCheckBox.IsChecked = allChecked;
            }

            parent.BindRunDetails();

            if (MasterPivot.SelectedIndex != 0)
            {
                IsolatedStorageHelper.SaveData(file);
            }

            if (e.CauseNavigationReset)
            {
                MasterPivot.SelectedIndex = 0;
            }

            //});
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            selectedDate = DateTime.Now;
            string date = string.Empty;

            if (NavigationContext.QueryString.TryGetValue("selectedDate", out date))
            {
                selectedDate = DateTime.Parse(date,CultureInfo.CurrentCulture);
            }

            AppSettings settings = new AppSettings();
            MasterPivotItem.Header = selectedDate.ToString(settings.ScheduleDateFormatString,CultureInfo.CurrentCulture);
            //DateTextBlock.Text = selectedDate.ToShortDateString();
           BindWorkouts(selectedDate);

            base.OnNavigatedTo(e);
        }

        private void ExerciseList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ExerciseList != null)
            {
                ExerciseListControl control = ExerciseList.SelectedItem as ExerciseListControl;

                if (control != null)
                {
                    //foreach (var child in ExerciseList.Items)
                    //{
                    //    ExerciseListControl childc = child as ExerciseListControl;

                    //    if (childc != null)
                    //    {
                    //        childc.Opacity = 0.6;
                    //    }
                    //}

                    //control.Opacity = 1.0;

                MasterPivot.SelectedIndex = (int)control.Tag;
                ExerciseList.SelectedIndex = -1;

                }
            }
        }

        private void AddWorkoutButton_Click(object sender, EventArgs e)
        {
            string parms = string.Format(CultureInfo.CurrentCulture,"?selectedDate={0}&protocol={1}",
            selectedDate.ToShortDateString(),
            "Open");
            Uri next = new Uri("/AddExercises.xaml" + parms, UriKind.RelativeOrAbsolute);
            NavigationService.Navigate(next);
        }

        private void AddJogButton_Click(object sender, EventArgs e)
        {
            string parms = string.Format(CultureInfo.CurrentCulture, "?selectedDate={0}",
            selectedDate.ToShortDateString());
            Uri uri = new Uri("/AddCardio.xaml"+parms, UriKind.RelativeOrAbsolute);
            NavigationService.Navigate(uri);
        }

        private void HomeWorkoutButton_Click(object sender, EventArgs e)
        {
            MasterPivot.SelectedIndex = 0;
        }

        private void HomeMenuItem_Click(object sender, EventArgs e)
        {
             if (MasterPivot.SelectedIndex != 0)
             {
                 MasterPivot.SelectedIndex = 0;
             }
        }

        private void control_ItemDeleted(object sender, RoutedEventArgs e)
        {
            file = IsolatedStorageHelper.LoadData();
            BindWorkouts(selectedDate);
        }

        private void Control_LiftItemDeleted(object sender, RoutedEventArgs e)
        {
            //int index = MasterPivot.SelectedIndex;
            file = IsolatedStorageHelper.LoadData();
            BindWorkouts(selectedDate);

            //if (MasterPivot.Items.Count() - 1 >= index)
            //{
            //    MasterPivot.SelectedIndex = index;
            //}
           
        }

        private void HomeWorkoutButton_Click_1(object sender, EventArgs e)
        {
            Uri uri = new Uri("/MainPage.xaml",UriKind.RelativeOrAbsolute);
            NavigationService.Navigate(uri);
        }

        private void ShareButton_Click(object sender, EventArgs e)
        {
            EmailComposeTask task = new EmailComposeTask();
            task.Body = file.GetWorkoutTextSummary(selectedDate);
            task.Subject = "My Workout Summary For " + selectedDate.ToShortTimeString();
            task.Show();
        }

        private void Rest60MenuItem_Click(object sender, EventArgs e)
        {
            ApplicationBarMenuItem item = sender as ApplicationBarMenuItem;

            string time = item.Text.Replace("rest ", String.Empty).Replace(" seconds", string.Empty);

            Uri uri = new Uri(string.Format(CultureInfo.CurrentCulture, "/RestTimer.xaml?length={0}", time), UriKind.RelativeOrAbsolute);
            NavigationService.Navigate(uri);
        }

        #endregion

        //private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        //{
        //    AppSettings settings = new AppSettings();
        //    MasterPivotItem.Header = selectedDate.ToString(settings.ScheduleDateFormatString, CultureInfo.CurrentCulture);
        //    //DateTextBlock.Text = selectedDate.ToShortDateString();
        //    BindWorkouts(selectedDate);
        //}

    }
}