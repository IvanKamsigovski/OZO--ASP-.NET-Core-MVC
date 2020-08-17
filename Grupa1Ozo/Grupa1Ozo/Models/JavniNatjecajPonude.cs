using System;
using System.Collections.Generic;

namespace Grupa1Ozo.Models
{
    public partial class JavniNatjecajPonude
    {
        public int JavniNatjecajPonudeId { get; set; }
        public string Firma { get; set; }
        public decimal Cijena { get; set; }
        public int? JavniNatjecajId { get; set; }

        public virtual JavniNatjecaj JavniNatjecaj { get; set; }
    }
}
