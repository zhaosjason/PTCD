using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Configuration;

public partial class LabelPrint : System.Web.UI.Page
{
    OleDbConnection conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
    string siteURL = System.Configuration.ConfigurationManager.AppSettings["SiteURL"].ToString();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        string listString = Request.QueryString["ids"];
        string[] idStrArr = listString.Split(',');
        int[] idIntArr = new int[idStrArr.Length];
        for (int i = 0; i < idIntArr.Length; i++)
            idIntArr[i] = Convert.ToInt32(idStrArr[i]);

        string labelInfoSelectCmdStr = "SELECT ID, Chemical_Name, Checksum FROM Chemicals WHERE ";
        for (int i = 0; i < idIntArr.Length; i++)
            labelInfoSelectCmdStr += ("ID = " + idIntArr[i] + " OR ");
        labelInfoSelectCmdStr = labelInfoSelectCmdStr.Substring(0, labelInfoSelectCmdStr.Length - 4);
        OleDbCommand labelInfoSelectCmd = new OleDbCommand(labelInfoSelectCmdStr, conn);

        conn.Open();
        OleDbDataReader drLabelInfo = labelInfoSelectCmd.ExecuteReader();

        int cellCount = 0;
        int cellsPerRow = 3;
        int cellsPerTable = 30;

        LitCtrl.Text += ("<table class='PrintTable'>");
        LitCtrl.Text += ("<tr class='PrintTableRow'>");
        while (drLabelInfo.Read())
        {
            int dotSize = 5;
            string url = siteURL + "withdraw.aspx?id=" + drLabelInfo["ID"];
            LitCtrl.Text += ("<td>");
            LitCtrl.Text += ("<span class='QRDiv'><img class='QRImg' src='http://" + "qrfree.kaywa.com/?l=0&s=" + dotSize + "&d=" + url + "'></span>");
            LitCtrl.Text += ("<div class='infoDiv'>" + drLabelInfo["Chemical_Name"] + "<br />Checksum: " + drLabelInfo["Checksum"] + "</div>");
            LitCtrl.Text += ("<div class='idDiv'>" + drLabelInfo["ID"] + "</div>");
            LitCtrl.Text += ("</td>");

            cellCount++;
            if (cellCount % cellsPerRow == 0)
            {
                LitCtrl.Text += ("</tr>");
                LitCtrl.Text += ("<tr class='PrintTableRow'>");
            }
            if (cellCount % cellsPerTable == 0)
            {
                LitCtrl.Text += ("</table>");
                LitCtrl.Text += ("\n\n\n\n<table class='PrintTable'>");
            }
        }

        if (cellCount == 1)
            LitCtrl.Text += ("<td></td><td></td>");
        if(cellCount == 2)
            LitCtrl.Text += ("<td></td>");
        LitCtrl.Text += ("</tr>");
        LitCtrl.Text += ("</table>");

        conn.Close();
        drLabelInfo.Close();
    }

    //If the QR codes are not printing properly here are some alternative source websites for printing QR images
    //http://api.qrserver.com/v1/create-qr-code/?data=ChrisOffline.com&size=250x250
    //http://beqrious.com/generator.php?type=web-url&color=5f55f5&web-url%5Burl%5D=ChrisOffline.com&file_size=S
    //http://www.mobile-barcodes.com/qr-code-generator/generator.php?str=ChrisOffline.com&barcode=url
}