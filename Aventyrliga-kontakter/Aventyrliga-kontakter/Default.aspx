<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Aventyrliga_kontakter.Default" ViewStateMode="Disabled" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Äventyrliga kontakter</title>
</head>
<body>
    <form id="form" runat="server">
    <div>
        <%-- Tabellen med kontakterna --%>
        <asp:ListView ID="ContactListView" runat="server" selectMethod="ContactListView_GetData" ItemType="Aventyrliga_kontakter.Model.Contact" DataKeyNames="ContactID"
            InsertMethod="ContactListView_InsertItem" UpdateMethod="ContactListView_UpdateItem" DeleteMethod="ContactListView_DeleteItem"
            InsertItemPosition="FirstItem">
            <LayoutTemplate>
                <table>
                    <tr>
                        <th>Förnamn</th>
                        <th>Efternamn</th>
                        <th>Email-Adress</th>
                    </tr>
                    <asp:PlaceHolder ID="ItemPlaceholder" runat="server"></asp:PlaceHolder>
                </table>
            </LayoutTemplate>

            <%-- Items --%>
            <ItemTemplate>
                <tr>
                    <td><%# Item.FirstName %></td>
                    <td><%# Item.LastName %></td>
                    <td><%# Item.EmailAddress %></td>
                </tr>
            </ItemTemplate>

            <%-- Om formuläret är tomt --%>
            <EmptyDataTemplate>
                <p>Det finns ingen data att visa.</p>
            </EmptyDataTemplate>

            <%-- För insert --%>
            <InsertItemTemplate>
                <tr>
                    <td><asp:TextBox ID="FirstName" runat="server" Text="<%# BindItem.FirstName %>"></asp:TextBox></td>
                    <td><asp:TextBox ID="LastName" runat="server" Text="<%# BindItem.LastName %>"></asp:TextBox></td>
                    <td><asp:TextBox ID="EmailAddress" runat="server" Text="<%# BindItem.EmailAddress %>"></asp:TextBox></td>

                    <td><asp:LinkButton CommandName="Insert" Text="Lägg till" runat="server" /></td>
                    <td><asp:LinkButton CommandName="Cancel" Text="Avbryt" CausesValidation="false" runat="server" /></td>
                    <td></td>

                </tr>
            </InsertItemTemplate>

            <%-- För update --%>
            <EditItemTemplate>
                <tr>
                    <td><asp:TextBox ID="FirstName" runat="server" Text="<%# BindItem.FirstName %>"></asp:TextBox></td>
                    <td><asp:TextBox ID="LastName" runat="server" Text="<%# BindItem.LastName %>"></asp:TextBox></td>
                    <td><asp:TextBox ID="EmailAddress" runat="server" Text="<%# BindItem.EmailAddress %>"></asp:TextBox></td>

                    <td><asp:LinkButton CommandName="Update" Text="Lägg till" runat="server" /></td>
                    <td></td>

                </tr>

            </EditItemTemplate>

        </asp:ListView>
    
    </div>
    </form>
</body>
</html>
