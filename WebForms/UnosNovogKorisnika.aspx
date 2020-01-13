<%@ Page Title="" Language="C#" MasterPageFile="~/Master/BioEn.Master" AutoEventWireup="true" CodeBehind="UnosNovogKorisnika.aspx.cs" Inherits="BieEnWeb.WebForms.UnosNovogKorisnika" %>
<%@ Register src="../UserControls/ucUnosNovogKorisnika.ascx" tagname="ucUnosNovogKorisnika" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:ucUnosNovogKorisnika ID="ucUnosNovogKorisnika1" runat="server" />
</asp:Content>
