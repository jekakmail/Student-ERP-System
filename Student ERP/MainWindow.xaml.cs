using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Win32;

namespace Student_ERP
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Data _data;

        private string _file = string.Empty;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var openDlg = new OpenFileDialog() {Filter = "XML files|*.xml"};

            if (openDlg.ShowDialog() == null) return;

            _file = openDlg.FileName;

            try
            {
                using (var reader = new StreamReader(_file))
                {
                    XmlSerializer deserializer = new XmlSerializer(typeof(Data));
                    _data = (Data)deserializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void Saving(string fName)
        {
            string fn = string.Empty;
            if (string.IsNullOrEmpty(fName))
            {
                fn = _file;
            }
            
            var xml = new XmlSerializer(typeof(Data));

            var encoding = Encoding.GetEncoding("UTF-8");
            
            using (StreamWriter sw = new StreamWriter(fn, false, encoding))
            {
                xml.Serialize(sw, _data, new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") }));
            }
        }

        private void CloseWindow_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CloseWindow_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }

        private void SaveCommand_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_file))
                e.CanExecute = true;
        }
        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Saving(string.Empty);
        }
    }
}
