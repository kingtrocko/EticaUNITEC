﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReporteAlumnos.aspx.cs" Inherits="EticaUNITEC.Reporte.ReporteAlumnos" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <link href="../Css/grids/style.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/style1.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/style2.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/style3.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/signup.css" rel="stylesheet" type="text/css" />
   <style type="text/css">
    #form1
    {
        height: 470;
        width: 900px;
        min-height:75%;
    }
    </style>
    
    
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>

     <div class="wrapper">
        <div class="signuponepage main content clearfix">
                <div class="signup-steps step-1 clearfix">
                    <h1 class="redtext">Reporte de Faltas por Alumno</h1>
                </div>
        </div>
    </div>
    <div id="container">


    <asp:ScriptManager ID="ScriptManager1" 
    runat="server" 
    EnablePartialRendering="true">
   
    </asp:ScriptManager>
    <telerik:RadGrid ID="RadGrid1" 
    runat="server" 
    AllowSorting="True" 
        CellSpacing="0"
         DataSourceID="SqlDataSource1" 
            AllowPaging="True"
            GridLines="None"
            CssClass="mGrid"
            PagerStyle-CssClass="mGrid"
            AlternatingRoridLines="None"
            AllowStyle-CssClass="mGrid"
            AlternatingItemStyle-CssClass ="alt"
            
           
           >
           

        <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
<MasterTableView autogeneratecolumns="False" datasourceid="SqlDataSource1">
<CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

<RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
<HeaderStyle Width="20px"></HeaderStyle>
</RowIndicatorColumn>

<ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
<HeaderStyle Width="20px"></HeaderStyle>
</ExpandCollapseColumn>

    <Columns>
        <telerik:GridBoundColumn DataField="FaltaId" 
            FilterControlAltText="Filter FaltaId column" HeaderText="Identificador de Falta" 
            SortExpression="FaltaId" UniqueName="FaltaId">
        </telerik:GridBoundColumn>
        
        <telerik:GridBoundColumn DataField="AlumnoNombre" 
            FilterControlAltText="Filter AlumnoNombre column" 
            HeaderText="Nombre" SortExpression="AlumnoNombre" 
            UniqueName="Alumnonombre">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="AlumnoCuenta"  
            FilterControlAltText="Filter AlumnoCuenta column" HeaderText="No. de Cuenta" 
            SortExpression="AlumnoCuenta" UniqueName="AlumnoCuenta">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="CarreraNombre" 
            FilterControlAltText="Filter CarreraNombre column" HeaderText="Nombre Carrera" 
            SortExpression="CarreraNombre" UniqueName="CarreraNombre">
        </telerik:GridBoundColumn>
         <telerik:GridBoundColumn DataField="ArticuloNumero" DataType="System.Int32" 
            FilterControlAltText="Filter ArticuloNumero column" HeaderText="Número Articulo" 
            SortExpression="ArticuloNumero" UniqueName="ArticuloNumero">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="IncisoLetra" 
            FilterControlAltText="Filter IncisoLetra column" HeaderText="Inciso" 
            SortExpression="IncisoLetra" UniqueName="IncisoLetra">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="FaltaTitulo" 
            FilterControlAltText="Filter FaltaTitulo column" HeaderText="Falta" 
            SortExpression="FaltaTitulo" UniqueName="faltatitulo">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="FaltaFecha" DataType="System.DateTime" 
            FilterControlAltText="Filter FaltaFecha column" HeaderText="Fecha Falta" 
            SortExpression="FaltaFecha" UniqueName="FaltaFecha">
        </telerik:GridBoundColumn>
    </Columns>
    
    <GroupByExpressions>
      <telerik:GridGroupByExpression>
        <GroupByFields>
          <telerik:GridGroupByField FieldName="AlumnoCuenta" />
        </GroupByFields>
        </telerik:GridGroupByExpression>
    </GroupByExpressions>

<EditFormSettings>
<EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
</EditFormSettings>
</MasterTableView>

<FilterMenu EnableImageSprites="False">
<WebServiceSettings>
<ODataSettings InitialContainerName=""></ODataSettings>
</WebServiceSettings>
</FilterMenu>

<HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default">
<WebServiceSettings>
<ODataSettings InitialContainerName=""></ODataSettings>
</WebServiceSettings>
</HeaderContextMenu>
    <PagerStyle CssClass="pgr"></PagerStyle>
    </telerik:RadGrid>
        <br />
    </div>
    </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:DBLocalConnectionString %>" 
        SelectCommand="SELECT * FROM [AgrupadoAlumnos]"></asp:SqlDataSource>
    </form>
</body>
</html>
