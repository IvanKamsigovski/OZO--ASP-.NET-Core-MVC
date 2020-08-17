using System;
using System.Collections.Generic;

namespace Grupa1Ozo.Models
{
    public partial class Lokacija
    {
        public Lokacija()
        {
            Skladiste = new HashSet<Skladiste>();
        }

        public int LokacijaId { get; set; }
        public string Adresa { get; set; }
        public string Grad { get; set; }

        public virtual ICollection<Skladiste> Skladiste { get; set; }
    }
}
