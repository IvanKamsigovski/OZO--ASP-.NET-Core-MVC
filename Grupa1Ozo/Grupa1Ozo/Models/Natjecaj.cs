using System;
using System.Collections.Generic;

namespace Grupa1Ozo.Models
{
    public partial class Natjecaj
    {
        public Natjecaj()
        {
            Posao = new HashSet<Posao>();
            Usluga = new HashSet<Usluga>();
        }

        public int NatjecajId { get; set; }
        public string Opis { get; set; }
        public int JavniNatjecajId { get; set; }

        public virtual JavniNatjecaj JavniNatjecaj { get; set; }
        public virtual ICollection<Posao> Posao { get; set; }
        public virtual ICollection<Usluga> Usluga { get; set; }
    }
}
