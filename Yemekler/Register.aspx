<%@ Page Language="C#" Async="true" AutoEventWireup="true" MasterPageFile="~/MasterPage.Master" CodeBehind="Register.aspx.cs" Inherits="Yemekler.Register" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <style>
        body {
            font-family: 'Arial', sans-serif;
            background-color: #f8f4ec;
            margin: 0;
            padding: 0;
        }

        .register-container {
            width: 40%;
            margin: 100px auto;
            padding: 30px;
            background-color: #fff;
            border-radius: 10px;
            box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);
            text-align: center;
        }

        .register-container h2 {
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

        .chk-agreement {
            display: flex;
            align-items: center;
            justify-content: start;
            font-size: 14px;
            color: #666;
            margin-top: 15px;
            width: 90%;
            margin-left: auto;
            margin-right: auto;
        }

        .btn-primary {
            display: inline-block;
            width: 90%;
            padding: 12px;
            margin-top: 20px;
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

        .agreement-text {
            font-size: 14px;
            margin-top: 15px;
            color: #666;
        }

        .agreement-text a {
            color: #007bff;
            text-decoration: none;
        }

        .agreement-text a:hover {
            text-decoration: underline;
        }
    </style>
    <div class="register-container">
        <h2>Kayıt Ol</h2>
        <!-- Hata Mesajları -->
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>

        <!-- Kullanıcı Adı Girişi -->
        <asp:TextBox 
            ID="txtUsername" 
            runat="server" 
            CssClass="form-control" 
            placeholder="Kullanıcı adınızı giriniz..." />
        
        <!-- Email Adresi Girişi -->
        <asp:TextBox 
            ID="txtEmail" 
            runat="server" 
            CssClass="form-control" 
            placeholder="Email adresinizi giriniz..." />
        
        <!-- Şifre Girişi -->
        <asp:TextBox 
            ID="txtPassword" 
            runat="server" 
            CssClass="form-control" 
            placeholder="Şifrenizi giriniz..." 
            TextMode="Password" />
        
        <!-- Kullanıcı Sözleşmesi -->
        <div class="chk-agreement">
            <asp:CheckBox 
                ID="chkAgreement" 
                runat="server" 
                Text="Kullanıcı sözleşmesini onaylıyorum" />
        </div>

        <!-- Kayıt Ol Butonu -->
        <asp:Button 
            ID="btnRegister" 
            runat="server" 
            Text="Kayıt Ol" 
            OnClick="btnRegister_Click" 
            CssClass="btn-primary" />
    </div>
</asp:Content>
