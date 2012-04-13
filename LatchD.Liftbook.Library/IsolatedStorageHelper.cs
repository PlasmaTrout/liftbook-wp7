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
using System.IO.IsolatedStorage;
using System.Xml.Serialization;
using System.Text;
using System.IO;
using System.Globalization;

namespace WPLiftsLibrary
{
    public class IsolatedStorageHelper
    {
        public static void SaveData(WpLiftsDataFile log)
        {
            string fileName = "wpliftslog"+AppSettings.APP_VER.ToString(CultureInfo.CurrentCulture)+".dat";
            using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(fileName, System.IO.FileMode.Create, file))
                {
                    XmlSerializer s = new XmlSerializer(log.GetType());
                    s.Serialize(stream, log);
                }
            }
        }

        public static WpLiftsDataFile LoadData()
        {
            WpLiftsDataFile log = null;
            string fileName = "wpliftslog"+AppSettings.APP_VER.ToString(CultureInfo.CurrentCulture)+".dat";

            try
            {
                using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(fileName, System.IO.FileMode.Open, file))
                    {
                        XmlSerializer s = new XmlSerializer(typeof(WpLiftsDataFile));
                        try
                        {
                            object o = s.Deserialize(stream);
                            log = o as WpLiftsDataFile;
                        }
                        catch (InvalidOperationException)
                        {
                            stream.Position = 0;
                            StringBuilder sb = new StringBuilder();
                            StreamReader reader = new StreamReader(stream);
                            sb.Append(reader.ReadToEnd());
                            MessageBox.Show(string.Format(CultureInfo.CurrentCulture,TextualResources.RESET_ERROR));
                            log = new WpLiftsDataFile();
                            SaveData(log);
                        }
                    }
                }
            }
            catch (IsolatedStorageException)
            {
                MessageBoxResult result = MessageBox.Show(string.Format(CultureInfo.CurrentCulture,TextualResources.DATAFILE_CREATE));

                if (result == MessageBoxResult.OK)
                {
                    SaveData(new WpLiftsDataFile());
                    //SaveData(WpLiftsDataFile.CreateTestFile());
                    WpLiftsDataFile newLog = LoadData();
                    return newLog;
                }
                

            }

            return log;
        }
    }
}
