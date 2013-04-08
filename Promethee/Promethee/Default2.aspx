<%@ Page Title="Método Promethee" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Default2.aspx.cs" Inherits="Promethee.Default2" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>
        Inicialización del Método</h1>
    <ol>
        <li>Alternativas (cantidad y nombre).</li>
        <li>Criterios (cantidad, nombre, peso y función de preferencia de cada uno).</li>
        <li>Cargar los valores.</li>
    </ol>
    <h1>
        Paso 1: Determinar como se situan las alternativas con respecto a cada atributo
    </h1>
    <h1>
        Paso 2: Expresar la intensidad de la preferencia de la alternativa Xi comparada
        con Xk
    </h1>
    <h1>
        Paso 3: Expresar cómo xi supera a las demás alternativas y cómo es superada por
        las otras.
    </h1>
    <h1>
        Paso 4: Obtener el preorden deseado
    </h1>
    <h1>
        PROMETHEE II</h1>
</asp:Content>
