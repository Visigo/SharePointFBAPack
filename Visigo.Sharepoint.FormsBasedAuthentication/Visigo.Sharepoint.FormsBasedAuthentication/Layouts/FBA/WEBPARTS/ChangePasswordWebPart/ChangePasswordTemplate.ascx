<table cellpadding="0" border="0">
    <tbody>
        <tr>
	        <td align="center" colspan="2"><asp:Label ID="ChangePasswordTitle" runat="server" /></td>
        </tr>
        <tr>
	        <td align="center" colspan="2"><asp:Label ID="Instruction" runat="server" /></td>
        </tr>
        <tr id="UserNameRow" runat="server">
	        <td align="right">
                <asp:Label ID="UserNameLabel" AssociatedControlID="UserName" runat="server" /></td>
            <td>
                <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" Display="Dynamic"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
	        <td align="right">
                <asp:Label ID="CurrentPasswordLabel" AssociatedControlID="CurrentPassword" runat="server" /></td>
            <td>
            
                <asp:TextBox ID="CurrentPassword" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="CurrentPassword" Display="Dynamic"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
	        <td align="right">
                <asp:Label ID="NewPasswordLabel" AssociatedControlID="NewPassword" runat="server" /></td>
            <td>
                <asp:TextBox ID="NewPassword" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword" Display="Dynamic"></asp:RequiredFieldValidator></td>
        </tr>
        <tr id="PasswordHintRow" runat="server">
	        <td></td>
            <td>
                <asp:Label ID="PasswordHint" runat="server" /></td>
        </tr>
        <tr>
	        <td align="right">
                <asp:Label ID="ConfirmNewPasswordLabel" AssociatedControlID="ConfirmNewPassword" runat="server" /></td>
            <td>
                <asp:TextBox ID="ConfirmNewPassword" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword" Display="Dynamic"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
	        <td align="center" colspan="2">
                <asp:CompareValidator ID="ConfirmNewPasswordCompare" ControlToValidate="ConfirmNewPassword" ControlToCompare="NewPassword"
                runat="server" Display="Dynamic"></asp:CompareValidator></td>
        </tr>
        <tr>
            <td align="center" colspan="2" style="color: red">
                <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
            </td>
        </tr>
        <tr>
	        <td align="right">
                <asp:Button ID="ChangePasswordButton" Visible="false" runat="server" CommandName="ChangePassword" />
                <asp:LinkButton ID="ChangePasswordLinkButton" Visible="false" runat="server" CommandName="ChangePassword" />
                <asp:ImageButton ID="ChangePasswordImageButton" Visible="false" runat="server" CommandName="ChangePassword" />
            </td>
            <td>
                <asp:Button ID="CancelButton" Visible="false" runat="server" CommandName="Cancel" CausesValidation="false" />
                <asp:LinkButton ID="CancelLinkButton" Visible="false" runat="server" CommandName="Cancel" CausesValidation="false" />
                <asp:ImageButton ID="CancelImageButton" Visible="false" runat="server" CommandName="Cancel" CausesValidation="false" />
            </td>
        </tr>
        <tr id="HelpPageRow" runat="server">
	        <td colspan="2">
                <asp:Image runat="server" ID="HelpPageIcon" /><asp:HyperLink runat="server" ID="HelpPageLink"></asp:HyperLink>
            </td>
        </tr>
        <tr id="CreateUserRow" runat="server">
	        <td colspan="2">
                <asp:Image runat="server" ID="CreateUserIcon" /><asp:HyperLink runat="server" ID="CreateUserLink"></asp:HyperLink>
            </td>
        </tr>
        <tr id="PasswordRecoveryRow" runat="server">
	        <td colspan="2">
                <asp:Image runat="server" ID="PasswordRecoveryIcon" /><asp:HyperLink runat="server" ID="PasswordRecoveryLink"></asp:HyperLink>
            </td>
        </tr>
        <tr id="EditProfileRow" runat="server">
	        <td colspan="2">
                <asp:Image runat="server" ID="EditProfileIcon" /><asp:HyperLink runat="server" ID="EditProfileLink"></asp:HyperLink>
            </td>
        </tr>
    </tbody>
</table>