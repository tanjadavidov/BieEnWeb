using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BieEnWeb.UserControls
{
    public partial class ucPromenaKorisnickihPrava : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !Page.IsCallback)
            {
                pnlUnos.Visible = false;
                tbImePrezime.Enabled = true;
                ddlUlogaPopuni();

            }
        }


        protected void ddlUlogaPopuni()
        {           

            BioEnWcf.IWcfBioEn client = new BioEnWcf.WcfBioEnClient();
            BioEnWcf.UlogaVratiRequest zahtev = new BioEnWcf.UlogaVratiRequest();
            BioEnWcf.UlogaVratiResponse odgovor = new BioEnWcf.UlogaVratiResponse();

            try
            {
                odgovor = client.UlogaVrati(zahtev);
            }
            catch (Exception ex)
            {
                ObradaiObavestenje("Greška u proceduri ddlUlogaPopuni pri pozivanju metode UlogaVrati iz servisa!  \\n" + ex.Message);
                return;
            }
            try
            {
                if (odgovor.UlogaVratiResult.dtGreska.Rows.Count > 0)
                    ObradaiObavestenje(odgovor.UlogaVratiResult.dtGreska.Rows[0][0].ToString());
                else
                {
                    ddlUloga.DataSource = odgovor.UlogaVratiResult.dtUlogaVrati.Rows;
                    ddlUloga.DataValueField = "IDUloga";
                    ddlUloga.DataTextField = "NazivUloge";
                    ddlUloga.DataBind();
                }
            }
            catch (Exception ex)
            {
                ObradaiObavestenje("Greška u aplikaciji u metodi ddlUlogaPopuni  \\n" + ex.Message);
                return;
            }
        }


        private void ObradaiObavestenje(string poruka)
        {
            try
            {
                poruka = (poruka.Replace("\n", "")).Replace("'", "");
                poruka = (poruka.Replace("\r", "")).Replace("'", "");
                string script = "alert('" + poruka + "');";
                Page page1 = (Page)HttpContext.Current.Handler;
                ScriptManager.RegisterStartupScript(page1, typeof(Page), "Prikaži obaveštenje", script, true);
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
            }
        }

        protected void btnPretrazi_OnClick(object sender, EventArgs e)
        {

        }

        protected void btnOcisti_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("PromenaKorisnickihPrava.aspx?");
        }

        protected void btnSacuvaj_OnClick(object sender, EventArgs e)
        {

            BioEnWcf.IWcfBioEn client = new BioEnWcf.WcfBioEnClient();
            string KorisnickoIme = tbImePrezime.Text;
            int IdUloga = int.Parse(ddlUloga.SelectedValue);
            bool Status = bool.Parse(ddlStatus.SelectedValue);
            BioEnWcf.PromenaPravaKorisnikaRequest zahtev = new BioEnWcf.PromenaPravaKorisnikaRequest(KorisnickoIme, IdUloga, Status);
            BioEnWcf.PromenaPravaKorisnikaResponse odgovor = new BioEnWcf.PromenaPravaKorisnikaResponse();

            try
            {
                odgovor = client.PromenaPravaKorisnika(zahtev);  //client.PromenaPravaKorisnika(zahtev);
            }
            catch (Exception ex)
            {
                ObradaiObavestenje("Greška u proceduri PromenaPravaKorisnika pri pozivu metode PromenaPravaKorisnika iz servisa!  \\n\\n" + ex.Message);
                return;
            }
            try
            {
                if (odgovor.PromenaPravaKorisnikaResult.dtGreska.Rows.Count > 0)
                    ObradaiObavestenje(odgovor.PromenaPravaKorisnikaResult.dtGreska.Rows[0][0].ToString());
                else
                {
                    ObradaiObavestenje("Uspešno sačuvani podaci o promeni korisničkih prava!");
                    btnSacuvaj.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ObradaiObavestenje("Greška kod čuvanja podataka o promeni korisnika!  \\n\\n " + ex.Message);
                return;
            }
        }

        protected void btnProveri_OnClick(object sender, EventArgs e)
        {
           
            BioEnWcf.IWcfBioEn client = new BioEnWcf.WcfBioEnClient();
            BioEnWcf.dsUlaz ulaz = new BioEnWcf.dsUlaz();
            BioEnWcf.dsIzlaz izlaz = new BioEnWcf.dsIzlaz();
            string KorisnickoIme = tbImePrezime.Text; 
            BioEnWcf.KorisnikVratiRequest zahtev = new BioEnWcf.KorisnikVratiRequest(KorisnickoIme);
            BioEnWcf.KorisnikVratiResponse odgovor = new BioEnWcf.KorisnikVratiResponse();

            try
            {
                odgovor = client.KorisnikVrati(zahtev);
            }
            catch (Exception ex)
            {
                ObradaiObavestenje("Greška u proceduri KorisnikVrati pri pozivu metode KorisnikVrati iz servisa!  \\n\\n" + ex.Message);
                return;
            }
            try
            {
                if (odgovor.KorisnikVratiResult.dtGreska.Rows.Count > 0)
                    ObradaiObavestenje(odgovor.KorisnikVratiResult.dtGreska.Rows[0][0].ToString());
                else
                {
                    if (odgovor.KorisnikVratiResult.dtKorisnikVrati.Rows[0]["IDKorisnik"].ToString() == "0")
                        ObradaiObavestenje("Korisnik ne postoji");
                    else
                    {
                        tbImeKorisnika.Text = odgovor.KorisnikVratiResult.dtKorisnikVrati.Rows[0]["Ime"].ToString();
                        tbPrezimeKorisnika.Text = odgovor.KorisnikVratiResult.dtKorisnikVrati.Rows[0]["Prezime"].ToString();
                        ddlUloga.SelectedValue = odgovor.KorisnikVratiResult.dtKorisnikVrati.Rows[0]["IDUloga"].ToString();
                        ddlStatus.SelectedValue = odgovor.KorisnikVratiResult.dtKorisnikVrati.Rows[0]["Status"].ToString();
                        tbImePrezime.Enabled = false;
                        pnlUnos.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ObradaiObavestenje("Greška u aplikaciji u metodi btnProveri_OnClick!  \\n\\n " + ex.Message);
                return;
            }
        }
    }
}