using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BieEnWeb.UserControls
{
    public partial class ucDrzava : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack && !Page.IsCallback)
            {
                //  btnUnesi.Visible = true;
                pnlUnos.Visible = false;
                Ucitaj();
            }




        }

        protected void gv_SelectedIndexChanged(object sender, GridViewPageEventArgs e)
        {

            //  gv.PageIndex = e.NewPageIndex; //gv_PageIndexChanging

            Ucitaj();
        }


        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv.PageIndex = e.NewPageIndex;

            Ucitaj();
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


        private void Ucitaj()
        {
            TraceLogging.TraceLogger.trace(this.GetType(), (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this, "");


            BioEnWcf.IWcfBioEn client = new BioEnWcf.WcfBioEnClient();
            BioEnWcf.DrzavaVratiRequest zahtev = new BioEnWcf.DrzavaVratiRequest();
            BioEnWcf.DrzavaVratiResponse odgovor = new BioEnWcf.DrzavaVratiResponse();

            try
            {
                odgovor = client.DrzavaVrati(zahtev);
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);

                ObradaiObavestenje("Greška u proceduri Drzava pri pozivu servisa!  \\n\\n" + ex.Message);
                return;
            }

            try
            {
                if (odgovor.DrzavaVratiResult.dtGreska.Rows.Count > 0)
                    ObradaiObavestenje(odgovor.DrzavaVratiResult.dtGreska.Rows[0][0].ToString());
                else
                {
                    if (odgovor.DrzavaVratiResult.dtDrzavaVrati.Rows.Count == 0)
                    {
                        ObradaiObavestenje("Ne postoje podaci za tražene parametre !");
                        return;
                    }

                    Session["gv"] = odgovor.DrzavaVratiResult.dtDrzavaVrati;//ovo se koristi kod sortiranja
                    gv.DataSource = odgovor.DrzavaVratiResult.dtDrzavaVrati.Rows;
                    gv.DataBind();

                    lblUkupanBroj.Text = "Ukupan broj: " + odgovor.DrzavaVratiResult.dtDrzavaVrati.Rows.Count;
                    Label2.Text = "Pregled podataka o Državama";

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

        protected void gv_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        protected void btnObrisi_Click(object sender, EventArgs e)
        {
            GridViewRow redUGridu = (GridViewRow)(((Button)sender).Parent).Parent;
            int rowIndex = redUGridu.RowIndex;

            int Id = int.Parse(((Label)gv.Rows[rowIndex].FindControl("lblIdDrzava")).Text);
            int IdKorisnikPromene = int.Parse(Session["Korisnik_IDKorisnik"].ToString());

            BioEnWcf.IWcfBioEn client = new BioEnWcf.WcfBioEnClient();
            BioEnWcf.dsUlaz ulaz = new BioEnWcf.dsUlaz();
            BioEnWcf.dsUlaz.dtdboRow red = ulaz.dtdbo.NewdtdboRow();

            red = ulaz.dtdbo.NewdtdboRow();

            red.IdKorisnikUnos = IdKorisnikPromene;
            red.Id = Id;
            red.VremeUnos = DateTime.Now;

            ulaz.dtdbo.AdddtdboRow(red);
            BioEnWcf.DrzavaBrisiRequest zahtev = new BioEnWcf.DrzavaBrisiRequest(ulaz);
            BioEnWcf.DrzavaBrisiResponse odgovor = new BioEnWcf.DrzavaBrisiResponse();


            try
            {
                odgovor = client.DrzavaBrisi(zahtev);
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                ObradaiObavestenje("Greška u proceduri DrzavaBrisi pri pozivu servisa!  \\n\\n" + ex.Message);
                return;
            }
            try
            {
                if (odgovor.DrzavaBrisiResult.dtGreska.Rows.Count > 0)
                {

                    ObradaiObavestenje(odgovor.DrzavaBrisiResult.dtGreska.Rows[0][0].ToString());
                    return;
                }
                else
                {
                    ObradaiObavestenje("Uspešno obrisan podatak o Državi!");
                    //   btnSacuvaj.Enabled = false;
                    Ucitaj();
                    //  tbNazivJedMere.Text = ""; tbSkracNazJedMere.Text = "";
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                   , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                ObradaiObavestenje("Greška u aplikaciji u metodi DrzavaBrisi!  \\n\\n" + ex.Message);
                return;
            }
        }

        protected void btnIzmeni_Click(object sender, EventArgs e)
        {
            GridViewRow redUGridu = (GridViewRow)(((Button)sender).Parent).Parent;
            int rowIndex = redUGridu.RowIndex;

            for (int i = 0; i < gv.Rows.Count; i++)
            {
                GridViewRow redGrid = gv.Rows[i];
                int red = redGrid.RowIndex;
                if (rowIndex == red)
                {
                    ((TextBox)(gv.Rows[i].FindControl("tbNazivDrzave"))).ReadOnly = false;

                    ((Button)(gv.Rows[i].FindControl("btnSacuvajIzmene"))).Enabled = true;
                    ((Button)(gv.Rows[i].FindControl("btnOtkaziIzmene"))).Enabled = true;
                    ((Button)(gv.Rows[i].FindControl("btnIzmeni"))).Enabled = false;
                }
            }
        }

        protected void btnSacuvajIzmene_Click(object sender, EventArgs e)
        {
            TraceLogging.TraceLogger.trace(this.GetType(), (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this, "");

            GridViewRow redUGridu = (GridViewRow)(((Button)sender).Parent).Parent;
            int rowIndex = redUGridu.RowIndex;

            int Id = int.Parse(((Label)gv.Rows[rowIndex].FindControl("lblIdDrzava")).Text);
            string NazivDrzave = ((TextBox)gv.Rows[rowIndex].FindControl("tbNazivDrzave")).Text;
             //   ((TextBox)gv.Rows[rowIndex].FindControl("tbNazivDrzave")).Text.ToString();
            if (NazivDrzave == "" || NazivDrzave == " ")
            {
                ObradaiObavestenje("Neophodno je uneti naziv drzave!");
                return;
            }

            int IDKorisnikPromene = int.Parse(Session["Korisnik_IDKorisnik"].ToString());

            if (Izmeni1(Id, NazivDrzave, IDKorisnikPromene))
            {
                ObradaiObavestenje("Uspešno izmenjen podatak o državi!");
                Ucitaj();
            }
        }

           
            private bool Izmeni1(int Id, string NazivDrzave, int IDKorisnikPromene)
     //private void Izmeni(object sender, EventArgs e)
            {
                
                BioEnWcf.IWcfBioEn client = new BioEnWcf.WcfBioEnClient();
                BioEnWcf.dsIzlaz izlaz = new BioEnWcf.dsIzlaz();
                BioEnWcf.dsUlaz ulaz = new BioEnWcf.dsUlaz();
                BioEnWcf.dsUlaz.dtdboRow red = ulaz.dtdbo.NewdtdboRow();
               
                    red = ulaz.dtdbo.NewdtdboRow();

                red.Id = Id;
                red.Naziv = NazivDrzave;
                red.IdKorisnikUnos = IDKorisnikPromene;

            ulaz.dtdbo.AdddtdboRow(red);

            BioEnWcf.DrzavaPromenaPodatakaRequest zahtev = new BioEnWcf.DrzavaPromenaPodatakaRequest(ulaz);
            BioEnWcf.DrzavaPromenaPodatakaResponse odgovor = new BioEnWcf.DrzavaPromenaPodatakaResponse();


            try
            {
                odgovor = client.DrzavaPromenaPodataka(zahtev);
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                ObradaiObavestenje("Greška u proceduri DrzavaPromenaPodataka pri pozivu metode DrzavaPromenaPodataka iz servisa!  \\n\\n" + ex.Message);
                return false;

            }
            try
            {
                if (odgovor.DrzavaPromenaPodatakaResult.dtGreska.Rows.Count > 0)
                {
                    ObradaiObavestenje(odgovor.DrzavaPromenaPodatakaResult.dtGreska.Rows[0][0].ToString());
                     return false;
                }
                else
                {
                    // btnUnesiZahtev.Enabled = false;
                    ObradaiObavestenje("Uspešno izmenjen podatak o državi!");
                    Ucitaj();
                    return true;
                }


            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);

                ObradaiObavestenje("Greška u aplikaciji u metodi DrzavaPromenaPodataka!  \\n\\n " + ex.Message);
                return false;
            }
        }
    protected void btnOtkaziIzmene_Click(object sender, EventArgs e)
        {
            GridViewRow redUGridu = (GridViewRow)(((Button)sender).Parent).Parent;
            int rowIndex = redUGridu.RowIndex;

            for (int i = 0; i < gv.Rows.Count; i++)
            {

                GridViewRow redGrid = gv.Rows[i];
                int red = redGrid.RowIndex;
                if (rowIndex == red)
                {
                    ((TextBox)(gv.Rows[i].FindControl("tbNazivDrzave"))).ReadOnly = true;

                    ((Button)(gv.Rows[i].FindControl("btnSacuvajIzmene"))).Enabled = false;
                    ((Button)(gv.Rows[i].FindControl("btnOtkaziIzmene"))).Enabled = false;
                    ((Button)(gv.Rows[i].FindControl("btnIzmeni"))).Enabled = true;
                }
            }
        }



        protected void tbNaziv_TextChanged(object sender, EventArgs e)
        {
            string s;
            s = tbNaziv.Text;
            tbNaziv.Text = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());   // s.ToLower(); 
        }


        protected void btnSacuvaj_OnClick(object sender, EventArgs e)
        {
            SacuvajPodatke();
            btnUnesi.Enabled = true;
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

            red.Naziv = tbNaziv.Text;

            ulaz.dtdbo.AdddtdboRow(red);
            BioEnWcf.DrzavaPromenaPodatakaRequest zahtev = new BioEnWcf.DrzavaPromenaPodatakaRequest(ulaz);
            BioEnWcf.DrzavaPromenaPodatakaResponse odgovor = new BioEnWcf.DrzavaPromenaPodatakaResponse();

            try
            {
                odgovor = client.DrzavaPromenaPodataka(zahtev);
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                ObradaiObavestenje("Greška u proceduri DrzavaPromenaPodataka pri pozivu servisa!  \\n\\n" + ex.Message);
                return;
            }
            try
            {
                if (odgovor.DrzavaPromenaPodatakaResult.dtGreska.Rows.Count > 0)
                {
                    ObradaiObavestenje(odgovor.DrzavaPromenaPodatakaResult.dtGreska.Rows[0][0].ToString());
                    tbNaziv.Text = ""; 
                }
                else
                {
                    ObradaiObavestenje("Uspešno sačuvani podaci o državi!");
                    //  btnSacuvaj.Enabled = false;
                    Ucitaj();
                    tbNaziv.Text = "";
                  

                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                ObradaiObavestenje("Greška u aplikaciji u metodi DrzavaPromenaPodataka!  \\n\\n" + ex.Message);
                return;

            }
        }


        protected void btnUnesi_onclick(object sender, EventArgs e)
        {
            pnlUnos.Visible = true;
            btnUnesi.Enabled = false;
        }





     /*   protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                BioEnWcf.IWcfBioEn client = new BioEnWcf.WcfBioEnClient();
                BioEnWcf.DrzavaVratiRequest zahtev = new BioEnWcf.DrzavaVratiRequest();
                BioEnWcf.DrzavaVratiResponse odgovor = new BioEnWcf.DrzavaVratiResponse();
              
                try
                {
                    odgovor = client.DrzavaVrati(zahtev);
                }
                catch (Exception ex)
                {

                    ObradaiObavestenje("Грешка у процедури DrzavaVrati при позиву методе DrzavaVrati из сервиса!  \\n\\n" + ex.Message);
                    return;
                }
                try
                {
                    if (odgovor.DrzavaVratiResult.dtGreska.Rows.Count > 0)
                        ObradaiObavestenje(odgovor.DrzavaVratiResult.dtGreska.Rows[0][0].ToString());
                    else
                    {

                        

                    }
                }
                catch (Exception ex)
                {
                    ObradaiObavestenje("Грешка у апликацији у методи DrzavaVrati!  \\n\\n " + ex.Message);
                    return;
                }
            }
        }*/

    }

}