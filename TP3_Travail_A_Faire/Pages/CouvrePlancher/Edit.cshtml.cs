using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace TP3_Travail_A_Faire.Pages.CouvrePlancher
{
    public class EditModel : PageModel
    {
		public string messageErreur = "";
        public CouvrePlancher cP= new CouvrePlancher();
		public void OnGet()
        {
            string id = Request.Query["id"];

			try
			{
				string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=CouvrePlanche1;Integrated Security=True";
				using(SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();
					string sql = "select * from CouvrePlancher where idCouvrePlancher = @id";
					using(SqlCommand cmd = new SqlCommand(sql,conn))
					{
						cmd.Parameters.AddWithValue("@id", id);
						using(SqlDataReader reader = cmd.ExecuteReader())
						{
							if (reader.Read())
							{
								cP.idCouvrePlancher = reader.GetInt32(0);
								cP.nomCouvrePlancher = reader.GetString(1);
								cP.prixMateriax = Convert.ToDouble(reader.GetDecimal(2));
								cP.prixMainOeuvre= Convert.ToDouble(reader.GetDecimal(3));

							}
						}
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Exception: " + e.ToString());
			}
        }
		public void OnPost()
		{
			cP.idCouvrePlancher = Convert.ToInt32(Request.Form["id"]);
			cP.nomCouvrePlancher = Request.Form["nom"];
			cP.prixMateriax = Convert.ToDouble(Request.Form["prixM"]);
			cP.prixMainOeuvre = Convert.ToDouble(Request.Form["prixMO"]);

			if (cP.nomCouvrePlancher == "" || cP.prixMateriax == 0 || cP.prixMainOeuvre == 0)
			{
				messageErreur = "Veuillez saisir le nom et le prix";
				return;
			}
			try
			{
				string connectionStirng = "Data Source=.\\SQLEXPRESS;Initial Catalog=CouvrePlanche1;Integrated Security=True";
				using(SqlConnection conn = new SqlConnection(connectionStirng))
				{
					conn.Open();
					string sql = "Update CouvrePlancher set nomCouvrePlancher=@nom, prixM2Materiaux=@prixM, prixM2MainOeuvre=@prixMO where idCouvrePlancher=@id";
					using(SqlCommand cmd =new SqlCommand(sql, conn))
					{
						cmd.Parameters.AddWithValue("@nom", cP.nomCouvrePlancher);
						cmd.Parameters.AddWithValue("@prixM", cP.prixMateriax);
						cmd.Parameters.AddWithValue("@prixMO", cP.prixMainOeuvre);
						cmd.Parameters.AddWithValue("@id", cP.idCouvrePlancher);
						
						cmd.ExecuteNonQuery();
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Exception: " + e.ToString());
			}
			Response.Redirect("/CouvrePlancher/Index");
		}
    }
}
