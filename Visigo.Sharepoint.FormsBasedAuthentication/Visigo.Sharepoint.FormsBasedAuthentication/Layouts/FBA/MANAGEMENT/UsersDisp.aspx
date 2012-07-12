<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UsersDisp.aspx.cs" Inherits="Visigo.Sharepoint.FormsBasedAuthentication.UsersDisp"
    DynamicMasterPageFile="~masterurl/default.master" %>

<%@ Register TagPrefix="wssuc" TagName="ToolBar" Src="~/_controltemplates/ToolBar.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ToolBarButton" Src="~/_controltemplates/ToolBarButton.ascx" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="FBA" Namespace="Visigo.Sharepoint.FormsBasedAuthentication"
    Assembly="Visigo.Sharepoint.FormsBasedAuthentication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9dba9f460226d31d" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    <SharePoint:EncodedLiteral ID="PageTitle" Text="<%$ Resources:FBAPackWebPages, UserMgmt_Title %>"
        EncodeMethod="HtmlEncode" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea"
    runat="server">
   	<a href="../../settings.aspx"><SharePoint:EncodedLiteral ID="EncodedLiteral1" runat="server" text="<%$Resources:wss,settings_pagetitle%>" EncodeMethod="HtmlEncode"/></a>&#32;<SharePoint:ClusteredDirectionalSeparatorArrow ID="ClusteredDirectionalSeparatorArrow1" runat="server" />
    <SharePoint:EncodedLiteral ID="TitleArea" Text="<%$ Resources:FBAPackWebPages, UserMgmt_Title %>"
        EncodeMethod="HtmlEncode" runat="server" />
