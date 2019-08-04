<%@ Page Title="" Language="C#" MasterPageFile="~/Anonymous.Master" AutoEventWireup="true" CodeFile="LogIn.aspx.cs" Inherits="LogIn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:UpdatePanel ID="pnlLogin" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="container card p-3 shadow-cube col" style="background-color: #ffffff; border-radius: 20px; margin-top: 50px; width: 500px;">
                <div style="text-align: center" class="form-group col-sm-12">
                    <asp:Image ID="Image3" runat="server" Width="300px" ImageUrl="~/Content/Images/ChatCubeLogo.png" />
                </div>
                <div class="form-group col-sm-12">
                    <div class="form-group">
                        <label class="control-label">Email Address</label>
                        <div class="form-group">
                            <div class="input-group mb-3">
                                <div class="input-group-prepend ">
                                    <span class="Icon input-group-text fas fa-user"></span>
                                </div>
                                <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <asp:RequiredFieldValidator runat="server" CssClass="badge badge-danger" Display="Dynamic" ID="rfvEmail" ControlToValidate="txtEmail" ErrorMessage="Required*"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="regexEmailValid" Font-Size="Small" CssClass="badge badge-danger" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmail" ErrorMessage="Invalid Email Format">
                            </asp:RegularExpressionValidator>
                        </div>
                    </div>
                </div>


                <div class="form-group col-sm-12">
                    <div class="form-group">
                        <label class="control-label">Password</label>
                        <div class="form-group">
                            <div class="input-group mb-3">
                                <div class="input-group-prepend">
                                    <span class="Icon input-group-text fas fa-key"></span>
                                </div>
                                <asp:TextBox ID="txtPassword" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <asp:RequiredFieldValidator runat="server" CssClass="badge badge-danger" Display="Dynamic" ID="rfvPassword" ControlToValidate="txtPassword" ErrorMessage="Required*"></asp:RequiredFieldValidator>
                            <div id="divError" style="display: none" runat="server" class="alert alert-dismissible alert-danger">
                                <asp:LinkButton ID="btnCloseError" runat="server" CssClass="close" OnClick="btnCloseError_Click"><span class="fas fa-times"></span></asp:LinkButton>
                                <h4 class="alert-heading">Error Information!</h4>
                                <asp:Label ID="lblStrength" ForeColor="Red" runat="server" Text="Label"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <a href="#">Forgot Password? Click Here</a>
                </div>
                <div class="form-group col-sm-12">
                </div>
                <div class="form-group col-sm-12">

                    <div class="input-group mb-3">
                        <asp:CheckBox ID="cbxRememberMe" CssClass="btn" runat="server" />
                        <div class="input-group-append">
                            <span class="Icon input-group-text" style="font-size: medium">Remeber Me ?</span>
                        </div>
                    </div>

                </div>

                <div class="form-group col-sm-12">
                    <asp:LinkButton ID="lbtnLogIn" OnClick="btnLogIn_Click" CssClass="btn col-sm-12" runat="server">Log In &nbsp;&nbsp;<span class="fas fa-door-open" style="font-size:x-large"></span></asp:LinkButton>
                </div>
                <div class="form-group col-sm-12">
                    <asp:LinkButton ID="lbtnRegister" PostBackUrl="~/Views/Registration.aspx" CssClass="btn col-sm-12" runat="server">Sign up &nbsp;&nbsp;<span class="fas fa-pen" style="font-size:x-large"></span></asp:LinkButton>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="cbxRememberMe" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

