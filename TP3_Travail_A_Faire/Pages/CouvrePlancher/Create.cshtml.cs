using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace TP3_Travail_A_Faire.Pages.CouvrePlancher
{
    public class CreateModel : PageModel
    {
        public CouvrePlancher CP = new CouvrePlancher();
		public string messageErreur = "";
		public void OnGet()
        {
        }
        public void OnPost()
        {
            CP.nomCouvrePlancher = Request.Form["nom"];
            CP.prixMateriax = Convert.ToDouble(Request.Form["prixM"]);
            CP.prixMainOeuvre = Convert.ToDouble(Request.Form["prixMO"]);
            if(CP.nomCouvrePlancher == "" && CP.prixMateriax == 0 && CP.prixMainOeuvre == 0)
            {
				messageErreur = "Veuillez saisir le nom et le prix du produit";
				return;
			}
            try
            {
                string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=CouvrePlanche1;Integrated Security=True";
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "insert into CouvrePlancher(nomCouvrePlancher, prixM2Materiaux,prixM2MainOeuvre) values(@nom, @prixM, @prixMO)";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@nom",CP.nomCouvrePlancher);
                        cmd.Parameters.AddWithValue("@prixM", Convert.ToString(CP.prixMateriax));
                        cmd.Parameters.AddWithValue("@prixMO", Convert.ToString(CP.prixMainOeuvre));

                        cmd.ExecuteNonQuery();
                    }
                }

			}
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            //Reinitialiser les données
            CP.nomCouvrePlancher = "";
            CP.prixMateriax = 0;
            CP.prixMainOeuvre = 0;

			Response.Redirect("/CouvrePlancher/Index");

		}
    }
}
