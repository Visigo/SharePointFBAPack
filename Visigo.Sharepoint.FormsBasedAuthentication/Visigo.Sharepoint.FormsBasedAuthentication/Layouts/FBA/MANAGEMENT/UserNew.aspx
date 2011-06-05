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
    <SharePoint:EncodedLiteral ID="PageTitle" Text="New Forms Based Authentication User"
        EncodeMethod="HtmlEncode" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea"
    runat="server">
    <SharePoint:EncodedLiteral ID="TitleArea" Text="New Forms Based Authentication User"
        EncodeMethod="HtmlEncode" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <table border="0" width="100%" cellspacing="0" cellpadding="0" class="ms-descriptiontext">
        <!-- User Name -->
        <wssuc:InputFormSection runat="server" Title="User Name">
            <template_inputformcontrols>
		        <wssuc:InputFormControl runat="server" LabelText="Type the user name:">
			        <Template_Control>
			            <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="red"></asp:Label>
			            <SharePoint:InputFormTextBox Title="User Name" class="ms-input" Columns="40" maxlength="255" ID="txtUsername" Direction="LeftToRight" Runat="server" />
			            <SharePoint:InputFormRequiredFieldValidator ID="InputFormRequiredFieldValidator1" ControlToValidate="txtUsername" Display="Dynamic" Runat="server"/>			            
			        </Template_Control>
		        </wssuc:InputFormControl>
	        </template_inputformcontrols>
        </wssuc:InputFormSection>
        <!-- Full Name -->
        <wssuc:InputFormSection runat="server" Title="Full Name">
            <template_inputformcontrols>
		        <wssuc:InputFormControl runat="server" LabelText="Type the full name:">
			        <Template_Control>
			            <SharePoint:InputFormTextBox Title="Full Name" class="ms-input" Columns="40" maxlength="255" ID="txtFullName" Direction="LeftToRight" Runat="server" />
			            <SharePoint:InputFormRequiredFieldValidator ID="InputFormRequiredFieldValidator2" ControlToValidate="txtFullName" Display="Dynamic" Runat="server"/>
			        </Template_Control>
		        </wssuc:InputFormControl>
	        </template_inputformcontrols>
        </wssuc:InputFormSection>
        <!-- Password -->
        <wssuc:InputFormSection runat="server" Title="Password">
            <template_inputformcontrols>
		        <wssuc:InputFormControl runat="server" LabelText="Enter and confirm the password:">
			        <Template_Control>
			            <SharePoint:InputFormTextBox Title="password" ToolTip="Enter password." class="ms-input" Columns="40" textmode="Password" maxlength="255" ID="txtPassword" Direction="LeftToRight" Runat="server" />
			            <SharePoint:InputFormRequiredFieldValidator ID="txtPasswordValidator1" ControlToValidate="txtPassword" Display="Dynamic" Runat="server"/>
			            <SharePoint:InputFormTextBox Title="confirm" ToolTip="Confirm password." class="ms-input" Columns="40" textmode="Password" maxlength="255" ID="txtConfirm" Runat="server" />
			            <SharePoint:InputFormRequiredFieldValidator ID="txtConfirmValidator1" ControlToValidate="txtConfirm" Display="Dynamic" Runat="server"/>
			            <SharePoint:InputFormCompareValidator ID="InputFormCompareValidatorPassword" SetFocusOnError="true"  ControlToValidate="txtConfirm" ControlToCompare="txtPassword" Type="String" Display="Dynamic" Operator="Equal" ErrorMessage="Password and confirmation do not match." runat="server"/>
			        </Template_Control>
		        </wssuc:InputFormControl>
	        </template_inputformcontrols>
        </wssuc:InputFormSection>
        <!-- Security Question -->
        <wssuc:InputFormSection id="QuestionSection" runat="server" Title="Security Question">
            <template_inputformcontrols>
		        <wssuc:InputFormControl runat="server" LabelText="Type the security question:">
			        <Template_Control>
			            <SharePoint:InputFormTextBox Title="Security Question" class="ms-input" Columns="40" maxlength="255" ID="txtQuestion" Direction="LeftToRight" Runat="server" />
			            <SharePoint:InputFormRequiredFieldValidator ID="InputFormRequiredFieldValidator3" ControlToValidate="txtQuestion" Display="Dynamic" Runat="server"/>
			        </Template_Control>
		        </wssuc:InputFormControl>
	        </template_inputformcontrols>
        </wssuc:InputFormSection>
        <!-- Security Answer -->
        <wssuc:InputFormSection id="AnswerSection" runat="server" Title="Security Answer">
            <template_inputformcontrols>
		        <wssuc:InputFormControl runat="server" LabelText="Type the answer to the security question:">
			        <Template_Control>
			            <SharePoint:InputFormTextBox Title="Security Answer" class="ms-input" Columns="40" maxlength="255" ID="txtAnswer" Direction="LeftToRight" Runat="server" />
			            <SharePoint:InputFormRequiredFieldValidator ID="InputFormRequiredFieldValidator4" ControlToValidate="txtAnswer" Display="Dynamic" Runat="server"/>
			        </Template_Control>
		        </wssuc:InputFormControl>
	        </template_inputformcontrols>
        </wssuc:InputFormSection>
        <!-- Email Address -->
        <wssuc:InputFormSection runat="server" Title="Email">
            <template_inputformcontrols>
		        <wssuc:InputFormControl runat="server" LabelText="Type the user's email address:">
			        <Template_Control>
			        <SharePoint:InputFormTextBox Title="txtEmail" class="ms-input" Columns="40" maxlength="255" ID="txtEmail" Direction="LeftToRight" Runat="server" />
			        <SharePoint:InputFormRegularExpressionValidator ID="InputFormRegExpressionFieldValidator1"  ControlToValidate="txtEmail" Display="Dynamic" runat="server" ValidationExpression="^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$" ErrorMessage="Enter a valid email address."/>
			        </Template_Control>
		        </wssuc:InputFormControl>
	        </template_inputformcontrols>
        </wssuc:InputFormSection>
        <!-- Is Active -->
        <wssuc:InputFormSection runat="server" id="ActiveSection" Title="Active">
            <template_inputformcontrols>
		        <wssuc:InputFormControl runat="server">
			        <Template_Control>
				        <SharePoint:InputFormCheckBox ID="isActive" Checked="true" ToolTip="If checked the account is active." LabelText="Check the box if the user's account is active." runat="server"/>
			        </Template_Control>
		        </wssuc:InputFormControl>
	        </template_inputformcontrols>
        </wssuc:InputFormSection>
        <!-- Groups -->
        <wssuc:InputFormSection runat="server" id="GroupSection" Title="Groups">
            <template_inputformcontrols>
		        <wssuc:InputFormControl runat="server" LabelText="Choose atleast one group to add the user to:">
			        <Template_Control>
				        <SharePoint:InputFormCheckBoxList ID="groupList" CssClass="ms-RadioText" ToolTip="Select the roles for this user" runat="server"/>
			        </Template_Control>
		        </wssuc:InputFormControl>
	        </template_inputformcontrols>
        </wssuc:InputFormSection>
        <!-- Roles -->
        <wssuc:InputFormSection runat="server" id="RolesSection" Title="Roles">
            <template_inputformcontrols>
		        <wssuc:InputFormControl runat="server">
			        <Template_Control>
				        <SharePoint:InputFormCheckBoxList ID="rolesList" CssClass="ms-RadioText" ToolTip="Select the roles for this user" runat="server"/>
			        </Template_Control>
		        </wssuc:InputFormControl>
	        </template_inputformcontrols>
        </wssuc:InputFormSection>
        <!-- Email -->
        <wssuc:InputFormSection runat="server" id="EmailSection" Title="Send E-Mail">
            <template_inputformcontrols>
		        <wssuc:InputFormControl runat="server">
			        <Template_Control>
			            <SharePoint:InputFormCheckBox id="emailUser" runat="server" Checked="False" LabelText="Send welcome e-mail to the new user" ToggleChildren=true>
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
