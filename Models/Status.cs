using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MirIgryshek.Models
{
    public partial class Status
    {
        public Status()
        {
            ZakazStatus = new HashSet<ZakazStatus>();
        }

        public int IdStatus { get; set; }
        public string Ima { get; set; }

        public virtual ICollection<ZakazStatus> ZakazStatus { get; set; }
    }
}
