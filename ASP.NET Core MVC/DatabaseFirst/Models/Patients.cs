using System;
using System.Collections.Generic;

namespace DatabaseFirst.Models
{
    public partial class Patients
    {
        public Patients()
        {
            Patientdoctors = new HashSet<Patientdoctors>();
        }

        public long Id { get; set; }
        public string Iin { get; set; }
        public string Familyname { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Address { get; set; }
        public string Phonenumber { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Deleted { get; set; }

        public virtual ICollection<Patientdoctors> Patientdoctors { get; set; }
    }
}
