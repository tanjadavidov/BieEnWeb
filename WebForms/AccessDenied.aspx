<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccessDenied.aspx.cs" Inherits="BieEnWeb.WebForms.AccessDenied" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Nemate pravo pristupa</title>

    <script type ="text/javascript">
        function ClientSideClick(myButton) {
            //Client side validation
            if (typeof (Page_ClientValidate) == 'function') {
                if (Page_ClientValidate() == false)
                { return false; }
            }

            //make sure the button is not of type "submit" but "button"
            if (myButton.getAttribute('type') == 'button') {
                //disable the button
                myButton.disabled = true;
                myButton.value = "Obrađuje se ...";
            }
            return true;
        }
    </script>

    <link rel="stylesheet" type="text/css" href="~/CSS/stilovi.css" />

    <style type="text/css">


        .auto-style6 {
            height: 58px;
        }
        .auto-style7 {
            width: 844px;
        }
    </style>
</head>
<body>
    <form id="form2" runat="server">
            <table style="text-align:center; width:100%">
                <tr>
                    <td class="auto-style6">&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td><asp:Label runat="server" ID="lblNemataPravoPristupa" Font-Bold="True" ForeColor="#C3010D">НЕМАТE ПРАВО ПРИСТУПА ОВОЈ СТРАНИ!!!</asp:Label></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
        </table>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <div style="text-align:center; width:100%">
            <%--<asp:Button ID="btnNazad" runat="server" CssClass="dugme200"  OnClick="btnNazad_Click" OnClientClick="ClientSideClick(this)" Text="НАЗАД НА АПЛИКАЦИЈУ" UseSubmitBehavior="false" Visible="false"/>--%>
        </div>
    </form>
</body>
</html>