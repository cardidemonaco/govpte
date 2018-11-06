using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HtmlAgilityPack;

namespace govpte
{
    public partial class General2018CardiAlysa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Override pages to use on election site
            List<string> urls = new List<string>
            {
                "http://18.221.153.194/m24/301.html",
                "http://18.221.153.194/m24/302.html",
                "http://18.221.153.194/m24/303.html",
                "http://18.221.153.194/m24/304.html",
                "http://18.221.153.194/m24/305.html",
                "http://18.221.153.194/m24/308.html",
                "http://18.221.153.194/m24/311.html",
                "http://18.221.153.194/m24/321.html",
                "http://18.221.153.194/m24/322.html",
                "http://18.221.153.194/m24/323.html",
                "http://18.221.153.194/m24/324.html",
                "http://18.221.153.194/m24/325.html",
                "http://18.221.153.194/m24/326.html",
                "http://18.221.153.194/m24/329.html",
                "http://18.221.153.194/m24/344.html",
                "http://18.221.153.194/m24/345.html",
                "http://18.221.153.194/m24/346.html",
                "http://18.221.153.194/m24/347.html",
                "http://18.221.153.194/m24/348.html",
                "http://18.221.153.194/m24/349.html",
                "http://18.221.153.194/m24/361.html",
                "http://18.221.153.194/m24/405.html",
                "http://18.221.153.194/m24/407.html",
                "http://18.221.153.194/m24/416.html",
                "http://18.221.153.194/m24/417.html",
                "http://18.221.153.194/m24/418.html"
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