<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Account.aspx.cs" Inherits="Yemekler.Account" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Account</title>
</head>
<body>
    <!-- Kullanıcı Karşılama Mesajı -->
    <h1>Hoş Geldiniz, <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label></h1>

    <!-- Tarifleriniz Başlığı ve Liste -->
    <h2>Tarifleriniz</h2>
    <asp:GridView ID="rptSavedRecipes" runat="server" AutoGenerateColumns="false" CssClass="recipes-table">
        <Columns>
            <asp:BoundField DataField="RecipeName" HeaderText="Tarif Adı" />
        </Columns>
    </asp:GridView>
    <asp:Label ID="lblMessage" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>

    <!-- Yeni Tarif Ekleme Formu -->
    <h2>Yeni Tarif Ekle</h2>
    <form id="form1" runat="server">
        <label for="txtRecipeName">Tarif Adı:</label>
        <asp:TextBox ID="txtRecipeName" runat="server" CssClass="recipe-input" />
        <br />
        <asp:Button ID="btnAddRecipe" runat="server" Text="Tarif Ekle" OnClick="btnAddRecipe_Click" CssClass="add-recipe-button" />
        <br />
        <asp:Label ID="lblSuccessMessage" runat="server" Text="" ForeColor="Green"></asp:Label>
    </form>
</body>
</html>
