<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NuevoRegistro.aspx.cs" Inherits="Etica_Unitec.NuevoRegistro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <link href="../Css/forms/signup.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/style1.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/style2.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/style3.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
    <body>
        <form id="form1" runat="server">
        <div class="wrapper">
        <div class="signuponepage main content clearfix">
            <div class="signup-steps step-1 clearfix">
                <h1 class="redtext">Registro de Privilegios</h1>
            </div>
            <div class="clearfix" >
                     <div class="side-content" style="width:350px; height:380px;">
                     <div class="signup-box">
                            <div id="id-form-element" class="form-element multi-field id">
                                <fieldset>
                                    <legend>
                                        <strong>Nombre del privilegio</strong>
                                    </legend>
                                    <label class="id">
                                        <asp:TextBox ID="txtLlave" runat="server"></asp:TextBox>
                                    </label>
                                </fieldset>
                            </div>
                            <div id="correo-form-element" class="form-element general-entry">
                                <label>
                                    <strong> Descripción del Privilegio </strong>
                                    <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" style="width: 100%; height: 155px; max-width:100%; max-height:155px;"></asp:TextBox>
                                </label>
                            </div>
                              <asp:Button ID="btnGuardar" runat="server" onclick="btnGuardar_Click" Text="Guardar" class="g-button" />
                              <asp:Button ID="btnCancelar" runat="server" onclick="btnCancelar_Click" Text="Cancelar" class="g-button" />
                        </div>

                    </div>
                </div>
            </div>
        </div>    
        </form>
    </body>
</html>
