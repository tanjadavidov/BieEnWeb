using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BieEnWeb.UserControls
{
    public partial class ucUnosNovogKorisnika : System.Web.UI.UserControl
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



        protected void btnSacuvaj_OnClick(object sender, EventArgs e)
        {


            //wcfKategorizacijaPS.IwcfKategorizacijaPS client = new wcfKategorizacijaPS.IwcfKategorizacijaPSClient();

            BioEnWcf.IWcfBioEn client = new BioEnWcf.WcfBioEnClient();


            //ulazni podaci
            string KorisnickoIme = tbImePrezime.Text;
            if (tbPocetnaLozinka.Text == "")
            {
                ObradaiObavestenje("Morate popuniti početnu lozinku (u zadatom formatu)");
                return;
            }
            string Lozinka = tbPocetnaLozinka.Text;
            //ime
            if (tbImeKorisnika.Text == "" || tbImeKorisnika.Text == " ")
            {
                ObradaiObavestenje("Unesite ime korisnika! ");
                return;
            }
            if (tbImeKorisnika.Text.Length != 0)
            {
                if (tbImeKorisnika.Text.Length <= 1)
                {
                    ObradaiObavestenje("Ime korisnika ne može biti kraće od 2 slova!");
                    return;
                }

                proveraImena();
                if (hdIme.Value == "1")
                {
                    ObradaiObavestenje("Ime korisnika: možete uneti crticu i slovne znakov (ćirilice ili latinice) ");
                    return;
                }

            }
            string Ime = tbImeKorisnika.Text;
            string lower = "UPPERCASE string";
            string upper = lower.ToUpper();



            //prezime
            if (tbPrezimeKorisnika.Text == "" || tbPrezimeKorisnika.Text == " ")
            {
                ObradaiObavestenje("Unesite prezime korisnika! ");
                return;
            }
            if (tbPrezimeKorisnika.Text.Length != 0)
            {
                if (tbPrezimeKorisnika.Text.Length <= 1)
                {

                    ObradaiObavestenje("Prezime korisnika ne može biti kraće od 2 slova!");
                    return;
                }

               proveraPrezimena();
                if (hdPrezime.Value == "1")
                {
                    ObradaiObavestenje("Prezime korisnika: možete uneti crticu i slovne znakove (ćirilice ili latinice)");
                    return;
                }
            }
            string Prezime = tbPrezimeKorisnika.Text;
            if (tbEPosta.Text == "" || tbEPosta.Text == " ")
            {
                ObradaiObavestenje("Unesite e Mail adresu korisnika! ");
                return;
            }
            //  mail
            if (tbEPosta.Text.Length < 8)
            {
                ObradaiObavestenje("Preverite da li ste dobro uneli e Mail adresu!");
                return;
            }

            if (tbEPosta.Text.Length > 7)   proveraMail();  //mk@gmail.com ??? 
            if (hdMail.Value == "2")
            {
                ObradaiObavestenje("e Mail adresa nije validna, proverite unos!");
                return;
            }

            dodatnaProveraMail();
            if (hdMailDodatni.Value == "1")
            {
                ObradaiObavestenje("e Mail adresa nije validna, ponovo proverite unos!");
                return;
            }
            dodatnaProveraMail();
            if (hdMailDodatni.Value == "1")
            {
                ObradaiObavestenje("e Mail adresa nije validna, ponovo proverite unos!");
                return;
            }
            string Eposta = tbEPosta.Text;
            if (ddlUloga.SelectedValue == "0")
            {
                ObradaiObavestenje("Morate uneti ulogu korisnika");
                return;
            }
            string Telefon = tbTelefon.Text;
            int IdUloga = int.Parse(ddlUloga.SelectedValue);
            BioEnWcf.KorisnikUnosRequest zahtev = new BioEnWcf.KorisnikUnosRequest(KorisnickoIme, Ime, Prezime, Eposta, Telefon, Lozinka, IdUloga);
            BioEnWcf.KorisnikUnosResponse odgovor = new BioEnWcf.KorisnikUnosResponse();

            try
            {
                odgovor = client.KorisnikUnos(zahtev);
            }
            catch (Exception ex)
            {
                ObradaiObavestenje("Greška u proceduri aaaKorisnikUnos pri pozivu metode KorisnikUnos iz servisa!  \\n\\n" + ex.Message);
                return;
            }
            try
            {
                if (odgovor.KorisnikUnosResult.dtGreska.Rows.Count > 0)
                    ObradaiObavestenje(odgovor.KorisnikUnosResult.dtGreska.Rows[0][0].ToString());
                else
                {
                    ObradaiObavestenje("Uspešno sačuvani podaci o korisniku!");
                    btnSacuvaj.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ObradaiObavestenje("Greška u aplikaciji u proceduri KorisnikUnos!  \\n\\n " + ex.Message);
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
                ObradaiObavestenje("Greška u proceduri uspKorisnikVrati pri pozivu metode KorisnikVrati iz servisa! \\n\\n" + ex.Message);
                return;
            }
            try
            {
                if (odgovor.KorisnikVratiResult.dtGreska.Rows.Count > 0)
                    ObradaiObavestenje(odgovor.KorisnikVratiResult.dtGreska.Rows[0][0].ToString());
                else
                {
                    if (odgovor.KorisnikVratiResult.dtKorisnikVrati.Rows[0]["IDKorisnik"].ToString() != "0")
                        ObradaiObavestenje("Korisnik već postoji!");
                    else
                    {
                        tbImePrezime.Enabled = false;
                        pnlUnos.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ObradaiObavestenje("Greška u aplikaciji u proceduri uspKorisnikVrati! \\n\\n " + ex.Message);
                return;
            }
        }

        protected void btnOcisti_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("UnosNovogKorisnika.aspx?");
        }



        private void proveraImena()
        {
            string ime = "";
            ime = tbImeKorisnika.Text;

            string strRegex = @"^[a-zžšđčćA-ZŽŠĐČĆабвгдђежзијклљмнњопрстћуфхцчџшАБВГДЂЕЖЗИЈКЛЉМНЊОПРСТЋУФХЦЧЏШ\-\s]+$";       //  а-шА-Ш\-\     "^[a-zA-Z\\s]+$"   ЉЊЖШЂЧЋ

            Regex re = new Regex(strRegex);
            if (re.IsMatch(ime))
                hdIme.Value = "0";   //true
            else
                hdIme.Value = "1"; //false
        }


        private void proveraPrezimena()
        {
            string prezime = "";
            prezime = tbPrezimeKorisnika.Text;

            string strRegex = @"^[a-zžšđčćA-ZŽŠĐČĆабвгдђежзијклљмнњопрстћуфхцчџшАБВГДЂЕЖЗИЈКЛЉМНЊОПРСТЋУФХЦЧЏШ\-\s]+$";      

            Regex re = new Regex(strRegex);
            if (re.IsMatch(prezime))
                hdPrezime.Value = "0";   //true
            else
                hdPrezime.Value = "1"; //false
        }


        private void proveraMail()
        {
            int manki;
            int poslednjaTacka;
            string adresa;
            adresa = tbEPosta.Text;
            manki = adresa.IndexOf("@");
            poslednjaTacka = adresa.LastIndexOf(".");
            if (manki < 1 || poslednjaTacka < manki + 2 || poslednjaTacka + 2 >= adresa.Length)
            {
                hdMail.Value = "2";  //pogrešna Zvanična Mail adresa
            }
            else
            {
                hdMail.Value = "0";  //pogrešna Zvanična Mail adresa
            }
        }


        private void dodatnaProveraMail()
        {
            string inputEmail;
            inputEmail = tbEPosta.Text;
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                hdMailDodatni.Value = "0";   //true
            else
                hdMailDodatni.Value = "1"; //false

        }


    }

}