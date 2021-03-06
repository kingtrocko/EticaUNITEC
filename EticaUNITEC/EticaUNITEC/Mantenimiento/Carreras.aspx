﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Carreras.aspx.cs" Inherits="EticaUNITEC.Carreras" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <link href="../Css/grids/style.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/style1.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/style2.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/style3.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/signup.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="wrapper">
        <div class="signuponepage main content clearfix">
                <div class="signup-steps step-1 clearfix">
                    <h1 class="redtext">Carreras Registradas</h1>
                </div>
        </div>
    </div>
    <div id="container">     
        <asp:GridView ID="Grid" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="CarreraId" DataSourceID="SqlData" 
            onrowediting="Grid_RowEditing"
            
            GridLines="None"
            AllowPaging="True"
            CssClass="mGrid"
            PagerStyle-CssClass="pgr"
            AlternatingRowStyle-CssClass="alt">
            <Columns>
                <asp:BoundField DataField="CarreraId" HeaderText="Id" InsertVisible="False" 
                    ReadOnly="True" SortExpression="CarreraId" />
                <asp:BoundField DataField="CarreraCodigo" HeaderText="Codigo" 
                    SortExpression="CarreraCodigo" />
                <asp:BoundField DataField="CarreraNombre" HeaderText="Nombre" 
                    SortExpression="CarreraNombre" />
                <asp:TemplateField HeaderText="Editar" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                            CommandName="Edit" Text="Editar"></asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" 
                            CommandName="Update" Text="Update"></asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                            CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Eliminar">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                            CommandName="Delete" 
                            OnClientClick="return confirm('¿Estas Seguro que Deseas Borrar?')" 
                            Text="Eliminar"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlData" runat="server" 
            ConflictDetection="CompareAllValues" 
            ConnectionString="<%$ ConnectionStrings:DBLocalConnectionString %>" 
            DeleteCommand="DELETE FROM [Carreras] WHERE [CarreraId] = @original_CarreraId AND [CarreraCodigo] = @original_CarreraCodigo AND [CarreraNombre] = @original_CarreraNombre" 
            InsertCommand="INSERT INTO [Carreras] ([CarreraCodigo], [CarreraNombre]) VALUES (@CarreraCodigo, @CarreraNombre)" 
            OldValuesParameterFormatString="original_{0}" 
            SelectCommand="SELECT [CarreraId], [CarreraCodigo], [CarreraNombre] FROM [Carreras]" 
            UpdateCommand="UPDATE [Carreras] SET [CarreraCodigo] = @CarreraCodigo, [CarreraNombre] = @CarreraNombre WHERE [CarreraId] = @original_CarreraId AND [CarreraCodigo] = @original_CarreraCodigo AND [CarreraNombre] = @original_CarreraNombre">
            <DeleteParameters>
                <asp:Parameter Name="original_CarreraId" Type="Int32" />
                <asp:Parameter Name="original_CarreraCodigo" Type="String" />
                <asp:Parameter Name="original_CarreraNombre" Type="String" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="CarreraCodigo" Type="String" />
                <asp:Parameter Name="CarreraNombre" Type="String" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="CarreraCodigo" Type="String" />
                <asp:Parameter Name="CarreraNombre" Type="String" />
                <asp:Parameter Name="original_CarreraId" Type="Int32" />
                <asp:Parameter Name="original_CarreraCodigo" Type="String" />
                <asp:Parameter Name="original_CarreraNombre" Type="String" />
            </UpdateParameters>
        </asp:SqlDataSource>
    
        <br />
        <asp:Button ID="BtnNuevo" runat="server" Text="Agregar Carrera" class="g-button" onclick="BtnNuevo_Click" />
        <br />
    
    </div>
    </form>
</body>
</html>
