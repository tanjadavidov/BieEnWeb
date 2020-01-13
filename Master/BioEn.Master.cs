using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace BieEnWeb.Master
{
    public partial class BioEn : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lbtnKorisnik.Text = Session["Korisnik_Ime"].ToString() + ' ' + Session["Korisnik_Prezime"].ToString();


                if (Session["Menu"] != null)
                {
                    string xmlStr = Session["Menu"].ToString();
                    if (xmlStr == "" || xmlStr == "<dsIzlaz/>")
                        xmlStr = "<siteMap></siteMap>";
                    menuXMLDS.Data = "";
                    menuXMLDS.Data = xmlStr;

                    OsnovniMenu.Items.Clear();
                    OsnovniMenu.DataBind();
                    OsnovniMenu.Attributes.Add("MenuItemClick", "OsnovniMenu_MenuItemClick");

                }
                else
                    lblStatus.Text = "Немате право на изабрану опцију";

                //GetTreeViewItems();
            }
        }


        private void PozoviJavascript(string script)
        {
            try
            {
                Page page1 = (Page)HttpContext.Current.Handler;
                ScriptManager.RegisterStartupScript(page1, typeof(Page), "ObradaiObavestenje", script, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void OsnovniMenu_MenuItemClick(object sender, MenuEventArgs e)
        {
            lblNaslov.Text = e.Item.Text;
        }

        protected void lnkPomoc_Click(object sender, EventArgs e)
        {

        }

        protected void lbtnKorisnik_Click(object sender, EventArgs e)
        {

            Response.Redirect("~/aaaWebForms/aaaKorisnik.aspx");
        }

        protected void lbtnOdjava_Click(object sender, EventArgs e)
        {
            Session.Abandon();

            Response.Redirect("../aaaWebForms/aaaPrijava.aspx");
        }



    }
}