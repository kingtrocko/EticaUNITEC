<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="EticaUNITEC.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="shortcut icon" type="image/x-icon" href="../Resources/unitec.ico"/> 

    <link href="../Css/forms/signup.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/style1.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/style2.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/style3.css" rel="stylesheet" type="text/css" />

    <title>Unitec SPS | Iniciar Sesion</title>
    
    
    <script type="text/javascript">
        
        function EnableContinueDueToTextBoxChanged(txtUserNameControl, txtUserNameControl2) {
            //txtUserNameControl.value = trim(txtUserNameControl.value);
            var continueButton = document.getElementById('bt_login');
            if (trim(txtUserNameControl.value).length > 0 && trim(txtUserNameControl2.value).length > 0) {
                continueButton.disabled = false;
            }
            else {
                continueButton.disabled = true;
            }
        }
        function trim(stringToTrim) {
            return stringToTrim.replace(/^\s+|\s+$/g, "");
        } 



        function validarEnBlanco() {
            var correo = document.getElementById("txt_correo").value;
            var pass = document.getElementById("txt_pass").value;
            
            if (correo == "" ||  pass == ""  ) 
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
    <form id="form1" runat="server" autocomplete="off">
            <div id="signin" class="signuponepage main content clearfix">
                <div class="clearfix">
                    <div class="side-content" style="margin:5px;">
                     <div class="signup-box" style="padding:15px; background:#F1F1F1;">
                     <h2 class="redtext">Iniciar Sesión<asp:Image ID="loginLogo" runat="server" ImageUrl="~/Resources/nLogou.png" /></h2>
                     
                            <div id="correo-form-element" class="form-element general-entry">
                                <label>
                                    <strong> Correo del Usuario:</strong><asp:TextBox ID="txt_correo" runat="server" onkeyup="javascript:EnableContinueDueToTextBoxChanged(this,document.getElementById('txt_pass'));"></asp:TextBox>
                                </label>
                            </div>
                          <div id="password-form-element" class="form-element general-entry">
                                <label>
                                    <strong> Contraseña:</strong>
                                    <asp:TextBox ID="txt_pass" runat="server" Enabled="true" TextMode="Password" onkeyup="javascript:EnableContinueDueToTextBoxChanged(this,document.getElementById('txt_correo'));"></asp:TextBox>
                                </label>
                            </div>
                                <asp:Button class="g-button" ID="bt_login" runat="server" onclick="Button1_Click" Text="Entrar" OnClientClick="return validarEnBlanco();" Enabled="false"/>
                        </div>
                        <div ID="warnMsg" runat="server" class="warningError" style="max-width: 270px; margin-left: auto; margin-right: auto;">La combinación del correo/contraseña es incorrecta o la cuenta no existe</div>
                    </div>
                </div>
            </div>

    </form>
</body>
</html>