</asp:Content>
<asp:Content ID="Content5" contentplaceholderid="PlaceHolderTitleBreadcrumb" runat="server">
  <SharePoint:UIVersionedContent ID="UIVersionedContent1" UIVersion="3" runat="server"><ContentTemplate>
	<asp:SiteMapPath
		SiteMapProvider="SPXmlContentMapProvider"
		id="ContentMap"
		SkipLinkText=""
		NodeStyle-CssClass="ms-sitemapdirectional"
		RootNodeStyle-CssClass="s4-die"
		PathSeparator="&#160;&gt; "
		PathSeparatorStyle-CssClass = "s4-bcsep"
		runat="server" />
  </ContentTemplate></SharePoint:UIVersionedContent>
  <SharePoint:UIVersionedContent ID="UIVersionedContent2" UIVersion="4" runat="server"><ContentTemplate>
	<SharePoint:ListSiteMapPath
		runat="server"
		SiteMapProviders="SPSiteMapProvider,SPXmlContentMapProvider"
		RenderCurrentNodeAsLink="false"
		PathSeparator=""
		CssClass="s4-breadcrumb"
		NodeStyle-CssClass="s4-breadcrumbNode"
		CurrentNodeStyle-CssClass="s4-breadcrumbCurrentNode"
		RootNodeStyle-CssClass="s4-breadcrumbRootNode"
		HideInteriorRootNodes="true"
		SkipLinkText="" />
  </ContentTemplate></SharePoint:UIVersionedContent>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderPageDescription" runat="server">
    <asp:PlaceHolder ID="ToolBarPlaceHolder" runat="server">
        <SharePoint:EncodedLiteral ID="DescArea" Text="<%$ Resources:FBAPackWebPages, UserMgmt_Desc %>"
            EncodeMethod="HtmlEncode" runat="server" />
    </asp:PlaceHolder>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <SharePoint:MenuTemplate ID="UserMenu" runat="server">
        <SharePoint:MenuItemTemplate ID="Edit" runat="server" Text="<%$ Resources:FBAPackWebPages, EditContextMenuText %>" ImageUrl="/_layouts/images/edititem.gif"
            ClientOnClickNavigateUrl="UserEdit.aspx?UserName=%USERNAME%" Title="<%$ Resources:FBAPackWebPages, EditContextMenuText %>">
        </SharePoint:MenuItemTemplate>
        <SharePoint:MenuItemTemplate ID="ResetPassword" runat="server" Text="<%$ Resources:FBAPackWebPages, ResetPasswordContextMenuText %>" ImageUrl="/_layouts/images/restore.gif"
            ClientOnClickNavigateUrl="UserResetPassword.aspx?UserName=%USERNAME%" Title="<%$ Resources:FBAPackWebPages, DeleteContextMenuText %>">
        </SharePoint:MenuItemTemplate>
        <SharePoint:MenuItemTemplate ID="Delete" runat="server" Text="<%$ Resources:FBAPackWebPages, DeleteContextMenuText %>" ImageUrl="/_layouts/images/delete.gif"
            ClientOnClickNavigateUrl="UserDelete.aspx?UserName=%USERNAME%" Title="<%$ Resources:FBAPackWebPages, DeleteContextMenuText %>">
        </SharePoint:MenuItemTemplate>
    </SharePoint:MenuTemplate>
        <div style="padding-top: 4px;padding-bottom: 4px;">
            <wssuc:ToolBar ID="onetidNavNodesTB" runat="server">
                <Template_Buttons>
                    <wssuc:ToolBarButton runat="server" Text="<%$ Resources:FBAPackWebPages, NewUserLabelText %>" ID="idNewNavNode" ToolTip="<%$ Resources:FBAPackWebPages, NewUserLabelText %>"
                        NavigateUrl="UserNew.aspx" ImageUrl="/_layouts/images/newitem.gif" AccessKey="U" />
                </Template_Buttons>
            </wssuc:ToolBar>
        </div>
    <div id="SearchControls" runat="server">
    <asp:Label runat="server" ID="lblSearch" Text="<%$ Resources:FBAPackWebPages, SearchLabelText %>" />
    <asp:TextBox ID="SearchText" runat="server"></asp:TextBox><asp:Button ID="Search"
            runat="server" Text="<%$ Resources:FBAPackWebPages, SearchButtonText %>" OnClick="Search_Click" /></div>
    <FBA:FBADataSource runat="server" ID="UserDataSource" ViewName="FBAUsersView" />
    <SharePoint:SPGridView ID="MemberGrid" runat="server" DataSourceID="UserDataSource"
        AutoGenerateColumns="false" AllowPaging="true" PageSize="20" AllowSorting="true">
        <Columns>
            <SharePoint:SPMenuField HeaderText="<%$ Resources:FBAPackWebPages, UserNameColHeaderText %>" TextFields="Name" MenuTemplateId="UserMenu"
                NavigateUrlFields="Name" NavigateUrlFormat="UserEdit.aspx?UserName={0}" TokenNameAndValueFields="USERNAME=Name"
                SortExpression="Name" />
            <SharePoint:SPBoundField DataField="Email" HeaderText="<%$ Resources:FBAPackWebPages, EmailColHeaderText %>" SortExpression="Email">
            </SharePoint:SPBoundField>
            <SharePoint:SPBoundField DataField="Title" HeaderText="<%$ Resources:FBAPackWebPages, FullNameColHeaderText %>" SortExpression="Title">
            </SharePoint:SPBoundField>
            <SharePoint:SPBoundField DataField="Active" HeaderText="<%$ Resources:FBAPackWebPages, ActiveColHeaderText %>" SortExpression="Active">
            </SharePoint:SPBoundField>
            <SharePoint:SPBoundField DataField="Locked" HeaderText="<%$ Resources:FBAPackWebPages, LockedColHeaderText %>" SortExpression="Locked">
            </SharePoint:SPBoundField>
            <SharePoint:SPBoundField DataField="LastLogin" HeaderText="<%$ Resources:FBAPackWebPages, LastLoginColHeaderText %>" SortExpression="LastLogin">
            </SharePoint:SPBoundField>
            <SharePoint:SPBoundField DataField="IsInSharePoint" HeaderText="<%$ Resources:FBAPackWebPages, IsInSharePointColHeaderText %>" SortExpression="IsInSharePoint">
            </SharePoint:SPBoundField>
            <SharePoint:SPBoundField DataField="Modified" HeaderText="<%$ Resources:FBAPackWebPages, ModifiedColHeaderText %>" SortExpression="Modified">
            </SharePoint:SPBoundField>
            <SharePoint:SPBoundField DataField="Created" HeaderText="<%$ Resources:FBAPackWebPages, CreatedColHeaderText %>" SortExpression="Created">
            </SharePoint:SPBoundField>
        </Columns>
    </SharePoint:SPGridView>
    <SharePoint:SPGridViewPager ID="SPGridViewPager1" GridViewId="MemberGrid" runat="server" />
    <p>
        <asp:Label runat="server" ID="lblMessage" ForeColor="Red" />
    </p>
</asp:Content>
