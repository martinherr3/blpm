USE [EDUAR]

SET IDENTITY_INSERT [EDUAR].[dbo].[TipoNovedadAulica] ON

INSERT INTO [TipoNovedadAulica] ([idTipoNovedadAulica], [descripcion]) VALUES ('1', 'Disciplina');
GO
INSERT INTO [TipoNovedadAulica] ([idTipoNovedadAulica], [descripcion]) VALUES ('2', 'Comportamiento');
GO
INSERT INTO [TipoNovedadAulica] ([idTipoNovedadAulica], [descripcion]) VALUES ('3', 'Otros');
GO

SET IDENTITY_INSERT [EDUAR].[dbo].[TipoNovedadAulica] OFF
