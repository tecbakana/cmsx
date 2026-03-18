<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucadrotator.ascx.cs" Inherits="controles_ucadrotator" %>

<asp:UpdatePanel ID="pnlAd" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Timer ID="tmrAd" runat="server" Interval="2000" OnTick="tmrAd_Tick" />
        <asp:AdRotator ID="adflex" runat="server" Width="600" />
    </ContentTemplate>
</asp:UpdatePanel>