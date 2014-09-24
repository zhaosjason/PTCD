using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.OleDb;

public partial class Database : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        object temp = Session["authenticated"];
        if (temp == null || temp.ToString() != "true")
        {
            Session.Add("back", "Database.aspx");
            Server.Transfer("Authentication.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //~~~~~~~~~~~~ SQL ~~~~~~~~~~~~
        string connStr = ConfigurationManager.ConnectionStrings["conn1"].ConnectionString;
        OleDbConnection connection = new OleDbConnection(connStr);

        OleDbCommand selectCmd = new OleDbCommand("SELECT c.ID, c.Chemical_Name, c.Class, c.Form, c.Family, c.Hazard_Code, c.Disposal_Code, c.Amount, c.Original_Amount, c.Units, c.Concentration, c.Location_ID, c.Purchase_Date, c.Checksum, l.Location_Name FROM Chemicals c INNER JOIN Locations l ON c.Location_ID = l.ID;", connection);

        connection.Open();
        OleDbDataReader dr = selectCmd.ExecuteReader();

        //~~~~~~~~~~~~ Header ~~~~~~~~~~~~
        TableRow thr = new TableHeaderRow();
        thr.TableSection = TableRowSection.TableHeader;

        TableCell tc = new TableCell();
        tc.Text = "ID";
        thr.Cells.Add(tc);
        tc = new TableCell();
        tc.Text = "Name";
        thr.Cells.Add(tc);
        tc = new TableCell();
        tc.Text = "Amount";
        thr.Cells.Add(tc);
        tc = new TableCell();
        tc.Text = "Original";
        thr.Cells.Add(tc);
        tc = new TableCell();
        tc.Text = "[ ]";
        thr.Cells.Add(tc);
        tc = new TableCell();
        tc.Text = "Class";
        thr.Cells.Add(tc);
        tc = new TableCell();
        tc.Text = "Form";
        thr.Cells.Add(tc);
        tc = new TableCell();
        tc.Text = "Family";
        thr.Cells.Add(tc);
        tc = new TableCell();
        tc.Text = "Hazard";
        thr.Cells.Add(tc);
        tc = new TableCell();
        tc.Text = "Disposal";
        thr.Cells.Add(tc);
        tc = new TableCell();
        tc.Text = "Purchase Date";
        thr.Cells.Add(tc);
        tc = new TableCell();
        tc.Text = "Location";
        thr.Cells.Add(tc);
        tc = new TableCell();
        tc.Text = "CKS";
        thr.Cells.Add(tc);

        this.dataTable3.Rows.Add(thr);

        //~~~~~~~~~~~~ Body & more SQL ~~~~~~~~~~~~
        TableRow tr;
        while (dr.Read())
        {
            tr = new TableRow();
            tc = new TableCell();
            if (dr["ID"].ToString().Length == 1)
                tc.Text = "000" + dr["ID"].ToString();
            else if (dr["ID"].ToString().Length == 2)
                tc.Text = "00" + dr["ID"].ToString();
            else if (dr["ID"].ToString().Length == 3)
                tc.Text = "0" + dr["ID"].ToString();
            else
                tc.Text = dr["ID"].ToString();
            tc.ToolTip = tc.Text;
            tr.Cells.Add(tc);
            tc = new TableCell();
            tc.Text = dr["Chemical_Name"].ToString();
            tc.ToolTip = tc.Text;
            tr.Cells.Add(tc);
            tc = new TableCell();
            tc.Text = dr["Amount"].ToString() + " " + dr["Units"].ToString();
            tc.ToolTip = tc.Text;
            tr.Cells.Add(tc);
            tc = new TableCell();
            tc.Text = dr["Original_Amount"].ToString() + " " + dr["Units"].ToString();
            tc.ToolTip = tc.Text;
            tr.Cells.Add(tc);
            tc = new TableCell();
            if (dr["Concentration"].ToString() != "")
                tc.Text = dr["Concentration"].ToString() + " M";
            else
                tc.Text = " ";
            tc.ToolTip = tc.Text;
            tr.Cells.Add(tc);
            tc = new TableCell();
            tc.Text = dr["Class"].ToString();
            tc.ToolTip = tc.Text;
            tr.Cells.Add(tc);
            tc = new TableCell();
            tc.Text = dr["Form"].ToString();
            tc.ToolTip = tc.Text;
            tr.Cells.Add(tc);
            tc = new TableCell();
            tc.Text = dr["Family"].ToString();
            tc.ToolTip = tc.Text;
            tr.Cells.Add(tc);
            tc = new TableCell();
            tc.Text = dr["Hazard_Code"].ToString();
            tc.ToolTip = tc.Text;
            tr.Cells.Add(tc);
            tc = new TableCell();
            tc.Text = dr["Disposal_Code"].ToString();
            tc.ToolTip = tc.Text;
            tr.Cells.Add(tc);
            tc = new TableCell();
            tc.Text = String.Format("{0:MM/dd/yyyy}", dr["Purchase_Date"]);
            tc.ToolTip = tc.Text;
            tr.Cells.Add(tc);
            tc = new TableCell();
            tc.Text = dr["Location_Name"].ToString();
            tc.ToolTip = tc.Text;
            tr.Cells.Add(tc);
            tc = new TableCell();
            tc.Text = dr["Checksum"].ToString();
            tc.ToolTip = tc.Text;
            tr.Cells.Add(tc);

            this.dataTable3.Rows.Add(tr);
        }
    }

    protected void btnAddEntry_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddEntry.aspx");
    }

    protected void btnEditTable_Click(object sender, EventArgs e)
    {
        Response.Redirect("EditDatabase.aspx");
    }
}