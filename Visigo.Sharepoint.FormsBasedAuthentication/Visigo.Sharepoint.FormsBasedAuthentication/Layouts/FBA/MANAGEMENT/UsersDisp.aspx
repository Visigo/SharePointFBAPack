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
    <SharePoint:EncodedLiteral ID="PageTitle" Text="Manage Forms Based Authentication Users"
        EncodeMethod="HtmlEncode" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea"
    runat="server">
    <SharePoint:EncodedLiteral ID="TitleArea" Text="Manage Forms Based Authentication Users"
        EncodeMethod="HtmlEncode" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderPageDescription" runat="server">
    <asp:PlaceHolder ID="ToolBarPlaceHolder" runat="server">Use this page to manage forms
        based authentication users
        <div style="padding-top: 4px;">
            <wssuc:ToolBar ID="onetidNavNodesTB" runat="server">
                <Template_Buttons>
                    <wssuc:ToolBarButton runat="server" Text="New User" ID="idNewNavNode" ToolTip="New User"
                        NavigateUrl="UserNew.aspx" ImageUrl="/_layouts/images/newitem.gif" AccessKey="U" />
                </Template_Buttons>
            </wssuc:ToolBar>
        </div>
    </asp:PlaceHolder>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <SharePoint:MenuTemplate ID="UserMenu" runat="server">
        <SharePoint:MenuItemTemplate ID="Edit" runat="server" Text="Edit" ImageUrl="/_layouts/images/edititem.gif"
            ClientOnClickNavigateUrl="UserEdit.aspx?UserName=%USERNAME%" Title="Edit">
        </SharePoint:MenuItemTemplate>
        <SharePoint:MenuItemTemplate ID="Delete" runat="server" Text="Delete" ImageUrl="/_layouts/images/delete.gif"
            ClientOnClickNavigateUrl="UserDelete.aspx?UserName=%USERNAME%" Title="Delete">
        </SharePoint:MenuItemTemplate>
    </SharePoint:MenuTemplate>
    <FBA:FBADataSource runat="server" ID="UserDataSource" ViewName="FBAUsersView" />
    <SharePoint:SPGridView ID="MemberGrid" runat="server" DataSourceID="UserDataSource"
        AutoGenerateColumns="false" AllowPaging="true" PageSize="20" AllowSorting="true">
        <Columns>
            <SharePoint:SPMenuField HeaderText="User Name" TextFields="Name" MenuTemplateId="UserMenu"
                NavigateUrlFields="Name" NavigateUrlFormat="UserEdit.aspx?UserName={0}" TokenNameAndValueFields="USERNAME=Name"
                SortExpression="Name" />
            <SharePoint:SPBoundField DataField="Email" HeaderText="Email" SortExpression="Email">
            </SharePoint:SPBoundField>
            <SharePoint:SPBoundField DataField="Title" HeaderText="Full Name" SortExpression="Title">
            </SharePoint:SPBoundField>
            <SharePoint:SPBoundField DataField="Active" HeaderText="Active" SortExpression="Active">
            </SharePoint:SPBoundField>
            <SharePoint:SPBoundField DataField="Locked" HeaderText="Locked" SortExpression="Locked">
            </SharePoint:SPBoundField>
            <SharePoint:SPBoundField DataField="IsInSharePoint" HeaderText="IsInSharePoint" SortExpression="IsInSharePoint">
            </SharePoint:SPBoundField>
            <SharePoint:SPBoundField DataField="Modified" HeaderText="Modified" SortExpression="Modified">
            </SharePoint:SPBoundField>
            <SharePoint:SPBoundField DataField="Created" HeaderText="Created" SortExpression="Created">
            </SharePoint:SPBoundField>
        </Columns>
    </SharePoint:SPGridView>
    <SharePoint:SPGridViewPager ID="SPGridViewPager1" GridViewId="MemberGrid" runat="server" />
    <p>
        <asp:Label runat="server" ID="lblMessage" ForeColor="Red" />
    </p>
</asp:Content>
