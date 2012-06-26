<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.SharePoint.ApplicationPages, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserNew.aspx.cs" Inherits="Visigo.Sharepoint.FormsBasedAuthentication.UserNew"
    DynamicMasterPageFile="~masterurl/default.master" %>
    
<%@ Register TagPrefix="wssuc" TagName="ToolBar" Src="~/_controltemplates/ToolBar.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ToolBarButton" Src="~/_controltemplates/ToolBarButton.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormSection" Src="~/_controltemplates/InputFormSection.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormControl" Src="~/_controltemplates/InputFormControl.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ButtonSection" Src="~/_controltemplates/ButtonSection.ascx" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    <SharePoint:EncodedLiteral ID="PageTitle" Text="<%$ Resources:FBAPackWebPages, NewUser_Title %>"
        EncodeMethod="HtmlEncode" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea"
    runat="server">
   	<a href="../../settings.aspx"><SharePoint:EncodedLiteral ID="EncodedLiteral1" runat="server" text="<%$Resources:wss,settings_pagetitle%>" EncodeMethod="HtmlEncode"/></a>&#32;<SharePoint:ClusteredDirectionalSeparatorArrow ID="ClusteredDirectionalSeparatorArrow1" runat="server" />
    <a href="UsersDisp.aspx"><SharePoint:EncodedLiteral ID="EncodedLiteral2" Text="<%$ Resources:FBAPackWebPages, UserMgmt_Title %>"
        EncodeMethod="HtmlEncode" runat="server" /></a>&#32;<SharePoint:ClusteredDirectionalSeparatorArrow ID="ClusteredDirectionalSeparatorArrow2" runat="server" />
    <SharePoint:EncodedLiteral ID="TitleArea" Text="<%$ Resources:FBAPackWebPages, NewUser_Title %>"
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
<asp:Content ID="Content4" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    
    <table border="0" width="100%" cellspacing="0" cellpadding="0" class="ms-descriptiontext">
        <!-- User Name -->
        <wssuc:InputFormSection runat="server" Title="<%$ Resources:FBAPackWebPages, UserNameColHeaderText %>">
            <template_inputformcontrols>
                <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="red"></asp:Label>
		        <wssuc:InputFormControl runat="server" LabelText="<%$ Resources:FBAPackWebPages, TypeUserNameLabelText %>">
			        <Template_Control>
			            
			            <SharePoint:InputFormTextBox Title="<%$ Resources:FBAPackWebPages, UserNameColHeaderText %>" class="ms-input" Columns="40" maxlength="255" ID="txtUsername" Direction="LeftToRight" Runat="server" autocomplete="off" />
			            <SharePoint:InputFormRequiredFieldValidator ID="InputFormRequiredFieldValidator1" ControlToValidate="txtUsername" Display="Dynamic" Runat="server"/>			            
			        </Template_Control>
		        </wssuc:InputFormControl>
	        </template_inputformcontrols>
        </wssuc:InputFormSection>
        <!-- Full Name -->
        <wssuc:InputFormSection runat="server" Title="<%$ Resources:FBAPackWebPages, FullNameColHeaderText %>">
            <template_inputformcontrols>
		        <wssuc:InputFormControl runat="server" LabelText="<%$ Resources:FBAPackWebPages, TypeFullNameLabelText %>">
			        <Template_Control>
			            <SharePoint:InputFormTextBox Title="<%$ Resources:FBAPackWebPages, FullNameColHeaderText %>" class="ms-input" Columns="40" maxlength="255" ID="txtFullName" Direction="LeftToRight" Runat="server" autocomplete="off" />
			            <SharePoint:InputFormRequiredFieldValidator ID="InputFormRequiredFieldValidator2" ControlToValidate="txtFullName" Display="Dynamic" Runat="server"/>
			        </Template_Control>
		        </wssuc:InputFormControl>
	        </template_inputformcontrols>
        </wssuc:InputFormSection>
        <!-- Password -->
        <wssuc:InputFormSection runat="server" Title="<%$ Resources:FBAPackWebPages, PasswordHeaderText %>">
            <template_inputformcontrols>
                <asp:Label ID="lblPasswordMessage" runat="server" Text="" ForeColor="red"></asp:Label>
		        <wssuc:InputFormControl runat="server" LabelText="<%$ Resources:FBAPackWebPages, ConfirmPasswordLabelText %>">
			        <Template_Control>
			            <SharePoint:InputFormTextBox Title="password" ToolTip="<%$ Resources:FBAPackWebPages, EnterPasswordToolTipText %>" class="ms-input" Columns="40" textmode="Password" maxlength="255" ID="txtPassword" Direction="LeftToRight" Runat="server" autocomplete="off" />
			            <SharePoint:InputFormRequiredFieldValidator ID="txtPasswordValidator1" ControlToValidate="txtPassword" Display="Dynamic" Runat="server"/>
			            <SharePoint:InputFormTextBox Title="confirm" ToolTip="<%$ Resources:FBAPackWebPages, ConfirmPasswordToolTipText %>" class="ms-input" Columns="40" textmode="Password" maxlength="255" ID="txtConfirm" Runat="server" autocomplete="off" />
			            <SharePoint:InputFormRequiredFieldValidator ID="txtConfirmValidator1" ControlToValidate="txtConfirm" Display="Dynamic" Runat="server"/>
			            <SharePoint:InputFormCompareValidator ID="InputFormCompareValidatorPassword" SetFocusOnError="true"  ControlToValidate="txtConfirm" ControlToCompare="txtPassword" Type="String" Display="Dynamic" Operator="Equal" ErrorMessage="Password and confirmation do not match." runat="server"/>
			        </Template_Control>
		        </wssuc:InputFormControl>
	        </template_inputformcontrols>
        </wssuc:InputFormSection>
        <!-- Security Question -->
        <wssuc:InputFormSection id="QuestionSection" runat="server" Title="<%$ Resources:FBAPackWebPages, SecurityQuestionHeaderText %>">
            <template_inputformcontrols>
                <asp:Label ID="lblQuestionMessage" runat="server" Text="" ForeColor="red"></asp:Label>
		        <wssuc:InputFormControl runat="server" LabelText="<%$ Resources:FBAPackWebPages, TypeSecurityQLableText %>">
			        <Template_Control>
			            <SharePoint:InputFormTextBox Title="<%$ Resources:FBAPackWebPages, SecurityQuestionHeaderText %>" class="ms-input" Columns="40" maxlength="255" ID="txtQuestion" Direction="LeftToRight" Runat="server" autocomplete="off" />
			            <SharePoint:InputFormRequiredFieldValidator ID="InputFormRequiredFieldValidator3" ControlToValidate="txtQuestion" Display="Dynamic" Runat="server"/>
			        </Template_Control>
		        </wssuc:InputFormControl>
	        </template_inputformcontrols>
        </wssuc:InputFormSection>
        <!-- Security Answer -->
        <wssuc:InputFormSection id="AnswerSection" runat="server" Title="<%$ Resources:FBAPackWebPages, SecurityAnswerHeaderText %>">
            <template_inputformcontrols>
                <asp:Label ID="lblAnswerMessage" runat="server" Text="" ForeColor="red"></asp:Label>
		        <wssuc:InputFormControl runat="server" LabelText="<%$ Resources:FBAPackWebPages, TypeSecurityALableText %>">
			        <Template_Control>
			            <SharePoint:InputFormTextBox Title="<%$ Resources:FBAPackWebPages, SecurityAnswerHeaderText %>" class="ms-input" Columns="40" maxlength="255" ID="txtAnswer" Direction="LeftToRight" Runat="server" autocomplete="off" />
			            <SharePoint:InputFormRequiredFieldValidator ID="InputFormRequiredFieldValidator4" ControlToValidate="txtAnswer" Display="Dynamic" Runat="server"/>
			        </Template_Control>
		        </wssuc:InputFormControl>
	        </template_inputformcontrols>
        </wssuc:InputFormSection>
        <!-- Email Address -->
        <wssuc:InputFormSection runat="server" Title="<%$ Resources:FBAPackWebPages, EmailColHeaderText %>">
            <template_inputformcontrols>
                <asp:Label ID="lblEmailMessage" runat="server" Text="" ForeColor="red"></asp:Label>
		        <wssuc:InputFormControl runat="server" LabelText="<%$ Resources:FBAPackWebPages, TypeEmailAddrLabelText %>">
			        <Template_Control>
			        <SharePoint:InputFormTextBox Title="<%$ Resources:FBAPackWebPages, EmailColHeaderText %>" class="ms-input" Columns="40" maxlength="255" ID="txtEmail" Direction="LeftToRight" Runat="server" autocomplete="off" />
			        <SharePoint:InputFormRegularExpressionValidator ID="InputFormRegExpressionFieldValidator1"  ControlToValidate="txtEmail" Display="Dynamic" runat="server" ValidationExpression=".+\@.+" ErrorMessage="Enter a valid email address."/>
			        </Template_Control>
		        </wssuc:InputFormControl>
	        </template_inputformcontrols>
        </wssuc:InputFormSection>
        <!-- Is Active -->
        <wssuc:InputFormSection runat="server" id="ActiveSection" Title="<%$ Resources:FBAPackWebPages, ActiveColHeaderText %>">
            <template_inputformcontrols>
		        <wssuc:InputFormControl runat="server">
			        <Template_Control>
				        <asp:CheckBox ID="isActive" Checked="true" ToolTip="<%$ Resources:FBAPackWebPages, ActiveCheckBoxToolTipText %>" Text="<%$ Resources:FBAPackWebPages, ActiveCheckBoxLabelText %>" runat="server"/>
			        </Template_Control>
		        </wssuc:InputFormControl>
	        </template_inputformcontrols>
        </wssuc:InputFormSection>
        <!-- Groups -->
        <wssuc:InputFormSection runat="server" id="GroupSection" Title="<%$ Resources:FBAPackWebPages, GroupHeaderText %>">
            <template_inputformcontrols>
		        <wssuc:InputFormControl runat="server" LabelText="<%$ Resources:FBAPackWebPages, GroupLabelText %>">
			        <Template_Control>
				        <SharePoint:InputFormCheckBoxList ID="groupList" CssClass="ms-RadioText" ToolTip="<%$ Resources:FBAPackWebPages, GroupToolTipText %>" runat="server" />
			        </Template_Control>
		        </wssuc:InputFormControl>
	        </template_inputformcontrols>
        </wssuc:InputFormSection>
        <!-- Roles -->
        <wssuc:InputFormSection runat="server" id="RolesSection" Title="<%$ Resources:FBAPackWebPages, GroupHeaderText %>">
            <template_inputformcontrols>
		        <wssuc:InputFormControl runat="server" LabelText="<%$ Resources:FBAPackWebPages, GroupLabelText %>">
			        <Template_Control>
				        <SharePoint:InputFormCheckBoxList ID="rolesList" CssClass="ms-RadioText" ToolTip="<%$ Resources:FBAPackWebPages, GroupToolTipText %>" runat="server" />
			        </Template_Control>
		        </wssuc:InputFormControl>
	        </template_inputformcontrols>
        </wssuc:InputFormSection>
        <!-- Email -->
        <wssuc:InputFormSection runat="server" id="EmailSection" Title="<%$ Resources:FBAPackWebPages, SendEmailHeaderText %>">
            <template_inputformcontrols>
		        <wssuc:InputFormControl runat="server">
			        <Template_Control>
			            <SharePoint:InputFormCheckBox id="emailUser" runat="server" Checked="False" LabelText="<%$ Resources:FBAPackWebPages, SendMailLabelText %>" ToggleChildren="true">
				            <SharePoint:EncodedLiteral ID="EncodedLiteral3" runat="server" text="<%$Resources:wss,aclver_SubjectLabel%>" EncodeMethod='HtmlEncode'/>
					        <br>
					        <Template_Control>
					            <SharePoint:InputFormTextBox Title="<%$Resources:wss,aclver_SubjectTitle%>" class="ms-input" ID="txtEmailSubject" Columns="40" Runat="server" MaxLength="255" />
					            <SharePoint:InputFormRequiredFieldValidator id="ReqValEmailSubject" runat="server" BreakBefore=true BreakAfter=true EnableClientScript="false" ControlToValidate="txtEmailSubject"/>
					        </Template_Control>
					        <br>
					        <SharePoint:EncodedLiteral ID="EncodedLiteral4" runat="server" text="<%$Resources:wss,aclver_BodyLabel%>" EncodeMethod='HtmlEncode'/>
					        <br>
					        <Template_Control>
					            <SharePoint:InputFormTextBox Title="<%$Resources:wss,aclver_BodyTitle%>" class="ms-input" ID="txtEmailBody" Runat="server" TextMode="MultiLine" Columns="40" Rows="8" Cols=64 MaxLength=512 />
					        </Template_Control>
			            </SharePoint:InputFormCheckBox>
			         </Template_Control>
			    </wssuc:InputFormControl>   
		    </template_inputformcontrols>
        </wssuc:InputFormSection>
        <!-- Buttons -->
        <wssuc:ButtonSection runat="server">
            <template_buttons>
		        <asp:Button UseSubmitBehavior="false" runat="server" class="ms-ButtonHeightWidth" OnClick="OnSubmit" Text="<%$Resources:wss,multipages_okbutton_text%>" id="BtnOk" accesskey="<%$Resources:wss,okbutton_accesskey%>"/>
		    </template_buttons>
        </wssuc:ButtonSection>
    </table>
</asp:Content>
