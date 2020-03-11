<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucDrzava.ascx.cs" Inherits="BieEnWeb.UserControls.ucDrzava" %>
<%--LanguaScriptManager ge--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


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
                myButton.value = "OBRAĐUJE SE...";
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


 <div class="row">
        <div class="col-12" style="overflow-x:auto;">
            <div style="margin-left: auto; margin-right: auto; text-align: center;">
                <br /><br />
                <div class="row"> 
                <%--<div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-12">    tanjaovde--%>
               <div class="col-6" style="overflow-x:auto; align-content:center"> 
                <asp:Label ID="Label2" runat="server" Text="" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
                </div>
                 </div>
                <br />

               <div class="row">
<%--              <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-12" style="overflow-x:auto;">--%>
                   <%--<div class="col-6" style="overflow-x:auto;">--%>
              <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12" style="text-align:left">
                <asp:Label ID="lblUkupanBroj" runat="server" Text="" CssClass="float-md-right" Font-Bold="true" Font-Size="12px" Font-Names="Sans-serif"></asp:Label>
             </div>
              </div>
            </div>
          <%--  CssClass="auto-style72"--%>

       
             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                   <%-- <asp:Panel ID="pa" runat="server" Width="100%">--%>
                        <div class="container">
                            <div class="row">
                                <div class="col-12" style="font-weight: bold; font-style: italic; text-decoration: underline; background-color: #dacef2;">
                                    <p>Država</p>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-12" style="overflow-x:auto;">


    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False"  DataKeyNames="IdDrzava" EmptyDataText="Nema podataka za prikaz" 
         EnableViewState="True"  ShowFooter="false" Width="90%" PageSize="5"  OnPageIndexChanging="gv_PageIndexChanging" AllowSorting="True"  AllowPaging="true" 
        OnSelectedIndexChanged="gv_SelectedIndexChanged"  >   <%--OnRowDataBound="gv_RowDataBound"--%>
    <Columns>

        <asp:TemplateField HeaderStyle-BorderColor="White" headerstyle-width="5%" HeaderStyle-BorderWidth="0px" HeaderStyle-CssClass="poravnavanjeTekstaCentar vertikalnoCentriranTekst" 
            HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="8" HeaderStyle-Wrap="true" HeaderText="" ItemStyle-BorderColor="White" ItemStyle-BorderWidth="0px" 
            ItemStyle-CssClass="poravnavanjeTekstaCentar vertikalnoCentriranTekst" ShowHeader="false"  >   <%--HeaderStyle-BackColor="White"--%>
            <ItemTemplate>
                <asp:Button ID="btnObrisi" runat="server" CssClass="dugme100" OnClick="btnObrisi_Click" OnClientClick="if(!confirm('Sigurni ste?')) return false;" 
                    Text="Obriši" UseSubmitBehavior="false" width="100%"/>
            </ItemTemplate>
            <HeaderStyle BorderColor="White" BorderWidth="0px" CssClass="poravnavanjeTekstaCentar vertikalnoCentriranTekst" Font-Bold="True" Font-Size="8pt" Width="5%" Wrap="True" />
            <ItemStyle BorderColor="White" BorderWidth="0px" CssClass="poravnavanjeTekstaCentar vertikalnoCentriranTekst" />
        </asp:TemplateField>

        <asp:TemplateField FooterStyle-BackColor="White" headerstyle-width="5%" FooterStyle-BorderStyle="None" HeaderStyle-BorderColor="Black" HeaderStyle-BorderWidth="1px"
            HeaderStyle-CssClass="poravnavanjeTekstaCentar vertikalnoCentriranTekst" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="9" HeaderStyle-Wrap="true" 
            HeaderText="Id drzava" ItemStyle-BorderColor="Black" ItemStyle-BorderWidth="1px" ItemStyle-CssClass="poravnavanjeTekstaDesno vertikalnoCentriranTekst"
            ItemStyle-Font-Bold="false" ItemStyle-Font-Size="10" ShowHeader="true" Visible="true" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <asp:Label ID="lblIdDrzava" runat="server" Text='<%# Eval("idDrzava") %>'></asp:Label>
            </ItemTemplate>
            <FooterStyle BackColor="White" BorderStyle="None" />
            <HeaderStyle BorderColor="Black" BorderWidth="1px" CssClass="poravnavanjeTekstaCentar vertikalnoCentriranTekst" Font-Bold="True" Font-Size="9pt" Width="5%" Wrap="True" />
            <ItemStyle BorderColor="Black" BorderWidth="1px" CssClass="poravnavanjeTekstaDesno vertikalnoCentriranTekst" Font-Bold="False" Font-Size="10pt" HorizontalAlign="Center" />
        </asp:TemplateField>

        <asp:TemplateField FooterStyle-BackColor="White" headerstyle-width="8%" FooterStyle-BorderStyle="None" HeaderStyle-BorderColor="Black" HeaderStyle-BorderWidth="1px" 
            HeaderStyle-CssClass="poravnavanjeTekstaCentar vertikalnoCentriranTekst" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="9" HeaderStyle-Wrap="true" 
            HeaderText="Naziv države" ItemStyle-BorderColor="Black" ItemStyle-BorderWidth="1px"  
            ItemStyle-Font-Bold="false" ItemStyle-Font-Size="10" ShowHeader="true" Visible="true" >  <%--CssClass="Indent poravnavanjeTekstaCentar vertikalnoCentriranTekst"--%>
            <ItemTemplate>
                <asp:TextBox ID="tbNazivDrzave"  runat="server"  style="text-align: center" width="99%" Text='<%# Eval("NazivDrzave") %>'  ></asp:TextBox>  <%--ReadOnly="true"--%>
            </ItemTemplate>


            <FooterStyle BackColor="White" BorderStyle="None" />
            <HeaderStyle BorderColor="Black" BorderWidth="1px" CssClass="poravnavanjeTekstaCentar vertikalnoCentriranTekst" Font-Bold="True" Font-Size="9pt" Width="8%" Wrap="True" />
            <ItemStyle BorderColor="Black" BorderWidth="1px" Font-Bold="False" Font-Size="10pt" />


        </asp:TemplateField>

        <asp:TemplateField HeaderStyle-BorderColor="White" headerstyle-width="5%" HeaderStyle-BorderWidth="0px" HeaderStyle-CssClass="poravnavanjeTekstaCentar vertikalnoCentriranTekst"
            HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="8" HeaderStyle-Wrap="true" HeaderText="" ItemStyle-BorderColor="White" ItemStyle-BorderWidth="0px"
            ItemStyle-CssClass="poravnavanjeTekstaCentar vertikalnoCentriranTekst" ShowHeader="false" >  <%--HeaderStyle-BackColor="White"--%>
            <ItemTemplate>
                <asp:Button ID="btnIzmeni" runat="server" CssClass="dugme100" OnClick="btnIzmeni_Click" OnClientClick="ClientSideClick(this)" 
                    Text="Izmeni" UseSubmitBehavior="false" width="100%"/>
            </ItemTemplate>
            <HeaderStyle BorderColor="White" BorderWidth="0px" CssClass="poravnavanjeTekstaCentar vertikalnoCentriranTekst" Font-Bold="True" Font-Size="8pt" Width="5%" Wrap="True" />
            <ItemStyle BorderColor="White" BorderWidth="0px" CssClass="poravnavanjeTekstaCentar vertikalnoCentriranTekst" />
        </asp:TemplateField>

        <asp:TemplateField HeaderStyle-BorderColor="White" headerstyle-width="5%" HeaderStyle-BorderWidth="0px" HeaderStyle-CssClass="poravnavanjeTekstaCentar vertikalnoCentriranTekst" 
            HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="8" HeaderStyle-Wrap="true" HeaderText="" ItemStyle-BorderColor="White" ItemStyle-BorderWidth="0px" 
            ItemStyle-CssClass="poravnavanjeTekstaCentar vertikalnoCentriranTekst" ShowHeader="false" >   <%--HeaderStyle-BackColor="White"--%>
            <ItemTemplate>
                <asp:Button ID="btnSacuvajIzmene" Enabled="false" runat="server" CssClass="dugme100" OnClick="btnSacuvajIzmene_Click" OnClientClick="ClientSideClick(this)" 
                    Text="Sačuvaj izmene" UseSubmitBehavior="false"  width="100%"/>
            </ItemTemplate>
            <HeaderStyle BorderColor="White" BorderWidth="0px" CssClass="poravnavanjeTekstaCentar vertikalnoCentriranTekst" Font-Bold="True" Font-Size="8pt" Width="5%" Wrap="True" />
            <ItemStyle BorderColor="White" BorderWidth="0px" CssClass="poravnavanjeTekstaCentar vertikalnoCentriranTekst" />
        </asp:TemplateField>

        <asp:TemplateField HeaderStyle-BorderColor="White" headerstyle-width="5%" HeaderStyle-BorderWidth="0px" HeaderStyle-CssClass="poravnavanjeTekstaCentar vertikalnoCentriranTekst"
            HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="8" HeaderStyle-Wrap="true" HeaderText="" ItemStyle-BorderColor="White" ItemStyle-BorderWidth="0px"
            ItemStyle-CssClass="poravnavanjeTekstaCentar vertikalnoCentriranTekst" ShowHeader="false" > <%--HeaderStyle-BackColor="White"--%>
            <ItemTemplate>
                <asp:Button ID="btnOtkaziIzmene" Enabled="false" runat="server" CssClass="dugme100" OnClick="btnOtkaziIzmene_Click" OnClientClick="ClientSideClick(this)"
                    Text="Otkaži izmene" UseSubmitBehavior="false" width="100%"/>
            </ItemTemplate>
            <HeaderStyle BorderColor="White" BorderWidth="0px" CssClass="poravnavanjeTekstaCentar vertikalnoCentriranTekst" Font-Bold="True" Font-Size="8pt" Width="5%" Wrap="True" />
            <ItemStyle BorderColor="White" BorderWidth="0px" CssClass="poravnavanjeTekstaCentar vertikalnoCentriranTekst" />
        </asp:TemplateField>
    </Columns>
    <RowStyle BackColor="#dacef2" />  <%--  #EFF3FB--%>
    <EditRowStyle BackColor="#dacef2" />   <%--#2461BF--%>
    <SelectedRowStyle BackColor="#2461BF" Font-Bold="True" ForeColor="Black" />  <%--    #D1DDF1           #333333--%>
    <PagerStyle BackColor="White" HorizontalAlign="Left" />
    <HeaderStyle BackColor="#dacef2" Font-Bold="True" ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Middle" /> <%-- "#b3e6f9"  #EFF3FB--%>
    <FooterStyle BackColor="#509ee3" Font-Bold="True" ForeColor="Black" HorizontalAlign="Right" VerticalAlign="Top" />
    <AlternatingRowStyle BackColor="White" />
