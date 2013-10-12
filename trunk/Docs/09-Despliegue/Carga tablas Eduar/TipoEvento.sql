USE [EDUAR]

SET IDENTITY_INSERT [EDUAR].[dbo].[TipoEvento] ON

INSERT INTO [TipoEvento] ([idTipoEvento], [descripcion]) VALUES ('1', 'Evaluación');
GO
INSERT INTO [TipoEvento] ([idTipoEvento], [descripcion]) VALUES ('2', 'Reunión');
GO
INSERT INTO [TipoEvento] ([idTipoEvento], [descripcion]) VALUES ('3', 'Excursión');
GO
INSERT INTO [TipoEvento] ([idTipoEvento], [descripcion]) VALUES ('4', 'Clase Diaria');
GO


SET IDENTITY_INSERT [EDUAR].[dbo].[TipoEvento] OFF
