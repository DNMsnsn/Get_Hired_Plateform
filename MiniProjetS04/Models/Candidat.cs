using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniProjetS04.Models
{
    public class Candidat : Utilisateur
    {
        public Cv cv {  get; set; }
        public Candidature candidature { get; set; }
        public Offre offre { get; set; }
    }
}