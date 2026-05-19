using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniProjetS04.Models
{
    public class Candidature
    {
        public int id { get; set; }
        public int id_utilisateur { get; set; }
        public int id_offre { get; set; }
        public string objet { get; set; }
        public DateTime dt_publication { get; set; }
        public string statut { get; set; }
        public string titre_poste {  get; set; }
        public string entreprise { get; set; }
    }
}