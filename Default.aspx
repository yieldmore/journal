<%@ Page Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="journal._Default1" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <asp:Repeater runat="server" ID="IssuesCtl">
        <ItemTemplate><li><a href="/issues/default.aspx/<%#DataBinder.Eval(Container.DataItem, "IssueNumber")%>"><%#DataBinder.Eval(Container.DataItem, "IssueNumber")%></a></li></ItemTemplate>
    </asp:Repeater>
    <asp:Repeater runat="server" ID="TitlesCtl">
        <ItemTemplate><li><a href="/issues/default.aspx/<%=Issue.IssueNumber%>/<%#Container.DataItem%>"><%#Container.DataItem%></a></li></ItemTemplate>
    </asp:Repeater>
    <div runat="server" ID="TitleCtl" visible="false">
        <b><%=Issue.Author%></b>
        <%=Issue.Content%>
    </div>
</asp:Content>