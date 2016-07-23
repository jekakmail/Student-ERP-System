using System.Xml.Serialization;

namespace Student_ERP
{
    [XmlType("Student")]
    public class Student
    {
        [XmlAttribute]
        public int Id { get; set; }

        [XmlElement(ElementName = "FirstName")]
        public string FirstName { get; set; }

        [XmlElement(ElementName = "Last")]
        public string Last { get; set; }

        [XmlElement(ElementName = "Age")]
        public int Age { get; set; }

        [XmlElement(ElementName = "Gender")]
        public int Gender { get; set; }
    }
}