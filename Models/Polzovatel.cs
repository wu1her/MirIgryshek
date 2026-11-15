using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MirIgryshek.Models
{
    public partial class Polzovatel
    {
        public Polzovatel()
        {
            PolzovatelRole = new HashSet<PolzovatelRole>();
            PolzovatelZakaz = new HashSet<PolzovatelZakaz>();
        }

        public string Login { get; set; }
        public string Fio { get; set; }
        public string Parol { get; set; }

        public virtual ICollection<PolzovatelRole> PolzovatelRole { get; set; }
        public virtual ICollection<PolzovatelZakaz> PolzovatelZakaz { get; set; }
    }
}
