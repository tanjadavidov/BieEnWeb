<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucJediniceMere.ascx.cs" Inherits="BieEnWeb.UserControls.ucJediniceMere" %>

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
<br />


<div class="container">
  

    <asp:Panel ID="pnlUnos" runat="server">
       

        <div class="row">
            <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:Label ID="Label5" CssClass="float-md-left" runat="server" Text="Skraćeni naziv jedinice mere" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
            </div>
            <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:TextBox ID="tbSkracNazJedMere" CssClass="glowing-border" runat="server" MaxLength="50" Width="225px" Height="24px" OnTextChanged="tbSkracNazJedMere_TextChanged"></asp:TextBox>
            </div>
             <div class="col-xl-3 col-lg-4 col-md-12 col-sm-12 col-12">
                <asp:Label ID="Label1" runat="server" Text="(Unesite skraćeni naziv jedinice mere)" Font-Bold="False" Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:Label>
            </div>
        </div>

     <div class="row">
             <%-- <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:Label ID="idJedMere" CssClass="float-md-left" runat="server" Text="Id jedinice mere" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif" ></asp:Label>
            </div>
            <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:TextBox ID="tbIdJedMere" CssClass="glowing-border" runat="server" MaxLength="50" Width="225px" Height="24px" Enabled="false"></asp:TextBox>
            </div>--%>
            <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:Label ID="Label4" CssClass="float-md-left" runat="server" Text="Naziv jedinice mere" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
            </div>
            <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:TextBox ID="tbNazivJedMere" CssClass="glowing-border" runat="server" MaxLength="50" Width="225px" Height="24px" OnTextChanged="tbNazivJedMere_TextChanged"></asp:TextBox>
            </div>
            <div class="col-xl-3 col-lg-4 col-md-12 col-sm-12 col-12">
                <asp:Label ID="Label11" runat="server" Text="(Unesite naziv jedinice mere)" Font-Bold="False" Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:Label>
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

 <div class="row">
        <div class="col-12" style="overflow-x:auto;">
            <div style="margin-left: auto; margin-right: auto; text-align: center;">
                <br /><br />
                <div class="row"> 
                <%--<div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-12">--%>
                     <div class="col-6" style="overflow-x:auto;">
                <asp:Label ID="Label2" runat="server" Text="" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
                </div>
                 </div>
               <div class="row">
