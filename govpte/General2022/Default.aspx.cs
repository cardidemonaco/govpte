using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

using HtmlAgilityPack;

namespace govpte
{
    public partial class General2022 : System.Web.UI.Page
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
            List<string> urls = new List<string>
                {
                    "https://electionresults.macombgov.org/m37/5.html", //Governor
                     "https://electionresults.macombgov.org/m37/6.html", //SOS
                     "https://electionresults.macombgov.org/m37/7.html", //AG
                     "https://electionresults.macombgov.org/m37/9.html", //US Rep 10th
                     "https://electionresults.macombgov.org/m37/12.html", //State Senate 10th
                     "https://electionresults.macombgov.org/m37/13.html", //State Senate 11th
                     "https://electionresults.macombgov.org/m37/14.html", //State Senate 12th
                     "https://electionresults.macombgov.org/m37/18.html", //State House 11th
                     "https://electionresults.macombgov.org/m37/19.html", //State House 12th
                     "https://electionresults.macombgov.org/m37/20.html", //State House 13th
                     "https://electionresults.macombgov.org/m37/21.html", //State House 14th
                     "https://electionresults.macombgov.org/m37/22.html", //State House 57th
                     "https://electionresults.macombgov.org/m37/23.html", //State House 58th
                     "https://electionresults.macombgov.org/m37/26.html", //State House 61st
                     "https://electionresults.macombgov.org/m37/35.html", //County Executive
                     "https://electionresults.macombgov.org/m37/37.html", //County 2nd
                     "https://electionresults.macombgov.org/m37/39.html", //County 4th
                     "https://electionresults.macombgov.org/m37/40.html", //County 5th
                     "https://electionresults.macombgov.org/m37/41.html", //County 6th
                     "https://electionresults.macombgov.org/m37/43.html", //County 8th
                     "https://electionresults.macombgov.org/m37/44.html", //County 9th
                     "https://electionresults.macombgov.org/m37/45.html", //County 10th
                     "https://electionresults.macombgov.org/m37/46.html", //County 11th
                     "https://electionresults.macombgov.org/m37/47.html", //County 12th
                     "https://electionresults.macombgov.org/m37/48.html", //County 13th
                     "https://electionresults.macombgov.org/m37/52.html", //Clinton Twp Trustee
                     "https://electionresults.macombgov.org/m37/53.html", //Washington Twp Trustee
                     "https://electionresults.macombgov.org/m37/54.html", //MSC
                     "https://electionresults.macombgov.org/m37/56.html", //COA
                     "https://electionresults.macombgov.org/m37/58.html", //Macomb Circuit
                     "https://electionresults.macombgov.org/m37/64.html", //MCC
                     "https://electionresults.macombgov.org/m37/80.html", //Romeo President
                     "https://electionresults.macombgov.org/m37/95.html", //Eastpointe Schools
                     "https://electionresults.macombgov.org/m37/116.html", //Roseville Schools
                     "https://electionresults.macombgov.org/m37/107.html", //Mt Clemens Schools
                     "https://electionresults.macombgov.org/m37/123.html", //Prop 1
                     "https://electionresults.macombgov.org/m37/124.html", //Prop 2
                     "https://electionresults.macombgov.org/m37/125.html", //Prop 3
                     "https://electionresults.macombgov.org/m37/126.html", //Veterans
                     "https://electionresults.macombgov.org/m37/127.html", //SMART
                     "https://electionresults.macombgov.org/m37/132.html", //Chesterfield Marijuana
                     "https://electionresults.macombgov.org/m37/133.html", //Chesterfield Library
                     "https://electionresults.macombgov.org/m37/142.html" //Macomb ISD
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