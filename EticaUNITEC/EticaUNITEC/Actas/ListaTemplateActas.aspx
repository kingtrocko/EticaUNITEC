<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListaTemplateActas.aspx.cs" Inherits="EticaUNITEC.Seguridad.ListaTemplateActas" %>

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
                    <h1 class="redtext">Lista de Plantillas</h1>
                </div>
        </div>
    </div>
    <div id="container">
        <asp:GridView ID="grid" 
            runat="server" 
            AutoGenerateColumns="False" 
            onrowdeleting="grid_RowDeleting"
            GridLines="None"
            CssClass="mGrid"
            PagerStyle-CssClass="pgr"
            AlternatingRowStyle-CssClass="alt">

<AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>

            <Columns>
                <asp:BoundField DataField="TemplateId" HeaderText="TemplateId" />
                <asp:BoundField DataField="TemplateNombre" HeaderText="Nombre" />
                <asp:BoundField DataField="TemplateHTML" HeaderText="TemplateHTML" />
                <asp:BoundField DataField="TemplateDescripcion" HeaderText="Descripcion" />
                <asp:BoundField DataField="CategoriaId" HeaderText="CategoriaId" />
                <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" 
                                CommandName="Update" Text="Update"></asp:LinkButton>
                            &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                                CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink2" runat="server" 
                                NavigateUrl='<%# "TemplateActas.aspx?id=" + Eval("TemplateId")  %>' 
                                Text="Editar">Editar</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
            

                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" 
                        OnClientClick="return confirm('Esta seguro que quiere borrar este Template?');"
                            CommandName="Delete" Text="Borrar"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

<PagerStyle CssClass="pgr"></PagerStyle>
        </asp:GridView>


        <asp:Button ID="btnNuevo" class="g-button" runat="server" onclick="btnNuevo_Click" 
            Text="Agregar Nuevo" />
        <br />
    
    </div>
    </form>
</body>
</html>
