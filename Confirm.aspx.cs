using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Configuration;
using System.Net.Mail;
using System.IO;
using System.Text.RegularExpressions;

public partial class Confirm : System.Web.UI.Page
{
    OleDbConnection conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
    Transaction transaction;

    protected void Page_Load(object sender, EventArgs e)
    {
        transaction = (Transaction)Session["transaction"];
        if (transaction != null)
        {
            OleDbCommand teacherSelectCmd = new OleDbCommand("SELECT First_Name, Last_Name FROM Teachers WHERE ID = ?", conn);
            teacherSelectCmd.Parameters.Add(new OleDbParameter("@ID", transaction.teacherID));
            string fullTeacherName = "";
            conn.Open();
            OleDbDataReader drTeachers = teacherSelectCmd.ExecuteReader();
            while (drTeachers.Read())
                fullTeacherName = drTeachers["First_Name"] + " " + drTeachers["Last_Name"];
            conn.Close();
            drTeachers.Close();

            this.lblTeacherName.Text = fullTeacherName;
            this.lblChemicalName.Text = transaction.chemicalName;
            this.lblAmount.Text = transaction.amountTakenFieldUnits.ToString();
            this.lblUnits.Text = transaction.fieldUnits;
        }
    }

    protected void btnGoBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Withdraw.aspx?id=" + transaction.chemicalID);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        OleDbCommand chemicalsUpdateCmd = new OleDbCommand("UPDATE Chemicals SET Amount = ?, Last_Withdraw_Date = ?, Last_Teacher_ID = ? WHERE ID = ?", conn);
        chemicalsUpdateCmd.Parameters.Add(new OleDbParameter("@Amount", transaction.amountLeftDatabaseUnits));
        chemicalsUpdateCmd.Parameters.Add(new OleDbParameter("@Last_Withdraw_Date", DateTime.Now.ToString()));
        chemicalsUpdateCmd.Parameters.Add(new OleDbParameter("@Last_Teacher_ID", transaction.teacherID));
        chemicalsUpdateCmd.Parameters.Add(new OleDbParameter("@ID", transaction.chemicalID));

        OleDbCommand transactionsAddCmd = new OleDbCommand("INSERT INTO Transactions (Transaction_Date, Chemical_Name, Amount_Taken, Units, Amount_Left, Teacher_Name, Chemical_ID) VALUES (?, ?, ?, ?, ?, ?, ?)", conn);
        transactionsAddCmd.Parameters.Add(new OleDbParameter("@Transaction_Date", DateTime.Now.ToString()));
        transactionsAddCmd.Parameters.Add(new OleDbParameter("@Chemical_Name", transaction.chemicalName));
        transactionsAddCmd.Parameters.Add(new OleDbParameter("@Amount_Taken", transaction.amountTakenDatabaseUnits));
        transactionsAddCmd.Parameters.Add(new OleDbParameter("@Units", transaction.databaseUnits));
        transactionsAddCmd.Parameters.Add(new OleDbParameter("@Amount_Left", transaction.amountLeftDatabaseUnits));
        transactionsAddCmd.Parameters.Add(new OleDbParameter("@Teacher_Name", transaction.teacherName));
        transactionsAddCmd.Parameters.Add(new OleDbParameter("@Chemical_ID", transaction.chemicalID));

        OleDbCommand teacherEmailCmd = new OleDbCommand("SELECT Email FROM Teachers WHERE ID = ?", conn);
        teacherEmailCmd.Parameters.Add(new OleDbParameter("@ID", transaction.teacherID));
        string teacherEmail = "";

        conn.Open();
        chemicalsUpdateCmd.ExecuteNonQuery();
        transactionsAddCmd.ExecuteNonQuery();

        OleDbDataReader drTeacherEmail = teacherEmailCmd.ExecuteReader();
        while (drTeacherEmail.Read())
            teacherEmail = (string)drTeacherEmail["Email"];
        drTeacherEmail.Close();
        conn.Close();

        sendNotification(transaction.teacherName, teacherEmail, transaction.chemicalName, transaction.amountTakenFieldUnits, transaction.fieldUnits);
        
        Session["transaction"] = null;
        Response.Redirect("Complete.aspx");
    }

    protected void sendNotification(string teacherName, string teacherEmail, string chemicalName, double chemicalAmount, string chemicalUnits)
    {
        int numHits = 0;
        string bestChemicalName = "";
        string infoPageLink = getInfoPageLocation(chemicalName, out bestChemicalName, out numHits);
        string senderName = "PTCD Mailer";
        string messageSubject = "PTCD MSDS: " + chemicalName;
        string messageBody = "You are receiving this email because you withdrew <strong>" + chemicalAmount + chemicalUnits + "</strong> of <strong>" + chemicalName + "</strong> from storage." + 
            "<br /> If this is incorrect please contact the current PT science department chair";
        Response.Write(infoPageLink);

        if (numHits > 0)
            messageBody += "<br/ ><br /> The MSDS page that best matches the chemical you took is for \"" + bestChemicalName + "\"" +
                "<br/ > The PDF for the MSDS page can be accessed <a href='" + infoPageLink + "'> here </a>";

        MailMessage message = new MailMessage();
        SmtpClient smtpServer = new SmtpClient();

        message.Subject = messageSubject;
        message.From = new MailAddress("ptcdmailer@gmail.com", senderName);
        message.To.Add(teacherEmail);
        message.Body = messageBody;
        message.IsBodyHtml = true;

        smtpServer.EnableSsl = true;

        try
        {
            smtpServer.Send(message);
            // EMAIL SENT
        }
        catch (Exception ex)
        {
            // EMAIL DID NOT SEND
        }
    }

    protected string getInfoPageLocation(string chemicalName, out string foundChemicalName, out int numMatches)
    {
        string chemicalNameSimplified = new Regex("[^a-zA-Z0-9 -]").Replace(chemicalName, "");
        string[] chemicalWords = chemicalNameSimplified.Split(' ');

        string path = Server.MapPath("msds/");
        string[] msdsFilePaths = Directory.GetFiles(path);

        int bestHitCount = 0;
        string bestFilePath = "";
        string bestChemicalName = "";
        foreach (string filePath in msdsFilePaths)
        {
            string wholeFileName = Path.GetFileName(filePath);
            string simplifiedFileName = new Regex("[^a-zA-Z0-9 -]").Replace(wholeFileName, "");
            string[] fileWords = simplifiedFileName.Split(' ');
            int hitCount = 0;

            foreach(string f in fileWords)
                foreach (string c in chemicalWords)
                    if(f.Equals(c, StringComparison.InvariantCultureIgnoreCase))
                        hitCount++;

            if(hitCount > bestHitCount)
            {
                bestFilePath = wholeFileName;
                bestHitCount = hitCount;
                bestChemicalName = wholeFileName.Substring(0, wholeFileName.Length - 4);
            }
            hitCount = 0;
        }

        numMatches = bestHitCount;
        if(bestHitCount > 0)
            bestFilePath = Uri.EscapeDataString(bestFilePath);
        foundChemicalName = bestChemicalName;
        string pagePathPrefix = "cs.parktudor.org/projects/ptcd/msds/";
        return pagePathPrefix + bestFilePath;
    }
}