<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucMesto.ascx.cs" Inherits="BieEnWeb.UserControls.ucMesto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<%--<link href="../css/stilovi.css" rel="stylesheet" type="text/css" />--%>
<%--<link href="../css/default.css" rel="stylesheet" type="text/css" />--%>

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

    .desno {
        text-align: right;
    }

   
    
   
   
    .lista {
        min-width: 65px;
    }

    @media screen and (max-width: 1200px) and (min-width: 576px) {
        .razmak1 {
            padding-top: 16px;
        }
    }

    @media screen and (max-width: 576px) {
        .razmak2 {
            padding-top: 16px;
        }
    }
    .auto-style1 {
        background-color: #ae97db;
        border-radius: 6px;
        font-family: Arial;
        font-weight: bold;
        color: #000000;
        font-size: 11px;
        border: solid #66023c 1px;
    }
</style>

<asp:UpdatePanel ID="UpdatePanel8" runat="server">
    <ContentTemplate>
        <asp:Panel ID="pa" runat="server" Width="100%">

            <br />
            <br />

             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="paMesto" runat="server" Width="100%" Visible="true">
                        <div class="container">
                            <div class="row">
                                <div class="col-12" style="font-weight: bold; font-style: italic; text-decoration: underline; background-color: #dacef2;">
                                    <p>Prikaži mesta</p>
                                </div>
                            </div>
                             <div class="row">
                          

                                <div class="col-xl-2 col-md-6 col-sm-6 col-12">
                                    <asp:Label ID="lblStringR" runat="server" Font-Italic="True" ForeColor="Red" Width="100%"></asp:Label>
                                </div>
                            </div>
                            <div class="row">

                             <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12" style="text-align:left">
                                <asp:Label ID="lblUkupanBroj" runat="server" Text="Broj Redova" CssClass="float-md-right" Font-Bold="true" Font-Size="12px" Font-Names="Sans-serif"></asp:Label>
                              </div>
                             </div>

                            <div class="row">
                                <div class="col-12" style="overflow-x: auto;">
                                    
                                    <asp:GridView ID="gv" runat="server" HorizontalAlign="Center" AutoGenerateColumns="False" EmptyDataText="Nema podataka!" DataKeyNames="IdMesto"
                                        OnRowDataBound="gv_RowDataBound" Width="90%" Visible="true" PageSize="5"  OnPageIndexChanging="gv_PageIndexChanging" AllowPaging="true" >
                                        <Columns>

                                                   <asp:TemplateField HeaderText="IdOpstina" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIdOpstina" runat="server" Text='<%# Eval("IdOpstina") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                              <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" headerstyle-width="10%" >
                                                <ItemTemplate>
                                                      <asp:Button ID="btnObrisi" runat="server"   CssClass="dugme100" OnClick="btnObrisi_Click" OnClientClick="if(!confirm('Sigurni ste?')) return false;" 
                                                          Text="Obriši" UseSubmitBehavior="false" Width="99%" />
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                              <asp:TemplateField HeaderText="Opština" Visible="true" headerstyle-width="20%" >
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlOpstina" runat="server" Width="99%" Height="24px" CssClass="glowing-border" Enabled="false"></asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>  

                                             <asp:TemplateField HeaderText="Id mesta" Visible="false" >
                                                <ItemTemplate>
                                                        <asp:Label ID="lblIdMesto" runat="server" Text='<%# Eval("IdMesto") %>'></asp:Label>
