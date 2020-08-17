using System;
using System.Collections.Generic;

namespace Grupa1Ozo.Models
{
    public partial class UslugaNarudzba
    {
        public int UslugaNarudzbaId { get; set; }
        public int UslugaId { get; set; }
        public int NarudzbaId { get; set; }

        public virtual Narudzba Narudzba { get; set; }
        public virtual Usluga Usluga { get; set; }
    }
}
