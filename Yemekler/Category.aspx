<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Category.aspx.cs" Inherits="Yemekler.Category" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="category-container">
        <h1 class="category-title"><asp:Label ID="lblCategoryName" runat="server" Text=""></asp:Label></h1>

        <div class="recipes">
            <asp:Repeater ID="rptCategoryRecipes" runat="server">
                <ItemTemplate>
                    <div class="recipe-card">
                        <a href="Detay.aspx?Id=<%# Eval("Id") %>">
                            <img src='<%# Eval("ResimUrl") %>' alt='<%# Eval("YemekAdi") %>' />
                            <h4><%# Eval("YemekAdi") %></h4>
                            <p><i class="fa fa-clock"></i> <%# Eval("HazirlikSuresi") %> dk</p>
                        </a>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>

    <style>
        /* Ana kapsayıcı */
        .category-container {
            max-width: 1200px;
            margin: 0 auto;
            padding: 20px;
            background-color: #fdf5e6;
            border-radius: 10px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
        }

        .category-title {
            text-align: center;
            font-size: 28px;
            color: #d2691e;
            margin-bottom: 20px;
            font-family: 'Dancing Script', cursive;
        }

        /* Tarif kartları düzeni */
        .recipes {
            display: grid;
            grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
            gap: 20px;
            padding: 10px 0;
        }

        .recipe-card {
            background-color: #fff;
            border-radius: 10px;
            overflow: hidden;
            text-align: center;
            transition: transform 0.3s ease, box-shadow 0.3s ease;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
        }

        .recipe-card:hover {
            transform: scale(1.05);
            box-shadow: 0 8px 16px rgba(0, 0, 0, 0.2);
        }

        

        /* Tarif adı */
        .recipe-card h4 {
            font-size: 18px;
            color: #2c3e50;
            margin: 10px 0;
            font-family: Arial, sans-serif;
        }

        /* Hazırlık süresi */
        .recipe-card p {
            font-size: 14px;
            color: #777;
            margin: 0 0 10px 0;
            font-family: Arial, sans-serif;
        }

        
        /* Tarif kartları düzeni */
        .recipe-card img {
            width: 100%;
            height: 200px;
            object-fit: cover;
            border-bottom: 2px solid #fddede; /* Çok açık pastel kırmızı/pembe */
        }

        /* Detay bağlantısı */
        .recipe-card a {
            text-decoration: none;
            color: inherit;
            display: block;
            padding: 10px;
            font-size: 14px;
            background-color: #fde4e4; /* Çok açık pastel kırmızı/pembe */
            color: #333; /* Yazıyı daha okunabilir yapmak için koyu gri */
            border-radius: 5px;
            margin: 10px auto;
            max-width: 80%;
            transition: background-color 0.3s ease, color 0.3s ease;
        }

        .recipe-card a:hover {
            background-color: #fbcaca; /* Hover sırasında biraz daha belirgin ama hala açık pastel */
            color: #000; /* Yazı hover sırasında siyaha dönüyor */
        }

    </style>
</asp:Content>
