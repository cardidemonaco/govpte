using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HtmlAgilityPack;

namespace govpte
{
    public partial class General2018StateLegislatureCritical : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Override pages to use on election site
            List<string> urls = new List<string>
            {
                "http://18.221.153.194/m24/307.html",
                "http://18.221.153.194/m24/309.html",
                "http://18.221.153.194/m24/313.html",
                "http://18.221.153.194/m24/314.html",
                "http://18.221.153.194/m24/315.html",
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