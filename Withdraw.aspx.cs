using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using System.Configuration;

public partial class Withdraw : System.Web.UI.Page
{
    OleDbConnection conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        String queryStringID = Request.QueryString["ID"];
        if (queryStringID != null)
        {
            if (!IsPostBack)
            {
                OleDbCommand unitsSelectCmd = new OleDbCommand("SELECT * FROM Units", conn);

                OleDbCommand teachersSelectCmd = new OleDbCommand("SELECT First_Name, Last_Name, ID FROM Teachers", conn);
                OleDbCommand chemicalDataSelectCmd = new OleDbCommand("SELECT * FROM Chemicals WHERE ID = ?", conn);
                chemicalDataSelectCmd.Parameters.Add(new OleDbParameter("@ID", queryStringID));

                string defaultMessage = "Select Your Name";
                ListItem defaultListItem = new ListItem(defaultMessage, "");
                this.ddlTeacherName.Items.Add(defaultListItem);

                conn.Open();
                OleDbDataReader drTeachers = teachersSelectCmd.ExecuteReader();
                while (drTeachers.Read())
                    this.ddlTeacherName.Items.Add(new ListItem(drTeachers["Last_Name"] + ", " + drTeachers["First_Name"], drTeachers["ID"].ToString()));
                drTeachers.Close();

                OleDbDataReader drUnits = unitsSelectCmd.ExecuteReader();
                this.ddlUnits.DataSource = drUnits;
                this.ddlUnits.DataTextField = "Units";
                this.ddlUnits.DataValueField = "Units";
                this.ddlUnits.DataBind();
                drUnits.Close();

                object chemicalNameObject = null;
                object amountObject = null;
                object unitsObject = null;
                OleDbDataReader drChemicalData = chemicalDataSelectCmd.ExecuteReader();
                while (drChemicalData.Read())
                {
                    chemicalNameObject = drChemicalData["Chemical_Name"];
                    amountObject = drChemicalData["Amount"];
                    unitsObject = drChemicalData["Units"];
                }
                conn.Close();
                //if (!chemicalNameObject.ToString().Equals("") && !amountObject.ToString().Equals("") && !unitsObject.ToString().Equals(""))
                //{
                    this.lblChemicalName.Text = (string)chemicalNameObject;
                    this.lblCurrentAmount.Text = "" + amountObject;
                    this.lblCurrentUnits.Text = (string)unitsObject;
                //}

                if (Session["transaction"] != null)
                {
                    Transaction transaction = (Transaction)Session["transaction"];
                    this.txtAmount.Text = transaction.amountTakenFieldUnits + "";
                    this.ddlTeacherName.SelectedValue = transaction.teacherID + "";
                    this.ddlUnits.SelectedValue = transaction.fieldUnits;
                    this.txtChecksum.Text = transaction.checksum;
                }
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        String queryStringID = Request.QueryString["id"];
        if (queryStringID != null)
        {
            Boolean nameExists = true;//
            Boolean amountExists = true;//
            Boolean amountIsNumber = true;//
            Boolean amountIsInBounds = true;//
            Boolean checksumExists = true;//
            Boolean checksumMatches = true;//
            Boolean unitsMatch = true;//
            Boolean unitsConvert = true;//

            string databaseChecksum = "";
            double databaseAmount = 0;
            double fieldAmount = 0;
            double fieldAmountNewUnits = 0;
            string databaseUnits = "";
            string fieldUnits = this.ddlUnits.SelectedItem.Value;

            OleDbCommand checksumSelectCmd = new OleDbCommand("SELECT Checksum FROM Chemicals WHERE ID = ?", conn);
            checksumSelectCmd.Parameters.Add(new OleDbParameter("@ID", queryStringID));
            OleDbCommand unitsSelectCmd = new OleDbCommand("SELECT Units FROM Chemicals WHERE ID = ?", conn);
            unitsSelectCmd.Parameters.Add(new OleDbParameter("@ID", queryStringID));
            OleDbCommand amountSelectCmd = new OleDbCommand("SELECT Amount FROM Chemicals WHERE ID = ?", conn);
            amountSelectCmd.Parameters.Add(new OleDbParameter("@ID", queryStringID));

            conn.Open();
            OleDbDataReader drChecksum = checksumSelectCmd.ExecuteReader();
            OleDbDataReader drUnits = unitsSelectCmd.ExecuteReader();
            OleDbDataReader drAmount = amountSelectCmd.ExecuteReader();
            while (drChecksum.Read())
                databaseChecksum = drChecksum["Checksum"].ToString();
            while (drUnits.Read())
                databaseUnits = drUnits["Units"].ToString();
            while (drAmount.Read())
                databaseAmount = Convert.ToDouble(drAmount["Amount"]);
            conn.Close();
            drChecksum.Close();
            drUnits.Close();
            drAmount.Close();

            if (this.ddlTeacherName.SelectedItem.Text.Equals("Select Your Name"))
                nameExists = false;
            if (this.txtAmount.Text == "")
                amountExists = false;
            if (this.txtChecksum.Text == "")
                checksumExists = false;

            if (!this.ddlUnits.SelectedItem.Value.Equals(databaseUnits))
                unitsMatch = false;
            if (!double.TryParse(this.txtAmount.Text, out fieldAmount))
                amountIsNumber = false;
            if (amountIsNumber)
                fieldAmountNewUnits = fieldAmount;
            if (amountExists && amountIsNumber && !unitsMatch)
            {
                if (databaseUnits.Equals("mg") && fieldUnits.Equals("g"))
                    fieldAmountNewUnits = fieldAmount * 1000;
                else if (databaseUnits.Equals("mg") && fieldUnits.Equals("kg"))
                    fieldAmountNewUnits = fieldAmount * 1000000;
                else if (databaseUnits.Equals("mg") && fieldUnits.Equals("oz"))
                    fieldAmountNewUnits = fieldAmount * 28349.5;
                else if (databaseUnits.Equals("g") && fieldUnits.Equals("mg"))
                    fieldAmountNewUnits = fieldAmount / 1000;
                else if (databaseUnits.Equals("g") && fieldUnits.Equals("kg"))
                    fieldAmountNewUnits = fieldAmount * 1000;
                else if (databaseUnits.Equals("g") && fieldUnits.Equals("oz"))
                    fieldAmountNewUnits = fieldAmount * 28.3495;
                else if (databaseUnits.Equals("kg") && fieldUnits.Equals("mg"))
                    fieldAmountNewUnits = fieldAmount / 1000000;
                else if (databaseUnits.Equals("kg") && fieldUnits.Equals("g"))
                    fieldAmountNewUnits = fieldAmount / 1000;
                else if (databaseUnits.Equals("kg") && fieldUnits.Equals("oz"))
                    fieldAmountNewUnits = fieldAmount * .0283595;
                else if (databaseUnits.Equals("oz") && fieldUnits.Equals("mg"))
                    fieldAmountNewUnits = fieldAmount / 28359.5;
                else if (databaseUnits.Equals("oz") && fieldUnits.Equals("g"))
                    fieldAmountNewUnits = fieldAmount / 28.3595;
                else if (databaseUnits.Equals("oz") && fieldUnits.Equals("kg"))
                    fieldAmountNewUnits = fieldAmount / .0283595;

                else if (databaseUnits.Equals("L") && fieldUnits.Equals("mL"))
                    fieldAmountNewUnits = fieldAmount / 1000;
                else if (databaseUnits.Equals("mL") && fieldUnits.Equals("L"))
                    fieldAmountNewUnits = fieldAmount * 1000;

                else
                    unitsConvert = false;
            }
            if (amountExists && amountIsNumber && (databaseAmount - fieldAmountNewUnits < 0 || fieldAmountNewUnits <= 0))
                amountIsInBounds = false;
            if (!txtChecksum.Text.Equals(databaseChecksum, StringComparison.InvariantCultureIgnoreCase))
                checksumMatches = false;

            string errorStringNameNotChosen = "You must select your name";
            string errorStringAmountNotEntered = "You must enter an amount";
            string errorStringChecksumNotEntered = "You must enter a checksum";
            string errorStringAmountNotANumber = "The amount must be a valid number";
            string errorStringAmountOutOfBounds = "The amount entered was out of bounds";
            string errorStringChecksumNotValid = "The checksum did not match the records";
            string errorStringUnitsNotCompatible = "The chemical units entered are not valid";

            this.lblErrors.Text = "* Errors with your request: <br />";
            if (nameExists == false)
                this.lblErrors.Text = this.lblErrors.Text + errorStringNameNotChosen + "<br />";
            if (amountExists == false)
                this.lblErrors.Text = this.lblErrors.Text + errorStringAmountNotEntered + "<br />";
            if (checksumExists == false)
                this.lblErrors.Text = this.lblErrors.Text + errorStringChecksumNotEntered + "<br />";
            if (amountExists == true && amountIsNumber == false)
                this.lblErrors.Text = this.lblErrors.Text + errorStringAmountNotANumber + "<br />";
            if (amountExists == true && amountIsNumber == true && amountIsInBounds == false)
                this.lblErrors.Text = this.lblErrors.Text + errorStringAmountOutOfBounds + "<br />";
            if (checksumExists == true && checksumMatches == false)
                this.lblErrors.Text = this.lblErrors.Text + errorStringChecksumNotValid + "<br />";
            if (unitsConvert == false)
                this.lblErrors.Text = this.lblErrors.Text + errorStringUnitsNotCompatible + "<br />";

            // Things that can be wrong on the Withdraw Page:
            // Teacher's name not selected
            // Amount not entered
            // Checksum not entered
            // Amount not a number
            // Amount too big
            // Checksum incorrect
            // Units don't convert

            this.lblErrors.Visible = false;
            if (nameExists && amountExists && amountIsNumber && amountIsInBounds &&
                unitsConvert && checksumExists && checksumMatches)
            {
                //int teacherID, string teacherName, int chemicalID, string chemicalName, 
                //double amountTakenDatabaseUnits, double amountTakenFieldUnits, double amountLeftDatabaseUnits, 
                //string databaseUnits, string fieldUnits, string checksum
                Transaction currentTransaction = new Transaction(
                    Convert.ToInt32(this.ddlTeacherName.SelectedItem.Value),
                    this.ddlTeacherName.SelectedItem.Text,
                    Convert.ToInt32(queryStringID),
                    this.lblChemicalName.Text,

                    fieldAmountNewUnits,
                    Math.Round(fieldAmount, 3), // This rounding will cause some slight errors
                    Math.Round(databaseAmount - fieldAmountNewUnits, 3),
                    databaseUnits,
                    fieldUnits,

                    this.txtChecksum.Text);
                Session["transaction"] = currentTransaction;
                Response.Redirect("Confirm.aspx");
            }
            else
                this.lblErrors.Visible = true;
        }
    }
}