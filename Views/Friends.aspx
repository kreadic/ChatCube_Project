<%@ Page Title="" Language="C#" MasterPageFile="~/LogInMaster.master" AutoEventWireup="true" CodeFile="Friends.aspx.cs" Inherits="Views_Friends" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin:0 auto;">
        <ul id="ulFriends" runat="server" class="list-group col-lg-5  col-sm-12 m-1">
            <li class="header list-group-item" style="background-color: #7c2020; border-color: white; color: white">
                <h4>Friends</h4>
            </li>
        </ul>
        <ul id="ulPeople" runat="server" class="list-group col-lg-5 col-sm-12 m-1">
            <li class="header list-group-item" style="background-color: #7c2020; border-color: white; color: white">
                <h4>Registered People</h4>
            </li>
        </ul>
    </div>
    <div class="row" style="margin:0 auto;">
        <ul id="ulPending" runat="server" class="list-group col-lg-5  col-sm-12 m-1">
            <li class="header list-group-item" style="background-color: #7c2020; border-color: white; color: white">
                <h4>Pending sent Invites</h4>

            </li>
        </ul>
        <ul id="ulReceivedInvites" runat="server" class="list-group col-lg-5  col-sm-12 m-1">
            <li class="header list-group-item" style="background-color: #7c2020; border-color: white; color: white">
                <h4>Pending Received Invites</h4>
            </li>
        </ul>
    </div>

</asp:Content>

