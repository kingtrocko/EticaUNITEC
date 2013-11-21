<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="EticaUNITEC.Usuarios" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Css/grids/style.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/style1.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/style2.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/style3.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/signup.css" rel="stylesheet" type="text/css" />
   <style type="text/css">
    #form1
    {
        height: 470;
        width: 900px;
        min-height:75%;
    }
    </style>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="wrapper">
        <div class="signuponepage main content clearfix">
                <div class="signup-steps step-1 clearfix">
                    <h1 class="redtext">Usuarios Registrados</h1>
                </div>
        </div>
    </div>
    <div id="container">
        
        <asp:GridView ID="GridView1" 
            GridLines="None"
            AllowPaging="True"
            CssClass="mGrid"
            PagerStyle-CssClass="pgr"
            AlternatingRowStyle-CssClass="alt"

            runat="server" 
            AutoGenerateColumns="False"
            DataKeyNames="UsuarioId" 
            DataSourceID="SqlDataSource1" 
            onrowdeleting="GridView1_RowDeleting">
            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
            <Columns>
                <asp:BoundField DataField="UsuarioId" HeaderText="UsuarioId"
                    InsertVisible="False" ReadOnly="True" SortExpression="UsuarioId" 
                    Visible="False" />
                <asp:BoundField DataField="UsuarioNombre" HeaderText="Nombre" 
                    SortExpression="UsuarioNombre" />
                <asp:BoundField DataField="UsuarioCorreo" HeaderText="Correo Electronico" 
                    SortExpression="UsuarioCorreo" />
                <asp:BoundField DataField="UsuarioTelefono" HeaderText="Teléfono" 
                    SortExpression="UsuarioTelefono" />
                <asp:BoundField DataField="UsuarioDireccion" HeaderText="Dirección" 
                    SortExpression="UsuarioDireccion" />
                <asp:TemplateField ShowHeader="False" HeaderText="Editar">
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" 
                            CommandName="Update" Text="Update"></asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                            CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "~/Seguridad/UpdateUser.aspx?userID=" + Eval("UsuarioId")  %>' Text="Editar">Editar</asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Borrar" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                            CommandName="Delete" 
                            OnClientClick="return confirm('Esta seguro que quiere borrar este usuario?');" 
                            Text="Borrar"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

<PagerStyle CssClass="pgr"></PagerStyle>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:DBLocalConnectionString %>" 
            DeleteCommand="DELETE FROM [Usuarios] WHERE [UsuarioId] = @UsuarioId" 
            InsertCommand="INSERT INTO [Usuarios] ([UsuarioNombre], [UsuarioCorreo], [UsuarioTelefono], [UsuarioDireccion]) VALUES (@UsuarioNombre, @UsuarioCorreo, @UsuarioTelefono, @UsuarioDireccion)" 
            SelectCommand="SELECT [UsuarioId], [UsuarioNombre], [UsuarioCorreo], [UsuarioTelefono], [UsuarioDireccion] FROM [Usuarios]" 
            
            
            UpdateCommand="UPDATE [Usuarios] SET [UsuarioNombre] = @UsuarioNombre, [UsuarioCorreo] = @UsuarioCorreo, [UsuarioTelefono] = @UsuarioTelefono, [UsuarioDireccion] = @UsuarioDireccion WHERE [UsuarioId] = @UsuarioId">
            <DeleteParameters>
                <asp:Parameter Name="UsuarioId" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="UsuarioNombre" Type="String" />
                <asp:Parameter Name="UsuarioCorreo" Type="String" />
                <asp:Parameter Name="UsuarioTelefono" Type="String" />
                <asp:Parameter Name="UsuarioDireccion" Type="String" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="UsuarioNombre" Type="String" />
                <asp:Parameter Name="UsuarioCorreo" Type="String" />
                <asp:Parameter Name="UsuarioTelefono" Type="String" />
                <asp:Parameter Name="UsuarioDireccion" Type="String" />
                <asp:Parameter Name="UsuarioId" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
    
        <br />
        <asp:Button ID="Button1" class="g-button" runat="server" onclick="Button1_Click" 
            Text="Agregar Nuevo" />
        <br />
    
    </div>
    </form>
</body>
</html>
