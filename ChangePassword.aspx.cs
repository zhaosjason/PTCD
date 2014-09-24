using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Configuration;

public partial class ChangePassword : System.Web.UI.Page
{
    OleDbConnection conn;

    protected void Page_Load(object sender, EventArgs e)
    {
        conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        OleDbCommand selectCmd = new OleDbCommand("SELECT Pass FROM Passwords WHERE ID = 1", conn);

        conn.Open();
        OleDbDataReader dr = selectCmd.ExecuteReader();

        String currPass = null;
        while (dr.Read())
            currPass = dr["Pass"].ToString();

        //Response.Write(currPass);
        cvCurrpass.ValueToCompare = currPass;
        conn.Close();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("ChangePassword.aspx");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        lblConfirm.Text = "";
        if (Page.IsValid)
        {
            OleDbCommand changePassword = new OleDbCommand("UPDATE Passwords SET Pass = ? WHERE ID = 1", conn);
            changePassword.Parameters.Add(new OleDbParameter("@Pass", OleDbType.VarChar, 100, "Pass"));
            changePassword.Parameters["@Pass"].Value = tbConfirmPass.Text;

            conn.Open();
            changePassword.ExecuteNonQuery();
            lblConfirm.Text = "Password Changed Successfully";
            conn.Close();
        }


    }
}