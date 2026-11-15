using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MirIgryshek.Models
{
    public partial class Tovar
    {
        public Tovar()
        {
            TovarKategoria = new HashSet<TovarKategoria>();
            TovarPostavshik = new HashSet<TovarPostavshik>();
            TovarProizvoditel = new HashSet<TovarProizvoditel>();
            ZakazTovar = new HashSet<ZakazTovar>();
        }

        public string Articul { get; set; }
        public string Ima { get; set; }
        public string Opisanie { get; set; }
        public decimal Cena { get; set; }
        public int Skidka { get; set; }
        public int Kolichestvo { get; set; }
        public string Izmerenie { get; set; }
        public string Photo { get; set; }

        public virtual ICollection<TovarKategoria> TovarKategoria { get; set; }
        public virtual ICollection<TovarPostavshik> TovarPostavshik { get; set; }
        public virtual ICollection<TovarProizvoditel> TovarProizvoditel { get; set; }
        public virtual ICollection<ZakazTovar> ZakazTovar { get; set; }
    }
}
