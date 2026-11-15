using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MirIgryshek.Models
{
    public partial class Proizvoditel
    {
        public Proizvoditel()
        {
            TovarProizvoditel = new HashSet<TovarProizvoditel>();
        }

        public int IdProizvoditel { get; set; }
        public string Ima { get; set; }

        public virtual ICollection<TovarProizvoditel> TovarProizvoditel { get; set; }
    }
}
