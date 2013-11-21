<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inciso.aspx.cs" Inherits="EticaUNITEC.Mantenimiento.Inciso" %>

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
            <div id="reg-falta" class="signuponepage main content clearfix">
                <div class="clearfix" >
                    <div class="side-content" style="margin:5px;">
                        <div class="signup-box" style="padding:15px;">
                        <h2 class="redtext">Listado de Incisos</h2>
                            <div id="fault-form-element" class="form-element multi-field id">
                                <fieldset>
                                    <label class="column1" style="width:30%;">
                                            <strong>Numero del Articulo</strong>
                                            <asp:TextBox ID="txtArt" runat="server"></asp:TextBox>
                                    </label>
                                </fieldset>
                            </div>
                            <div id="container" style="margin-left:0px;">
                                <asp:GridView ID="gridInc" runat="server" AutoGenerateColumns="False" 
                                    onrowdeleting="gridInc_RowDeleting" onrowediting="gridInc_RowEditing" 
                                    GridLines="None"
                                    CssClass="mGrid"
                                    PagerStyle-CssClass="pgr"
                                    AlternatingRowStyle-CssClass="alt">
<AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                    <Columns>
                                        <asp:BoundField DataField="ArticuloId" />
                                        <asp:BoundField DataField="IncisoId" />
                                        <asp:BoundField DataField="IncisoLetra" HeaderText="Letra" />
                                        <asp:BoundField DataField="IncisoContenido" HeaderText="Contenido" />
                                        <asp:BoundField DataField="IncisoDescripcion" HeaderText="Descripcion" />
                                        <asp:TemplateField HeaderText="Editar" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                                                    CommandName="Edit" Text="Editar"></asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="True" 
                                                    CommandName="Update" Text="Update"></asp:LinkButton>
                                                &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                                                    CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Borrar" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="False" 
                                                    CommandName="Delete" Text="Borrar" OnClientClick="return confirm('¿Seguro que desea elmimar?')"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>

<PagerStyle CssClass="pgr"></PagerStyle>
                                </asp:GridView>
                            </div>  
                            <asp:Button ID="btnNuevo" runat="server" Text="Agregar inciso" class="g-button" onclick="btnNuevo_Click" />
                            <asp:Button ID="btnReg" runat="server" Text="Volver atras" class="g-button" onclick="btnReg_Click" />
                            
                        </div>
                    </div>
                </div>
            </div>
    </form>
</body>
</html>
