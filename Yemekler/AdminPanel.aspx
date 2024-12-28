<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="AdminPanel.aspx.cs" Inherits="Yemekler.AdminPanel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Yemek Tarifleri Yönetim Paneli</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Yemek Tarifleri Yönetim Paneli</h1>
            <asp:Button ID="btnFetchRecipes" runat="server" Text="Tarifleri Güncelle" OnClick="btnFetchRecipes_Click" />
            <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>
