using System;
using System.Collections.Generic;

namespace Grupa1Ozo.Models
{
    public partial class KategorijaUsluge
    {
        public KategorijaUsluge()
        {
            Usluga = new HashSet<Usluga>();
        }

        public int KategorijaUslugeId { get; set; }
        public string Naziv { get; set; }

        public virtual ICollection<Usluga> Usluga { get; set; }
    }
}
