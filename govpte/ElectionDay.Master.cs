using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HtmlAgilityPack;

namespace govpte
{
    public partial class ElectionDay : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Make list of pages to search
            List<string> urls = new List<string>
            {
                "http://18.221.153.194/m24/301.html",
                "http://18.221.153.194/m24/302.html",
                "http://18.221.153.194/m24/303.html",
                "http://18.221.153.194/m24/304.html",
                "http://18.221.153.194/m24/305.html",
                "http://18.221.153.194/m24/306.html",
                "http://18.221.153.194/m24/307.html",
                "http://18.221.153.194/m24/308.html",
                "http://18.221.153.194/m24/309.html",
                "http://18.221.153.194/m24/310.html",
                "http://18.221.153.194/m24/311.html",
                "http://18.221.153.194/m24/312.html",
                "http://18.221.153.194/m24/313.html",
                "http://18.221.153.194/m24/314.html",
                "http://18.221.153.194/m24/315.html",
                "http://18.221.153.194/m24/316.html",
                "http://18.221.153.194/m24/317.html",
                "http://18.221.153.194/m24/318.html",
                "http://18.221.153.194/m24/319.html",
                "http://18.221.153.194/m24/320.html",
                "http://18.221.153.194/m24/321.html",
                "http://18.221.153.194/m24/322.html",
                "http://18.221.153.194/m24/323.html",
                "http://18.221.153.194/m24/324.html",
                "http://18.221.153.194/m24/325.html",
                "http://18.221.153.194/m24/326.html",
                "http://18.221.153.194/m24/327.html",
                "http://18.221.153.194/m24/328.html",
                "http://18.221.153.194/m24/329.html"
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