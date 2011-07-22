<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.SharePoint.ApplicationPages, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserEdit.aspx.cs" Inherits="Visigo.Sharepoint.FormsBasedAuthentication.UserEdit"
    DynamicMasterPageFile="~masterurl/default.master" %>

<%@ Register TagPrefix="wssuc" TagName="ToolBar" Src="~/_controltemplates/ToolBar.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ToolBarButton" Src="~/_controltemplates/ToolBarButton.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormSection" Src="~/_controltemplates/InputFormSection.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormControl" Src="~/_controltemplates/InputFormControl.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ButtonSection" Src="~/_controltemplates/ButtonSection.ascx" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    <SharePoint:EncodedLiteral ID="PageTitle" Text="Edit Forms Based Authentication User"
        EncodeMethod="HtmlEncode" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea"
    runat="server">
    <SharePoint:EncodedLiteral ID="TitleArea" Text="Edit Forms Based Authentication User"
        EncodeMethod="HtmlEncode" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <script type="text/javascript">
        function confirmPasswordReset() {
            return confirm("<SharePoint:EncodedLiteral runat='server' text='Are you sure you want to reset the password? The user will be emailed the new password' EncodeMethod='EcmaScriptStringLiteralEncode'/>");
        }
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <table border="0" width="100%" cellspacing="0" cellpadding="0" class="ms-descriptiontext">
        <wssuc:InputFormSection runat="server" Title="User Name">
            <template_inputformcontrols>
            <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="red"></asp:Label>
		    <wssuc:InputFormControl runat="server" LabelText="Username is read only:">
			    <Template_Control>
			    <SharePoint:InputFormTextBox Title="Username" Enabled="false" ReadOnly="true" class="ms-input" Columns="40" maxlength="255" ID="txtUsername" Direction="LeftToRight" Runat="server" />
			    <SharePoint:InputFormRequiredFieldValidator ID="InputFormRequiredFieldValidator1" ControlToValidate="txtUsername" Display="Dynamic" Runat="server"/>
			    
			    </Template_Control>
		    </wssuc:InputFormControl>
	    </template_inputformcontrols>
        </wssuc:InputFormSection>
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
        <wssuc:InputFormSection runat="server" id="CategorySection1" Title="Active">
            <template_inputformcontrols>
		    <wssuc:InputFormControl runat="server">
			    <Template_Control>
				    <SharePoint:InputFormCheckBox ID="isActive" ToolTip="If checked the account is active." LabelText="Check the box if the user's account is active." runat="server"/>
			    </Template_Control>
		    </wssuc:InputFormControl>
	    </template_inputformcontrols>
        </wssuc:InputFormSection>
        <wssuc:InputFormSection runat="server" Title="Account Locked">
            <template_inputformcontrols>
		    <wssuc:InputFormControl runat="server">
			    <Template_Control>
				    <SharePoint:InputFormCheckBox ID="isLocked" ToolTip="If checked the account is locked." LabelText="Uncheck the box to unlock the user's account." runat="server"/>
			    </Template_Control>
		    </wssuc:InputFormControl>
	    </template_inputformcontrols>
        </wssuc:InputFormSection>
        <wssuc:InputFormSection runat="server" id="GroupSection" Title="Groups">
            <template_inputformcontrols>
	        <wssuc:InputFormControl runat="server" LabelText="Choose atleast one group to add the user to:">
		        <Template_Control>
			        <SharePoint:InputFormCheckBoxList ID="groupList" CssClass="ms-RadioText" ToolTip="Select the roles for this user" runat="server"/>
		        </Template_Control>
	        </wssuc:InputFormControl>
        </template_inputformcontrols>
        </wssuc:InputFormSection>
        <wssuc:InputFormSection runat="server" id="RolesSection" Title="Roles">
            <template_inputformcontrols>
	        <wssuc:InputFormControl runat="server">
		        <Template_Control>
			        <SharePoint:InputFormCheckBoxList ID="rolesList" CssClass="ms-RadioText" ToolTip="Select the roles for this user" runat="server"/>
		        </Template_Control>
	        </wssuc:InputFormControl>
        </template_inputformcontrols>
        </wssuc:InputFormSection>
        <wssuc:ButtonSection runat="server">
            <template_buttons>
	        <asp:Button UseSubmitBehavior="false" runat="server" class="ms-ButtonHeightWidth" Width="100px" OnClick="OnResetPassword" OnClientClick="if (!confirmPasswordReset()) return false;" Text="Reset Password" id="BtnReset" accesskey="P"/>
    	    <asp:Button UseSubmitBehavior="false" runat="server" class="ms-ButtonHeightWidth" OnClick="OnSubmit" Text="Save" id="BtnOk" accesskey="<%$Resources:wss,okbutton_accesskey%>"/>	    
	    </template_buttons>
        </wssuc:ButtonSection>
    </table>
</asp:Content>
