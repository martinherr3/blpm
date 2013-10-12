USE [EDUAR]

SET IDENTITY_INSERT [EDUAR].[dbo].[TipoRegistroClases] ON

INSERT INTO [TipoRegistroClases] ([idTipoRegistroClases], [nombre]) VALUES ('1', 'Clase Normal');
GO
INSERT INTO [TipoRegistroClases] ([idTipoRegistroClases], [nombre]) VALUES ('2', 'Ausencia Docente Con Aviso');
GO
INSERT INTO [TipoRegistroClases] ([idTipoRegistroClases], [nombre]) VALUES ('3', 'Ausencia Docente Sin Aviso');
GO
INSERT INTO [TipoRegistroClases] ([idTipoRegistroClases], [nombre]) VALUES ('4', 'Ausencia Alumnos');
GO
INSERT INTO [TipoRegistroClases] ([idTipoRegistroClases], [nombre]) VALUES ('5', 'Paro Docente');
GO
INSERT INTO [TipoRegistroClases] ([idTipoRegistroClases], [nombre]) VALUES ('6', 'Taller Docente');
GO
INSERT INTO [TipoRegistroClases] ([idTipoRegistroClases], [nombre]) VALUES ('7', 'Feriado');
GO

SET IDENTITY_INSERT [EDUAR].[dbo].[TipoRegistroClases] OFF