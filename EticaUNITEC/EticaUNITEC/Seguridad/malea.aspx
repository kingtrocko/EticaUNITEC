<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="malea.aspx.cs" Inherits="FKMANO.malea" %>

<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Css/grids/style.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/style1.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/style2.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/style3.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/signup.css" rel="stylesheet" type="text/css" />
    
    <title></title>


     <script type="text/javascript">

         function validarEnBlanco() {
             var cod = document.getElementById("TextCodigo").value;
             var nom = document.getElementById("TextNombre").value;

             if (cod == "" || nom == "") {
                 alert("Hay datos en blanco");
                 return false;
             }
             else
                 return true;
         }
        
   
    </script>
</head>
<body >
    <form id="form1" runat="server">
            <div id="reg-falta" class="signuponepage main content clearfix">
                <div class="clearfix" >
                    <div class="side-content" style="margin:5px;">
                        <div class="signup-box" style="padding:15px;">
                        <h2 class="redtext">Registro de carrera</h2>
                            <div id="fault-form-element" class="form-element multi-field id">
                                <fieldset>
                                    <label class="column1" style="width:20%;">
                                            <strong>Id</strong>
                                            <asp:TextBox ID="TextId" runat="server" ReadOnly="True"></asp:TextBox>
                                    </label>
                                </fieldset>
                            </div>
                            <div id="Div1" class="form-element multi-field id">
                                <fieldset>
                                    <label class="column1" style="width:30%;">
                                            <strong>Codigo de la carrera</strong>
                                            <asp:TextBox ID="TextCodigo" runat="server"></asp:TextBox>
                                    </label>
                                </fieldset>
                            </div>
                            <div id="Div2" class="form-element multi-field id">
                                <fieldset>
                                    <label class="column1" style="width:50%;">
                                            <strong>Nombre de la carrera</strong>
                                            <asp:textbox ID="TextNombre" runat="server"></asp:textbox>
                                    </label>
                                </fieldset>
                            </div>
                            <asp:Button ID="BtnGuardar" runat="server" Text="Guardar" class="g-button" onclick="BtnGuardar_Click"  OnClientClick="return validarEnBlanco();"/>
                            <asp:Button ID="BtnCancelar" runat="server" Text="Cancelar" class="g-button" onclick="BtnCancelar_Click" />
                        </div>
                    </div>
                </div>
            </div>
    </form>
</body>
</html>
