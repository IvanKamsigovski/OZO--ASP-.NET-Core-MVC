using System;
using System.Collections.Generic;

namespace Grupa1Ozo.Models
{
    public partial class Skladiste
    {
        public Skladiste()
        {
            Oprema = new HashSet<Oprema>();
        }

        public int SkladisteId { get; set; }
        public string Naziv { get; set; }
        public int LokacijaId { get; set; }

        public virtual Lokacija Lokacija { get; set; }
        public virtual ICollection<Oprema> Oprema { get; set; }
    }
}
