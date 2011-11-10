<table cellpadding="0" border="0">
	<tbody>
        <tr>
		    <td><asp:Label ID="Success" runat="server" /></td>
	    </tr>
        <tr>
		    <td align="right">
                <asp:Button ID="ContinueButton" Visible="false" runat="server" CommandName="Continue" />
                <asp:LinkButton ID="ContinueLinkButton" Visible="false" runat="server" CommandName="Continue" />
                <asp:ImageButton ID="ContinueImageButton" Visible="false" runat="server" CommandName="Continue" />
            </td>
	    </tr>
        <tr id="EditProfileRow" runat="server">
		    <td><asp:Image runat="server" ID="EditProfileIcon" /><asp:HyperLink runat="server" ID="EditProfileLink"></asp:HyperLink></td>
	    </tr>
    </tbody>
</table>