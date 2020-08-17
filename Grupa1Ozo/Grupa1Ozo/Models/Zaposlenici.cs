using System;
using System.Collections.Generic;

namespace Grupa1Ozo.Models
{
    public partial class Zaposlenici
    {
        public Zaposlenici()
        {
            ZaposleniciCertifikati = new HashSet<ZaposleniciCertifikati>();
            ZaposleniciStruka = new HashSet<ZaposleniciStruka>();
        }

        public int ZaposleniciId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public int? OpcinaId { get; set; }
        public int? UslugaId { get; set; }

        public virtual Opcina Opcina { get; set; }
        public virtual Usluga Usluga { get; set; }
        public virtual ICollection<ZaposleniciCertifikati> ZaposleniciCertifikati { get; set; }
        public virtual ICollection<ZaposleniciStruka> ZaposleniciStruka { get; set; }
    }
}
