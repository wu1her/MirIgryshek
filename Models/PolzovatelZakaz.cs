using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MirIgryshek.Models
{
    public partial class PolzovatelZakaz
    {
        public string Login { get; set; }
        public int Nomer { get; set; }

        public virtual Polzovatel LoginNavigation { get; set; }
        public virtual Zakaz NomerNavigation { get; set; }
    }
}
