using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace WPLiftsLibrary
{
    public class UIHelper
    {
        public static void WashTextField(TextBox box)
        {
            foreach (char c in box.Text.ToCharArray())
            {
                if (!char.IsNumber(c))
                {
                    box.Text = box.Text.Replace(c.ToString(), string.Empty);
                }
            }
        }
    }
}
