<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsertarEvidencia.aspx.cs" Inherits="EticaUNITEC.InsertarEvidencia" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    
    </div>
    <br />
    <asp:FileUpload  ID="FileUpload1" runat="server" />
    <br />
    <br />
    <asp:TextBox ID="TextBox2" runat="server" ontextchanged="TextBox2_TextChanged"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
    <asp:Image ID="Image1" runat="server" ImageUrl="~/Faltas/Archivo.ashx?cod=14" />



    <asp:ListView ID="ListView1" runat="server" DataKeyNames="EvidenciaId" 
        DataSourceID="SqlDataSource1">
       
        <EditItemTemplate>
            <tr style="background-color: #FFCC66;color: #000080;">
                <td>
                    <asp:Button ID="UpdateButton" runat="server" CommandName="Update" 
                        Text="Update" />
                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                        Text="Cancel" />
                </td>
                <td>
                    <asp:TextBox ID="EvidenciaDescripcionTextBox" runat="server" 
                        Text='<%# Bind("EvidenciaDescripcion") %>' />
                </td>
                <td>
                    <asp:TextBox ID="EvidenciaNombreArchivoTextBox" runat="server" 
                        Text='<%# Bind("EvidenciaNombreArchivo") %>' />
                </td>
                <td>
                    <asp:Label ID="EvidenciaIdLabel1" runat="server" 
                        Text='<%# Eval("EvidenciaId") %>' />
                </td>
                <td>
                    <asp:TextBox ID="EvidenciaContenidoTextBox" runat="server" 
                        Text='<%# Bind("EvidenciaContenido") %>' />
                </td>
            </tr>
        </EditItemTemplate>
        <EmptyDataTemplate>
            <table runat="server" 
                style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;">
                <tr>
                    <td>
                        No data was returned.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <InsertItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="InsertButton" runat="server" CommandName="Insert" 
                        Text="Insert" />
                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                        Text="Clear" />
                </td>
                <td>
                    <asp:TextBox ID="EvidenciaDescripcionTextBox" runat="server" 
                        Text='<%# Bind("EvidenciaDescripcion") %>' />
                </td>
                <td>
                    <asp:TextBox ID="EvidenciaNombreArchivoTextBox" runat="server" 
                        Text='<%# Bind("EvidenciaNombreArchivo") %>' />
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:TextBox ID="EvidenciaContenidoTextBox" runat="server" 
                        Text='<%# Bind("EvidenciaContenido") %>' />
                </td>
            </tr>
        </InsertItemTemplate>
        <ItemTemplate>
            <tr style="background-color: #FFFBD6;color: #333333;">
                <td>
                    <asp:Label ID="EvidenciaDescripcionLabel" runat="server" 
                        Text='<%# Eval("EvidenciaDescripcion") %>' />
                </td>
                <td>
                    <a href="Faltas/Archivo.ashx?cod=<%# Eval("EvidenciaId") %>">
                    <asp:Label ID="EvidenciaNombreArchivoLabel" runat="server" 
                        Text='<%# Eval("EvidenciaNombreArchivo") %>' />
                        </a>
                </td>
                <td>
                    <asp:Label ID="EvidenciaIdLabel" runat="server" 
                        Text='<%# Eval("EvidenciaId") %>' />
                </td>
              
                <td>
                      <img onclick="alert('<%# Eval("EvidenciaId") %>');" alt="" src="Faltas/Archivo.ashx?cod=<%# Eval("EvidenciaId") %>" />
                </td>
            </tr>
        </ItemTemplate>
        <LayoutTemplate>
            <table runat="server">
                <tr runat="server">
                    <td runat="server">
                        <table ID="itemPlaceholderContainer" runat="server" border="1" 
                            style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                            <tr runat="server" style="background-color: #FFFBD6;color: #333333;">
                                <th runat="server">
                                    EvidenciaDescripcion</th>
                                <th runat="server">
                                    EvidenciaNombreArchivo</th>
                                <th runat="server">
                                    EvidenciaId</th>
                                <th runat="server">
                                    EvidenciaContenido</th>
                            </tr>
                            <tr ID="itemPlaceholder" runat="server">
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server">
                    <td runat="server" 
                        style="text-align: center;background-color: #FFCC66;font-family: Verdana, Arial, Helvetica, sans-serif;color: #333333;">
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
        <SelectedItemTemplate>
            <tr style="background-color: #FFCC66;font-weight: bold;color: #000080;">
                <td>
                    <asp:Label ID="EvidenciaDescripcionLabel" runat="server" 
                        Text='<%# Eval("EvidenciaDescripcion") %>' />
                </td>
                <td>
                    <asp:Label ID="EvidenciaNombreArchivoLabel" runat="server" 
                        Text='<%# Eval("EvidenciaNombreArchivo") %>' />
                </td>
                <td>
                    <asp:Label ID="EvidenciaIdLabel" runat="server" 
                        Text='<%# Eval("EvidenciaId") %>' />
                </td>
                <td>
                    <asp:Label ID="EvidenciaContenidoLabel" runat="server" 
                        Text='<%# Eval("EvidenciaContenido") %>' />
                </td>
            </tr>
        </SelectedItemTemplate>
    </asp:ListView>



    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:DBLocalConnectionString %>" 
        SelectCommand="SELECT [EvidenciaDescripcion], [EvidenciaNombreArchivo], [EvidenciaId], [EvidenciaContenido] FROM [Evidencias]">
    </asp:SqlDataSource>



    </form>
</body>
</html>
