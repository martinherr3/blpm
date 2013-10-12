USE [EDUAR]

SET IDENTITY_INSERT [EDUAR].[dbo].[TipoPersonaRol] ON

INSERT INTO [TipoPersonaRol] ([idTipoPersonaRol], [idTipoPersona], [nombreRol]) VALUES ('1', '1', 'Docente');
GO
INSERT INTO [TipoPersonaRol] ([idTipoPersonaRol], [idTipoPersona], [nombreRol]) VALUES ('2', '1', 'Psicopedagogo');
GO
INSERT INTO [TipoPersonaRol] ([idTipoPersonaRol], [idTipoPersona], [nombreRol]) VALUES ('3', '1', 'Preceptor');
GO
INSERT INTO [TipoPersonaRol] ([idTipoPersonaRol], [idTipoPersona], [nombreRol]) VALUES ('4', '1', 'Director');
GO
INSERT INTO [TipoPersonaRol] ([idTipoPersonaRol], [idTipoPersona], [nombreRol]) VALUES ('5', '2', 'Alumno');
GO
INSERT INTO [TipoPersonaRol] ([idTipoPersonaRol], [idTipoPersona], [nombreRol]) VALUES ('6', '3', 'Tutor');
GO
INSERT INTO [TipoPersonaRol] ([idTipoPersonaRol], [idTipoPersona], [nombreRol]) VALUES ('7', '4', 'Administrador');
GO

SET IDENTITY_INSERT [EDUAR].[dbo].[TipoPersonaRol] OFF