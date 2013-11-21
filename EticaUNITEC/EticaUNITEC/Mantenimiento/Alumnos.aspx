<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Alumnos.aspx.cs" Inherits="EticaUNITEC.Alumnos" %>

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
                    <h1 class="redtext">Alumnos Registrados</h1>
                </div>
        </div>
    </div>
    <div id="container">       

         <asp:GridView ID="Grid" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="AlumnoId" DataSourceID="SqlData" 
            onrowediting="Grid_RowEditing" 
            GridLines="None"
            AllowPaging="True"
            CssClass="mGrid"
            PagerStyle-CssClass="pgr"
            AlternatingRowStyle-CssClass="alt">
            <Columns>
                <asp:BoundField DataField="AlumnoId" HeaderText="Id" 
                    InsertVisible="False" ReadOnly="True" SortExpression="AlumnoId" />
                <asp:BoundField DataField="AlumnoNombre" HeaderText="Nombre" 
                    SortExpression="AlumnoNombre" />
                <asp:BoundField DataField="AlumnoCuenta" HeaderText="Cuenta" 
                    SortExpression="AlumnoCuenta" />
                <asp:BoundField DataField="AlumnoGenero" HeaderText="Genero" 
                    SortExpression="AlumnoGenero" />
                <asp:BoundField DataField="CarreraId" HeaderText="CarreraId" 
                    SortExpression="CarreraId" />
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
            DeleteCommand="DELETE FROM [Alumnos] WHERE [AlumnoId] = @original_AlumnoId AND [AlumnoNombre] = @original_AlumnoNombre AND [AlumnoCuenta] = @original_AlumnoCuenta AND [AlumnoGenero] = @original_AlumnoGenero AND [CarreraId] = @original_CarreraId" 
            InsertCommand="INSERT INTO [Alumnos] ([AlumnoNombre], [AlumnoCuenta], [AlumnoGenero], [CarreraId]) VALUES (@AlumnoNombre, @AlumnoCuenta, @AlumnoGenero, @CarreraId)" 
            OldValuesParameterFormatString="original_{0}" 
            SelectCommand="SELECT [AlumnoId], [AlumnoNombre], [AlumnoCuenta], [AlumnoGenero], [CarreraId] FROM [Alumnos]" 
            UpdateCommand="UPDATE [Alumnos] SET [AlumnoNombre] = @AlumnoNombre, [AlumnoCuenta] = @AlumnoCuenta, [AlumnoGenero] = @AlumnoGenero, [CarreraId] = @CarreraId WHERE [AlumnoId] = @original_AlumnoId AND [AlumnoNombre] = @original_AlumnoNombre AND [AlumnoCuenta] = @original_AlumnoCuenta AND [AlumnoGenero] = @original_AlumnoGenero AND [CarreraId] = @original_CarreraId">
            <DeleteParameters>
                <asp:Parameter Name="original_AlumnoId" Type="Int32" />
                <asp:Parameter Name="original_AlumnoNombre" Type="String" />
                <asp:Parameter Name="original_AlumnoCuenta" Type="String" />
                <asp:Parameter Name="original_AlumnoGenero" Type="String" />
                <asp:Parameter Name="original_CarreraId" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="AlumnoNombre" Type="String" />
                <asp:Parameter Name="AlumnoCuenta" Type="String" />
                <asp:Parameter Name="AlumnoGenero" Type="String" />
                <asp:Parameter Name="CarreraId" Type="Int32" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="AlumnoNombre" Type="String" />
                <asp:Parameter Name="AlumnoCuenta" Type="String" />
                <asp:Parameter Name="AlumnoGenero" Type="String" />
                <asp:Parameter Name="CarreraId" Type="Int32" />
                <asp:Parameter Name="original_AlumnoId" Type="Int32" />
                <asp:Parameter Name="original_AlumnoNombre" Type="String" />
                <asp:Parameter Name="original_AlumnoCuenta" Type="String" />
                <asp:Parameter Name="original_AlumnoGenero" Type="String" />
                <asp:Parameter Name="original_CarreraId" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
    
        <br />
        <asp:Button ID="BtnNuevo" runat="server" onclick="BtnNuevo_Click" class="g-button" Text="Agregar Alumno" />
        <br />
    
    </div>
    </form>
</body>
</html>