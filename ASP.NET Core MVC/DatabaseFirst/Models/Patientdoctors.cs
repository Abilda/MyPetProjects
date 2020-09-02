using System;
using System.Collections.Generic;

namespace DatabaseFirst.Models
{
    public partial class Patientdoctors
    {
        public Patientdoctors()
        {
            Conclusions = new HashSet<Conclusions>();
        }

        public long Id { get; set; }
        public long Doctorid { get; set; }
        public long Patientid { get; set; }
        public DateTime Visitdate { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Deleted { get; set; }

        public virtual Doctors Doctor { get; set; }
        public virtual Patients Patient { get; set; }
        public virtual ICollection<Conclusions> Conclusions { get; set; }
    }
}
