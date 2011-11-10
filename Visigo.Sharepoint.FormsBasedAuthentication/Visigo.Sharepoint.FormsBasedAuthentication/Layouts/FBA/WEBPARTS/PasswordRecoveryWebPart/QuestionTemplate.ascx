<table border="0" cellpadding="0">
    <tr>
    <td align="center" colspan="2">
        <asp:Label ID="QuestionTitle" runat="server" /></td>
    </tr>
    <tr>
    <td align="center" colspan="2">
        <asp:Label ID="QuestionInstruction" runat="server" /></td>
    </tr>
    <tr>
    <td align="right">
        <asp:Label ID="UserNameLabel" runat="server" /></td>
    <td>
        <asp:Literal ID="UserName" runat="server"></asp:Literal>
    </td>
    </tr>
    <tr>
    <td align="right">
        <asp:Label ID="QuestionLabel" runat="server" /></td>
    <td>
        <asp:Literal ID="Question" runat="server"></asp:Literal>
    </td>
    </tr>
    <tr>
    <td align="right">
        <asp:Label ID="AnswerLabel" runat="server" AssociatedControlID="Answer" /></td>
    <td>
        <asp:TextBox ID="Answer" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="AnswerRequired" runat="server" ControlToValidate="Answer"  Display="Dynamic"></asp:RequiredFieldValidator>
    </td>
    </tr>
    <tr>
    <td align="center" colspan="2" style="color: red">
        <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
    </td>
    </tr>
    <tr>
    <td align="right" colspan="2">
        <asp:Button ID="SubmitButton" Visible="false" runat="server" CommandName="Submit" />
        <asp:LinkButton ID="SubmitLinkButton" Visible="false" runat="server" CommandName="Submit" />
        <asp:ImageButton ID="SubmitImageButton" Visible="false" runat="server" CommandName="Submit" />
    </td>
    </tr>
</table>