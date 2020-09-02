using System;
using System.Collections.Generic;

namespace DatabaseFirst.Models
{
    public partial class Doctors
    {
        public Doctors()
        {
            Patientdoctors = new HashSet<Patientdoctors>();
        }

        public long Id { get; set; }
        public string Familyname { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Deleted { get; set; }
        public long Specializationid { get; set; }

        public virtual Specialization Specialization { get; set; }
        public virtual ICollection<Patientdoctors> Patientdoctors { get; set; }
    }
}
