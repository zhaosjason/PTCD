using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.OleDb;

public partial class Settings : System.Web.UI.Page
{
    OleDbConnection conn;

    protected void Page_Init(object sender, EventArgs e)
    {
        object temp = Session["authenticated"];
        if (temp == null || temp.ToString() != "true")
        {
            Session.Add("back", "Settings.aspx");
            Server.Transfer("Authentication.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Label1.Text = "";

        if (!IsPostBack)
        {
            string connStr = ConfigurationManager.ConnectionStrings["conn1"].ConnectionString;
            OleDbConnection connection = new OleDbConnection(connStr);

            //Teachers
            OleDbCommand selectCmd = new OleDbCommand("SELECT * FROM Teachers", connection);

            connection.Open();
            OleDbDataReader dr = selectCmd.ExecuteReader();

            while (dr.Read())
            {
                this.DropDownList1.Items.Add(new ListItem(dr["Last_Name"] + ", " + dr["First_Name"]));
            }

            this.DropDownList1.Items.Insert(0, new ListItem("Select a name"));
            this.DropDownList1.SelectedValue = "Select a name";
            connection.Close();

            //Locations
            selectCmd = new OleDbCommand("SELECT * FROM Locations", connection);

            connection.Open();
            dr = selectCmd.ExecuteReader();

            this.DropDownList2.DataSource = dr;
            this.DropDownList2.DataTextField = "Location_Name";
            this.DropDownList2.DataValueField = "Location_Name";
            this.DropDownList2.DataBind();
            this.DropDownList2.Items.Insert(0, new ListItem("Select a location"));
            this.DropDownList2.SelectedValue = "Select a location";

            connection.Close();
        }

        conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["conn1"].ConnectionString);
        OleDbCommand selectCmd2 = new OleDbCommand("SELECT Pass FROM Passwords WHERE ID = 1", conn);

        conn.Open();
        OleDbDataReader dr2 = selectCmd2.ExecuteReader();

        String currPass = null;
        while (dr2.Read())
            currPass = dr2["Pass"].ToString();

        //Response.Write(currPass);
        cvCurrpass.ValueToCompare = currPass;
        conn.Close();
    }

    protected void btnDone_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (this.DropDownList1.SelectedIndex != 0 || this.DropDownList2.SelectedIndex != 0)
        {
            string connStr = ConfigurationManager.ConnectionStrings["conn1"].ConnectionString;
            OleDbConnection connection = new OleDbConnection(connStr);

            OleDbCommand deleteCmd = null;

            if (this.DropDownList1.SelectedIndex != 0)
            {
                deleteCmd = new OleDbCommand("DELETE FROM Teachers WHERE Last_Name = '" + this.DropDownList1.SelectedValue.Split(',')[0] + "'", connection);
            }

            if (this.DropDownList2.SelectedIndex != 0)
            {
                deleteCmd = new OleDbCommand("DELETE FROM Locations WHERE Location_Name = '" + this.DropDownList2.SelectedValue + "'", connection);
            }

            connection.Open();
            deleteCmd.ExecuteReader();
            connection.Close();

            this.Label1.Text = "The fields have been sucessfully deleted!";
            this.refreshPage();
        }
        else
        {
            this.Label1.Text = "* Please fill in the required fields.";
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if ((this.TextBox1.Text.Trim() != "" && this.TextBox2.Text.Trim() != "" && this.TextBox4.Text.Trim() != "") ||
            (this.TextBox3.Text.Trim() != ""))
        {
            string connStr = ConfigurationManager.ConnectionStrings["conn1"].ConnectionString;
            OleDbConnection connection = new OleDbConnection(connStr);

            OleDbCommand insertCmd = null;

            if (this.TextBox1.Text.Trim() != "" && this.TextBox2.Text.Trim() != "" && this.TextBox4.Text.Trim() != "")
            {
                insertCmd = new OleDbCommand("INSERT INTO Teachers(First_Name, Last_Name, Email) Values (?, ?, ?)", connection);
                insertCmd.Parameters.Add(new OleDbParameter("@First_Name", this.TextBox1.Text));
                insertCmd.Parameters.Add(new OleDbParameter("@Last_Name", this.TextBox2.Text));
                insertCmd.Parameters.Add(new OleDbParameter("@Email", this.TextBox4.Text));
            }

            if (this.TextBox3.Text.Trim() != "")
            {
                insertCmd = new OleDbCommand("INSERT INTO Locations(Location_Name) Values (?)", connection);
                insertCmd.Parameters.Add(new OleDbParameter("@Location_Name", this.TextBox3.Text));
            }

            connection.Open();
            insertCmd.ExecuteNonQuery();
            connection.Close();

            this.Label1.Text = "The fields have been sucessfully added!";
            this.refreshPage();
        }
        else
        {
            this.Label1.Text = "* Please fill in the required fields.";
        }
    }

    protected void refreshPage()
    {
        this.TextBox1.Text = "";
        this.TextBox2.Text = "";
        this.TextBox4.Text = "";
        this.TextBox3.Text = "";

        this.DropDownList1.Items.Clear();
        this.DropDownList2.Items.Clear();

        string connStr = ConfigurationManager.ConnectionStrings["conn1"].ConnectionString;
        OleDbConnection connection = new OleDbConnection(connStr);

        //Teachers
        OleDbCommand selectCmd = new OleDbCommand("SELECT * FROM Teachers", connection);

        connection.Open();
        OleDbDataReader dr = selectCmd.ExecuteReader();

        while (dr.Read())
        {
            this.DropDownList1.Items.Add(new ListItem(dr["Last_Name"] + ", " + dr["First_Name"]));
        }

        this.DropDownList1.Items.Insert(0, new ListItem("Select a name"));
        this.DropDownList1.SelectedValue = "Select a name";
        connection.Close();

        //Locations
        selectCmd = new OleDbCommand("SELECT * FROM Locations", connection);

        connection.Open();
        dr = selectCmd.ExecuteReader();

        this.DropDownList2.DataSource = dr;
        this.DropDownList2.DataTextField = "Location_Name";
        this.DropDownList2.DataValueField = "Location_Name";
        this.DropDownList2.DataBind();
        this.DropDownList2.Items.Insert(0, new ListItem("Select a location"));
        this.DropDownList2.SelectedValue = "Select a location";

        connection.Close();
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
            lblConfirm.Text = "Password Changed Successfully!";
            conn.Close();
        }
    }
}