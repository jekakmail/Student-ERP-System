using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Student_ERP.Annotations;

namespace Student_ERP
{
    [XmlType("Student")]
    public class Student : INotifyPropertyChanged, IDataErrorInfo
    {
        private int _id;
        private string _firstName;
        private string _last;
        private int _age;
        private int _gender;
        
        [XmlAttribute]
        public int Id
        {
            get { return _id; }
            set
            {
                if (value == _id) return;
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        [XmlElement(ElementName = "FirstName")]
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (value == _firstName) return;
                _firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        [XmlElement(ElementName = "Last")]
        public string Last
        {
            get { return _last; }
            set
            {
                if (value == _last) return;
                _last = value;
                OnPropertyChanged("Last");
            }
        }

        [XmlElement(ElementName = "Age")]
        public int Age
        {
            get { return _age; }
            set
            {
                if (value == _age) return;
                _age = value;
                OnPropertyChanged("Age");
            }
        }

        [XmlElement(ElementName = "Gender")]
        public int Gender
        {
            get { return _gender; }
            set
            {
                if (value == _gender) return;
                _gender = value;
                OnPropertyChanged("Gender");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case "FirstName":
                        if (string.IsNullOrEmpty(FirstName))
                        {
                            error = "Поле обязательно для заполнения";
                            Error = "Поле Имя пустое";
                        }
                        else
                        {
                            Error = string.Empty;
                        }
                        break;
                    case "Last":
                        if (string.IsNullOrEmpty(Last))
                        {
                            error = "Поле обязательно для заполнения";
                            Error = "Поле Фамилия пустое";
                        }
                        else
                        {
                            Error = string.Empty;
                        }
                        break;
                    case "Age":
                        if (Age < 16 || Age > 100)
                        {
                            error = "Возраст должен быть в диапазоне [16:100]";
                            Error = "Возраст не в разрешенном диапазоне";
                        }
                        else
                        {
                            Error = string.Empty;
                        }
                        break;
                }
                return error;
            }
        }

        [XmlIgnore]
        public string Error { get; set; }
    }
}