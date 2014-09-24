<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Labeling.aspx.cs" Inherits="Labeling" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PTCD - Labeling</title>
    <link href='http://fonts.googleapis.com/css?family=Marvel' rel='stylesheet' type='text/css' />
    <link href="styles/LabelingTheme.css" rel="stylesheet" type="text/css" />
    <link href="styles/PrimaryStyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="scripts/jquery-1.9.1.js"></script>
    <script src="scripts/jquery-ui-1.10.3.custom.js"></script>
    <script>
        $(function () {
            $("#accordion").accordion({
                heightStyle: "content",
                event: "click hoverintent"
            });
        });

        $.event.special.hoverintent = {
            setup: function () {
                $(this).bind("mouseover", jQuery.event.special.hoverintent.handler);
            },
            teardown: function () {
                $(this).unbind("mouseover", jQuery.event.special.hoverintent.handler);
            },
            handler: function (event) {
                var currentX, currentY, timeout,
        args = arguments,
        target = $(event.target),
        previousX = event.pageX,
        previousY = event.pageY;

                function track(event) {
                    currentX = event.pageX;
                    currentY = event.pageY;
                };

                function clear() {
                    target
          .unbind("mousemove", track)
          .unbind("mouseout", clear);
                    clearTimeout(timeout);
                }

                function handler() {
                    var prop,
          orig = event;

                    if ((Math.abs(previousX - currentX) +
            Math.abs(previousY - currentY)) < 7) {
                        clear();

                        event = $.Event("hoverintent");
                        for (prop in orig) {
                            if (!(prop in event)) {
                                event[prop] = orig[prop];
                            }
                        }
                        // Prevent accessing the original event since the new event
                        // is fired asynchronously and the old event is no longer
                        // usable (#6028)
                        delete event.originalEvent;

                        target.trigger(event);
                    } else {
                        previousX = currentX;
                        previousY = currentY;
                        timeout = setTimeout(handler, 100);
                    }
                }

                timeout = setTimeout(handler, 100);
                target.bind({
                    mousemove: track,
                    mouseout: clear
                });
            }
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="wrapper">
        <div id="header" style="width: 800px;">
            <span id="header-text">PARK TUDOR CHEMICAL DATABASE</span>
        </div>
        <div id="form-header">
            Labeling</div>
        <div id="accordion">
            <h3 class="labeling-accordion-header">
                Before clicking go...</h3>
            <div class="instructionsDiv">
                <strong class="labeling-strong">Instructions:</strong>
                <br />
                <ul class="labeling-tables">
                    <li>Open in Chrome on Windows</li>
                    <li>Enter IDs for all labels</li>
                    <li>Separate IDs with commas and hyphens</li>
                </ul>
            </div>
            <h3 class="labeling-accordion-header">
                After clicking go...</h3>
            <div class="instructionsDiv">
                <strong class="labeling-strong">Instructions:</strong><br />
                <ol class="labeling-tables">
                    <li>Make sure all QR code images loaded</li>
                    <li>Print the web page with labels</li>
                    <li>Set all margins on the pages to zero</li>
                    <li>Turn off header and footer options</li>
                    <li>Print with label paper</li>
                </ol>
            </div>
        </div>

        <div id="idInputTableDiv">
            <table id="idInputTable">
                <tr>
                    <td style="padding: 2px; font-size: 16px; height: 33px;">
                        &nbsp; &nbsp; &nbsp; <strong>IDs to print:</strong>
                    </td>
                    <td style="padding: 2px; font-size: 16px; height: 33px;">
                        <asp:TextBox class="idTextField" ID="txtIDs" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="idInputTD" style="text-align: center;">
                        <asp:Button ID="btnSelect" runat="server" Text="Go" OnClick="btnSelect_Click" Height="25px"
                            Width="100px" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" Height="25px"
                            Width="100px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="idInputTD">
                        <asp:Label ID="lblErrors" runat="server" Text="*" Visible="False"></asp:Label>
                    </td>
                </tr>
            </table>
            <div id="missing-ID-container"><asp:CheckBox ID="chkMissingIDs" runat="server" /> Do not skip missing IDs</div>
        </div>
    </div>
    </form>
</body>
</html>
