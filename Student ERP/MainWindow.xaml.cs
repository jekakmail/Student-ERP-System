using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Student_ERP.Annotations;

namespace Student_ERP
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public Data _data;

        private string _file = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
        }

        public Data Data
        {
            get { return _data; }
            set
            {
                if (Equals(value, _data)) return;
                _data = value;
                OnPropertyChanged("Data");
            }
        }

        private void Saving(string fName)
        {
            string fn = fName;
            if (string.IsNullOrEmpty(fn))
            {
                fn = _file;
            }
            
            var xml = new XmlSerializer(typeof(Data));

            var encoding = Encoding.GetEncoding("UTF-8");


            try
            {
                using (StreamWriter sw = new StreamWriter(fn, false, encoding))
                {
                    xml.Serialize(sw, _data, new XmlSerializerNamespaces(new[] {new XmlQualifiedName("", "")}));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveCommand_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            if (Data!=null)
                e.CanExecute = true;
        }
        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(_file))
            {
                var saveDlg = new SaveFileDialog() { Title = "Save", AddExtension = true, Filter = "XML Files|*.xml" };

                if (!(bool)saveDlg.ShowDialog()) return;

                _file = saveDlg.FileName;

                if (string.IsNullOrEmpty(_file))
                    return;
            }

            Saving(string.Empty);
        }

        private void SaveAsCommand_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_file))
                e.CanExecute = true;
        }

        private void SaveAsCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var saveAsDlg = new SaveFileDialog() {Title = "Save as", AddExtension = true, Filter = "XML Files|*.xml"};

            if (!(bool)saveAsDlg.ShowDialog()) return;
            
            var fn = saveAsDlg.FileName;

            if (string.IsNullOrEmpty(fn))
                return;

            Saving(fn);
        }

        private void DeleteCommand_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            if (LbStudents.SelectedItems.Count > 0)
                e.CanExecute = true;
        }

        private void DeleteCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if(MessageBox.Show($"Are you sure you want to remove the {LbStudents.SelectedItems.Count} elements?","Removing",MessageBoxButton.YesNo,MessageBoxImage.Question)==MessageBoxResult.No)
                return;

            if (LbStudents.SelectedItems.Count > 1)
            {
                var students = new List<Student>();
                foreach (var item in LbStudents.SelectedItems)
                {
                    students.Add(item as Student);
                }

                
                foreach (var item in students)
                {
                    Data.Items.Remove(item as Student);
                }
            }
            else if (_data != null) _data.Items.Remove(LbStudents.SelectedItem as Student);
        }
        
        private void EditCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if(LbStudents.SelectedItems.Count>1)
                return;

            var st = LbStudents.SelectedItem as Student;
            var el = Data.Items.First(p => p == st);
            var editWnd = new AddEditWindow(st);
            editWnd.ShowDialog();

            if (editWnd.StudentObj != null)
            {
                el.Id = editWnd.StudentObj.Id;
                el.Age = editWnd.StudentObj.Age;
                el.FirstName = editWnd.StudentObj.FirstName;
                el.Last = editWnd.StudentObj.Last;
                el.Gender = editWnd.StudentObj.Gender;
            }
        }

        private void EditCommand_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            if (LbStudents.SelectedItems.Count==1)
                e.CanExecute = true;
        }
        
        private void AddCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var addWnd = new AddEditWindow();
            addWnd.ShowDialog();
            if (Data == null)
            {
                _data = new Data();
            }

            var lastOrDefault = _data.Items.LastOrDefault();
            if (lastOrDefault != null)
            {
                var id = lastOrDefault.Id;
                if (addWnd.StudentObj != null) addWnd.StudentObj.Id = ++id;
            }
            if (addWnd.StudentObj != null)
            {
                _data.Items.Add(addWnd.StudentObj);
                OnPropertyChanged("Data");
            }
                
        }

        private void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (Data != null)
            {
                if (MessageBox.Show("The discovery will lead to the loss of unsaved data. Would you like to continue?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    return;
            }


            var openDlg = new OpenFileDialog() { Filter = "XML files|*.xml", CheckFileExists = true };

            if (!(bool)openDlg.ShowDialog()) return;

            _file = openDlg.FileName;

            try
            {
                if (!string.IsNullOrEmpty(_file))
                    using (var reader = new StreamReader(_file))
                    {
                        XmlSerializer deserializer = new XmlSerializer(typeof(Data));
                        Data = (Data)deserializer.Deserialize(reader);
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //LbStudents.ItemsSource = Data.Items;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AboutCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var aboutWnd = new AboutWindow();
            aboutWnd.ShowDialog();
        }
    }
}