<%--              <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-12" style="overflow-x:auto;">--%>
                   <%--<div class="col-6" style="overflow-x:auto;">--%>
                       <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12" style="text-align:left">
                <asp:Label ID="lblUkupanBroj" runat="server" Text="" CssClass="float-md-right" Font-Bold="true" Font-Size="12px" Font-Names="Sans-serif"></asp:Label>
             </div>
              </div>
            </div>
          <%--  CssClass="auto-style72"--%>

             

                <asp:GridView ID="gvJedMere" runat="server" AutoGenerateColumns="False"  DataKeyNames="IdJedMere" EmptyDataText="Nema podataka za prikaz" 
         EnableViewState="True"  ShowFooter="false" Width="60%" PageSize="5"  OnPageIndexChanging="gvJedMere_PageIndexChanging" AllowSorting="True"  AllowPaging="true">
    <Columns>

        <asp:TemplateField HeaderStyle-BorderColor="White" headerstyle-width="5%" HeaderStyle-BorderWidth="0px" HeaderStyle-CssClass="poravnavanjeTekstaCentar vertikalnoCentriranTekst" 
            HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="8" HeaderStyle-Wrap="true" HeaderText="" ItemStyle-BorderColor="White" ItemStyle-BorderWidth="0px" 
            ItemStyle-CssClass="poravnavanjeTekstaCentar vertikalnoCentriranTekst" ShowHeader="false" >   <%--HeaderStyle-BackColor="White"--%>
            <ItemTemplate>
                <asp:Button ID="btnObrisi" runat="server" CssClass="dugme100" OnClick="btnObrisi_Click" OnClientClick="if(!confirm('Сигурни сте?')) return false;" Text="Обриши" UseSubmitBehavior="false" />
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField FooterStyle-BackColor="White" headerstyle-width="5%" FooterStyle-BorderStyle="None" HeaderStyle-BorderColor="Black" HeaderStyle-BorderWidth="1px"
            HeaderStyle-CssClass="poravnavanjeTekstaCentar vertikalnoCentriranTekst" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="9" HeaderStyle-Wrap="true" 
            HeaderText="Id jm" ItemStyle-BorderColor="Black" ItemStyle-BorderWidth="1px" ItemStyle-CssClass="poravnavanjeTekstaDesno vertikalnoCentriranTekst"
            ItemStyle-Font-Bold="false" ItemStyle-Font-Size="10" ShowHeader="true" Visible="true" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <asp:Label ID="lblIdJedMere" runat="server" Text='<%# Eval("idJedMere") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField FooterStyle-BackColor="White" headerstyle-width="8%" FooterStyle-BorderStyle="None" HeaderStyle-BorderColor="Black" HeaderStyle-BorderWidth="1px" 
            HeaderStyle-CssClass="poravnavanjeTekstaCentar vertikalnoCentriranTekst" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="9" HeaderStyle-Wrap="true" 
            HeaderText="jm skraćeno" ItemStyle-BorderColor="Black" ItemStyle-BorderWidth="1px"  
            ItemStyle-Font-Bold="false" ItemStyle-Font-Size="10" ShowHeader="true" Visible="true" >  <%--CssClass="Indent poravnavanjeTekstaCentar vertikalnoCentriranTekst"--%>
            <ItemTemplate>
                <asp:TextBox ID="tbSkracNazJedMere"  runat="server"  style="text-align: center" width="99%" Text='<%# Eval("SkracNazJedMere") %>'  ReadOnly="true"></asp:TextBox>  <%----%>
                <%--<cc1:MaskedEditExtender ID="tbCenaPozicije_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                    Enabled="True" TargetControlID="tbCenaPozicije" MaskType="Number" InputDirection="LeftToRight"
                    CultureName="sr-Latn-CS" Mask="999999,99" AutoComplete="false">
                </cc1:MaskedEditExtender>--%>
            </ItemTemplate>
        </asp:TemplateField>

         <asp:TemplateField FooterStyle-BackColor="White"  FooterStyle-BorderStyle="None" HeaderStyle-BorderColor="Black" HeaderStyle-BorderWidth="1px" HeaderStyle-CssClass="poravnavanjeTekstaCentar vertikalnoCentriranTekst" 
             HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="9" HeaderStyle-Wrap="false" HeaderText="&nbsp;&nbsp; Jedinica mere  &nbsp;&nbsp;"  ItemStyle-BorderColor="Black" ItemStyle-BorderWidth="1px" 
             ItemStyle-CssClass="poravnavanjeTekstaDesno vertikalnoCentriranTekst" ItemStyle-Font-Bold="false" ItemStyle-Font-Size="10" ShowHeader="true" Visible="true">
            <ItemTemplate>
                <asp:TextBox ID="tbNazJedMere"  runat="server" CssClass="Indent" Text='<%# Eval("NazJedMere") %>' Width="99%" ReadOnly="true"></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderStyle-BorderColor="White" headerstyle-width="5%" HeaderStyle-BorderWidth="0px" HeaderStyle-CssClass="poravnavanjeTekstaCentar vertikalnoCentriranTekst"
            HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="8" HeaderStyle-Wrap="true" HeaderText="" ItemStyle-BorderColor="White" ItemStyle-BorderWidth="0px"
            ItemStyle-CssClass="poravnavanjeTekstaCentar vertikalnoCentriranTekst" ShowHeader="false" >  <%--HeaderStyle-BackColor="White"--%>
            <ItemTemplate>
                <asp:Button ID="btnIzmeni" runat="server" CssClass="dugme100" OnClick="btnIzmeni_Click" OnClientClick="ClientSideClick(this)" Text="Izmeni" UseSubmitBehavior="false" />
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderStyle-BorderColor="White" headerstyle-width="5%" HeaderStyle-BorderWidth="0px" HeaderStyle-CssClass="poravnavanjeTekstaCentar vertikalnoCentriranTekst" 
            HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="8" HeaderStyle-Wrap="true" HeaderText="" ItemStyle-BorderColor="White" ItemStyle-BorderWidth="0px" 
            ItemStyle-CssClass="poravnavanjeTekstaCentar vertikalnoCentriranTekst" ShowHeader="false" >   <%--HeaderStyle-BackColor="White"--%>
            <ItemTemplate>
                <asp:Button ID="btnSacuvajIzmene" Enabled="false" runat="server" CssClass="dugme100" OnClick="btnSacuvajIzmene_Click" OnClientClick="ClientSideClick(this)" 
                    Text="Sačuvaj izmene" UseSubmitBehavior="false"  />
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderStyle-BorderColor="White" headerstyle-width="5%" HeaderStyle-BorderWidth="0px" HeaderStyle-CssClass="poravnavanjeTekstaCentar vertikalnoCentriranTekst"
            HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="8" HeaderStyle-Wrap="true" HeaderText="" ItemStyle-BorderColor="White" ItemStyle-BorderWidth="0px"
            ItemStyle-CssClass="poravnavanjeTekstaCentar vertikalnoCentriranTekst" ShowHeader="false" > <%--HeaderStyle-BackColor="White"--%>
            <ItemTemplate>
                <asp:Button ID="btnOtkaziIzmene" Enabled="false" runat="server" CssClass="dugme100" OnClick="btnOtkaziIzmene_Click" OnClientClick="ClientSideClick(this)"
                    Text="Otkaži izmene" UseSubmitBehavior="false" />
            </ItemTemplate>
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
               
        </div>
    </div>


    <div class="row">
        <div class="col-12" style="overflow-x:auto;">

                <%--  <asp:GridView ID="gvJedMere1" runat="server" HorizontalAlign="Center" AutoGenerateColumns="False" EmptyDataText="Nema jedinica mere" PageSize="5"  OnPageIndexChanging="gvJedMere_PageIndexChanging" 
                 Style="margin-top: 0px"  
                AllowSorting="True"  AllowPaging="true" Width=50% OnSelectedIndexChanged="gvJedMere_SelectedIndexChanged"   >  
                
                <Columns>
                    <asp:BoundField DataField="idJedMere" HeaderText="Id jedinice mere" HeaderStyle-Width="2%" SortExpression="idJedMere" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="true"    />
                    <asp:BoundField DataField="SkracNazJedMere" HeaderText="Skraćeni naziv jm" ReadOnly="True" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center" SortExpression="SkracNazJedMere" />
                    <asp:BoundField DataField="NazJedMere" HeaderText="Naziv jedinice mere" HeaderStyle-Width="10%" SortExpression="NazJedMere" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="true"/>                                   
                </Columns>
                <RowStyle BackColor="#EFF3FB" />
                <EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="#d6d6d6" ForeColor="#002079" Font-Size="Small" />
                <PagerStyle BackColor="White" HorizontalAlign="Left" />
                <HeaderStyle HorizontalAlign="Center" BackColor="#ae97db" ForeColor="black" Font-Size="Small" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>--%>
   
             
        </div>
    </div>
</div>