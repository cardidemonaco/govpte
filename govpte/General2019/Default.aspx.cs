using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

using HtmlAgilityPack;

namespace govpte
{
    public partial class General2019 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Override pages to use on election site
            //  Eastpointe Council election is ranked-choice voting, so it is not listed
            List<string> urls = new List<string>
            {
                "http://18.221.153.194/m27/5.html", //Eastpointe Mayor
                "http://18.221.153.194/m27/51.html", //Eastpointe Charter Amendment
                "http://18.221.153.194/m27/52.html", //Eastpointe Community Schools Sinking Fund 
                "http://18.221.153.194/m27/16.html", //Mt. Clemens Mayor
                "http://18.221.153.194/m27/17.html", //Mt. Clemens City Commission
                "http://18.221.153.194/m27/21.html", //Roseville City Council
                "http://18.221.153.194/m27/23.html", //St. Clair Shores Council
                "http://18.221.153.194/m27/25.html", //Sterling Heights Council
                "http://18.221.153.194/m27/28.html", //Warren Mayor
                "http://18.221.153.194/m27/29.html", //Warren Clerk
                "http://18.221.153.194/m27/30.html", //Warren Treasurer
                "http://18.221.153.194/m27/31.html", //Warren At-Large Council
                "http://18.221.153.194/m27/32.html", //Warren District 1
                "http://18.221.153.194/m27/34.html", //Warren District 3
                "http://18.221.153.194/m27/35.html" //Warren District 4
            };

            gvRaces.DataSource = urls;
            gvRaces.DataBind();
        }

        protected void gvRaces_RowDataBound(object sender, GridViewRowEventArgs e)
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
    }
}