using System;
using System.Collections.Generic;

namespace Api.Models
{
    public partial class Conclusions
    {
        public long Id { get; set; }
        public string Complaints { get; set; }
        public string Diagnosis { get; set; }
        public DateTime Visitdate { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Deleted { get; set; }
        public long Patientdoctorid { get; set; }

        public virtual Patientdoctors Patientdoctor { get; set; }
    }
}
