USE [EDUAR]

SET IDENTITY_INSERT [EDUAR].[dbo].[Indicadores] ON

INSERT INTO [Indicadores] ([idIndicador], [nombre], [nombreSP], [invertirEscala], [parametroCantidad], [diasHastaPrincipal], [diasHastaIntermedio], [diasHastaSecundario], [verdeNivelPrincipal], [verdeNivelIntermedio], [verdeNivelSecundario], [rojoNivelPrincipal], [rojoNivelIntermedio], [rojoNivelSecundario]) VALUES (1, '% De Cobertura de la Planificación', 'Planificacion', '1', 0, 30, 40, 400, 85, 50, 50, 50, 25, 10);
GO
INSERT INTO [Indicadores] ([idIndicador], [nombre], [nombreSP], [invertirEscala], [parametroCantidad], [diasHastaPrincipal], [diasHastaIntermedio], [diasHastaSecundario], [verdeNivelPrincipal], [verdeNivelIntermedio], [verdeNivelSecundario], [rojoNivelPrincipal], [rojoNivelIntermedio], [rojoNivelSecundario]) VALUES (2, 'Alumnos con más de X faltas', 'Inasistencias', '0', 1, 60, 150, 365, 1, 2, 3, 12, 7, 5);
GO
INSERT INTO [Indicadores] ([idIndicador], [nombre], [nombreSP], [invertirEscala], [parametroCantidad], [diasHastaPrincipal], [diasHastaIntermedio], [diasHastaSecundario], [verdeNivelPrincipal], [verdeNivelIntermedio], [verdeNivelSecundario], [rojoNivelPrincipal], [rojoNivelIntermedio], [rojoNivelSecundario]) VALUES (3, 'Ausencias Docentes', 'InasistenciasDocentes', '0', 0, 200, 300, 400, 0, 3, 3, 15, 10, 15);
GO
INSERT INTO [Indicadores] ([idIndicador], [nombre], [nombreSP], [invertirEscala], [parametroCantidad], [diasHastaPrincipal], [diasHastaIntermedio], [diasHastaSecundario], [verdeNivelPrincipal], [verdeNivelIntermedio], [verdeNivelSecundario], [rojoNivelPrincipal], [rojoNivelIntermedio], [rojoNivelSecundario]) VALUES (4, 'Alumnos con más de X Amonestaciones', 'Sanciones', '0', 4, 30, 60, 365, 3, 2, 3, 10, 7, 10);
GO
INSERT INTO [Indicadores] ([idIndicador], [nombre], [nombreSP], [invertirEscala], [parametroCantidad], [diasHastaPrincipal], [diasHastaIntermedio], [diasHastaSecundario], [verdeNivelPrincipal], [verdeNivelIntermedio], [verdeNivelSecundario], [rojoNivelPrincipal], [rojoNivelIntermedio], [rojoNivelSecundario]) VALUES (5, 'Promedio General', 'PromedioGeneral', '1', 0, 90, 190, 365, 6, 6, 6, 4, 4, 4);
GO
INSERT INTO [Indicadores] ([idIndicador], [nombre], [nombreSP], [invertirEscala], [parametroCantidad], [diasHastaPrincipal], [diasHastaIntermedio], [diasHastaSecundario], [verdeNivelPrincipal], [verdeNivelIntermedio], [verdeNivelSecundario], [rojoNivelPrincipal], [rojoNivelIntermedio], [rojoNivelSecundario]) VALUES (6, 'Alumnos con X Asignaturas Reprobadas', 'Reprobadas', '0', 5, 90, 150, 365, 2, 2, 2, 4, 6, 8);
GO


SET IDENTITY_INSERT [EDUAR].[dbo].[Indicadores] OFF