using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grupa1Ozo.Models
{
    public class UslugaKategorijaUslugeViewModel
    {
        public List<Usluga> Usluga { get; set; }
        public SelectList KategorijeUsluga { get; set; }
        public string KategorijaUsluge { get; set; }
        public string SearchString { get; set; }
    }
}
