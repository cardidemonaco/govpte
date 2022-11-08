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
                    <asp:MenuItem NavigateUrl="~/General2022/Default.aspx" Text="Cardi &amp; Alysa's Ballot" Value="Cardi &amp; Alysa's Ballot"></asp:MenuItem>
                </Items>
            </asp:Menu>


        </div>
    </form>
</body>
</html>
