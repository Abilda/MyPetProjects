using System;
using System.Collections.Generic;

namespace DatabaseFirst.Models
{
    public partial class Specialization
    {
        public Specialization()
        {
            Doctors = new HashSet<Doctors>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Deleted { get; set; }

        public virtual ICollection<Doctors> Doctors { get; set; }
    }
}
