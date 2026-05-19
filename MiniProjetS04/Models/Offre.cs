using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;

namespace MiniProjetS04.Models
{
    public class Offre
    {
        public int Id { get; set; }
        public int id_utilisateur { get; set; }
        public string titre_poste { get; set; }
        public string entreprise { get; set; }
        public string description { get; set; }
        public string mission { get; set; }
        public string profil_rechercher { get; set; }
        public string competences_rechercher { get; set; }
        public string conditions_travail { get; set; }
        public string lieu_travail { get; set; }
        public string avantages { get; set; }
        public string statut { get; set; }
        public string type { get; set; }
        public string categorie { get; set; }
        public Utilisateur utilisateur { get; set; }
        public DateTime date_ajout {  get; set; }
        public int nb_condidatures { get; set; }
    }
}