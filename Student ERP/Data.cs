using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using Student_ERP.Annotations;

namespace Student_ERP
{
    [XmlRoot("Students")]
    public class Data : INotifyPropertyChanged
    {
        private ObservableCollection<Student> _items;
        public Data() { Items = new ObservableCollection<Student>();}

        [XmlElement("Student")]
        public ObservableCollection<Student> Items
        {
            get { return _items; }
            set
            {
                if (Equals(value, _items)) return;
                _items = value;
                OnPropertyChanged("Items");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}