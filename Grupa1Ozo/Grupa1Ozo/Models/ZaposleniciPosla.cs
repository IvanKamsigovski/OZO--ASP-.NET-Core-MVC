using System;
using System.Collections.Generic;

namespace Grupa1Ozo.Models
{
    public partial class ZaposleniciPosla
    {
        public int ZaposleniciPoslaId { get; set; }
        public int ZaposlenikId { get; set; }
        public int? PosaoId { get; set; }

        public virtual Posao Posao { get; set; }
    }
}
