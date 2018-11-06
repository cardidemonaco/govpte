<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="govpte.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <h1>It's Election Day! </h1>

            <asp:Menu ID="Menu2" runat="server" Font-Size="Larger">
                <Items>
                    <asp:MenuItem NavigateUrl="~/General2018/General2018CardiAlysa.aspx" Text="Cardi &amp; Alysa's Ballot" Value="Cardi &amp; Alysa's Ballot"></asp:MenuItem>
                </Items>
            </asp:Menu>
            <br />
            <br />

            <asp:Menu ID="Menu1" runat="server" Font-Size="Large">
                <Items>
                    <asp:MenuItem NavigateUrl="~/General2018/CountyCommissionAll.aspx" Text="County (all)" Value="County Commission (all)"></asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/General2018/CountyCommissionCritical.aspx" Text="County Commission (critical)" Value="County Commission (critical)"></asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/General2018/StateLegislature.aspx" Text="State Legislature (all)" Value="State Legislature (all)"></asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/General2018/StateLegislatureCritical.aspx" Text="State Legislature (critical)" Value="State Legislature (critical)"></asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/General2018/Judicial.aspx" Text="Judicial" Value="Judicial"></asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/General2018/Education.aspx" Text="Education" Value="Education"></asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/General2018/Proposals.aspx" Text="Proposals" Value="Proposals"></asp:MenuItem>
                </Items>
            </asp:Menu>

        </div>
    </form>
</body>
</html>
