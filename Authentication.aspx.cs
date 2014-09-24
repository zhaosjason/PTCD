using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.OleDb;
using System.Data;

public partial class Authentication : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void authenticateButton_Click(object sender, EventArgs e)
    {
        string connStr = ConfigurationManager.ConnectionStrings["conn1"].ConnectionString;
        OleDbConnection connection = new OleDbConnection(connStr);

        OleDbCommand selectCmd = new OleDbCommand("SELECT Pass FROM Passwords", connection);

        connection.Open();
        OleDbDataReader dr = selectCmd.ExecuteReader();

        while (dr.Read())
        {
            String password = dr["Pass"].ToString();
            if (this.authenticateText.Text == password)
            {
                Session.Add("authenticated", "true");
                if(Session["back"] == null)
                    Response.Redirect("Home.aspx");
                else
                    Response.Redirect(Session["back"].ToString());
            }
            else
                Session.Abandon();
        }
    }
}