<%--                                                       <asp:TextBox ID="tbIdMesto" runat="server" Text='<%# Eval("IdMesto") %>' Width="90%" Height="24px" CssClass="glowing-border"></asp:TextBox>                                                                                                       --%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Ptt Broj" Visible="true" headerstyle-width="10%"  >
                                                <ItemTemplate>
                                                   <%-- <asp:Label ID="lblPttBroj" runat="server" Text='<%# Eval("PttBroj") %>'></asp:Label>--%>
                                                    <asp:TextBox ID="tbPttBroj" runat="server" style="text-align: center" Text='<%# Eval("PttBroj") %>' Width="99%" Height="24px" CssClass="glowing-border" Enabled="false"></asp:TextBox> 
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Naziv mesta" headerstyle-width="20%" HeaderStyle-CssClass="poravnavanjeTekstaCentar vertikalnoCentriranTekst">
                                                <ItemTemplate>
                                                       <asp:TextBox ID="tbNazivMesta" runat="server" Text='<%# Eval("NazivMesta") %>' Width="99%" Height="24px" CssClass="glowing-border" Enabled="false"></asp:TextBox>                                                                                                       
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" headerstyle-width="10%">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnIzmeni" runat="server"  CssClass="dugme100" Width="99%" OnClick="btnIzmeni_Click" OnClientClick="ClientSideClick(this)" Text="Izmeni" UseSubmitBehavior="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center"  headerstyle-width="10%">
                                                <ItemTemplate>
                                                   <asp:Button ID="btnSacuvajIzmene" Enabled="false" runat="server" Width="99%"  CssClass="dugme100" OnClick="btnSacuvajIzmene_Click" OnClientClick="ClientSideClick(this)" Text="Sačuvaj izmene" UseSubmitBehavior="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center"  headerstyle-width="10%">
                                                <ItemTemplate>
                                                   <asp:Button ID="btnOtkaziIzmene" Enabled="false" runat="server" Width="99%"  CssClass="dugme100" OnClick="btnOtkaziIzmene_Click" OnClientClick="ClientSideClick(this)" Text="Otkaži izmene" UseSubmitBehavior="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <RowStyle BackColor="#EFF3FB" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#005094" />
                                        <PagerStyle BackColor="White" HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#dacef2" ForeColor="Black" Font-Size="Medium" HorizontalAlign="Center"/>
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>


            <br />
            <br />

    
            
  <div class="container">
    <div class="row">
    <div class="col-12" style="overflow-x:auto; font-style: italic ">
<asp:Button ID="Button2" runat="server" CssClass="dugme200" Text="Unos novog mesta" Style="font-size: 14px;"
    OnClick="btnUnesiNovoMesto_onclick" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False"  />
        </div>
    </div>
          </div>



              </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>

<br />
<br />

    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="PaUnosMesta1" runat="server" Width="100%" Visible="false" BorderStyle="Double">
                        <div class="container">
                            <div class="row">
                                <div class="col-12" style="font-weight: bold; font-style: italic; text-decoration: underline; background-color: #dacef2;">
                                    <p>Unesi mesto</p>
                                </div>
                            </div>

                             <div class="row "  >

                                <div class="col-xl-2 col-md-6 col-sm-6 col-12 d-flex">
                                    <asp:Label ID="lblOpstina" runat="server" Text="Opština:" CssClass="float-md-right " Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
                                </div>

                                <div class="col-xl-1 col-md-6 col-sm-6 col-12 d-flex">
                                    <asp:Label ID="lblPttBroj" runat="server" Text="Ptt broj:" CssClass="float-md-right " Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
                                </div>

                                <div class="col-xl-2 col-md-6 col-sm-6 col-12 d-flex">
                                    <asp:Label ID="lblNazivMesta" runat="server" Text="Naziv mesta:" CssClass="float-md-right " Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
                                </div>

                                <div class="col-xl-2 col-md-6 col-sm-6 col-12 ">
                                    <asp:Label ID="Label1" runat="server" Text="" CssClass="float-md-right " Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
                                </div>
                                  <div class="col-xl-1 col-md-6 col-sm-6 col-12 ">
                                    <asp:Label ID="Label3" runat="server" Text="" CssClass="float-md-right " Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
                                </div>
                            </div>
                            
                               <div class="row">

                                <div class="col-xl-2 col-md-6 col-sm-6 col-12">
                                    <asp:DropDownList ID="ddlOpstina" runat="server" Width="100%"  Height="24px" CssClass="glowing-border"></asp:DropDownList>

                                </div>

                                <div class="col-xl-1 col-md-6 col-sm-6 col-12">
                                  <asp:TextBox ID="tbPttBroj" runat="server"  Width="100%"  Height="24px" CssClass="glowing-border"></asp:TextBox> 
                                </div>

                                <div class="col-xl-3 col-md-6 col-sm-6 col-12 ">
                                   <asp:TextBox ID="tbNazivMesta" runat="server" width="100%"   Height="24px" CssClass="glowing-border"></asp:TextBox>          <%-- Text='<%# Eval("NazivMesta") %>'  --%>                                                                                          
                                </div>

                                <div class="col-xl-2 col-md-6 col-sm-6 col-12">
                                    <asp:Button ID="btnSacuvaj1" Enabled="true" runat="server" CssClass="dugme100" OnClick="btnSacuvaj1_Click" OnClientClick="ClientSideClick(this)" Text="Sačuvaj" UseSubmitBehavior="false" />
                                </div>
                                    <div class="col-xl-2 col-md-6 col-sm-6 col-12">
                                    <asp:Button ID="btnOcisti" Enabled="true" runat="server" CssClass="dugme100" OnClick="btnOcisti_Click" OnClientClick="ClientSideClick(this)" Text="Ponovi" UseSubmitBehavior="false" />
                                </div>
                                   <div class="col-xl-1 col-md-6 col-sm-6 col-12">
                                    <asp:Button ID="btnOdustani" Enabled="true" runat="server" CssClass="dugme100" OnClick="btnOdustani_Click" OnClientClick="ClientSideClick(this)" Text="Odustani" UseSubmitBehavior="false" />
                                </div>
                            </div>

                        
                        </div>
                   <br />
                <br />
          
  
                        
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>

