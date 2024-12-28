<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="AramaSonuclari.aspx.cs" Inherits="Yemekler.AramaSonuclari" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        #aramaSonuclari {
            text-align: center;
            margin-top: 20px;
        }

        .kart {
            display: inline-block;
            width: 200px;
            margin: 20px;
            border: 1px solid #ddd;
            border-radius: 10px;
            box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);
            overflow: hidden;
            text-align: center;
            transition: transform 0.3s;
        }

        .kart:hover {
            transform: scale(1.05);
        }

        .kart img {
            width: 100%;
            height: 150px;
            object-fit: cover;
        }

        .kart h3 {
            padding: 10px;
            font-size: 18px;
            color: #333;
        }

    </style>
     <h2>Arama Sonuçları</h2>
    <asp:Repeater ID="rptAramaSonuclari" runat="server">
        <ItemTemplate>
            <div class="kart">
                <a href='Detay.aspx?Id=<%# Eval("Id") %>'>
                    <img src='<%# Eval("ResimUrl") %>' alt='<%# Eval("YemekAdi") %>' />
                    <h3><%# Eval("YemekAdi") %></h3>
                </a>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
