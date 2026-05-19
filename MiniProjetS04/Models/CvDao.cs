using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MiniProjetS04.Models
{
    public class CvDao : DAO<Cv>
    {
        Connexion cnx = new Connexion();

        public Cv findById(int id)
        {
            if(id != 0)
            {
                using (SqlConnection con = cnx.GetConnection())
                {
                    con.Open();
                    string query = "SELECT * FROM cv WHERE id = @id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Cv cv = new Cv
                                {
                                    Id = (int)reader["id"],
                                    Id_utilisateur = (int)reader["id_utilisateur"],
                                    Titre_prof = reader["Titre_prof"].ToString(),
                                    experience = reader["experiences"].ToString(),
                                    formations = reader["formations"].ToString(),
                                    competences = reader["competences"].ToString(),
                                    projets = reader["projets"].ToString(),
                                    certifications = reader["certifications"].ToString(),
                                    dt_creation = reader["dt_creation"].ToString()
                                };
                                return cv;
                            }
                        }
                    }
                }
            }
            return new Cv();
        }
        public List<Cv> findAll()
        {
            using(SqlConnection con = cnx.GetConnection())
            {
                con.Open();
                string query = "SELECT * FROM cv";
                using(SqlCommand cmd = new SqlCommand(query, con))
                {
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Cv> cvs = new List<Cv>();
                        while(reader.Read())
                        {
                            Cv cv = new Cv
                            {
                                Id = (int)reader["id"],
                                Id_utilisateur = (int)reader["id_utilisateur"],
                                Titre_prof = reader["Titre_prof"].ToString(),
                                experience = reader["experiences"].ToString(),
                                formations = reader["formations"].ToString(),
                                competences = reader["competences"].ToString(),
                                projets = reader["projets"].ToString(),
                                certifications = reader["certifications"].ToString(),
                                dt_creation = reader["dt_creation"].ToString()
                            };
                            cvs.Add(cv);
                        }
                        if(cvs.Count != 0)
                        {
                            return cvs;
                        }

                        return new List<Cv>();
                    }
                }
            }
            
        }

        public bool insert(Cv obj)
        {
            if(obj.Id_utilisateur != 0)
            {
                using (SqlConnection con = cnx.GetConnection())
                {
                    con.Open();
                    string query = "INSERT INTO cv (id_utilisateur, Titre_prof, experiences, formations, competences, projets, certifications) " +
                        "VALUES (@id_utilisateur, @Titre_prof, @experience, @formations, @competences, @projets, @certifications)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id_utilisateur", obj.Id_utilisateur);
                        cmd.Parameters.AddWithValue("@Titre_prof", obj.Titre_prof ?? "");
                        cmd.Parameters.AddWithValue("@experience", obj.experience ?? "");
                        cmd.Parameters.AddWithValue("@formations", obj.formations ?? "");
                        cmd.Parameters.AddWithValue("@competences", obj.competences ?? "");
                        cmd.Parameters.AddWithValue("@projets", obj.projets ?? "");
                        cmd.Parameters.AddWithValue("@certifications", obj.certifications ?? "");
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            return false;
        }

        public bool update(Cv obj)
        {
            if(obj.Id != 0 && obj.Id_utilisateur != 0)
            {
                using (SqlConnection con = cnx.GetConnection())
                {
                    con.Open();
                    string query = "UPDATE cv SET id_utilisateur = @id_utilisateur, Titre_prof = @Titre_prof, experiences = @experience, " +
                        "formations = @formations, competences = @competences, projets = @projets, certifications = @certifications " +
                        "WHERE id = @id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", obj.Id);
                        cmd.Parameters.AddWithValue("@id_utilisateur", obj.Id_utilisateur);
                        cmd.Parameters.AddWithValue("@Titre_prof", obj.Titre_prof ?? "");
                        cmd.Parameters.AddWithValue("@experience", obj.experience ?? "");
                        cmd.Parameters.AddWithValue("@formations", obj.formations ?? "");
                        cmd.Parameters.AddWithValue("@competences", obj.competences ?? "");
                        cmd.Parameters.AddWithValue("@projets", obj.projets ?? "");
                        cmd.Parameters.AddWithValue("@certifications", obj.certifications ?? "");
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }

            return false;
        }
        public bool delete(Cv obj)
        {
            if(obj.Id != 0)
            {
                using (SqlConnection con = cnx.GetConnection())
                {
                    con.Open();
                    string query = "DELETE FROM cv WHERE id = @id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", obj.Id);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }

            return false;
        }

        public Cv findByUserId(int userId)
        {
            if(userId != 0)
            {
                using (SqlConnection con = cnx.GetConnection())
                {
                    con.Open();
                    string query = "SELECT * FROM cv WHERE id_utilisateur = @userId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Cv cv = new Cv
                                {
                                    Id = (int)reader["id"],
                                    Id_utilisateur = (int)reader["id_utilisateur"],
                                    Titre_prof = reader["Titre_prof"].ToString(),
                                    experience = reader["experiences"].ToString(),
                                    formations = reader["formations"].ToString(),
                                    competences = reader["competences"].ToString(),
                                    projets = reader["projets"].ToString(),
                                    certifications = reader["certifications"].ToString(),
                                    dt_creation = reader["dt_creation"].ToString()
                                };
                                return cv;
                            }
                        }
                    }
                }
            }
            return new Cv();
        }


    }
}