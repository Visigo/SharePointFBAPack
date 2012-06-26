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
    <SharePoint:EncodedLiteral ID="PageTitle" Text="<% $Resources: FBAPackWebPages, SiteConfig_Title %>"
        EncodeMethod="HtmlEncode" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea"
    runat="server">
    <a href="../../settings.aspx"><SharePoint:EncodedLiteral ID="EncodedLiteral4" runat="server" text="<%$Resources:wss,settings_pagetitle%>" EncodeMethod="HtmlEncode"/></a>&#32;<SharePoint:ClusteredDirectionalSeparatorArrow ID="ClusteredDirectionalSeparatorArrow1" runat="server" />
    <SharePoint:EncodedLiteral ID="TitleArea" Text="<% $Resources: FBAPackWebPages, SiteConfig_Title %>"
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
    <SharePoint:EncodedLiteral ID="DescArea" Text="<% $Resources: FBAPackWebPages, SiteConfig_Desc %>"
        EncodeMethod="HtmlEncode" runat="server" /> </asp:PlaceHolder>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <table border="0" width="100%" cellspacing="0" cellpadding="0" class="ms-descriptiontext">
        <wssuc:InputFormSection runat="server" Title="<% $Resources: FBAPackWebPages, MembershipOptionHeaderText %>">
            <template_description>
		   <SharePoint:EncodedLiteral ID="EncodedLiteral3" runat="server" text="<% $Resources: FBAPackWebPages, MembershipOptionHeaderDesc %>" EncodeMethod='HtmlEncode'/>
	    </template_description>
            <template_inputformcontrols>
		    <wssuc:InputFormControl runat="server" LabelText="<% $Resources: FBAPackWebPages, EnableRolesLabelText %>">
			    <Template_Control>
                    <asp:CheckBox Title="<% $Resources: FBAPackWebPages, EnableRolesToolTipText %>" ID="chkEnableRoles" runat="server" />			       
			    </Template_Control>
		    </wssuc:InputFormControl>
		    <wssuc:InputFormControl runat="server" LabelText="<% $Resources: FBAPackWebPages, ReviewMembershipRequestLabelText %>">
			    <Template_Control>
			       <asp:CheckBox Title="<% $Resources: FBAPackWebPages, ReviewMembershipRequestToolTipText %>" ID="chkReviewMembershipRequests" runat="server" />
			    </Template_Control>
		    </wssuc:InputFormControl>
	    </template_inputformcontrols>
        </wssuc:InputFormSection>
        <wssuc:InputFormSection runat="server" Title="<% $Resources: FBAPackWebPages, MembershipSiteReviewHeaderText %>">
            <template_description>
		   <asp:Literal  runat="server" text="<% $Resources: FBAPackWebPages, MembershipSiteReviewHeaderDesc %>" />
	    </template_description>
            <template_inputformcontrols>
		    <wssuc:InputFormControl runat="server" LabelText="<% $Resources: FBAPackWebPages, PasswordChangePageLabelText %>" ExampleText="<% $Resources: FBAPackWebPages, PasswordChangePageExampleText %>" >
			    <Template_Control>
			       <SharePoint:InputFormTextBox Title="<% $Resources: FBAPackWebPages, PasswordChangePageToolTipText %>" class="ms-input" Columns="40" maxlength="255" ID="txtChangePasswordPage" Direction="LeftToRight" Runat="server" />
			    </Template_Control>
		    </wssuc:InputFormControl>
		    <wssuc:InputFormControl runat="server" LabelText="<% $Resources: FBAPackWebPages, PasswordQuestionPageLabelText %>">
			    <Template_Control>
			       <SharePoint:InputFormTextBox Title="<% $Resources: FBAPackWebPages, PasswordQuestionPageToolTipText %>" class="ms-input" Columns="40" maxlength="255" ID="txtPasswordQuestionPage" Direction="LeftToRight" Runat="server" />
			    </Template_Control>
		    </wssuc:InputFormControl>
		    <wssuc:InputFormControl runat="server" LabelText="<% $Resources: FBAPackWebPages, ThankYouPageLabelText %>">
			    <Template_Control>
			       <SharePoint:InputFormTextBox Title="<% $Resources: FBAPackWebPages, ThankYouPageToolTipText %>" class="ms-input" Columns="40" maxlength="255" ID="txtThankYouPage" Direction="LeftToRight" Runat="server" />
			    </Template_Control>
		    </wssuc:InputFormControl>
	    </template_inputformcontrols>
        </wssuc:InputFormSection>
        <wssuc:InputFormSection runat="server" Title="<% $Resources: FBAPackWebPages, MembershipEmailTemplateHeaderText %>">
            <template_description>
		   <SharePoint:EncodedLiteral ID="EncodedLiteral2" runat="server" text="<% $Resources: FBAPackWebPages, MembershipEmailTemplateHeaderDesc %>" EncodeMethod='HtmlEncode'/>
	    </template_description>
            <template_inputformcontrols>
		    <wssuc:InputFormControl runat="server" LabelText="<% $Resources: FBAPackWebPages, ReplyToLabelText %>">
			    <Template_Control>
			       <SharePoint:InputFormTextBox Title="<% $Resources: FBAPackWebPages, ReplyToToolTipText %>" class="ms-input" Columns="40" maxlength="255" ID="txtReplyTo" Direction="LeftToRight" Runat="server" />
			    </Template_Control>
		    </wssuc:InputFormControl>
		    <wssuc:InputFormControl runat="server" LabelText="<% $Resources: FBAPackWebPages, MembershipApprovedLabelText %>">
			    <Template_Control>
			       <SharePoint:InputFormTextBox Title="<% $Resources: FBAPackWebPages, MembershipApprovedToolTipText %>" class="ms-input" TextMode="MultiLine" Columns="80" Rows="10" ID="txtMembershipApproved" Direction="LeftToRight" Runat="server" />
			    </Template_Control>
		    </wssuc:InputFormControl>
		    <wssuc:InputFormControl runat="server" LabelText="<% $Resources: FBAPackWebPages, MembershipPendingLabelText %>">
			    <Template_Control>
			       <SharePoint:InputFormTextBox Title="<% $Resources: FBAPackWebPages, MembershipPendingToolTipText %>" class="ms-input" TextMode="MultiLine" Columns="80" Rows="10" ID="txtMembershipPending" Direction="LeftToRight" Runat="server" />
			    </Template_Control>
		    </wssuc:InputFormControl>
		    		    <wssuc:InputFormControl runat="server" LabelText="<% $Resources: FBAPackWebPages, MembershipRejectedLabelText %>">
			    <Template_Control>
			       <SharePoint:InputFormTextBox Title="<% $Resources: FBAPackWebPages, MembershipRejectedToolTipText %>" class="ms-input" TextMode="MultiLine" Columns="80" Rows="10" ID="txtMembershipRejected" Direction="LeftToRight" Runat="server" />
			    </Template_Control>
		    </wssuc:InputFormControl>
		    		    <wssuc:InputFormControl runat="server" LabelText="<% $Resources: FBAPackWebPages, PasswordRecoveryLabelText %>">
			    <Template_Control>
			       <SharePoint:InputFormTextBox Title="<% $Resources: FBAPackWebPages, PasswordRecoveryToolTipText %>" class="ms-input" TextMode="MultiLine" Columns="80" Rows="10" ID="txtPasswordRecovery" Direction="LeftToRight" Runat="server" />
			    </Template_Control>
		    </wssuc:InputFormControl>
            <wssuc:InputFormControl ID="InputFormControl1" runat="server" LabelText="<% $Resources: FBAPackWebPages, ResetPasswordLabelText %>">
			    <Template_Control>
			       <SharePoint:InputFormTextBox Title="<% $Resources: FBAPackWebPages, ResetPasswordToolTipText %>" class="ms-input" TextMode="MultiLine" Columns="80" Rows="10" ID="txtResetPassword" Direction="LeftToRight" Runat="server" />
			    </Template_Control>
		    </wssuc:InputFormControl>
	    </template_inputformcontrols>
        </wssuc:InputFormSection>
        <wssuc:InputFormSection runat="server" Title="<% $Resources: FBAPackWebPages, VersionHeaderText %>" Description="<% $Resources: FBAPackWebPages,VersionHeaderDesc %>">
            <template_inputformcontrols>
		        <wssuc:InputFormControl runat="server" LabelText="" >
                    <Template_Control>
			            <asp:Label id="lblVersion" runat="server" />
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
