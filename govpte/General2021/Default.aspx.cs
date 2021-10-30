using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

using HtmlAgilityPack;

namespace govpte
{
    public partial class General2021 : System.Web.UI.Page
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
                    //Eastpointe Proposals
                    "https://electionresults.macombgov.org/m33/32.html", //Mayoral Primary
                    "https://electionresults.macombgov.org/m33/33.html", //Library Millage

                    //Candidates
                    "https://electionresults.macombgov.org/m33/4.html", //State Senator
                    "https://electionresults.macombgov.org/m33/6.html", //Center Line Council
                    "https://electionresults.macombgov.org/m33/15.html", //Mount Clemens Commission
                    "https://electionresults.macombgov.org/m33/16.html", //New Baltimore Mayor
                    "https://electionresults.macombgov.org/m33/26.html", //SCS Council
                    "https://electionresults.macombgov.org/m33/27.html", //Sterling Heights Mayor
                    "https://electionresults.macombgov.org/m33/28.html", //Sterling Heights Council
                    "https://electionresults.macombgov.org/m33/31.html", //Grosse Pointe Shores judge

                    //Proposals 
                    "https://electionresults.macombgov.org/m33/34.html", //Mount Clemens $1,500 -> $15,000
                    "https://electionresults.macombgov.org/m33/35.html", //New Baltimore Fire Millage
                    "https://electionresults.macombgov.org/m33/36.html", //New Baltimore Road Bond $30M
                    "https://electionresults.macombgov.org/m33/37.html", //Fraser Public Schools Headlee Override
                    "https://electionresults.macombgov.org/m33/38.html", //Romeo schools bond
                    "https://electionresults.macombgov.org/m33/39.html" //Romeo schools headlee override
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