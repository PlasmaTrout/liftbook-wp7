﻿#pragma checksum "E:\Bazaar\wp7\trunk\Liftbook\LatchD.Liftbook\Controls\CardioListControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D468ADACA0B789EB772FB2B6F56853FA"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.225
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
    
    
    public partial class CardioListControl : System.Windows.Controls.UserControl {
        
        internal Microsoft.Phone.Controls.ContextMenu ContextMenu2;
        
        internal Microsoft.Phone.Controls.MenuItem DeleteItem;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.TextBlock CardioTextBlock;
        
        internal System.Windows.Controls.CheckBox CompletedCheckBox;
        
        internal System.Windows.Controls.TextBlock DistanceLabel;
        
        internal System.Windows.Controls.TextBlock TimeLabel;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/LatchD.Liftbook;component/Controls/CardioListControl.xaml", System.UriKind.Relative));
            this.ContextMenu2 = ((Microsoft.Phone.Controls.ContextMenu)(this.FindName("ContextMenu2")));
            this.DeleteItem = ((Microsoft.Phone.Controls.MenuItem)(this.FindName("DeleteItem")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.CardioTextBlock = ((System.Windows.Controls.TextBlock)(this.FindName("CardioTextBlock")));
            this.CompletedCheckBox = ((System.Windows.Controls.CheckBox)(this.FindName("CompletedCheckBox")));
            this.DistanceLabel = ((System.Windows.Controls.TextBlock)(this.FindName("DistanceLabel")));
            this.TimeLabel = ((System.Windows.Controls.TextBlock)(this.FindName("TimeLabel")));
        }
    }
}

