﻿#pragma checksum "C:\Users\JEFFSMAC\Projects\liftbook-wp7\v12\Liftbook\LatchD.Liftbook\Controls\ExerciseListControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "696E3AEEB9FD9DB7604ACE0FB5D19F03"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.261
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
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
    
    
    public partial class ExerciseListControl : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Border MainBorder;
        
        internal Microsoft.Phone.Controls.ContextMenu ContextMenu;
        
        internal Microsoft.Phone.Controls.MenuItem DeleteMenuItem;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Media.ImageBrush LogoImageBrush;
        
        internal System.Windows.Controls.TextBlock WorkoutTextBlock;
        
        internal System.Windows.Controls.CheckBox CompletedCheckBox;
        
        internal System.Windows.Controls.TextBlock RecordOneRepLabel;
        
        internal System.Windows.Controls.TextBlock OneRepMaxLabel;
        
        internal System.Windows.Controls.TextBlock TonnageLabel;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/LatchD.Liftbook;component/Controls/ExerciseListControl.xaml", System.UriKind.Relative));
            this.MainBorder = ((System.Windows.Controls.Border)(this.FindName("MainBorder")));
            this.ContextMenu = ((Microsoft.Phone.Controls.ContextMenu)(this.FindName("ContextMenu")));
            this.DeleteMenuItem = ((Microsoft.Phone.Controls.MenuItem)(this.FindName("DeleteMenuItem")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.LogoImageBrush = ((System.Windows.Media.ImageBrush)(this.FindName("LogoImageBrush")));
            this.WorkoutTextBlock = ((System.Windows.Controls.TextBlock)(this.FindName("WorkoutTextBlock")));
            this.CompletedCheckBox = ((System.Windows.Controls.CheckBox)(this.FindName("CompletedCheckBox")));
            this.RecordOneRepLabel = ((System.Windows.Controls.TextBlock)(this.FindName("RecordOneRepLabel")));
            this.OneRepMaxLabel = ((System.Windows.Controls.TextBlock)(this.FindName("OneRepMaxLabel")));
            this.TonnageLabel = ((System.Windows.Controls.TextBlock)(this.FindName("TonnageLabel")));
        }
    }
}
