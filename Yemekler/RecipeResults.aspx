<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="RecipeResults.aspx.cs" Inherits="Yemekler.RecipeResults" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="page-title">Tarif Sonuçları</h2>
    <div class="recipes">
        <asp:Repeater ID="rptRecipes" runat="server">
            <ItemTemplate>
                <div class="recipe-card">
                    <a href='Detay.aspx?Id=<%# Eval("Id") %>'>
                        <img src='<%# Eval("ResimUrl") %>' alt='<%# Eval("YemekAdi") %>' />
                        <h3><%# Eval("YemekAdi") %></h3>
                    </a>
                </div>
            </ItemTemplate>
        </asp:Repeater>
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

        .recipes {
            display: grid;
            grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
            gap: 20px;
            padding: 20px;
            max-width: 1200px;
            margin: 0 auto;
        }

        .recipe-card {
            background-color: #fff;
            border-radius: 10px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
            overflow: hidden;
            text-align: center;
            transition: transform 0.3s ease, box-shadow 0.3s ease;
        }

        .recipe-card:hover {
            transform: scale(1.05);
            box-shadow: 0 8px 16px rgba(0, 0, 0, 0.2);
        }

        .recipe-card img {
            width: 100%;
            height: 200px;
            object-fit: cover;
            border-bottom: 2px solid #f8c291;
        }

        .recipe-card h3 {
            margin: 10px 0;
            font-size: 18px;
            color: #2c3e50;
        }

        .recipe-card a {
            text-decoration: none;
            color: inherit;
        }

        .recipe-card a:hover h3 {
            color: #e74c3c;
        }
    </style>
</asp:Content>
