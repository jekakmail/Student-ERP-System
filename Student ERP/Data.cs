using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace Student_ERP
{
    [XmlRoot("Students")]
    public class Data
    {
        public Data() { Items = new ObservableCollection<Student>();}


        [XmlElement("Student")] 
        public ObservableCollection<Student> Items { get; set; }
    }
}