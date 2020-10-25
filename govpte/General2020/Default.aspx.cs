using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

using HtmlAgilityPack;

namespace govpte
{
    public partial class General2020 : System.Web.UI.Page
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
                    "https://electionresults.macombgov.org/m31/80.html", //CT Trustee

                    "https://electionresults.macombgov.org/m31/23.html", //County Prosecutor
                    "https://electionresults.macombgov.org/m31/24.html", //County Sheriff
                    "https://electionresults.macombgov.org/m31/25.html", //County Clerk
                    "https://electionresults.macombgov.org/m31/26.html", //County Treasurer
                    "https://electionresults.macombgov.org/m31/27.html", //County Public Works Commissioner

                    "https://electionresults.macombgov.org/m31/28.html", //Commission 1 Nard
                    "https://electionresults.macombgov.org/m31/29.html", //Commission 2 Xiong
                    "https://electionresults.macombgov.org/m31/31.html", //Commission 4 Chi
                    "https://electionresults.macombgov.org/m31/32.html", //Commission 5 Mijac
                    "https://electionresults.macombgov.org/m31/36.html", //Commission 9 Wallace
                    "https://electionresults.macombgov.org/m31/37.html", //Commission 10 Leonetti
                    "https://electionresults.macombgov.org/m31/38.html", //Commission 11 Haugh
                    "https://electionresults.macombgov.org/m31/39.html", //Commission 12 Matuzak

                    "https://electionresults.macombgov.org/m31/9.html", //State House 18 Hertel
                    "https://electionresults.macombgov.org/m31/10.html", //State House 20 Steenland
                    "https://electionresults.macombgov.org/m31/11.html", //State House 24 Woodman
                    "https://electionresults.macombgov.org/m31/12.html", //State House 25 Shannon
                    "https://electionresults.macombgov.org/m31/13.html", //State House 28 Stone
                    "https://electionresults.macombgov.org/m31/15.html", //State House 31 Sowerby

                    "https://electionresults.macombgov.org/m31/44.html", //CT Supervisor
                    "https://electionresults.macombgov.org/m31/55.html", //CT Clerk
                    "https://electionresults.macombgov.org/m31/66.html", //CT Treasurer

                    "https://electionresults.macombgov.org/m31/88.html", //County Probate Court
                    "https://electionresults.macombgov.org/m31/90.html", //38th District Court

                    "https://electionresults.macombgov.org/m31/97.html", //MCC Trustee

                    "https://electionresults.macombgov.org/m31/137.html", //Eastpointe Community Schools
                    "https://electionresults.macombgov.org/m31/126.html", //Roseville Community Schools

                    "https://electionresults.macombgov.org/m31/156.html", //SCS Charter Amendment (H of the P)
                    "https://electionresults.macombgov.org/m31/157.html", //SH Charter Amendment (4 yr terms)
                    "https://electionresults.macombgov.org/m31/158.html", //SH Signature requirements
                    "https://electionresults.macombgov.org/m31/159.html", //Utica marijuana
                    "https://electionresults.macombgov.org/m31/161.html" //Warren Charter Amendment (term limits)
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