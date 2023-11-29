using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace TP3_Travail_A_Faire.Pages.CouvrePlancher
{
    public class DeletModel : PageModel
    {
		public CouvrePlancher CP = new CouvrePlancher();
		public void OnGet()
        {
			string id = Request.Query["id"];

			try
			{
				string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=CouvrePlanche1;Integrated Security=True";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					string sql = "delete from CouvrePlancher where idCouvrePlancher = @id";

					using (SqlCommand cmd = new SqlCommand(sql, connection))
					{
						cmd.Parameters.AddWithValue("@id", id);
						cmd.ExecuteNonQuery();
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Exception: 0" + e.ToString());
			}
			Response.Redirect("/CouvrePlancher/Index");
		}
    }
}
