using System;
using System.Collections.Generic;

namespace Grupa1Ozo.Models
{
    public partial class ZaposleniciStruka
    {
        public int ZaposleniciId { get; set; }
        public int StrukaId { get; set; }

        public virtual Struka Struka { get; set; }
        public virtual Zaposlenici Zaposlenici { get; set; }
    }
}
