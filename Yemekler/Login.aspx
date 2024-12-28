<%@ Page Language="C#" Async="true" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Yemekler.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <style>
        body {
            font-family: 'Arial', sans-serif;
            background-color: #f8f4ec;
            margin: 0;
            padding: 0;
        }

        .login-container {
            width: 40%;
            margin: 100px auto;
            padding: 30px;
            background-color: #fff;
            border-radius: 10px;
            box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);
            text-align: center;
        }

        .login-container h2 {
            font-size: 28px;
            margin-bottom: 20px;
            color: #333;
        }

        .form-control {
            width: 90%;
            margin: 10px auto;
            padding: 12px;
            font-size: 16px;
            border: 1px solid #ccc;
            border-radius: 5px;
            box-sizing: border-box;
            background-color: #f8f4ec;
        }

        .form-control:focus {
            border: 1px solid #ffba08;
            outline: none;
        }

        .btn-primary {
            display: inline-block;
            width: 90%;
            padding: 12px;
            margin-top: 10px;
            font-size: 16px;
            font-weight: bold;
            background-color: #007bff;
            color: #fff;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }

        .btn-primary:hover {
            background-color: #0056b3;
        }

        .register-link p {
            font-size: 14px;
            margin-top: 15px;
            color: #666;
        }

        .register-link a {
            color: #007bff;
            text-decoration: none;
        }

        .register-link a:hover {
            text-decoration: underline;
        }

        .error-message {
            font-size: 14px;
            color: #d9534f;
            margin-bottom: 15px;
        }
    </style>

    <div class="login-container">
        <h2>Giriş Yap</h2>
        <!-- Hata mesajlarını göstermek için -->
        <asp:Label ID="lblMessage" runat="server" CssClass="error-message" Visible="false"></asp:Label>
        
        <!-- Email Girişi -->
        <asp:TextBox 
            ID="txtEmail" 
            runat="server" 
            CssClass="form-control" 
            placeholder="Email adresinizi giriniz..."></asp:TextBox>
        
        <!-- Şifre Girişi -->
        <asp:TextBox 
            ID="txtPassword" 
            runat="server" 
            CssClass="form-control" 
            placeholder="Şifre giriniz..." 
            TextMode="Password"></asp:TextBox>
        
        <!-- Giriş Butonu -->
        <asp:Button 
            ID="btnLogin" 
            runat="server" 
            Text="Giriş" 
            OnClick="btnLogin_Click" 
            CssClass="btn-primary" />
        
        <!-- Kayıt Ol Linki -->
        <div class="register-link">
            <p>Hala hesabınız yok mu? <a href="Register.aspx">Kayıt olun!</a></p>
        </div>
    </div>
</asp:Content>


