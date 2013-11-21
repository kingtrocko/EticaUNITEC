<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="EticaUNITEC.Home.Home" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Expires" content="-1" />
    <meta http-equiv="CACHE-CONTROL" content="NO-CACHE" />

    <link rel="icon" href="~/Resources/unitec.ico" type="image/x-icon" />
    <link href="../Css/MainStyle.css" rel="stylesheet" type="text/css" />
    <link href="../Css/MainStyle2.css" rel="stylesheet" type="text/css" />
    <title>Unitec SPS | Sistema de Faltas</title>   
   
    <script type="text/javascript">
        if (navigator.userAgent.toLowerCase().indexOf('chrome') != -1) {
            document.write('<link href="../Css/homechrome.css" rel="stylesheet" type="text/css" />');
        }
    </script>
    
    <script src="../Resources/jquery.min.js" type="text/javascript"></script>

    <script type='text/javascript'>

        $(function () {
            var iFrames = $('iframe');
            function iResize() {
                for (var i = 0, j = iFrames.length; i < j; i++) {
                    iFrames[i].style.height = '470px';
                    if (iFrames[i].contentWindow.document.body.offsetHeight > 1000) {
                        iFrames[i].style.height = iFrames[i].contentWindow.document.body.offsetHeight + 'px';
                    } else if (iFrames[i].contentWindow.document.body.offsetHeight <= 470) {
                        iFrames[i].style.height = 470 + 'px';
                    } else {
                        iFrames[i].style.height = iFrames[i].contentWindow.document.body.offsetHeight + 20 + 'px';
                    }
                }
            }

            if ($.browser.safari || $.browser.opera) {

                iFrames.load(function () {
                    setTimeout(iResize, 0);
                });

                for (var i = 0, j = iFrames.length; i < j; i++) {
                    var iSource = iFrames[i].src;
                    iFrames[i].src = '';
                    iFrames[i].src = iSource;
                }

            } else {
                iFrames.load(function () {
                    this.style.height = '470px';
                    if (this.contentWindow.document.body.offsetHeight > 1000) {
                        this.style.height = this.contentWindow.document.body.offsetHeight + 'px';
                    } else if (this.contentWindow.document.body.offsetHeight <= 470) {
                        this.style.height = 470 + 'px';
                    } else
                    {
                        this.style.height = this.contentWindow.document.body.offsetHeight + 20 + 'px';
                    }
                });
            }

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="head-bar">
            <div class="wrapper">
                <asp:Image ID="Logo_Header" runat="server" ImageUrl="~/Resources/logo_header.png" />
                    <div class="white">
                        <ul id="menu">  
                        <li><a href="Home.aspx"><strong>Inicio</strong><span>Sistema de ética</span></a>  
                        </li>
                        <li class="Decoration sbbr bgfilled"><a href="#" class="drop"><strong>Actas</strong><span>Documentación</span></a><!-- Begin 5 columns Item -->  
                            <div class="dropdown">
                                    <ul class="sub">  
                                        <li><a href="../Actas/ListaTemplateActas.aspx" target="placeholder_frame">Actas</a></li>  
                                    </ul>   
                            </div>
                        </li > 
                        <li class="Decoration sbbr bgfilled"><a href="#" class="drop"><strong>Faltas</strong><span>Sanciones</span></a><!-- Begin 4 columns Item -->  
                            <div class="dropdown">
                                    <ul class="sub">  
                                        <li><a href="../Faltas/ListaFaltas.aspx" target="placeholder_frame">Lista de Faltas</a></li>  
                                        <li><a href="../Faltas/MantenimientoFaltas.aspx" target="placeholder_frame">Crear Faltas</a></li>  
                                    </ul>   
                            </div>
                        </li> 
  
                        <li class="Decoration sbbr bgfilled"><a href="#" class="drop"><strong>Seguridad</strong><span>Accesos</span></a>  
                              <div class="dropdown"> 
                                    <ul class="sub">  
                                        <li><a href="../Seguridad/Usuarios.aspx" target="placeholder_frame">Usuarios</a></li>  
                                        <li><a href="../Seguridad/MantenimientoRoles.aspx" target="placeholder_frame" >Roles</a></li>  
                                        <li><a href="../Seguridad/PrivilegioInicio.aspx" target="placeholder_frame" >Privilégios</a></li> 
                                    </ul>   
                            </div>
                        </li>
                        <li class="Decoration sbbr bgfilled"><a href="#" class="drop"><strong>Administración</strong><span>Artículos y Reportes</span></a><!-- Begin 3 columns Item -->  
                              <div class="dropdown">
                                    <ul class="sub">  
                                        <li><a href="../Mantenimiento/Articulo.aspx" target="placeholder_frame">Articulos del Reglamento</a></li>  
                                        <li><a href="../Mantenimiento/Alumnos.aspx" target="placeholder_frame">Alumnos</a></li>  
                                        <li><a href="../Mantenimiento/Carreras.aspx" target="placeholder_frame">Carreras</a></li>  
                                        <li><a href="#">Reportes</a></li>  
                                    </ul>  
                            </div>
                        </li>
                        <li class="Decoration sbbr menu_right bgfilled"><a href="#"><strong ID="userSpot" runat="server">Iniciar sesión</strong><span>Limitado</span></a>
                        <div class="dropdown align_right">
                                    <ul class="sub">  
                                        <li><a href="#">Configuración</a></li>  
                                        <li><a href="../Seguridad/Logout.aspx" target="placeholder_frame">Cerrar sesión</a></li>  
                                    </ul>  
                            </div>
                        </li>
                    </ul>  
                </div>
            </div>
        </div>
        <div class="myDiv">
            <iframe id="placeholder_frame" width="900" height="470" class="iframe" name="placeholder_frame" src="../Seguridad/Usuarios.aspx">

            </iframe>
        </div>
    </form>
</body>
</html>
