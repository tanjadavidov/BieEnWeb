<%@ Page Title="" Language="C#" MasterPageFile="~/Master/BioEn.Master" AutoEventWireup="true" CodeBehind="PromenaLozinkeKorisnika.aspx.cs" Inherits="BieEnWeb.WebForms.PromenaLozinkeKorisnika" %>
<%@ Register src="../UserControls/ucPromenaLozinkeKorisnika.ascx" tagname="ucPromenaLozinkeKorisnika" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:ucPromenaLozinkeKorisnika ID="ucPromenaLozinkeKorisnika1" runat="server" />
</asp:Content>
