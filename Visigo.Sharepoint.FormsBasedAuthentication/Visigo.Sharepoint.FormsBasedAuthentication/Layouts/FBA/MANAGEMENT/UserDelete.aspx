<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.SharePoint.ApplicationPages, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserDelete.aspx.cs" Inherits="Visigo.Sharepoint.FormsBasedAuthentication.UserDelete"
    DynamicMasterPageFile="~masterurl/default.master" %>

<%@ Register TagPrefix="wssuc" TagName="InputFormSection" Src="~/_controltemplates/InputFormSection.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormControl" Src="~/_controltemplates/InputFormControl.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ButtonSection" Src="~/_controltemplates/ButtonSection.ascx" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    <SharePoint:EncodedLiteral ID="PageTitle" Text="<% $Resources: FBAPackWebPages, DeleteUser_Title %>"
        EncodeMethod="HtmlEncode" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea"
    runat="server">
    <SharePoint:EncodedLiteral ID="TitleArea" Text="<% $Resources: FBAPackWebPages, DeleteUser_Title %>"
        EncodeMethod="HtmlEncode" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <script language="javascript">
        try {
            document.getElementById("BtnCancel").focus();
        }
        catch (e) { }

        function confirmDelete() {
            return confirm("<SharePoint:EncodedLiteral runat='server' text='<% $Resources: FBAPackWebPages, DeleteConfirmText %>' EncodeMethod='EcmaScriptStringLiteralEncode'/>");
        }
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <table border="0" width="100%" cellspacing="0" cellpadding="0" class="ms-descriptiontext">
        <wssuc:InputFormSection runat="server" Title="<% $Resources: FBAPackWebPages, DeleteConfirmHeaderText %>">
            <template_inputformcontrols>
		    <wssuc:InputFormControl runat="server" LabelText="">
			    <Template_Control>
			        <asp:Label ID="localizedMsg" runat="server" Text="<% $Resources: FBAPackWebPages, DeleteConfirmLabelText %>" Visible="false"></asp:Label>
			        <asp:Label ID="deleteMsg" runat="server" Text=""></asp:Label>
			    </Template_Control>
		    </wssuc:InputFormControl>
	    </template_inputformcontrols>
        </wssuc:InputFormSection>
        <wssuc:ButtonSection runat="server">
            <template_buttons>
		<asp:Button UseSubmitBehavior="false" runat="server" class="ms-ButtonHeightWidth" OnClick="OnDelete" OnClientClick="if (!confirmDelete()) return false;" Text="<% $Resources: FBAPackWebPages, DeleteButtonText %>" id="BtnDelete" accesskey="D"/>
		</template_buttons>
        </wssuc:ButtonSection>
    </table>
</asp:Content>
