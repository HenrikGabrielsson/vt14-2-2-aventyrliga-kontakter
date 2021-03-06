﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Aventyrliga_kontakter.Default" ViewStateMode="Disabled" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Äventyrliga kontakter</title>

    <link href="~/Style/contactStyle.css" rel="stylesheet" />
    <script type="text/javascript" src="Script/AdventureScript.js"></script>
</head>
<body>
    <form id="form" runat="server">
    <div>

        <asp:Panel ID="SuccessPanel" runat="server" Visible="false" CssClass="SuccessPanel" >
            <asp:ImageButton ID="CloseButton" ImageUrl="~/Content/close.png" OnClientClick="closeWindow()" runat="server" />
            <asp:Label ID="SuccessLabel" runat="server" Text="Label"></asp:Label>
        </asp:Panel>    

        <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="Validation" />


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
                <asp:DataPager ID="DataPager" runat="server" PageSize="15">
                    <Fields>
                        <asp:NextPreviousPagerField ShowFirstPageButton="true" ShowLastPageButton="false" ShowNextPageButton="false" ShowPreviousPageButton="false" FirstPageText="Första" ButtonType="Button" />
                        <asp:NumericPagerField ButtonType="Button" />
                        <asp:NextPreviousPagerField ShowLastPageButton="true" ShowFirstPageButton="false" ShowNextPageButton="false" ShowPreviousPageButton="false" LastPageText="Sista" ButtonType="Button" />
                    </Fields>
                </asp:DataPager>
            </LayoutTemplate>

            <%-- Items --%>
            <ItemTemplate>
                <tr>
                    <td><%# Item.FirstName %></td>
                    <td><%# Item.LastName %></td>
                    <td><%# Item.EmailAddress %></td>
                    <td class="Command">
                        <asp:LinkButton runat="server" CommandName="Delete" Text="Ta bort" OnClientClick="return confirmDelete()"/>
                        <asp:LinkButton runat="server" CommandName="Edit" Text="Redigera" CausesValidation="false" />
                    </td>
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
                    <td></td>

                </tr>
            </InsertItemTemplate>

            <%-- För update --%>
            <EditItemTemplate>
                <tr>
                    <td><asp:TextBox ID="FirstName" runat="server" Text="<%# BindItem.FirstName %>"></asp:TextBox></td>
                    <td><asp:TextBox ID="LastName" runat="server" Text="<%# BindItem.LastName %>"></asp:TextBox></td>
                    <td><asp:TextBox ID="EmailAddress" runat="server" Text="<%# BindItem.EmailAddress %>"></asp:TextBox></td>

                    <td><asp:LinkButton CommandName="Update" Text="Uppdatera" runat="server" /></td>
                    <td><asp:LinkButton CommandName="Cancel" Text="Avbryt" runat="server" /></td>

                </tr>

            </EditItemTemplate>

        </asp:ListView>
    
    </div>
    </form>
</body>
</html>
