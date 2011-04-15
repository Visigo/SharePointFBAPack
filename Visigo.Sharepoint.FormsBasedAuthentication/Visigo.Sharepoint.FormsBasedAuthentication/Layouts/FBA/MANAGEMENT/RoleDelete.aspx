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
    <SharePoint:EncodedLiteral ID="PageTitle" Text="Delete Forms Based Authentication Role"
        EncodeMethod="HtmlEncode" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea"
    runat="server">
    <SharePoint:EncodedLiteral ID="TitleArea" Text="Delete Forms Based Authentication Role"
        EncodeMethod="HtmlEncode" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <script language="javascript">
        try {
            document.getElementById("BtnCancel").focus();
        }
        catch (e) { }

        function confirmDelete() {
            return confirm("<SharePoint:EncodedLiteral runat='server' text='Are you sure you want to remove all users from this role and permenantly delete this role?' EncodeMethod='EcmaScriptStringLiteralEncode'/>");
        }
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <table border="0" width="100%" cellspacing="0" cellpadding="0" class="ms-descriptiontext">
        <wssuc:InputFormSection runat="server" Title="Role Name">
            <template_inputformcontrols>
		    <wssuc:InputFormControl runat="server" LabelText="Type the role name:">
			    <Template_Control>
			    <SharePoint:InputFormTextBox Title="Role Name" Enabled="false" class="ms-input" Columns="40" maxlength="255" ID="txtRole" Direction="LeftToRight" Runat="server" />
			    <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="red"></asp:Label>
			    </Template_Control>
		    </wssuc:InputFormControl>
	    </template_inputformcontrols>
        </wssuc:InputFormSection>
        <wssuc:ButtonSection runat="server">
            <template_buttons>
	    <asp:Button UseSubmitBehavior="false" runat="server" CssClass="ms-ButtonHeightWidth" OnClick="OnDelete" OnClientClick="if (!confirmDelete()) return false;" Text="Delete" id="BtnDelete" accesskey="D"/>
	    </template_buttons>
        </wssuc:ButtonSection>
    </table>
</asp:Content>
