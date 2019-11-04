<%@ Page Title="" Language="C#" MasterPageFile="~/ElectionDay.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="govpte.General2019" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel ID="updatePanelPage" runat="server" UpdateMode="Conditional" >
        <ContentTemplate>

        <asp:Timer ID="timerPage" runat="server" Interval="30000" OnTick="timerPage_Tick" ></asp:Timer>

        <asp:CheckBox ID="cbAutoRefresh" runat="server" Text="Auto refresh every 30 seconds" Checked="true" AutoPostBack="true" OnCheckedChanged="cbAutoRefresh_CheckedChanged" />

        <p>
            <a target="_blank" href="http://18.221.153.194/m27/results.html">Official Macomb County Website - Race List</a>
        </p>

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

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
