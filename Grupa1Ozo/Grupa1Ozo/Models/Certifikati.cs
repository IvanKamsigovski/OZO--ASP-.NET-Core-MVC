using System;
using System.Collections.Generic;

namespace Grupa1Ozo.Models
{
    public partial class Certifikati
    {
        public Certifikati()
        {
            ZaposleniciCertifikati = new HashSet<ZaposleniciCertifikati>();
        }

        public int CertifikatiId { get; set; }
        public string Naziv { get; set; }

        public virtual ICollection<ZaposleniciCertifikati> ZaposleniciCertifikati { get; set; }
    }
}
