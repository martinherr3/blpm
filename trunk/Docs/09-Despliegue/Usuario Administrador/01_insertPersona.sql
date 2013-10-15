USE [EDUAR]

SET IDENTITY_INSERT [EDUAR].[dbo].Personas ON

ALTER TABLE Personas NOCHECK CONSTRAINT FK_Personas_Localidades
ALTER TABLE Personas NOCHECK CONSTRAINT FK_Personas_TiposDocumentos

INSERT INTO Personas ([idPersona], [nombre], [apellido], [numeroDocumento], [idTipoDocumento], [domicilio], [barrio], [idLocalidad], [sexo], [fechaNacimiento], [telefonoFijo], [telefonoCelular], [telefonoCelularAlternativo], [email], [activo], [username], [idTipoPersona]) 
VALUES (
'1', 
'Juan Carlos', 
'Administrador', 
'32111222', 
1, 
'Marcelo T de Alvear 540', 
'', 
1, 
'M', 
'1984-10-13', 
NULL, 
NULL, 
NULL, 
'eduar.administrador@eduar2.com', 
'1', 
'eduar.administrador', 
'4');
GO

ALTER TABLE Personas CHECK CONSTRAINT FK_Personas_Localidades
ALTER TABLE Personas CHECK CONSTRAINT FK_Personas_TiposDocumentos 
SET IDENTITY_INSERT [EDUAR].[dbo].Personas OFF