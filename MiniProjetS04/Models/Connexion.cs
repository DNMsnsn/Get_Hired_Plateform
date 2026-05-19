using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MiniProjetS04.Models
{
    public class Connexion
    {
        public SqlConnection GetConnection()
        {
            return new SqlConnection("Data Source=DESKTOP-E3IJGOU\\SQLEXPRESS;Initial Catalog=GetHired;Integrated Security=True;");
        }
    }
}