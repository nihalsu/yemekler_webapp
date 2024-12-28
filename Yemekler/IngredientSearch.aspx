<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="IngredientSearch.aspx.cs" Inherits="Yemekler.IngredientSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="page-title">Malzemelere Göre Tarif Ara</h2>
    <div class="ingredient-container">
        <!-- Malzeme Ekleme Alanı -->
        <div class="add-ingredient">
            <label for="txtIngredient">Malzeme Ekle:</label>
            <asp:TextBox ID="txtIngredient" runat="server" placeholder="Malzeme ekleyin" CssClass="ingredient-input"></asp:TextBox>
            <asp:Button ID="btnAddIngredient" runat="server" Text="Ekle" OnClick="btnAddIngredient_Click" CssClass="add-btn" />
        </div>

        <!-- Eklenen Malzemeler -->
        <ul class="ingredient-list">
            <asp:Repeater ID="rptIngredients" runat="server">
                <ItemTemplate>
                    <li class="ingredient-item">
                        <%# Container.DataItem %>
                        <asp:Button ID="btnRemoveIngredient" runat="server" Text="Kaldır" CommandArgument='<%# Container.DataItem %>' OnClick="btnRemoveIngredient_Click" CssClass="remove-btn" />
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>

        <!-- Tarif Önerme Butonu -->
        <asp:Button ID="btnSearchRecipes" runat="server" Text="Tarif Öner" OnClick="btnSearchRecipes_Click" CssClass="search-recipes-btn" />
    </div>

    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #fdf5e6;
            margin: 0;
            padding: 0;
        }

        .page-title {
            text-align: center;
            font-size: 24px;
            color: #e74c3c;
            margin-bottom: 20px;
        }

        .ingredient-container {
            max-width: 800px;
            margin: 0 auto;
            background-color: #fff;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }

        .add-ingredient {
            display: flex;
            gap: 10px;
            align-items: center;
            margin-bottom: 20px;
        }

        .ingredient-input {
            flex: 1;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }

        .add-btn {
            padding: 10px 20px;
            background-color: #e74c3c;
            color: white;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }

        .add-btn:hover {
            background-color: #c0392b;
        }

        .ingredient-list {
            list-style: none;
            padding: 0;
            margin: 0 0 20px;
        }

        .ingredient-item {
            background-color: #f8c291;
            padding: 10px;
            border-radius: 5px;
            margin-bottom: 10px;
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

        .remove-btn {
            background-color: #e74c3c;
            color: white;
            border: none;
            padding: 5px 10px;
            border-radius: 5px;
            cursor: pointer;
            font-size: 12px;
            transition: background-color 0.3s ease;
        }

        .remove-btn:hover {
            background-color: #c0392b;
        }

        .search-recipes-btn {
            display: block;
            width: 100%;
            text-align: center;
            padding: 15px;
            background-color: #f76c6c;
            color: white;
            border: none;
            border-radius: 5px;
            font-size: 16px;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }

        .search-recipes-btn:hover {
            background-color: #e94e4e;
        }
    </style>
</asp:Content>
