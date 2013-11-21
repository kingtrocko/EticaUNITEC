<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrivilegioInicio.aspx.cs" Inherits="Etica_Unitec.PrivilegioInicio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Css/grids/style.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/style1.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/style2.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/style3.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/signup.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="wrapper">
            <div class="signuponepage main content clearfix">
                <div class="signup-steps step-1 clearfix">
                    <h1 class="redtext">Privilegios Actuales</h1>
                </div>
            </div>
        </div>
        <div id="container">
            <asp:GridView ID="gridPrivilegio" 
                          runat="server" 
                          
                         GridLines="None"
                         AllowPaging="True"
                         CssClass="mGrid"
                         PagerStyle-CssClass="pgr"
                         AlternatingRowStyle-CssClass="alt"

                          AutoGenerateColumns="False" 
                          onrowdeleting="gridPrivilegio_RowDeleting" 
                          DataKeyNames="PrivilegioId" 
                          onrowcommand="gridPrivilegio_RowCommand" 
                          onrowcreated="gridPrivilegio_RowCreated" 
                onpageindexchanged="gridPrivilegio_PageIndexChanged" 
                onpageindexchanging="gridPrivilegio_PageIndexChanging">
<AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                <Columns>
                    <asp:BoundField DataField="PrivilegioId" HeaderText="ID" />
                    <asp:BoundField DataField="PrivilegioLlave" HeaderText="LLAVE" />
                    <asp:BoundField DataField="PrivilegioDescripcion" HeaderText="DESCRIPCION" />
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" 
                                CommandName="Update" Text="Update"></asp:LinkButton>
                            &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                                CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "~/Seguridad/ModificarRegistro.aspx?userID=" + Eval("PrivilegioId")  %>'>Editar</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" 
                                OnClientClick="return confirm('¿Estas Seguro que Deseas Borrar?')" 
                                runat="server" CausesValidation="False" 
                                CommandName="Delete" Text="Borrar" ></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
        
<PagerStyle CssClass="pgr"></PagerStyle>
        
            </asp:GridView>
  
            <asp:SqlDataSource ID="BDSql" runat="server" 
                ConnectionString="<%$ ConnectionStrings:DBLocalConnectionString %>" 
                SelectCommand="SELECT [PrivilegioId], [PrivilegioLlave], [PrivilegioDescripcion] FROM [Privilegios]">
            </asp:SqlDataSource>
        <br />
                <asp:Button ID="btnAdd" runat="server" onclick="btnAdd_Click" Text="Agregar Nuevo" class="g-button" />
        <br />
    
    </div>
    </form>
</body>
</html>
