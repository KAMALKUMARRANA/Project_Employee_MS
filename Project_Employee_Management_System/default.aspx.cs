using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading;

namespace Project_Employee_Management_System
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void Alert(string message)
        {
            var m = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(message);
            var script = string.Format("alert({0});", m);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", script, true);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            
            string name = txtName.Text.Trim();
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            string cs1 = ConfigurationManager.ConnectionStrings["DBCS1"].ConnectionString;
            try
            {
                using (SqlConnection con = new SqlConnection(cs1))
                {
                    con.Open();
                    DataTable dt = new DataTable();
                    SqlCommand cmdinsert = new SqlCommand();
                    cmdinsert.Connection = con;
                    // cmdinsert.CommandText = "insert into tblProject values(@Project_Id ,@Name)";
                    cmdinsert.CommandText = name;
                    // cmdinsert.Parameters.AddWithValue("@Project_Id", pid);
                    //  cmdinsert.Parameters.AddWithValue("@Name", name);
                    cmdinsert.ExecuteNonQuery();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmdinsert;
                    da.Fill(dt);

                    gv.DataSource = dt;
                    gv.DataBind();

                    Response.Write("Quary Execute Sucessfully!");

                }
            }
            catch
            {
                Alert("Dadabase Error!");
            }
        }
    }
}