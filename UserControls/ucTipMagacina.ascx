<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucTipMagacina.ascx.cs" Inherits="BieEnWeb.UserControls.ucTipMagacina" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>





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


<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:Panel ID="pa" runat="server" Width="100%">

            <br />
            <br />

             <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="paTipMagacina" runat="server" Width="100%" Visible="true">
                        <div class="container">
                            <div class="row">
                                <div class="col-12" style="font-weight: bold; font-style: italic; text-decoration: underline; background-color: #dacef2;">
                                    <p>Prikaži tipove magacina</p>
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
                                    
                                    <asp:GridView ID="gv" runat="server" HorizontalAlign="Center" AutoGenerateColumns="False" EmptyDataText="Nema podataka!" DataKeyNames="IdTipMagacina"
                                        OnRowDataBound="gv_RowDataBound" Width="90%" Visible="true" PageSize="5"  OnPageIndexChanging="gv_PageIndexChanging" AllowPaging="true" >
                                        <Columns>


                                             <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" headerstyle-width="10%" >
                                                <ItemTemplate>
                                                      <asp:Button ID="btnObrisi" runat="server"   CssClass="dugme100" OnClick="btnObrisi_Click" OnClientClick="if(!confirm('Sigurni ste?')) return false;" 
                                                          Text="Obriši" UseSubmitBehavior="false" Width="99%" />
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                                   <asp:TemplateField HeaderText="Id tip magacina" headerstyle-width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIdTipMagacina" runat="server" Text='<%# Eval("IdTipMagacina") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Naziv tipa magacina" Visible="true" headerstyle-width="50%"  >
                                                <ItemTemplate>
                                                   <%-- <asp:Label ID="lblPttBroj" runat="server" Text='<%# Eval("PttBroj") %>'></asp:Label>--%>
                                                    <asp:TextBox ID="tbNazivTipaMagacina" runat="server" style="text-align: center" Text='<%# Eval("NazivTipaMagacina") %>' Width="99%" Height="24px" CssClass="glowing-border" Enabled="false"></asp:TextBox> 
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