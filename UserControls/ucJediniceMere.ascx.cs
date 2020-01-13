using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BieEnWeb.UserControls
{
    public partial class ucJediniceMere : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Ucitaj();
        }

        protected void btnSacuvaj_OnClick(object sender, EventArgs e)
        {
            SacuvajPodatke();
        }

        protected void gvJedMere_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        protected void gvJedMere_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvJedMere.PageIndex = e.NewPageIndex;

            /*  int? IdRegBr = 0;
              try
              {
                  IdRegBr = Convert.ToInt32(ddlRegBr.SelectedValue);

              }

              catch (Exception)
              {
                  IdRegBr = null;
                  // PrikaziObavestenje("Нисте одабрали регистарски број");

                  PopuniEstetskiPvozilo();


                  tbBrDoz.Text = "";
                  tbIdKomitent.Text = "";
                  tbNazivKomitenta.Text = "";
                  tbjmbg.Text = "";
                  tbUlicaIBroj.Text = "";
                  tbOpstina.Text = "";

                  return;
              }*/

           Ucitaj();

        }


        private void Ucitaj()
        {
            TraceLogging.TraceLogger.trace(this.GetType(), (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this, "");


        //    wcfRezervacijaPM.IwcfRezervacijaPM client = new wcfRezervacijaPM.IwcfRezervacijaPMClient();


            BioEnWcf.IWcfBioEn client = new BioEnWcf.WcfBioEnClient();
            BioEnWcf.JedinicaMereVratiRequest zahtev = new BioEnWcf.JedinicaMereVratiRequest();
            BioEnWcf.JedinicaMereVratiResponse odgovor = new BioEnWcf.JedinicaMereVratiResponse();

            try
            {
                odgovor = client.JedinicaMereVrati(zahtev);
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);

                ObradaiObavestenje("Greška u proceduri JedinicaMereVrati pri pozivu servisa!  \\n\\n" + ex.Message);
                return;
            }

            try
            {
                if (odgovor.JedinicaMereVratiResult.dtGreska.Rows.Count > 0)
                    ObradaiObavestenje(odgovor.JedinicaMereVratiResult.dtGreska.Rows[0][0].ToString());
                else
                {
                    if (odgovor.JedinicaMereVratiResult.dtJedinicaMereVrati.Rows.Count == 0)
                    {
                        ObradaiObavestenje("Ne postoje podaci za tražene parametre !");
                        return;
                    }

                    Session["gvJedMere"] = odgovor.JedinicaMereVratiResult.dtJedinicaMereVrati;//ovo se koristi kod sortiranja
                    gvJedMere.DataSource = odgovor.JedinicaMereVratiResult.dtJedinicaMereVrati.Rows;
                    gvJedMere.DataBind();

                    lblUkupanBroj.Text = "Ukupan broj: " + odgovor.JedinicaMereVratiResult.dtJedinicaMereVrati.Rows.Count;
                    Label2.Text = "Pregled podataka o Jedinicama mere";

                    //tanjaovde
                  /*  foreach (GridViewRow red in gvJedMere.Rows)
                    {
                        TextBox NazJedMere = (TextBox)red.FindControl("tbNazJedMere");
                        NazJedMere.Text = NazJedMere.Text.Replace('.', ',');
                    }*/
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                ObradaiObavestenje("Greška u aplikaciji u metodi Ucitaj.!  \\n\\n " + ex.Message);
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
        

        protected void SacuvajPodatke()
        {
            BioEnWcf.IWcfBioEn client = new BioEnWcf.WcfBioEnClient();
            BioEnWcf.dsUlaz ulaz = new BioEnWcf.dsUlaz();
            BioEnWcf.dsUlaz.dtdboRow red = ulaz.dtdbo.NewdtdboRow();

            red = ulaz.dtdbo.NewdtdboRow();

            //IDKorisnik Unos i Promena
            int SysKorisnik = int.Parse((Session["Korisnik_IDKorisnik"]).ToString());
            red.IdKorisnikUnos = SysKorisnik;
           
            red.PunNaziv = tbNazivJedMere.Text;
            red.SkraceniNaziv = tbSkracNazJedMere.Text;
            
            ulaz.dtdbo.AdddtdboRow(red);
            BioEnWcf.JediniceMerePromenaPodatakaRequest zahtev = new BioEnWcf.JediniceMerePromenaPodatakaRequest(ulaz);
            BioEnWcf.JediniceMerePromenaPodatakaResponse odgovor = new BioEnWcf.JediniceMerePromenaPodatakaResponse();

            try
            {
                odgovor = client.JediniceMerePromenaPodataka(zahtev);
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                ObradaiObavestenje("Greška u proceduri JediniceMerePromenaPodataka pri pozivu servisa!  \\n\\n" + ex.Message);
                return;
            }
            try
            {
                if (odgovor.JediniceMerePromenaPodatakaResult.dtGreska.Rows.Count > 0)
                {
                    ObradaiObavestenje(odgovor.JediniceMerePromenaPodatakaResult.dtGreska.Rows[0][0].ToString());
                    tbNazivJedMere.Text = ""; tbSkracNazJedMere.Text = "";
                }
                else
                {
                    ObradaiObavestenje("Uspešno sačuvani podaci o Jedinici mere!");
                  //  btnSacuvaj.Enabled = false;
                    Ucitaj();
                    tbNazivJedMere.Text = ""; tbSkracNazJedMere.Text = "";
                    
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                ObradaiObavestenje("Greška u aplikaciji u metodi JediniceMerePromenaPodataka!  \\n\\n" + ex.Message);
                return;

            }
        }


        protected void btnObrisi_Click(object sender, EventArgs e)
        {
            GridViewRow redUGridu = (GridViewRow)(((Button)sender).Parent).Parent;
            int rowIndex = redUGridu.RowIndex;

            int IdJedMere = int.Parse(((Label)gvJedMere.Rows[rowIndex].FindControl("lblIdJedMere")).Text);
            int IdKorisnikPromene = int.Parse(Session["Korisnik_IDKorisnik"].ToString());

            BioEnWcf.IWcfBioEn client = new BioEnWcf.WcfBioEnClient();
            BioEnWcf.dsUlaz ulaz = new BioEnWcf.dsUlaz();
            BioEnWcf.dsUlaz.dtdboRow red = ulaz.dtdbo.NewdtdboRow();

            red = ulaz.dtdbo.NewdtdboRow();

            red.IdKorisnikUnos = IdKorisnikPromene;
            red.Id = IdJedMere;
  
            ulaz.dtdbo.AdddtdboRow(red);
            BioEnWcf.JMBrisiRequest zahtev = new BioEnWcf.JMBrisiRequest(ulaz);
            BioEnWcf.JMBrisiResponse odgovor = new BioEnWcf.JMBrisiResponse();


            try
            {
                odgovor = client.JMBrisi(zahtev);
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                ObradaiObavestenje("Greška u proceduri JMBrisi pri pozivu servisa!  \\n\\n" + ex.Message);
                return;
            }
            try
            {
                if (odgovor.JMBrisiResult.dtGreska.Rows.Count > 0)
                {
                   
                    ObradaiObavestenje(odgovor.JMBrisiResult.dtGreska.Rows[0][0].ToString());
                    return;
                }
                else
                {
                    ObradaiObavestenje("Uspešno obrisan podatak o Jedinici mere!");
                 //   btnSacuvaj.Enabled = false;
                    Ucitaj();
                  //  tbNazivJedMere.Text = ""; tbSkracNazJedMere.Text = "";
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                   , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                ObradaiObavestenje("Greška u aplikaciji u metodi JMBrisi!  \\n\\n" + ex.Message);
                return;
            }
        }

        protected void btnIzmeni_Click(object sender, EventArgs e)
        {
            GridViewRow redUGridu = (GridViewRow)(((Button)sender).Parent).Parent;
            int rowIndex = redUGridu.RowIndex;

            for (int i = 0; i < gvJedMere.Rows.Count; i++)
            {
                GridViewRow redGrid = gvJedMere.Rows[i];
                int red = redGrid.RowIndex;
                if (rowIndex == red)
                {
                    ((TextBox)(gvJedMere.Rows[i].FindControl("tbSkracNazJedMere"))).ReadOnly = false;
                    ((TextBox)(gvJedMere.Rows[i].FindControl("tbNazJedMere"))).ReadOnly = false;

                    ((Button)(gvJedMere.Rows[i].FindControl("btnSacuvajIzmene"))).Enabled = true;
                    ((Button)(gvJedMere.Rows[i].FindControl("btnOtkaziIzmene"))).Enabled = true;
                    ((Button)(gvJedMere.Rows[i].FindControl("btnIzmeni"))).Enabled = false;
                }
            }
        }

        protected void btnSacuvajIzmene_Click(object sender, EventArgs e)
        {

           

            GridViewRow redUGridu = (GridViewRow)(((Button)sender).Parent).Parent;
            int rowIndex = redUGridu.RowIndex;

            int IdJedMere = int.Parse(((Label)gvJedMere.Rows[rowIndex].FindControl("lblIdJedMere")).Text);

            string SkraceniNazivJM = ((TextBox)gvJedMere.Rows[rowIndex].FindControl("tbSkracNazJedMere")).Text.ToString();
            if (SkraceniNazivJM == "" || SkraceniNazivJM == " ")
            {
                ObradaiObavestenje("Neophodno je uneti skraćeni naziv jedinice mere!");
                return;
            }

            string JedinicaMere = ((TextBox)gvJedMere.Rows[rowIndex].FindControl("tbNazJedMere")).Text.ToString();
            if (JedinicaMere == "" || JedinicaMere == " ")
            {
                ObradaiObavestenje("Neophodno je uneti pun naziv jedinice mere!");
                return;
            }

            int IDKorisnikPromene = int.Parse(Session["Korisnik_IDKorisnik"].ToString());

            if (IzmeniJM(IdJedMere, SkraceniNazivJM, JedinicaMere,  IDKorisnikPromene))
            {
                ObradaiObavestenje("Uspešno izmenjen podatak o jedinici mere!");
                Ucitaj();
            }
        }

        private bool IzmeniJM(int IdJedMere, string SkraceniNazivJM, string JedinicaMere,  int IDKorisnikPromene)
        {

            BioEnWcf.IWcfBioEn client = new BioEnWcf.WcfBioEnClient();
            BioEnWcf.dsUlaz ulaz = new BioEnWcf.dsUlaz();
            BioEnWcf.dsUlaz.dtdboRow red = ulaz.dtdbo.NewdtdboRow();

            red = ulaz.dtdbo.NewdtdboRow();

            /*   int SysKorisnik = int.Parse((Session["Korisnik_IDKorisnik"]).ToString());
               red.IdKorisnikUnos = SysKorisnik;*/

            red.Id = IdJedMere;
            red.SkraceniNaziv = SkraceniNazivJM;
            red.PunNaziv = JedinicaMere;
            red.IdKorisnikUnos = IDKorisnikPromene;

            ulaz.dtdbo.AdddtdboRow(red);
            BioEnWcf.JediniceMerePromenaPodatakaRequest zahtev = new BioEnWcf.JediniceMerePromenaPodatakaRequest(ulaz);
            BioEnWcf.JediniceMerePromenaPodatakaResponse odgovor = new BioEnWcf.JediniceMerePromenaPodatakaResponse();



            try
            {
                odgovor = client.JediniceMerePromenaPodataka(zahtev);
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                ObradaiObavestenje("Greška u proceduri JediniceMerePromenaPodataka pri pozivu servisa!  \\n\\n" + ex.Message);
                return false;
            }
            try
            {
                if (odgovor.JediniceMerePromenaPodatakaResult.dtGreska.Rows.Count > 0)
                {
                    ObradaiObavestenje(odgovor.JediniceMerePromenaPodatakaResult.dtGreska.Rows[0][0].ToString());
                  //  tbNazivJedMere.Text = ""; tbSkracNazJedMere.Text = "";
                    return false;
                }
                else
                {
                    ObradaiObavestenje("Uspešno sačuvani podaci o Jedinici mere!");
                 //   btnSacuvaj.Enabled = false;
                    Ucitaj();
                  //  tbNazivJedMere.Text = ""; tbSkracNazJedMere.Text = "";
                    return true;

                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                ObradaiObavestenje("Greška u aplikaciji u metodi JediniceMerePromenaPodataka!  \\n\\n" + ex.Message);
                return false;

            }
        }


        protected void btnOtkaziIzmene_Click(object sender, EventArgs e)
        {
            GridViewRow redUGridu = (GridViewRow)(((Button)sender).Parent).Parent;
            int rowIndex = redUGridu.RowIndex;

            for (int i = 0; i < gvJedMere.Rows.Count; i++)
            {

                GridViewRow redGrid = gvJedMere.Rows[i];
                int red = redGrid.RowIndex;
                if (rowIndex == red)
                {
                    ((TextBox)(gvJedMere.Rows[i].FindControl("tbSkracNazJedMere"))).ReadOnly = true;
                    ((TextBox)(gvJedMere.Rows[i].FindControl("tbNazJedMere"))).ReadOnly = true;

                    ((Button)(gvJedMere.Rows[i].FindControl("btnSacuvajIzmene"))).Enabled = false;
                    ((Button)(gvJedMere.Rows[i].FindControl("btnOtkaziIzmene"))).Enabled = false;
                    ((Button)(gvJedMere.Rows[i].FindControl("btnIzmeni"))).Enabled = true;
                }
            }
        }

        protected void tbNazivJedMere_TextChanged(object sender, EventArgs e)
        {
            string s; 
            s = tbNazivJedMere.Text;
            tbNazivJedMere.Text = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
        }

        protected void tbSkracNazJedMere_TextChanged(object sender, EventArgs e)
        {
            string s; 
            s = tbSkracNazJedMere.Text;
            tbSkracNazJedMere.Text = s.ToLower();   //System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(s.ToLowerInvariant());    //ToTitleCase(s.ToLower());
        }
    }
}