<%@ Register TagPrefix="FBA" Namespace="Visigo.Sharepoint.FormsBasedAuthentication.HIP" Assembly="Visigo.Sharepoint.FormsBasedAuthentication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9dba9f460226d31d" %>

<table border="0" cellpadding="0"  id="MembershipRequestTable" runat="server" >
	<tbody>
        <tr>
			<td align="center" colspan="2"><asp:Label ID="Header" runat="server" /></td>
		</tr>
        <tr>
			<td align="center" colspan="2"><asp:Label ID="Instruction" runat="server" /></td>
		</tr>
        <tr>
			<td align="right"><asp:Label ID="UserNameLabel" AssociatedControlID="UserName" runat="server" /></td>
            <td>
                <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" Display="Dynamic"></asp:RequiredFieldValidator></td>
		</tr>
        <tr id="PasswordRow" runat="server" visible="false">
            <td align="right">
                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" /></td>
            <td>
                <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"  Display="Dynamic" />
            </td>
        </tr>
        <tr id="ConfirmPasswordRow" runat="server" visible="false">
            <td align="right">
                <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword"/></td>
            <td>
                <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword" Display="Dynamic" />
            </td>
        </tr>
        <tr>
			<td align="right"><asp:Label ID="FirstNameLabel" AssociatedControlID="FirstName" runat="server" /></td>
            <td>
                <asp:TextBox ID="FirstName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="FirstNameRequired" runat="server" ControlToValidate="FirstName" Display="Dynamic"></asp:RequiredFieldValidator></td>
		</tr>
        <tr>
			<td align="right"><asp:Label ID="LastNameLabel" AssociatedControlID="LastName" runat="server" /></td>
            <td>
                <asp:TextBox ID="LastName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="LastNameRequired" runat="server" ControlToValidate="LastName" Display="Dynamic"></asp:RequiredFieldValidator></td>
		</tr>
        <tr>
			<td align="right"><asp:Label ID="EmailLabel" AssociatedControlID="Email" runat="server" /></td>
            <td>
                <asp:TextBox ID="Email" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email" Display="Dynamic"></asp:RequiredFieldValidator></td>
		</tr>
        <tr id="QuestionRow" runat="server" visible="false">
			<td align="right"><asp:Label ID="QuestionLabel" AssociatedControlID="Question" runat="server" /></td>
            <td>
                <asp:TextBox ID="Question" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="QuestionRequired" runat="server" ControlToValidate="Question" Display="Dynamic"></asp:RequiredFieldValidator></td>
		</tr>
        <tr id="AnswerRow" runat="server" visible="false">
			<td align="right"><asp:Label ID="AnswerLabel" AssociatedControlID="Answer" runat="server" /></td>
            <td>
                <asp:TextBox ID="Answer" runat="server" EnableViewState="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="AnswerRequired" runat="server" ControlToValidate="Answer" Display="Dynamic"></asp:RequiredFieldValidator></td>
		</tr>
        <tr id="HipPictureRow" runat="server" visible="false">
			<td align="right"><asp:Label ID="HipPictureLabel" AssociatedControlID="HipPicture" runat="server" /></td>
            <td align="left">
                <asp:Label ID="HipInstructionsLabel" runat="server" /><br />
                <FBA:ImageHipChallenge ID="HipPicture" Width="210" Height="70" runat="server" /><br />
                <asp:Label ID="HipPictureDescriptionLabel" runat="server" />
            </td>
		</tr>
        <tr id="HipAnswerRow" runat="server" visible="false">
			<td align="right"><asp:Label ID="HipAnswerLabel" AssociatedControlID="HipAnswer" runat="server" /></td>
            <td>
                <asp:TextBox ID="HipAnswer" runat="server"></asp:TextBox>
                <FBA:HipValidator ID="HipAnswerValidator" runat="server" ControlToValidate="HipAnswer" HipChallenge="HipPicture"  Display="Dynamic" /><br />
                <asp:LinkButton ID="HipReset" runat="server" CommandName="HipReset" CausesValidation="false" />
            </td>
		</tr>
        <tr id="ConfirmPasswordCompareRow" runat="server" visible="false">
	        <td align="center" colspan="2">
                <asp:CompareValidator ID="ConfirmPasswordCompare" ControlToValidate="ConfirmPassword" ControlToCompare="Password"
                runat="server" Display="Dynamic"></asp:CompareValidator></td>
        </tr>
        <tr>
            <td align="center" colspan="2" style="color: red">
                <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                <asp:Literal ID="FBAErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
            </td>
        </tr>
        <tr>
			<td align="right" colspan="2">
                <asp:Button ID="CreateUserButton" Visible="false" runat="server" CommandName="MoveNext" />
                <asp:LinkButton ID="CreateUserLinkButton" Visible="false" runat="server" CommandName="MoveNext" />
                <asp:ImageButton ID="CreateUserImageButton" Visible="false" runat="server" CommandName="MoveNext" />&nbsp;
                <asp:Button ID="CancelButton" Visible="false" runat="server" CommandName="Cancel" CausesValidation="false" />
                <asp:LinkButton ID="CancelLinkButton" Visible="false" runat="server" CommandName="Cancel"  CausesValidation="false" />
                <asp:ImageButton ID="CancelImageButton" Visible="false" runat="server" CommandName="Cancel"  CausesValidation="false" /></td>
		</tr>
	</tbody>
</table>