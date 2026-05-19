using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniProjetS04.Models
{
    public class Utilisateur
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Dt_naiss { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string Psw { get; set; }
        public string Dt_inscrit { get; set; }
        public string Statut { get; set; }
        public string Adresse { get; set; }
        public string Linkedin { get; set; }
        public string Github { get; set; }
        public string Meta { get; set; }

    }
}