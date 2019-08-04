<%@ Page Title="" Language="C#" MasterPageFile="~/LogInMaster.master" AutoEventWireup="true" CodeFile="Profile.aspx.cs" Inherits="Views_Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <h1 style="text-align: center; color: white; font-weight: bolder">Profile Information</h1>
    <div class="row  m-5">
        <div class="col-lg-3 col-md-6 col-sm-12">
            <asp:Image ID="Image2" CssClass="rounded rounded-circle m-1" ImageUrl="~/Content/Images/default_Photo.jpg" BorderColor="#7c2020" BorderStyle="Solid" BorderWidth="5px" BackColor="#7c2020" Width="300px" Height="300px" runat="server" />
        </div>
        <div class="col-lg-8 col-md-6 col-sm-12" style="color: white">
            <h2 style="text-align: center">
                <asp:Label ID="lblFullName" runat="server"></asp:Label></h2>
            <div style="font-size: x-large">
                <div class="row m-2">
                    <span class="fas fa-envelope"></span>&nbsp;&nbsp<asp:Label ID="lblEmail" runat="server" ></asp:Label>
                </div>
                <div class="row m-2">
                    <span class="fas fa-phone"></span>&nbsp;&nbsp<asp:Label ID="lblCellNumber" runat="server"></asp:Label>
                </div>
                <div class="row m-2">
                    <asp:Label ID="lblGender" Font-Size="100px" CssClass="fas fa-male" runat="server"></asp:Label>
                    <asp:Label ID="lblBio" Font-Size="x-large" CssClass="m-1" runat="server"></asp:Label>
                </div>
                <div class="row">
                    <asp:LinkButton ID="btnChangePassword" CssClass="btn btn-light col-sm-12" runat="server" OnClick="btnChangePassword_Click">Change Password &nbsp;&nbsp;<span class="fas fa-undo-alt" style="font-size:x-large"></span></asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
    
</asp:Content>

