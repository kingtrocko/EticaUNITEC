<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MantenimientoArticulo.aspx.cs" Inherits="EticaUNITEC.Mantenimiento.MantenimientoArticulo" %>

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
            var num = document.getElementById("txtNum").value;
           
            if (num == "" ) {
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
                        <h2 class="redtext">Registro de Articulos</h2>
                            <div id="fault-form-element" class="form-element multi-field id" style="margin-top:70px">
                                <fieldset>
                                    <label class="column1" style="width:30%;">
                                            <strong>Numero del Articulo</strong>
                                            <asp:TextBox ID="txtNum" runat="server" AutoPostBack="True" ontextchanged="txtNum_TextChanged"></asp:TextBox>
                                    </label>
                                </fieldset>
                            </div>
                            <div id="Div1" class="form-element multi-field id">
                                <fieldset>
                                    <label class="column1" style="width:30%;">
                                            <strong>Categoria</strong>
                                            <div class="goog-inline-block goog-flat-menu-button jfk-select styled-select">
                                                <asp:DropDownList ID="cbCateg" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                    </label>
                                </fieldset>
                            </div>
                            <br />
                            <asp:Button ID="btnGuardar" runat="server" onclick="btnGuardar_Click" Text="Guardar"  OnClientClick="return validarEnBlanco();"  class="g-button" />
                            <asp:Button ID="btnReg" runat="server" onclick="btnReg_Click" Text="Volver atras" class="g-button"/>
                            
                        </div>
                    </div>
                </div>
            </div>
    </form>
</body>
</html>
