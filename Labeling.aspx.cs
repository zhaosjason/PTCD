using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Configuration;

public partial class Labeling : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        object temp = Session["authenticated"];
        if (temp == null || temp.ToString() != "true")
        {
            Session.Add("back", "Labeling.aspx");
            Server.Transfer("Authentication.aspx");
        }
    }

    OleDbConnection conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["conn1"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected string toListString(string full)
    {
        string toReturn = "";
        string[] parts = full.Split(',');
        foreach (string part in parts)
        {
            int index = part.IndexOf('-');
            if (index != -1)
            {
                string firstIndexStr = part.Substring(0, index);
                string secondIndexStr = part.Substring(index + 1, part.Length - index - 1);
                int firstIndex = Convert.ToInt32(firstIndexStr);
                int secondIndex = Convert.ToInt32(secondIndexStr);
                for (int i = firstIndex; i <= secondIndex; i++)
                    toReturn += i + ",";
            }
            else
                toReturn += part + ",";
        }
        toReturn = toReturn.Substring(0, toReturn.Length - 1);
        return toReturn;
    }

    protected string fromListString(string full)
    {
        if (full == "")
            return "";
        else if (full.Length == 1)
            return full;

        string[] parts = full.Split(',');
        int[] intParts = new int[parts.Length];
        for (int i = 0; i < parts.Length; i++)
            intParts[i] = Convert.ToInt32(parts[i]);
        string ret = parts[0];
        
        for(int i = 1; i < parts.Length; i++)
        {
            if (i != parts.Length - 1)
            {
                if (intParts[i] - intParts[i - 1] == 1 && intParts[i + 1] - intParts[i] == 1)
                    continue;
                else if (intParts[i] - intParts[i - 1] != 1 && intParts[i + 1] - intParts[i] != 1)
                    ret += "," + parts[i];
                else if (intParts[i] - intParts[i - 1] != 1 && intParts[i + 1] - intParts[i] == 1)
                    ret += "," + parts[i];
                else if (intParts[i] - intParts[i - 1] == 1 && intParts[i + 1] - intParts[i] != 1)
                    ret += "-" + parts[i];
            }
            else
            {
                if (intParts[i] - intParts[i - 1] == 1)
                    ret += "-" + parts[i];
                else if(intParts[i] - intParts[i - 1] != 1)
                    ret += "," + parts[i];
            }
        }
        return ret;
    }

    protected Boolean listContainsList(string list1, string list2, out string err)
    {
        err = "";
        string[] strArr1 = list1.Split(',');
        string[] strArr2 = list2.Split(',');
        int[] intArr1 = new int[strArr1.Length]; //database indexes
        int[] intArr2 = new int[strArr2.Length]; //typed indexes

        for (int i = 0; i < strArr1.Length; i++)
            intArr1[i] = Convert.ToInt32(strArr1[i]);
        for (int i = 0; i < strArr2.Length; i++)
            intArr2[i] = Convert.ToInt32(strArr2[i]);

        Boolean areAllInDatabase = true;
        for (int i2 = 0; i2 < intArr2.Length; i2++)
        {
            Boolean isInDatabase = false;
            for (int i1 = 0; i1 < intArr1.Length; i1++)
            {
                if (intArr1[i1] == intArr2[i2])
                {
                    isInDatabase = true;
                    break;
                }
            }
            if (!isInDatabase)
            {
                err += intArr2[i2] + ",";
                areAllInDatabase = false;
            }
        }
        if (err.Length > 0)
            err = err.Substring(0, err.Length - 1);
        return areAllInDatabase;
    }

    protected void btnSelect_Click(object sender, EventArgs e)
    {
        Boolean existInDatabase = false;
        Boolean fieldDataExists = false;

        string idsString = this.txtIDs.Text;
        Regex.Replace(idsString, @"\s+", "");
        string listString = "";

        if (idsString != "")
            fieldDataExists = true;

        if (fieldDataExists == false)
        {
            this.lblErrors.Visible = true;
            this.lblErrors.Text = "* ";
            this.lblErrors.Text += "You must enter at least one ID";
        }
        else
        {
            listString = toListString(idsString);

            OleDbCommand inBoundsSelectCmd = new OleDbCommand("SELECT ID FROM Chemicals", conn);

            conn.Open();
            OleDbDataReader drInBounds = inBoundsSelectCmd.ExecuteReader();
            string idsInDatabaseStr = "";
            while (drInBounds.Read())
            {
                idsInDatabaseStr += drInBounds["ID"] + ",";
            }
            conn.Close();
            drInBounds.Close();
            idsInDatabaseStr = idsInDatabaseStr.Substring(0, idsInDatabaseStr.Length - 1);
            string outErr = "";
            if (listContainsList(idsInDatabaseStr, listString, out outErr))
                existInDatabase = true;

            if (existInDatabase == false && this.chkMissingIDs.Checked)
            {
                this.lblErrors.Visible = true;
                this.lblErrors.Text = "* ";
                this.lblErrors.Text += "Some of the IDs were not found in the database: " + fromListString(outErr);
            }

            if (existInDatabase == true || (existInDatabase == false && this.chkMissingIDs.Checked == false))
            {
                string redirectString = "LabelPrint.aspx?ids=" + listString;
                Response.Redirect(redirectString);
            }
        }
    }

    protected string listUnion(string listString1, string listString2)
    {
        string toReturn = "";

        string[] list1 = listString1.Split(',');
        string[] list2 = listString2.Split(',');

        for (int x = 0; x < list1.Length; x++)
        {
            for (int y = 0; y < list2.Length; y++)
            {
                if (list1[x].Equals(list2[y]))
                {
                    toReturn += list1[x] + ",";
                    break;
                }
            }
        }
        toReturn = toReturn.Substring(0, toReturn.Length - 1);
        return toReturn;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
}