<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Async="true" Inherits="Yemekler.Main" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Rastgele Tarifler Bölümü -->
    <div class="recipes">
        <asp:Repeater ID="rptRandomRecipes" runat="server">
            <ItemTemplate>
                <div class="recipe-card" onclick="window.location.href='Detay.aspx?Id=<%# Eval("Id") %>'">
                    <img src='<%# Eval("ResimUrl") %>' alt='<%# Eval("YemekAdi") %>' />
                    <h4><%# Eval("YemekAdi") %></h4>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f8f8f8;
            margin: 0;
            padding: 0;
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
            transition: transform 0.3s ease, box-shadow 0.3s ease;
            cursor: pointer;
            text-align: center;
        }

        .recipe-card:hover {
            transform: scale(1.05);
            box-shadow: 0 8px 16px rgba(0, 0, 0, 0.2);
        }

        .recipe-card img {
            width: 100%;
            height: 200px;
            object-fit: cover;
        }

        .recipe-card h4 {
            margin: 10px 0;
            font-size: 18px;
            color: #333;
        }
    </style>

</asp:Content>
