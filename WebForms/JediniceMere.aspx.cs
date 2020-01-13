﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BieEnWeb.WebForms
{
    public partial class JediniceMere : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !IsCallback)
            {
                ((Label)Page.Master.FindControl("lblNaslov")).Text = "Unos jedinice mere ";
                aaaProvera();
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
            if (Session["Korisnik_PocetnaLozinka"] == null)
                Response.Redirect("~/aaaWebForms/aaaPrijava.aspx");
            if (Session["Korisnik_PocetnaLozinka"].ToString() == "1")
                Response.Redirect("~/aaaWebForms/aaaKorisnik.aspx");


            //Провера права приступа

            bool imaPravo = false;


            BioEnWcf.IWcfBioEn client = new BioEnWcf.WcfBioEnClient();
            BioEnWcf.dsUlaz ulaz = new BioEnWcf.dsUlaz();
            BioEnWcf.dsUlaz.dtaaaRow red = ulaz.dtaaa.NewdtaaaRow();


            red.idKorisnik = int.Parse(Session["Korisnik_IDKorisnik"].ToString());
            red.idFunkcija = 4;   // idFunkcija 
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
                ObradaiObavestenje("Greška u metodi aaaProvera pri pozivu procedure KorisnikPravoNaFunkciju!  \\n\\n" + ex.Message);
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
                ObradaiObavestenje("Greška u aplikaciji u metodi aaaProvera!  \\n\\n" + ex.Message);
                return;
            }

            if (!imaPravo)
                Response.Redirect("~/WebForms/AccessDenied.aspx");
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

    }
}