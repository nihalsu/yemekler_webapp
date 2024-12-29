<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Account.aspx.cs" Inherits="Yemekler.Account" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Account</title>
    <style>
        body {
            font-family: 'Dancing Script', cursive; /* El yazısı tarzında yazı tipi */
            background-color: #f7f4e9;
            margin: 0;
            padding: 0;
        }

        .container {
            max-width: 900px;
            margin: 50px auto;
            background-color: #fff;
            border-radius: 10px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            padding: 20px;
            border: 2px solid #d4af37; /* Defter kenarı görünümü */
        }

        h1 {
            text-align: center;
            font-size: 2.5rem;
            color: #d2691e;
        }

        h2 {
            font-size: 2rem;
            color: #333;
            margin-bottom: 20px;
            text-align: left;
            border-bottom: 2px solid #d4af37;
            padding-bottom: 5px;
        }

        .recipes-table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }

        .recipes-table th, .recipes-table td {
            border: 1px solid #ddd;
            padding: 10px;
            text-align: left;
        }

        .recipes-table th {
            background-color: #f4e3d7;
            color: #333;
        }

        .recipes-table td {
            background-color: #fff;
        }

        .details-button {
            padding: 10px 20px;
            background-color: #ff9800;
            color: white;
            border: none;
            border-radius: 20px;
            cursor: pointer;
            font-family: 'Arial', sans-serif;
            font-size: 1rem;
            transition: background-color 0.3s ease, transform 0.2s ease;
            text-align: center;
            display: inline-block;
        }

        .details-button:hover {
            background-color: #e68a00;
            transform: scale(1.05);
        }

        .message {
            text-align: center;
            font-size: 1.2rem;
            color: #ff0000;
        }

        .success-message {
            color: #28a745;
        }
    </style>
    <link href="https://fonts.googleapis.com/css2?family=Dancing+Script:wght@400;700&display=swap" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Hoş Geldiniz, <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label></h1>
            <h2>Tarifleriniz</h2>
            <asp:GridView ID="rptSavedRecipes" runat="server" AutoGenerateColumns="false" CssClass="recipes-table" OnRowCommand="rptSavedRecipes_RowCommand" DataKeyNames="RecipeId">
                <Columns>
                    <asp:BoundField DataField="RecipeName" HeaderText="Tarif Adı" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <button type="button" class="details-button" onclick="window.location.href='Detay.aspx?Id=<%# Eval("RecipeId") %>'">
                                Detayları Gör
                            </button>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Label ID="lblMessage" runat="server" Text="" Visible="false" CssClass="message"></asp:Label>
        </div>
    </form>
</body>
</html>
