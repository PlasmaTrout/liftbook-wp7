﻿#pragma checksum "C:\Users\JEFFSMAC\Projects\liftbook-wp7\LatchD.Liftbook\Controls\ScheduledDateControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "DB2A999C5A9D81C40CE06648D5EFFFAB"
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
    
    
    public partial class ScheduledDateControl : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.TextBlock DateTextBlock;
        
        internal System.Windows.Controls.CheckBox CompletedCheck;
        
        internal System.Windows.Documents.Run LiftNumberRun;
        
        internal System.Windows.Documents.Run RunNumberRun;
        
        internal System.Windows.Controls.TextBlock ProtocolTextBlock;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/LatchD.Liftbook;component/Controls/ScheduledDateControl.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.DateTextBlock = ((System.Windows.Controls.TextBlock)(this.FindName("DateTextBlock")));
            this.CompletedCheck = ((System.Windows.Controls.CheckBox)(this.FindName("CompletedCheck")));
            this.LiftNumberRun = ((System.Windows.Documents.Run)(this.FindName("LiftNumberRun")));
            this.RunNumberRun = ((System.Windows.Documents.Run)(this.FindName("RunNumberRun")));
            this.ProtocolTextBlock = ((System.Windows.Controls.TextBlock)(this.FindName("ProtocolTextBlock")));
        }
    }
}