</asp:GridView>   

 </Columns>
           
   </div>
      </div>
   </div>
                  <%--  </asp:Panel>--%>
                </ContentTemplate>
            </asp:UpdatePanel>
                                    
        </div>
    </div>

<br />
<div class="row">
    <div class="col-12" style="overflow-x:auto;">
<asp:Button ID="btnUnesi" runat="server" CssClass="dugme200" Text="Унеси novu državu" Style="font-size: 14px;"
    OnClick="btnUnesi_onclick" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False"  /><%--  Visible="false"--%>
        </div>
    </div>
<br />
<div class="row">
    <div class="col-12" style="overflow-x:auto;">
  <asp:Panel ID="pnlUnos" runat="server">
       

        <div class="row">
            
            <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:Label ID="lbNaziv" CssClass="float-md-left" runat="server" Text="Naziv države" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
            </div>
            <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:TextBox ID="tbNaziv" CssClass="glowing-border" runat="server" MaxLength="50" Width="225px" Height="24px" OnTextChanged="tbNaziv_TextChanged"></asp:TextBox>
            </div>
             <div class="col-xl-3 col-lg-4 col-md-12 col-sm-12 col-12">
                <asp:Label ID="Label1" runat="server"  Text="(Unesite naziv države)" Font-Bold="False" Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:Label>
            </div>
                
        </div>
      
   
        <div class="row">
            <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">

            </div>
            <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:Button ID="btnSacuvaj" runat="server" CssClass="dugme150" Width="225px" Enabled="true" OnClick="btnSacuvaj_OnClick" OnClientClick="ClientSideClick(this)" 
                    Style="text-align: center; font-size: 14px;" Text="Sačuvaj" UseSubmitBehavior="false" />
            </div>
        </div>
    </asp:Panel>
   </div>
   </div>
<br />
<div class="row">

    


    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateDeleteButton="True" AutoGenerateEditButton="True" AutoGenerateSelectButton="True">
    </asp:GridView>

    


    </div>

