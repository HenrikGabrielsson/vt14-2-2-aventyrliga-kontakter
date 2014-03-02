<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Aventyrliga_kontakter.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Äventyrliga kontakter</title>
</head>
<body>
    <form id="form" runat="server">
    <div>
        <%-- Tabellen med kontakterna --%>
        <asp:ListView ID="ContactListView" runat="server" selectMethod="ContactListView_GetData" ItemType="Aventyrliga_kontakter.Model.Contact" DataKeyNames="ContactID">
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

            <ItemTemplate>
                <tr>
                    <td><%# Item.FirstName %></td>
                    <td><%# Item.LastName %></td>
                    <td><%# Item.EmailAddress %></td>
                </tr>

            </ItemTemplate>
            <EmptyDataTemplate>
                <p>Det finns ingen data att visa.</p>
            </EmptyDataTemplate>


        </asp:ListView>
    
    </div>
    </form>
</body>
</html>
