<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="TemplateActas.aspx.cs" Inherits="EticaUNITEC.Seguridad.Editor" %>

<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Css/forms/signup.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/style1.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/style2.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/style3.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function validarEnBlanco() {
            var titulo = document.getElementById("txtTitulo").value;
            var descripcion = document.getElementById("txtDescripcion").value;
            var contenido = FTB_API.FreeTextBox1.GetHtml();
            if (titulo == "" || descripcion == "" || contenido == "") {
                alert("Hay datos en blanco");
                return false;
            }
            else
                return true;
        }
    </script>
    <script type="text/javascript">
        if (navigator.userAgent.toLowerCase().indexOf('chrome') != -1) {
            document.write('<link href="../Css/forchrome.css" rel="stylesheet" type="text/css" />');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">

                <div id="reg-falta" class="signuponepage main content clearfix">
                <div class="clearfix" >
                    <div class="side-content" style="margin:5px;">
                        <div class="signup-box" style="padding:15px;">
                        <h2 class="redtext">Registro de Plantilla</h2>
                            <div id="fault-form-element" class="form-element multi-field id">
                                <fieldset>
                                    <label class="column1" style="width:30%;">
                                            <strong>Titulo</strong>
                                            <asp:TextBox ID="txtTitulo" runat="server"></asp:TextBox>
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
                            <div id="theme-form-element" class="form-element multi-field id">
                                <fieldset>
                                    <label class="column1" style="width:90%;">
                                            <strong>Descipcion</strong>
                                            <asp:TextBox ID="txtDescripcion" runat="server" Width="529px"></asp:TextBox>
                                    </label>
                                </fieldset>
                            </div><!--Aki termina-->
                            <div>
                                <FTB:FreeTextBox ID="FreeTextBox1" runat="server" ButtonSet="OfficeMac" 
                                    Width="760" BackColor="White">
                                </FTB:FreeTextBox><br />

                                 <asp:Button ID="btnGuardar" runat="server" class="g-button" Text="Guardar" OnClientClick="return validarEnBlanco();" onclick="btnGuardar_Click"  />
                                 <asp:Button ID="btnCancelar" runat="server" class="g-button" Text="Cancelar" onclick="btnCancelar_Click" /> 
                                <style type="text/css">
                                    .FreeTextBox1_OuterTable {
                                        background-color: #FFFFFF;
                                        width: 760px;
                                    }
                                    .FreeTextBox1_EndTab {
                                        background-color: #FFFFFF;
                                        border-width: 0px;
                                    }
                                    .FreeTextBox1_DesignBox {
                                        background-color: #FFFFFF;
                                        border: 1px solid #808080;
                                        border-radius:5px;
                                    }
                                    .FreeTextBox1_StartTabOn {
                                        background-color: #FFFFFF;
                                        border-width: 0;
                                        padding: 0;
                                    }
                                </style>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    </form>
</body>
</html>
