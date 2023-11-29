using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace TP3_Travail_A_Faire.Pages.CouvrePlancher
{
    public class IndexModel : PageModel
    {
		public List<CouvrePlancher> CouvrePlancherList = new List<CouvrePlancher>();
		public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=CouvrePlanche1;Integrated Security=True";
                using(SqlConnection con = new SqlConnection(connectionString))
                {
                    string sql = "select * from CouvrePlancher";
                    con.Open();
                    using(SqlCommand cmd = new SqlCommand(sql,con))
                    {
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CouvrePlancher CP = new CouvrePlancher();
                                CP.idCouvrePlancher=reader.GetInt32(0);
                                CP.nomCouvrePlancher = reader.GetString(1);
                                CP.prixMateriax = Convert.ToDouble(reader.GetDecimal(2));
                                CP.prixMainOeuvre = Convert.ToDouble(reader.GetDecimal(3));

								CouvrePlancherList.Add(CP);

							}
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
    public class CouvrePlancher
    {
        public int idCouvrePlancher;
        public string nomCouvrePlancher = "";
        public double prixMateriax;
        public double prixMainOeuvre;
    }
}
