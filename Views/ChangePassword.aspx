<%@ Page Title="" Language="C#" MasterPageFile="~/Anonymous.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="Views_ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="pnlLogin" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="container card p-3 shadow-cube col" style="background-color: #ffffff; border-radius: 20px; margin-top: 50px; width: 500px;">
                <div style="text-align: center" class="form-group col-sm-12">
                    <asp:Image ID="Image3" runat="server" Width="300px" ImageUrl="~/Content/Images/ChatCubeLogo.png" />
                </div>
                <h3 style="margin: 0 auto">Reset Password</h3>
                <hr style="background-color: #7c2020;" />
                <div id="divError" style="display: none" runat="server" class="alert alert-dismissible alert-danger">
                                <asp:LinkButton ID="btnCloseError" runat="server" CssClass="close" OnClick="btnCloseError_Click"><span class="fas fa-times"></span></asp:LinkButton>
                                <h4 class="alert-heading">Error Information!</h4>
                                <asp:Label ID="lblStrength" ForeColor="Red" runat="server" Text="Label"></asp:Label>
                            </div>
                <div class="row">
                    <div class="form-group col-lg-6  col-sm-12">
                        <label class="control-label">Security Question</label>
                        <asp:DropDownList ID="ddlQuestions" CssClass="form-control dropdown-toggle" runat="server">
                            <asp:ListItem>What is your dog's name?</asp:ListItem>
                            <asp:ListItem>What is your mother's maiden name?</asp:ListItem>
                            <asp:ListItem>What is favourite movie?</asp:ListItem>
                            <asp:ListItem>What is favourite colour?</asp:ListItem>
                            <asp:ListItem>What is favourite food?</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" CssClass="badge badge-danger" Display="Dynamic" ID="rfvQuestions" ControlToValidate="ddlQuestions" ErrorMessage="Required*"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group col-lg-6  col-sm-12">
                        <label class="control-label">Answer</label>
                        <asp:TextBox ID="txtAnswer" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" CssClass="badge badge-danger" Display="Dynamic" ID="rfvAnswer" ControlToValidate="txtAnswer" ErrorMessage="Required*"></asp:RequiredFieldValidator>

                    </div>
                </div>
                <div class="row">
                    <div class="form-group col-sm-12">
                        <label class="control-label">Old Password</label>
                        <div class="form-group">
                            <div class="input-group mb-3">
                                <div class="input-group-prepend">
                                    <span class="Icon input-group-text fas fa-clock"></span>
                                </div>
                                <asp:TextBox ID="txtOldPassword" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <asp:RequiredFieldValidator runat="server" CssClass="badge badge-danger" Display="Dynamic" ID="rfvOldPassword" ControlToValidate="txtOldPassword" ErrorMessage="Required*"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group col-sm-12">
                        <label class="control-label">New Password</label>
                        <div class="form-group">
                            <div class="input-group mb-3">
                                <div class="input-group-prepend">
                                    <span class="Icon input-group-text fas fa-key"></span>
                                </div>
                                <asp:TextBox ID="txtNewPassword" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <asp:RequiredFieldValidator runat="server" CssClass="badge badge-danger" Display="Dynamic" ID="rfvNewPassword" ControlToValidate="txtNewPassword" ErrorMessage="Required*"></asp:RequiredFieldValidator>

                        </div>
                    </div>
                    <div class="form-group col-sm-12">
                        <label class="control-label">Confirm Password</label>
                        <asp:TextBox ID="txtConfirm" CssClass="form-control" TextMode="Password" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" CssClass="badge badge-danger" Display="Dynamic" ID="rfvConfirm" ControlToValidate="txtConfirm" ErrorMessage="Required*"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group col-sm-12">
                        <asp:LinkButton ID="lbtnReset" OnClick="lbtnReset_Click" CssClass="btn col-sm-12" runat="server">Reset &nbsp;&nbsp;<span class="fas fa-undo-alt" style="font-size:x-large"></span></asp:LinkButton>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

