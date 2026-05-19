using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MiniProjetS04.Models
{
    public class CandidatDao
    {
        public List<Candidat> listCandidats(int id)
        {
            List<Candidat> liste = new List<Candidat>();
            Connexion cnx = new Connexion();

            string query = "SELECT " +
    " u.nom, u.prenom, u.email, u.tel," +
    " c.id AS id_candidature, c.objet, c.date_publication," +
    " c.statut AS statut_candidature," +
    " o.titre_poste, o.entreprise," +
    // On récupère les infos du CV via des sous-requêtes pour éviter de multiplier les lignes
    " (SELECT TOP 1 titre_prof FROM cv WHERE id_utilisateur = u.id) AS titre_prof," +
    " (SELECT TOP 1 formations FROM cv WHERE id_utilisateur = u.id) AS formations," +
    " (SELECT TOP 1 experiences FROM cv WHERE id_utilisateur = u.id) AS experiences," +
    " (SELECT TOP 1 competences FROM cv WHERE id_utilisateur = u.id) AS competences," +
    " (SELECT TOP 1 projets FROM cv WHERE id_utilisateur = u.id) AS projets," +
    " (SELECT TOP 1 certifications FROM cv WHERE id_utilisateur = u.id) AS certifications" +
    " FROM condidatures c " +
    " INNER JOIN utilisateur u ON c.id_utilisateur = u.id" +
    " INNER JOIN offres o ON c.id_offres = o.id" +
    " WHERE o.id_utilisateur = @id_recruteur;";

            if(id != 0)
            {
                using (SqlConnection con = cnx.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id_recruteur", id);
                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Candidat c = new Candidat();
                        c.candidature = new Candidature();
                        c.offre = new Offre();
                        c.cv = new Cv();

                        c.Nom = reader["nom"].ToString();
                        c.Prenom = reader["prenom"].ToString();
                        c.Email = reader["email"].ToString();
                        c.Tel = reader["tel"].ToString();
                        c.candidature.objet = reader["objet"].ToString();

                        c.candidature.dt_publication = Convert.ToDateTime(reader["date_publication"]);
                        c.candidature.statut = reader["statut_candidature"].ToString();
                        c.candidature.id = (int)reader["id_candidature"];

                        c.cv.Titre_prof = reader["titre_prof"]?.ToString();
                        c.cv.formations = reader["formations"]?.ToString();
                        c.cv.experience = reader["experiences"]?.ToString();
                        c.cv.competences = reader["competences"]?.ToString();
                        c.cv.projets = reader["projets"]?.ToString();
                        c.cv.certifications = reader["certifications"]?.ToString();

                        c.offre.titre_poste = reader["titre_poste"].ToString();
                        c.offre.entreprise = reader["entreprise"].ToString();

                        liste.Add(c);
                    }
                    return liste;
                }
            }

            return new List<Candidat>();
        } 
    }
}