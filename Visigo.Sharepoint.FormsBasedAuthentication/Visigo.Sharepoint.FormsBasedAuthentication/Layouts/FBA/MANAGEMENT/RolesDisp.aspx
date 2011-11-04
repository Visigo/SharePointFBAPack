<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.SharePoint.ApplicationPages, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RolesDisp.aspx.cs" Inherits="Visigo.Sharepoint.FormsBasedAuthentication.RolesDisp"
    DynamicMasterPageFile="~masterurl/default.master" %>

<%@ Register TagPrefix="wssuc" TagName="ToolBar" Src="~/_controltemplates/ToolBar.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ToolBarButton" Src="~/_controltemplates/ToolBarButton.ascx" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="FBA" Namespace="Visigo.Sharepoint.FormsBasedAuthentication"
    Assembly="Visigo.Sharepoint.FormsBasedAuthentication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9dba9f460226d31d" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    <SharePoint:EncodedLiteral ID="PageTitle" Text="<% $Resources: FBAPackWebPages, RolesMgmt_Title %>"
        EncodeMethod="HtmlEncode" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea"
    runat="server">
    <SharePoint:EncodedLiteral ID="TitleArea" Text="<% $Resources: FBAPackWebPages, RolesMgmt_Title %>"
        EncodeMethod="HtmlEncode" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderPageDescription" runat="server">
    <asp:PlaceHolder ID="ToolBarPlaceHolder" runat="server">
    <SharePoint:EncodedLiteral ID="DescArea" Text="<% $Resources: FBAPackWebPages, RolesMgmt_Desc %>"
        EncodeMethod="HtmlEncode" runat="server" />
    </asp:PlaceHolder>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PlaceHolderMain" runat="server">
        <div style="padding-top: 4px;padding-bottom: 4px;">
            <wssuc:ToolBar ID="onetidNavNodesTB" runat="server">
                <Template_Buttons>
                    <wssuc:ToolBarButton runat="server" Text="<% $Resources: FBAPackWebPages, NewUserLabelText %>" ID="idNewNavNode" ToolTip="<% $Resources: FBAPackWebPages, NewUserLabelText %>"
                        NavigateUrl="UserNew.aspx?Source=RolesDisp.aspx" ImageUrl="/_layouts/images/newitem.gif" AccessKey="U" />
                    <wssuc:ToolBarButton runat="server" Text="<% $Resources: FBAPackWebPages, NewRoleLabelText %>" ID="idNewCatNode" ToolTip="<% $Resources: FBAPackWebPages, NewRoleLabelText %>"
                        NavigateUrl="RoleNew.aspx?Source=RolesDisp.aspx" ImageUrl="/_layouts/images/newitem.gif" AccessKey="R" />
                </Template_Buttons>
            </wssuc:ToolBar>
        </div>
    <SharePoint:MenuTemplate ID="RoleMenu" runat="server">
        <SharePoint:MenuItemTemplate ID="DeleteRole" runat="server" Text="<% $Resources: FBAPackWebPages, DeleteContextMenuText %>" ImageUrl="/_layouts/images/delete.gif"
            ClientOnClickNavigateUrl="RoleDelete.aspx?Role=%ROLE%&Source=RolesDisp.aspx" Title="<% $Resources: FBAPackWebPages, DeleteContextMenuText %>">
        </SharePoint:MenuItemTemplate>
    </SharePoint:MenuTemplate>
    <FBA:FBADataSource runat="server" ID="RoleDataSource" ViewName="FBARolesView" />
    <SharePoint:SPGridView ID="RoleGrid" runat="server" DataSourceID="RoleDataSource"
        AutoGenerateColumns="false" AllowPaging="true" PageSize="3" AllowSorting="true">
        <Columns>
            <SharePoint:SPMenuField HeaderText="<% $Resources: FBAPackWebPages, RoleColHeaderText %>" TextFields="Role" MenuTemplateId="RoleMenu"
                NavigateUrlFields="Role" NavigateUrlFormat="RoleDelete.aspx?Role={0}&Source=RolesDisp.aspx" TokenNameAndValueFields="ROLE=Role"
                SortExpression="Role" />
            <SharePoint:SPBoundField HeaderText="<% $Resources: FBAPackWebPages, UsersInRoleColHeaderText %>" DataField="UsersInRole" SortExpression="UsersInRole" />
        </Columns>
    </SharePoint:SPGridView>
    <SharePoint:SPGridViewPager ID="SPGridViewPagerRoleMenu" GridViewId="RoleGrid" runat="server" />
    <p>
        <asp:Label runat="server" ID="lblMessage" ForeColor="Red" />
    </p>
</asp:Content>
