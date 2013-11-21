<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MantenimientoRoles.aspx.cs" Inherits="EticaUNITEC.MantenimientoRoles1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Css/forms/signup.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/style1.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/style2.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/style3.css" rel="stylesheet" type="text/css" />

    <title></title>
    <script type="text/javascript">
        function confirmAgregarPrivilegios() {
            var opcion = confirm('Desea asignarle los Roles al usuario?');
            if (!opcion)
                document.location.href = "~/Seguridad/Usuarios.aspx";
            else
                return opcion;
        }



        function validarEnBlanco() {
            var id = document.getElementById("txtRolId").value;
            var nom = document.getElementById("txtRolNombre").value;
            var rol = document.getElementById("txtRolDescripcion").value; 
            if (id == "" || nom == "" || rol=="") {
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
    <div class="wrapper">
        <div class="signuponepage main content clearfix">
            <div class="signup-steps step-1 clearfix">
                <h1 class="redtext">Registro de Roles</h1>
            </div>
            <div class="clearfix" >
                <div class="sign-up" style="width:360px; height: 201px;">
                    <div id="Div1" class="form-element multi-field general">
                        <fieldset>
                            <label class="column1">
                            <strong>Privilegios Disponibles</strong>
                            </label>
                            <label class="column2">
                                <strong>Privilegios Asignados</strong>
                            </label>
                        </fieldset>
                    </div>
                    <asp:Panel ID="Panel1" runat="server" GroupingText="" Height="175px" 
                        Width="360px">
                        <table class="style1">
                            <tr>
                                <td class="style2">
                                    <asp:ListBox ID="listPrivilegiosDisponibles" runat="server" Height="160px" Width="155px">
                                    </asp:ListBox>
                                </td>
                                <td class="style4">
                                    <table class="style3">
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnEnviarAAsignados" class="g-button" Width="30px" runat="server" Text="  &gt;  " onclick="btnEnviarAAsignados_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnEnviarAAsignadosTodos" class="g-button" Width="30px" runat="server" Text=" &gt;&gt; " onclick="btnEnviarAAsignadosTodos_Click"  />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnEnviarADisponibles" class="g-button" Width="30px" runat="server" Text="  &lt;  " onclick="btnEnviarADisponibles_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnEnviarADisponiblesTodos" class="g-button" Width="30px" runat="server" Text=" &lt;&lt; " onclick="btnEnviarADisponiblesTodos_Click"  />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <asp:ListBox ID="listPrivilegiosAsignados" runat="server" Height="160px" Width="155px">
                                    </asp:ListBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                 </div>
                     <div class="side-content" style="width:350px; height:380px;">
                     <div class="signup-box">
                            <div id="id-form-element" class="form-element multi-field id">
                                <fieldset>
                                    <legend>
                                        <strong>Id del rol</strong>
                                    </legend>
                                    <label class="id">
                                        <asp:TextBox ID="txtRolId" runat="server" ReadOnly="True"></asp:TextBox>
                                    </label>
                                </fieldset>
                            </div>
                            <div id="nombre-form-element" class="form-element general-entry">
                                <label>
                                    <strong> Nombre del rol </strong>
                                    <asp:TextBox ID="txtRolNombre" runat="server"></asp:TextBox>
                                </label>
                            </div>
                            <div id="correo-form-element" class="form-element general-entry">
                                <label>
                                    <strong> Descripción del rol </strong>
                                    <asp:TextBox ID="txtRolDescripcion" runat="server" class="TextArea" TextMode="MultiLine" style="width: 100%; height: 155px; max-width:100%; max-height:155px;"></asp:TextBox>
                                </label>
                            </div>
                                <asp:Button ID="btnGuardar" class="g-button" runat="server" Text="Guardar"  OnClientClick = "return validarEnBlanco();" onclick="btnGuardar_Click" />
                                <asp:Button ID="btnCancelar" class="g-button" runat="server" Text="Cancelar" onclick="btnCancelar_Click" />
                        </div>

                    </div>
                </div>
            </div>
        </div>    
    </form>
</body>
</html>
