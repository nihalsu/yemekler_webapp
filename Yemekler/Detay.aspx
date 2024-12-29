<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="Detay.aspx.cs" Inherits="Yemekler.Detay" %>

<!DOCTYPE html>
<html lang="tr">
<head runat="server">
    <meta charset="utf-8" />
    <title>Yemek Detayı</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            background-color: #fdf5e6;
        }

        .container {
            max-width: 1100px;
            margin: 30px auto;
            padding: 40px;
            background-color: #fff8f1;
            border-radius: 10px;
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
            border: 2px solid #d1c4b1;
        }

        .recipe-title {
            text-align: center;
            font-size: 36px;
            color: #d2691e;
            font-family: 'Dancing Script', cursive;
            margin-bottom: 20px;
        }

        .recipe-info {
            display: flex;
            gap: 30px;
            margin-bottom: 30px;
        }
        .recipe-image img {
            width: 100%;
            height: 100%; /* Çerçeveye tam oturmasını sağlar */
            object-fit: cover; /* Gerekirse değiştirin: fill veya contain */
            border-radius: 10px; /* Çerçeveye yuvarlaklık uygular */
            display: block; /* Görüntü kenar boşluklarını sıfırlar */
        }

        .recipe-image {
            flex: 1;
            max-width: 400px;
            border-radius: 10px;
            overflow: hidden;
            border: 2px solid #d1b09b;
        }

        

        .recipe-details {
            flex: 2;
        }

        .recipe-details h3 {
            font-size: 24px;
            color: #d2691e;
            margin-bottom: 15px;
        }

        .recipe-details ul {
            list-style-type: square;
            padding-left: 20px;
            font-size: 16px;
            color: #4e4a42;
        }

        .recipe-details ul li {
            margin-bottom: 10px;
        }

        .recipe-instructions {
            margin-top: 20px;
        }

        .recipe-instructions h3 {
            font-size: 24px;
            color: #d2691e;
            margin-bottom: 10px;
        }

        .recipe-instructions p {
            font-size: 16px;
            line-height: 1.6;
            color: #4e4a42;
        }

        .btn-save {
            display: block;
            width: 200px;
            margin: 30px auto 0;
            padding: 12px;
            background-color: #d2691e;
            color: white;
            border: none;
            border-radius: 5px;
            font-size: 18px;
            text-align: center;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }

        .btn-save:hover {
            background-color: #a0522d;
        }
    </style>
    <link href="https://fonts.googleapis.com/css2?family=Dancing+Script&display=swap" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1 class="recipe-title"><asp:Literal ID="ltlYemekAdi" runat="server"></asp:Literal></h1>

            <div class="recipe-info">
                <div class="recipe-image">
                   <img id="recipeImage" runat="server" alt="Tarif Resmi" />
                </div>
                <div class="recipe-details">
                    <h3>Malzemeler:</h3>
                    <ul>
                        <asp:Repeater ID="rptMalzemeler" runat="server">
                            <ItemTemplate>
                                <li><%# Container.DataItem %></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </div>

            <div class="recipe-instructions">
                <h3>Tarif:</h3>
                <p><asp:Literal ID="ltlTarif" runat="server"></asp:Literal></p>
                <asp:Button ID="btnSaveRecipe" runat="server" Text="Kaydet" CssClass="btn-save" OnClick="btnSaveRecipe_Click" />
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
