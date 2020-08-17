using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grupa1Ozo.Models
{
    public class ZaposleniciOpcinaViewModel
    {
        public List<Zaposlenici> Zaposlenici { get; set; }
        //public SelectList Opcine { get; set; }
        public SelectList Struke { get; set; }
        public SelectList Certifikati { get; set; }
        public string Opcina { get; set; }
        public string Struka { get; set; }
        public string Certifikat { get; set; }
        public string SearchString { get; set; }
    }
}