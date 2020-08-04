using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

using HtmlAgilityPack;

namespace govpte
{
    public partial class Primary2020 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateGridView();

                EnableOrDisableTimer();
            }
        }

        private void PopulateGridView()
        {
            //Override pages to use on election site
            //  Eastpointe Council election is ranked-choice voting, so it is not listed
            List<string> urls = new List<string>
                {
                    "http://18.221.153.194/m30/467-DEM_682-REP.html", //Clinton Twp - Trustee
                    "http://18.221.153.194/m30/178-DEM_7007-REP.html", //Delegate to County Convention - Eastpointe Precinct 2
                    "http://18.221.153.194/m30/465-DEM_680-REP.html", //Clinton Twp - Clerk
                    "http://18.221.153.194/m30/33-DEM_247-REP.html", //County Commissioner - 5th District
                    "http://18.221.153.194/m30/37-DEM_251-REP.html", //County Commissioner - 9th District
                    "http://18.221.153.194/m30/38-DEM_252-REP.html", //County Commissioner - 10th District
                    "http://18.221.153.194/m30/40-DEM_254-REP.html", //County Commissioner - 12th District
                    "http://18.221.153.194/m30/24-DEM_238-REP.html", //Prosecuting Attorney
                    "http://18.221.153.194/m30/439.html", //Judge of Probate Court - Incumbent Position
                    "http://18.221.153.194/m30/14-DEM_228-REP.html", //Representative in State Legislature - 18th District
                    "http://18.221.153.194/m30/15-DEM_229-REP.html", //Representative in State Legislature - 22nd District
                    "http://18.221.153.194/m30/20-DEM_234-REP.html", //Representative in State Legislature - 31st District
                    "http://18.221.153.194/m30/29-DEM_243-REP.html",
                    "http://18.221.153.194/m30/30-DEM_244-REP.html",
                    "http://18.221.153.194/m30/32-DEM_246-REP.html",
                    "http://18.221.153.194/m30/41-DEM_255-REP.html",
                    "http://18.221.153.194/m30/16-DEM_230-REP.html",
                    "http://18.221.153.194/m30/18-DEM_232-REP.html",
                    "http://18.221.153.194/m30/440.html", //Mount Clemens Commission
                    "http://18.221.153.194/m30/444.html",
                    "http://18.221.153.194/m30/445.html",
                    "http://18.221.153.194/m30/443.html"
                };

            gvRaces.DataSource = urls;
            gvRaces.DataBind();
        }

        protected void gvRaces_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string drv = (string)e.Row.DataItem;

                    HyperLink linkRace = (HyperLink)e.Row.FindControl("linkRace");
                    Label lblBarGraph = (Label)e.Row.FindControl("lblBarGraph");
                    Label lblCandidate = (Label)e.Row.FindControl("lblCandidate");
                    Label lblCandidateVotes = (Label)e.Row.FindControl("lblCandidateVotes");

                    var url = drv;
                    var web = new HtmlWeb();
                    var doc = web.Load(url);

                    HtmlNode hncRace = doc.DocumentNode.SelectSingleNode("//*[@class=\"racetitle1\"]");
                    HtmlNodeCollection hncRaces = doc.DocumentNode.SelectNodes("//*[@class=\"racetable\"]");
                    int i = 2;
                    foreach (HtmlNode hnRace in hncRaces)
                    {
                        linkRace.Text = hncRace.InnerText;
                        linkRace.NavigateUrl = drv;
                        linkRace.Target = "_blank";

                        IEnumerable<HtmlNode> hncCandidates = hnRace.SelectNodes($"/html[1]/body[1]/center[2]/table[{i}]//*[@class=\"candname\"]");
                        foreach (HtmlNode hnCandidate in hncCandidates)
                        {
                            lblCandidate.Text += hnCandidate.InnerText + "<br />";
                        }

                        HtmlNodeCollection hncCandidatesVoteTotals = hnRace.SelectNodes($"/html[1]/body[1]/center[2]/table[{i}]//*[@class=\"vtotal\"]");
                        int sum = 0;
                        foreach (HtmlNode hnCandidateVoteTotal in hncCandidatesVoteTotals)
                        {
                            lblCandidateVotes.Text += hnCandidateVoteTotal.InnerText + "<br />";
                            sum += Convert.ToInt32(hnCandidateVoteTotal.InnerText.Replace(",", ""));
                        }

                        lblCandidate.Text += "<br />";
                        lblCandidateVotes.Text += "<br />";

                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        protected void cbAutoRefresh_CheckedChanged(object sender, EventArgs e)
        {
            EnableOrDisableTimer();
        }

        private void EnableOrDisableTimer()
        {
            //If checkbox checked, refresh the UpdatePanel once a minute...
            if (cbAutoRefresh.Checked)
                timerPage.Enabled = true;
            else
                timerPage.Enabled = false;
        }

        protected void timerPage_Tick(object sender, EventArgs e)
        {
            PopulateGridView();
            updatePanelPage.Update();
        }
    }
}