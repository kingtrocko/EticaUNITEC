<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MantenimientoAlumno.aspx.cs" Inherits="EticaUNITEC.NnuevoAlumno" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript">
    function Validacion() {
        var nom = document.getElementById("TextNombre").value;
        var cue = document.getElementById("TextCuenta").value;

        if (nom == "") {
            alert("Ingrese el Nombre!!!");
            return false;
        }
        else if (cue == "") {
            alert("Ingrese el Numero de Cuenta!!!");
            return false;
        }else
            return true;
    }
</script>
    <script type="text/javascript">
        if (navigator.userAgent.toLowerCase().indexOf('chrome') != -1) {
            document.write('<link href="../Css/forchrome.css" rel="stylesheet" type="text/css" />');
        }
    </script>
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
                        <h2 class="redtext">Registro de Alumno</h2>
                            <div id="fault-form-element" class="form-element multi-field id">
                                <fieldset>
                                    <label class="column1" style="width:20%;">
                                            <strong>Id</strong>
                                            <asp:textbox ID="TextId" runat="server" ReadOnly="True"></asp:textbox>
                                    </label>
                                </fieldset>
                            </div>
                            <div id="Div1" class="form-element multi-field id">
                                <fieldset>
                                    <label class="column1" style="width:50%;">
                                            <strong>Nombre del alumno</strong>
                                            <asp:textbox ID="TextNombre" runat="server"></asp:textbox>
                                    </label>
                                </fieldset>
                            </div>
                            <div id="Div2" class="form-element multi-field id">
                                <fieldset>
                                    <label class="column1" style="width:30%;">
                                            <strong>N. Cuenta</strong>
                                            <asp:textbox ID="TextCuenta" runat="server"></asp:textbox>
                                    </label>
                                </fieldset>
                            </div>
                            <div id="Div3" class="form-element multi-field id">
                                <fieldset>
                                    <label class="column1" style="width:30%;">
                                            <strong>Genero</strong>
                                            <div class="goog-inline-block goog-flat-menu-button jfk-select styled-select">
                                                <asp:DropDownList ID="ComboGenero" runat="server">
                                                    <asp:ListItem>M</asp:ListItem>
                                                    <asp:ListItem>F</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                    </label>
                                </fieldset>
                            </div>
                            <div id="Div4" class="form-element multi-field id">
                                <fieldset>
                                    <label class="column1" style="width:40%;">
                                            <strong>Carrera</strong>
                                            <div class="goog-inline-block goog-flat-menu-button jfk-select styled-select">
                                                <asp:DropDownList ID="ComboCarrera" runat="server" DataSourceID="Data" 
                                                    DataTextField="CarreraNombre" DataValueField="CarreraId" >
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="Data" runat="server" 
                                                    ConnectionString="<%$ ConnectionStrings:DBLocalConnectionString %>" 
                                                    SelectCommand="SELECT [CarreraId], [CarreraNombre] FROM [Carreras]">
                                                </asp:SqlDataSource>
                                            </div>
                                    </label>
                                </fieldset>
                                </div>
                                <asp:Button ID="BtnGuardar" runat="server" Text="Guardar" class="g-button" onclick="BtnGuardar_Click" OnClientClick = "return Validacion();"/>
                                <asp:Button ID="BtnCancelar" runat="server" Text="Cancelar" class="g-button" onclick="BtnCancelar_Click" />
                        </div>
                    </div>
                </div>
            </div>
    </form>
</body>
</html>