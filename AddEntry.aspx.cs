using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.OleDb;
using System.Data;

public partial class AddEntry : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        object temp = Session["authenticated"];
        if (temp == null || temp.ToString() != "true")
        {
            Session.Add("back", "AddEntry.aspx");
            Server.Transfer("Authentication.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string connStr = ConfigurationManager.ConnectionStrings["conn1"].ConnectionString;
            OleDbConnection connection = new OleDbConnection(connStr);

            OleDbCommand selectCmd = new OleDbCommand("SELECT Units FROM Units", connection);

            connection.Open();
            OleDbDataReader dr = selectCmd.ExecuteReader();

            this.DropDownList1.DataSource = dr;
            this.DropDownList1.DataTextField = "Units";
            this.DropDownList1.DataValueField = "Units";
            this.DropDownList1.DataBind();
            this.DropDownList1.Items.Insert(0, new ListItem("Units", ""));
            this.DropDownList1.SelectedValue = "Units";
            connection.Close();

            selectCmd = new OleDbCommand("SELECT Location_Name FROM Locations", connection);

            connection.Open();
            dr = selectCmd.ExecuteReader();

            this.DropDownList2.DataSource = dr;
            this.DropDownList2.DataTextField = "Location_Name";
            this.DropDownList2.DataValueField = "Location_Name";
            this.DropDownList2.DataBind();
            this.DropDownList2.Items.Insert(0, new ListItem("Select a location", ""));
            this.DropDownList2.SelectedValue = "Select a location";
            connection.Close();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (this.TextBox1.Text != "" && this.TextBox2.Text != "" && this.DropDownList1.SelectedIndex != 0 &&
            this.TextBox5.Text != "" && this.TextBox6.Text != "" && this.TextBox7.Text != "" && this.TextBox8.Text != "" &&
            this.TextBox9.Text != "" && this.DropDownList2.SelectedIndex != 0)
        {

            string connStr = ConfigurationManager.ConnectionStrings["conn1"].ConnectionString;
            OleDbConnection connection = new OleDbConnection(connStr);

            OleDbCommand insertCmd = new OleDbCommand("INSERT INTO Chemicals(Chemical_Name, Amount, Original_Amount, Units, Concentration, Class, Form, Family, Hazard_Code, Disposal_Code, Purchase_Date, Location_ID, Checksum) Values (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", connection);
            insertCmd.Parameters.Add(new OleDbParameter("@Chemical_Name", this.TextBox1.Text));
            insertCmd.Parameters.Add(new OleDbParameter("@Amount", this.TextBox2.Text));
            insertCmd.Parameters.Add(new OleDbParameter("@Original_Amount", this.TextBox2.Text));
            insertCmd.Parameters.Add(new OleDbParameter("@Units", this.DropDownList1.SelectedItem.Text));
            if(this.TextBox4.Text == "")
                insertCmd.Parameters.Add(new OleDbParameter("@Concentration", "0"));
            else
                insertCmd.Parameters.Add(new OleDbParameter("@Concentration", this.TextBox4.Text));
            insertCmd.Parameters.Add(new OleDbParameter("@Class", this.TextBox5.Text));
            insertCmd.Parameters.Add(new OleDbParameter("@Form", this.TextBox6.Text));
            insertCmd.Parameters.Add(new OleDbParameter("@Family", this.TextBox7.Text));
            insertCmd.Parameters.Add(new OleDbParameter("@Hazard_Code", this.TextBox8.Text));
            insertCmd.Parameters.Add(new OleDbParameter("@Disposal_Code", this.TextBox9.Text));
            if(this.TextBox11.Text.Trim() == "")
                insertCmd.Parameters.Add(new OleDbParameter("@Purchase_Date", Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"))));
            else
                insertCmd.Parameters.Add(new OleDbParameter("@Purchase_Date", Convert.ToDateTime(this.TextBox11.Text)));
            insertCmd.Parameters.Add(new OleDbParameter("@Location_ID", (this.DropDownList2.SelectedIndex)));
            if (this.TextBox10.Text.Trim() == "")
            {
                OleDbConnection temp = new OleDbConnection(connStr);
                OleDbCommand selectCmd = new OleDbCommand("SELECT ID, checksum FROM Chemicals", temp);

                temp.Open();
                OleDbDataReader tempDr = selectCmd.ExecuteReader();

                List<string> checksums = new List<string>();
                while (tempDr.Read())
                {
                    checksums.Add(tempDr["Checksum"].ToString());
                }

                string chars = "0123456789abcdefghijklmnopqrstuvwxyz0123456789";
                Random random = new Random();
                string result = "";

                bool unique = false;
                while (unique == false)
                {
                    result = new string(Enumerable.Repeat(chars, 3).Select(s => s[random.Next(s.Length)]).ToArray());
                    unique = true;
                    for (int i = 0; i < checksums.Count; i++)
                    {
                        if (checksums[i] == result)
                            unique = false;
                    }
                }

                temp.Close();

                insertCmd.Parameters.Add(new OleDbParameter("@Checksum", result));
            }
            else
                insertCmd.Parameters.Add(new OleDbParameter("@Checksum", this.TextBox10.Text));

            connection.Open();
            insertCmd.ExecuteNonQuery();
            connection.Close();

            Response.Redirect("Database.aspx");
        }
        else
        {
            this.Label1.Text = "* Please fill out all required fields.";
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Database.aspx");
    }
}