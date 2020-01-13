using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BieEnWeb.UserControls
{
    public partial class ucPromenaLozinkeKorisnika : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !Page.IsCallback)
            {
                tbImePrezime.Enabled = true;
                tbPocetnaLozinka.Enabled = false;

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
                ScriptManager.RegisterStartupScript(page1, typeof(Page), "Прикажи обавештење", script, true);
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
            }
        }

       

        protected void btnOcisti_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("PromenaLozinkeKorisnika.aspx?");
        }




        protected void btnPromeniLozinku_OnClick(object sender, EventArgs e)
        {
            
            BioEnWcf.IWcfBioEn client = new BioEnWcf.WcfBioEnClient();

            if (tbImePrezime.Text == "" || tbPocetnaLozinka.Text == "")
                ObradaiObavestenje("Унесите корисничко име и лозинку");


          /*  BioEnWcf.dsUlaz ulaz = new BioEnWcf.dsUlaz();
            BioEnWcf.dsIzlaz izlaz = new BioEnWcf.dsIzlaz();*/

            string KorisnickoIme = tbImePrezime.Text;
            string Lozinka = tbPocetnaLozinka.Text;

            BioEnWcf.RestartLozinkeRequest zahtev = new BioEnWcf.RestartLozinkeRequest(KorisnickoIme, Lozinka);
            BioEnWcf.RestartLozinkeResponse odgovor = new BioEnWcf.RestartLozinkeResponse();



            try
            {
                odgovor = client.RestartLozinke(zahtev);
            }
            catch (Exception ex)
            {
                ObradaiObavestenje("Грешка у процедури btnPromeniLozinku_OnClick при позиву методе AAA_RestartLozinke из сервиса!  \\n\\n" + ex.Message);
                return;
            }
            try
            {
                if (odgovor.RestartLozinkeResult.dtGreska.Rows.Count > 0)
                    ObradaiObavestenje(odgovor.RestartLozinkeResult.dtGreska.Rows[0][0].ToString());
                else
                {
                    ObradaiObavestenje("Успешно промењена лозинка!");
                    btnPromeniLozinku.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ObradaiObavestenje("Грешка код промене лозинке корисника!  \\n\\n " + ex.Message);
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
                ObradaiObavestenje("Greška u proceduri uspKorisnikVrati pri pozivu metode KorisnikVrati iz servisa!  \\n\\n" + ex.Message);
                return;
            }
            try
            {
                if (odgovor.KorisnikVratiResult.dtGreska.Rows.Count > 0)
                    ObradaiObavestenje(odgovor.KorisnikVratiResult.dtGreska.Rows[0][0].ToString());
                else
                {
                    if (odgovor.KorisnikVratiResult.dtKorisnikVrati.Rows[0]["IDKorisnik"].ToString() == "0")
                        ObradaiObavestenje("Korisnik već postoji!");
                    else
                    {
                        tbImePrezime.Enabled = false;
                        tbPocetnaLozinka.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ObradaiObavestenje("Грешка у апликацији у процедури btnPretrazi_OnClick!  \\n\\n " + ex.Message);
                return;
            }
        }
    }
}