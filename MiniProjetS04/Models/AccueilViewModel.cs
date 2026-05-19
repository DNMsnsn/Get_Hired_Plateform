using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniProjetS04.Models
{
    public class AccueilViewModel
    {
        public List<Offre> Offres { get; set; } = new List<Offre>();
        public List<Utilisateur> Utilisateurs { get; set; } = new List<Utilisateur>();
        public int nbUtilisateurs { get; set; }
        public int nbOffres { get; set; }
        public int nbCandidatures { get; set; }
    }
}