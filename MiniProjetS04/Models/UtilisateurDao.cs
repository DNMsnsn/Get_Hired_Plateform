using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MiniProjetS04.Models
{
    public class UtilisateurDao : DAO<Utilisateur>
    {

        Connexion cnx = new Connexion();

        public Utilisateur findById(int id)
        {

            if(id != 0)
            {
                using (SqlConnection con = cnx.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Utilisateur WHERE Id = @id", con);
                    cmd.Parameters.AddWithValue("@id", id);
                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Utilisateur utilisateur = new Utilisateur
                        {
                            Id = (int)reader["Id"],
                            Nom = reader["Nom"].ToString(),
                            Prenom = reader["Prenom"].ToString(),
                            Dt_naiss = reader["Dt_naiss"].ToString(),
                            Tel = reader["Tel"].ToString(),
                            Email = reader["Email"].ToString(),
                            Psw = reader["Psw"].ToString(),
                            Dt_inscrit = reader["Dt_inscrit"].ToString(),
                            Statut = reader["Statut"].ToString(),
                            Adresse = reader["Adresse"].ToString(),
                            Linkedin = reader["lien_linkedin"].ToString(),
                            Github = reader["lien_github"].ToString(),
                            Meta = reader["lien_meta"].ToString()
                        };
                        return utilisateur;
                    }
                }
            }

            return new Utilisateur();
        }

        public List<Utilisateur> findAll()
        {

            List<Utilisateur> utilisateurs = new List<Utilisateur>();

            using (SqlConnection con = cnx.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Utilisateur", con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Utilisateur utilisateur = new Utilisateur
                    {
                        Id = (int)reader["Id"],
                        Nom = reader["Nom"].ToString(),
                        Prenom = reader["Prenom"].ToString(),
                        Dt_naiss = reader["Dt_naiss"].ToString(),
                        Tel = reader["Tel"].ToString(),
                        Email = reader["Email"].ToString(),
                        Psw = reader["Psw"].ToString(),
                        Dt_inscrit = reader["Dt_inscrit"].ToString(),
                        Statut = reader["Statut"].ToString(),
                        Adresse = reader["Adresse"].ToString(),
                        Linkedin = reader["lien_linkedin"].ToString(),
                        Github = reader["lien_github"].ToString(),
                        Meta = reader["lien_meta"].ToString()
                    };
                    utilisateurs.Add(utilisateur);
                }
            }

            if(utilisateurs != null)
            {
                return utilisateurs;
            } 
            return new List<Utilisateur>();
        }

        public bool insert(Utilisateur obj)
        {
            using (SqlConnection con = cnx.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Utilisateur (Nom, Prenom, Dt_naiss, Tel, Email, Psw, " +
                    "Adresse, lien_linkedin, lien_github, lien_meta) VALUES (@Nom, @Prenom, @Dt_naiss, @Tel, @Email, @Psw," +
                    "@Adresse, @Linkedin, @Github, @Meta)", con);
                cmd.Parameters.AddWithValue("@Nom", obj.Nom ?? "nom" + obj.Id);
                cmd.Parameters.AddWithValue("@Prenom", obj.Prenom ?? "prenom" + obj.Id);
                cmd.Parameters.AddWithValue("@Dt_naiss", obj.Dt_naiss ?? "");
                cmd.Parameters.AddWithValue("@Tel", obj.Tel ?? "");
                cmd.Parameters.AddWithValue("@Email", obj.Email ?? "");
                cmd.Parameters.AddWithValue("@Psw", obj.Psw ?? "admin");
                cmd.Parameters.AddWithValue("@Adresse", obj.Adresse ?? "");
                cmd.Parameters.AddWithValue("@Linkedin", obj.Linkedin ?? "");
                cmd.Parameters.AddWithValue("@Github", obj.Github ?? "");
                cmd.Parameters.AddWithValue("@Meta", obj.Meta ?? "");

                con.Open();

                int result = cmd.ExecuteNonQuery();

                return result > 0;
            }
        }

        public bool update(Utilisateur obj)
        {
            using (SqlConnection con = cnx.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("UPDATE Utilisateur SET Nom = @Nom, Prenom = @Prenom, " +
                    "Tel = @Tel, Email = @Email, Psw = @Psw, Adresse = @Adresse, lien_linkedin = @Linkedin, lien_github = @Github, lien_meta = @Meta " +
                    "WHERE Id = @Id", con);
                cmd.Parameters.AddWithValue("@Id", obj.Id);
                cmd.Parameters.AddWithValue("@Nom", obj.Nom ?? "nom" + obj.Id);
                cmd.Parameters.AddWithValue("@Prenom", obj.Prenom ?? "prenom" + obj.Id);
                cmd.Parameters.AddWithValue("@Tel", obj.Tel ?? "");
                cmd.Parameters.AddWithValue("@Email", obj.Email ?? "");
                cmd.Parameters.AddWithValue("@Psw", obj.Psw ?? "admin");
                cmd.Parameters.AddWithValue("@Adresse", obj.Adresse ?? "");
                cmd.Parameters.AddWithValue("@Linkedin", obj.Linkedin ?? "");
                cmd.Parameters.AddWithValue("@Github", obj.Github ?? "");
                cmd.Parameters.AddWithValue("@Meta", obj.Meta ?? "");
                con.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }
        public bool delete(Utilisateur obj)
        {
            if(obj.Id != 0)
            {
                using (SqlConnection con = cnx.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Utilisateur WHERE Id = @Id", con);
                    cmd.Parameters.AddWithValue("@Id", obj.Id);
                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }

            return false;
        }


        public int Authentification(string email, string password)
        {
            if(!email.Equals(null) && !password.Equals(null))
            {
                using (SqlConnection con = cnx.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("SELECT id FROM Utilisateur WHERE Email = @Email AND Psw = @Psw", con);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Psw", password);
                    con.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        return (int)result;
                    }
                    return -1;
                }
            }

            return -1;
        }

        public bool updateStatutUtilisateur(Utilisateur u)
        {
            if(u.Id != 0 && !u.Statut.Equals(null))
            {
                using (SqlConnection con = cnx.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("UPDATE utilisateur SET statut = @stat WHERE id = @id", con);
                    cmd.Parameters.AddWithValue("@stat", u.Statut);
                    cmd.Parameters.AddWithValue("@id", u.Id);
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

        public int countUtilisateur()
        {
            using (SqlConnection con = cnx.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM utilisateur", con);
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