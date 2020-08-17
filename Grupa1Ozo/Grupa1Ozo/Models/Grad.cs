using System;
using System.Collections.Generic;

namespace Grupa1Ozo.Models
{
    public partial class Grad
    {
        public int GradId { get; set; }
        public string Naziv { get; set; }
        public int? PostanskiBroj { get; set; }
        public int? OpcinaId { get; set; }

        public virtual Opcina Opcina { get; set; }
    }
}
