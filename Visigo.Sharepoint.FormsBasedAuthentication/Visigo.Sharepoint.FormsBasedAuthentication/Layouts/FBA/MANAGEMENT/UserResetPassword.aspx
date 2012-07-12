<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.SharePoint.ApplicationPages, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserResetPassword.aspx.cs" Inherits="Visigo.Sharepoint.FormsBasedAuthentication.UserResetPassword"
    DynamicMasterPageFile="~masterurl/default.master" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormSection" Src="~/_controltemplates/InputFormSection.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormControl" Src="~/_controltemplates/InputFormControl.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ButtonSection" Src="~/_controltemplates/ButtonSection.ascx" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    <SharePoint:EncodedLiteral ID="PageTitle" Text="<% $Resources: FBAPackWebPages, ResetPasswordUser_Title %>"
        EncodeMethod="HtmlEncode" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea"
    runat="server">
   	<a href="../../settings.aspx"><SharePoint:EncodedLiteral ID="EncodedLiteral1" runat="server" text="<%$Resources:wss,settings_pagetitle%>" EncodeMethod="HtmlEncode"/></a>&#32;<SharePoint:ClusteredDirectionalSeparatorArrow ID="ClusteredDirectionalSeparatorArrow1" runat="server" />
    <a href="UsersDisp.aspx"><SharePoint:EncodedLiteral ID="EncodedLiteral2" Text="<%$ Resources:FBAPackWebPages, UserMgmt_Title %>"
        EncodeMethod="HtmlEncode" runat="server" /></a>&#32;<SharePoint:ClusteredDirectionalSeparatorArrow ID="ClusteredDirectionalSeparatorArrow2" runat="server" />
    <SharePoint:EncodedLiteral ID="TitleArea" Text="<% $Resources: FBAPackWebPages, ResetPasswordUser_Title %>"
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
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <script language="javascript">
        function confirmResetPassword() {
            if (Page_ClientValidate()) {
                return confirm("<SharePoint:EncodedLiteral runat='server' text='<% $Resources: FBAPackWebPages, ResetPasswordConfirmText %>' EncodeMethod='EcmaScriptStringLiteralEncode'/>");
            }
            else {
                return false;
            }

        }
        function ResetAutoPasswordOnClick()
        {
	        if (browseris.ie4up || browseris.nav6up)
	        {
		        document.getElementById(<%SPHttpUtility.AddQuote(SPHttpUtility.NoEncode(txtNewPassword.ClientID),Response.Output);%>).disabled = true;
		        ValidatorEnable(document.getElementById(<%SPHttpUtility.AddQuote(SPHttpUtility.NoEncode(InputFormRequiredFieldValidatorNewPassword.ClientID),Response.Output);%>),false);
                document.getElementById(<%SPHttpUtility.AddQuote(SPHttpUtility.NoEncode(chkSendEmail.ClientID),Response.Output);%>).disabled = true;
	        }
        }
        function ResetSelectPasswordOnClick()
        {
	        if (browseris.ie4up || browseris.nav6up)
	        {
		        document.getElementById(<%SPHttpUtility.AddQuote(SPHttpUtility.NoEncode(txtNewPassword.ClientID),Response.Output);%>).disabled = false;
		        document.getElementById(<%SPHttpUtility.AddQuote(SPHttpUtility.NoEncode(InputFormRequiredFieldValidatorNewPassword.ClientID),Response.Output);%>).enabled = true;
                document.getElementById(<%SPHttpUtility.AddQuote(SPHttpUtility.NoEncode(chkSendEmail.ClientID),Response.Output);%>).disabled = false;
	        }
        }

        function InitResetPassword() {
            if (document.getElementById(<%SPHttpUtility.AddQuote(SPHttpUtility.NoEncode(resetAutoPassword.ClientID),Response.Output);%>).checked) {
                ResetAutoPasswordOnClick();
            }
            else {
                ResetSelectPasswordOnClick();
            }
        }

        _spBodyOnLoadFunctionNames.push("InitResetPassword");

    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <table border="0" width="100%" cellspacing="0" cellpadding="0" class="ms-descriptiontext">
        <wssuc:InputFormSection runat="server" Title="<% $Resources: FBAPackWebPages, ResetPasswordHeaderText %>">
            <Template_Description>
				    <SharePoint:EncodedLiteral ID="EncodedLiteral3" runat="server" text="<%$Resources:FBAPackWebPages, ResetPasswordDescText%>" EncodeMethod='HtmlEncode'/>
	       </Template_Description>
            <Template_InputFormControls>
            <asp:Label ID="resetPasswordMsg" runat="server" Text=""></asp:Label>
			<SharePoint:InputFormRadioButton id="resetAutoPassword"
				GroupName="resetPassword"
				runat="server"
				onclick="javascript:ResetAutoPasswordOnClick()"
				title="<%$Resources:FBAPackWebPages,ResetAutoPasswordText%>"
				LabelText="<%$Resources:FBAPackWebPages,ResetAutoPasswordText%>" />
			<SharePoint:InputFormRadioButton id="resetSelectPassword"
				GroupName="resetPassword"
				runat="server" 
				onclick="javascript:ResetSelectPasswordOnClick()"
				title="<%$Resources:FBAPackWebPages,ResetSelectPasswordText%>"
				LabelText="<%$Resources:FBAPackWebPages,ResetSelectPasswordText%>"
				>
					<table border="0" width="100%" cellspacing="0" cellpadding="0">
						<wssuc:InputFormControl ID="InputFormControl1"  runat="server" 
							LabelText="<%$Resources:FBAPackWebPages,NewPasswordLabelText%>"
						>
							<Template_Control>
								<SharePoint:InputFormTextBox title="<%$Resources:FBAPackWebPages,NewPasswordToolTipText%>" class="ms-input" ID="txtNewPassword" Columns="35" Runat="server" MaxLength="512" Direction="LeftToRight" TextMode="Password" autocomplete="off" />
                                <SharePoint:InputFormRequiredFieldValidator ID="InputFormRequiredFieldValidatorNewPassword" ControlToValidate="txtNewPassword" Display="Dynamic" Runat="server"/>
                                <asp:Label ID="lblNewPasswordError" runat="server" Text="" ForeColor="red" ></asp:Label>
							</Template_Control>
						</wssuc:InputFormControl>
                        <wssuc:InputFormControl ID="InputFormControl3" runat="server">
			                <Template_Control>
                                <asp:CheckBox Title="<% $Resources: FBAPackWebPages, ResetPasswordSendEmailToolTipText %>" ID="chkSendEmail" runat="server" Text="<% $Resources: FBAPackWebPages, ResetPasswordSendEmailLabelText %>" />			       
			                </Template_Control>
		                </wssuc:InputFormControl>
					</table>
			</SharePoint:InputFormRadioButton>
	   </Template_InputFormControls>
        </wssuc:InputFormSection>
        <wssuc:ButtonSection runat="server">
            <template_buttons>
		<asp:Button UseSubmitBehavior="false" runat="server" class="ms-ButtonHeightWidth" OnClick="OnResetPassword" OnClientClick="if (!confirmResetPassword()) return false;" Text="<% $Resources: FBAPackWebPages, ResetPasswordButtonText %>" id="BtnResetPassword" accesskey="R"/>
		</template_buttons>
        </wssuc:ButtonSection>
    </table>
</asp:Content>