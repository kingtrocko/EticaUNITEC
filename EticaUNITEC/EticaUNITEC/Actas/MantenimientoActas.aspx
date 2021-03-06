﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MantenimientoActas.aspx.cs" Inherits="EticaUNITEC.MantenimientoActas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript">
        function validar() {
            var tb = document.getElementById("txtArticuloNumero");
            var tb2 = document.getElementById("txtIncisoLetra").value;
            var tb3 = document.getElementById("txtIncisoDescripcion").value;
            var tb4 = document.getElementById("txtIncisoContenido").value;
            if (tb.value == ""  || tb2=="" || tb3=="" || tb4=="") {
                 alert("Falta Datos que Llenar! Ingrese Valor");
                 return false;

             }

             
             return confirm("Estas seguro????");
         }
        
    </script>


</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Articulo Numero: "></asp:Label>
        <asp:TextBox ID="txtArticuloNumero" runat="server" AutoPostBack="True" 
            ontextchanged="txtArticuloNumero_TextChanged"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Categoria: "></asp:Label>
        <asp:DropDownList ID="cmbCategoria" runat="server" DataSourceID="DSCategorias" 
            DataTextField="CategoriaNombre" DataValueField="CategoriaId" Height="22px" 
            Width="171px">
        </asp:DropDownList>
        <asp:SqlDataSource ID="DSCategorias" runat="server" 
            ConnectionString="<%$ ConnectionStrings:DBLocalConnectionString %>" 
            DeleteCommand="DELETE FROM [Categorias] WHERE [CategoriaId] = @CategoriaId" 
            InsertCommand="INSERT INTO [Categorias] ([CategoriaId], [CategoriaNombre]) VALUES (@CategoriaId, @CategoriaNombre)" 
            SelectCommand="SELECT [CategoriaId], [CategoriaNombre] FROM [Categorias]" 
            UpdateCommand="UPDATE [Categorias] SET [CategoriaNombre] = @CategoriaNombre WHERE [CategoriaId] = @CategoriaId">
            <DeleteParameters>
                <asp:Parameter Name="CategoriaId" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="CategoriaId" Type="Int32" />
                <asp:Parameter Name="CategoriaNombre" Type="String" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="CategoriaNombre" Type="String" />
                <asp:Parameter Name="CategoriaId" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <br />
        <br />
        <asp:Label ID="Label3" runat="server" Text="Inciso Letra: "></asp:Label>
        <asp:TextBox ID="txtIncisoLetra" runat="server"></asp:TextBox>
        <br />
    
    </div>
    <asp:Label ID="Label4" runat="server" Text="Descripcion del Inciso: "></asp:Label>
    <br />
    <asp:TextBox ID="txtIncisoDescripcion" runat="server" Height="63px" 
        TextMode="MultiLine" Width="607px"></asp:TextBox>
    <br />
    <br />
    <asp:Label ID="Label5" runat="server" Text="Contenido del Inciso: "></asp:Label>
    <br />
    <asp:TextBox ID="txtIncisoContenido" runat="server" Height="157px" 
        TextMode="MultiLine" Width="599px"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="btnGuardar" runat="server" OnClientClick="return validar();"  onclick="btnGuardar_Click" 
        Text="Guardar" />
&nbsp;
    <asp:Button ID="btnNuevo" runat="server" onclick="btnNuevo_Click" 
        Text="Nuevo" />
    </form>
</body>
</html>
