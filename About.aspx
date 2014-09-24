<%@ Page Language="C#" AutoEventWireup="true" CodeFile="About.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>About</title>
    <link rel="stylesheet" href="styles/PrimaryStyleSheet.css" type="text/css" />
    <style type="text/css">
        .style1
        {
            margin-top: 5px;
            width: 779px;
        }
        .style2
        {
            width: 130px;
            text-align: right;
        }
        .style3
        {
            width: 111px;
        }
        .style4
        {
            width: 170px;
        }
        .style5
        {
            width: 130px;
            text-align: right;
            height: 21px;
        }
        .style6
        {
            width: 111px;
            height: 21px;
        }
        .style7
        {
            width: 170px;
            height: 21px;
        }
        .style8
        {
            height: 21px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="wrapper">
        <div id="header">
            <span id="header-text">PARK TUDOR CHEMICAL DATABASE</span>
            <a href="Home.aspx" id="right-item" class="navi-item">></a>
        </div>
        <div class="about-wrappers">
            <span><asp:Image ID="Image1" CssClass="about-images" runat="server" ImageUrl="images/question_mark.PNG" /><span class="about-text"><span class="about-text-header">About</span><br /><span class="about-text-body">This website is a secure gateway to the Park Tudor Chemical Database.  From this website, you can edit, add, and delete entries from the database as well as monitor chemical supplies 
            and recent transactions.  To withdraw a chemical, use the PTCD mobile web application by scanning a chemical label with a QR code scanner on your mobile device.  This site and the mobile PTCD site were developed by the Park Tudor Web Development Team (Jason Zhao, Chris Gregory, Michael Xu) 
            in the 2012 - 2013 school year.</span></span></span>
        </div>
        <div class="about-wrappers">
            <span><asp:Image ID="Image2" CssClass="about-images" runat="server" ImageUrl="images/search.PNG" /><span class="about-text"><span class="about-text-header">Help & Support</span><br /><span class="about-text-body">If you ever need help, you can always reference the Troubleshooting manual linked 
            below for tutorials, troubleshooting, and contact information.  Alternativly, you can also contact us for help at one of the numbers or email addresses listed below.
            <br />
            <span style="display: block; margin: 8px 0px 0px 10px;">
            Click <a class="link" href="documents/PTCD Troubleshooting Manual.docx">here</a> to access the PTCD Troubleshooting Manual for help and information. 
            <br />
            To view the PTCD release notes, click <a class="link" href="documents/PTCD Release Notes.txt">here</a></span></span></span></span>
        </div>
        <div class="about-wrappers">
            <span><asp:Image ID="Image3" CssClass="about-images" runat="server" ImageUrl="images/user.PNG" /><span class="about-text"><span class="about-text-header">Contact Us</span><br /><span class="about-text-body">If you have any questions or feedback, please do not hesitate to contact us at one of the following locations: 
            <!--Chris Gregory (LaMichael Xu (Labeling, MSDS Mailing Coordinator): (317) 431-5315 or mxu@parktudor.org</span>-->
            <br />
            </span>
            <table class="style1">
                <tr>
                    <td class="style5">
                        Jason Zhao:&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; </td>
                    <td class="style6">
                        (317) 412-3849</td>
                    <td class="style7">
                        jzhao@parktudor.org</td>
                    <td class="style8">
                        Team Leader and Desktop Site Coordinator</td>
                </tr>
                <tr>
                    <td class="style2">
                        Michael Xu:&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; </td>
                    <td class="style3">
                        (317) 431-5315</td>
                    <td class="style4">
                        mxu@parktudor.org</td>
                    <td>
                        Database Security and MSDS Notification Coordinator</td>
                </tr>
                <tr>
                    <td class="style2">
                        Chris Gregory:&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; </td>
                    <td class="style3">
                        (317) 644-9573</td>
                    <td class="style4">
                        cgregory@parktudor.org</td>
                    <td>
                        Chemical Labeling and Mobile Site Coordinator</td>
                </tr>
            </table>
            </span></span>
        </div>
    </div>
    </form>
</body>
</html>
