using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MirIgryshek.Models
{
    public partial class Postavshik
    {
        public Postavshik()
        {
            TovarPostavshik = new HashSet<TovarPostavshik>();
        }

        public int IdPostavshik { get; set; }
        public string Ima { get; set; }

        public virtual ICollection<TovarPostavshik> TovarPostavshik { get; set; }
    }
}
