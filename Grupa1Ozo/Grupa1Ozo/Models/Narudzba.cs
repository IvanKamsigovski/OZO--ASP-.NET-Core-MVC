using System;
using System.Collections.Generic;

namespace Grupa1Ozo.Models
{
    public partial class Narudzba
    {
        public Narudzba()
        {
            UslugaNarudzba = new HashSet<UslugaNarudzba>();
        }

        public int NarudzbaId { get; set; }
        public DateTime DatumNarudzbe { get; set; }
        public string StatusNarudzbe { get; set; }

        public virtual ICollection<UslugaNarudzba> UslugaNarudzba { get; set; }
    }
}
