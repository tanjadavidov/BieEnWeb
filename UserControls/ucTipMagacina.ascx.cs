using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BieEnWeb.UserControls
{
    public partial class ucTipMagacina : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !Page.IsCallback)
            {
                //gv.Visible = true;
             //   PaUnosMesta.Visible = false;
              //  PaU.Visible = false;
             //   Ucitaj();

            }
        }
    }
}