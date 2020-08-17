using System;
using System.Collections.Generic;

namespace Grupa1Ozo.Models
{
    public partial class Struka
    {
        public Struka()
        {
            ZaposleniciStruka = new HashSet<ZaposleniciStruka>();
        }

        public int StrukaId { get; set; }
        public string Naziv { get; set; }

        public virtual ICollection<ZaposleniciStruka> ZaposleniciStruka { get; set; }
    }
}
