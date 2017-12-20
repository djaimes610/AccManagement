using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentApp.Models
{
    public class CarpetaViewModel
    {
        public int id { get; set; }
        public string text { get; set; }
        public List<CarpetaViewModel> nodes { get; set; }
    }
}