<br />
<br />

        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="PaUnosMesta" runat="server" Width="100%" Visible="false">
                        <div class="container">
                            <div class="row">
                                <div class="col-12" style="font-weight: bold; font-style: italic; text-decoration: underline; background-color: #dacef2;">
                                    <p>Unesi mesto</p>
                                </div>
                            </div>
                            
                            <div class="row">
                                <div class="col-12" style="overflow-x: auto;">
                                    
                                    <asp:GridView ID="gvUnosMesta" runat="server" HorizontalAlign="Center" AutoGenerateColumns="false" EmptyDataText="Nema podataka!" DataKeyNames="IdMesto" OnRowDataBound="gvUnosMesta_RowDataBound"  Width="100%" > 
                                        <Columns>

                                                   <asp:TemplateField HeaderText="IdOpstina" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIdOpstina" runat="server" ></asp:Label>   <%--Text='<%# Eval("IdOpstina") %>'--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                              <asp:TemplateField HeaderText="Opština" >
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlOpstina" runat="server" Width="95%" Height="24px" CssClass="glowing-border"></asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>  

                                             <asp:TemplateField HeaderText="Id mesta">
                                                <ItemTemplate>
                                                   <asp:Label ID="lblIdMesto" runat="server" ></asp:Label>   <%--Text='<%# Eval("IdMesto") %>'--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Ptt Broj" >
                                                <ItemTemplate>
                                                     <asp:TextBox ID="tbPttBroj" runat="server"  Width="90%" Height="24px" CssClass="glowing-border"></asp:TextBox>   <%--Text='<%# Eval("PttBroj") %>'--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Naziv mesta">
                                                <ItemTemplate>
                                                       <asp:TextBox ID="tbNazivMesta" runat="server"  Width="90%" Height="24px" CssClass="glowing-border"></asp:TextBox>          <%-- Text='<%# Eval("NazivMesta") %>'  --%>                                                                                          
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                          <%--  <asp:TemplateField HeaderText="" >
                                                <ItemTemplate>
                                                      <asp:Button ID="btnObrisi" runat="server" CssClass="dugme100" OnClick="btnObrisi_Click" OnClientClick="if(!confirm('Sigurni ste?')) return false;" Text="Obriši" UseSubmitBehavior="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                          <%--  <asp:TemplateField HeaderText="" >
                                                <ItemTemplate>
                                                    <asp:Button ID="btnIzmeni" runat="server" CssClass="dugme100" OnClick="btnIzmeni_Click" OnClientClick="ClientSideClick(this)" Text="Izmeni" UseSubmitBehavior="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                            <asp:TemplateField HeaderText="" >
                                                <ItemTemplate>
                                                   <asp:Button ID="btnSacuvaj1" Enabled="false" runat="server" CssClass="dugme100" OnClick="btnSacuvaj1_Click" OnClientClick="ClientSideClick(this)" Text="Sačuvaj izmene" UseSubmitBehavior="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                         <%--   <asp:TemplateField HeaderText="" >
                                                <ItemTemplate>
                                                   <asp:Button ID="btnOtkaziIzmene" Enabled="false" runat="server" CssClass="dugme100" OnClick="btnOtkaziIzmene_Click" OnClientClick="ClientSideClick(this)" Text="Otkaži izmene" UseSubmitBehavior="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                        </Columns>
                                        <RowStyle BackColor="#EFF3FB" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#005094" />
                                        <PagerStyle BackColor="White" HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#dacef2" ForeColor="Black" Font-Size="Medium" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                   <br />
                <br />
             <br />
  
                        
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>





            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="PaU" runat="server" Width="100%" Visible="true">
                        <div class="container">
                            <div class="row">
                                <div class="col-12" style="font-weight: bold; font-style: italic; text-decoration: underline; background-color: #dacef2;">
                                    <p>Unesi mesta</p>
                                </div>
                            </div>
                             <div class="row">
                                <div class="col-xl-2 col-md-6 col-sm-6 col-12">
                                    <asp:Label ID="Label2" runat="server" Text="Zadaj broj mesta za unos:" CssClass="float-md-right desno" Font-Bold="true" Font-Size="12px" Font-Names="Sans-serif"></asp:Label>

                                </div>

                                <div class="col-xl-3 col-md-6 col-sm-6 col-12">
                                    <asp:TextBox ID="tbBrojMestaU" runat="server" CssClass="glowing-border" Width="100%"></asp:TextBox>
                                </div>

                                <div class="col-xl-3 col-md-6 col-sm-6 col-12 razmak1 razmak2">
                                    <asp:Button ID="btnPromeniBrojMestaU" runat="server" CssClass="auto-style1" OnClick="btnPromeniBrojMestaU_OnClick" OnClientClick="ClientSideClick(this)" Text="Prikaži zadati broj mesta za unos" UseSubmitBehavior="False" Style="text-align: center; font-size: 14px;" Width="266px" />
                                </div>

                                <div class="col-xl-2 col-md-6 col-sm-6 col-12">
                                    <asp:Label ID="lblStringRU" runat="server" Font-Italic="True" ForeColor="Red" Width="100%"></asp:Label>
                                </div>
                            </div>
                            <div class="row">

                             <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12" style="text-align:left">
                                <asp:Label ID="lblUkupanBrojU" runat="server" Text="Broj Redova" CssClass="float-md-right" Font-Bold="true" Font-Size="12px" Font-Names="Sans-serif"></asp:Label>
                              </div>
                             </div>

                            <div class="row">
                                <div class="col-12" style="overflow-x: auto;">
                                    
                                    <asp:GridView ID="gvU" runat="server" HorizontalAlign="Center" AutoGenerateColumns="False" EmptyDataText="Nema podataka!" DataKeyNames="IdMesto" OnRowDataBound="gvU_RowDataBound" Width="100%" Visible="true">
                                        <Columns>

                                                   <asp:TemplateField HeaderText="IdOpstina" Visible="True">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIdOpstina" runat="server" Text='<%# Eval("IdOpstina") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                              <asp:TemplateField HeaderText="Opština" Visible="true">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlOpstina" runat="server" Width="95%" Height="24px" CssClass="glowing-border"></asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>  

                                             <asp:TemplateField HeaderText="Id mesta">
                                                <ItemTemplate>
                                                   <asp:Label ID="lblIdMesto" runat="server" Text='<%# Eval("IdMesto") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Ptt Broj" Visible="true">
                                                <ItemTemplate>
                                                     <asp:TextBox ID="tbPttBroj" runat="server" Text='<%# Eval("PttBroj") %>' Width="90%" Height="24px" CssClass="glowing-border"></asp:TextBox> 
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Naziv mesta">
                                                <ItemTemplate>
                                                       <asp:TextBox ID="tbNazivMesta" runat="server" Text='<%# Eval("NazivMesta") %>' Width="90%" Height="24px" CssClass="glowing-border"></asp:TextBox>                                                                                                       
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                                                                         

                                            <asp:TemplateField HeaderText="" >
                                                <ItemTemplate>
                                                      <asp:Button ID="btnObrisi" runat="server" CssClass="dugme100" OnClick="btnObrisi_Click" OnClientClick="if(!confirm('Sigurni ste?')) return false;" Text="Obriši" UseSubmitBehavior="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="" >
                                                <ItemTemplate>
                                                    <asp:Button ID="btnIzmeni" runat="server" CssClass="dugme100" OnClick="btnIzmeni_Click" OnClientClick="ClientSideClick(this)" Text="Izmeni" UseSubmitBehavior="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="" >
                                                <ItemTemplate>
                                                   <asp:Button ID="btnSacuvajIzmene" Enabled="false" runat="server" CssClass="dugme100" OnClick="btnSacuvajIzmene_Click" OnClientClick="ClientSideClick(this)" Text="Sačuvaj izmene" UseSubmitBehavior="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="" >
                                                <ItemTemplate>
                                                   <asp:Button ID="btnOtkaziIzmene" Enabled="false" runat="server" CssClass="dugme100" OnClick="btnOtkaziIzmene_Click" OnClientClick="ClientSideClick(this)" Text="Otkaži izmene" UseSubmitBehavior="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <RowStyle BackColor="#EFF3FB" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#005094" />
                                        <PagerStyle BackColor="White" HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#dacef2" ForeColor="Black" Font-Size="Medium" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                   <br />
                <br />
             <br />
      <div class="container">
    <div class="row">
    <div class="col-12" style="overflow-x:auto; font-style: italic ">
<asp:Button ID="btnUnesi" runat="server" CssClass="dugme200" Text="Unesi nova mesta" Style="font-size: 14px;"
    OnClick="btnUnesi_onclick" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False"  /><%--  Visible="false"--%>
        </div>
    </div>
          </div>
                        
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>