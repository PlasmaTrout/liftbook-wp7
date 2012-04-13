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
using System.Threading;
using System.Globalization;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;
using System.IO;

namespace WPLifts
{
    public partial class RestTimer : PhoneApplicationPage,IDisposable
    {
        Timer t;
        int seconds = 60;
        SoundEffect buzzerSound;

        public RestTimer()
        {
            InitializeComponent();
            SetupSound();
        }

        private void SetupSound()
        {
            FrameworkDispatcher.Update();
            Stream s = TitleContainer.OpenStream("Sounds/buzzer_x2.wav");
            buzzerSound = SoundEffect.FromStream(s);
        }

        private void UpdateTimerCountLabel()
        {
            TimeCountLabel.Text = this.seconds.ToString("000", CultureInfo.CurrentCulture);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            string secondsLength = string.Empty;

            if (NavigationContext.QueryString.TryGetValue("length", out secondsLength))
            {
                seconds = int.Parse(secondsLength,CultureInfo.CurrentCulture);
            }

            UpdateTimerCountLabel();

            t = new Timer(new TimerCallback(TimerCallback), null, 1000, 1000);

            base.OnNavigatedTo(e);
        }

        private void TimerCallback(object sender)
        {
            if (seconds == 0)
            {
                t.Dispose();

                this.Dispatcher.BeginInvoke(delegate()
                {
                    FrameworkDispatcher.Update();
                    buzzerSound.Play(1.0f, 0.0f, 0.0f);
                    NavigationService.GoBack();
                    
                });

                
            }
            else
            {
                seconds = seconds - 1;
            }


            this.Dispatcher.BeginInvoke(delegate()
            {
                UpdateTimerCountLabel();
            });

        }


        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool all)
        {
            if (all)
            {
                t.Dispose();
            }
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }
        
    }
}