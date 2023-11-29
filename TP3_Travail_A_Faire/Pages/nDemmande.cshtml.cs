using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography.X509Certificates;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace TP3_Travail_A_Faire.Pages
{
    public class nDemmandeModel : PageModel
    {

        public int idDemande;
        public double Longueur;
        public double Largeur;
        public string TypeC = "";

        public string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=CouvrePlanche1;Integrated Security=True";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            Longueur = Convert.ToDouble(Request.Form["longueur"]);
            Largeur = Convert.ToDouble(Request.Form["largeur"]);
            TypeC = Request.Form["typeC"];
            if (Longueur != 0 && Largeur != 0)
            {
                try
                {
                    using(SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        // selection id du couvre plancer
                        string sqlP = "select * from CouvrePlancher where nomCouvrePlancher = '" + TypeC + "'";
                        using(SqlCommand cmd = new SqlCommand( sqlP, conn))
                        {
                            using(SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if(TypeC.Equals(reader.GetString(1)))
                                    {
                                        idDemande = reader.GetInt32(0);
                                    }
                                }
                            }
                            string sqlD = "Insert into Demandes(longueurPiece,largeurPiece,typeCouvrePlancher) values(@long,@larg,@type);";
                            using (SqlCommand cmdD = new SqlCommand(sqlD, conn))
                            {
                                cmdD.Parameters.AddWithValue("@long", Longueur);
                                cmdD.Parameters.AddWithValue("@larg", Largeur);
                                cmdD.Parameters.AddWithValue("@type", idDemande);
                                cmdD.ExecuteNonQuery();
                            }
                        }
                        conn.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        public List<string> typeDeCouvre()
        {
            List<string> list = new List<string>();
            try
            {
                using(SqlConnection conn=new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "Select nomCouvrePlancher from CouvrePlancher;";
                    using(SqlCommand cmd=new SqlCommand(sql, conn))
                    {
                        using(SqlDataReader reader=cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(reader.GetString(0));
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            
            return list;
        }
    }
}
