using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MiniProjetS04.Models
{
    public class CandidatureDao : DAO<Candidature>
    {
        Connexion cnx = new Connexion();
        public Candidature findById(int id)
        {
            if(id != 0)
            {
                using (SqlConnection con = cnx.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM condidatures WHERE id = @id", con);
                    cmd.Parameters.AddWithValue("@id", id);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        Candidature candidature = new Candidature
                        {
                            id = (int)reader["id"],
                            id_utilisateur = (int)reader["id_utilisateur"],
                            id_offre = (int)reader["id_offre"],
                            objet = reader["objet"].ToString(),
                            dt_publication = (DateTime)reader["dt_publication"],
                            statut = reader["statut"].ToString()
                        };
                        return candidature;
                    }
                    return new Candidature();
                }
            }
            return new Candidature();
        }
        public List<Candidature> findAll()
        {
            using(SqlConnection con = cnx.GetConnection())
            {
                List<Candidature> candidatures = new List<Candidature>();
                SqlCommand cmd = new SqlCommand("SELECT * FROM condidatures", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Candidature candidature = new Candidature
                    {
                        id = (int)reader["id"],
                        id_utilisateur = (int)reader["id_utilisateur"],
                        id_offre = (int)reader["id_offre"],
                        objet = reader["objet"].ToString(),
                        dt_publication = (DateTime)reader["dt_publication"],
                        statut = reader["statut"].ToString()
                    };
                    candidatures.Add(candidature);
                }
                if(candidatures.Count > 0)
                {
                    return candidatures;
                }

                return new List<Candidature>();
            }
        }

        public bool insert(Candidature obj)
        {
            if(obj.id_utilisateur != 0 &&  obj.id_offre != 0)
            {
                using (SqlConnection con = cnx.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO condidatures (id_utilisateur, id_offres, objet, statut) " +
                        "VALUES (@id_utilisateur, @id_offre, @objet, @statut)", con);
                    cmd.Parameters.AddWithValue("@id_utilisateur", obj.id_utilisateur);
                    cmd.Parameters.AddWithValue("@id_offre", obj.id_offre);
                    cmd.Parameters.AddWithValue("@objet", obj.objet);
                    cmd.Parameters.AddWithValue("@statut", obj.statut);
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            return false;
        }

        public bool update(Candidature obj)
        {
            if(obj.id != 0)
            {
                using (SqlConnection con = cnx.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("UPDATE condidatures SET id_utilisateur = @id_utilisateur, id_offres = @id_offre, " +
                        "objet = @objet, dt_publication = @dt_publication, statut = @statut WHERE id = @id", con);
                    cmd.Parameters.AddWithValue("@id_utilisateur", obj.id_utilisateur);
                    cmd.Parameters.AddWithValue("@id_offre", obj.id_offre);
                    cmd.Parameters.AddWithValue("@objet", obj.objet);
                    cmd.Parameters.AddWithValue("@dt_publication", obj.dt_publication);
                    cmd.Parameters.AddWithValue("@statut", obj.statut);
                    cmd.Parameters.AddWithValue("@id", obj.id);
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            return false;
        }
        public bool delete(Candidature obj)
        {
            if(obj.id != 0)
            {
                using (SqlConnection con = cnx.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM condidatures WHERE id = @id", con);
                    cmd.Parameters.AddWithValue("@id", obj.id);
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            return false;
        }

        public List<Candidature> findByUserId(int userId)
        {
            if(userId != 0)
            {
                using (SqlConnection con = cnx.GetConnection())
                {
                    con.Open();
                    string query = "SELECT " +
                        "c.id,o.titre_poste,o.entreprise,c.objet,c.date_publication," +
                        "c.statut " +
                        "FROM condidatures c " +
                        "INNER JOIN offres o " +
                        "ON c.id_offres = o.id " +
                        "WHERE c.id_utilisateur = @id_utilisateur";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id_utilisateur", userId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Candidature> candidatures = new List<Candidature>();
                    while (reader.Read())
                    {
                        Candidature can = new Candidature
                        {
                            id = (int)reader["id"],
                            titre_poste = reader["titre_poste"].ToString(),
                            entreprise = reader["entreprise"].ToString(),
                            objet = reader["objet"].ToString(),
                            dt_publication = (DateTime)reader["date_publication"],
                            statut = reader["statut"].ToString()
                        };
                        candidatures.Add(can);
                    }
                    return candidatures;
                }
            }
            return new List<Candidature>();
        }

        public int countCandidature()
        {
            using(SqlConnection con = cnx.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM condidatures", con);
                con.Open();
                int count = (int) cmd.ExecuteScalar();

                if(count > 0)
                {
                    return count;
                }
            }

            return 0;
        }

        public bool updateStatut(Candidature obj)
        {
            if(obj.id != 0 && !obj.statut.Equals(null))
            {
                using (SqlConnection con = cnx.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("UPDATE condidatures SET statut = @statut WHERE id = @id", con);
                    cmd.Parameters.AddWithValue("@statut", obj.statut);
                    cmd.Parameters.AddWithValue("@id", obj.id);
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            return false;
        }

        public bool exist(Candidature obj)
        {
            if(obj.id_utilisateur != 0 && obj.id_offre != 0)
            {
                using (SqlConnection con = cnx.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM condidatures WHERE id_utilisateur = @uid AND id_offres = @oid", con);
                    cmd.Parameters.AddWithValue("@uid", obj.id_utilisateur);
                    cmd.Parameters.AddWithValue("@oid", obj.id_offre);

                    con.Open();

                    int rowsAffected = (int)cmd.ExecuteScalar();
                    return rowsAffected > 0;
                }
            }
            return false;
        }
    }
}