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

USE [EDUAR_aspnet_services]

INSERT INTO aspnet_Users ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES ('340DD3A1-F04E-427C-8851-07EBECA2417A', 
'379AA002-D108-4F26-9597-24CE888FFAEB', 
'eduar.administrador', 
'eduar.administrador', 
NULL, 
0, 
GETDATE());
GO

USE [EDUAR_aspnet_services]

INSERT INTO aspnet_Membership([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES ('340DD3A1-F04E-427C-8851-07EBECA2417A', 
'379AA002-D108-4F26-9597-24CE888FFAEB', 
'h4d/w+B6jR76h4/XHzxtQw==', 
0, 
'auG5MSjlTT6S0g25LeZ6YA==', 
NULL, 
'eduar.administrador@eduar2.com', 
'eduar.administrador@eduar2.com', 
'Mi Número de Documento es', 
'32111222', 
1, 
0, 
getdate(), 
getdate(), 
getdate(), 
getdate(), 
0, 
0, 
0, 
0, 
NULL);
GO

USE [EDUAR_aspnet_services]

INSERT INTO aspnet_UsersInRoles ([UserId], [RoleId]) VALUES (
'379AA002-D108-4F26-9597-24CE888FFAEB', 
'41AA295B-7655-4D6D-8ED2-919F6EE6A02A');
GO
