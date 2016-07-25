using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;
using Student_ERP.Annotations;

namespace Student_ERP
{
    public sealed partial class AddEditWindow : Window, INotifyPropertyChanged
    {
        
        public AddEditWindow()
        {
            InitializeComponent();

            StudentObj = new Student();
        }

        private readonly Student _conStudent;
        public AddEditWindow(object student)
        {
            InitializeComponent();
            _conStudent = student as Student;
            if (_conStudent != null)
                StudentObj = new Student()
                {
                    Age = _conStudent.Age,
                    FirstName = _conStudent.FirstName,
                    Gender = _conStudent.Gender,
                    Last = _conStudent.Last,
                    Id = _conStudent.Id
                };
            if (StudentObj != null) SelectedRadioButton = (GenderRadioButtons)StudentObj.Gender;
        }

        public GenderRadioButtons SelectedRadioButton
        {
            get { return _selectedRadioButton; }
            set
            {
                if (value == _selectedRadioButton) return;
                _selectedRadioButton = value;
                _studentObj.Gender = (int) value;
                OnPropertyChanged("SelectedRadioButton");
            }
        }

        private Student _studentObj;
        private GenderRadioButtons _selectedRadioButton;

        public Student StudentObj
        {
            get { return _studentObj; }
            set
            {
                if (Equals(value, _studentObj)) return;
                _studentObj = value;
                OnPropertyChanged("StudentObj");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            StudentObj = _conStudent;
        }

        private void SaveCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(StudentObj.Error))
                Close();
        }

        private void SaveCommand_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            if ( StudentObj != null && string.IsNullOrEmpty(StudentObj.Error))
                e.CanExecute = true;
        }
    }
}
