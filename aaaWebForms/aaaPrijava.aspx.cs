using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BieEnWeb.aaaWebForms
{
    public partial class aaaPrijava : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !Page.IsCallback)
            {
                Session.Clear();
                ddlFunkcija_Popuni();


            }
        }

        protected void btnPrijava_Click(object sender, EventArgs e)
        {
            bool uspesnaPrijava = false;
            bool pocetnaLozinka = false;

            
            BioEnWcf.IWcfBioEn client = new BioEnWcf.WcfBioEnClient();
            BioEnWcf.dsUlaz ulaz = new BioEnWcf.dsUlaz();
            BioEnWcf.dsUlaz.dtaaaRow red = ulaz.dtaaa.NewdtaaaRow();



            if (tbKorisnickoIme.Text == "" || tbKorisnickoIme.Text == " ")
            {
                ObradaiObavestenje("Unesite korisničko ime!");
                return;
            }
            else
                red.KorisnickoIme = tbKorisnickoIme.Text;

            if (tbLozinka.Text == "" || tbLozinka.Text == " ")
            {
                ObradaiObavestenje("Unesite lozinku!");
                return;
            }
            else
                red.Lozinka = tbLozinka.Text;

            
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
                ObradaiObavestenje("Greška u proceduri KorisnikPrijava pri pozivu servisa! \\n\\n" + ex.Message);
                return;
            }
            try
            {
                if (odgovor.KorisnikPrijavaResult.dtGreska.Rows.Count > 0)
                {
                    ObradaiObavestenje(odgovor.KorisnikPrijavaResult.dtGreska.Rows[0][0].ToString());
                    return;
                }
                else
                {
                    Session["Korisnik_IDKorisnik"] = odgovor.KorisnikPrijavaResult.dtKorisnikPrijava.Rows[0]["IDKorisnik"].ToString();
                    Session["Korisnik_KorisnickoIme"] = odgovor.KorisnikPrijavaResult.dtKorisnikPrijava.Rows[0]["KorisnickoIme"].ToString();
                    Session["Korisnik_Ime"] = odgovor.KorisnikPrijavaResult.dtKorisnikPrijava.Rows[0]["Ime"].ToString();
                    Session["Korisnik_Prezime"] = odgovor.KorisnikPrijavaResult.dtKorisnikPrijava.Rows[0]["Prezime"].ToString();
                    Session["Korisnik_ePosta"] = odgovor.KorisnikPrijavaResult.dtKorisnikPrijava.Rows[0]["ePosta"].ToString();
                    Session["Korisnik_Telefon"] = odgovor.KorisnikPrijavaResult.dtKorisnikPrijava.Rows[0]["Telefon"].ToString();

                    string s1 = odgovor.KorisnikPrijavaResult.dtKorisnikPrijava.Rows[0]["PocetnaLozinka"].ToString();
                    bool b1 = bool.Parse(s1);
                    if (b1)
                    {
                        //Налог није у потпуности активиран! Потребно је унети нову лозинку
                        Session["Korisnik_PocetnaLozinka"] = "1";
                        pocetnaLozinka = true;
                    }
                    else
                    {
                        Session["Korisnik_PocetnaLozinka"] = "0";
                        pocetnaLozinka = false;
                    }
                    uspesnaPrijava = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                ObradaiObavestenje("Greška u aplikaciji u metodi btnPrijava_Click pri proveri naloga!  \\n\\n " + ex.Message);
                return;
            }


            //Popuni Mеni
            try
            {
               
                BioEnWcf.IWcfBioEn clientZaMeni = new BioEnWcf.WcfBioEnClient();
                BioEnWcf.dsUlaz ulazZaMeni = new BioEnWcf.dsUlaz();
                BioEnWcf.dsUlaz.dtaaaRow redZaMeni = ulazZaMeni.dtaaa.NewdtaaaRow();

                redZaMeni.idKorisnik = int.Parse(Session["Korisnik_IDKorisnik"].ToString());
                ulazZaMeni.dtaaa.AdddtaaaRow(redZaMeni);
                BioEnWcf.FunkcijaVratiZaKorLiRequest zahtevZaMeni = new BioEnWcf.FunkcijaVratiZaKorLiRequest(ulazZaMeni);
                BioEnWcf.FunkcijaVratiZaKorLiResponse odgovorZaMeni = new BioEnWcf.FunkcijaVratiZaKorLiResponse();


                try
                {
                    odgovorZaMeni = clientZaMeni.FunkcijaVratiZaKorLi(zahtevZaMeni);
                }
                catch (Exception ex)
                {
                    ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                        , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                    ObradaiObavestenje("Greška u proceduri aaaFunkcijaVratiZaKorLi pri pozivu servisa za popunjavanje menija!  \\n\\n" + ex.Message);
                    return;
                }
                try
                {
                    if (odgovorZaMeni.FunkcijaVratiZaKorLiResult.dtGreska.Rows.Count > 0)
                    {
                        ObradaiObavestenje(odgovorZaMeni.FunkcijaVratiZaKorLiResult.dtGreska.Rows[0][0].ToString());
                    }
                    else
                    {
                        ////popunjava se menu sa dtMeni iz dsIzlaz, a brišu se sve ostale u Izlazu koje nam ne trebaju             
                        odgovorZaMeni.FunkcijaVratiZaKorLiResult.Tables.Remove("dtGreska");

                        string xmlStr = odgovorZaMeni.FunkcijaVratiZaKorLiResult.GetXml().Replace("xmlns=\"http://tempuri.org/dsIzlaz.xsd\"", "");
                        xmlStr = xmlStr.Replace("<dsIzlaz >", "<dsIzlaz>");
                        xmlStr = xmlStr.Replace("</dsIzlaz>", "</dsIzlaz>");
                        xmlStr = xmlStr.Replace("<dtFunkcijaVratiZaKorLi>", "<siteMap");
                        xmlStr = xmlStr.Replace("</dtFunkcijaVratiZaKorLi>", "\" />");

                        xmlStr = xmlStr.Replace("<NazivFunkcije>", " Funkcija=\"");
                        xmlStr = xmlStr.Replace("</NazivFunkcije>", " ");
                        xmlStr = xmlStr.Replace("<Url>", "\" Url=\"");
                        xmlStr = xmlStr.Replace("</Url>", "");
                        xmlStr = xmlStr.Replace("\r\n", "");
                        xmlStr = xmlStr.Replace("  ", "");
                        xmlStr = xmlStr.Replace(" \"", "\"");
                        Session["Menu"] = xmlStr;
                    }
                }
                catch (Exception ex)
                {
                    ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                        , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                    ObradaiObavestenje("Greška u aplikaciji u metodi btnPrijava_Click kod popunjavanja menija!  \\n\\n " + ex.Message);
                    return;
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                ObradaiObavestenje(ex.Message);
            }

            if (uspesnaPrijava)
                if (pocetnaLozinka)
                    Response.Redirect("~/aaaWebForms/aaaKorisnik.aspx");
                else
                    Response.Redirect("~/WebForms/Default.aspx");

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



        private void ddlFunkcija_Popuni()
        {     
         

            BioEnWcf.IWcfBioEn client = new BioEnWcf.WcfBioEnClient();
            BioEnWcf.FunkcijaVratiRequest zahtev = new BioEnWcf.FunkcijaVratiRequest();
            BioEnWcf.FunkcijaVratiResponse odgovor = new BioEnWcf.FunkcijaVratiResponse();


            try
            {

                odgovor = client.FunkcijaVrati(zahtev);
            }
            catch (Exception ex)
            {

                PrikaziObavestenje("Грешка у процедури FunkcijaVrati  при позиву методе FunkcijaVrati из сервиса!  \\n\\n" + ex.Message);
                return;
            }
            try
            {
                if (odgovor.FunkcijaVratiResult.dtGreska.Rows.Count > 0)
                    PrikaziObavestenje(odgovor.FunkcijaVratiResult.dtGreska.Rows[0][0].ToString());
                else
                {
                    ddlFunkcija.DataSource = odgovor.FunkcijaVratiResult.dtFunkcijaVrati.Rows;   
                    ddlFunkcija.DataValueField = "IDFunkcija";
                    ddlFunkcija.DataTextField = "NazivFunkcije";
                    ddlFunkcija.DataBind();

                   
                }
            }
            catch (Exception ex)
            {
                PrikaziObavestenje("Грешка у апликацији у методи FunkcijaVrati!  \\n\\n " + ex.Message);
                return;
            }
        }



        private void PrikaziObavestenje(string poruka)
        {
            try
            {
                poruka = (poruka.Replace("\n", "")).Replace("'", "");
                poruka = (poruka.Replace("\r", "")).Replace("'", "");
                string script = "alert('" + poruka + "');";
                Page page1 = (Page)HttpContext.Current.Handler;
                ScriptManager.RegisterStartupScript(page1, typeof(Page), "PrikaziObavestenje", script, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}