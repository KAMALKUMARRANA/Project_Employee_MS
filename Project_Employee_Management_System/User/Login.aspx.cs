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
using System.Collections.Specialized;
using System.Net.Mail;
using System.Data.Sql;
using System.IO;
using System.Text;
using System.Linq;
using System.Net;

namespace Project_Employee_Management_System.User
{
    public partial class Login : System.Web.UI.Page
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

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Thread.Sleep(1500);
            string empid = txtEmpId.Text.Trim();
            string password = txtPassword.Text.Trim();
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            if (empid != null && password != null)
            {
                DataTable dt = new DataTable();
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "select * from tblUser where Employee_Id = @x and Password = @p";
                    cmd.Parameters.AddWithValue("@x", empid);
                    cmd.Parameters.AddWithValue("@p", password);
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }

                if (dt.Rows.Count != 0)
                {

                    Session.Add("userid", dt.Rows[0]["User_Id"].ToString().Trim());
                    Session.Add("password", dt.Rows[0]["Password"].ToString().Trim());
                    Session.Add("name", dt.Rows[0]["Name"].ToString().Trim());
                    Session.Add("role", dt.Rows[0]["Role"].ToString().Trim());
                    Session.Add("empid", dt.Rows[0]["Employee_Id"].ToString().Trim());
                   


                    Alert("Sucessfully Login!");

                }
                else
                {
                    Alert("Invalid Id or Password!");
                }

            }
        }

        protected void btnReg_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }
    }
}

