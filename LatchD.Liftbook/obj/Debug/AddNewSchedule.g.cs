﻿#pragma checksum "C:\Users\JEFFSMAC\Projects\liftbook-wp7\v12\Liftbook\LatchD.Liftbook\AddNewSchedule.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "8A33413A8056B29B0C4EFBB51E218133"
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
using Microsoft.Phone.Shell;
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


namespace WPLifts {
    
    
    public partial class AddNewSchedule : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.TextBlock ApplicationTitle;
        
        internal System.Windows.Controls.TextBlock PageTitle;
        
        internal Microsoft.Phone.Controls.DatePicker SelectedDatePicker;
        
        internal System.Windows.Controls.ComboBox ProtocolCombo;
        
        internal System.Windows.Controls.ComboBoxItem OpenProtocolItem;
        
        internal System.Windows.Controls.ComboBoxItem Wendler531Item;
        
        internal System.Windows.Controls.ComboBoxItem Beginning5x5Item;
        
        internal System.Windows.Controls.ComboBoxItem BeginningOSSItem;
        
        internal System.Windows.Controls.TextBlock DescriptionBlock;
        
        internal System.Windows.Controls.HyperlinkButton HyperInfoLink;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton NextButton;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/LatchD.Liftbook;component/AddNewSchedule.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.ApplicationTitle = ((System.Windows.Controls.TextBlock)(this.FindName("ApplicationTitle")));
            this.PageTitle = ((System.Windows.Controls.TextBlock)(this.FindName("PageTitle")));
            this.SelectedDatePicker = ((Microsoft.Phone.Controls.DatePicker)(this.FindName("SelectedDatePicker")));
            this.ProtocolCombo = ((System.Windows.Controls.ComboBox)(this.FindName("ProtocolCombo")));
            this.OpenProtocolItem = ((System.Windows.Controls.ComboBoxItem)(this.FindName("OpenProtocolItem")));
            this.Wendler531Item = ((System.Windows.Controls.ComboBoxItem)(this.FindName("Wendler531Item")));
            this.Beginning5x5Item = ((System.Windows.Controls.ComboBoxItem)(this.FindName("Beginning5x5Item")));
            this.BeginningOSSItem = ((System.Windows.Controls.ComboBoxItem)(this.FindName("BeginningOSSItem")));
            this.DescriptionBlock = ((System.Windows.Controls.TextBlock)(this.FindName("DescriptionBlock")));
            this.HyperInfoLink = ((System.Windows.Controls.HyperlinkButton)(this.FindName("HyperInfoLink")));
            this.NextButton = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("NextButton")));
        }
    }
}

