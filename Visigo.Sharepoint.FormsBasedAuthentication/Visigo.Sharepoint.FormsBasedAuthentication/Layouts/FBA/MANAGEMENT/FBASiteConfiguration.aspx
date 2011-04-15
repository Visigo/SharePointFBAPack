<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FBASiteConfiguration.aspx.cs"
    Inherits="Visigo.Sharepoint.FormsBasedAuthentication.FBASiteConfiguration" DynamicMasterPageFile="~masterurl/default.master" %>

<%@ Register TagPrefix="wssuc" TagName="ToolBar" Src="~/_controltemplates/ToolBar.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ToolBarButton" Src="~/_controltemplates/ToolBarButton.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormSection" Src="~/_controltemplates/InputFormSection.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormControl" Src="~/_controltemplates/InputFormControl.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ButtonSection" Src="~/_controltemplates/ButtonSection.ascx" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    <SharePoint:EncodedLiteral ID="PageTitle" Text="Manage Forms Based Authentication Users"
        EncodeMethod="HtmlEncode" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea"
    runat="server">
    <SharePoint:EncodedLiteral ID="TitleArea" Text="Manage Forms Based Authentication Configuration"
        EncodeMethod="HtmlEncode" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderPageDescription" runat="server">
    <asp:PlaceHolder ID="ToolBarPlaceHolder" runat="server">Use this page to manage forms
        based authentication site configuration such as site URLs. </asp:PlaceHolder>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <table border="0" width="100%" cellspacing="0" cellpadding="0" class="ms-descriptiontext">
        <wssuc:InputFormSection runat="server" Title="Membership Options">
            <template_description>
		   <SharePoint:EncodedLiteral ID="EncodedLiteral3" runat="server" text="These options control the functionality of the FBA web parts." EncodeMethod='HtmlEncode'/>
	    </template_description>
            <template_inputformcontrols>
		    <wssuc:InputFormControl runat="server" LabelText="Enable Roles:">
			    <Template_Control>
                    <SharePoint:InputFormCheckBox Title="EnableRoles" ID="chkEnableRoles" runat="server" />			       
			    </Template_Control>
		    </wssuc:InputFormControl>
		    <wssuc:InputFormControl runat="server" LabelText="Review Membership Requests:">
			    <Template_Control>
			       <SharePoint:InputFormCheckBox Title="ReviewMembershipRequests" ID="chkReviewMembershipRequests" runat="server" />
			    </Template_Control>
		    </wssuc:InputFormControl>
	    </template_inputformcontrols>
        </wssuc:InputFormSection>
        <wssuc:InputFormSection runat="server" Title="Membership Review Site URLs">
            <template_description>
		   <SharePoint:EncodedLiteral ID="EncodedLiteral1" runat="server" text="These pages can be used in email or FBA web parts." EncodeMethod='HtmlEncode'/>
	    </template_description>
            <template_inputformcontrols>
		    <wssuc:InputFormControl runat="server" LabelText="Change Password Page:">
			    <Template_Control>
			       <SharePoint:InputFormTextBox Title="ChangePasswordPage" class="ms-input" Columns="40" maxlength="255" ID="txtChangePasswordPage" Direction="LeftToRight" Runat="server" />
			    </Template_Control>
		    </wssuc:InputFormControl>
		    <wssuc:InputFormControl runat="server" LabelText="Password Question Page:">
			    <Template_Control>
			       <SharePoint:InputFormTextBox Title="PasswordQuestionPage" class="ms-input" Columns="40" maxlength="255" ID="txtPasswordQuestionPage" Direction="LeftToRight" Runat="server" />
			    </Template_Control>
		    </wssuc:InputFormControl>
		    <wssuc:InputFormControl runat="server" LabelText="Thank You Page:">
			    <Template_Control>
			       <SharePoint:InputFormTextBox Title="ThankYouPage" class="ms-input" Columns="40" maxlength="255" ID="txtThankYouPage" Direction="LeftToRight" Runat="server" />
			    </Template_Control>
		    </wssuc:InputFormControl>
	    </template_inputformcontrols>
        </wssuc:InputFormSection>
        <wssuc:InputFormSection runat="server" Title="Membership Email XSLT">
            <template_description>
		   <SharePoint:EncodedLiteral ID="EncodedLiteral2" runat="server" text="These are the paths to the XSLT used for the emails." EncodeMethod='HtmlEncode'/>
	    </template_description>
            <template_inputformcontrols>
		    <wssuc:InputFormControl runat="server" LabelText="Membership Approved:">
			    <Template_Control>
			       <SharePoint:InputFormTextBox Title="MembershipApproved" class="ms-input" Columns="40" maxlength="255" ID="txtMembershipApproved" Direction="LeftToRight" Runat="server" />
			    </Template_Control>
		    </wssuc:InputFormControl>
		    <wssuc:InputFormControl runat="server" LabelText="Membership Error:">
			    <Template_Control>
			       <SharePoint:InputFormTextBox Title="MembershipError" class="ms-input" Columns="40" maxlength="255" ID="txtMembershipError" Direction="LeftToRight" Runat="server" />
			    </Template_Control>
		    </wssuc:InputFormControl>
		    <wssuc:InputFormControl runat="server" LabelText="Membership Pending:">
			    <Template_Control>
			       <SharePoint:InputFormTextBox Title="MembershipPending" class="ms-input" Columns="40" maxlength="255" ID="txtMembershipPending" Direction="LeftToRight" Runat="server" />
			    </Template_Control>
		    </wssuc:InputFormControl>
		    		    <wssuc:InputFormControl runat="server" LabelText="Membership Rejected:">
			    <Template_Control>
			       <SharePoint:InputFormTextBox Title="MembershipRejected" class="ms-input" Columns="40" maxlength="255" ID="txtMembershipRejected" Direction="LeftToRight" Runat="server" />
			    </Template_Control>
		    </wssuc:InputFormControl>
		    		    <wssuc:InputFormControl runat="server" LabelText="Password Recovery:">
			    <Template_Control>
			       <SharePoint:InputFormTextBox Title="PasswordRecovery" class="ms-input" Columns="40" maxlength="255" ID="txtPasswordRecovery" Direction="LeftToRight" Runat="server" />
			    </Template_Control>
		    </wssuc:InputFormControl>
	    </template_inputformcontrols>
        </wssuc:InputFormSection>
        <wssuc:ButtonSection runat="server">
            <template_buttons>
		  <asp:Button UseSubmitBehavior="false" 
		  runat="server" 
		  class="ms-ButtonHeightWidth" 
		  OnClick="BtnUpdateSiteFBAConfig_Click" 
		  Text="<%$Resources:wss,multipages_okbutton_text%>" 
		  id="BtnCreate" 
		  accesskey="<%$Resources:wss,okbutton_accesskey%>"/>
	   </template_buttons>
        </wssuc:ButtonSection>
    </table>
    <p>
        <asp:Label runat="server" ID="lblMessage" ForeColor="Red" />
    </p>
</asp:Content>
