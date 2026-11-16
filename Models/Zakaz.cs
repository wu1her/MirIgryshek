using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MirIgryshek.Models
{
    public partial class Zakaz
    {
        public Zakaz()
        {
            PolzovatelZakaz = new HashSet<PolzovatelZakaz>();
            ZakazStatus = new HashSet<ZakazStatus>();
            ZakazTovar = new HashSet<ZakazTovar>();
        }

        public int Nomer { get; set; }
        public int Kod { get; set; }
        public DateTime DataZakaza { get; set; }
        public DateTime DataDostavka { get; set; }

        public virtual ICollection<PolzovatelZakaz> PolzovatelZakaz { get; set; }
        public virtual ICollection<ZakazStatus> ZakazStatus { get; set; }
        public virtual ICollection<ZakazTovar> ZakazTovar { get; set; }
    }
}
