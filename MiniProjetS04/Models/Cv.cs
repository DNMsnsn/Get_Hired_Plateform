using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniProjetS04.Models
{
    public class Cv
    {
        public int Id { get; set; }
        public int Id_utilisateur { get; set; }
        public string Titre_prof { get; set; }
        public string experience { get; set; }
        public string formations { get; set; }
        public string competences { get; set; }
        public string projets { get; set; }
        public string certifications { get; set; }
        public string dt_creation { get; set; }
    }
}