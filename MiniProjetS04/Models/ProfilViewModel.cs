using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniProjetS04.Models
{
    public class ProfilViewModel
    {
        public Utilisateur utilisateur { get; set; }
        public Cv cv { get; set; }
        public List<Offre> offres { get; set; } = new List<Offre>();
        public List<Candidature> candidatures { get; set; } = new List<Candidature>();
        public List<Candidat> candidats { get; set; } = new List<Candidat>();
    }
}