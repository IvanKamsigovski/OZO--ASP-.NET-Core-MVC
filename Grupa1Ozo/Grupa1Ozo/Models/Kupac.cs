using System;
using System.Collections.Generic;

namespace Grupa1Ozo.Models
{
    public partial class Kupac
    {
        public int KupacId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public int UslugaId { get; set; }

        public virtual Usluga Usluga { get; set; }
    }
}
