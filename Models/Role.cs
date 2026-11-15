using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MirIgryshek.Models
{
    public partial class Role
    {
        public Role()
        {
            PolzovatelRole = new HashSet<PolzovatelRole>();
        }

        public int IdRole { get; set; }
        public string Ima { get; set; }

        public virtual ICollection<PolzovatelRole> PolzovatelRole { get; set; }
    }
}
