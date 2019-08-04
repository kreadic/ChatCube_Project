<%@ Page Title="" Language="C#" MasterPageFile="~/Anonymous.master" AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="Views_Registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .form-control {
            border-color: #7c2020;
        }

            .form-control:focus {
                border-color: #7c2020;
                box-shadow: 0 0 0 0.3rem rgba(124, 32, 32, 0.25);
            }

        .Icon {
            color: #fff;
            background-color: #7c2020;
            font-size: large;
        }

        .btn {
            color: #7c2020;
            border-color: #7c2020;
        }

            .btn:hover {
                color: #fff;
                background-color: #7c2020;
            }

            .btn:focus .btn:active {
                color: #fff;
                background-color: #7c2020;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="pnlRegistration" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="card mb-3 p-4 shadow-cube" style="background-color: white; max-width: 800px; margin: 50px auto; color: #280202">
                <div class="card-header text-center" style="background-color: #7c2020; padding-bottom: 100px; margin-bottom: -100px">
                    <asp:ImageButton ID="ibntLogo" ImageUrl="~/Content/Images/ChatCubeLogoWhite.png" OnClick="ibntProfilePhoto_Click" Width="300px" runat="server" />
                    <h1 style="color: white">Registration</h1>
                </div>
                <div class="border-light row rounded rounded-circle" style="margin: 0 auto; max-width: 200px">
                    <asp:Image ID="imgProfilePhoto" CssClass="rounded rounded-circle" ImageUrl="~/Content/Images/default_Photo.jpg" BorderColor="#7c2020" BorderStyle="Solid" BorderWidth="5px" BackColor="#7c2020" Width="150px" Height="150px" runat="server" />
                </div>
                <div class="card-body">
                    <h3 style="margin: 0 auto">Personal Information</h3>
                    <hr style="background-color: #7c2020;">
                    <div class="row">
                        <div class="form-group col-lg-6  col-sm-12">
                            <label class="control-label">First Name</label>
                            <asp:TextBox ID="txtFirstname" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" CssClass="badge badge-danger" Display="Dynamic" ID="rfvFirstname" ControlToValidate="txtFirstname" ErrorMessage="Required*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group col-lg-6 col-sm-12">
                            <label class="control-label">Middle Name</label>
                            <asp:TextBox ID="txtMiddleName" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-lg-6  col-sm-12">
                            <label class="control-label">Surname</label>
                            <asp:TextBox ID="txtSurname" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" CssClass="badge badge-danger" Display="Dynamic" ID="rfvSurname" ControlToValidate="txtSurname" ErrorMessage="Required*"></asp:RequiredFieldValidator>
                        </div>

                        <div class="form-group col-lg-6  col-sm-12">
                            <div class="row">
                                <div class="form-group">
                                    <label class="control-label">Male</label>
                                    <div class="form-group col">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="Icon input-group-text fas fa-male"></span>
                                                <asp:CheckBox ID="cbxMale" CssClass="btn" AutoPostBack="true" OnCheckedChanged="cbxMale_CheckedChanged" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label">Female</label>
                                    <div class="form-group col">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="Icon input-group-text fas fa-female"></span>
                                                <asp:CheckBox ID="cbxFemale" CssClass="btn" AutoPostBack="true" OnCheckedChanged="cbxMale_CheckedChanged" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--<div class="row ">
                        <div class="form-group col-sm-12">
                            <label class="control-label">Upload Profile Photo</label>
                            <div class="form-group">
                                <div class="input-group mb-3">
                                    <div class="input-group-prepend">
                                        <span class="Icon input-group-text fas fa-image"></span>
                                        <ajaxToolkit:AjaxFileUpload ID="fuProfilePhoto" runat="server" AutoStartUpload="true" OnUploadCompleteAll="fuProfilePhoto_UploadCompleteAll"/>
                                        <asp:Button ID="btnUploadProfilePhoto" Visible="false" runat="server" OnClick="btnUploadProfilePhoto_Click" Text="Upload" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>--%>
                </div>
                <div class="card-body">
                    <h3 style="margin: 0 auto">Additional information</h3>
                    <hr style="background-color: #7c2020;">
                    <div class="row">
                        <div class="form-group col-lg-6  col-sm-12">
                            <label class="control-label">Email Address</label>
                            <div class="form-group">
                                <div class="input-group mb-3">
                                    <div class="input-group-prepend">
                                        <span class="Icon input-group-text fas fa-at chatcube-icon"></span>
                                    </div>
                                    <asp:TextBox ID="txtEmail" OnTextChanged="txtEmail_TextChanged" CssClass="form-control" AutoPostBack="true" runat="server"></asp:TextBox>
                                </div>
                                <asp:RequiredFieldValidator runat="server" CssClass="badge badge-danger" Display="Dynamic" ForeColor="#7c2020" ID="rfvEmail" ControlToValidate="txtEmail" ErrorMessage="Required*"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="regexEmailValid" Font-Size="Small" ForeColor="#7c2020" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmail" ErrorMessage="Invalid Email Format">
                                </asp:RegularExpressionValidator>

                            </div>
                        </div>
                        <div class="form-group col-lg-6 col-sm-12">
                            <label class="control-label">Cell Phone Number</label>
                            <div class="form-group">
                                <div class="input-group mb-3">
                                    <div class="input-group-prepend">
                                        <span class="Icon input-group-text fas fa-phone"></span>
                                    </div>
                                    <asp:TextBox ID="txtCellNumber" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <asp:RequiredFieldValidator runat="server" CssClass="badge badge-danger" Display="Dynamic" ID="rfvCellNumber" ControlToValidate="txtCellNumber" ErrorMessage="Required*"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-sm-12">
                            <label class="control-label">Bio</label>
                            <div class="form-group">
                                <div class="input-group mb-3">
                                    <div class="input-group-prepend">
                                        <span class="Icon input-group-text fas fa-book-open"></span>
                                    </div>
                                    <asp:TextBox ID="txtBio" TextMode="MultiLine" Rows="4" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card-body" id="divSecurity" runat="server" style="display: none">
                    <h3 style="margin: 0 auto">Security</h3>
                    <hr style="background-color: black;" />
                    <div class="row">
                        <div class="form-group col-lg-6 col-sm-12">
                            <label class="control-label">Password</label>
                            <div class="form-group">
                                <div class="input-group mb-3">
                                    <div class="input-group-prepend">
                                        <span class="Icon input-group-text fas fa-key"></span>
                                    </div>
                                    <asp:TextBox ID="txtPassword" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <asp:RequiredFieldValidator runat="server" Enabled="false" CssClass="badge badge-danger" Display="Dynamic" ID="rfvPassword" ControlToValidate="txtPassword" ErrorMessage="Required*"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group col-lg-6  col-sm-12">
                            <label class="control-label">Confirm Password</label>
                            <asp:TextBox ID="txtConfirmPassword" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" Enabled="false" CssClass="badge badge-danger" Display="Dynamic" ID="rfvConfirmPassword" ControlToValidate="txtConfirmPassword" ErrorMessage="Required*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group col-lg-6  col-sm-12">
                            <label class="control-label">Security Question</label>
                            <asp:DropDownList ID="ddlQuestions" CssClass="form-control dropdown-toggle" runat="server">
                                <asp:ListItem>What is your dog's name?</asp:ListItem>
                                <asp:ListItem>What is your mother's maiden name?</asp:ListItem>
                                <asp:ListItem>What is your favourite movie?</asp:ListItem>
                                <asp:ListItem>What is your favourite colour?</asp:ListItem>
                                <asp:ListItem>What is your favourite food?</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" Enabled="false" CssClass="badge badge-danger" Display="Dynamic" ID="rfvQuestions" ControlToValidate="ddlQuestions" ErrorMessage="Required*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group col-lg-6  col-sm-12">
                            <label class="control-label">Answer</label>
                            <asp:TextBox ID="txtAnswer" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" Enabled="false" CssClass="badge badge-danger" Display="Dynamic" ID="rfvAnswer" ControlToValidate="txtAnswer" ErrorMessage="Required*"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div id="divError" style="display: none" runat="server" class="alert alert-dismissible alert-danger col-sm-12">
                            <asp:LinkButton ID="btnCloseError" runat="server" CssClass="close" OnClick="btnCloseError_Click"><span class="fas fa-times"></span></asp:LinkButton>
                            <h4 class="alert-heading">Error Information!</h4>
                            <asp:Label ID="lblStrength" ForeColor="Red" runat="server" Text="Label"></asp:Label>
                        </div>
                    </div>


                </div>
                <hr style="background-color: #7c2020;">
                <div class="card-footer">
                    <div class="form-group col-sm-12">
                        <asp:LinkButton ID="lbtnRegister" CssClass="btn col-sm-12" runat="server" OnClick="lbtnRegister_Click">Resgister &nbsp;&nbsp;<span class="fas fa-pen-fancy" style="font-size:xx-large"></span></asp:LinkButton>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

