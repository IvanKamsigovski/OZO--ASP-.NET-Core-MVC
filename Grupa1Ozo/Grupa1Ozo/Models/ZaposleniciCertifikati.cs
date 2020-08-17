using System;
using System.Collections.Generic;

namespace Grupa1Ozo.Models
{
    public partial class ZaposleniciCertifikati
    {
        public int ZaposleniciId { get; set; }
        public int CertifikatiId { get; set; }

        public virtual Certifikati Certifikati { get; set; }
        public virtual Zaposlenici Zaposlenici { get; set; }
    }
}
