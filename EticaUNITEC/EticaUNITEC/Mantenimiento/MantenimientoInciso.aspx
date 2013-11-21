<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MantenimientoInciso.aspx.cs" Inherits="EticaUNITEC.Mantenimiento.MantenimientoInciso" %>

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
            var letra = document.getElementById("txtLetra").value;
            var cont = document.getElementById("txtCont").value;
            var desc = document.getElementById("txtDesc").value;
         

            if (letra == "" || cont == "" || desc == "" ) 
            {
                alert("Hay datos en blanco");
                return false;
            }
            else
                return true;
        }

        </script>
</head>
<body>
    <form id="form1" runat="server">
            <div id="reg-falta" class="signuponepage main content clearfix">
                <div class="clearfix" >
                    <div class="side-content" style="margin:5px;">
                        <div class="signup-box" style="padding:15px;">
                        <h2 class="redtext">Registro de Inciso</h2>
                            <div id="fault-form-element" class="form-element multi-field id">
                                <fieldset>
                                    <label class="column1" style="width:30%;">
                                            <strong>Letra del inciso</strong>
                                            <asp:TextBox ID="txtLetra" runat="server" AutoPostBack="True" ontextchanged="txtLetra_TextChanged"></asp:TextBox>
                                    </label>
                                </fieldset>
                            </div>
                            
                            <div id="correo-form-element" class="form-element general-entry">
                                <label>
                                    <strong> Contenido </strong>
                                    <asp:TextBox ID="txtCont" runat="server" TextMode="MultiLine" class="TextArea" style="width: 100%; height: 155px; max-width:100%; max-height:155px;"></asp:TextBox>
                                </label>
                            </div>
                            
                            <div id="Div1" class="form-element general-entry">
                                <label>
                                    <strong> Descripción </strong>
                                    <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" class="TextArea" style="width: 100%; height: 155px; max-width:100%; max-height:155px;"></asp:TextBox>
                                </label>
                            </div>
                            <br /> 
                                <asp:Button ID="btnGuardar" runat="server"  onclick="btnGuardar_Click" class="g-button"
                                Text="Guardar"  OnClientClick="return validarEnBlanco();" />
                                <asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" Text="Volver atras" class="g-button"/>
                        </div>
                    </div>
                </div>
            </div>
    </form>
</body>
</html>
