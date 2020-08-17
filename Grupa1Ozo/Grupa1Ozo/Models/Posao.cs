using System;
using System.Collections.Generic;

namespace Grupa1Ozo.Models
{
    public partial class Posao
    {
        public Posao()
        {
            OpremaPosla = new HashSet<OpremaPosla>();
            ZaposleniciPosla = new HashSet<ZaposleniciPosla>();
        }

        public int PosaoId { get; set; }
        public string Opis { get; set; }
        public int? NatjecajId { get; set; }

        public virtual Natjecaj Natjecaj { get; set; }
        public virtual ICollection<OpremaPosla> OpremaPosla { get; set; }
        public virtual ICollection<ZaposleniciPosla> ZaposleniciPosla { get; set; }
    }
}
