﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConsultaCategorias.aspx.cs" Inherits="EticaUNITEC.Reporte.ConsultaCategorias" %>

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
                    <h1 class="redtext">Reporte de Faltas por Categoría</h1>
                </div>
        </div>
    </div>

     <div id="container">
        
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
        <telerik:RadGrid ID="RadGrid1" runat="server" AllowPaging="True" 
            AllowSorting="True" CellSpacing="0" DataSourceID="SqlDataSource1" 
            GridLines="None" ShowGroupPanel="True" 
            onneeddatasource="RadGrid1_NeedDataSource">
            <ClientSettings AllowDragToGroup="True">
                <Selecting AllowRowSelect="True" />
                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
            </ClientSettings>
<MasterTableView autogeneratecolumns="False" datakeynames="CategoriaId" 
                datasourceid="SqlDataSource1">
 <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

<RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
<HeaderStyle Width="20px"></HeaderStyle>
</RowIndicatorColumn>

<ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
<HeaderStyle Width="20px"></HeaderStyle>
</ExpandCollapseColumn>

    <Columns>
        <telerik:GridBoundColumn DataField="CategoriaNombre" 
            FilterControlAltText="Filter CategoriaNombre column" 
            HeaderText="Nombre de la Categoria" SortExpression="CategoriaNombre" 
            UniqueName="CategoriaNombre">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="ArticuloNumero" 
            FilterControlAltText="Filter ArticuloNumero column" HeaderText="Numero del Articulo" 
            SortExpression="ArticuloNumero" UniqueName="ArticuloNumero" 
            DataType="System.Int32">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="IncisoLetra" 
            FilterControlAltText="Filter IncisoLetra column" HeaderText="Inciso" 
            SortExpression="IncisoLetra" UniqueName="IncisoLetra">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="IncisoContenido" 
            FilterControlAltText="Filter IncisoContenido column" HeaderText="Contenido" 
            SortExpression="IncisoContenido" UniqueName="IncisoContenido">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="IncisoDescripcion" 
            FilterControlAltText="Filter IncisoDescripcion column" HeaderText="Descripción" 
            SortExpression="IncisoDescripcion" UniqueName="IncisoDescripcion">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="CategoriaId" DataType="System.Int32" 
            FilterControlAltText="Filter CategoriaId column" HeaderText="Id Categoría" 
            ReadOnly="True" SortExpression="CategoriaId" UniqueName="CategoriaId" 
            Visible="False">
        </telerik:GridBoundColumn>
    </Columns>

    <GroupByExpressions>
      <telerik:GridGroupByExpression>
        <GroupByFields>
          <telerik:GridGroupByField FieldName="CategoriaNombre" />
        </GroupByFields>
        <SelectFields>
          <telerik:GridGroupByField FieldName="CategoriaNombre" HeaderText="Nombre Categoria" />
        </SelectFields>
      </telerik:GridGroupByExpression>
<telerik:GridGroupByExpression><SelectFields>
<telerik:GridGroupByField FieldName="ArticuloNumero" FieldAlias="ArticuloNumero" FormatString="" HeaderText="Numero del Articulo"></telerik:GridGroupByField>
</SelectFields>
<GroupByFields>
<telerik:GridGroupByField FieldName="ArticuloNumero" FieldAlias="ArticuloNumero" FormatString="" HeaderText=""></telerik:GridGroupByField>
</GroupByFields>
</telerik:GridGroupByExpression>
    </GroupByExpressions>

     <GroupByExpressions>
      <telerik:GridGroupByExpression>
        <GroupByFields>
          <telerik:GridGroupByField FieldName="ArticuloNumero" />
        </GroupByFields>
        <SelectFields>
          <telerik:GridGroupByField FieldName="ArticuloNumero" HeaderText="Numero del Articulo" />
        </SelectFields>
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
        </telerik:RadGrid>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:DBLocalConnectionString %>" 
            ProviderName="System.Data.SqlClient" 
            SelectCommand="SELECT [CategoriaNombre], [ArticuloNumero], [IncisoLetra], [IncisoContenido], [IncisoDescripcion], [CategoriaId] FROM [AgrupadoCategorias]">
        </asp:SqlDataSource>
    
    </div>
    </div>
    </form>
    
</body>
</html>
