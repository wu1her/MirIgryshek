using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MirIgryshek.Models
{
    public partial class ZakazStatus
    {
        public int Nomer { get; set; }
        public int IdStatus { get; set; }

        public virtual Status IdStatusNavigation { get; set; }
        public virtual Zakaz NomerNavigation { get; set; }
    }
}
