<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MantenimientoFaltas.aspx.cs" Inherits="EticaUNITEC.MantenimientoFaltas" %>

<%@ Register assembly="Ext.Net" namespace="Ext.Net" tagprefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link href="../Css/forms/signup.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/style1.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/style2.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/style3.css" rel="stylesheet" type="text/css" />
    
    <style type="text/css">
        body
        {
            height: auto;
            width: 700px;
            min-height:75%;
        }
    </style>

    <script type="text/javascript">
        if (navigator.userAgent.toLowerCase().indexOf('chrome') != -1) {
            document.write('<link href="../Css/forchrome.css" rel="stylesheet" type="text/css" />');
        }
    C


    <script>
function LoadPage(page,usediv) 
{
         // inicializar la variable request, dependiendo del tipo de explorador
         try
         {
           xmlhttp = window.XMLHttpRequest?new XMLHttpRequest(): new ActiveXObject("Microsoft.XMLHTTP");
         }
         catch (e)
         {
           alert("Error: no se pudo cargar la pagina.");
         }
        //Mostrar que la página se está cargando      
        document.getElementById(usediv).innerHTML = 'Cargando…';
       //scroll hacia arriba
       scroll(0,0);
       /*esta es la parte mas importante, cuando se hace la petición onreadystatchange cambiará    dependiendo de estado de la página. Cada vez que pasa, se llama a la función y comprueba que se haya cargado sin errores, si esto es verdad se almacena la página en xmlhttp.responseText, y se pinta en el div*/
       xmlhttp.onreadystatechange = function()
       {               
           //Comprobar que la página se ha terminado de cargar sin problemas.
           if ((xmlhttp.readyState == 4) && (xmlhttp.status == 200))
           {                   
              //escribir información enviada a la página     
              document.getElementById(usediv).innerHTML = xmlhttp.responseText;
           }     
       }

       //manda el request a la página
       xmlhttp.open("GET", page);      
       //aunque no hace faltamandar nada algunos navegadores esperan que les llegue algo
       xmlhttp.send(null);
       return false;
}
</script>

    <script type="text/javascript">

        function validarEnBlanco() {
            var cuenta = document.getElementById("txtCuenta").value;
            var nombre = document.getElementById("txtNombre").value;
            var carrera = document.getElementById("cmbCarrera").value;
            var genero = document.getElementById("radioBtnGenero").value;
            var categoria = document.getElementById("cmbCategoria").value;
            var tipo = document.getElementById("txtIncisoDescripcion").value;
            var descFalta = document.getElementById("txtDecripcionFalta").value;
            var tipoSancion = document.getElementById("cmbTipoSancion").value;

            if (cuenta == "" || nombre == "" || carrera == "" || genero == "" || categoria == "" ||
                tipo == "" || descFalta == "" || tipoSancion == "") 
            {
                alert("Hay datos en blanco");
                return false;
            }
            else
                return true;
        }

        function agregarArchivo(panelGrid) {
            try {
                var tabla = Ext.get(panelGrid);
                tabla = tabla.first();

                var fila = tabla.first();
                fila = fila.next();
                var items = 0;
                while (fila != null && fila.first().first().dom.tagName.toUpperCase() == 'A') {
                    items++;
                    fila = fila.next();
                }


                var count = tabla.dom.childNodes.length - items;
                //alert(count);
                var tr = tabla.createChild({ tag: 'td' });


                tr.createChild({ tag: 'td' });
                tr.createChild({ tag: 'td' });

                var td = tr.first();
                td.createChild({ tag: 'input', type: 'file', id: (panelGrid + ':File' + count), name: (panelGrid + ':File' + count) });

                td = td.next();
                td.createChild({ tag: 'input', type: 'text', id: panelGrid + ':File' + count + ':Text', name: panelGrid + ':File' + count + ':Text' });

                td = td.next();
                td.createChild({ tag: 'input', type: 'checkbox', id: panelGrid + ':CheckBox' + count, name: panelGrid + ':CheckBox' + count });


            }
            catch (ex) {
                alert(ex);
            }
        }

        function removerArchivo(panelGrid) {
            try {
                var tabla = Ext.get(panelGrid);
                tabla = tabla.first();
                var tr = tabla.first();

                tr = tr.next();
                var cont = 1;
                while (tr != null) {
                    var td = tr.first();
                    if (td.first().dom.tagName.toUpperCase() != 'A') {
                        td.first().dom.id = panelGrid + ':File' + cont;
                        td.first().dom.name = td.first().id;

                        td = tr.first().next();
                        td.first().dom.id = panelGrid + ':File:' + cont + ':Text';
                        td.first().dom.name = td.first().id;

                        td = tr.first().next().next();
                        td.first().dom.id = panelGrid + ':CheckBox' + cont;
                        td.first().dom.name = td.first().id;
                        cont++;
                    }
                    else
                        td = tr.first().next().next();
                    //refencio al checkbox para ver si ta checkiado
                    if (td.first().dom.checked == true) {
                        tr.remove();
                        //lo regreso al inicio para q pueda seguir
                        tr = tabla.first();
                        cont = 1;
                    }
                    tr = tr.next();

                }
            }
            catch (ex) {
                alert(ex);
            }
        }

        function plusSize() {
            parent.document.getElementById('placeholder_frame').style.height = document['body'].offsetHeight + 'px';
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">


   <asp:ScriptManager ID="ScriptManager1" runat="server" />
        
	<asp:UpdatePanel runat="server" id="UpdatePanel" updatemode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger controlid="cmbCategoria" eventname="TextChanged" />
        </Triggers>
            <ContentTemplate>
                      
            <div id="reg-falta" class="signuponepage main content clearfix">
                <div class="clearfix">
                    <div class="side-content" style="margin:5px;">
                         <div class="signup-box" style="padding:15px;">
                         <h2 class="redtext">Registro de Faltas<ext:ResourceManager ID="ResourceManager1" 
                                 runat="server" />
                             </h2>
                         <div id="date-doc-form-element" class="form-element multi-field general">
                            <fieldset>
                                <label class="column1" style="width:20%;">
                                <strong>Fecha</strong>
                                    <asp:TextBox ID="txtFecha" runat="server" ReadOnly="True"></asp:TextBox>
                                </label>
                                <label class="column2" style="width:20%;">
                                    <strong># Acta</strong>
                                    <asp:TextBox ID="txtNumeroActa" runat="server" ReadOnly="True"></asp:TextBox>
                                </label>
                            </fieldset>
                          </div>
                          <div id="account-name-form-element" class="form-element multi-field general">
                          <h6 class="redtext">Datos del Alumno</h6>
                            <fieldset>
                                <label class="column1" style="width:20%;">
                                <strong>Cuenta</strong>
                                        <asp:TextBox ID="txtCuenta" runat="server" AutoPostBack="True" ontextchanged="txtCuenta_TextChanged" ></asp:TextBox>
                                </label>
                                <label class="column2"style="width:60%;">
                                    <strong>Nombre completo</strong>
                                    <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
                                </label>
                            </fieldset>
                          </div>
                           <div id="car-form-element" class="form-element general-entry">
                           <fieldset>
                                <label class="column1" style="width:40%;">
                                <strong>Carrera</strong>
                                    <div class="goog-inline-block goog-flat-menu-button jfk-select styled-select">
                                        <asp:DropDownList ID="cmbCarrera" runat="server" DataTextField="CarreraNombre" DataValueField="CarreraId">
                                        </asp:DropDownList>
                                    </div>
                                </label>
                          </fieldset>
                          </div>
                           <div id="gen-form-element" class="form-element general-entry">
                           <fieldset>
                                <label class="column1" style="width:30%;">
                                <strong>Genero</strong>
                                    <div class="goog-inline-block goog-flat-menu-button jfk-select styled-select">
                                        <asp:DropDownList ID="radioBtnGenero" runat="server">
                                            <asp:ListItem Selected="True" Value="F">Femenino</asp:ListItem>
                                            <asp:ListItem Value="M">Masculino</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </label>
                          </fieldset>
                          </div>
                        <div id="Div3" class="form-element multi-field general">
                          <h6 class="redtext">Informacion de la Falta</h6>
                            <fieldset>
                                <label class="column1" style="width:30%;">
                                <strong>Titulo de la falta</strong>
                                    <asp:TextBox ID="txtFaltaTitulo" runat="server"></asp:TextBox>
                                </label>
                            </fieldset>
                          </div>
                        <div id="inf-form-element" class="form-element multi-field general">
                            <fieldset>
                                <label class="column1" style="width:30%;">
                                <strong>Categoria</strong>
                                    <div class="goog-inline-block goog-flat-menu-button jfk-select styled-select">
                                    <asp:DropDownList ID="cmbCategoria" runat="server" DataTextField="CategoriaNombre" 
                                        DataValueField="CategoriaId" AutoPostBack="True" ontextchanged="cmbCategoria_TextChanged">
                                    </asp:DropDownList>
                                    </div>
                                </label>
                                <label class="column2"style="width:50%;">
                                    <strong>Tipo</strong>
                                    <div class="goog-inline-block goog-flat-menu-button jfk-select styled-select">
                                    <asp:DropDownList ID="cmbTipoIncisoDescripcion" runat="server" AutoPostBack="True" 
                                        ontextchanged="cmbTipoIncisoDescripcion_TextChanged">
                                    </asp:DropDownList>
                                    </div>
                                </label>
                            </fieldset>
                          </div>
                         <div id="art-in-form-element" class="form-element multi-field general">
                            <fieldset>
                                <label class="column1" style="width:25%;">
                                <strong>Articulo #</strong>
                                    <asp:TextBox ID="txtArticuloNumero" runat="server" ReadOnly="True"></asp:TextBox>
                                </label>
                                <label class="column2" style="width:30%;">
                                <strong>Letra del inciso</strong>
                                    <asp:TextBox ID="txtIncisoLetra" runat="server" ReadOnly="True"></asp:TextBox>
                                </label>
                            </fieldset>
                          </div>
                          <div id="desInc-form-element" class="form-element general-entry">
                                <label class="column1" style="width:85%;">
                                    <asp:TextBox class="TextArea" ID="txtIncisoDescripcion" runat="server" 
                                    TextMode="MultiLine" 
                                    style="width: 100%; height: 155px; max-width:100%; max-height:155px;" 
                                    ontextchanged="txtIncisoDescripcion_TextChanged"></asp:TextBox>
                                </label>
                          </div>
                          <div id="desF-form-element" class="form-element general-entry">
                             <label class="column1" style="width:85%; top: 97px; left: 0px;">
                                    <strong>Descripcion de la falta cometida</strong>
                                    <asp:TextBox class="TextArea" ID="txtDecripcionFalta" runat="server" 
                                  TextMode="MultiLine" 
                                  style="width: 100%; height: 155px; max-width:100%; max-height:155px;" 
                                  ontextchanged="txtDecripcionFalta_TextChanged"></asp:TextBox>
                             </label>
                          </div>
                        <div id="Div2" class="form-element multi-field general">
                          <h6 class="redtext">Sancion</h6>
                            <fieldset>
                                <label class="column1" style="width:25%;">
                                <strong>Tipo</strong>
                                    <div class="goog-inline-block goog-flat-menu-button jfk-select styled-select">
                                    <asp:DropDownList ID="cmbTipoSancion" runat="server" 
                                        DataTextField="SancionNombre" DataValueField="SancionId">
                                    </asp:DropDownList>
                                    </div>
                                </label><!--aki voy-->
                                <label class="column2"style="width:60%;">
                                    <strong>Tiempo</strong>
                                    <asp:TextBox ID="txtTiempoSancion" runat="server"></asp:TextBox>
                                </label>
                            </fieldset>
                         </div>
                          <div id="Div1" class="form-element general-entry">
                             <label class="column1" style="width:85%;">
                                    <strong>Observacion</strong>
                                    <asp:TextBox class="TextArea" ID="txtDescripcionSancion" runat="server" TextMode="MultiLine" style="width: 100%; height: 155px; max-width:100%; max-height:155px;"></asp:TextBox>
                             </label>
                          </div>
                        <div id="Div4" class="form-element multi-field general">
                          <h6 class="redtext">Evidencia</h6>
                            <fieldset>
                                 <asp:ListView ID="evidencias" runat="server">
                                                 <EmptyDataTemplate>
                                                   <table id="evidencias_itemPlaceholderContainer" style="width: 100%;">
                                                <tr>
                                                    <td>
                                                       <span class="inputLabel">Archivo</span>
                                                    </td>
                                                    <td>
                                                        <span class="inputLabel">Descripcion</span>
                                                    </td>
                                                    <td>
                                                        <span class="inputLabel">Eliminar</span>
                                                    </td>
                                                </tr>
                                               
                                            </table>
                                                 </EmptyDataTemplate>
                                             <ItemTemplate>
                                                    <tr style="">
                                            
                                                        <td class="inputLabel">
                                                            <a href="Archivo.ashx?cod=<%# Eval("EvidenciaId") %>">Descargar </a>    
                                                        </td>
                                                        <td class="inputLabel">
                                                           <%# Eval("EvidenciaDescripcion") %>
                                                            <input type="hidden"  name="Archivo<%#Eval("EvidenciaId") %>" value="File_<%#Eval("EvidenciaId") %>"  />
                                                        </td>
                                                        <td>
                                                            <input id="Checkbox1" type="checkbox" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <LayoutTemplate>
                                                  
                                                                <table ID="itemPlaceholderContainer" width="100%" runat="server" border="0" style="">
                                                                      <tr>
                                                                                <td>
                                                                                   <span class="inputLabel">Archivo</span>
                                                                                </td>
                                                                                <td>
                                                                                    <span class="inputLabel">Descripcion</span>
                                                                                </td>
                                                                                <td>
                                                                                    <span class="inputLabel">Eliminar</span>
                                                                                </td>
                                                                        </tr>
                                                                    <tr ID="itemPlaceholder" runat="server">
                                                                    </tr>
                                                                </table>
                                                            
                                                </LayoutTemplate>
                                            </asp:ListView>
                                <asp:Button ID="btnAddFile" class="g-button" runat="server" Text="Agregar" 
                                     OnClientClick="agregarArchivo('evidencias_itemPlaceholderContainer');plusSize();return false;" />
                                <asp:Button ID="btnRemoveFile" class="g-button" runat="server" Text="Eliminar" OnClientClick="removerArchivo('evidencias_itemPlaceholderContainer');plusSize();return false;" />

                            </fieldset>
                         </div>

                              <asp:Button ID="btnNuevo" class="g-button" runat="server" Text="Nuevo" onclick="btnNuevo_Click" />
                              <asp:Button ID="btnGuardar" class="g-button" runat="server" Text="Guardar" OnClientClick="return validarEnBlanco();"  onclick="btnGuardar_Click" />
                        </div>
                    </div>
                </div>
          </div>
            <asp:FileUpload style="DISPLAY:NONE" ID="FileUpload1" runat="server" />

            	</ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
