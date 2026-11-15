using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MirIgryshek.Models
{
    public partial class TovarPostavshik
    {
        public string Articul { get; set; }
        public int IdPostavshik { get; set; }

        public virtual Tovar ArticulNavigation { get; set; }
        public virtual Postavshik IdPostavshikNavigation { get; set; }
    }
}
