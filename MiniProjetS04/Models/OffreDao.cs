using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.WebPages;

namespace MiniProjetS04.Models
{
    public class OffreDao : DAO<Offre>
    {
        Connexion cnx = new Connexion();
        public Offre findById(int id)
        {
            if(id != 0)
            {
                using (SqlConnection con = cnx.GetConnection())
                {
                    con.Open();
                    string query = "SELECT * FROM Offres WHERE Id = @Id";
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@Id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
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
                            categorie = reader["categorie"].ToString()
                        };
                        return offre;
                    }
                }
            }

            return new Offre();
        }

        public List<Offre> findAll()
        {
            using (SqlConnection con = cnx.GetConnection())
            {
                con.Open();
                string query = "select " +
                    "o.Id, o.id_utilisateur, " +
                    "o.titre_poste, o.entreprise, o.descriptions, o.missions, o.profile_rechercher, " +
                    "o.competences_rechercher, o.conditions_travail, o.avantages, o.statut, " +
                    "o.tp, o.categorie, o.lieu_travail, o.date_ajout,u.nom, u.prenom, " +
                    "( SELECT COUNT(*) FROM condidatures c " +
                    "WHERE c.id_offres = o.Id ) AS nb_candidatures " +
                    "from offres as o " +
                    "join utilisateur as u " +
                    "on o.id_utilisateur = u.id;";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                List<Offre> offres = new List<Offre>();
                while (reader.Read())
                {
                    Utilisateur utilisateur = new Utilisateur
                    {
                        Id = (int)reader["id_utilisateur"],
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
                        categorie = reader["categorie"].ToString(),
                        nb_condidatures = (int)reader["nb_candidatures"],
                        utilisateur = utilisateur
                    };
                    offres.Add(offre);
                }
                if(offres.Count > 0)
                {
                    return offres;
                }

                return new List<Offre>();
            }
        }
        public bool insert(Offre obj)
        {
            if (!obj.titre_poste.Equals(null) && !obj.titre_poste.IsEmpty())
            {
                using (SqlConnection con = cnx.GetConnection())
                {
                    con.Open();
                    string query = "INSERT INTO Offres (id_utilisateur, titre_poste, entreprise, descriptions, missions, profile_rechercher, " +
                        "competences_rechercher, conditions_travail, lieu_travail, avantages, statut, tp, categorie) " +
                        "VALUES (@id_utilisateur, @titre_poste, @entreprise, @description, @mission, @profil_rechercher, @competences_rechercher, " +
                        "@conditions_travail, @lieu_travail, @avantages, @statut, @type, @categorie)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id_utilisateur", obj.id_utilisateur);
                    cmd.Parameters.AddWithValue("@titre_poste", obj.titre_poste ?? "");
                    cmd.Parameters.AddWithValue("@entreprise", obj.entreprise ?? "");
                    cmd.Parameters.AddWithValue("@description", obj.description ?? "");
                    cmd.Parameters.AddWithValue("@mission", obj.mission ?? "");
                    cmd.Parameters.AddWithValue("@profil_rechercher", obj.profil_rechercher ?? "");
                    cmd.Parameters.AddWithValue("@competences_rechercher", obj.competences_rechercher ?? "");
                    cmd.Parameters.AddWithValue("@conditions_travail", obj.conditions_travail ?? "");
                    cmd.Parameters.AddWithValue("@lieu_travail", obj.lieu_travail ?? "");
                    cmd.Parameters.AddWithValue("@avantages", obj.avantages ?? "");
                    cmd.Parameters.AddWithValue("@statut", obj.statut ?? "active");
                    cmd.Parameters.AddWithValue("@type", obj.type ?? "");
                    cmd.Parameters.AddWithValue("@categorie", obj.categorie ?? "");
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }

            return false;
        }

        public bool update(Offre obj)
        {
            if(obj.Id != 0)
            {
                using (SqlConnection con = cnx.GetConnection())
                {
                    con.Open();
                    string query = "UPDATE Offres SET id_utilisateur = @id_utilisateur, titre_poste = @titre_poste, entreprise = @entreprise, " +
                        "descriptions = @description, missions = @mission, profile_rechercher = @profil_rechercher, competences_rechercher = @competences_rechercher, " +
                        "conditions_travail = @conditions_travail, lieu_travail = @lieu_travail, avantages = @avantages, statut = @statut, tp = @type, categorie = @categorie " +
                        "WHERE Id = @Id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id_utilisateur", obj.id_utilisateur);
                    cmd.Parameters.AddWithValue("@titre_poste", obj.titre_poste);
                    cmd.Parameters.AddWithValue("@entreprise", obj.entreprise);
                    cmd.Parameters.AddWithValue("@description", obj.description);
                    cmd.Parameters.AddWithValue("@mission", obj.mission);
                    cmd.Parameters.AddWithValue("@profil_rechercher", obj.profil_rechercher);
                    cmd.Parameters.AddWithValue("@competences_rechercher", obj.competences_rechercher);
                    cmd.Parameters.AddWithValue("@conditions_travail", obj.conditions_travail);
                    cmd.Parameters.AddWithValue("@lieu_travail", obj.lieu_travail);
                    cmd.Parameters.AddWithValue("@avantages", obj.avantages);
                    cmd.Parameters.AddWithValue("@statut", obj.statut);
                    cmd.Parameters.AddWithValue("@type", obj.type);
                    cmd.Parameters.AddWithValue("@categorie", obj.categorie);
                    cmd.Parameters.AddWithValue("@Id", obj.Id);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }

            return false;
        }
        public bool delete(Offre obj)
        {
            if(obj.Id != 0)
            {
                using (SqlConnection con = cnx.GetConnection())
                {
                    con.Open();

                    // Supprimer d'abord les candidatures lier a l'offre

                    SqlCommand cmd1 = new SqlCommand(
                        "DELETE FROM condidatures WHERE id_offres = @id", con);
                    cmd1.Parameters.AddWithValue("@id", obj.Id);
                    cmd1.ExecuteNonQuery();

                    // Ensuite supprimer l'offre

                    string query = "DELETE FROM Offres WHERE Id = @Id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Id", obj.Id);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            return false;
        }

        public List<Offre> findByUserId(int userId)
        {
            if(userId != 0)
            {
                using (SqlConnection con = cnx.GetConnection())
                {
                    con.Open();
                    string query = "select " +
                        "o.Id, o.id_utilisateur, " +
                        "o.titre_poste, o.entreprise, o.descriptions, o.missions, o.profile_rechercher, " +
                        "o.competences_rechercher, o.conditions_travail, o.avantages, o.statut, " +
                        "o.tp, o.categorie, o.lieu_travail, o.date_ajout, u.nom, u.prenom, " +
                        "( SELECT COUNT(*) FROM condidatures c " +
                        "WHERE c.id_offres = o.Id ) AS nb_candidatures " +
                        "from offres as o " +
                        "join utilisateur as u " +
                        "on o.id_utilisateur = u.id " +
                        "WHERE id_utilisateur = @id_utilisateur;";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id_utilisateur", userId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Offre> offres = new List<Offre>();
                    while (reader.Read())
                    {
                        Utilisateur utilisateur = new Utilisateur
                        {
                            Id = (int)reader["id_utilisateur"],
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
                            categorie = reader["categorie"].ToString(),
                            nb_condidatures = (int)reader["nb_candidatures"],
                            utilisateur = utilisateur
                        };
                        offres.Add(offre);
                    }
                    return offres;
                }
            }
            return new List<Offre>();
        }

        public bool updateStatutOffre(Offre o)
        {
            if(o.Id != 0 && !o.statut.Equals(null))
            {
                using (SqlConnection con = cnx.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("UPDATE offres SET statut = @stat WHERE id = @id", con);
                    cmd.Parameters.AddWithValue("@stat", o.statut);
                    cmd.Parameters.AddWithValue("@id", o.Id);
                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result != 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public int countOffre()
        {
            using (SqlConnection con = cnx.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM offres", con);
                con.Open();
                int count = (int)cmd.ExecuteScalar();

                if (count > 0)
                {
                    return count;
                }
            }

            return 0;
        }
    }
}