<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="savedRecipes.aspx.cs" Inherits="YemekTarifleriWeb.savedRecipes" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Kaydedilen Tarifler</title>
    <style>
        table {
            width: 100%;
            border-collapse: collapse;
        }

        table, th, td {
            border: 1px solid black;
        }

        th, td {
            padding: 10px;
            text-align: left;
        }

        th {
            background-color: #f2f2f2;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Kaydedilen Tarifler</h1>
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            <asp:GridView ID="gvSavedRecipes" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="YemekAdi" HeaderText="Yemek Adı" />
                    <asp:BoundField DataField="Kategori" HeaderText="Kategori" />
                    <asp:BoundField DataField="HazirlikSuresi" HeaderText="Hazırlık Süresi" />
                    <asp:BoundField DataField="TarifMetni" HeaderText="Tarif Metni" />
                    <asp:BoundField DataField="Malzemeler" HeaderText="Malzemeler" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
