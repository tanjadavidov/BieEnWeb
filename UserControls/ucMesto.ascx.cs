using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BieEnWeb.UserControls
{
    public partial class ucMesto : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !Page.IsCallback)
            {
                //gv.Visible = true;
                PaUnosMesta.Visible = false;
                PaU.Visible = false;
                Ucitaj();
            

            }
        }


        protected void Ucitaj()
        {

            TraceLogging.TraceLogger.trace(this.GetType(), (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this, "");

            BioEnWcf.IWcfBioEn client = new BioEnWcf.WcfBioEnClient();

            BioEnWcf.MestoVratiRequest zahtev = new BioEnWcf.MestoVratiRequest();
            BioEnWcf.MestoVratiResponse odgovor = new BioEnWcf.MestoVratiResponse();

            try
            {
                odgovor = client.MestoVrati(zahtev);
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                ObradaiObavestenje("Greška u proceduri kod vraćanja grida za Mesta u metodi MestoVrati!  \\n\\n" + ex.Message);
                return;
            }

            try
            {
                if (odgovor.MestoVratiResult.dtGreska.Rows.Count > 0)
                    ObradaiObavestenje(odgovor.MestoVratiResult.dtGreska.Rows[0][0].ToString());
                else
                {

                    if (odgovor.MestoVratiResult.dtMestoVrati.Rows.Count == 0)
                    {
                        ObradaiObavestenje("Ne postoje podaci za tražene parametre !");
                        return;
                    }

                    //    Session["gv"] = odgovor.DrzavaVratiResult.dtDrzavaVrati;//ovo se koristi kod sortiranja
                    gv.DataSource = odgovor.MestoVratiResult.dtMestoVrati.Rows;
                    gv.DataBind();

                    lblUkupanBroj.Text = "Ukupan broj: " + odgovor.MestoVratiResult.dtMestoVrati.Rows.Count;




                   





                }

            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                ObradaiObavestenje("Greška u proceduri kod vraćanja grida za Mesta u metodi MestoVrati!  \\n\\n " + ex.Message);
                return;
            }

        }


        protected void btnPromeniBrojMesta_OnClick(object sender, EventArgs e)
        {

            /*     if (tbBrojMesta.Text == "" || tbBrojMesta.Text == " ")
               {
                   ObradaiObavestenje("Unesite broj koliko mesta želite da prikažete/unesete  ");
                   return;
               }
               //int idMesto = 0;
               //try
               //{
               //    idMesto = int.Parse(tbIdMesto.Text);
               //}
               //catch (Exception)
               //{
               //    idMesto = 0;

               //    return;
               //}

               try
               {
                   if (int.Parse(tbBrojMesta.Text) < 0)
                   {
                       ObradaiObavestenje("Unesite broj veći ili jednak nuli!");
                       return;
                   }
               }
               catch
               {
                   ObradaiObavestenje("Neispravan oblik unosa!");
                   return;
               }

               int broj = int.Parse(tbBrojMesta.Text);

               //  SmestajneJediniceVrati(idMesto, broj); //grid za podatke o smestajnim jedinicama
              MestaVrati(broj);*/
        }


        protected void MestaVrati(int broj)
        {

            /*    TraceLogging.TraceLogger.trace(this.GetType(), (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this, "");

                BioEnWcf.IWcfBioEn client = new BioEnWcf.WcfBioEnClient();

                BioEnWcf.dsUlaz ulaz = new BioEnWcf.dsUlaz();
                BioEnWcf.dsUlaz.dtdboRow red1 = ulaz.dtdbo.NewdtdboRow();
                BioEnWcf.dsUlaz.dtInt32Row red2 = ulaz.dtInt32.NewdtInt32Row();


                //red1.Id = idMesto;
                //ulaz.dtdbo.AdddtdboRow(red1);

                red2.Broj = broj;
                ulaz.dtInt32.AdddtInt32Row(red2);

                BioEnWcf.MestoVrati_brojRequest zahtev = new BioEnWcf.MestoVrati_brojRequest(ulaz);
                BioEnWcf.MestoVrati_brojResponse odgovor = new BioEnWcf.MestoVrati_brojResponse();

                try
                {
                    odgovor = client.MestoVrati_broj(zahtev);
                }
                catch (Exception ex)
                {
                    ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                        , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                    ObradaiObavestenje("Greška u proceduri kod vraćanja grida za Mesta u metodi MestoVrati_broj!  \\n\\n" + ex.Message);
                    return;
                }

                try
                {
                    if (odgovor.MestoVrati_brojResult.dtGreska.Rows.Count > 0)
                    {
                        ObradaiObavestenje(odgovor.MestoVrati_brojResult.dtGreska.Rows[0][0].ToString());
                        return;
                    }
                    else
                    {
                        gv.DataSource = odgovor.MestoVrati_brojResult.dtMestoVrati_broj.Rows;
                        gv.DataBind();


                        tbBrojMesta.Visible = true;
                        int brojRedova = 0;
                        brojRedova = odgovor.MestoVrati_brojResult.dtMestoVrati_broj.Rows.Count;
                        lblUkupanBroj.Text = "Ukupan broj: " + brojRedova;

                        tbBrojMesta.Text = brojRedova.ToString();
                        tbBrojMesta.Visible = true;


                       //string IdOpstina = ((Label)gv.Rows[rowIndex].FindControl("lblIdOpstina")).Text.ToString();
                       // if (IdOpstina == "0")
                       // {
                       //     ddlOpstina.SelectedIndex = 0;
                       // }
                       // else
                       // {
                       //     ddlOpstina.SelectedValue = ((Label)gv.Rows[rowIndex].FindControl("lblIdOpstina")).Text.ToString();
                       // }
                    }

                }
                catch (Exception ex)
                {
                    ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                        , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                    ObradaiObavestenje("Greška u proceduri kod vraćanja grida za Mesta u metodi MestoVrati_broj!  \\n\\n " + ex.Message);
                    return;
                }*/

        }


        protected void MestaVratiU(int broj)
        {

            TraceLogging.TraceLogger.trace(this.GetType(), (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this, "");

            BioEnWcf.IWcfBioEn client = new BioEnWcf.WcfBioEnClient();

            BioEnWcf.dsUlaz ulaz = new BioEnWcf.dsUlaz();
            //  BioEnWcf.dsUlaz.dtdboRow red1 = ulaz.dtdbo.NewdtdboRow();
            BioEnWcf.dsUlaz.dtInt32Row red2 = ulaz.dtInt32.NewdtInt32Row();



            //red1.Id = idMesto;
            //ulaz.dtdbo.AdddtdboRow(red1);


            red2.Broj = broj;
            ulaz.dtInt32.AdddtInt32Row(red2);

            BioEnWcf.MestoVrati_brojRequest zahtev = new BioEnWcf.MestoVrati_brojRequest(ulaz);
            BioEnWcf.MestoVrati_brojResponse odgovor = new BioEnWcf.MestoVrati_brojResponse();

            try
            {
                odgovor = client.MestoVrati_broj(zahtev);
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                ObradaiObavestenje("Greška u proceduri kod vraćanja grida za Mesta u metodi MestoVrati_broj!  \\n\\n" + ex.Message);
                return;
            }

            try
            {
                if (odgovor.MestoVrati_brojResult.dtGreska.Rows.Count > 0)
                {
                    ObradaiObavestenje(odgovor.MestoVrati_brojResult.dtGreska.Rows[0][0].ToString());
                    return;
                }
                else
                {
                    gvU.DataSource = odgovor.MestoVrati_brojResult.dtMestoVrati_broj.Rows;
                    gvU.DataBind();


                    tbBrojMestaU.Visible = true;
                    int brojRedova = 0;
                    brojRedova = odgovor.MestoVrati_brojResult.dtMestoVrati_broj.Rows.Count;
                    lblUkupanBroj.Text = "Ukupan broj: " + brojRedova;

                    tbBrojMestaU.Text = brojRedova.ToString();



                    /*  string IdOpstina = ((Label)gv.Rows[rowIndex].FindControl("lblIdOpstina")).Text.ToString();
                      if (IdOpstina == "0")
                      {
                          ddlOpstina.SelectedIndex = 0;
                      }
                      else
                      {
                          ddlOpstina.SelectedValue = ((Label)gv.Rows[rowIndex].FindControl("lblIdOpstina")).Text.ToString();
                      }*/
                }

            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                ObradaiObavestenje("Greška u proceduri kod vraćanja grida za Mesta u metodi MestoVrati_broj!  \\n\\n " + ex.Message);
                return;
            }

        }



        protected void btnObrisi_Click(object sender, EventArgs e)
        {
            GridViewRow redUGridu = (GridViewRow)(((Button)sender).Parent).Parent;
            int rowIndex = redUGridu.RowIndex;
           // System.Data.DataRow deleteRow = redUGridu.RowIndex;

            ////
         /*  BioEnWcf.dsIzlaz izlaz = new BioEnWcf.dsIzlaz();
            BioEnWcf.dsIzlaz.dtMestoVratiRow red1 = izlaz.dtMestoVrati.NewdtMestoVratiRow();

            izlaz.dtMestoVrati.AdddtMestoVratiRow(red1);
   
              System.Data.DataRow deleteRow = izlaz.dtMestoVrati[rowIndex];
            
            izlaz.dtMestoVrati.Rows.Remove(deleteRow);
            gv.DataSource = izlaz.dtMestoVrati;
            gv.DataBind();*/

            //   BioEnWcf.MestoVratiRequest zahtev = new BioEnWcf.MestoVratiRequest();
            // BioEnWcf.MestoVratiResponse odgovor = new BioEnWcf.MestoVratiResponse();
            ////

            System.Data.DataTable table = new DataTable("Mesto");
            int Id = int.Parse(((Label)gv.Rows[rowIndex].FindControl("lblIdMesto")).Text);
            int IdKorisnikPromene = int.Parse(Session["Korisnik_IDKorisnik"].ToString());

            BioEnWcf.IWcfBioEn client = new BioEnWcf.WcfBioEnClient();
            BioEnWcf.dsUlaz ulaz = new BioEnWcf.dsUlaz();
            BioEnWcf.dsUlaz.dtdboRow red = ulaz.dtdbo.NewdtdboRow();


          
            red = ulaz.dtdbo.NewdtdboRow();

            red.IdKorisnikUnos = IdKorisnikPromene;
            red.Id = Id;
            red.VremeUnos = DateTime.Now;


            /////////////////
            //int rowIndex = ((GridViewRow)(((LinkButton)sender).Parent).Parent).RowIndex;

            //System.Data.DataRow deleteRow = izlaz.dtSmestajneJediniceVrati[rowIndex];
            //izlaz.dtSmestajneJediniceVrati.Rows.Remove(deleteRow);

            //gvSmestajneJedinice.DataSource = izlaz.dtSmestajneJediniceVrati;
            //gvSmestajneJedinice.DataBind();

            //gvSmestajneJedinice.Visible = true;

            //tbObjZaKat.Text = gvSmestajneJedinice.Rows.Count.ToString();
            //lblStringR.Text = " ";



            ////////////////////////////



            ulaz.dtdbo.AdddtdboRow(red);
            BioEnWcf.MestoBrisiRequest zahtev = new BioEnWcf.MestoBrisiRequest(ulaz);
            BioEnWcf.MestoBrisiResponse odgovor = new BioEnWcf.MestoBrisiResponse();
 

            try
            {
                odgovor = client.MestoBrisi(zahtev);
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                ObradaiObavestenje("Greška u proceduri MestoBrisi pri pozivu servisa!  \\n\\n" + ex.Message);
                return;
            }
            try
            {
                if (odgovor.MestoBrisiResult.dtGreska.Rows.Count > 0)
                {

                    ObradaiObavestenje(odgovor.MestoBrisiResult.dtGreska.Rows[0][0].ToString());
                    return;
                }
                else
                {
                    ObradaiObavestenje("Uspešno obrisan podatak o Mestu!");
                    //   btnSacuvaj.Enabled = false;
                    Ucitaj();
                    //  tbNazivJedMere.Text = ""; tbSkracNazJedMere.Text = "";
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                   , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                ObradaiObavestenje("Greška u aplikaciji u metodi MestoBrisi!  \\n\\n" + ex.Message);
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

                    ((DropDownList)(gv.Rows[i].FindControl("ddlOpstina"))).Enabled = true;

                    //  ((TextBox)(gv.Rows[i].FindControl("tbPttBroj"))).ReadOnly = false; 
                    ((TextBox)(gv.Rows[i].FindControl("tbPttBroj"))).Enabled = true;
                    ((TextBox)(gv.Rows[i].FindControl("tbNazivMesta"))).Enabled = true;

                    ((Button)(gv.Rows[i].FindControl("btnSacuvajIzmene"))).Enabled = true;
                    ((Button)(gv.Rows[i].FindControl("btnOtkaziIzmene"))).Enabled = true;
                    ((Button)(gv.Rows[i].FindControl("btnIzmeni"))).Enabled = false;
                }
            }
        }

        protected void btnSacuvajIzmene_Click(object sender, EventArgs e)
        {
            GridViewRow redUGridu = (GridViewRow)(((Button)sender).Parent).Parent;
            int rowIndex = redUGridu.RowIndex;

            int IdMesto = int.Parse(((Label)gv.Rows[rowIndex].FindControl("lblIdMesto")).Text);
           // int IdOpstina = int.Parse(((DropDownList)gvSmestajneJedinice.Rows[i].FindControl("ddlVrstaSmestaja")).SelectedValue);
            int IdOpstina = int.Parse(((DropDownList)gv.Rows[rowIndex].FindControl("ddlOpstina")).SelectedValue);

         //   int IdOpstina = int.Parse(((Label)gv.Rows[rowIndex].FindControl("lblIdOpstina")).Text);
            int PttBroj = int.Parse(((TextBox)gv.Rows[rowIndex].FindControl("tbPttBroj")).Text);


            string NazivMesta = ((TextBox)gv.Rows[rowIndex].FindControl("tbNazivMesta")).Text.ToString();
            if (NazivMesta == "" || NazivMesta == " ")
            {
                ObradaiObavestenje("Neophodno je uneti naziv mesta!");
                return;
            }

            

            int IDKorisnikPromene = int.Parse(Session["Korisnik_IDKorisnik"].ToString());

            if (IzmeniMesto(IdMesto, IdOpstina, PttBroj, NazivMesta, IDKorisnikPromene))
            {
                ObradaiObavestenje("Uspešno izmenjen podatak o mestu!");
                Ucitaj();
            }
        }


        private bool IzmeniMesto(int IdMesto, int IdOpstina, int PttBroj, string NazivMesta, int IDKorisnikPromene)
        {

            BioEnWcf.IWcfBioEn client = new BioEnWcf.WcfBioEnClient();
            BioEnWcf.dsUlaz ulaz = new BioEnWcf.dsUlaz();
            BioEnWcf.dsUlaz.dtdboRow red = ulaz.dtdbo.NewdtdboRow();

            red = ulaz.dtdbo.NewdtdboRow();

            red.Id = IdMesto;
            red.id1 = IdOpstina;
            red.broj = PttBroj;
            red.Naziv = NazivMesta;
            red.IdKorisnikUnos = IDKorisnikPromene;
            red.VremeUnos = DateTime.Now;

            ulaz.dtdbo.AdddtdboRow(red);
            BioEnWcf.MestoPromenaPodatakaRequest zahtev = new BioEnWcf.MestoPromenaPodatakaRequest(ulaz);
            BioEnWcf.MestoPromenaPodatakaResponse odgovor = new BioEnWcf.MestoPromenaPodatakaResponse();

            try
            {
                odgovor = client.MestoPromenaPodataka(zahtev);
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                ObradaiObavestenje("Greška u proceduri MestoPromenaPodataka pri pozivu servisa!  \\n\\n" + ex.Message);
                return false;
            }
            try
            {
                if (odgovor.MestoPromenaPodatakaResult.dtGreska.Rows.Count > 0)
                {
                    ObradaiObavestenje(odgovor.MestoPromenaPodatakaResult.dtGreska.Rows[0][0].ToString());
                    return false;
                }
                else
                {
                    ObradaiObavestenje("Uspešno sačuvani podaci o mestu!");
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
                ObradaiObavestenje("Greška u aplikaciji u metodi MestoPromenaPodataka!  \\n\\n" + ex.Message);
                return false;

            }
        }



        protected void btnSacuvaj1_Click(object sender, EventArgs e)
        {
            SacuvajPodatke();
            btnSacuvaj1.Enabled = true;
        }

        protected void btnOtkaziIzmene_Click(object sender, EventArgs e)
        {
            GridViewRow redUGridu = (GridViewRow)(((Button)sender).Parent).Parent;
            int rowIndex = redUGridu.RowIndex;

            DesableGrid(rowIndex);
            
        }

        protected void DesableGrid(int redIndex)
        {

           

            for (int i = 0; i < gv.Rows.Count; i++)
            {

                GridViewRow redGrid = gv.Rows[i];
                int red = redGrid.RowIndex;
                if (redIndex == red)
                {
                    ((DropDownList)(gv.Rows[i].FindControl("ddlOpstina"))).Enabled = false;

                    ((TextBox)(gv.Rows[i].FindControl("tbPttBroj"))).Enabled = false;
                    ((TextBox)(gv.Rows[i].FindControl("tbNazivMesta"))).Enabled = false;

                    ((Button)(gv.Rows[i].FindControl("btnSacuvajIzmene"))).Enabled = false;
                    ((Button)(gv.Rows[i].FindControl("btnOtkaziIzmene"))).Enabled = false;
                    ((Button)(gv.Rows[i].FindControl("btnIzmeni"))).Enabled = true;
                }
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

            red.broj = int.Parse(tbPttBroj.Text);
                 
            red.Naziv = tbNazivMesta.Text;
            red.id1 = int.Parse(ddlOpstina.SelectedValue);
            red.VremeUnos = DateTime.Now;
            

            ulaz.dtdbo.AdddtdboRow(red);
            BioEnWcf.MestoPromenaPodatakaRequest zahtev = new BioEnWcf.MestoPromenaPodatakaRequest(ulaz);
            BioEnWcf.MestoPromenaPodatakaResponse odgovor = new BioEnWcf.MestoPromenaPodatakaResponse();

            try
            {
                odgovor = client.MestoPromenaPodataka(zahtev);
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                ObradaiObavestenje("Greška u proceduri MestoPromenaPodataka pri pozivu servisa!  \\n\\n" + ex.Message);
                return;
            }
            try
            {
                if (odgovor.MestoPromenaPodatakaResult.dtGreska.Rows.Count > 0)
                {
                    ObradaiObavestenje(odgovor.MestoPromenaPodatakaResult.dtGreska.Rows[0][0].ToString());
                    tbNazivMesta.Text = "";
                }
                else
                {
                    ObradaiObavestenje("Uspešno sačuvani podaci o mestu!");
                    //  btnSacuvaj.Enabled = false;
                    Ucitaj();
                    clearTextInPaMesto(paMesto);

                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                ObradaiObavestenje("Greška u aplikaciji u metodi MestoPromenaPodataka!  \\n\\n" + ex.Message);
                return;

            }
        }


        protected void btnUnesi_onclick(object sender, EventArgs e)
        {
            //   pnMesto.Visible = true;
            btnUnesi.Enabled = false;


            BioEnWcf.IWcfBioEn client = new BioEnWcf.WcfBioEnClient();

            BioEnWcf.dsUlaz ulaz = new BioEnWcf.dsUlaz();
            // BioEnWcf.dsUlaz.dtdboRow red2 = ulaz.dtdbo.NewdtdboRow();
            BioEnWcf.dsUlaz.dtInt32Row red3 = ulaz.dtInt32.NewdtInt32Row();


            //Grid
            BioEnWcf.dsUlaz.dtdboRow red2 = ulaz.dtdbo.NewdtdboRow();
            for (int i = 0; i < this.gvU.Rows.Count; i++)
            {
                if (((DropDownList)gvU.Rows[i].FindControl("ddlOpstina")).SelectedValue == "0" || /*((TextBox)gvU.Rows[i].FindControl("lblIdMesto")).Text == "" ||*/
               ((TextBox)gvU.Rows[i].FindControl("tbPttBroj")).Text == "" || ((TextBox)gvU.Rows[i].FindControl("tbNazivMesta")).Text == "")
                {
                    ObradaiObavestenje("Uneti: Opštinu, PttBroj, NazivMesta!");
                    return;
                }
            }
            /*  for (int i = 0; i < this.gvU.Rows.Count; i++)
              {
                  if (((TextBox)gvU.Rows[i].FindControl("tbCetiriZvezdice")).Text == "" &&
                 ((TextBox)gvU.Rows[i].FindControl("tbTriZvezdice")).Text == "" &&
                 ((TextBox)gvU.Rows[i].FindControl("tbDveZvezdice")).Text == "" &&
                 ((TextBox)gvU.Rows[i].FindControl("tbJednaZvezdica")).Text == "")
                  {
                      ObradaiObavestenje("Унети бар једну врсту категорије !");
                      return;
                  }
              }*/

            /*   for (int i = 0; i < this.gvSmestajneJedinice.Rows.Count; i++)
               {
                   if (((TextBox)gvSmestajneJedinice.Rows[i].FindControl("tbCetiriZvezdice")).Text == "")
                       ((TextBox)gvSmestajneJedinice.Rows[i].FindControl("tbCetiriZvezdice")).Text = "0";
                   if (((TextBox)gvSmestajneJedinice.Rows[i].FindControl("tbTriZvezdice")).Text == "")
                       ((TextBox)gvSmestajneJedinice.Rows[i].FindControl("tbTriZvezdice")).Text = "0";
                   if (((TextBox)gvSmestajneJedinice.Rows[i].FindControl("tbDveZvezdice")).Text == "")
                       ((TextBox)gvSmestajneJedinice.Rows[i].FindControl("tbDveZvezdice")).Text = "0";
                   if (((TextBox)gvSmestajneJedinice.Rows[i].FindControl("tbJednaZvezdica")).Text == "")
                       ((TextBox)gvSmestajneJedinice.Rows[i].FindControl("tbJednaZvezdica")).Text = "0";
               }*/
            // deo za svaki red grida gvSmestajneJedinice 

            for (int i = 0; i < this.gvU.Rows.Count; i++)
            {
                red2 = ulaz.dtdbo.NewdtdboRow();

                red2.Id = int.Parse(((Label)gvU.Rows[i].FindControl("lblIdOpstina")).Text.ToString());
                red2.id1 = int.Parse(((Label)gvU.Rows[i].FindControl("lblIdMesto")).Text.ToString()); //int.Parse(tbIDZahteva.Text);
                red2.broj = int.Parse(((TextBox)gvU.Rows[i].FindControl("tbPttBroj")).Text);
                red2.Naziv = ((TextBox)gvU.Rows[i].FindControl("tbNazivMesta")).Text;
                //red2.CetiriZvezdice = int.Parse(((TextBox)gvU.Rows[i].FindControl("tbCetiriZvezdice")).Text);
                //red2.TriZvezdice = int.Parse(((TextBox)gvU.Rows[i].FindControl("tbTriZvezdice")).Text);
                //red2.DveZvezdice = int.Parse(((TextBox)gvU.Rows[i].FindControl("tbDveZvezdice")).Text);
                //red2.JednaZvezdica = int.Parse(((TextBox)gvU.Rows[i].FindControl("tbJednaZvezdica")).Text);
                //if (red2.BrojSmestajnihJedinica != red2.CetiriZvezdice + red2.TriZvezdice + red2.DveZvezdice + red2.JednaZvezdica)
                //{
                //    PrikaziObavestenje("Број смештајних јединица не одговара збиру свих категорија у реду" + " " + (i + 1).ToString());
                //    return;
                //}
                ulaz.dtdbo.AdddtdboRow(red2);
            }
        }


        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                BioEnWcf.IWcfBioEn client = new BioEnWcf.WcfBioEnClient();
                BioEnWcf.OpstinaVratiRequest zahtev = new BioEnWcf.OpstinaVratiRequest();
                BioEnWcf.OpstinaVratiResponse odgovor = new BioEnWcf.OpstinaVratiResponse();

                try
                {
                    odgovor = client.OpstinaVrati(zahtev);
                }
                catch (Exception ex)
                {

                    ObradaiObavestenje("Greška u proceduri kod vraćanja grida za ddlOpstine u metodi OpstinaVrati iz servisa!  \\n\\n" + ex.Message);
                    return;
                }
                try
                {
                    if (odgovor.OpstinaVratiResult.dtGreska.Rows.Count > 0)
                        ObradaiObavestenje(odgovor.OpstinaVratiResult.dtGreska.Rows[0][0].ToString());
                    else
                    {

                        DropDownList ddlOpstina = (e.Row.FindControl("ddlOpstina") as DropDownList);
                        ddlOpstina.DataSource = odgovor.OpstinaVratiResult.dtOpstinaVrati.Rows;
                        ddlOpstina.DataValueField = "idOpstina";
                        ddlOpstina.DataTextField = "NazivOpstine";
                        ddlOpstina.DataBind();

                        string idOpstina = (e.Row.FindControl("lblIdOpstina") as Label).Text;
                        ddlOpstina.Items.FindByValue(idOpstina).Selected = true;





                    }
                }
                catch (Exception ex)
                {
                    ObradaiObavestenje("Greška u proceduri u metodi OpstinaVrati!  \\n\\n " + ex.Message);
                    return;
                }
            }
        }

        protected void gvU_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                BioEnWcf.IWcfBioEn client = new BioEnWcf.WcfBioEnClient();
                BioEnWcf.OpstinaVratiRequest zahtev = new BioEnWcf.OpstinaVratiRequest();
                BioEnWcf.OpstinaVratiResponse odgovor = new BioEnWcf.OpstinaVratiResponse();

                try
                {
                    odgovor = client.OpstinaVrati(zahtev);
                }
                catch (Exception ex)
                {

                    ObradaiObavestenje("Greška u proceduri kod vraćanja grida za ddlOpstine u metodi OpstinaVrati iz servisa!  \\n\\n" + ex.Message);
                    return;
                }
                try
                {
                    if (odgovor.OpstinaVratiResult.dtGreska.Rows.Count > 0)
                        ObradaiObavestenje(odgovor.OpstinaVratiResult.dtGreska.Rows[0][0].ToString());
                    else
                    {

                        DropDownList ddlOpstina = (e.Row.FindControl("ddlOpstina") as DropDownList);
                        ddlOpstina.DataSource = odgovor.OpstinaVratiResult.dtOpstinaVrati.Rows;
                        ddlOpstina.DataValueField = "idOpstina";
                        ddlOpstina.DataTextField = "NazivOpstine";
                        ddlOpstina.DataBind();

                        string idOpstina = (e.Row.FindControl("lblIdOpstina") as Label).Text;
                        ddlOpstina.Items.FindByValue(idOpstina).Selected = true;


                    }
                }
                catch (Exception ex)
                {
                    ObradaiObavestenje("Greška u proceduri u metodi OpstinaVrati!  \\n\\n " + ex.Message);
                    return;
                }
            }
        }


        protected void gvUnosMesta_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                BioEnWcf.IWcfBioEn client = new BioEnWcf.WcfBioEnClient();
                BioEnWcf.OpstinaVratiRequest zahtev = new BioEnWcf.OpstinaVratiRequest();
                BioEnWcf.OpstinaVratiResponse odgovor = new BioEnWcf.OpstinaVratiResponse();

                try
                {
                    odgovor = client.OpstinaVrati(zahtev);
                }
                catch (Exception ex)
                {

                    ObradaiObavestenje("Greška u proceduri kod vraćanja grida za ddlOpstine u metodi OpstinaVrati iz servisa!  \\n\\n" + ex.Message);
                    return;
                }
                try
                {
                    if (odgovor.OpstinaVratiResult.dtGreska.Rows.Count > 0)
                        ObradaiObavestenje(odgovor.OpstinaVratiResult.dtGreska.Rows[0][0].ToString());
                    else
                    {

                        DropDownList ddlOpstina = (e.Row.FindControl("ddlOpstina") as DropDownList);
                        ddlOpstina.DataSource = odgovor.OpstinaVratiResult.dtOpstinaVrati.Rows;
                        ddlOpstina.DataValueField = "idOpstina";
                        ddlOpstina.DataTextField = "NazivOpstine";
                        ddlOpstina.DataBind();

                        string idOpstina = (e.Row.FindControl("lblIdOpstina") as Label).Text;
                        ddlOpstina.Items.FindByValue(idOpstina).Selected = true;

                    }
                }
                catch (Exception ex)
                {
                    ObradaiObavestenje("Greška u proceduri u metodi OpstinaVrati!  \\n\\n " + ex.Message);
                    return;
                }
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

        protected void btnPromeniBrojMestaU_OnClick(object sender, EventArgs e)
        {
            if (tbBrojMestaU.Text == "" || tbBrojMestaU.Text == " ")
            {
                ObradaiObavestenje("Unesite broj koliko mesta želite da prikažete/unesete  ");
                return;
            }
            //int idMesto = 0;
            //try
            //{
            //    idMesto = int.Parse(((Label)gvU.Rows[i].FindControl("lblIdMesto")).Text.ToString());  // int.Parse(tbIdMesto.Text);
            //}
            //catch (Exception)
            //{
            //    idMesto = 0;

            //    return;
            //}
            try
            {
                if (int.Parse(tbBrojMestaU.Text) < 0)
                {
                    ObradaiObavestenje("Unesite broj veći ili jednak nuli!");
                    return;
                }
            }
            catch
            {
                ObradaiObavestenje("Neispravan oblik unosa!");
                return;
            }

            int broj = int.Parse(tbBrojMestaU.Text);

            //  SmestajneJediniceVrati(idMesto, broj); //grid za podatke o smestajnim jedinicama
            MestaVratiU(broj);
        }

        protected void btnUnesiNovoMesto_onclick(object sender, EventArgs e)
        {
            // PaUnosMesta.Visible = true;
            PaUnosMesta1.Visible = true;
            ddlOPstina_popuni();
        }


        protected void ddlOPstina_popuni()
        {       

            BioEnWcf.IWcfBioEn client = new BioEnWcf.WcfBioEnClient();
            BioEnWcf.OpstinaVratiRequest zahtev = new BioEnWcf.OpstinaVratiRequest();
            BioEnWcf.OpstinaVratiResponse odgovor = new BioEnWcf.OpstinaVratiResponse();

            try
            {
                odgovor = client.OpstinaVrati(zahtev);
            }
            catch (Exception ex)
            {

                ObradaiObavestenje("Greška u proceduri kod vraćanja grida za ddlOpstine u metodi OpstinaVrati iz servisa!  \\n\\n" + ex.Message);
                return;
            }


            try
            {
                if (odgovor.OpstinaVratiResult.dtGreska.Rows.Count > 0)
                    ObradaiObavestenje(odgovor.OpstinaVratiResult.dtGreska.Rows[0][0].ToString());
                else
                {

                    //  DropDownList ddlOpstina = (e.Row.FindControl("ddlOpstina") as DropDownList);
                    ddlOpstina.DataSource = odgovor.OpstinaVratiResult.dtOpstinaVrati.Rows;
                    ddlOpstina.DataValueField = "idOpstina";
                    ddlOpstina.DataTextField = "NazivOpstine";
                    ddlOpstina.DataBind();

                  //  string idOpstina = (e.Row.FindControl("lblIdOpstina") as Label).Text;
                  //  ddlOpstina.Items.FindByValue(idOpstina).Selected = true;


                }
            }
            catch (Exception ex)
            {
                ObradaiObavestenje("Greška u proceduri u metodi OpstinaVrati!  \\n\\n " + ex.Message);
                return;
            }
        }

        protected void btnOdustani_Click(object sender, EventArgs e)
        {
            PaUnosMesta1.Visible = false;
        }


        private void clearTextInPaUnosMesta1(Panel PaUnosMesta1)
        {
            foreach (Control c in PaUnosMesta1.Controls)
            {
                if (c is TextBox)
                {
                    TextBox questionTextBox = c as TextBox;
                    if (questionTextBox != null)
                    {
                        questionTextBox.Text = "";
                    }

                    ddlOpstina.SelectedIndex = 0;
//                    ddlOpstina.Items.Clear();

                 /*  DropDownList questionddl = c as DropDownList;
                    if (questionddl is null)
                    {
                            questionddl.Items.Clear(); 
                     
                    }*/
                }
            }
        }


        private void clearTextInPaMesto(Panel PaMesto)
        {
            foreach (Control c in PaUnosMesta1.Controls)
            {
                if (c is TextBox)
                {
                    TextBox questionTextBox = c as TextBox;
                    if (questionTextBox != null)
                    {
                        questionTextBox.Text = "";
                    }

                    ddlOpstina.SelectedIndex = 0;
                    //                    ddlOpstina.Items.Clear();

                    /*  DropDownList questionddl = c as DropDownList;
                       if (questionddl is null)
                       {
                               questionddl.Items.Clear(); 

                       }*/
                }
            }
        }

        protected void btnOcisti_Click(object sender, EventArgs e)
        {
            //  PaUnosMesta1.Visible = false;
            clearTextInPaUnosMesta1(PaUnosMesta1);
        }


        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv.PageIndex = e.NewPageIndex;

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


    }
}