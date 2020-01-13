<%@ Page Title="" Language="C#" MasterPageFile="~/Master/BioEn.Master" AutoEventWireup="true" CodeBehind="JediniceMere.aspx.cs" Inherits="BieEnWeb.WebForms.JediniceMere" %>
<%@ Register src="../UserControls/ucJediniceMere.ascx" tagname="ucJediniceMere" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <uc1:ucJediniceMere ID="ucJediniceMere1" runat="server" />
</asp:Content>
