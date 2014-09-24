using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.OleDb;

public partial class EditDatabase : System.Web.UI.Page
{

    protected void Page_Init(object sender, EventArgs e)
    {
        object temp = Session["authenticated"];
        if (temp == null || temp.ToString() != "true")
        {
            Session.Add("back", "EditDatabase.aspx");
            Server.Transfer("Authentication.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            Session.Add("last", "ID");
    }

    protected void Sort_Click(object sender, EventArgs e)
    {
        LinkButton temp = (LinkButton) sender;

        if (GridView1.SortDirection == SortDirection.Ascending && temp.ID == Session["last"])
            this.GridView1.Sort(temp.ID, SortDirection.Descending);
        else
            this.GridView1.Sort(temp.ID, SortDirection.Ascending);

        Session["last"] = temp.ID;
    }
}