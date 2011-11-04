<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.SharePoint.ApplicationPages, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleDelete.aspx.cs" Inherits="Visigo.Sharepoint.FormsBasedAuthentication.RoleDelete"
    DynamicMasterPageFile="~masterurl/default.master" %>

<%@ Register TagPrefix="wssuc" TagName="InputFormSection" Src="~/_controltemplates/InputFormSection.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormControl" Src="~/_controltemplates/InputFormControl.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ButtonSection" Src="~/_controltemplates/ButtonSection.ascx" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    <SharePoint:EncodedLiteral ID="PageTitle" Text="<% $Resources: FBAPackWebPages, DeleteRole_Title %>"
        EncodeMethod="HtmlEncode" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea"
    runat="server">
    <SharePoint:EncodedLiteral ID="TitleArea" Text="<% $Resources: FBAPackWebPages, DeleteRole_Title %>"
        EncodeMethod="HtmlEncode" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <script language="javascript">
        try {
            document.getElementById("BtnCancel").focus();
        }
        catch (e) { }

        function confirmDelete() {
            return confirm("<SharePoint:EncodedLiteral runat='server' text='<% $Resources: FBAPackWebPages, DeleteRoleConfirmText %>' EncodeMethod='EcmaScriptStringLiteralEncode'/>");
        }
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <table border="0" width="100%" cellspacing="0" cellpadding="0" class="ms-descriptiontext">
        <wssuc:InputFormSection runat="server" Title="<% $Resources: FBAPackWebPages, RoleNameHeaderText %>">
            <template_inputformcontrols>
		    <wssuc:InputFormControl runat="server" LabelText="<% $Resources: FBAPackWebPages, RoleNameLabelText %>">
			    <Template_Control>
			    <SharePoint:InputFormTextBox Title="<% $Resources: FBAPackWebPages, RoleNameHeaderText %>" Enabled="false" class="ms-input" Columns="40" maxlength="255" ID="txtRole" Direction="LeftToRight" Runat="server" />
                <asp:Label ID="localizedMsg" runat="server" Text="<% $Resources: FBAPackWebPages, UsersInRoleMsg %>" ForeColor="red" Visible="false"></asp:Label>
                <asp:Label ID="notExistMsg" runat="server" Text="<% $Resources: FBAPackWebPages, RoleNotExistenceMsg %>" ForeColor="red" Visible="false"></asp:Label>
			    <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="red"></asp:Label>
			    </Template_Control>
		    </wssuc:InputFormControl>
	    </template_inputformcontrols>
        </wssuc:InputFormSection>
        <wssuc:ButtonSection runat="server">
            <template_buttons>
	    <asp:Button UseSubmitBehavior="false" runat="server" CssClass="ms-ButtonHeightWidth" OnClick="OnDelete" OnClientClick="if (!confirmDelete()) return false;" Text="<% $Resources: FBAPackWebPages, DeleteButtonText %>" id="BtnDelete" accesskey="D"/>
	    </template_buttons>
        </wssuc:ButtonSection>
    </table>
</asp:Content>
