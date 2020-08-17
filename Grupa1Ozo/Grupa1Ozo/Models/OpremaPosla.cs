using System;
using System.Collections.Generic;

namespace Grupa1Ozo.Models
{
    public partial class OpremaPosla
    {
        public int OpremaPoslaId { get; set; }
        public int OpremaId { get; set; }
        public int? PosaoId { get; set; }

        public virtual Posao Posao { get; set; }
    }
}
