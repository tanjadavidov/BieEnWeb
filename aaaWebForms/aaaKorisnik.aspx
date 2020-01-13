<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="aaaKorisnik.aspx.cs" Inherits="BieEnWeb.aaaWebForms.aaaKorisnik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bioenergana: unos korisnika</title>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>
    <meta name="description" content="" />
    <meta name="author" content="" />

    <link rel="icon" href="~/Images/BioEnSymbol.jpg" />

    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.4.1/css/all.css"/>

    <!-- Bootstrap core CSS -->
    <link href="~/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />

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
                myButton.value = "SAČEKAJTE ...";
            }
            return true;
        }
    </script>

    <link rel="stylesheet" type="text/css" href="~/CSS/stilovi.css" />

    <link href="../../CSS/simple-sidebar.css" rel="stylesheet" />

    <style type="text/css">
        body {
            font-family: -apple-system,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,"Noto Sans",sans-serif,"Apple Color Emoji","Segoe UI Emoji","Segoe UI Symbol","Noto Color Emoji";
            font-size: 1rem;
            font-weight: 400;
            line-height: 1.5;
            color: #212529;
            text-align: left;
        }

    </style>

</head>
<body>
    <form id="form1" runat="server">


        <header id="top">
            <div class="row21">
              <div id="logo"><a href="https://hr.wikipedia.org/wiki/Elektrane_na_biomasu_i_otpad" target="_blank"><img src="../../Images/BioEnerganaLogo.jpg" /></a></div>
              <div id="naslov">
                  <asp:Label ID="Label1" runat="server" Text="BIOENERGANA"
                                        Font-Bold="True" ForeColor="black"></asp:Label>
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;       <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="black">Energetski efikasna farma</asp:Label>
              </div>
          
              <div id="tabela1" class="ml-auto">
                <div class="row">
                  <div class="text-center">
                      <asp:Label ID="lblKorisnik" runat="server" Visible="true" Font-Bold="True" ForeColor="black"></asp:Label>
                    <div style="padding-top: 10px;">
                        <asp:LinkButton ID="lbtnOdjava" CssClass="btn btn-danger" Width="150px" runat="server" Visible="true" Font-Bold="True" ForeColor="white" OnClick="lbtnOdjava_Click"><i class="fas fa-power-off"></i>Одјавите се</asp:LinkButton>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </header>


        <div class="d-flex" id="wrapper">
            <!-- Sidebar -->
            <!-- Page Content -->
            <div id="page-content-wrapper">

              <nav class="navbar navbar-expand-lg navbar-light bg-light border-bottom" >
                <div id="nazivStranice1">
                    <asp:Label ID="lblNaslov" runat="server" Font-Bold="True" ForeColor="black"></asp:Label>
                </div>
              </nav>

              <br />
              <br />
              <div class="container-fluid" >
              <!-- Ovde se unose Web Forme -->
                  <div class="container">
                      <div class="row">
                          <div class="col-md-4 col-sm-6 col-12">
                            
                        </div>
                        <div class="col-md-3 col-sm-6 col-12">
                            <asp:Button ID="btnNazad" runat="server" CssClass="dugme250"  OnClick="btnNazad_Click" OnClientClick="ClientSideClick(this)" Text="Nazad na aplikaciju" UseSubmitBehavior="false" Font-Size="12px" />
                        </div>
                      </div>

                      <br />

                     <div class="row">
                        <div class="col-md-4 col-sm-6 col-12">
                            <asp:Label ID="lblKorisnickoIme" CssClass="float-md-right" runat="server" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif">Korisničko ime</asp:Label>
                        </div>
                        <div class="col-md-3 col-sm-6 col-12">
                            <asp:TextBox ID="tbKorisnickoIme" CssClass="glowing-border" runat="server" Width="250px" Height="24px"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4 col-sm-6 col-12">
                            <asp:Label ID="lblIme" CssClass="float-md-right" runat="server" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif">Ime</asp:Label>
                        </div>
                        <div class="col-md-3 col-sm-6 col-12">
                            <asp:TextBox ID="tbIme" CssClass="glowing-border" runat="server" Width="250px" Height="24px"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4 col-sm-6 col-12">
                            <asp:Label ID="lblPrezime" CssClass="float-md-right" runat="server" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif">Prezime</asp:Label>
                        </div>
                        <div class="col-md-3 col-sm-6 col-12">
                            <asp:TextBox ID="tbPrezime" CssClass="glowing-border" runat="server" Width="250px" Height="24px"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4 col-sm-6 col-12">
                            <asp:Label ID="lblEposta" CssClass="float-md-right" runat="server" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif">E-pošta</asp:Label>
                        </div>
                        <div class="col-md-3 col-sm-6 col-12">
                            <asp:TextBox ID="tbEposta" CssClass="glowing-border" runat="server" Width="250px" Height="24px"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4 col-sm-6 col-12">
                            <asp:Label ID="lblTelefon" CssClass="float-md-right" runat="server" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif">Telefon</asp:Label>
                        </div>
                        <div class="col-md-3 col-sm-6 col-12">
                            <asp:TextBox ID="tbTelefon" CssClass="glowing-border" runat="server" Width="250px" Height="24px"></asp:TextBox>
                        </div>
                    </div>

                      <br />

                    <div class="row">
                        <div class="col-md-4 col-sm-6 col-12">
                            <asp:Label ID="lblLozinka_Stara" CssClass="float-md-right" runat="server" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif">Stara lozinka</asp:Label>
                        </div>
                        <div class="col-md-3 col-sm-6 col-12">
                            <asp:TextBox ID="tbLozinka_Stara" CssClass="glowing-border" runat="server" Width="250px" TextMode="Password" Height="24px"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4 col-sm-6 col-12">
                            <asp:Label ID="lblLozinka_Nova" CssClass="float-md-right" runat="server" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif">Nova lozinka</asp:Label>
                        </div>
                        <div class="col-md-3 col-sm-6 col-12">
                            <asp:TextBox ID="tbLozinka_Nova" CssClass="glowing-border" runat="server" Width="250px" TextMode="Password" Height="24px"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4 col-sm-6 col-12">
                            <asp:Label ID="lblLozinka_NovaPonovljeno" CssClass="float-md-right" runat="server" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif">Potvrdite novu lozinku</asp:Label>
                        </div>
                        <div class="col-md-3 col-sm-6 col-12">
                            <asp:TextBox ID="tbLozinka_NovaPonovljeno" CssClass="glowing-border" runat="server" Width="250px" TextMode="Password" Height="24px"></asp:TextBox>
                        </div>
                    </div>

                      <br />

                    <div class="row">
                        <div class="col-md-4 col-sm-6 col-12">
                            
                        </div>
                        <div class="col-md-3 col-sm-6 col-12">
                            <asp:Button ID="btnPromena" runat="server" CssClass="dugme250"  OnClick="btnPromena_Click" OnClientClick="ClientSideClick(this)" Text="Promeni podatke" UseSubmitBehavior="false" Font-Size="12px" />
                        </div>
                    </div>

                  </div>
              <!-- Kraj unosa Web Formi -->
              </div>
            </div>
            <!-- /#page-content-wrapper -->
        </div>

        <br />

        <!-- Footer -->
          <footer class="page-footer font-small bg-light fixed-bottom ">
            <!-- Copyright -->
            <div class="footer-copyright text-center">© <a href="https://hr.wikipedia.org/wiki/Elektrane_na_biomasu_i_otpad" target="_blank">Bio isplativa farma</a>, Novo orahovo, Bačka Topola
            </div>
            <!-- Copyright -->
          </footer>
          <!-- Footer -->

        
    </form>
</body>
</html>
