<%@ Page Title="" Language="C#" MasterPageFile="~/Master/BioEn.Master" AutoEventWireup="true" CodeBehind="PromenaKorisnickihPrava.aspx.cs" Inherits="BieEnWeb.WebForms.PromenaKorisnickihPrava" %>
<%@ Register src="../UserControls/ucPromenaKorisnickihPrava.ascx" tagname="ucPromenaKorisnickihPrava" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:ucPromenaKorisnickihPrava ID="ucPromenaKorisnickihPrava1" runat="server" />
</asp:Content>
