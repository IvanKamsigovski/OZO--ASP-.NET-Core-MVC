using System;
using System.Collections.Generic;

namespace Grupa1Ozo.Models
{
    public partial class JavniNatjecaj
    {
        public JavniNatjecaj()
        {
            JavniNatjecajPonude = new HashSet<JavniNatjecajPonude>();
            Natjecaj = new HashSet<Natjecaj>();
        }

        public int JavniNatjecajId { get; set; }
        public string Dobitnik { get; set; }

        public virtual ICollection<JavniNatjecajPonude> JavniNatjecajPonude { get; set; }
        public virtual ICollection<Natjecaj> Natjecaj { get; set; }
    }
}
