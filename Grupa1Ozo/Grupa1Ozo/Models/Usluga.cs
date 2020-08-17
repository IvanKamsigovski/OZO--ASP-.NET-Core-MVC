using System;
using System.Collections.Generic;

namespace Grupa1Ozo.Models
{
    public partial class Usluga
    {
        public Usluga()
        {
            Kupac = new HashSet<Kupac>();
            Oprema = new HashSet<Oprema>();
            UslugaNarudzba = new HashSet<UslugaNarudzba>();
            Zaposlenici = new HashSet<Zaposlenici>();
        }

        public int UslugaId { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public int Cijena { get; set; }
        public int KategorijaUslugeId { get; set; }
        public int? NatjecajId { get; set; }

        public virtual KategorijaUsluge KategorijaUsluge { get; set; }
        public virtual Natjecaj Natjecaj { get; set; }
        public virtual ICollection<Kupac> Kupac { get; set; }
        public virtual ICollection<Oprema> Oprema { get; set; }
        public virtual ICollection<UslugaNarudzba> UslugaNarudzba { get; set; }
        public virtual ICollection<Zaposlenici> Zaposlenici { get; set; }
    }
}
