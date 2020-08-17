using System;
using System.Collections.Generic;

namespace Grupa1Ozo.Models
{
    public partial class Opcina
    {
        public Opcina()
        {
            Grad = new HashSet<Grad>();
            Zaposlenici = new HashSet<Zaposlenici>();
        }

        public int OpcinaId { get; set; }
        public string Naziv { get; set; }

        public virtual ICollection<Grad> Grad { get; set; }
        public virtual ICollection<Zaposlenici> Zaposlenici { get; set; }
    }
}
