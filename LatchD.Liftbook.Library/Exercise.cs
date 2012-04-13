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
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace WPLiftsLibrary
{
    public class Exercise
    {
        #region Properties

        [XmlAttribute]
        public string Name
        {
            get;
            set;
        }

        [XmlAttribute]
        public LiftProtocol Protocol
        {
            get;
            set;
        }

        [XmlAttribute]
        public string VideoUri
        {
            get;
            set;
        }

        [XmlAttribute]
        public string ImageName
        {
            get;
            set;
        }

        #endregion
    }

    public class ExerciseCollection : Collection<Exercise>
    {
    }
}
