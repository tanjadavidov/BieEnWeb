using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BieEnWeb.aaaWebForms
{
    public partial class aaaKorisnik : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !Page.IsCallback)
            {
                PopuniTrenutnePodatke();
                lblNaslov.Text = "AŽURIRANJE KORISNIČKOG NALOGA";
                aaaProvera();

                if (Session["Korisnik_PocetnaLozinka"].ToString() == "1")
                {
                    btnNazad.Visible = false;
                    ObradaiObavestenje("Vaš nalog je u potpunosti aktiviran, početna lozinka je uspešno kreirana!  \\n\\nUnesite novu lozinku i ažurirajte vaše lične podatke!");
                }
            }
        }


       protected void aaaProvera()
        {
            //Провери да ли је улогован
            if (Session["Korisnik_IDKorisnik"] == null || Session["Korisnik_IDKorisnik"].ToString() == "")
                Response.Redirect("~/aaaWebForms/aaaPrijava.aspx");
            if (Session["Korisnik_KorisnickoIme"] == null)
                Response.Redirect("~/aaaWebForms/aaaPrijava.aspx");
            if (Session["Korisnik_Ime"] == null)
                Response.Redirect("~/aaaWebForms/aaaPrijava.aspx");
            if (Session["Korisnik_Prezime"] == null)
                Response.Redirect("~/aaaWebForms/aaaPrijava.aspx");
            if (Session["Korisnik_ePosta"] == null)
                Response.Redirect("~/aaaWebForms/aaaPrijava.aspx");
            if (Session["Korisnik_Telefon"] == null)
                Response.Redirect("~/aaaWebForms/aaaPrijava.aspx");
            //if (Session["Korisnik_IDSifNarucilac"] == null)
            //    Response.Redirect("~/AAA/WebForms/AAAPrijava.aspx");
            //if (Session["Korisnik_NazivNarucioca"] == null)
            //    Response.Redirect("~/AAA/WebForms/AAAPrijava.aspx");

            //Провера права приступа

            bool imaPravo = false;
          
            BioEnWcf.IWcfBioEn client = new BioEnWcf.WcfBioEnClient();
            BioEnWcf.dsUlaz ulaz = new BioEnWcf.dsUlaz();
            BioEnWcf.dsUlaz.dtaaaRow red = ulaz.dtaaa.NewdtaaaRow();



            red.idKorisnik = int.Parse(Session["Korisnik_IDKorisnik"].ToString());
            red.idFunkcija = 2;        //   ***********************************
            ulaz.dtaaa.AdddtaaaRow(red);

            BioEnWcf.KorisnikPravoNaFunkcijuRequest zahtev = new BioEnWcf.KorisnikPravoNaFunkcijuRequest(ulaz);
            BioEnWcf.KorisnikPravoNaFunkcijuResponse odgovor = new BioEnWcf.KorisnikPravoNaFunkcijuResponse();


            try
            {
                odgovor = client.KorisnikPravoNaFunkciju(zahtev);
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                ObradaiObavestenje("Грешка у процедури KorisnikPravoNaFunkciju при позиву сервиса за проверу налога!  \\n\\n" + ex.Message);
                
                return;
            }
            try
            {
                if (odgovor.KorisnikPravoNaFunkcijuResult.dtGreska.Rows.Count > 0)
                    ObradaiObavestenje(odgovor.KorisnikPravoNaFunkcijuResult.dtGreska.Rows[0][0].ToString());
                else
                {
                    string sPravo = odgovor.KorisnikPravoNaFunkcijuResult.dtKorisnikPravoNaFunkciju.Rows[0]["PravoNaFunkciju"].ToString();
                    if (sPravo == "1")
                        imaPravo = true;
                    else
                        imaPravo = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                ObradaiObavestenje("Грешка у апликацији у методи KorisnikPravoNaFunkciju при провери налога!  \\n\\n " + ex.Message);
                return;
            }

            if (!imaPravo)
                Response.Redirect("~/WebForms/AccessDenied.aspx");
        }
        
            

        protected void PopuniTrenutnePodatke()
        {
            //Session["Korisnik_IDKorisnik"]
            tbKorisnickoIme.Text = Session["Korisnik_KorisnickoIme"].ToString();
            tbIme.Text = Session["Korisnik_Ime"].ToString();
            tbPrezime.Text = Session["Korisnik_Prezime"].ToString();
            tbEposta.Text = Session["Korisnik_ePosta"].ToString();
            tbTelefon.Text = Session["Korisnik_Telefon"].ToString();

            lblKorisnik.Text = Session["Korisnik_Ime"].ToString() + ' ' + Session["Korisnik_Prezime"].ToString();
        }


        protected void btnPromena_Click(object sender, EventArgs e)
        {
            if (tbLozinka_Stara.Text == "")
            {
                ObradaiObavestenje("Unesite staru lozinku!");
                return;
            }
            if (tbLozinka_Nova.Text == "")
            {
                ObradaiObavestenje("Unesite novu lozinku!");
                return;
            }
            if (tbLozinka_NovaPonovljeno.Text == "")
            {
                ObradaiObavestenje("Potvrdite novu lozinku!");
                return;
            }

            string sLozinka_Stara = tbLozinka_Stara.Text;
            string sLozinka_Nova = tbLozinka_Nova.Text;
            string sLozinka_NovaPonovljeno = tbLozinka_NovaPonovljeno.Text;

            if (sLozinka_Nova != sLozinka_NovaPonovljeno)
            {
                ObradaiObavestenje("Unete nove lozinke se ne poklapaju!");
                return;
            }

            //Провера да ли је исправна стара лозинка
            ProveraStarihPodataka();
        }


        protected void ProveraStarihPodataka()
        {

            BioEnWcf.IWcfBioEn client = new BioEnWcf.WcfBioEnClient();
            BioEnWcf.dsUlaz ulaz = new BioEnWcf.dsUlaz();
            BioEnWcf.dsUlaz.dtaaaRow red = ulaz.dtaaa.NewdtaaaRow();

            red.KorisnickoIme = Session["Korisnik_KorisnickoIme"].ToString();
            red.Lozinka = tbLozinka_Stara.Text;

            ulaz.dtaaa.AdddtaaaRow(red);
            BioEnWcf.KorisnikPrijavaRequest zahtev = new BioEnWcf.KorisnikPrijavaRequest(ulaz);
            BioEnWcf.KorisnikPrijavaResponse odgovor = new BioEnWcf.KorisnikPrijavaResponse();
           

            try
            {
                odgovor = client.KorisnikPrijava(zahtev);  
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                ObradaiObavestenje("Greška u proceduri KorisnikPrijava pri pozivu servisa!  \\n\\n" + ex.Message);
                return;
            }
            try
            {
                if (odgovor.KorisnikPrijavaResult.dtGreska.Rows.Count>0)
                    ObradaiObavestenje(odgovor.KorisnikPrijavaResult.dtGreska.Rows[0][0].ToString());
                else if (odgovor.KorisnikPrijavaResult.dtKorisnikPrijava.Rows.Count !=1)
                {
                    ObradaiObavestenje("Greška u aplikaciji u metodi KorisnikPrijava!  \\n\\nNije pronađen originalni nalog!");
                    return;
                }
                else
                {
                    //Промени податке
                    PromeniPodatke();
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                ObradaiObavestenje("Greška u aplikaciji u metodi KorisnikPrijava!  \\n\\n " + ex.Message);
                return;
            }
        }


        protected void PromeniPodatke()
        {

           
            BioEnWcf.IWcfBioEn client = new BioEnWcf.WcfBioEnClient();
            BioEnWcf.dsUlaz ulaz = new BioEnWcf.dsUlaz();
            BioEnWcf.dsUlaz.dtaaaRow red = ulaz.dtaaa.NewdtaaaRow();

            //NOVI PODACI
            //IDKorisnik se ne menja
            
            red.idKorisnik = int.Parse(Session["Korisnik_IDKorisnik"].ToString());


            if (tbKorisnickoIme.Text == "")
            {
                ObradaiObavestenje("Unesite korisničko ime!");
                return;
            }
            else
                red.KorisnickoIme = tbKorisnickoIme.Text;

            if (tbIme.Text == "")
            {
                ObradaiObavestenje("Korisničko ime!");
                return;
            }
            else
                red.Ime = tbIme.Text;

            if (tbPrezime.Text == "")
            {
                ObradaiObavestenje("Unesite prezime!");
                return;
            }
            else
                red.Prezime = tbPrezime.Text;

            if (tbEposta.Text == "")
            {
                ObradaiObavestenje("Unesite e-poštu!");
                return;
            }
            else
                red.ePosta = tbEposta.Text;

            if (tbTelefon.Text == "")
            {
                ObradaiObavestenje("Unesite telefon!");
                return;
            }
            else
                red.Telefon = tbTelefon.Text;

            red.Lozinka = tbLozinka_Nova.Text;

           
            ulaz.dtaaa.AdddtaaaRow(red);
            BioEnWcf.KorisnikPromenaPodatakaRequest zahtev = new BioEnWcf.KorisnikPromenaPodatakaRequest(ulaz);
            BioEnWcf.KorisnikPromenaPodatakaResponse odgovor = new BioEnWcf.KorisnikPromenaPodatakaResponse();

            try
            {
                odgovor = client.KorisnikPromenaPodataka(zahtev);
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                ObradaiObavestenje("Greška u proceduri KorisnikPromenaPodataka pri pozivu servisa!  \\n\\n" + ex.Message);
                return;
            }
            try
            {
                if (odgovor.KorisnikPromenaPodatakaResult.dtGreska.Rows.Count > 0)
                    ObradaiObavestenje(odgovor.KorisnikPromenaPodatakaResult.dtGreska.Rows[0][0].ToString());
                else
                {
                    //Session["Korisnik_IDKorisnik"] ---- Остаје не промењено!!!
                    Session["Korisnik_KorisnickoIme"] = tbKorisnickoIme.Text;
                    Session["Korisnik_Ime"] = tbIme.Text;
                    Session["Korisnik_Prezime"] = tbPrezime.Text;
                    Session["Korisnik_ePosta"] = tbEposta.Text;  
                    Session["Korisnik_Telefon"] = tbTelefon.Text;
                    Session["Korisnik_PocetnaLozinka"] = "0";
                    lblKorisnik.Text = tbIme.Text + ' ' + tbPrezime.Text;

                    btnNazad.Visible = true;
                    PopuniTrenutnePodatke();

                    ObradaiObavestenje("Uspešno promenjeni podaci!");
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                ObradaiObavestenje("Greška u aplikaciji u metodi PromeniPodatke!  \\n\\n" + ex.Message);
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


        protected void btnNazad_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/WebForms/Default.aspx");
        }


        protected void lbtnOdjava_Click(object sender, EventArgs e)
        {
            Session["Korisnik_IDKorisnik"] = null;
            Session["Korisnik_KorisnickoIme"] = null;
            Session["Korisnik_Ime"] = null;
            Session["Korisnik_Prezime"] = null;
            Session["Korisnik_ePosta"] = null;
            Session["Korisnik_Telefon"] = null;

            Response.Redirect("~/aaaWebForms/aaaPrijava.aspx");
        }


    }
}