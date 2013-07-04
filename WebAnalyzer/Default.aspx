<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAnalyzer._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %>.</h1>
                <h2>Your guide to learning Analyzers!</h2>
            </hgroup>
            <p>
                Analyzers are a huge part of Sitecore 7 and this tool will help you understand how they work.
            </p>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>Enter some text:</h3>
    <div>
        <asp:TextBox ID="BaseText" runat="server" Height="189px" Width="984px" AutoPostBack="True" OnTextChanged="BaseText_TextChanged" TextMode="MultiLine"></asp:TextBox>
    </div>
    <div>
        <asp:TextBox ID="Result" runat="server" Height="379px" Width="981px" TextMode="MultiLine"></asp:TextBox>
    </div>
    <asp:Button ID="Go" runat="server" Text="Run" OnClick="Go_Click" />
</asp:Content>
