using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MirIgryshek.Models
{
    public partial class ZakazPynkti
    {
        public int Nomer { get; set; }
        public int IdPynkt { get; set; }

        public virtual Pynkti IdPynktNavigation { get; set; }
        public virtual Zakaz NomerNavigation { get; set; }
    }
}
