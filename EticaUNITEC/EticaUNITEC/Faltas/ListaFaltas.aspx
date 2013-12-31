<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListaFaltas.aspx.cs" Inherits="EticaUNITEC.Faltas.ListaFaltas" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../Css/grids/style.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/style1.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/style2.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/style3.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/signup.css" rel="stylesheet" type="text/css" />
    <title></title>
    
    
     <style type="text/css">
         .cpHeader
        {
            color: #DD4B39;
            cursor: pointer;
           
    font-size: 1.54em;
    font-weight: normal;
    line-height: 24px;
    margin: 0 0 0.46em;
    margin: 0px 0px 20px 0px;
            
        }
        .cpBody
        {
            
        }      
    </style>
    
    
    
    
    
    <script type="text/javascript">
        if (navigator.userAgent.toLowerCase().indexOf('chrome') != -1) {
            document.write('<link href="../Css/forchrome.css" rel="stylesheet" type="text/css" />');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="reg-falta" class="signuponepage main content clearfix">
                <div class="clearfix">
                   <div class="side-content" style="margin:5px; height: 710px; width: 803px;">
                
                    
         <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Panel ID="pHeader" runat="server" CssClass="cpHeader" Width="639px">
                <asp:Image ID="Image1" runat="server" Height="16px" Width="16px" />
                &nbsp;<asp:Label ID="lblText" runat="server" />
            </asp:Panel>
 
            <asp:Panel ID="pBody" runat="server" CssClass="cpBody" Height="16px" 
                Width="782px">
                    <br />
                    <br />
                  
                    <h2 class="redtext">Consulta de Falta</h2>
                        <div id="acc-carr-form-element" class="form-element multi-field general">
                            <fieldset>
                                <label class="column1" style="width:20%;">
                                    <strong>Num. Cuenta</strong>
                                    <asp:TextBox ID="txtCuenta" runat="server"></asp:TextBox>
                                </label>
                                <label class="column2" style="width:30%;">
                                    <strong>Carrera</strong>
                                    <div class="goog-inline-block goog-flat-menu-button jfk-select styled-select">
                                        <asp:DropDownList ID="cmbCarrera" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </label>
                            </fieldset>
                        </div>
                        <div id="name-form-element" class="form-element multi-field general">
                            <fieldset>
                                <label class="column1"style="width:79%;">
                                    <strong>Nombre</strong>
                                    <asp:TextBox ID="txtAlumnoNombre" runat="server" ></asp:TextBox>
                                </label>
                            </fieldset>
                         </div>
                        <div id="title-cat" class="form-element multi-field general">
                            <fieldset>
                                <label class="column1" style="width:40%;">
                                    <strong>Titulo de la falta</strong>
                                    <asp:TextBox ID="txtFaltaTitulo" runat="server"></asp:TextBox>
                                </label>
                                <label class="column2" style="width:30%;">
                                    <strong>Categoria</strong>
                                    <div class="goog-inline-block goog-flat-menu-button jfk-select styled-select">
                                        <asp:DropDownList ID="cmbCategoria" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </label>
                            </fieldset>
                        </div>
                        <div id="Date-form-element" class="form-element multi-field general">
                            <fieldset>
                                <label class="column1" style="width:20%;">
                                    <strong>Fecha</strong>
                                    <asp:TextBox ID="txtFechaIncio" runat="server"></asp:TextBox>
                                </label>
                                <label class="column2" style="width:20%;">
                                    <strong></strong>
                                    <asp:TextBox ID="txtFechaFin" runat="server"></asp:TextBox>
                                </label>
                            </fieldset>
                        </div>
                        <asp:Button ID="btnFiltrar" class="g-button" runat="server" onclick="btnFiltrar_Click" Text="Buscar" />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
            </asp:Panel>
 
    <ajaxtoolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" TargetControlID="pBody" CollapseControlID="pHeader" ExpandControlID="pHeader"
    Collapsed="true" TextLabelID="lblText" CollapsedText="Opciones de Búsqueda" ExpandedText="Opciones de Búsqueda"
    CollapsedSize="0"
     
    ImageControlID="Image1"
    
    
    
    
    
    
    
    
    
    
    
     CollapsedImage="~/Resources/Up.png"
     ExpandedImage="~/Resources/Down.png">
      </ajaxtoolkit:CollapsiblePanelExtender>
           
        </ContentTemplate>
        </asp:UpdatePanel>
                    
                    
                    
                    
           <br />
           <br /> 
            
            <div id="container">
                <asp:GridView ID="grid" runat="server" onrowdeleting="grid_RowDeleting" 
                    onrowediting="grid_RowEditing" CssClass="mGrid" GridLines="None" 
                    AutoGenerateColumns="False">
                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                    <Columns>            
                        <asp:BoundField DataField="FaltaId" HeaderText="FaltaId" />
                        <asp:BoundField DataField="AlumnoCuenta" HeaderText="N. Cuenta" />
                        <asp:BoundField DataField="AlumnoNombre" HeaderText="Nombre del Alumno" />
                        <asp:BoundField DataField="CarreraNombre" HeaderText="Carrera" />
                        <asp:BoundField DataField="CategoriaNombre" HeaderText="Categoria" />
                        <asp:BoundField DataField="FaltaTitulo" HeaderText="Titulo de la falta" />
                        <asp:HyperLinkField HeaderImageUrl="~/Faltas/Imprimir.aspx" Text="Imprimir" />
                        <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink2" runat="server" 
                                NavigateUrl='<%# "MantenimientoFaltas.aspx?id=" + Eval("FaltaId")  %>' 
                                Text="Editar">Editar</asp:HyperLink>

                            <%--<asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                                CommandName="Edit" Text="Editar"></asp:LinkButton>--%>
                            
                        </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" 
                                    CommandName="Update" Text="Update"></asp:LinkButton>
                                &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                                    CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                            </EditItemTemplate>
                        </asp:TemplateField>
                       
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                                OnClientClick="return confirm('Esta seguro que quiere borrar esta informacion?');"
                                    CommandName="Delete" Text="Borrar"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                    </Columns>
                    <PagerStyle CssClass="pgr"></PagerStyle>
                 </asp:GridView>
            </div>
                    </div>
                </div>
            </div>
            
    </form>
</body>
</html>
