using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.OleDb;
using System.Data;

public partial class Home : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        object temp = Session["authenticated"];
        if (temp == null || temp.ToString() != "true")
        {
            Session.Add("back", "Home.aspx");
            Server.Transfer("Authentication.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //~~~~~~~~~~~~ SQL ~~~~~~~~~~~~
        string connStr = ConfigurationManager.ConnectionStrings["conn1"].ConnectionString;
        OleDbConnection connection = new OleDbConnection(connStr);

        OleDbCommand selectCmd = new OleDbCommand("SELECT ID, Chemical_Name, Amount, Original_Amount, Units, Concentration, Class, Form, Purchase_Date, Last_Withdraw_Date FROM Chemicals", connection);

        connection.Open();
        OleDbDataReader dr = selectCmd.ExecuteReader();

        //~~~~~~~~~~~~ Header 1 ~~~~~~~~~~~~
        TableRow thr = new TableHeaderRow();
        thr.TableSection = TableRowSection.TableHeader;

        TableCell tc = new TableCell();
        tc.Text = "ID";
        thr.Cells.Add(tc);
        tc = new TableCell();
        tc.Text = "Chemical Name";
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
        tc.Text = "Purchase Date";
        thr.Cells.Add(tc);
        tc = new TableCell();
        tc.Text = "Last Withdrawn";
        thr.Cells.Add(tc);

        this.dataTable1.Rows.Add(thr);

        //~~~~~~~~~~~~ Body 1 & more SQL ~~~~~~~~~~~~
        TableRow tr;
        while (dr.Read())
        {
            if (dr["Amount"].ToString() != "" && dr["Original_Amount"].ToString() != "" &&
                dr["Purchase_Date"].ToString() != "" && dr["Last_Withdraw_date"].ToString() != "" &&
                (Convert.ToDouble(dr["Amount"].ToString()) <= (0.2 * Convert.ToDouble(dr["Original_Amount"].ToString())) ||
                (DateTime.Now - Convert.ToDateTime(dr["Purchase_Date"])).TotalDays > 1095 ||
                (DateTime.Now - Convert.ToDateTime(dr["Last_Withdraw_Date"])).TotalDays > 415))
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
                tc.Text = dr["Amount"].ToString() + dr["Units"].ToString();
                tc.ToolTip = tc.Text;
                tr.Cells.Add(tc);
                tc = new TableCell();
                tc.Text = dr["Original_Amount"].ToString() + dr["Units"].ToString();
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
                tc.Text = String.Format("{0:MM/dd/yyyy}", dr["Purchase_Date"]);
                tc.ToolTip = tc.Text;
                tr.Cells.Add(tc);
                tc = new TableCell();
                tc.Text = String.Format("{0:MM/dd/yyyy hh:mm tt}", dr["Last_Withdraw_Date"]);
                tc.ToolTip = tc.Text;
                tr.Cells.Add(tc);

                this.dataTable1.Rows.Add(tr);
            }
        }

        dr.Close();
        connection.Close();

        //~~~~~~~~~~~~ SQL ~~~~~~~~~~~~
        connStr = ConfigurationManager.ConnectionStrings["conn1"].ConnectionString;
        connection = new OleDbConnection(connStr);

        selectCmd = new OleDbCommand("SELECT tr.Transaction_Date, tr.Chemical_ID, tr.Chemical_Name, tr.Teacher_Name, tr.Amount_Taken, tr.Units, tr.Amount_Left FROM Transactions tr", connection);

        connection.Open();
        dr = selectCmd.ExecuteReader();

        //~~~~~~~~~~~~ Header 2 ~~~~~~~~~~~~
        thr = new TableHeaderRow();
        thr.TableSection = TableRowSection.TableHeader;

        tc = new TableCell();
        tc.Text = "Transaction Date";
        thr.Cells.Add(tc);
        tc = new TableCell();
        tc.Text = "ID";
        thr.Cells.Add(tc);
        tc = new TableCell();
        tc.Text = "Chemical Name";
        thr.Cells.Add(tc);
        tc = new TableCell();
        tc.Text = "Amount Taken";
        thr.Cells.Add(tc);
        tc = new TableCell();
        tc.Text = "Amount Left";
        thr.Cells.Add(tc);
        tc = new TableCell();
        tc.Text = "Teacher Name";
        thr.Cells.Add(tc);

        this.dataTable2.Rows.Add(thr);

        //~~~~~~~~~~~~ Body 2 & more SQL ~~~~~~~~~~~~
        while (dr.Read())
        {
            tr = new TableRow();

            tc = new TableCell();
            tc.Text = dr["Transaction_Date"].ToString();
            tc.ToolTip = tc.Text;
            tr.Cells.Add(tc);
            tc = new TableCell();
            if (dr["Chemical_ID"].ToString().Length == 1)
                tc.Text = "000" + dr["Chemical_ID"].ToString();
            else if (dr["Chemical_ID"].ToString().Length == 2)
                tc.Text = "00" + dr["Chemical_ID"].ToString();
            else if (dr["Chemical_ID"].ToString().Length == 3)
                tc.Text = "0" + dr["Chemical_ID"].ToString();
            else
                tc.Text = dr["Chemical_ID"].ToString();
            tc.ToolTip = tc.Text;
            tr.Cells.Add(tc);
            tc = new TableCell();
            tc.Text = dr["Chemical_Name"].ToString();
            tc.ToolTip = tc.Text;
            tr.Cells.Add(tc);
            tc = new TableCell();
            tc.Text = dr["Amount_Taken"].ToString() + " " + dr["Units"].ToString();
            tc.ToolTip = tc.Text;
            tr.Cells.Add(tc);
            tc = new TableCell();
            tc.Text = dr["Amount_Left"].ToString() + " " + dr["Units"].ToString();
            tc.ToolTip = tc.Text;
            tr.Cells.Add(tc);
            tc = new TableCell();
            tc.Text = dr["Teacher_Name"].ToString();
            tc.ToolTip = tc.Text;
            tr.Cells.Add(tc);

            this.dataTable2.Rows.Add(tr);
        }

        dr.Close();
        connection.Close();
    }
}