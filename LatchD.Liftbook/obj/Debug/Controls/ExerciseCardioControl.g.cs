﻿#pragma checksum "C:\Users\JEFFSMAC\Projects\liftbook-wp7\LatchD.Liftbook\Controls\ExerciseCardioControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "57EA208AED7D44683C10EBC4FB3B5338"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.261
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace WPLifts.Controls {
    
    
    public partial class ExerciseCardioControl : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.CheckBox IsCompletedCheckBox;
        
        internal System.Windows.Controls.TextBox HoursTextBox;
        
        internal System.Windows.Controls.TextBox MinutesTextBox;
        
        internal System.Windows.Controls.TextBox SecondsTextBox;
        
        internal System.Windows.Controls.TextBox MilesTextBox;
        
        internal System.Windows.Documents.Run PaceRun;
        
        internal System.Windows.Documents.Run MPHRun;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/LatchD.Liftbook;component/Controls/ExerciseCardioControl.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.IsCompletedCheckBox = ((System.Windows.Controls.CheckBox)(this.FindName("IsCompletedCheckBox")));
            this.HoursTextBox = ((System.Windows.Controls.TextBox)(this.FindName("HoursTextBox")));
            this.MinutesTextBox = ((System.Windows.Controls.TextBox)(this.FindName("MinutesTextBox")));
            this.SecondsTextBox = ((System.Windows.Controls.TextBox)(this.FindName("SecondsTextBox")));
            this.MilesTextBox = ((System.Windows.Controls.TextBox)(this.FindName("MilesTextBox")));
            this.PaceRun = ((System.Windows.Documents.Run)(this.FindName("PaceRun")));
            this.MPHRun = ((System.Windows.Documents.Run)(this.FindName("MPHRun")));
        }
    }
}

