<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExtEjemplo1.aspx.cs" Inherits="EticaUNITEC.Ejemplos_Clase.ExtEjemplo1" %>

<%@ Register assembly="Ext.Net" namespace="Ext.Net" tagprefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
     
    </div>
    <ext:ResourceManager ID="ResourceManager1" runat="server" />
    <script type="text/javascript">
        Ext.Msg.alert('Titulo', 'Contenido');
    </script>

    <ext:DateField ID="DateField1" runat="server" AllowBlank="False" 
        BlankText="Este campo es necesario papo viejo!!!" />
    <ext:Label ID="Label1" runat="server">
    </ext:Label>
    <ext:Button ID="Button1" runat="server" Icon="Add" Text="Submit" 
        ToolTip="Salvar" >
       <DirectEvents>
          <Click  OnEvent="Click"  >  
             <EventMask Msg="Esperate vo!!!!" ShowMask="true" />
          </Click>
       </DirectEvents>
       <Listeners>
          <Click Handler="return Ext.getCmp('DateField1').validate();" /> 
       </Listeners>    
    </ext:Button>
        

    </form>
    
</body>
</html>
