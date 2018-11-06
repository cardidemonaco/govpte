<%@ Page Title="" Language="C#" MasterPageFile="~/ElectionDay.Master" AutoEventWireup="true" CodeBehind="General2018CardiAlysa.aspx.cs" Inherits="govpte.General2018CardiAlysa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:GridView ID="gvRaces" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvRaces_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="Race">
                <ItemTemplate>
                    <table>
                        <tr>
                            <td colspan="2">
                                <asp:HyperLink ID="linkRace" runat="server" Font-Size="Larger" style="font-weight: 700">[linkRace]</asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblCandidate" runat="server" style="font-weight: 700"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblCandidateVotes" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

</asp:Content>
