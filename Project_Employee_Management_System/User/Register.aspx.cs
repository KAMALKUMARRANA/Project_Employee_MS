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

namespace Project_Employee_Management_System.User
{
    public partial class Register : System.Web.UI.Page
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


        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Thread.Sleep(1000);
            string userid = txtUserId.Text.Trim();
            string name = txtName.Text.Trim();
            string role = txtRole.Text.Trim();
            string empid = txtEmployeeId.Text.Trim();
            string pass = txtPassword.Text.Trim();
            try {
                string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    DataTable dt = new DataTable();
                    SqlCommand cmdinsert = new SqlCommand();
                    cmdinsert.Connection = con;
                    cmdinsert.CommandText = "insert into tblUser values (@User_Id ,@Name,@Password,@Role,@Employee_Id)";

                    cmdinsert.Parameters.AddWithValue("@User_Id", userid);
                    cmdinsert.Parameters.AddWithValue("@Name", name);
                    cmdinsert.Parameters.AddWithValue("@Password", pass);
                    cmdinsert.Parameters.AddWithValue("@Role", role);
                    cmdinsert.Parameters.AddWithValue("@Employee_Id", empid);
                    cmdinsert.ExecuteNonQuery();

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.Credentials = new System.Net.NetworkCredential("kamalworld.official@gmail.com", "thpvebvvvusqupsn");
                    MailMessage msg = new MailMessage();
                    smtp.EnableSsl = true;
                    msg.Subject = "Account Credential@DEIMS";
                    msg.Body = "Dear " + txtName.Text.Trim() + " Your are Successfully register our system. \n Your Employee ID is: " + empid + " and Password is: " + txtPassword.Text.Trim() + ".Don't shere with enyone! \n\n N.B:-  You Eligible to login our system when Head Of Department Activate Your Account!" + "\n\n\n Thanks & Regard\n Admin \n DEIMS!";
                    string toaddress = txtEmail.Text.Trim();
                    msg.To.Add(toaddress);
                    string fromaddress = "ADMIN@DEIMS<kamalworld.official@gmail.com> ";
                    msg.From = new MailAddress(fromaddress);

                    smtp.Send(msg);


                    Response.Write("Done!");
                }

            }
            catch
            {
                Alert("User already exist!");
            }
            }
        
        }
    
}
