<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucUnosNovogKorisnika.ascx.cs" Inherits="BieEnWeb.UserControls.ucUnosNovogKorisnika" %>

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
                myButton.value = "ОБРАЂУЈЕ СЕ ...";
            }
            return true;
        }

</script>

<link href="../CSS/stilovi.css" rel="stylesheet" />
<link href="../CSS/simple-sidebar.css" rel="stylesheet" />

<style type="text/css">
    body {
        font-family: -apple-system,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,"Noto Sans",sans-serif,"Apple Color Emoji","Segoe UI Emoji","Segoe UI Symbol","Noto Color Emoji";
        font-size: 1rem;
        font-weight: 400;
        line-height: 1.5;
        color: #212529;
        text-align: left;
    }

    #OsnovniMenu a {
        color: white !important;
        font-weight: 600 !important;
        text-decoration: none;
        display: block;
        white-space: normal !important;
    }

    .list-group-flush:last-child .list-group-item:last-child {
        margin-bottom: unset !important;
        border-bottom: unset !important;
    }

</style>




<br />
<br />

<div class="container">
    <div class="row">
        <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">
            <asp:Label ID="Label2" CssClass="float-md-right" runat="server" Text="Korisničko ime" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
        </div>
        <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-12">
            <asp:TextBox ID="tbImePrezime" runat="server" CssClass="glowing-border" MaxLength="50" Width="225px" Height="24px"></asp:TextBox>
        </div>
        <div class="col-xl-3 col-lg-4 col-md-12 col-sm-12 col-12">
            <asp:Label ID="Label12" runat="server" Text="(Unesite ime.prezime)" Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:Label>
        </div>
        <div class="col-xl-2 col-lg-6 col-md-6 col-sm-6 col-6 razmak">
            <asp:Button ID="btnProveri" runat="server" OnClick="btnProveri_OnClick" CssClass="dugme150 dugmeMobilni150" OnClientClick="ClientSideClick(this)" Style="text-align: center; font-size: 14px;"
                    Text="Proveri" Enabled="true" UseSubmitBehavior="false"/>
        </div>
        <div class="col-xl-2 col-lg-6 col-md-6 col-sm-6 col-6 razmak">
            <asp:Button ID="btnOcisti" runat="server" CssClass="dugme150 dugmeMobilni150" Enabled="true" OnClick="btnOcisti_OnClick" OnClientClick="ClientSideClick(this)" Style="text-align: center; font-size: 14px;" Text="Očisti ekran" UseSubmitBehavior="false" />
        </div>
    </div>

    <asp:Panel ID="pnlUnos" runat="server">
        <div class="row">
            <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:Label ID="Label4" CssClass="float-md-right" runat="server" Text="Početna lozinka" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
            </div>
            <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:TextBox ID="tbPocetnaLozinka" CssClass="glowing-border" runat="server" MaxLength="50" Width="225px" Height="24px"></asp:TextBox>
            </div>
            <div class="col-xl-3 col-lg-4 col-md-12 col-sm-12 col-12">
                <asp:Label ID="Label11" runat="server" Text="(Unesite imegodina)" Font-Bold="False" Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:Label>
            </div>
        </div>

        <div class="row">
            <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:Label ID="Label5" CssClass="float-md-right" runat="server" Text="Ime korisnika" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
            </div>
            <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:TextBox ID="tbImeKorisnika" CssClass="glowing-border" runat="server" MaxLength="50" Width="225px" Height="24px"></asp:TextBox>
            </div>
        </div>

        <div class="row">
            <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:Label ID="Label6" CssClass="float-md-right" runat="server" Text="Prezime korisnika" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
            </div>
            <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:TextBox ID="tbPrezimeKorisnika" CssClass="glowing-border" runat="server" MaxLength="50" Width="225px" Height="24px"></asp:TextBox>
            </div>
        </div>

        <div class="row">
            <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:Label ID="Label7" CssClass="float-md-right" runat="server" Text="e Mail adresa" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
            </div>
            <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:TextBox ID="tbEPosta" CssClass="glowing-border" runat="server" MaxLength="150" Width="225px" Height="24px"></asp:TextBox>
            </div>
        </div>

        <div class="row">
            <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:Label ID="Label8" CssClass="float-md-right" runat="server" Text="Telefon" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
            </div>
            <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:TextBox ID="tbTelefon" CssClass="glowing-border" runat="server" MaxLength="50" Width="225px" Height="24px"></asp:TextBox>
            </div>
        </div>

        <div class="row">
            <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:Label ID="Label9" CssClass="float-md-right" runat="server" Text="Uloga" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
            </div>
            <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:DropDownList ID="ddlUloga" CssClass="glowing-border" runat="server" Width="225px" Height="24px" AppendDataBoundItems="false">
                </asp:DropDownList>
            </div>
        </div>

        <br />
        <div class="row">
            <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">

            </div>
            <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:Button ID="btnSacuvaj" runat="server" CssClass="dugme150" Width="225px" Enabled="true" OnClick="btnSacuvaj_OnClick" OnClientClick="ClientSideClick(this)" Style="text-align: center; font-size: 14px;" Text="Sačuvaj" UseSubmitBehavior="false" />
            </div>
        </div>
    </asp:Panel>
</div>

<asp:HiddenField ID="hdIme" runat="server" />
<asp:HiddenField ID="hdMail" runat="server" />
<asp:HiddenField ID="hdPrezime" runat="server" />
<asp:HiddenField ID="hdMailDodatni" runat="server" />


