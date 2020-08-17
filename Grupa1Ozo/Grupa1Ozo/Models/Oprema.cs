using System;
using System.Collections.Generic;

namespace Grupa1Ozo.Models
{
    public partial class Oprema
    {
        public int OpremaId { get; set; }
        public string NazivOpreme { get; set; }
        public int Raspolozivost { get; set; }
        public int SkladisteId { get; set; }
        public int UslugaId { get; set; }

        public virtual Skladiste Skladiste { get; set; }
        public virtual Usluga Usluga { get; set; }
    }
}
