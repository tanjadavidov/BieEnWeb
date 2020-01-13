<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="aaaPrijava.aspx.cs" Inherits="BieEnWeb.aaaWebForms.aaaPrijava" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta http-equiv="X-UA-Compatible" content="ie=edge" />
    <link rel="icon" href="~/Images/BioEnSymbol.jpg" />
    <title>BioEnergana: prijava</title>

    <script type="text/javascript">
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
                myButton.value = "Sačekajte ...";
            }
            return true;
        }
    </script>

    <link href="~/CSS/login-style.css" type="text/css" rel="stylesheet" />
    
</head>
<body>

    <div id="bg1" style="background-image: url('../../Images/BioEnPozadina.jpg'); background-size: cover; background-position:center center; background-repeat:no-repeat;"></div>

    <form id="form1" runat="server" defaultbutton="btnPrijava">

        <header id="top">
        
          <div id="logo"><a href="https://hr.wikipedia.org/wiki/Elektrane_na_biomasu_i_otpad" target="_blank"><img src="../../Images/BioEnerganaLogo.jpg" /></a></div>
          <div id="naslov">
            <h3 style="font-size:18px;">Eko isplativa farma</h3>
            <h4>BioEnergana</h4>

          </div>
           
        </header>

        <div>
            <asp:Label ID="lblNapomena" Visible="false" runat="server" Font-Bold="True" ForeColor="#006878">Da bi pristupili ovoj aplikacije morate biti prijavljeni</asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          <asp:DropDownList ID="ddlFunkcija" runat="server" AutoPostBack="False" Width="200px"></asp:DropDownList>

        </div>

        <div class="loginbox">
            <img src="../../Images/avatarPurple.png" class="avatar" />
            <h1>Пријава</h1>
            <div id="form2">
                <p>Korisničko ime</p>
                <asp:TextBox runat="server" ID="tbKorisnickoIme" type="text" name="" placeholder="Unesite korisničko ime"></asp:TextBox>
                
                <p>Lozinka</p>
                <asp:TextBox runat="server" ID="tbLozinka" TextMode="Password" type="password" name="" placeholder="Unesite lozinku"></asp:TextBox>
            
                <asp:Button ID="btnPrijava" type="submit" name="" CssClass="dugmeLogin" runat="server" Text="Prijavite se" OnClick="btnPrijava_Click" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="false" />
            </div>
        </div>


        <div>

        </div>
    </form>
</body>
</html>
