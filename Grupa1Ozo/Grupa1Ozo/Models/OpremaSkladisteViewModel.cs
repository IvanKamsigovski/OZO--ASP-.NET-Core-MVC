using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grupa1Ozo.Models
{
    public class OpremaSkladisteViewModel
    {
        public List<Oprema> Oprema { get; set; }
        public SelectList Skladista { get; set; }
        public SelectList Usluge { get; set; }
        public string Skladiste { get; set; }
        public string Usluga { get; set; }
        public string SearchString { get; set; }
    }
}
