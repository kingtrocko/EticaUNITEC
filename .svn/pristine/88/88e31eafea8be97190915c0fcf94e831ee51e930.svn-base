<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateUser.aspx.cs" Inherits="EticaUNITEC.UpdateUser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <link href="../Css/forms/signup.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/style1.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/style2.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/style3.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript">
        function confirmAgregarRoles() {
            var opcion = confirm('Desea asignarle los Roles al usuario?');
            if (!opcion)
                document.location.href = "~/Seguridad/Usuarios.aspx";
            else
                return opcion;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="wrapper">
        <div class="signuponepage main content clearfix">
            <div class="signup-steps step-1 clearfix">
                <h1 class="redtext">Registro de Usuario</h1>
            </div>
            <div class="clearfix" >
                <div class="sign-up">
                    <div id="Div1" class="form-element multi-field general">
                        <fieldset>
                            <label class="column1">
                            <strong>Roles Disponibles</strong>
                            </label>
                            <label class="column2">
                                <strong>Roles Asignados</strong>
                            </label>
                        </fieldset>
                    </div>
                    <asp:Panel ID="Panel1" runat="server" GroupingText="" Height="175px" 
                        Width="360px">
                        <table class="style1">
                            <tr>
                                <td class="style2">
                                    <asp:ListBox ID="listRolesDisponibles" runat="server" Height="160px" Width="155px">
                                    </asp:ListBox>
                                </td>
                                <td class="style4">
                                    <table class="style3">
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnEnviarAAsignados" class="g-button" runat="server" Text="  &gt;  " onclick="btnEnviarAAsignados_Click" Width="30px"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnEnviarAAsignadosTodos" class="g-button" runat="server" Text=" &gt;&gt; " onclick="btnEnviarAAsignadosTodos_Click" Width="30px"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnEnviarADisponibles" class="g-button" runat="server" Text="  &lt;  " onclick="btnEnviarADisponibles_Click" Width="30px"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnEnviarADisponiblesTodos" class="g-button" runat="server" Text=" &lt;&lt; " onclick="btnEnviarADisponiblesTodos_Click"  Width="30px"/>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <asp:ListBox ID="listRolesAsignados" runat="server" Height="160px" Width="155px">
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
                                        <strong>Id de usuario</strong>
                                    </legend>
                                    <label class="id">
                                        <asp:TextBox ID="txt_id" runat="server" ReadOnly="True"></asp:TextBox>
                                    </label>
                                </fieldset>
                            </div>
                            <div id="nombre-form-element" class="form-element general-entry">
                                <label>
                                    <strong> Nombre del usuario </strong>
                                    <asp:TextBox ID="txt_nombre" runat="server"></asp:TextBox>
                                </label>
                            </div>
                            <div id="correo-form-element" class="form-element general-entry">
                                <label>
                                    <strong> Correo del usuario </strong>
                                    <asp:TextBox ID="txt_correo" runat="server"></asp:TextBox>
                                </label>
                            </div>
                            <div id="pass-tel-form-element" class="form-element multi-field general">
                                <fieldset>
                                    <label class="column1">
                                    <strong>Contraseña</strong>
                                        <asp:TextBox ID="txt_pass" runat="server" Enabled="False"></asp:TextBox>
                                    </label>
                                    <label class="column2">
                                        <strong>Teléfono de usuario</strong>
                                        <asp:TextBox ID="txt_tel" runat="server"></asp:TextBox>
                                    </label>
                                </fieldset>
                            </div>
                            <div id="mail-form-element" class="form-element">
                                <label>
                                    <strong> Dirección del usuario </strong>
                                    <asp:TextBox ID="txt_dir" runat="server"></asp:TextBox>
                                </label>
                            </div>
                                <asp:Button class="g-button" ID="br_guardar" runat="server" onclick="br_guardar_Click" Text="Guardar" />
                                <asp:Button class="g-button" ID="bt_cancelar" runat="server" onclick="bt_cancelar_Click" Text="Cancelar" />
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
