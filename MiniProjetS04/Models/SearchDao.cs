using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.WebPages;

namespace MiniProjetS04.Models
{
    public class SearchDao
    {
        Connexion cnx = new Connexion();
        public List<Offre> search(Search s)
        {
            List<Offre> list = new List<Offre>();

            string query = @"
                            SELECT 
                                o.Id, o.id_utilisateur, o.titre_poste, o.entreprise, o.descriptions, 
                                o.missions, o.profile_rechercher, o.competences_rechercher, 
                                o.conditions_travail, o.avantages, o.statut, o.tp, o.categorie, 
                                o.lieu_travail, o.date_ajout, u.nom, u.prenom,
                                (SELECT COUNT(*) FROM condidatures c WHERE c.id_offres = o.Id) as nb_candidatures
                            FROM offres o
                            JOIN utilisateur u ON o.id_utilisateur = u.id
                            WHERE o.statut = 'active'
                            AND (
                                o.titre_poste LIKE @search 
                                OR o.entreprise LIKE @search 
                                OR o.lieu_travail LIKE @search 
                                OR o.categorie LIKE @search
                            )";

            if(s != null && !s.sentence.IsEmpty())
            {
                using (SqlConnection con = cnx.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@search", "%" + s.sentence + "%");
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Utilisateur utilisateur = new Utilisateur
                        {
                            Nom = reader["nom"].ToString(),
                            Prenom = reader["prenom"].ToString()
                        };
                        Offre offre = new Offre
                        {
                            Id = (int)reader["Id"],
                            id_utilisateur = (int)reader["id_utilisateur"],
                            titre_poste = reader["titre_poste"].ToString(),
                            entreprise = reader["entreprise"].ToString(),
                            description = reader["descriptions"].ToString(),
                            mission = reader["missions"].ToString(),
                            profil_rechercher = reader["profile_rechercher"].ToString(),
                            competences_rechercher = reader["competences_rechercher"].ToString(),
                            conditions_travail = reader["conditions_travail"].ToString(),
                            lieu_travail = reader["lieu_travail"].ToString(),
                            avantages = reader["avantages"].ToString(),
                            statut = reader["statut"].ToString(),
                            type = reader["tp"].ToString(),
                            date_ajout = (DateTime)reader["date_ajout"],
                            utilisateur = utilisateur,
                            categorie = reader["categorie"].ToString(),
                            nb_condidatures = (int)reader["nb_candidatures"]
                        };
                        list.Add(offre);
                    }
                    return list;
                }
            }

            return new List<Offre>();
        }
    }
}