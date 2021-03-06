USE [EDUAR]
GO
/****** Object:  StoredProcedure [dbo].[(TBR)Reporte_ConsolidadoInasistenciasPeriodo2]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 13/07/11
-- Parameters:
-- idAlumno: Alumno
-- periodo: Año 
-- =============================================
CREATE PROCEDURE [dbo].[(TBR)Reporte_ConsolidadoInasistenciasPeriodo2]
	 @idAlumno NUMERIC = NULL
	,@idCicloLectivo NUMERIC = NULL
	,@idPeriodo NUMERIC = 0
	,@idCurso NUMERIC = NULL
AS
 SET NOCOUNT OFF;

--Declare @fechaInicio date = null
--Declare @fechaFin date = null

--select 
--	@fechaInicio = min(fechaInicio), 
--	@fechaFin = max(fechaFin)
--from periodo
--where idciclolectivo=@idCicloLectivo


	SELECT PER.apellido+', '+PER.nombre as 'Alumno'
	,
	CASE @idPeriodo
	     WHEN 0 THEN 'Ciclo Lectivo '+cast(datepart(year,CL.fechaInicio) as varchar)
	     ELSE PRD.nombre
	 END
	 AS 'Periodo'
	,SUM(TA.valor) as 'Inasistencias'
	,NIV.nombre as 'Nivel'
	,TA.descripcion as 'Motivo'
    FROM 
		[Asistencia] AS A 
			INNER JOIN [AlumnoCursoCicloLectivo] AS ALC ON A.idAlumnoCursoCicloLectivo = ALC.idAlumnoCursoCicloLectivo
			INNER JOIN [TipoAsistencia] AS TA ON TA.idTipoAsistencia = A.idTipoAsistencia
			INNER JOIN [Alumnos] AS ALU ON alc.idAlumno = ALU.idAlumno
			INNER JOIN [Personas] AS PER ON ALU.idPersona = PER.idPersona
			INNER JOIN [CursosCicloLectivo] AS CCL ON ALC.idCursoCicloLectivo = CCL.idCursoCicloLectivo
			INNER JOIN [CicloLectivo] AS CL ON CCL.idCicloLectivo = CL.idCicloLectivo
			INNER JOIN [Curso] AS CUR ON CUR.idCurso = CCL.idCurso
			INNER JOIN [Periodo] AS PRD ON PRD.idCicloLectivo = CL.idCicloLectivo
			INNER JOIN [Nivel] AS NIV ON CUR.idNivel = NIV.idNivel
    WHERE 
		(@idAlumno IS NULL OR @idAlumno=alu.idAlumno)
	    AND (@idCicloLectivo IS NULL OR @idCicloLectivo=ccl.idCicloLectivo)
	    AND (@idCurso IS NULL OR @idCurso=ccl.idCursoCicloLectivo)
	    --AND (A.fecha BETWEEN CL.fechaInicio AND CL.fechaFin)
	    --AND (TA.idTipoAsistenciaTransaccional <> 1)
	GROUP BY PER.apellido+', '+PER.nombre, NIV.nombre, TA.descripcion,
	CASE @idPeriodo
	   WHEN 0 THEN 'Ciclo Lectivo '+cast(datepart(year,CL.fechaInicio) as varchar) 
	   ELSE PRD.nombre 
	END

GO
/****** Object:  StoredProcedure [dbo].[(TBR)Reporte_ConsolidadoSancionesPeriodo2]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 13/07/11
-- Parameters:
-- idAlumno: Alumno
-- periodo: Año 
-- =============================================
CREATE PROCEDURE [dbo].[(TBR)Reporte_ConsolidadoSancionesPeriodo2]
	 @idAlumno NUMERIC = NULL
	,@idCicloLectivo NUMERIC = NULL
	,@idPeriodo NUMERIC = 0
	,@idCurso NUMERIC = NULL
 
AS
 SET NOCOUNT OFF;
 
Declare @fechaInicio date = null
Declare @fechaFin date = null

if (@idPeriodo > 0)
begin
	SELECT 
		@fechaInicio = min(fechaInicio), 
		@fechaFin = max(fechaFin)
	FROM Periodo
	WHERE idciclolectivo=@idCicloLectivo
	  AND (@idPeriodo = 0 OR idPeriodo=@idPeriodo )
end

	SELECT 
		 PER.apellido+' '+PER.nombre as 'Alumno'
		,
		CASE @idPeriodo
			 WHEN 0 THEN 'Ciclo Lectivo '+cast(datepart(year,CL.fechaInicio) as varchar)
			 ELSE PRD.nombre
		 END
		 AS 'Periodo'
		, MS.descripcion as 'Motivo'
		, TS.nombre as 'Tipo'
		,
		CASE @idPeriodo
			 WHEN 0 THEN (S.cantidad)
			 ELSE sum(S.cantidad)
		 END
			AS 'Sanciones'
    FROM 
		[Sancion] AS S 
			INNER JOIN [AlumnoCursoCicloLectivo] AS ALC ON S.idAlumnoCursoCicloLectivo = ALC.idAlumnoCursoCicloLectivo
			INNER JOIN [Alumnos] AS ALU ON ALC.idAlumno = ALU.idAlumno
			INNER JOIN [Personas] AS PER ON ALU.idPersona = PER.idPersona
     		INNER JOIN [CursosCicloLectivo] AS CCL ON ALC.idCursoCicloLectivo = CCL.idCursoCicloLectivo
			INNER JOIN [Curso] AS CUR ON CUR.idCurso = CCL.idCurso
			INNER JOIN [CicloLectivo] AS CL ON CCL.idCicloLectivo = CL.idCicloLectivo
			INNER JOIN [Periodo] AS PRD ON PRD.idCicloLectivo = CL.idCicloLectivo
			INNER JOIN [MotivoSancion] AS MS ON S.idMotivoSancion  = MS.idMotivoSancion
			INNER JOIN [TipoSancion] AS TS ON S.idTipoSancion = TS.idTipoSancion	
    WHERE 
		(@idAlumno IS NULL OR @idAlumno=ALU.idAlumno)
	    AND (@idCicloLectivo IS NULL OR @idCicloLectivo=CL.idCicloLectivo)
	    AND (@idCurso IS NULL OR @idCurso=CCL.idCursoCicloLectivo)
	    AND (@idPeriodo IS NULL OR @idPeriodo = 0 OR S.fecha BETWEEN @fechaInicio AND @fechaFin)
	    AND (@idPeriodo IS NULL OR @idPeriodo = 0 OR @idPeriodo=PRD.idPeriodo)
	GROUP BY PER.apellido+' '+PER.nombre, MS.descripcion, TS.nombre
	,
	CASE @idPeriodo
	     WHEN 0 THEN 'Ciclo Lectivo '+cast(datepart(year,CL.fechaInicio) as varchar)
	     ELSE PRD.nombre
	END
	,S.cantidad

GO
/****** Object:  StoredProcedure [dbo].[(TBR)Reporte_PromedioCalificacionesPeriodoIntercurso]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 24/09/11
-- Parameters:
-- idAlumno: Alumno
-- periodo: Año 
-- =============================================
CREATE PROCEDURE [dbo].[(TBR)Reporte_PromedioCalificacionesPeriodoIntercurso]
	 @idAlumno NUMERIC = NULL
	,@idAsignatura NUMERIC = NULL
	,@idCicloLectivo NUMERIC = NULL
	,@idPeriodo NUMERIC = NULL
	,@idNivel NUMERIC = NULL
 
AS
 SET NOCOUNT OFF;
 
Declare @fechaInicio date = null
Declare @fechaFin date = null

select 
	@fechaInicio = min(fechaInicio), 
	@fechaFin = max(fechaFin)
from periodo
where idciclolectivo=@idCicloLectivo

if (@idPeriodo <= 0 OR @idPeriodo IS NULL)
BEGIN 
	SELECT 
	 PER.apellido+', '+PER.nombre as 'Alumno'
	,A.nombre as 'Asignatura'
	,'Ciclo Lectivo '+cast(datepart(year,@fechaInicio) as varchar) as 'Periodo'
	,AVG(cast(VES.nombre as int)) as 'Promedio'
    FROM 
		[Asignatura] AS A 
			INNER JOIN [AsignaturaCurso] AS AC ON A.idAsignatura = AC.idAsignatura
			INNER JOIN [AlumnoCurso] AS ALC ON AC.idCurso = ALC.idCurso
			INNER JOIN [Alumnos] AS ALU ON ALC.idAlumno = ALU.idAlumno
			INNER JOIN [Personas] AS PER ON ALU.idPersona = PER.idPersona
			INNER JOIN [Calificacion] AS CAL ON ALU.idAlumno = CAL.idAlumno AND AC.idAsignaturaCurso = CAL.idAsignaturaCurso
			INNER JOIN [ValoresEscalaCalificacion] AS VES ON CAL.idValorCalificacion = VES.idValorEscalaCalificacion
			INNER JOIN [CicloLectivo] AS CL ON AC.idCicloLectivo = CL.idCicloLectivo
			INNER JOIN [Curso] AS CUR ON CUR.idCurso = AC.idCurso
			INNER JOIN [Evaluacion] AS EV ON EV.idAsignaturaCurso = AC.idAsignaturaCurso
			INNER JOIN [Periodo] AS PRD ON PRD.idCicloLectivo = CL.idCicloLectivo
    WHERE 
		(@idAlumno IS NULL OR @idAlumno=ALU.idAlumno)
		AND (@idAsignatura IS NULL OR @idAsignatura=A.idAsignatura)
	    AND (@idCicloLectivo IS NULL OR @idCicloLectivo=CL.idCicloLectivo)
	    AND VES.idValorEscalaCalificacion < 14
	    AND CAL.fecha BETWEEN @fechaInicio AND @fechaFin
	    AND (@idNivel IS NULL OR @idNivel=CUR.idNivel)
	GROUP BY PER.apellido+', '+PER.nombre, A.nombre
END

if (@idPeriodo > 0)
BEGIN 
	SELECT 
		PER.apellido+', '+PER.nombre as 'Alumno'
		,A.nombre as 'Asignatura'
		,PRD.nombre as 'Periodo'
		,AVG(cast(VES.nombre as int)) as 'Promedio'
    FROM 
		[Asignatura] AS A 
			INNER JOIN [AsignaturaCurso] AS AC ON A.idAsignatura = AC.idAsignatura
			INNER JOIN [AlumnoCurso] AS ALC ON AC.idCurso = ALC.idCurso
			INNER JOIN [Alumnos] AS ALU ON ALC.idAlumno = ALU.idAlumno
			INNER JOIN [Personas] AS PER ON ALU.idPersona = PER.idPersona
			INNER JOIN [Calificacion] AS CAL ON ALU.idAlumno = CAL.idAlumno AND AC.idAsignaturaCurso = CAL.idAsignaturaCurso
			INNER JOIN [ValoresEscalaCalificacion] AS VES ON CAL.idValorCalificacion = VES.idValorEscalaCalificacion
			INNER JOIN [CicloLectivo] AS CL ON AC.idCicloLectivo = CL.idCicloLectivo
			INNER JOIN [Periodo] AS PRD ON PRD.idPeriodo = CAL.idPeriodo AND PRD.idCicloLectivo = CL.idCicloLectivo
			INNER JOIN [Curso] AS CUR ON CUR.idCurso = AC.idCurso
			INNER JOIN [Evaluacion] AS EV ON EV.idAsignaturaCurso = AC.idAsignaturaCurso
    WHERE 
		(@idAlumno IS NULL OR @idAlumno=ALU.idAlumno)
		AND (@idAsignatura IS NULL OR @idAsignatura=A.idAsignatura)
	    AND (@idCicloLectivo IS NULL OR @idCicloLectivo=CL.idCicloLectivo)
	    AND (@idPeriodo IS NULL OR @idPeriodo=PRD.idPeriodo)
	    AND VES.idValorEscalaCalificacion < 14
	    AND (@idNivel IS NULL OR @idNivel=CUR.idNivel)
	GROUP BY PER.apellido+', '+PER.nombre, A.nombre, PRD.nombre
END

GO
/****** Object:  StoredProcedure [dbo].[_proc_generate]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[_proc_generate]  
  @tableName AS VARCHAR(100)  
AS  
  
--CAPITALIZE TABLENAME  
SET @tableName = UPPER(LEFT(@tableName,1)) + RIGHT(@tableName, LEN(@tableName) -1)  
  
--SALTO DE LÍNEA  
DECLARE @nl AS CHAR  
SET @nl = CHAR(10) + CHAR(13)   
  
--CABECERA  
DECLARE @spHeaders AS VARCHAR(1000)  
SET @spHeaders = 'SET ANSI_NULLS ON' + @nl +  
'GO' + @nl +  
'SET QUOTED_IDENTIFIER ON' + @nl +  
'GO' + @nl +  
'-- =============================================' + @nl +  
'-- Author:  TU_NOMBRE' + @nl +  
'-- Create date: ' + CONVERT(VARCHAR, GETDATE(), 3) + @nl +  
'-- ============================================='  
  
DECLARE @table AS VARCHAR(MAX)  
DECLARE @column AS VARCHAR(MAX)  
DECLARE @data_type AS VARCHAR(MAX)  
DECLARE @length AS INT  
DECLARE @precision AS INT  
DECLARE @scale AS INT  
  
--PARÁMETROS  
DECLARE @spParameters AS VARCHAR(MAX) SET @spParameters = ''  
  
--LISTA DE CAMPOS  
DECLARE @fieldList AS VARCHAR(MAX) SET @fieldList = ''  
  
--LISTA DE CAMPOS PARA EL SET DEL UPDATE  
DECLARE @fieldSetList AS VARCHAR(MAX) SET @fieldSetList = ''  
  
--LISTA DE PARÁMETROS PARA EL INSERT  
DECLARE @insertParameters AS VARCHAR(MAX) SET @insertParameters = ''  
  
--CONDICIONES  
DECLARE @spConditions AS VARCHAR(MAX) SET @spConditions = ''  
  
DECLARE c CURSOR STATIC FOR  
select table_name, column_name, data_type, character_maximum_length,numeric_precision, numeric_scale from information_schema.columns where table_name = @tableName order by ordinal_position  
OPEN c FETCH NEXT FROM c INTO @table, @column, @data_type, @length, @precision, @scale  
WHILE @@FETCH_STATUS = 0 BEGIN  
  
 SET @spParameters = @spParameters + (CASE WHEN LEN(@spParameters) >0 THEN @nl + ' ,' ELSE '  ' END) + '@' + @column + ' ' + UPPER(@data_type) + (CASE @data_type WHEN 'VARCHAR' THEN '('+CAST(@length AS VARCHAR)+')' WHEN 'DECIMAL' THEN '('+CAST(@precision AS VARCHAR)+', '+CAST(@scale AS VARCHAR)+')' ELSE '' END) + ' = NULL'  
 SET @fieldList = @fieldList + (CASE WHEN LEN(@fieldList) >0 THEN @nl + '    ,' ELSE '' END) + @column  
 SET @spConditions = @spConditions + (CASE WHEN LEN(@spConditions) >0 THEN @nl + '   AND ' ELSE '' END) + '(@' + @column + ' IS NULL OR @' + @column + '=' + @column + ')'  
 SET @fieldSetList = @fieldSetList + (CASE WHEN LEN(@fieldSetList) >0 THEN @nl + '     ,' ELSE '      ' END) + @column + ' = @' + @column  
 SET @insertParameters = @insertParameters + (CASE WHEN LEN(@insertParameters) >0 THEN @nl + '    ,' ELSE '' END) + '@' + @column  
  
 FETCH NEXT FROM c INTO @table, @column, @data_type, @length, @precision, @scale  
END  
CLOSE c DEALLOCATE c  
  
--********************************  
--*********** SELECT *************  
--********************************  
DECLARE @SELECT AS VARCHAR(MAX)  
SET @SELECT = @spHeaders + @nl  
--SET @SELECT = @SELECT + ' if exists(select * from dbo.sysobjects where id = object_id([dbo].['+ @tableName + '_Select]) and OBJECTPROPERTY(id, IsProcedure) = 1) ' + @nl +
-- ' drop procedure [dbo].['+ @tableName + '_Select]' + @nl +
-- 'GO' + @nl
SET @SELECT = @SELECT + 'ALTER PROCEDURE ' + @tableName + '_Select' + @nl  
SET @SELECT = @SELECT + @spParameters + @nl  
SET @SELECT = @SELECT + 'AS' + @nl + ' SET NOCOUNT OFF;' + @nl + @nl  
SET @SELECT = @SELECT + '    SELECT ' + @fieldList + @nl  
SET @SELECT = @SELECT + '    FROM ' + @table + @nl  
SET @SELECT = @SELECt + '    WHERE ' + @spConditions + @nl  
  
--********************************  
--*********** UPDATE *************  
--********************************  
DECLARE @UPDATE AS VARCHAR(MAX)  
SET @UPDATE = @spHeaders + @nl  
SET @UPDATE = @UPDATE + 'ALTER PROCEDURE ' + @tableName + '_Update' + @nl  
SET @UPDATE = @UPDATE + @spParameters + @nl  
SET @UPDATE = @UPDATE + 'AS' + @nl + ' SET NOCOUNT OFF;' + @nl + @nl  
SET @UPDATE = @UPDATE + '    UPDATE ' + @table + ' SET ' + @nl  
SET @UPDATE = @UPDATE + @fieldSetList + @nl  
SET @UPDATE = @UPDATE + '    WHERE ' + @spConditions + @nl  
  
--********************************  
--*********** DELETE *************  
--********************************  
DECLARE @DELETE AS VARCHAR(MAX)  
SET @DELETE = @spHeaders + @nl  
SET @DELETE = @DELETE + 'ALTER PROCEDURE ' + @tableName + '_Delete' + @nl  
SET @DELETE = @DELETE + @spParameters + @nl  
SET @DELETE = @DELETE + 'AS' + @nl + ' SET NOCOUNT OFF;' + @nl + @nl  
SET @DELETE = @DELETE + '    DELETE FROM ' + @table + @nl  
SET @DELETE = @DELETE + '    WHERE ' + @spConditions + @nl  
  
--********************************  
--*********** INSERT *************  
--********************************  
DECLARE @INSERT AS VARCHAR(MAX)  
SET @INSERT = @spHeaders + @nl  
SET @INSERT = @INSERT + 'ALTER PROCEDURE ' + @tableName + '_Insert' + @nl  
SET @INSERT = @INSERT + @spParameters + @nl  
SET @INSERT = @INSERT + 'AS' + @nl + ' SET NOCOUNT OFF;' + @nl + @nl  
SET @INSERT = @INSERT + '    INSERT INTO ' + @table + '(' + @nl  
SET @INSERT = @INSERT + '     ' + @fieldList + @nl  
SET @INSERT = @INSERT + ' )' + @nl + ' VALUES(' + @nl + '     ' + @insertParameters + @nl  
SET @INSERT = @INSERT + ' )' + @nl  
  
--********************************  
--*********** EXISTS *************  
--********************************  
DECLARE @EXISTS AS VARCHAR(MAX)  
SET @EXISTS = @spHeaders + @nl  
SET @EXISTS = @EXISTS + 'ALTER PROCEDURE ' + @tableName + '_Exists' + @nl  
SET @EXISTS = @EXISTS + @spParameters + @nl  
SET @EXISTS = @EXISTS + ' ,@exists BIT OUT' + @nl  
SET @EXISTS = @EXISTS + 'AS' + @nl + ' SET NOCOUNT OFF;' + @nl + @nl  
SET @EXISTS = @EXISTS + '    IF EXISTS (' + @nl + ' SELECT ' + LEFT(@fieldList,CHARINDEX(@nl,@fieldList))  
SET @EXISTS = @EXISTS + '    FROM ' + @table + @nl  
SET @EXISTS = @EXISTS + '    WHERE ' + @spConditions + @nl + ' )' + @nl  
SET @EXISTS = @EXISTS + ' SET @exists = 1' + @nl + ' ELSE SET @exists = 0'  
  
--MOSTRAR GENERADOS  
PRINT + '-- =====INSERT==================================' + @nl + @INSERT  
PRINT + '-- =====DELETE==================================' + @nl + @DELETE  
PRINT + '-- =====UPDATE==================================' + @nl + @UPDATE  
PRINT + '-- =====SELECT==================================' + @nl + @SELECT  
PRINT + '-- =====EXISTS==================================' + @nl + @EXISTS

GO
/****** Object:  StoredProcedure [dbo].[Accesos_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 22/07/11
-- =============================================
CREATE PROCEDURE [dbo].[Accesos_Insert]
  @idAcceso NUMERIC = NULL
 ,@idPagina NUMERIC = NULL
 ,@fecha DATE = NULL
 ,@hora TIME = NULL
 ,@username VARCHAR(50) = NULL
 ,@url VARCHAR(250) = NULL
 ,@titulo VARCHAR(50) = NULL
AS
 SET NOCOUNT OFF;

SELECT @idPagina=idPagina
FROM [Paginas]
WHERE url = @url


IF (@idPagina IS NULL OR @idPagina = 0)
	BEGIN
		EXECUTE @idPagina = Paginas_Insert
			@idPagina
		   ,@titulo
		   ,@url
	END
ELSE
	BEGIN
		EXECUTE Paginas_Update
			@idPagina
		   ,@titulo
		   ,@url
	END
BEGIN
	INSERT INTO Accesos(
	 idPagina
	,fecha
	,hora
	,username
 )
 VALUES(
	 @idPagina
	,@fecha
	,@hora
	,@username
 )
 
 SET @idAcceso = @@IDENTITY
END

GO
/****** Object:  StoredProcedure [dbo].[Accesos_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 22/07/11
-- =============================================
CREATE PROCEDURE [dbo].[Accesos_Select]
  @idAcceso NUMERIC = NULL
 ,@idPagina NUMERIC = NULL
 ,@fecha DATE = NULL
 ,@hora TIME = NULL
 ,@username VARCHAR(50) = NULL
 ,@fechaDesde DATE = NULL
 ,@fechaHasta DATE  = NULL
AS
 SET NOCOUNT OFF;

    SELECT A.idAcceso
    ,A.idPagina
    ,A.fecha
    ,A.hora
    ,A.username
    ,P.titulo
    ,P.url
    FROM Accesos AS A INNER JOIN Paginas AS P ON A.idPagina = P.idPagina
    WHERE (@idAcceso IS NULL OR @idAcceso=A.idAcceso)
   AND (@idPagina IS NULL OR @idPagina=A.idPagina)
   AND (@fecha IS NULL OR @fecha=A.fecha)
   AND (@hora IS NULL OR @hora=A.hora)
   AND (@username IS NULL OR @username=A.username)
   AND (@fechaDesde IS NULL OR A.fecha >= @fechaDesde)
   AND (@fechaHasta IS NULL OR A.fecha <= @fechaHasta)

GO
/****** Object:  StoredProcedure [dbo].[AgendaActividades_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 01/08/11
-- =============================================
CREATE PROCEDURE [dbo].[AgendaActividades_Insert]
  @idCursoCicloLectivo NUMERIC = NULL
 ,@descripcion VARCHAR(250) = NULL
 ,@fechaCreacion DATE = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;

 INSERT INTO AgendaActividades(
     idCursoCicloLectivo
    ,descripcion
    ,fechaCreacion
    ,activo
 )
 VALUES(
     @idCursoCicloLectivo
    ,@descripcion
    ,@fechaCreacion
    ,@activo
 )

GO
/****** Object:  StoredProcedure [dbo].[AgendaActividades_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 07/08/11
-- =============================================
CREATE PROCEDURE [dbo].[AgendaActividades_Select]
  @idAgendaActividad NUMERIC = NULL
 ,@idCursoCicloLectivo NUMERIC = NULL
 ,@descripcion VARCHAR(250) = NULL
 ,@fechaCreacion DATE = NULL
 ,@activo BIT = NULL
 ,@idCurso NUMERIC = NULL
 ,@idCicloLectivo NUMERIC = NULL
 ,@usuario VARCHAR(50) = NULL
AS
 SET NOCOUNT OFF;

    SELECT DISTINCT
     A.idAgendaActividad
    ,A.idCursoCicloLectivo
    ,A.descripcion
    ,A.fechaCreacion
    ,A.activo
    ,CCL.idCurso
    ,CCL.idCicloLectivo
    ,N.nombre + ' ' + C.nombre AS 'curso'
    ,CL.nombre AS 'cicloLectivo'
    ,CL.fechaInicio
    ,CL.fechaFin
		,PP.username AS 'usuario'
    FROM AgendaActividades AS A 
		INNER JOIN CursosCicloLectivo AS CCL 
			ON A.idCursoCicloLectivo = CCL.idCursoCicloLectivo
		INNER JOIN Curso AS C
			ON CCL.idCurso = C.idCurso
		INNER JOIN Nivel AS N
			ON C.idNivel = N.idNivel
		INNER JOIN CicloLectivo AS CL
			ON CCL.idCicloLectivo = CL.idCicloLectivo
		INNER JOIN AsignaturaCicloLectivo AS AC ON AC.idCursoCicloLectivo = CCL.idCursoCicloLectivo
		INNER JOIN Personal AS P ON AC.idDocente = P.idPersonal
		INNER JOIN Personas AS PP ON P.idPersona = PP.idPersona
    WHERE (@idAgendaActividad IS NULL OR @idAgendaActividad=idAgendaActividad)
	   AND (@idCursoCicloLectivo IS NULL OR @idCursoCicloLectivo=A.idCursoCicloLectivo)
	   AND (@descripcion IS NULL OR @descripcion=descripcion)
	   AND (@fechaCreacion IS NULL OR @fechaCreacion=fechaCreacion)
	   AND (@activo IS NULL OR @activo=A.activo)
	   AND (@idCurso IS NULL OR @idCurso = CCL.idCursoCicloLectivo)
	   AND (@idCicloLectivo IS NULL OR @idCicloLectivo=CCL.idCicloLectivo)
	   AND (@usuario IS NULL OR @usuario = PP.username)
	ORDER BY cicloLectivo DESC,curso ASC

GO
/****** Object:  StoredProcedure [dbo].[AgendaActividades_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 07/08/11
-- =============================================
CREATE PROCEDURE [dbo].[AgendaActividades_Update]
  @idAgendaActividad NUMERIC = NULL
 ,@idCursoCicloLectivo NUMERIC = NULL
 ,@descripcion VARCHAR(250) = NULL
 ,@fechaCreacion DATE = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;

    UPDATE AgendaActividades SET 
      idCursoCicloLectivo = @idCursoCicloLectivo
     ,descripcion = @descripcion
     ,fechaCreacion = @fechaCreacion
     ,activo = @activo
    WHERE @idAgendaActividad=idAgendaActividad

GO
/****** Object:  StoredProcedure [dbo].[AlumnoCursoCicloLectivo_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 27/09/11
-- =============================================
CREATE PROCEDURE [dbo].[AlumnoCursoCicloLectivo_Insert]
  @idAlumnoCursoCicloLectivo NUMERIC = NULL
 ,@idAlumnoCursoCicloLectivoTransaccional NUMERIC = NULL
 ,@idAlumno NUMERIC = NULL
 ,@idCursoCicloLectivo NUMERIC = NULL
 
AS
 SET NOCOUNT OFF;

select @idAlumno=idAlumno
from Alumnos
where idAlumnoTransaccional = @idAlumno

select @idCursoCicloLectivo=idCursoCicloLectivo
from CursosCicloLectivo
where idCursoCicloLectivoTransaccional = @idCursoCicloLectivo

select @idAlumnoCursoCicloLectivo= idAlumnoCursoCicloLectivo
from AlumnoCursoCicloLectivo
where AlumnoCursoCicloLectivo.idAlumnoCursoCicloLectivoTransaccional= @idAlumnoCursoCicloLectivoTransaccional

IF (@idAlumnoCursoCicloLectivo IS NULL OR @idAlumnoCursoCicloLectivo = 0)
		BEGIN
			INSERT INTO AlumnoCursoCicloLectivo
           (idAlumnoCursoCicloLectivoTransaccional
           ,idAlumno
           ,idCursoCicloLectivo)
     VALUES
           (@idAlumnoCursoCicloLectivoTransaccional
           ,@idAlumno
           ,@idCursoCicloLectivo)
		SET @idAlumnoCursoCicloLectivo = @@IDENTITY
		END
	ELSE
	BEGIN
		EXECUTE AlumnoCursoCicloLectivo_Update
				 @idAlumnoCursoCicloLectivo
				,@idAlumnoCursoCicloLectivoTransaccional
				,@idAlumno
				,@idCursoCicloLectivo
					
	END

GO
/****** Object:  StoredProcedure [dbo].[AlumnoCursoCicloLectivo_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 23/07/11
-- =============================================
CREATE PROCEDURE [dbo].[AlumnoCursoCicloLectivo_Update]
  @idAlumnoCursoCicloLectivo NUMERIC = NULL
 ,@idAlumnoCursoCicloLectivoTransaccional NUMERIC = NULL
 ,@idAlumno NUMERIC = NULL
 ,@idCursoCicloLectivo NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    UPDATE AlumnoCursoCicloLectivo SET 
      idAlumnoCursoCicloLectivoTransaccional = @idAlumnoCursoCicloLectivoTransaccional
     ,idAlumno = @idAlumno
     ,idCursoCicloLectivo = @idCursoCicloLectivo
    WHERE @idAlumnoCursoCicloLectivo=idAlumnoCursoCicloLectivo

GO
/****** Object:  StoredProcedure [dbo].[Alumnos_Delete]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 12/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Alumnos_Delete]
  @idAlumno NUMERIC = NULL
 ,@idPersona NUMERIC = NULL
 ,@legajo VARCHAR(50) = NULL
 ,@fechaAlta DATE = NULL
 ,@fechaBaja DATE = NULL
AS
 SET NOCOUNT OFF;

    DELETE FROM Alumnos
    WHERE (@idAlumno IS NULL OR @idAlumno=idAlumno)
   AND (@idPersona IS NULL OR @idPersona=idPersona)
   AND (@legajo IS NULL OR @legajo=legajo)
   AND (@fechaAlta IS NULL OR @fechaAlta=fechaAlta)
   AND (@fechaBaja IS NULL OR @fechaBaja=fechaBaja)

-- =====UPDATE==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[Alumnos_Exists]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 12/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Alumnos_Exists]
  @idAlumno NUMERIC = NULL
 ,@idPersona NUMERIC = NULL
 ,@legajo VARCHAR(50) = NULL
 ,@fechaAlta DATE = NULL
 ,@fechaBaja DATE = NULL
 ,@exists BIT OUT
AS
 SET NOCOUNT OFF;

    IF EXISTS (
 SELECT idAlumno
    FROM Alumnos
    WHERE (@idAlumno IS NULL OR @idAlumno=idAlumno)
   AND (@idPersona IS NULL OR @idPersona=idPersona)
   AND (@legajo IS NULL OR @legajo=legajo)
   AND (@fechaAlta IS NULL OR @fechaAlta=fechaAlta)
   AND (@fechaBaja IS NULL OR @fechaBaja=fechaBaja)
 )
 SET @exists = 1
 ELSE SET @exists = 0

GO
/****** Object:  StoredProcedure [dbo].[Alumnos_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 12/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Alumnos_Insert]
  @idAlumno NUMERIC = NULL output
 ,@idAlumnoTransaccional NUMERIC = NULL
 ,@idPersona NUMERIC = NULL
 ,@legajo VARCHAR(50) = NULL
 ,@fechaAlta DATE = NULL
 ,@fechaBaja DATE = NULL
 ,@activo bit = null
AS
 SET NOCOUNT OFF;

  SELECT @idAlumno=idAlumno
  FROM [dbo].[Alumnos]
  WHERE [idPersona]= @idPersona
  
  SELECT @activo=activo
	FROM [dbo].[Personas]
	WHERE [idPersona]= @idPersona
	
	if (@activo = 0)
	BEGIN 
		SELECT @fechaBaja=GETDATE()
	END
	
IF (@idAlumno IS NULL OR @idAlumno=0)
  BEGIN
    INSERT INTO Alumnos(
     idAlumnoTransaccional
    ,idPersona
    ,legajo
    ,fechaAlta
    ,fechaBaja
 )
 VALUES(
     @idAlumnoTransaccional
    ,@idPersona
    ,@legajo
    ,GETDATE()
    ,@fechaBaja
 )
 SET @idAlumno = @@IDENTITY
END
ELSE
BEGIN
	EXECUTE Alumnos_Update
	 @idAlumno
	,@idAlumnoTransaccional
    ,@idPersona
    ,@legajo
    ,@fechaAlta
    ,@fechaBaja
END
   RETURN SCOPE_IDENTITY()


GO
/****** Object:  StoredProcedure [dbo].[Alumnos_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 12/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Alumnos_Select]
  @idAlumno NUMERIC = NULL
 ,@idPersona NUMERIC = NULL
 ,@legajo VARCHAR(50) = NULL
 ,@fechaAlta DATE = NULL
 ,@fechaBaja DATE = NULL
AS
 SET NOCOUNT OFF;

    SELECT idAlumno
    ,idPersona
    ,legajo
    ,fechaAlta
    ,fechaBaja
    FROM Alumnos
    WHERE (@idAlumno IS NULL OR @idAlumno=idAlumno)
   AND (@idPersona IS NULL OR @idPersona=idPersona)
   AND (@legajo IS NULL OR @legajo=legajo)
   AND (@fechaAlta IS NULL OR @fechaAlta=fechaAlta)
   AND (@fechaBaja IS NULL OR @fechaBaja=fechaBaja)

GO
/****** Object:  StoredProcedure [dbo].[Alumnos_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 12/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Alumnos_Update]
  @idAlumno NUMERIC = NULL
 ,@idAlumnoTransaccional NUMERIC = NULL
 ,@idPersona NUMERIC = NULL
 ,@legajo VARCHAR(50) = NULL
 ,@fechaAlta DATE = NULL
 ,@fechaBaja DATE = NULL
AS
 SET NOCOUNT OFF;

    UPDATE Alumnos SET 
	 idAlumnoTransaccional = @idAlumnoTransaccional
     ,idPersona = @idPersona
     ,legajo = @legajo
     ,fechaAlta = @fechaAlta
     ,fechaBaja = @fechaBaja
    WHERE @idAlumno=idAlumno

-- =====SELECT==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[AlumnosPorCurso_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 12/06/11
-- =============================================
CREATE PROCEDURE [dbo].[AlumnosPorCurso_Select]
  @idCurso NUMERIC = NULL
 ,@idCicloLectivo NUMERIC = NULL
 ,@idAlumno NUMERIC = NULL
 ,@idPersona NUMERIC = NULL
 ,@legajo VARCHAR(50) = NULL
 ,@fechaAlta DATE = NULL
 ,@fechaBaja DATE = NULL
 ,@username VARCHAR(50) = NULL
 ,@usernameTutor VARCHAR(50) = NULL
AS
 SET NOCOUNT OFF;

--Declare @idCicloLectivo numeric = null

--select @idCicloLectivo=idCicloLectivo
--from CicloLectivo
--where activo = 1

    SELECT distinct A.idAlumno
		,P.apellido
		,P.nombre
		,A.idPersona
		,A.legajo
		,A.fechaAlta
		,A.fechaBaja
		,P.activo
		,CCL.idCursoCicloLectivo
		,N.nombre AS 'nivel'
		,C.nombre AS 'curso'
    FROM Alumnos A
		INNER JOIN [AlumnoCursoCicloLectivo] AS AC 
			ON A.idAlumno = AC.idAlumno
		INNER JOIN [Personas] AS P 
			ON A.idPersona = P.idPersona
		INNER JOIN [CursosCicloLectivo] AS CCL 
			ON AC.idCursoCicloLectivo = CCL.idCursoCicloLectivo
		INNER JOIN TutorAlumno AS TA 
			ON A.idAlumno = TA.idAlumno
		INNER JOIN Tutor AS T 
			ON TA.idTutor = T.idTutor
		INNER JOIN Personas AS PT
			ON T.idPersona = PT.idPersona
		INNER JOIN Curso AS C
			ON CCL.idCurso = C.idCurso
		INNER JOIN Nivel AS N
			ON C.idNivel = N.idNivel
    WHERE (@idAlumno IS NULL OR @idAlumno=A.idAlumno)
		AND (@idPersona IS NULL OR @idPersona=A.idPersona)
		AND (@legajo IS NULL OR @legajo=A.legajo)
		AND (@fechaAlta IS NULL OR @fechaAlta=A.fechaAlta)
		AND (@fechaBaja IS NULL OR @fechaBaja=A.fechaBaja)
		AND (@idCurso IS NULL OR @idCurso = CCL.idCursoCicloLectivo)
		AND (@idCicloLectivo IS NULL OR @idCicloLectivo = CCL.idCicloLectivo)
		AND (@username IS NULL OR @username = P.username)
		AND (@usernameTutor IS NULL OR @usernameTutor = PT.username)

GO
/****** Object:  StoredProcedure [dbo].[AlumnosPorNivelCicloLectivo_select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 22/10/11
-- =============================================

CREATE PROCEDURE [dbo].[AlumnosPorNivelCicloLectivo_select]

  @idNivel INT = NULL
 ,@listaCicloLectivo VARCHAR(MAX) = NULL
	AS
		SET NOCOUNT OFF;

	SELECT DISTINCT A.idAlumno as 'idAlumno', PER.apellido+', '+PER.nombre as 'Nombre'
	FROM Nivel N
		INNER JOIN Curso AS C ON N.idNivel=C.idNivel
		INNER JOIN CursosCicloLectivo AS CCL ON C.idCurso=CCL.idCurso
		INNER JOIN AlumnoCursoCicloLectivo AS ACL ON CCL.idCursoCicloLectivo=ACL.idCursoCicloLectivo
		INNER JOIN Alumnos AS A ON ACL.idAlumno=A.idAlumno
		INNER JOIN Personas AS PER ON PER.idPersona=A.idPersona
	WHERE (@idNivel IS NULL OR N.idNivel = @idNivel)
	AND (@listaCicloLectivo IS NULL OR CCL.idCicloLectivo IN (SELECT * FROM [ArrayToTable] (@listaCicloLectivo)))

GO
/****** Object:  StoredProcedure [dbo].[AlumnosTutorPorCurso_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 12/06/11
-- =============================================
CREATE PROCEDURE [dbo].[AlumnosTutorPorCurso_Select]
  @idCurso NUMERIC = NULL
 ,@idAlumno NUMERIC = NULL
 ,@idPersona NUMERIC = NULL
 ,@legajo VARCHAR(50) = NULL
 ,@fechaAlta DATE = NULL
 ,@fechaBaja DATE = NULL
 ,@username VARCHAR(50) = NULL
AS
 SET NOCOUNT OFF;

--Declare @idCicloLectivo numeric = null

--select @idCicloLectivo=idCicloLectivo
--from CicloLectivo
--where activo = 1

    SELECT distinct A.idAlumno
		,P.apellido
		,P.nombre
		,A.idPersona
		,A.legajo
		,A.fechaAlta
		,A.fechaBaja
		,P.activo
		,CCL.idCursoCicloLectivo
    FROM Alumnos A
		INNER JOIN [AlumnoCursoCicloLectivo] AS AC 
			ON A.idAlumno = AC.idAlumno
		INNER JOIN [Personas] AS P 
			ON A.idPersona = P.idPersona
		INNER JOIN [CursosCicloLectivo] AS CCL 
			ON AC.idCursoCicloLectivo = CCL.idCursoCicloLectivo
    WHERE (@idAlumno IS NULL OR @idAlumno=A.idAlumno)
		AND (@idPersona IS NULL OR @idPersona=A.idPersona)
		AND (@legajo IS NULL OR @legajo=A.legajo)
		AND (@fechaAlta IS NULL OR @fechaAlta=A.fechaAlta)
		AND (@fechaBaja IS NULL OR @fechaBaja=A.fechaBaja)
		AND (@idCurso IS NULL OR @idCurso = CCL.idCursoCicloLectivo)
		--AND (@idCicloLectivo IS NULL OR @idCicloLectivo = CCL.idCicloLectivo)
		AND (@username IS NULL OR @username = P.username)

GO
/****** Object:  StoredProcedure [dbo].[AmbitoEncuesta_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AmbitoEncuesta_Select]
  @idAmbitoEncuesta NUMERIC = NULL
 ,@nombre VARCHAR(100) = NULL
 ,@descripcion VARCHAR(250) = NULL
AS
 SET NOCOUNT OFF;

    SELECT idAmbito
    ,nombre
    ,descripcion
    FROM AmbitoEncuesta
    WHERE (@idAmbitoEncuesta IS NULL OR @idAmbitoEncuesta=idAmbito)
		AND (@nombre IS NULL OR @nombre=nombre)
		AND (@descripcion IS NULL OR @descripcion=descripcion)

-- =====EXISTS==================================
SET ANSI_NULLS ON
GO
/****** Object:  StoredProcedure [dbo].[AmbitoRol_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 23/10/12
-- =============================================
CREATE PROCEDURE [dbo].[AmbitoRol_Select]
  @idAmbito NUMERIC = NULL
 ,@rolName VARCHAR(50) = NULL
AS
 SET NOCOUNT OFF;

    SELECT idAmbito
    ,rolName
    FROM RelAmbitoRol
    WHERE (@idAmbito IS NULL OR @idAmbito=idAmbito)
   AND (@rolName IS NULL OR @rolName=rolName)
GO
/****** Object:  StoredProcedure [dbo].[Asignatura_Delete]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 20/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Asignatura_Delete]
  @idAsignatura NUMERIC = NULL
 ,@idAsignaturaTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
AS
 SET NOCOUNT OFF;

    DELETE FROM Asignatura
    WHERE (@idAsignatura IS NULL OR @idAsignatura=idAsignatura)
   

-- =====UPDATE==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[Asignatura_Exists]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 20/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Asignatura_Exists]
  @idAsignatura NUMERIC = NULL
 ,@idAsignaturaTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@exists BIT OUT
AS
 SET NOCOUNT OFF;

    IF EXISTS (
 SELECT idAsignatura
    FROM Asignatura
    WHERE (@idAsignatura IS NULL OR @idAsignatura=idAsignatura)
   AND (@idAsignaturaTransaccional IS NULL OR @idAsignaturaTransaccional=idAsignaturaTransaccional)
   AND (@nombre IS NULL OR @nombre=nombre)
 )
 SET @exists = 1
 ELSE SET @exists = 0

GO
/****** Object:  StoredProcedure [dbo].[Asignatura_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 20/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Asignatura_Insert]
  @idAsignatura NUMERIC = NULL output
 ,@idAsignaturaTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(80) = NULL
AS
 SET NOCOUNT OFF;

SELECT @idAsignatura=idAsignatura 
  FROM [Asignatura]
  WHERE [idAsignaturaTransaccional]= @idAsignaturaTransaccional
  
IF (@idAsignatura is null or @idAsignatura=0)
  BEGIN
    INSERT INTO Asignatura(
     idAsignaturaTransaccional
    ,nombre
 )
 VALUES(
     @idAsignaturaTransaccional
    ,@nombre
 )
 SET @idAsignatura = @@IDENTITY
end
ELSE
BEGIN
	Execute Asignatura_Update
		  @idAsignatura
		 ,@idAsignaturaTransaccional
		 ,@nombre
end

GO
/****** Object:  StoredProcedure [dbo].[Asignatura_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 20/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Asignatura_Select]
  @idAsignatura NUMERIC = NULL
 ,@idAsignaturaTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
AS
 SET NOCOUNT OFF;

    SELECT idAsignatura
    ,idAsignaturaTransaccional
    ,nombre
    FROM Asignatura
    WHERE (@idAsignatura IS NULL OR @idAsignatura=idAsignatura)
	   AND (@idAsignaturaTransaccional IS NULL OR @idAsignaturaTransaccional=idAsignaturaTransaccional)
	   AND (@nombre IS NULL OR @nombre=nombre)

ORDER BY nombre ASC

GO
/****** Object:  StoredProcedure [dbo].[Asignatura_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 20/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Asignatura_Update]
  @idAsignatura NUMERIC = NULL
 ,@idAsignaturaTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(80) = NULL
AS
 SET NOCOUNT OFF;

    UPDATE Asignatura SET 
      idAsignaturaTransaccional = @idAsignaturaTransaccional
     ,nombre = @nombre
    WHERE (@idAsignatura IS NULL OR @idAsignatura=idAsignatura)

GO
/****** Object:  StoredProcedure [dbo].[AsignaturaCicloLectivo_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 22/06/11
-- =============================================
CREATE PROCEDURE [dbo].[AsignaturaCicloLectivo_Insert]
  @idAsignaturaCicloLectivo NUMERIC = NULL
 ,@idAsignaturaCicloLectivoTransaccional NUMERIC = NULL
 ,@idAsignatura NUMERIC = NULL
 ,@idDocente NUMERIC = NULL
 ,@idCursoCicloLectivo NUMERIC = NULL
 ,@idHorario NUMERIC = NULL
AS
 SET NOCOUNT OFF;

select @idCursoCicloLectivo=idCursoCicloLectivo
from CursosCicloLectivo
where idCursoCicloLectivoTransaccional=@idCursoCicloLectivo
	
SELECT @idDocente=PP.idPersonal
from Personas  as P inner join Personal as PP on P.idPersona = PP.idPersona
where PP.idPersonalTransaccional = @idDocente
	
select @idAsignatura = idAsignatura
from Asignatura
where Asignatura.idAsignaturaTransaccional = @idAsignatura	
	
select @idAsignaturaCicloLectivo=idAsignaturaCicloLectivo
from AsignaturaCicloLectivo
where idAsignaturaCicloLectivoTransaccional=@idAsignaturaCicloLectivoTransaccional

IF (@idAsignaturaCicloLectivo IS NULL OR @idAsignaturaCicloLectivo = 0)
		BEGIN
			INSERT INTO [AsignaturaCicloLectivo]
           ([idAsignaturaCicloLectivoTransaccional]
           ,[idAsignatura]
           ,[idDocente]
           ,[idCursoCicloLectivo]
           ,[idHorario])
		 
		 VALUES(
			@idAsignaturaCicloLectivoTransaccional
           ,@idAsignatura
           ,@idDocente
           ,@idCursoCicloLectivo
           ,@idHorario)
		SET @idAsignaturaCicloLectivo = @@IDENTITY
		 
		END

	ELSE
	BEGIN
		EXECUTE AsignaturaCicloLectivo_Update
			 @idAsignaturaCicloLectivo
			,@idAsignaturaCicloLectivoTransaccional
			,@idAsignatura
			,@idDocente
			,@idCursoCicloLectivo
			,@idHorario
	END

GO
/****** Object:  StoredProcedure [dbo].[AsignaturaCicloLectivo_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 22/06/11
-- =============================================
CREATE PROCEDURE [dbo].[AsignaturaCicloLectivo_Select]
  @idAsignaturaCurso NUMERIC = NULL
 ,@idCursoCicloLectivo NUMERIC = NULL
 ,@idAsignatura NUMERIC = NULL
 ,@idDocente NUMERIC = NULL
 --,@idOrientacion NUMERIC = NULL
 ,@idCicloLectivo NUMERIC = NULL
 ,@usuario VARCHAR(50) = NULL
AS
 SET NOCOUNT OFF;

--IF @idCicloLectivo IS NULL
--BEGIN
--	SELECT @idCicloLectivo = idCicloLectivo
--	FROM CicloLectivo Where activo = 1
--END 

SELECT DISTINCT
	 idAsignaturaCicloLectivo
    ,AC.idCursoCicloLectivo
    ,AC.idAsignatura
    ,idDocente
    --,AC.idOrientacion
    --,AC.idCicloLectivo
    ,A.nombre AS 'nombreAsignatura'
    ,N.nombre + ' ' + C.nombre  AS 'nombreCurso'
    ,PP.nombre as 'nombreDocente'
    ,PP.apellido as 'apellidoDocente'
    ,PP.idPersona
    FROM AsignaturaCicloLectivo AS AC 
		INNER JOIN CursosCicloLectivo as CCL ON AC.idCursoCicloLectivo = CCL.idCursoCicloLectivo
		INNER JOIN Asignatura AS A ON AC.idAsignatura = A.idAsignatura
		INNER JOIN Curso AS C ON CCL.idCurso = C.idCurso
		INNER JOIN Nivel AS N ON N.idNivel = C.idNivel
		INNER JOIN CicloLectivo AS CL ON CCL.idCicloLectivo = CL.idCicloLectivo
		INNER JOIN Personal AS P ON AC.idDocente = P.idPersonal
		INNER JOIN Personas AS PP ON P.idPersona = PP.idPersona
		--INNER JOIN Orientacion AS O ON C.idOrientacion = O.idOrientacion
    WHERE 
    (@idAsignaturaCurso IS NULL OR @idAsignaturaCurso=idAsignaturaCicloLectivo)
   AND (@idCursoCicloLectivo IS NULL OR @idCursoCicloLectivo=AC.idCursoCicloLectivo)
   AND (@idAsignatura IS NULL OR @idAsignatura=AC.idAsignatura)
   AND (@idDocente IS NULL OR @idDocente=idDocente)
   --AND (@idOrientacion IS NULL OR @idOrientacion=AC.idOrientacion)
   AND (@idCicloLectivo IS NULL OR @idCicloLectivo=CCL.idCicloLectivo)
   AND (@usuario IS NULL OR @usuario = PP.username)
	ORDER BY nombreAsignatura ASC

GO
/****** Object:  StoredProcedure [dbo].[AsignaturaCicloLectivo_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 10/10/11
-- =============================================
CREATE PROCEDURE [dbo].[AsignaturaCicloLectivo_Update]
  @idAsignaturaCicloLectivo NUMERIC = NULL
 ,@idAsignaturaCicloLectivoTransaccional NUMERIC = NULL
 ,@idAsignatura NUMERIC = NULL
 ,@idDocente NUMERIC = NULL
 ,@idCursoCicloLectivo NUMERIC = NULL
 ,@idHorario NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    UPDATE AsignaturaCicloLectivo SET 
      idAsignaturaCicloLectivoTransaccional = @idAsignaturaCicloLectivoTransaccional
     ,idAsignatura = @idAsignatura
     ,idDocente = @idDocente
     ,idCursoCicloLectivo = @idCursoCicloLectivo
     ,idHorario = @idHorario
    WHERE (@idAsignaturaCicloLectivo IS NULL OR @idAsignaturaCicloLectivo=idAsignaturaCicloLectivo)

GO
/****** Object:  StoredProcedure [dbo].[AsignaturaCurso_Delete]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 22/06/11
-- =============================================
CREATE PROCEDURE [dbo].[AsignaturaCurso_Delete]
  @idAsignaturaCurso NUMERIC = NULL
 ,@idCurso NUMERIC = NULL
 ,@idAsignatura NUMERIC = NULL
 ,@idDocente NUMERIC = NULL
 ,@idOrientacion NUMERIC = NULL
 ,@idCicloLectivo NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    DELETE FROM AsignaturaCurso
    WHERE (@idAsignaturaCurso IS NULL OR @idAsignaturaCurso=idAsignaturaCurso)
   
-- =====UPDATE==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[AsignaturaCurso_Exists]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 22/06/11
-- =============================================
CREATE PROCEDURE [dbo].[AsignaturaCurso_Exists]
  @idAsignaturaCurso NUMERIC = NULL
 ,@idCurso NUMERIC = NULL
 ,@idAsignatura NUMERIC = NULL
 ,@idDocente NUMERIC = NULL
 ,@idOrientacion NUMERIC = NULL
 ,@idCicloLectivo NUMERIC = NULL
 ,@exists BIT OUT
AS
 SET NOCOUNT OFF;

    IF EXISTS (
 SELECT idAsignaturaCurso
    FROM AsignaturaCurso
    WHERE (@idAsignaturaCurso IS NULL OR @idAsignaturaCurso=idAsignaturaCurso)
   AND (@idCurso IS NULL OR @idCurso=idCurso)
   AND (@idAsignatura IS NULL OR @idAsignatura=idAsignatura)
   AND (@idDocente IS NULL OR @idDocente=idDocente)
   AND (@idOrientacion IS NULL OR @idOrientacion=idOrientacion)
   AND (@idCicloLectivo IS NULL OR @idCicloLectivo=idCicloLectivo)
 )
 SET @exists = 1
 ELSE SET @exists = 0

GO
/****** Object:  StoredProcedure [dbo].[AsignaturaCurso_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 22/06/11
-- =============================================
CREATE PROCEDURE [dbo].[AsignaturaCurso_Select]
  @idAsignaturaCurso NUMERIC = NULL
 ,@idCurso NUMERIC = NULL
 ,@idAsignatura NUMERIC = NULL
 ,@idDocente NUMERIC = NULL
 ,@idOrientacion NUMERIC = NULL
 ,@idCicloLectivo NUMERIC = NULL
 ,@usuario VARCHAR(50) = NULL
AS
 SET NOCOUNT OFF;

IF @idCicloLectivo IS NULL
BEGIN
	SELECT @idCicloLectivo = idCicloLectivo
	FROM CicloLectivo Where activo = 1
END 

SELECT DISTINCT
	 idAsignaturaCurso
    ,AC.idCurso
    ,AC.idAsignatura
    ,idDocente
    ,AC.idOrientacion
    ,AC.idCicloLectivo
    ,A.nombre AS 'nombreAsignatura'
    ,N.nombre + ' ' + C.nombre  AS 'nombreCurso'
    ,PP.nombre as 'nombreDocente'
    ,PP.apellido as 'apellidoDocente'
    ,PP.idPersona
    FROM AsignaturaCurso AS AC 
		INNER JOIN Asignatura AS A ON AC.idAsignatura = A.idAsignatura
		INNER JOIN Curso AS C ON AC.idCurso = C.idCurso
		INNER JOIN Nivel AS N ON N.idNivel = C.idNivel
		INNER JOIN CicloLectivo AS CL ON AC.idCicloLectivo = CL.idCicloLectivo
		INNER JOIN Personal AS P ON AC.idDocente = P.idPersonal
		INNER JOIN Personas AS PP ON P.idPersona = PP.idPersona
		INNER JOIN Orientacion AS O ON AC.idOrientacion = O.idOrientacion
    WHERE (@idAsignaturaCurso IS NULL OR @idAsignaturaCurso=idAsignaturaCurso)
   AND (@idCurso IS NULL OR @idCurso=AC.idCurso)
   AND (@idAsignatura IS NULL OR @idAsignatura=AC.idAsignatura)
   AND (@idDocente IS NULL OR @idDocente=idDocente)
   AND (@idOrientacion IS NULL OR @idOrientacion=AC.idOrientacion)
   AND (@idCicloLectivo IS NULL OR @idCicloLectivo=AC.idCicloLectivo)
   AND (@usuario IS NULL OR @usuario = PP.username)
	ORDER BY nombreAsignatura ASC

GO
/****** Object:  StoredProcedure [dbo].[AsignaturaCurso_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 22/06/11
-- =============================================
CREATE PROCEDURE [dbo].[AsignaturaCurso_Update]
  @idAsignaturaCurso NUMERIC = NULL
 ,@idCurso NUMERIC = NULL
 ,@idAsignatura NUMERIC = NULL
 ,@idDocente NUMERIC = NULL
 ,@idOrientacion NUMERIC = NULL
 ,@idCicloLectivo NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    UPDATE AsignaturaCurso SET 
      idCurso = @idCurso
     ,idAsignatura = @idAsignatura
     ,idDocente = @idDocente
     ,idOrientacion = @idOrientacion
     ,idCicloLectivo = @idCicloLectivo
    WHERE (@idAsignaturaCurso IS NULL OR @idAsignaturaCurso=idAsignaturaCurso)

GO
/****** Object:  StoredProcedure [dbo].[AsignaturaNivel_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AsignaturaNivel_Insert]
  @idAsignaturaNivel NUMERIC = NULL
 ,@idAsignaturaNivelTransaccional NUMERIC = NULL
 ,@idAsignatura NUMERIC = NULL
 ,@idNivel NUMERIC = NULL
 ,@idOrientacion NUMERIC = NULL
 ,@cargaHoraria NCHAR = NULL
AS
 SET NOCOUNT off;
 
 	select @idAsignaturaNivel=idAsignaturaNivel
 from AsignaturaNivel
 where idAsignaturaNivelTransaccional=@idAsignaturaNivelTransaccional

	select @idAsignatura=idAsignatura
 from Asignatura
 where idAsignaturaTransaccional = @idAsignatura

	select @idNivel=idNivel
 from Nivel
 where idNivelTransaccional = @idNivel
 
	select @idOrientacion = idOrientacion
 from Orientacion
 where idOrientacionTransaccional = @idOrientacion
			
if (@idAsignaturaNivel is null or @idAsignaturaNivel=0)
	begin
		INSERT INTO [AsignaturaNivel] (
			 idAsignaturaNivelTransaccional
			,idAsignatura
			,idNivel
			,idOrientacion
			,cargaHoraria
			)
			VALUES(
			 @idAsignaturaNivelTransaccional
			,@idAsignatura
			,@idNivel
			,@idOrientacion
			,@cargaHoraria
			)
		
		SET @idAsignaturaNivel = @@IDENTITY
	 end
 else
	 begin
		 EXECUTE [AsignaturaNivel_Update] 
		   @idAsignaturaNivel
		  ,@idAsignaturaNivelTransaccional
		  ,@idAsignatura
		  ,@idNivel
		  ,@idOrientacion
		  ,@cargaHoraria
	 end

GO
/****** Object:  StoredProcedure [dbo].[AsignaturaNivel_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 22/03/13
-- =============================================
CREATE PROCEDURE [dbo].[AsignaturaNivel_Select]
  @idAsignaturaNivel NUMERIC = NULL
 ,@idCicloLectivo NUMERIC = NULL
 ,@idAsignaturaNivelTransaccional NUMERIC = NULL
 ,@idAsignatura NUMERIC = NULL
 ,@idNivel NUMERIC = NULL
 ,@idOrientacion NUMERIC = NULL
 ,@cargaHoraria NCHAR = NULL
 ,@docente VARCHAR(50) = NULL
AS
 SET NOCOUNT OFF;



    SELECT distinct idAsignaturaNivel
    ,idAsignaturaNivelTransaccional
    ,AsignaturaNivel.idAsignatura
	,Asignatura.nombre AS 'Asignatura'
    ,AsignaturaNivel.idNivel
	,Nivel.nombre AS 'Nivel'
    ,AsignaturaNivel.idOrientacion
	,Orientacion.nombre as 'Orientacion'
    ,cargaHoraria
      FROM AsignaturaNivel
		INNER JOIN Asignatura ON AsignaturaNivel.idAsignatura = Asignatura.idAsignatura
		INNER JOIN Nivel ON AsignaturaNivel.idNivel = Nivel.idNivel
		INNER JOIN Orientacion ON Orientacion.idOrientacion = AsignaturaNivel.idOrientacion
		INNER JOIN AsignaturaCicloLectivo as ACL 
			ON AsignaturaNivel.idAsignatura = ACL.idAsignatura 
		INNER JOIN CursosCicloLectivo AS CCL ON ACL.idCursoCicloLectivo = CCL.idCursoCicloLectivo
		INNER JOIN CicloLectivo AS CL ON CCL.idCicloLectivo = CL.idCicloLectivo AND CL.activo = 1
		INNER JOIN Curso AS C ON CCL.idCurso = C.idCurso
			AND C.idNivel = AsignaturaNivel.idNivel
		INNER JOIN Personal AS P ON ACL.idDocente = P.idPersonal
		INNER JOIN Personas AS PP ON P.idPersona = PP.idPersona
    WHERE (@idAsignaturaNivel IS NULL OR @idAsignaturaNivel=idAsignaturaNivel)
   AND (@idAsignaturaNivelTransaccional IS NULL OR @idAsignaturaNivelTransaccional=idAsignaturaNivelTransaccional)
   AND (@idAsignatura IS NULL OR @idAsignatura=AsignaturaNivel.idAsignatura)
   AND (@idOrientacion IS NULL OR @idOrientacion=AsignaturaNivel.idOrientacion)
   AND (@cargaHoraria IS NULL OR @cargaHoraria=cargaHoraria)
   AND (@idNivel IS NULL OR @idNivel= Nivel.idNivel)
   AND (@docente IS NULL OR @docente = PP.username)
   AND (@idCicloLectivo IS NULL OR CCL.idCicloLectivo = @idCicloLectivo)
GO
/****** Object:  StoredProcedure [dbo].[AsignaturaNivel_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 22/03/13
-- =============================================
CREATE PROCEDURE [dbo].[AsignaturaNivel_Update]
  @idAsignaturaNivel NUMERIC = NULL
 ,@idAsignaturaNivelTransaccional NUMERIC = NULL
 ,@idAsignatura NUMERIC = NULL
 ,@idNivel NUMERIC = NULL
 ,@idOrientacion NUMERIC = NULL
 ,@cargaHoraria NCHAR = NULL
AS
 SET NOCOUNT OFF;

    UPDATE AsignaturaNivel SET 
      idAsignaturaNivelTransaccional = @idAsignaturaNivelTransaccional
     ,idAsignatura = @idAsignatura
     ,idNivel = @idNivel
     ,idOrientacion = @idOrientacion
     ,cargaHoraria = @cargaHoraria
    WHERE @idAsignaturaNivel=idAsignaturaNivel
  
GO
/****** Object:  StoredProcedure [dbo].[AsignaturasPorNivelCicloLectivo_select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 22/10/11
-- =============================================

CREATE PROCEDURE [dbo].[AsignaturasPorNivelCicloLectivo_select]

  @idNivel NUMERIC = NULL
 ,@listaCicloLectivo VARCHAR(MAX) = NULL
	AS
		SET NOCOUNT OFF;

	SELECT DISTINCT ACL.idAsignatura, A.nombre as 'nombreAsignatura'
	FROM Nivel N
		INNER JOIN Curso AS C ON N.idNivel=C.idNivel
		INNER JOIN CursosCicloLectivo AS CCL ON C.idCurso=CCL.idCurso
		INNER JOIN AsignaturaCicloLectivo AS ACL ON CCL.idCursoCicloLectivo=ACL.idCursoCicloLectivo
		INNER JOIN Asignatura AS A ON ACL.idAsignatura=A.idAsignatura
	WHERE (@idNivel IS NULL OR @idNivel=N.idNivel)
	AND (@listaCicloLectivo IS NULL OR CCL.idCicloLectivo IN (SELECT * FROM [ArrayToTable] (@listaCicloLectivo)))

GO
/****** Object:  StoredProcedure [dbo].[Asistencia_Delete]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 26/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Asistencia_Delete]
  @idAsistencia NUMERIC = NULL
 ,@idAsistenciaTransaccional NUMERIC = NULL
 ,@idAlumno INT = NULL
 ,@idTipoAsistencia INT = NULL
 ,@fecha DATETIME = NULL
AS
 SET NOCOUNT OFF;

    DELETE FROM Asistencia
    WHERE (@idAsistencia IS NULL OR @idAsistencia=idAsistencia)
  
-- =====UPDATE==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[Asistencia_Exists]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 26/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Asistencia_Exists]
  @idAsistencia NUMERIC = NULL
 ,@idAsistenciaTransaccional NUMERIC = NULL
 ,@idAlumno INT = NULL
 ,@idTipoAsistencia INT = NULL
 ,@fecha DATETIME = NULL
 ,@exists BIT OUT
AS
 SET NOCOUNT OFF;

    IF EXISTS (
 SELECT idAsistencia
    FROM Asistencia
    WHERE (@idAsistencia IS NULL OR @idAsistencia=idAsistencia)
  )
 SET @exists = 1
 ELSE SET @exists = 0



-- =====INSERT==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[Asistencia_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 26/06/11
-- =============================================

CREATE PROCEDURE [dbo].[Asistencia_Insert]
  @idAsistencia NUMERIC = NULL
 ,@idAsistenciaTransaccional NUMERIC = NULL
 ,@idAlumno INT = NULL
 ,@idTipoAsistencia INT = NULL
 ,@fecha DATETIME = NULL
AS
 SET NOCOUNT OFF;

SELECT @idAlumno = alu.idAlumnoCursoCicloLectivo
FROM AlumnoCursoCicloLectivo ALU INNER JOIN Alumnos as A on alu.idAlumno = A.idAlumno
WHERE ALU.idAlumnoCursoCicloLectivoTransaccional=@idAlumno
--WHERE @idAlumno = A.idAlumnoTransaccional

SELECT @idTipoAsistencia = idTipoAsistencia
FROM TipoAsistencia
WHERE idTipoAsistenciaTransaccional = @idTipoAsistencia

SELECT @idAsistencia = idAsistencia 
FROM Asistencia
WHERE idAsistenciaTransaccional = @idAsistenciaTransaccional

IF (@idAsistencia is NULL OR @idAsistencia = 0)
BEGIN
		INSERT INTO Asistencia(
		 idAsistenciaTransaccional
		,idAlumnoCursoCicloLectivo
		,idTipoAsistencia
		,fecha
		,fechaCarga
		 )
		 VALUES(
			 @idAsistenciaTransaccional
			,@idAlumno
			,@idTipoAsistencia
			,@fecha
			,GETDATE()
		 )
	 SET @idAsistencia = @@IDENTITY
 END
 ELSE 
	 BEGIN
		EXECUTE  Asistencia_Update
		  @idAsistencia 
		 ,@idAsistenciaTransaccional 
		 ,@idAlumno 
		 ,@idTipoAsistencia 
		 ,@fecha 
	END

GO
/****** Object:  StoredProcedure [dbo].[Asistencia_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 26/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Asistencia_Select]
  @idAsistencia NUMERIC = NULL
 ,@idAsistenciaTransaccional NUMERIC = NULL
 ,@idAlumno INT = NULL
 ,@idTipoAsistencia INT = NULL
 ,@fecha DATETIME = NULL
AS
 SET NOCOUNT OFF;

    SELECT idAsistencia
    ,idAsistenciaTransaccional
    ,idAlumnoCursoCicloLectivo
    ,idTipoAsistencia
    ,fecha
    FROM Asistencia
    WHERE (@idAsistencia IS NULL OR @idAsistencia=idAsistencia)
 
/****** Object:  StoredProcedure [dbo].[CicloLectivo_Insert]    Script Date: 10/23/2011 19:04:50 ******/
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[Asistencia_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 26/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Asistencia_Update]
  @idAsistencia NUMERIC = NULL
 ,@idAsistenciaTransaccional NUMERIC = NULL
 ,@idAlumno INT = NULL
 ,@idTipoAsistencia INT = NULL
 ,@fecha DATETIME = NULL
AS
 SET NOCOUNT OFF;

    UPDATE Asistencia SET 
      idAsistenciaTransaccional = @idAsistenciaTransaccional
     ,idAlumnoCursoCicloLectivo = @idAlumno
     ,idTipoAsistencia = @idTipoAsistencia
     ,fecha = @fecha
    WHERE (@idAsistencia IS NULL OR @idAsistencia=idAsistencia)

GO
/****** Object:  StoredProcedure [dbo].[AsistenciaInformeMail_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 25/09/11
-- =============================================
CREATE PROCEDURE [dbo].[AsistenciaInformeMail_Select]
  @fechaCarga DATE = NULL
 ,@idProcesoAutomatico INT = NULL
AS
 SET NOCOUNT OFF;

SELECT @fechaCarga= isnull(MAX(fechaEjecucion),null)
FROM ProcesosEjecutados
WHERE idProcesoAutomatico = @idProcesoAutomatico and resultado=1

	SELECT DISTINCT
		 A.fecha
		,P.nombre as 'nombreAlumno'
		,P.apellido as 'apellidoAlumno'
		,PT.nombre as 'nombreTutor'
		,PT.apellido as 'apellidoTutor'
		,PT.email
		,TP.descripcion
		,PT.idPersona AS 'idPersonaTutor'
		,P.idPersona as 'idPersonaAlumno'
    FROM 
		Asistencia AS A 
		INNER JOIN AlumnoCursoCicloLectivo AS ACCL ON A.idAlumnoCursoCicloLectivo = ACCL.idAlumnoCursoCicloLectivo
		INNER JOIN Alumnos AS AL ON ACCL.idAlumno = AL.idAlumno
		INNER JOIN Personas AS P ON AL.idPersona = P.idPersona
		INNER JOIN TutorAlumno AS TA ON AL.idAlumno = TA.idAlumno
		INNER JOIN TipoAsistencia AS TP ON A.idTipoAsistencia = TP.idTipoAsistencia
		LEFT JOIN Tutor AS T ON TA.idTutor = T.idTutor
		LEFT JOIN Personas AS PT ON T.idPersona = PT.idPersona
	WHERE 
		A.idTipoAsistencia <> 553
		AND
		PT.email <> ''
		AND
	    (@fechaCarga IS NULL OR @fechaCarga=fechaCarga)
   	    AND A.fecha BETWEEN 
			(SELECT fechaInicio from CicloLectivo where activo = 1) 
			and 
			(SELECT fechaFin from CicloLectivo where activo = 1) 
	ORDER BY PT.idPersona


GO
/****** Object:  StoredProcedure [dbo].[AsistenciaInformeSMS_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:  Martin Herran
-- Create date: 25/10/2012
-- =============================================
CREATE PROCEDURE [dbo].[AsistenciaInformeSMS_Select]
  @fechaCarga DATE = NULL
 ,@idProcesoAutomatico INT = NULL
AS
 SET NOCOUNT OFF;

SELECT @fechaCarga= isnull(MAX(fechaEjecucion),null)
FROM ProcesosEjecutados
WHERE idProcesoAutomatico = @idProcesoAutomatico and resultado=1

	SELECT DISTINCT
		 A.fecha
		,P.nombre as 'nombreAlumno'
		,P.apellido as 'apellidoAlumno'
		,PT.nombre as 'nombreTutor'
		,PT.apellido as 'apellidoTutor'
		,PT.telefonoCelular
		,TP.descripcion
		,PT.idPersona AS 'idPersonaTutor'
		,P.idPersona as 'idPersonaAlumno'
    FROM 
		Asistencia AS A 
		INNER JOIN AlumnoCursoCicloLectivo AS ACCL ON A.idAlumnoCursoCicloLectivo = ACCL.idAlumnoCursoCicloLectivo
		INNER JOIN Alumnos AS AL ON ACCL.idAlumno = AL.idAlumno
		INNER JOIN Personas AS P ON AL.idPersona = P.idPersona
		INNER JOIN TutorAlumno AS TA ON AL.idAlumno = TA.idAlumno
		INNER JOIN TipoAsistencia AS TP ON A.idTipoAsistencia = TP.idTipoAsistencia
		LEFT JOIN Tutor AS T ON TA.idTutor = T.idTutor
		LEFT JOIN Personas AS PT ON T.idPersona = PT.idPersona
	WHERE 
		A.idTipoAsistencia <> 13
		AND
		PT.telefonoCelular <> ''
   	    AND A.fecha BETWEEN 
		(SELECT dateadd(d,-14,GETDATE())) 
		AND 
		(SELECT GETDATE())  
			ORDER BY PT.idPersona

GO
/****** Object:  StoredProcedure [dbo].[Baja_Usuarios]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo
-- Create date: 14/04/2013
-- =============================================
CREATE PROCEDURE [dbo].[Baja_Usuarios]

AS
 SET NOCOUNT OFF;

--REALIZO UNA LIMPIEZA DE AQUELLOS TUTORES QUE ESTAN ACTIVOS PERO NO TIENEN ALUMNOS HABILITADOS EN EL CICLO LECTIVO ACTUAL
	UPDATE Personas
	SET activo = 0
	WHERE idPersona IN
	(
		SELECT DISTINCT P.idPersona
			FROM TutorAlumno AS TA
			INNER JOIN Tutor AS T ON TA.idTutor = T.idTutor
			INNER JOIN Personas AS P ON T.idPersona = P.idPersona
			WHERE TA.idAlumno NOT IN 
				(	SELECT A.idAlumno
					FROM TutorAlumno AS TA
					INNER JOIN Alumnos AS A ON A.idAlumno = TA.idAlumno
					INNER JOIN Personas AS P ON A.idPersona = P.idPersona AND P.activo=1
				)
			AND P.activo = 1
	)

--EFECTÚO LA BAJA DEL SISTEMA
UPDATE EDUAR_aspnet_services.dbo.aspnet_Membership
SET IsApproved = 0
WHERE UserId IN
(
	SELECT U.UserId
	FROM EDUAR_aspnet_services.dbo.aspnet_Membership AS M
	INNER JOIN EDUAR_aspnet_services.dbo.aspnet_Users AS U ON M.UserId = U.UserId 
	INNER JOIN Personas AS P ON U.UserName = P.username AND P.activo = 0
	WHERE M.IsApproved=1
)

-- =====SELECT==================================
SET ANSI_NULLS ON
GO
/****** Object:  StoredProcedure [dbo].[Calificacion_Delete]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 26/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Calificacion_Delete]
  @idCalificacion NUMERIC = NULL
 ,@idCalificacionTransaccional NUMERIC = NULL
 ,@observaciones NVARCHAR = NULL
 ,@fecha DATE = NULL
 ,@idValorEscalaCalificacion NUMERIC = NULL
 ,@idAlumno NUMERIC = NULL
 ,@idAsignatura NUMERIC = NULL
 ,@idPeriodo NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    DELETE FROM Calificacion
    WHERE (@idCalificacion IS NULL OR @idCalificacion=idCalificacion)

GO
/****** Object:  StoredProcedure [dbo].[Calificacion_Exists]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 26/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Calificacion_Exists]
  @idCalificacion NUMERIC = NULL
 ,@idCalificacionTransaccional NUMERIC = NULL
 ,@observaciones NVARCHAR = NULL
 ,@fecha DATE = NULL
 ,@idValorCalificacion NUMERIC = NULL
 ,@idAlumno NUMERIC = NULL
 ,@idPeriodo NUMERIC = NULL
 ,@exists BIT OUT
AS
 SET NOCOUNT OFF;

    IF EXISTS (
 SELECT idCalificacion
    FROM Calificacion
    WHERE (@idCalificacion IS NULL OR @idCalificacion=idCalificacion)
   AND (@idCalificacionTransaccional IS NULL OR @idCalificacionTransaccional=idCalificacionTransaccional)
   AND (@observaciones IS NULL OR @observaciones=observaciones)
   AND (@fecha IS NULL OR @fecha=fecha)
   AND (@idValorCalificacion IS NULL OR @idValorCalificacion=idValorCalificacion)
   AND (@idAlumno IS NULL OR @idAlumno=idAlumnoCursoCicloLectivo)
   AND (@idPeriodo IS NULL OR @idPeriodo=idPeriodo)
 )
 SET @exists = 1
 ELSE SET @exists = 0

GO
/****** Object:  StoredProcedure [dbo].[Calificacion_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 26/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Calificacion_Insert]
  @idCalificacion NUMERIC = NULL
 ,@idCalificacionTransaccional NUMERIC = NULL
 ,@observaciones NVARCHAR = NULL
 ,@fecha DATE = NULL
 ,@idValorCalificacion NUMERIC = NULL
 ,@idAlumnoCursoCicloLectivo NUMERIC = NULL
 ,@idAsignatura NUMERIC = NULL
 ,@idPeriodo NUMERIC = NULL
 ,@idInstanciaCalificacion NUMERIC = NULL
AS
 SET NOCOUNT OFF;

 DECLARE @idCursoCicloLectivo NUMERIC = 0
 
SELECT @idCalificacion=idCalificacion
  FROM [Calificacion] 
  WHERE [idCalificacionTransaccional]= @idCalificacionTransaccional
	
SELECT @idValorCalificacion=idValorEscalaCalificacion
FROM [ValoresEscalaCalificacion]
WHERE idValorEscalaCalificacionTransaccional= @idValorCalificacion
  
SELECT @idAlumnoCursoCicloLectivo=idAlumnoCursoCicloLectivo 
FROM [AlumnoCursoCicloLectivo]
WHERE idAlumnoCursoCicloLectivoTransaccional= @idAlumnoCursoCicloLectivo

SELECT @idCursoCicloLectivo= idCursoCicloLectivo 
FROM [AlumnoCursoCicloLectivo]
WHERE idAlumnoCursoCicloLectivoTransaccional= @idAlumnoCursoCicloLectivo

SELECT @idPeriodo=idPeriodo	
FROM Periodo
WHERE idPeriodoTransaccional=@idPeriodo

--SELECT @idAsignatura=AC.idAsignaturaCicloLectivo
--FROM [AsignaturaCicloLectivo] AC inner join Asignatura as A on AC.idAsignatura = A.idAsignatura
--where A.idAsignaturaTransaccional = @idAsignatura
--	and AC.idCursoCicloLectivo = @idCursoCicloLectivo

SELECT @idAsignatura=A.idAsignatura
FROM Asignatura as A 
where A.idAsignaturaTransaccional = @idAsignatura

IF (@idCalificacion IS NULL OR @idCalificacion = 0 )
	  BEGIN
		  INSERT INTO [Calificacion](
			 idCalificacionTransaccional
			,observaciones
			,fecha
			,idValorCalificacion
			,idAlumnoCursoCicloLectivo
			,idAsignaturaCicloLectivo
			,idPeriodo
			,IdInstanciaCalificacion
			)
		 VALUES(
			 @idCalificacionTransaccional
			,@observaciones
			,@fecha
			,@idValorCalificacion
			,@idAlumnoCursoCicloLectivo
			,@idAsignatura
			,@idPeriodo
			,@idInstanciaCalificacion
			)   
		SET @idCalificacion = @@IDENTITY
	  END
ELSE
	BEGIN
		EXECUTE [Calificacion_Update] 
		   @idCalificacion
		  ,@idCalificacionTransaccional
		  ,@observaciones
		  ,@fecha
		  ,@idValorCalificacion
		  ,@idAlumnoCursoCicloLectivo
		  ,@idAsignatura
		  ,@idPeriodo
		  ,@idInstanciaCalificacion
	  	END

GO
/****** Object:  StoredProcedure [dbo].[Calificacion_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Calificacion_Select]
@idCalificacion NUMERIC = NULL
 ,@idCalificacionTransaccional NUMERIC = NULL
 ,@observaciones VARCHAR(50) = NULL
 ,@fecha DATE = NULL
 ,@idValorCalificacion NUMERIC = NULL
 ,@idAlumno NUMERIC = NULL
 ,@idPeriodo NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    SELECT idCalificacion
    ,idCalificacionTransaccional
    ,observaciones
    ,fecha
    ,idValorCalificacion
    ,idAlumnoCursoCicloLectivo
    ,idPeriodo
    From Calificacion
    
    WHERE (@idCalificacion IS NULL OR @idCalificacion=idCalificacion)
   AND (@idCalificacionTransaccional IS NULL OR @idCalificacionTransaccional=idCalificacion)
   AND (@observaciones IS NULL OR @observaciones=observaciones)
   AND (@fecha IS NULL OR @fecha=fecha)
   AND (@idValorCalificacion IS NULL OR @idValorCalificacion=idValorCalificacion)
   AND (@idAlumno IS NULL OR @idAlumno=idAlumnoCursoCicloLectivo)
   AND (@idPeriodo IS NULL OR @idPeriodo=idPeriodo)

GO
/****** Object:  StoredProcedure [dbo].[Calificacion_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 26/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Calificacion_Update]
  @idcalificacion NUMERIC = NULL
 ,@idCalificacionTransaccional NUMERIC = NULL
 ,@observaciones NVARCHAR = NULL
 ,@fecha DATE = NULL
 ,@idValorCalificacion NUMERIC = NULL
 ,@idAlumno NUMERIC = NULL
 ,@idAsignatura NUMERIC = NULL
 ,@idPeriodo NUMERIC = NULL
 ,@idInstanciaCalificacion NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    UPDATE Calificacion SET 
   
     idCalificacionTransaccional = @idCalificacionTransaccional
     ,observaciones = @observaciones
     ,fecha = @fecha
     ,idValorCalificacion = @idValorCalificacion
     ,idAlumnoCursoCicloLectivo = @idAlumno
     ,idAsignaturaCicloLectivo = @idAsignatura
     ,idPeriodo = @idPeriodo
     ,idInstanciaCalificacion = @idInstanciaCalificacion
    WHERE  idCalificacion=@idCalificacion

GO
/****** Object:  StoredProcedure [dbo].[CargosPersonal_Delete]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CargosPersonal_Delete]
  @idCargoPersonal NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@descripcion VARCHAR(250) = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;

    DELETE FROM CargosPersonal
    WHERE (@idCargoPersonal IS NULL OR @idCargoPersonal=idCargoPersonal)
   AND (@nombre IS NULL OR @nombre=nombre)
   AND (@descripcion IS NULL OR @descripcion=descripcion)
   AND (@activo IS NULL OR @activo=activo)

-- =====UPDATE==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[CargosPersonal_Exists]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CargosPersonal_Exists]
  @idCargoPersonal NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@descripcion VARCHAR(250) = NULL
 ,@activo BIT = NULL
 ,@exists BIT OUT
AS
 SET NOCOUNT OFF;

    IF EXISTS (
 SELECT idCargoPersonal
    FROM CargosPersonal
    WHERE (@idCargoPersonal IS NULL OR @idCargoPersonal=idCargoPersonal)
   AND (@nombre IS NULL OR @nombre=nombre)
   AND (@descripcion IS NULL OR @descripcion=descripcion)
   AND (@activo IS NULL OR @activo=activo)
 )
 SET @exists = 1
 ELSE SET @exists = 0

GO
/****** Object:  StoredProcedure [dbo].[CargosPersonal_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CargosPersonal_Insert]
  @idCargoPersonal NUMERIC = NULL output
 ,@idCargoPersonalTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@descripcion VARCHAR(250) = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT off;
 
 select @idCargoPersonal=idCargoPersonal
 from CargosPersonal
 where idCargoPersonalTransaccional=@idCargoPersonalTransaccional
			
if (@idCargoPersonal is null or @idCargoPersonal=0)
begin
    INSERT INTO CargosPersonal(
     idCargoPersonalTransaccional
    ,nombre
    ,descripcion
    ,activo
 )
 VALUES(
	 @idCargoPersonalTransaccional
    ,@nombre
    ,@descripcion
    ,@activo
 )
 SET @idCargoPersonal = @@IDENTITY
 end
 else
 begin
 
 EXECUTE [CargosPersonal_Update] 
   @idCargoPersonal
  ,@idCargoPersonalTransaccional
  ,@nombre
  ,@descripcion
  ,@activo
 end

-- =====DELETE==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[CargosPersonal_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CargosPersonal_Select]
  @idCargoPersonal NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@descripcion VARCHAR(250) = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;

    SELECT idCargoPersonal
    ,nombre
    ,descripcion
    ,activo
    FROM CargosPersonal
    WHERE (@idCargoPersonal IS NULL OR @idCargoPersonal=idCargoPersonal)
   AND (@nombre IS NULL OR @nombre=nombre)
   AND (@descripcion IS NULL OR @descripcion=descripcion)
   AND (@activo IS NULL OR @activo=activo)

-- =====EXISTS==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[CargosPersonal_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CargosPersonal_Update]
  @idCargoPersonal NUMERIC = NULL
 ,@idCargoPersonalTransaccional numeric = null
 ,@nombre VARCHAR(50) = NULL
 ,@descripcion VARCHAR(250) = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;

    UPDATE CargosPersonal SET 
     idCargoPersonalTransaccional=@idCargoPersonalTransaccional
     ,nombre = @nombre
     ,descripcion = @descripcion
     ,activo = @activo
    WHERE (@idCargoPersonal IS NULL OR @idCargoPersonal=idCargoPersonal)   

-- =====SELECT==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[CategoriaPregunta_Delete]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo
-- Create date: 18/03/13
-- =============================================
CREATE PROCEDURE [dbo].[CategoriaPregunta_Delete]
 @idCategoriaPregunta NUMERIC = NULL
AS
 SET NOCOUNT OFF;

	DELETE FROM CategoriaPregunta
	WHERE
		idCategoria = @idCategoriaPregunta

GO
/****** Object:  StoredProcedure [dbo].[CategoriaPregunta_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo
-- Create date: 18/03/13
-- =============================================
CREATE PROCEDURE [dbo].[CategoriaPregunta_Insert]
 @idCategoria NUMERIC = NULL output
 ,@nombre VARCHAR(250) = NULL
 ,@descripcion VARCHAR(4000) = NULL
 ,@idAmbito NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    INSERT INTO CategoriaPregunta(
     nombre
		,descripcion
    ,idAmbito
 )
 VALUES(
    @nombre 
		,@descripcion
    ,@idAmbito
 )
SET @idCategoria = @@IDENTITY
GO
/****** Object:  StoredProcedure [dbo].[CategoriaPregunta_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CategoriaPregunta_Select]
  @idCategoria NUMERIC = NULL
 ,@idAmbito NUMERIC = NULL
 ,@nombre VARCHAR(250) = NULL
 ,@descripcion VARCHAR(4000) = NULL
AS
 SET NOCOUNT OFF;

SELECT CP.idCategoria
,CP.nombre as 'nombreCategoria'
,CP.descripcion as 'descripcionCategoria'
,CP.idAmbito
,A.nombre as 'nombreAmbito'
,A.descripcion as 'descripcionAmbito'
,(SELECT COUNT(E.idEncuesta)
			FROM Encuesta AS E
			INNER JOIN Pregunta AS P ON E.idEncuesta = P.idEncuesta
			and CP.idCategoria=P.idCategoria ) as 'cantidadEncuestas'
FROM CategoriaPregunta as CP
INNER JOIN AmbitoEncuesta as A ON CP.idAmbito=A.idAmbito
WHERE (@idCategoria IS NULL OR @idCategoria=CP.idCategoria)
AND (@idAmbito IS NULL OR @idAmbito=CP.idAmbito)
GO
/****** Object:  StoredProcedure [dbo].[CategoriaPregunta_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo
-- Create date: 18/03/13
-- =============================================
CREATE PROCEDURE [dbo].[CategoriaPregunta_Update]
 @idCategoriaPregunta NUMERIC = NULL
 ,@nombre VARCHAR(250) = NULL
 ,@descripcion VARCHAR(4000) = NULL
 ,@idAmbito NUMERIC = NULL
AS
 SET NOCOUNT OFF;

	UPDATE CategoriaPregunta
	SET
		nombre = @nombre
		,descripcion = @descripcion
		,idAmbito = @idAmbito
	WHERE
		idCategoria = @idCategoriaPregunta
GO
/****** Object:  StoredProcedure [dbo].[CategoriaPreguntaUtilizada]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Pablo
-- Create date: 22/03/2013
-- Description:	Mostrar cuantas encuestas utilizan una categoria dada
-- =============================================

CREATE PROCEDURE [dbo].[CategoriaPreguntaUtilizada]
@idCategoria NUMERIC = NULL
AS
 SET NOCOUNT OFF;

SELECT COUNT(*)
FROM Encuesta AS E
INNER JOIN Pregunta AS P ON E.idEncuesta = P.idEncuesta
INNER JOIN CategoriaPregunta AS CP ON CP.idCategoria=P.idCategoria
WHERE @idCategoria = CP.idCategoria
GO
/****** Object:  StoredProcedure [dbo].[CategoriasEncuesta_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[CategoriasEncuesta_Select]
  @idEncuesta NUMERIC = NULL
AS
 SET NOCOUNT OFF;

SELECT C.idCategoria,
C.nombre,
C.descripcion,
A.idAmbito,
A.nombre AS 'nombreAmbito',
A.descripcion AS 'descripcionAmbito'
FROM Encuesta AS E
INNER JOIN AmbitoEncuesta AS A ON A.idAmbito = E.idAmbito
INNER JOIN CategoriaPregunta AS C ON C.idAmbito = E.idAmbito

WHERE 
(@idEncuesta IS NULL OR
	( 
		@idEncuesta=E.idEncuesta
		AND 
		C.idCategoria IN 
		(
			SELECT P.idCategoria
			FROM dbo.Pregunta AS P
			WHERE P.idEncuesta = @idEncuesta
		)
	)
)	

GO
/****** Object:  StoredProcedure [dbo].[CicloLectivo_Delete]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 20/06/11
-- =============================================
CREATE PROCEDURE [dbo].[CicloLectivo_Delete]
  @idCicloLectivo NUMERIC = NULL
 ,@idCicloLectivoTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@fechaInicio DATE = NULL
 ,@fechaFin DATE = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;

    DELETE FROM CicloLectivo
    WHERE (@idCicloLectivo IS NULL OR @idCicloLectivo=idCicloLectivo)
   

-- =====UPDATE==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[CicloLectivo_Exists]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 20/06/11
-- =============================================
CREATE PROCEDURE [dbo].[CicloLectivo_Exists]
  @idCicloLectivo NUMERIC = NULL
 ,@idCicloLectivoTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@fechaInicio DATE = NULL
 ,@fechaFin DATE = NULL
 ,@activo BIT = NULL
 ,@exists BIT OUT
AS
 SET NOCOUNT OFF;

    IF EXISTS (
 SELECT idCicloLectivo
    FROM CicloLectivo
    WHERE (@idCicloLectivo IS NULL OR @idCicloLectivo=idCicloLectivo)
   AND (@idCicloLectivoTransaccional IS NULL OR @idCicloLectivoTransaccional=idCicloLectivoTransaccional)
   AND (@nombre IS NULL OR @nombre=nombre)
   AND (@fechaInicio IS NULL OR @fechaInicio=fechaInicio)
   AND (@fechaFin IS NULL OR @fechaFin=fechaFin)
   AND (@activo IS NULL OR @activo=activo)
 )
 SET @exists = 1
 ELSE SET @exists = 0

GO
/****** Object:  StoredProcedure [dbo].[CicloLectivo_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 20/06/11
-- =============================================
CREATE PROCEDURE [dbo].[CicloLectivo_Insert]
  @idCicloLectivo NUMERIC = NULL output
 ,@idCicloLectivoTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@fechaInicio DATE = NULL
 ,@fechaFin DATE = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;
SELECT @idCicloLectivo=idCicloLectivo 
  FROM [dbo].[CicloLectivo]
  WHERE [idCicloLectivoTransaccional]= @idCicloLectivoTransaccional


if (@idCicloLectivo is null or @idCicloLectivo=0)
	begin
		INSERT INTO CicloLectivo(
		 idCicloLectivoTransaccional
		,nombre
		,fechaInicio
		,fechaFin
		,activo
	 )
	 VALUES(
		 @idCicloLectivoTransaccional
		,@nombre
		,@fechaInicio
		,@fechaFin
		,@activo
	 )
	 SET @idCicloLectivo = @@IDENTITY
	end
else
	begin
	 execute CicloLectivo_Update
	  @idCicloLectivo 
	 ,@idCicloLectivoTransaccional 
	 ,@nombre 
	 ,@fechaInicio
	 ,@fechaFin 
	 ,@activo
	end

GO
/****** Object:  StoredProcedure [dbo].[CicloLectivo_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 20/06/11
-- =============================================
CREATE PROCEDURE [dbo].[CicloLectivo_Select]
  @idCicloLectivo NUMERIC = NULL
 ,@idCicloLectivoTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@fechaInicio DATE = NULL
 ,@fechaFin DATE = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;

    SELECT idCicloLectivo
    ,idCicloLectivoTransaccional
    ,nombre
    ,fechaInicio
    ,fechaFin
    ,activo
    FROM CicloLectivo
    WHERE (@idCicloLectivo IS NULL OR @idCicloLectivo=idCicloLectivo)
   AND (@idCicloLectivoTransaccional IS NULL OR @idCicloLectivoTransaccional=idCicloLectivoTransaccional)
   AND (@nombre IS NULL OR @nombre=nombre)
   AND (@fechaInicio IS NULL OR @fechaInicio=fechaInicio)
   AND (@fechaFin IS NULL OR @fechaFin=fechaFin)
   AND (@activo IS NULL OR @activo=activo)

	ORDER BY fechaFin DESC

GO
/****** Object:  StoredProcedure [dbo].[CicloLectivo_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 20/06/11
-- =============================================
CREATE PROCEDURE [dbo].[CicloLectivo_Update]
  @idCicloLectivo NUMERIC = NULL
 ,@idCicloLectivoTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@fechaInicio DATE = NULL
 ,@fechaFin DATE = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;

    UPDATE CicloLectivo SET 
      idCicloLectivoTransaccional = @idCicloLectivoTransaccional
     ,nombre = @nombre
     ,fechaInicio = @fechaInicio
     ,fechaFin = @fechaFin
     ,activo = @activo
    WHERE (@idCicloLectivo IS NULL OR @idCicloLectivo=idCicloLectivo)
   

-- =====SELECT==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[Citacion_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:  LAURA
-- Create date: 25/08/11
-- =============================================
CREATE PROCEDURE [dbo].[Citacion_Insert]
  @idCitacion NUMERIC = NULL
 ,@fecha DATE = NULL
 ,@hora TIME = NULL
 ,@username VARCHAR(50) = NULL
 ,@idMotivoCitacion NUMERIC = NULL
 ,@idTutor NUMERIC = NULL
 ,@detalle VARCHAR(250) = NULL
 ,@activo BIT = NULL
 ,@idCurso NUMERIC = NULL
AS
 SET NOCOUNT OFF;

Declare @idOrganizador numeric = null
	SELECT @idOrganizador = P.idPersona
	FROM Personas AS P --INNER JOIN Personal AS PER ON P.idPersona = PER.idPersona
	WHERE P.username = @username 

    INSERT INTO Citacion(
     fecha
    ,hora
    ,idOrganizador
    ,idMotivoCitacion
    ,idTutor
    ,detalle
    ,activo
    ,idCurso
 )
 VALUES(
     @fecha
    ,@hora
    ,@idOrganizador
    ,@idMotivoCitacion
    ,@idTutor
    ,@detalle
    ,@activo
    ,@idCurso
 )
 
  SET @idCitacion = @@IDENTITY


GO
/****** Object:  StoredProcedure [dbo].[Citacion_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:  LAURA
-- Create date: 25/08/11
-- =============================================
CREATE PROCEDURE [dbo].[Citacion_Select]
  @idCitacion NUMERIC = NULL
 ,@fecha DATE = NULL
 ,@hora TIME = NULL
 ,@idOrganizador NUMERIC = NULL
 ,@usuario VARCHAR(49) = NULL
 ,@idMotivoCitacion NUMERIC = NULL
 ,@idTutor NUMERIC = NULL
 ,@detalle VARCHAR(250) = NULL
 ,@activo BIT = NULL
 ,@vencidas BIT = NULL
 ,@fechaDesde DATE = NULL
 ,@fechaHasta DATE = NULL
 ,@usuarioTutor VARCHAR(50) = NULL
AS
 SET NOCOUNT OFF;

	SELECT @idOrganizador = P.idPersona
	FROM Personas AS P --INNER JOIN Personal AS PER ON P.idPersona = PER.idPersona
	WHERE P.username = @usuario

    SELECT 
     C.idCitacion
    ,C.fecha
    ,C.hora
    ,C.idOrganizador
    ,C.idMotivoCitacion
    ,C.idTutor
    ,C.detalle
    ,C.activo
    ,O.nombre AS 'nombreOrganizador'
    ,O.apellido AS 'apellidoOrganizador' 
    ,O.username AS 'usernameOrganizador'
    ,P.nombre AS 'nombreTutor'
    ,P.apellido AS 'apellidoTutor'
    ,MC.descripcion AS 'motivoCitacion'
    ,C.idCurso
    FROM Citacion AS C 
		--INNER JOIN Personal AS PER ON C.idOrganizador = PER.idPersonal
		INNER JOIN Personas AS O ON C.idOrganizador = O.idPersona
		INNER JOIN Tutor AS T ON C.idTutor = T.idTutor
		INNER JOIN Personas AS P ON T.idPersona = P.idPersona
		INNER JOIN MotivoCitacion AS MC ON C.idMotivoCitacion = MC.idMotivoCitacion
		INNER JOIN Personal AS Pe ON Pe.idPersona = O.idPersona
		--INNER JOIN AsignaturaCicloLectivo as ACL on Pe.idPersonal=ACL.iddocente
		--inner join CursosCicloLectivo as CCL on CCL.idCursoCicloLectivo=ACL.idCursoCicloLectivo 
		--inner join Curso as Cu on Cu.idCurso=CCL.idCurso 
		--inner join Nivel as N on N.idNivel =Cu.idNivel 
		--inner join CicloLectivo as CL on CL.idCicloLectivo=CCL.idCicloLectivo 
		--inner join Asignatura as A on A.idAsignatura=ACL.idAsignatura
 
    WHERE (@idCitacion IS NULL OR @idCitacion <> C.idCitacion)
   AND (@fecha IS NULL OR @fecha=C.fecha)
   AND (@hora IS NULL OR @hora=C.hora)
   AND (@idOrganizador IS NULL OR @idOrganizador=C.idOrganizador)
   AND (@idMotivoCitacion IS NULL OR @idMotivoCitacion=C.idMotivoCitacion)
   AND (@idTutor IS NULL OR @idTutor=C.idTutor)
   AND (@detalle IS NULL OR @detalle=C.detalle)
   AND (@activo IS NULL OR @activo=C.activo)
   AND (@fechaDesde IS NULL OR C.fecha BETWEEN @fechaDesde AND @fechaHasta)
   AND (@usuarioTutor IS NULL OR @usuarioTutor=P.username)
   AND (@vencidas IS NOT NULL OR C.fecha >= GETDATE())
order by c.fecha desc,c.hora desc


GO
/****** Object:  StoredProcedure [dbo].[Citacion_Select_TESTING]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 25/08/11
-- =============================================
CREATE PROCEDURE [dbo].[Citacion_Select_TESTING]
  @idCitacion NUMERIC = NULL
 ,@fecha DATE = NULL
 ,@hora TIME = NULL
 ,@idOrganizador NUMERIC = NULL
 ,@usuario VARCHAR(49) = NULL
 ,@idMotivoCitacion NUMERIC = NULL
 ,@idTutor NUMERIC = NULL
 ,@detalle VARCHAR(250) = NULL
 ,@activo BIT = NULL
 ,@vencidas BIT = NULL
 ,@fechaDesde DATE = NULL
 ,@fechaHasta DATE = NULL
 ,@usuarioTutor VARCHAR(50) = NULL
AS
 SET NOCOUNT OFF;

	SELECT @idOrganizador = P.idPersona
	FROM Personas AS P --INNER JOIN Personal AS PER ON P.idPersona = PER.idPersona
	WHERE P.username = @usuario

    SELECT 
     C.idCitacion
    ,C.fecha
    ,C.hora
    ,C.idOrganizador
    ,C.idMotivoCitacion
    ,C.idTutor
    ,C.detalle
    ,C.activo
    ,O.nombre AS 'nombreOrganizador'
    ,O.apellido AS 'apellidoOrganizador' 
    ,O.username AS 'usernameOrganizador'
    ,P.nombre AS 'nombreTutor'
    ,P.apellido AS 'apellidoTutor'
	  ,P1.nombre AS 'nombreAlumno'
	  ,P1.apellido AS 'apellidoAlumno' 
    ,MC.descripcion AS 'motivoCitacion'
    ,C.idCurso
    FROM Citacion AS C 
		--INNER JOIN Personal AS PER ON C.idOrganizador = PER.idPersonal
		INNER JOIN Personas AS O ON C.idOrganizador = O.idPersona
		INNER JOIN Tutor AS T ON C.idTutor = T.idTutor
		INNER JOIN Personas AS P ON T.idPersona = P.idPersona
		INNER JOIN MotivoCitacion AS MC ON C.idMotivoCitacion = MC.idMotivoCitacion
		INNER JOIN Personal AS Pe ON Pe.idPersona = O.idPersona
		INNER JOIN TutorAlumno AS TAL ON TAL.idTutor=T.idTutor
		INNER JOIN Alumnos AS ALU ON ALU.idAlumno = TAL.idAlumno
		INNER JOIN Personas AS P1 ON P1.idPersona = ALU.idPersona
		--INNER JOIN AsignaturaCicloLectivo as ACL on Pe.idPersonal=ACL.iddocente
		--inner join CursosCicloLectivo as CCL on CCL.idCursoCicloLectivo=ACL.idCursoCicloLectivo 
		--inner join Curso as Cu on Cu.idCurso=CCL.idCurso 
		--inner join Nivel as N on N.idNivel =Cu.idNivel 
		--inner join CicloLectivo as CL on CL.idCicloLectivo=CCL.idCicloLectivo 
		--inner join Asignatura as A on A.idAsignatura=ACL.idAsignatura
 
    WHERE (@idCitacion IS NULL OR @idCitacion <> C.idCitacion)
   AND (@fecha IS NULL OR @fecha=C.fecha)
   AND (@hora IS NULL OR @hora=C.hora)
   AND (@idOrganizador IS NULL OR @idOrganizador=C.idOrganizador)
   AND (@idMotivoCitacion IS NULL OR @idMotivoCitacion=C.idMotivoCitacion)
   AND (@idTutor IS NULL OR @idTutor=C.idTutor)
   AND (@detalle IS NULL OR @detalle=C.detalle)
   AND (@activo IS NULL OR @activo=C.activo)
   AND (@fechaDesde IS NULL OR C.fecha BETWEEN @fechaDesde AND @fechaHasta)
   AND (@usuarioTutor IS NULL OR @usuarioTutor=P.username)
   AND (@vencidas IS NOT NULL OR C.fecha >= GETDATE())
order by c.fecha desc,c.hora desc


GO
/****** Object:  StoredProcedure [dbo].[Citacion_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 25/08/11
-- =============================================
CREATE PROCEDURE [dbo].[Citacion_Update]
  @idCitacion NUMERIC = NULL
 ,@fecha DATE = NULL
 ,@hora TIME = NULL
 ,@idOrganizador NUMERIC = NULL
 ,@idMotivoCitacion NUMERIC = NULL
 ,@idTutor NUMERIC = NULL
 ,@detalle VARCHAR(250) = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;

    UPDATE Citacion SET 
      fecha = @fecha
     ,hora = @hora
     ,idOrganizador = @idOrganizador
     ,idMotivoCitacion = @idMotivoCitacion
     ,idTutor = @idTutor
     ,detalle = @detalle
     ,activo = @activo
    WHERE @idCitacion=idCitacion

GO
/****** Object:  StoredProcedure [dbo].[CitacionValidarDisponibilidad_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 25/08/11
-- =============================================
CREATE PROCEDURE [dbo].[CitacionValidarDisponibilidad_Select]
  @idCitacion NUMERIC = NULL
 ,@fecha DATE = NULL
 ,@hora TIME = NULL
 ,@username VARCHAR(50) = NULL
 ,@idTutor NUMERIC = NULL
AS
 SET NOCOUNT OFF;

	DECLARE @idOrganizador NUMERIC 
	SELECT @idOrganizador = PER.idPersonal 
	FROM Personas AS P INNER JOIN Personal AS PER ON P.idPersona = PER.idPersona
	WHERE P.username = @username
	
	SELECT 
		 C.idCitacion
		,C.fecha
		,C.hora
		,C.idOrganizador
		,C.idMotivoCitacion
		,C.idTutor
		,C.detalle
		,C.activo
		,O.nombre AS 'nombreOrganizador'
		,O.apellido AS 'apellidoOrganizador' 
		,P.nombre AS 'nombreTutor'
		,P.apellido AS 'apellidoTutor'
		,MC.descripcion AS 'motivoCitacion'
    FROM Citacion AS C 
		INNER JOIN Personal AS PER ON C.idOrganizador = PER.idPersonal
		INNER JOIN Personas AS O ON PER.idPersona = O.idPersona
		INNER JOIN Tutor AS T ON C.idTutor = T.idTutor
		INNER JOIN Personas AS P ON T.idPersona = P.idPersona
		INNER JOIN MotivoCitacion AS MC ON C.idMotivoCitacion = MC.idMotivoCitacion
    WHERE 
			(@fecha=C.fecha)
	   AND	(@hora between C.hora and DATEADD(hh,1,C.hora))
	   AND	(@idOrganizador=C.idOrganizador OR @idTutor=C.idTutor)
	   AND	(C.activo = 1)
	   AND  (@idCitacion IS NULL OR @idCitacion <> C.idCitacion)

GO
/****** Object:  StoredProcedure [dbo].[Configuraciones_Delete]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 12/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Configuraciones_Delete]
  @idConfiguracion INT = NULL
 ,@nombre NVARCHAR = NULL
 ,@descripcion NVARCHAR = NULL
 ,@valor NVARCHAR = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;

    DELETE FROM Configuraciones
    WHERE (@idConfiguracion IS NULL OR @idConfiguracion=idConfiguracion)
   AND (@nombre IS NULL OR @nombre=nombre)
   AND (@descripcion IS NULL OR @descripcion=descripcion)
   AND (@valor IS NULL OR @valor=valor)
   AND (@activo IS NULL OR @activo=activo)

-- =====UPDATE==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[Configuraciones_Exists]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 12/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Configuraciones_Exists]
  @idConfiguracion INT = NULL
 ,@nombre NVARCHAR = NULL
 ,@descripcion NVARCHAR = NULL
 ,@valor NVARCHAR = NULL
 ,@activo BIT = NULL
 ,@exists BIT OUT
AS
 SET NOCOUNT OFF;

    IF EXISTS (
 SELECT idConfiguracion
    FROM Configuraciones
    WHERE (@idConfiguracion IS NULL OR @idConfiguracion=idConfiguracion)
   AND (@nombre IS NULL OR @nombre=nombre)
   AND (@descripcion IS NULL OR @descripcion=descripcion)
   AND (@valor IS NULL OR @valor=valor)
   AND (@activo IS NULL OR @activo=activo)
 )
 SET @exists = 1
 ELSE SET @exists = 0

GO
/****** Object:  StoredProcedure [dbo].[Configuraciones_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 12/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Configuraciones_Insert]
  @idConfiguracion INT = NULL
 ,@nombre NVARCHAR = NULL
 ,@descripcion NVARCHAR = NULL
 ,@valor NVARCHAR = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;

    INSERT INTO Configuraciones(
     idConfiguracion
    ,nombre
    ,descripcion
    ,valor
    ,activo
 )
 VALUES(
     @idConfiguracion
    ,@nombre
    ,@descripcion
    ,@valor
    ,@activo
 )

-- =====DELETE==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[Configuraciones_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 12/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Configuraciones_Select]
  @idConfiguracion INT = NULL
 ,@nombre varchar(50) = NULL
 ,@descripcion varchar(250) = NULL
 ,@valor varchar(50) = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;

    SELECT idConfiguracion
    ,nombre
    ,descripcion
    ,valor
    ,activo
    FROM Configuraciones
    WHERE (@idConfiguracion IS NULL OR @idConfiguracion=idConfiguracion)
   AND (@nombre IS NULL OR @nombre=nombre)
   AND (@descripcion IS NULL OR @descripcion=descripcion)
   AND (@valor IS NULL OR @valor=valor)
   AND (@activo IS NULL OR @activo=activo)

-- =====EXISTS==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[Configuraciones_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 12/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Configuraciones_Update]
  @idConfiguracion INT = NULL
 ,@nombre NVARCHAR = NULL
 ,@descripcion NVARCHAR = NULL
 ,@valor NVARCHAR = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;

    UPDATE Configuraciones SET 
	  nombre = @nombre
     ,descripcion = @descripcion
     ,valor = @valor
     ,activo = @activo
    WHERE (@idConfiguracion IS NULL OR @idConfiguracion=idConfiguracion)
   AND (@nombre IS NULL OR @nombre=nombre)
   AND (@descripcion IS NULL OR @descripcion=descripcion)
   AND (@valor IS NULL OR @valor=valor)
   AND (@activo IS NULL OR @activo=activo)

-- =====SELECT==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionesEstablecimiento_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:  LAURA
-- Create date: 22/07/11
-- =============================================
CREATE PROCEDURE [dbo].[ConfiguracionesEstablecimiento_Insert]
  @idConfiguracionEstablecimiento NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@descripcion VARCHAR(250) = NULL
 ,@valor VARCHAR(50) = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;



GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionesEstablecimiento_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 12/06/11
-- =============================================
CREATE PROCEDURE [dbo].[ConfiguracionesEstablecimiento_Select]
  @idConfiguracionEstablecimiento INT = NULL
 ,@nombre varchar(50) = NULL
 ,@descripcion varchar(250) = NULL
 ,@valor varchar(50) = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;

    SELECT idConfiguracionEstablecimiento
    ,nombre
    ,descripcion
    ,valor
    ,activo
    FROM ConfiguracionesEstablecimiento
    WHERE (@idConfiguracionEstablecimiento IS NULL OR @idConfiguracionEstablecimiento=idConfiguracionEstablecimiento)
   AND (@nombre IS NULL OR @nombre=nombre)
   AND (@descripcion IS NULL OR @descripcion=descripcion)
   AND (@valor IS NULL OR @valor=valor)
   AND (@activo IS NULL OR @activo=activo)

-- =====EXISTS==================================
SET ANSI_NULLS ON


GO
/****** Object:  StoredProcedure [dbo].[Contenido_Delete]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 01/05/12
-- =============================================
CREATE PROCEDURE [dbo].[Contenido_Delete]
  @idContenido NUMERIC = NULL
 ,@usernameBaja VARCHAR(50) = NULL

AS
 SET NOCOUNT OFF;

  Declare @idPersonaBaja numeric = null
select @idPersonaBaja = idPersona
from Personas where Personas.username = @usernameBaja

    UPDATE Contenido SET 
		 fechaBaja = GETDATE()
		,idPersonaBaja = @idPersonaBaja
		,activo = 0
	WHERE (@idContenido IS NULL OR idContenido = @idContenido)
		AND activo = 1

GO
/****** Object:  StoredProcedure [dbo].[Contenido_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 24/03/13
-- =============================================
CREATE PROCEDURE [dbo].[Contenido_Insert]
  @idContenido NUMERIC = NULL output
 ,@idCurricula NUMERIC = NULL
 ,@descripcion VARCHAR(50) = NULL
 ,@activo BIT = NULL
 ,@fechaBaja NUMERIC = NULL
 ,@idPersonaBaja NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    INSERT INTO Contenido(
     idCurricula
    ,descripcion
    ,activo
    ,fechaBaja
    ,idPersonaBaja
 )
 VALUES(
     @idCurricula
    ,@descripcion
    ,1
    ,null
    ,null
 )

   SET @idContenido = @@IDENTITY
--RETURN SCOPE_IDENTITY()
GO
/****** Object:  StoredProcedure [dbo].[Contenido_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 01/05/12
-- =============================================
CREATE PROCEDURE [dbo].[Contenido_Select]
  @idContenido NUMERIC = NULL
 ,@idCurricula NUMERIC = NULL
 ,@descripcion VARCHAR(50) = NULL
 ,@activo BIT = NULL
 ,@fechaBaja Date = NULL
 ,@idPersonaBaja NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    SELECT idContenido
    ,idCurricula
    ,descripcion
    ,activo
    ,fechaBaja
    ,idPersonaBaja
    FROM Contenido
    WHERE (@idContenido IS NULL OR @idContenido=idContenido)
   AND (@idCurricula IS NULL OR @idCurricula=idCurricula)
   AND (@descripcion IS NULL OR @descripcion=descripcion)
   AND (@activo IS NULL OR @activo=activo)
   AND (@fechaBaja IS NULL OR @fechaBaja=fechaBaja)
   AND (@idPersonaBaja IS NULL OR @idPersonaBaja=idPersonaBaja)
 order by descripcion
GO
/****** Object:  StoredProcedure [dbo].[Contenido_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 01/05/12
-- =============================================
CREATE PROCEDURE [dbo].[Contenido_Update]
  @idContenido NUMERIC = NULL
 ,@descripcion VARCHAR(50) = NULL
 ,@idCurricula NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    UPDATE Contenido SET 
      descripcion = @descripcion
     ,idCurricula = @idCurricula
    WHERE @idContenido=idContenido
  
GO
/****** Object:  StoredProcedure [dbo].[ContenidosPlanificados_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 20/08/12
-- =============================================
CREATE PROCEDURE [dbo].[ContenidosPlanificados_Select]
  @idAsignaturaCicloLectivo NUMERIC = NULL
AS
 SET NOCOUNT OFF;

declare  @idCurricula NUMERIC = NULL
 
select @idCurricula = PA.idCurricula
 from
	RelPlanificacionCurso as RPC 
		inner join AsignaturaCicloLectivo AS ACL on rpc.idCursoCicloLectivo = acl.idCursoCicloLectivo
			and acl.idAsignaturaCicloLectivo = @idAsignaturaCicloLectivo
		inner join PlanificacionAnual as PA on pa.idPlanificacionAnual = RPC.idPlanificacionAnual
		inner join Curricula as C on c.idCurricula = pa.idCurricula
			and c.idAsignatura = acl.idAsignatura

 SELECT TC.idTemaContenido
    ,TC.idContenido
    ,C.descripcion + ' - ' + TC.titulo as [titulo]
    ,TC.detalle
    ,TPTC.obligatorio
    FROM TemaContenido AS TC
		INNER JOIN Contenido AS C ON TC.idContenido = C.idContenido
		INNER JOIN TemaPlanificacionTemaContenido AS TPTC ON TPTC.idTemaContenido = TC.idTemaContenido
		INNER JOIN TemaPlanificacionAnual AS TPA ON TPA.idTemaPlanificacion = TPTC.idTemaPlanificacion
		INNER JOIN PlanificacionAnual AS PA ON PA.idPlanificacionAnual = TPA.idPlanificacionAnual
			AND PA.idCurricula = @idCurricula
			WHERE 
		 TC.idTemaContenido NOT IN (
		SELECT TC.idTemaContenido
		FROM TemaContenido AS TC
			INNER JOIN Contenido AS C 
				ON TC.idContenido = C.idContenido
			INNER JOIN TemaPlanificacionTemaContenido AS TPTC
				ON TC.idTemaContenido = TPTC.idTemaContenido
			INNER JOIN dbo.DetalleRegistroClases AS DTC
				ON DTC.idTemaContenido = TC.idTemaContenido
			INNER JOIN RegistroClases AS RC ON RC.idRegistroClases = DTC.idRegistroClases
			AND RC.idAsignaturaCurso = @idAsignaturaCicloLectivo
		GROUP BY RC.idAsignaturaCurso ,TC.idTemaContenido
		HAVING	
			SUM(DTC.porcentaje) >= 100
		)
   ORDER BY TC.idTemaContenido, TC.titulo
GO
/****** Object:  StoredProcedure [dbo].[Curricula_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 24/03/13
-- =============================================
CREATE PROCEDURE [dbo].[Curricula_Insert]
  @idCurricula NUMERIC = NULL output
 ,@fechaAlta DATE = NULL
 ,@usernameAlta VARCHAR(50) = NULL
 ,@idAsignatura NUMERIC = NULL
 ,@idNivel NUMERIC = NULL
 ,@idOrientacion NUMERIC = NULL
AS
 SET NOCOUNT OFF;

Declare @idPersonaAlta numeric = null
select @idPersonaAlta = idPersona
from Personas where Personas.username = @usernameAlta 

    INSERT INTO Curricula(
     fechaAlta
    ,fechaModificacion
    ,idPersonaAlta
    ,idPersonaModificacion
    ,idAsignatura
    ,idNivel
    ,idOrientacion
 )
 VALUES(
     GETDATE()
    ,null
    ,@idPersonaAlta
    ,null
    ,@idAsignatura
    ,@idNivel
    ,@idOrientacion
 )

  SET @idCurricula = @@IDENTITY
--RETURN SCOPE_IDENTITY()
GO
/****** Object:  StoredProcedure [dbo].[Curricula_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 19/03/13
-- =============================================
CREATE PROCEDURE [dbo].[Curricula_Select]
  @idCurricula NUMERIC = NULL
 ,@fechaAlta DATE = NULL
 ,@fechaModificacion DATE = NULL
 ,@idPersonaAlta NUMERIC = NULL
 ,@idPersonaModificacion NUMERIC = NULL
 ,@idAsignatura NUMERIC = NULL
 ,@idNivel NUMERIC = NULL
 ,@idOrientacion NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    SELECT 
	 Curricula.idCurricula
    ,Curricula.fechaAlta
    ,Curricula.fechaModificacion
    ,Curricula.idPersonaAlta
	,Personas.username AS 'UsuarioAlta'
    ,Curricula.idPersonaModificacion
	,PM.username AS 'UsuarioModificacion'
    ,Curricula.idAsignatura
	,Asignatura.nombre AS 'Asignatura'
    ,Curricula.idNivel
	,Nivel.nombre AS 'Nivel'
    ,Curricula.idOrientacion
	,Orientacion.nombre AS 'Orientacion'
    FROM Curricula
		INNER JOIN Asignatura ON Curricula.idAsignatura = Asignatura.idAsignatura
		INNER JOIN Nivel ON Curricula.idNivel = Nivel.idNivel 
		INNER JOIN Orientacion ON Curricula.idOrientacion = Orientacion.idOrientacion
		INNER JOIN Personas ON Curricula.idPersonaAlta = Personas.idPersona
		LEFT JOIN Personas AS PM ON Curricula.idPersonaModificacion = PM.idPersona
    WHERE (@idCurricula IS NULL OR @idCurricula=idCurricula)
   AND (@fechaAlta IS NULL OR @fechaAlta=fechaAlta)
   AND (@fechaModificacion IS NULL OR @fechaModificacion=fechaModificacion)
   AND (@idPersonaAlta IS NULL OR @idPersonaAlta=idPersonaAlta)
   AND (@idPersonaModificacion IS NULL OR @idPersonaModificacion=idPersonaModificacion)
   AND (@idAsignatura IS NULL OR @idAsignatura=Curricula.idAsignatura)
   AND (@idNivel IS NULL OR @idNivel=Curricula.idNivel)
   AND (@idOrientacion IS NULL OR @idOrientacion=Curricula.idOrientacion)
GO
/****** Object:  StoredProcedure [dbo].[Curricula_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 24/03/13
-- =============================================
CREATE PROCEDURE [dbo].[Curricula_Update]
  @idCurricula NUMERIC = NULL
 ,@fechaAlta DATE = NULL
 ,@fechaModificacion DATE = NULL
 ,@usernameModificacion VARCHAR(50) = NULL
 ,@idAsignatura NUMERIC = NULL
 ,@idNivel NUMERIC = NULL
 ,@idOrientacion NUMERIC = NULL
AS
 SET NOCOUNT OFF;

 Declare @idPersonaModificacion numeric = null
select @idPersonaModificacion = idPersona
from Personas where Personas.username = @usernameModificacion 

    UPDATE Curricula SET 
      fechaModificacion = getdate()
     ,idPersonaModificacion = @idPersonaModificacion
     ,idAsignatura = @idAsignatura
     ,idNivel = @idNivel
     ,idOrientacion = @idOrientacion
    WHERE @idCurricula=idCurricula
   
GO
/****** Object:  StoredProcedure [dbo].[Curso_Delete]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 20/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Curso_Delete]
  @idCurso NUMERIC = NULL
 ,@idCursoTransaccional NUMERIC = NULL
 ,@idNivel NUMERIC = NULL
 ,@division VARCHAR(50) = NULL
 ,@idPreceptor NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 --,@idCicloLectivo NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    DELETE FROM Curso
    WHERE (@idCurso IS NULL OR @idCurso=idCurso)

-- =====UPDATE==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[Curso_Exists]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 20/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Curso_Exists]
  @idCurso NUMERIC = NULL
 ,@idCursoTransaccional NUMERIC = NULL
 ,@idNivel NUMERIC = NULL
 ,@idPreceptor NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 --,@idCicloLectivo NUMERIC = NULL
 ,@exists BIT OUT
AS
 SET NOCOUNT OFF;

    IF EXISTS (
 SELECT idCurso
    FROM Curso
    WHERE (@idCurso IS NULL OR @idCurso=idCurso)
   AND (@idCursoTransaccional IS NULL OR @idCursoTransaccional=idCursoTransaccional)
   AND (@idNivel IS NULL OR @idNivel=idNivel)
    AND (@idPreceptor IS NULL OR @idPreceptor=idPreceptor)
   AND (@nombre IS NULL OR @nombre=nombre)
   --AND (@idCicloLectivo IS NULL OR @idCicloLectivo=idCicloLectivo)
 )
 SET @exists = 1
 ELSE SET @exists = 0

GO
/****** Object:  StoredProcedure [dbo].[Curso_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 20/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Curso_Insert]
  @idCurso NUMERIC = NULL
 ,@idCursoTransaccional NUMERIC = NULL
 ,@idNivel NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
AS
 SET NOCOUNT OFF;

SELECT @idCurso=idCurso 
  FROM [Curso]
  WHERE [idCursoTransaccional]= @idCursoTransaccional

Select @idNivel=idNivel
from [Nivel]
where idNivelTransaccional=@idNivel

if (@idCurso is null or @idCurso=0)
	begin
		INSERT INTO Curso(
		 idCursoTransaccional
		,idNivel
		,nombre
	 )
	 VALUES(
		 @idCursoTransaccional
		,@idNivel
		,@nombre
	 )
	 SET @idCurso = @@IDENTITY
	end
else
	begin
	 execute Curso_Update
	  @idCurso 
	 ,@idCursoTransaccional
	 ,@idNivel
	 ,@nombre 
	end

GO
/****** Object:  StoredProcedure [dbo].[Curso_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 20/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Curso_Select]
  @idCurso NUMERIC = NULL
 ,@idCursoTransaccional NUMERIC = NULL
 ,@idNivel NUMERIC = NULL
 ,@division VARCHAR(50) = NULL
 ,@idPreceptor NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@idCicloLectivo NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    SELECT idCurso
    ,idCursoTransaccional
    ,idNivel
    ,idPreceptor
    ,nombre
    ,idCicloLectivo
    FROM Curso
    WHERE (@idCurso IS NULL OR @idCurso=idCurso)
   AND (@idCursoTransaccional IS NULL OR @idCursoTransaccional=idCursoTransaccional)
   AND (@idNivel IS NULL OR @idNivel=idNivel)
   AND (@idPreceptor IS NULL OR @idPreceptor=idPreceptor)
   AND (@nombre IS NULL OR @nombre=nombre)
   AND (@idCicloLectivo IS NULL OR @idCicloLectivo=idCicloLectivo)

GO
/****** Object:  StoredProcedure [dbo].[Curso_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 20/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Curso_Update]
  @idCurso NUMERIC = NULL
 ,@idCursoTransaccional NUMERIC = NULL
 ,@idNivel NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL

AS
 SET NOCOUNT OFF;

    UPDATE Curso SET 
      idCursoTransaccional = @idCursoTransaccional
     ,idNivel = @idNivel
     ,nombre = @nombre
    WHERE @idCurso=idCurso

GO
/****** Object:  StoredProcedure [dbo].[Curso_Update_CicloLectivo]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Curso_Update_CicloLectivo] 
AS

/* Este cursor deja las contraseñas iguales al nombre de usuario.
La tabla Cliente tiene estos tres campos: CliCod, CliUser, CliPass */
-- declaramos las variables
declare @idCurso as numeric
declare @idCicloLectivo as numeric
--declare @pass as varchar(50)
-- declaramos un cursor llamado "CURSORITO". El select debe contener sólo los campos a utilizar.
declare CURSORITO cursor for
select distinct idCurso, idCicloLectivo from AsignaturaCurso
open CURSORITO
-- Avanzamos un registro y cargamos en las variables los valores encontrados en el primer registro
fetch next from CURSORITO
into @idCurso, @idCicloLectivo
  while @@fetch_status = 0
begin
	Declare @cantidad numeric = null
	Declare @idCursoCicloLectivo numeric = null
	Declare @fecha Date = getdate()
	Select @cantidad = COUNT(idCursoCicloLectivo)
	From CursosCicloLectivo
	Where idCurso = @idCurso AND idCicloLectivo = @idCicloLectivo

	IF (@cantidad IS NULL OR @cantidad = 0)
	BEGIN
		INSERT INTO CursosCicloLectivo (idCurso,idCicloLectivo)
		VALUES (@idCurso,@idCicloLectivo)
		
		SELECT @idCursoCicloLectivo=MAX(CCL.idCursoCicloLectivo) from CursosCicloLectivo as CCL
		
		EXECUTE [AgendaActividades_Insert] 
				   0
				  ,@idCursoCicloLectivo
				  ,@idCurso
				  ,@idCicloLectivo
				  ,'Agenda generada automáticamente'
				  ,@fecha
				  ,1
	END
	
	-- Avanzamos otro registro
	fetch next from CURSORITO
	into @idCurso, @idCicloLectivo
end
-- cerramos el cursor
    close CURSORITO

GO
/****** Object:  StoredProcedure [dbo].[CursoCicloLectivo_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CursoCicloLectivo_Insert]
  @idCursoCicloLectivo NUMERIC = NULL
 ,@idCursoCicloLectivoTransaccional NUMERIC = NULL
 ,@idCurso NUMERIC = NULL
 ,@idCicloLectivo NUMERIC = NULL
 ,@idPreceptor NUMERIC = NULL
 ,@idOrientacion NUMERIC = NULL
AS
 SET NOCOUNT off;
 
	select @idCurso=idCurso
 from Curso
 where idCursoTransaccional = @idCurso
 
	select @idCicloLectivo=idCicloLectivo
 from CicloLectivo
 where idCicloLectivoTransaccional = @idCicloLectivo
 
	select @idCursoCicloLectivo=idCursoCicloLectivo
 from CursosCicloLectivo
 where idCursoCicloLectivoTransaccional=@idCursoCicloLectivoTransaccional

	select @idOrientacion = idOrientacion
 from Orientacion
 where idOrientacionTransaccional = @idOrientacion
			
	select @idPreceptor = idPersonal
from Personal 
where idPersonalTransaccional = @idPreceptor and idCargoPersonal= 41 -- Preceptor

if (@idCursoCicloLectivo is null or @idCursoCicloLectivo=0)
begin
    INSERT INTO [CursosCicloLectivo]
           ([idCursoCicloLectivoTransaccional]
           ,[idCurso]
           ,[idCicloLectivo]
           ,[idPreceptor]
		   ,[idOrientacion])
	 VALUES(
		 @idCursoCicloLectivoTransaccional
		,@idCurso
		,@idCicloLectivo
		,@idPreceptor
		,@idOrientacion )
		
		SET @idCursoCicloLectivo = @@IDENTITY
		
 declare @descripcion VARCHAR(250) = 'Agenda generada automáticamente'
 declare @fechaCreacion DATE = getdate()
 declare @activo BIT = 1
		
		EXECUTE [AgendaActividades_Insert] 
				   @idCursoCicloLectivo
				  ,@descripcion
				  ,@fechaCreacion
				  ,@activo
 end
 else
	 begin
		 EXECUTE [CursosCicloLectivo_Update] 
			 @idCursoCicloLectivo
			,@idCursoCicloLectivoTransaccional
			,@idCurso
			,@idCicloLectivo
			,@idPreceptor
			,@idOrientacion
	 end

GO
/****** Object:  StoredProcedure [dbo].[CursoCicloLectivo_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 20/06/11
-- =============================================
CREATE PROCEDURE [dbo].[CursoCicloLectivo_Select]
  @idCurso NUMERIC = NULL
 ,@idCursoTransaccional NUMERIC = NULL
 ,@idNivel NUMERIC = NULL
 ,@idPreceptor NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@idCicloLectivo NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    SELECT 
		--C.idCurso,
		 C.idCursoTransaccional
		,C.idNivel
		,C.idPreceptor
		,C.nombre
		,C.idCicloLectivo
		,CCL.idCursoCicloLectivo as 'idCurso'
    FROM Curso AS C
		INNER JOIN [CursosCicloLectivo] AS CCL ON C.idCurso = CCL.idCurso
		INNER JOIN [CicloLectivo] AS CL ON CCL.idCicloLectivo = CL.idCicloLectivo
    WHERE (@idCurso IS NULL OR @idCurso=C.idCurso)
		AND (@idCursoTransaccional IS NULL OR @idCursoTransaccional=C.idCursoTransaccional)
		AND (@idNivel IS NULL OR @idNivel=C.idNivel)
		AND (@idPreceptor IS NULL OR @idPreceptor=C.idPreceptor)
		AND (@nombre IS NULL OR @nombre=C.nombre)
		AND (@idCicloLectivo IS NULL OR @idCicloLectivo=CL.idCicloLectivo)

GO
/****** Object:  StoredProcedure [dbo].[CursosCicloLectivo_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 07/08/11
-- =============================================
CREATE PROCEDURE [dbo].[CursosCicloLectivo_Select]
  @idCursoCicloLectivo NUMERIC = NULL
 ,@idCurso NUMERIC = NULL
 ,@idNivel NUMERIC = NULL
 ,@idCicloLectivo NUMERIC = NULL
 ,@username VARCHAR(50) = NULL
 ,@usernamePreceptor VARCHAR(50) = NULL
AS
 SET NOCOUNT OFF;

 
    SELECT DISTINCT
		 CCL.idCursoCicloLectivo as 'idCurso'
		,CCL.idCursoCicloLectivo
		--,CCL.idCurso
		,CCL.idCicloLectivo
		,N.nombre + ' ' + C.nombre as 'nombre'
		,CCL.idPreceptor
		,N.nombre as 'nivel'
		,N.idNivel
    FROM CursosCicloLectivo CCL 
		INNER JOIN Personal as PeP on CCL.idPreceptor = Pep.idPersonal
		INNER JOIN Personas as PPe on PeP.idPersona = PPe.idPersona
		INNER JOIN AsignaturaCicloLectivo AS AC 
			ON CCL.idCursoCicloLectivo = AC.idCursoCicloLectivo
		INNER JOIN Personal AS PD ON AC.idDocente = PD.idPersonal
		INNER JOIN Personas AS P ON PD.idPersona = P.idPersona
		INNER JOIN Curso as C ON CCL.idCurso = C.idCurso
		INNER JOIN Nivel as N on C.idNivel = N.idNivel
    WHERE 
		(@idCursoCicloLectivo IS NULL OR @idCursoCicloLectivo=CCL.idCursoCicloLectivo)
	AND (@idCurso IS NULL OR @idCurso=CCL.idCurso)
	AND (@idCicloLectivo IS NULL OR @idCicloLectivo=CCL.idCicloLectivo)
	AND (@username IS NULL OR @username = P.username)
	AND (@usernamePreceptor IS NULL OR @usernamePreceptor = PPe.username)
	AND (@idNivel IS NULL OR @idNivel = N.idNivel)
GO
/****** Object:  StoredProcedure [dbo].[CursosCicloLectivo_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 10/10/11
-- =============================================
CREATE PROCEDURE [dbo].[CursosCicloLectivo_Update]
  @idCursoCicloLectivo NUMERIC = NULL
 ,@idCursoCicloLectivoTransaccional NUMERIC = NULL
 ,@idCurso NUMERIC = NULL
 ,@idCicloLectivo NUMERIC = NULL
 ,@idPreceptor NUMERIC = NULL
 ,@idOrientacion NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    UPDATE CursosCicloLectivo SET 
      idCursoCicloLectivoTransaccional = @idCursoCicloLectivoTransaccional
     ,idCurso = @idCurso
     ,idCicloLectivo = @idCicloLectivo
     ,idPreceptor = @idPreceptor
	 ,idOrientacion = @idOrientacion
    WHERE (@idCursoCicloLectivo IS NULL OR @idCursoCicloLectivo=idCursoCicloLectivo)

GO
/****** Object:  StoredProcedure [dbo].[DatosIndicador_Inasistencias]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[DatosIndicador_Inasistencias]	
	@FechaDesde DATETIME,
	@FechaHasta DATETIME,
	@idCursoCicloLectivo NUMERIC
AS
BEGIN	
	SET NOCOUNT ON;

	DECLARE @parametroCantidad NUMERIC = 0
	DECLARE @idAlumno NUMERIC = 0
	DECLARE @idAlumnoPrevio NUMERIC = 0
	DECLARE @cantidadAlumnos NUMERIC = 0
	DECLARE @cantidadFaltasAlumno NUMERIC = 0
	DECLARE @nombreAlumno VARCHAR(50) = ''
	DECLARE @apellidoAlumno VARCHAR(50) = ''
	DECLARE @fecha DATE = GETDATE()
	DECLARE @listaAlumnos VARCHAR(1000)=''
    DECLARE @faltas NUMERIC(3,2) = 0;
		
	SELECT @parametroCantidad = parametroCantidad
	FROM dbo.Indicadores 
	WHERE nombreSP = 'Inasistencias'

	DECLARE ALUMNOS_Cursor CURSOR FOR 
	SELECT
	  ALC.idAlumnoCursoCicloLectivo, 
	  P.nombre AS [alumno_nombre], 
	  P.apellido AS [alumno_apellido],
	  AST.fecha,
	  TA.valor
	FROM	[AlumnoCursoCicloLectivo] AS ALC 
			INNER JOIN [CursosCicloLectivo] AS CCL ON ALC.idCursoCicloLectivo = CCL.idCursoCicloLectivo
			INNER JOIN [CicloLectivo] AS CL ON CCL.idCicloLectivo = CL.idCicloLectivo AND CL.activo = 1
			INNER JOIN [Asistencia] AS AST ON AST.idAlumnoCursoCicloLectivo = ALC.idAlumnoCursoCicloLectivo
			INNER JOIN [Alumnos] AS A ON A.idAlumno = ALC.idAlumno
			INNER JOIN [Personas] AS P ON A.idPersona = P.idPersona
			INNER JOIN [TipoAsistencia] AS TA ON AST.idTipoAsistencia=TA.idTipoAsistencia
	WHERE 
		TA.idTipoAsistencia <> 13
		AND 
		@idCursoCicloLectivo = CCL.idCursoCicloLectivo
		AND
		AST.fecha BETWEEN @fechaDesde AND @fechaHasta
	ORDER BY 1;

	SET @idAlumnoPrevio = 0
	
	CREATE TABLE #TablaTemporal (idalumno INT, Nombre VARCHAR(50), Apellido VARCHAR(50), Fecha DATE, Faltas NUMERIC(3,2) )

	OPEN ALUMNOS_Cursor;
	FETCH NEXT FROM ALUMNOS_Cursor INTO @idAlumno,@nombreAlumno,@apellidoAlumno,@fecha,@faltas
	WHILE @@FETCH_STATUS = 0
		BEGIN
			IF(@idAlumno <> @idAlumnoPrevio)
				BEGIN
					IF (@cantidadFaltasAlumno > @parametroCantidad)
						SET @listaAlumnos = CAST( @idAlumnoPrevio AS VARCHAR) + ',' + @listaAlumnos
					SET @cantidadFaltasAlumno = 0
				END
			
			SET	@cantidadFaltasAlumno = @cantidadFaltasAlumno + 1	
			SET @idAlumnoPrevio = @idAlumno
			
			INSERT INTO #TablaTemporal (
				idalumno,
				Nombre,
				Apellido,
				Fecha,
			  Faltas
			) VALUES ( 
				/* idAlumno - NUMERIC*/ @idAlumno,
				/* nombre - VARCHAR(50) */ @nombreAlumno,
				/* apellido - VARCHAR(50) */ @apellidoAlumno,
				/* Fecha - DATE */ @fecha,
                @faltas ) ;
			
		  FETCH NEXT FROM ALUMNOS_Cursor INTO @idAlumno,@nombreAlumno,@apellidoAlumno,@fecha,@faltas
		END;
		-- PARA EVALUAR COMO QUEDO EL ÚLTIMO ALUMNO
		IF (@cantidadFaltasAlumno > @parametroCantidad)
			SET @listaAlumnos =  CAST(@idAlumnoPrevio AS VARCHAR) + ',' + @listaAlumnos;
		
		DELETE FROM #TablaTemporal
		 WHERE idalumno NOT IN (SELECT * FROM [ArrayToTable] (@listaAlumnos))
		
		SELECT 
			   Apellido,
			   Nombre,
			   REPLICATE('0',(2 - LEN(CAST(DATEPART(dd,fecha) AS VARCHAR(2))))) + CAST(DATEPART(dd,fecha) AS VARCHAR(2))
			   + '/' +
			   REPLICATE('0',(2 - LEN(CAST(DATEPART(mm,fecha) AS VARCHAR(2))))) + CAST(DATEPART(mm,fecha) AS VARCHAR(2))
			   + '/' +
			   CAST(DATEPART(yyyy,fecha) AS VARCHAR(4)) AS Fecha,
			   Faltas
		FROM #TablaTemporal ORDER BY apellido,nombre,fecha ASC
		
		DROP TABLE #TablaTemporal
	CLOSE ALUMNOS_Cursor;
	DEALLOCATE ALUMNOS_Cursor;
END



GO
/****** Object:  StoredProcedure [dbo].[DatosIndicador_Inasistencias_Modificado]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[DatosIndicador_Inasistencias_Modificado]	
	@FechaDesde DATETIME,
	@FechaHasta DATETIME,
	@idCursoCicloLectivo NUMERIC
AS
BEGIN	
	SET NOCOUNT ON;

	DECLARE @parametroCantidad NUMERIC = 0
	DECLARE @idAlumno NUMERIC = 0
	DECLARE @idAlumnoPrevio NUMERIC = 0
	DECLARE @cantidadAlumnos NUMERIC = 0
	DECLARE @cantidadFaltasAlumno NUMERIC = 0
	DECLARE @nombreAlumno VARCHAR(50) = ''
	DECLARE @apellidoAlumno VARCHAR(50) = ''
	DECLARE @fecha DATE = GETDATE()
	DECLARE @listaAlumnos VARCHAR(1000)=''
  DECLARE @faltas NUMERIC(3,2) = 0;
		
	SELECT @parametroCantidad = parametroCantidad
	FROM dbo.Indicadores 
	WHERE nombreSP = 'Inasistencias'

	DECLARE ALUMNOS_Cursor CURSOR FOR 
	SELECT
	  ALC.idAlumnoCursoCicloLectivo, 
	  P.nombre AS [alumno_nombre], 
	  P.apellido AS [alumno_apellido],
	  AST.fecha,
		TA.valor
	FROM	[AlumnoCursoCicloLectivo] AS ALC 
			INNER JOIN [CursosCicloLectivo] AS CCL ON ALC.idCursoCicloLectivo = CCL.idCursoCicloLectivo
			INNER JOIN [CicloLectivo] AS CL ON CCL.idCicloLectivo = CL.idCicloLectivo AND CL.activo = 1
			INNER JOIN [Asistencia] AS AST ON AST.idAlumnoCursoCicloLectivo = ALC.idAlumnoCursoCicloLectivo
			INNER JOIN [Alumnos] AS A ON A.idAlumno = ALC.idAlumno
			INNER JOIN [Personas] AS P ON A.idPersona = P.idPersona
			INNER JOIN [TipoAsistencia] AS TA ON AST.idTipoAsistencia=TA.idTipoAsistencia
	WHERE 
		TA.idTipoAsistencia <> 13
		AND 
		@idCursoCicloLectivo = CCL.idCursoCicloLectivo
		AND
		AST.fecha BETWEEN @fechaDesde AND @fechaHasta
	ORDER BY 1;

	SET @idAlumnoPrevio = 0
	
	CREATE TABLE #TablaTemporal (idalumno INT, Nombre VARCHAR(50), Apellido VARCHAR(50), Fecha DATE, Faltas NUMERIC(3,2) )

	OPEN ALUMNOS_Cursor;
	FETCH NEXT FROM ALUMNOS_Cursor INTO @idAlumno,@nombreAlumno,@apellidoAlumno,@fecha,@faltas
	WHILE @@FETCH_STATUS = 0
		BEGIN
			IF(@idAlumno <> @idAlumnoPrevio)
				BEGIN
					IF (@cantidadFaltasAlumno > @parametroCantidad)
						SET @listaAlumnos = CAST( @idAlumnoPrevio AS VARCHAR) + ',' + @listaAlumnos
					SET @cantidadFaltasAlumno = 0
				END
			
			SET	@cantidadFaltasAlumno = @cantidadFaltasAlumno + 1	
			SET @idAlumnoPrevio = @idAlumno
			
			INSERT INTO #TablaTemporal (
				idalumno,
				Nombre,
				Apellido,
				Fecha,
			  Faltas
			) VALUES ( 
				/* idAlumno - NUMERIC*/ @idAlumno,
				/* nombre - VARCHAR(50) */ @nombreAlumno,
				/* apellido - VARCHAR(50) */ @apellidoAlumno,
				/* Fecha - DATE */ @fecha,
        @faltas ) ;
			
		  FETCH NEXT FROM ALUMNOS_Cursor INTO @idAlumno,@nombreAlumno,@apellidoAlumno,@fecha,@faltas
		END;
		-- PARA EVALUAR COMO QUEDO EL ÚLTIMO ALUMNO
		IF (@cantidadFaltasAlumno > @parametroCantidad)
			SET @listaAlumnos =  CAST(@idAlumnoPrevio AS VARCHAR) + ',' + @listaAlumnos;
		
		DELETE FROM #TablaTemporal
		 WHERE idalumno NOT IN (SELECT * FROM [ArrayToTable] (@listaAlumnos))
		
		SELECT 
			   Apellido,
			   Nombre,
			   REPLICATE('0',(2 - LEN(CAST(DATEPART(dd,fecha) AS VARCHAR(2))))) + CAST(DATEPART(dd,fecha) AS VARCHAR(2))
			   + '/' +
			   REPLICATE('0',(2 - LEN(CAST(DATEPART(mm,fecha) AS VARCHAR(2))))) + CAST(DATEPART(mm,fecha) AS VARCHAR(2))
			   + '/' +
			   CAST(DATEPART(yyyy,fecha) AS VARCHAR(4)) AS Fecha,
				 Faltas
		FROM #TablaTemporal ORDER BY apellido,nombre,fecha ASC
		
		DROP TABLE #TablaTemporal
	CLOSE ALUMNOS_Cursor;
	DEALLOCATE ALUMNOS_Cursor;
END



GO
/****** Object:  StoredProcedure [dbo].[DatosIndicador_InasistenciasDocentes]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DatosIndicador_InasistenciasDocentes]
	@FechaDesde DATETIME,
	@FechaHasta DATETIME,
	@idCursoCicloLectivo NUMERIC
AS
BEGIN	
	SET NOCOUNT ON;

	SELECT
	  P.apellido AS [Apellido],
	  P.nombre AS [Nombre], 
	  REPLICATE('0',(2 - LEN(CAST(DATEPART(dd,EA.fechaEvento) AS VARCHAR(2))))) + CAST(DATEPART(dd,EA.fechaEvento) AS VARCHAR(2))
	   + '/' +
	   REPLICATE('0',(2 - LEN(CAST(DATEPART(mm,EA.fechaEvento) AS VARCHAR(2))))) + CAST(DATEPART(mm,EA.fechaEvento) AS VARCHAR(2))
	   + '/' +
	   CAST(DATEPART(yyyy,EA.fechaEvento) AS VARCHAR(4)) AS [Fecha],
	   ASG.nombre as [Asignatura]
	FROM	
		dbo.RegistroClases AS RC 
		INNER JOIN dbo.TipoRegistroClases AS TRC ON RC.idTipoRegistroClases = TRC.idTipoRegistroClases
		INNER JOIN dbo.AsignaturaCicloLectivo AS ACL ON ACL.idAsignaturaCicloLectivo = RC.idAsignaturaCurso
		INNER JOIN dbo.EventoAgenda AS EA ON EA.idEventoAgenda = RC.idEventoAgenda
		INNER JOIN dbo.Personal AS D ON D.idPersonal = ACL.idDocente
		INNER JOIN Personas AS P ON D.idPersona = P.idPersona
		INNER JOIN Asignatura AS ASG ON ASG.idAsignatura = ACL.idAsignatura
	WHERE 
		TRC.idTipoRegistroClases IN (2,3,5)
		AND 
		@idCursoCicloLectivo = ACL.idCursoCicloLectivo
		AND
		EA.fechaEvento BETWEEN @fechaDesde AND @fechaHasta
	ORDER BY 1,2,3;

END

GO
/****** Object:  StoredProcedure [dbo].[DatosIndicador_Planificacion]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[DatosIndicador_Planificacion]	
	@FechaDesde DATETIME,
	@FechaHasta DATETIME,
	@idCursoCicloLectivo NUMERIC
AS
BEGIN	
	SET NOCOUNT ON;

CREATE TABLE #TablaTemporal (
	planificados INT,
	dictados INT,
	asignatura VARCHAR (80)
) 

INSERT INTO #TablaTemporal SELECT
	(
		SELECT
			COUNT (
				DISTINCT TPTC.idTemaContenido
			)
		
		FROM
				CicloLectivo AS CL
		INNER JOIN
	CursosCicloLectivo AS CCL ON CL.idCicloLectivo = CCL.idCicloLectivo AND CL.activo = 1
		INNER JOIN
	AsignaturaCicloLectivo AS ACL ON CCL.idCursoCicloLectivo = ACL.idCursoCicloLectivo
		INNER JOIN 
	Curricula AS C ON C.idAsignatura = ACL.idAsignatura
		INNER JOIN
	PlanificacionAnual AS PA ON PA.idCurricula = C.idCurricula
		INNER JOIN 
	TemaPlanificacionAnual AS TPA ON PA.idPlanificacionAnual = TPA.idPlanificacionAnual
		INNER JOIN
	TemaPlanificacionTemaContenido AS TPTC ON TPA.idTemaPlanificacion = TPTC.idTemaPlanificacion
		INNER JOIN 
	TemaContenido AS TC ON TC.idTemaContenido = TPTC.idTemaContenido AND TC.activo = 1

		WHERE
			TPA.fechaInicioEstimada BETWEEN @FechaDesde AND @FechaHasta
		AND TPA.fechaFinEstimada BETWEEN @FechaDesde AND @FechaHasta
		AND CCL.idCursoCicloLectivo = @idCursoCicloLectivo
		AND ACL.idAsignaturaCicloLectivo = ACL3.idAsignaturaCicloLectivo
	) AS 'planificados',
	ISNULL(
		(
			SELECT
				COUNT (DISTINCT DRC2.idTemaContenido)
			
			FROM
				CicloLectivo AS CL2
			INNER JOIN CursosCicloLectivo AS CCL2 ON CL2.idCicloLectivo = CCL2.idCicloLectivo
			AND CL2.activo = 1
			INNER JOIN AsignaturaCicloLectivo AS ACL2 ON CCL2.idCursoCicloLectivo = ACL2.idCursoCicloLectivo
			INNER JOIN Asignatura AS A ON ACL2.idAsignatura = A.idAsignatura
			LEFT OUTER JOIN RegistroClases AS RC ON RC.idAsignaturaCurso = ACL2.idAsignaturaCicloLectivo
			LEFT OUTER JOIN EventoAgenda AS EA2 ON RC.idEventoAgenda = EA2.idEventoAgenda
			LEFT OUTER JOIN DetalleRegistroClases AS DRC2 ON RC.idRegistroClases = DRC2.idRegistroClases
			LEFT OUTER JOIN	TemaContenido AS TC ON TC.idTemaContenido = DRC2.idTemaContenido AND TC.activo = 1
			WHERE
				ACL2.idAsignaturaCicloLectivo = ACL3.idAsignaturaCicloLectivo
			AND EA2.fechaEvento BETWEEN @FechaDesde AND @FechaHasta
			AND CCL2.idCursoCicloLectivo = @idCursoCicloLectivo
			
			GROUP BY
				ACL2.idAsignaturaCicloLectivo,
				A.nombre
		),
		0
	) AS 'dictados',
	A.nombre
FROM
	CicloLectivo AS CL2
INNER JOIN CursosCicloLectivo AS CCL2 ON CL2.idCicloLectivo = CCL2.idCicloLectivo
AND CL2.activo = 1
INNER JOIN AsignaturaCicloLectivo AS ACL3 ON CCL2.idCursoCicloLectivo = ACL3.idCursoCicloLectivo
INNER JOIN Asignatura AS A ON ACL3.idAsignatura = A.idAsignatura
WHERE
	CCL2.idCursoCicloLectivo = @idCursoCicloLectivo 

SELECT
		*
	FROM
		#TablaTemporal
	WHERE
		NOT (
			planificados = 0
			AND dictados = 0
		) 

DROP TABLE #TablaTemporal
	
END


GO
/****** Object:  StoredProcedure [dbo].[DatosIndicador_PromedioGeneral]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DatosIndicador_PromedioGeneral]
	@FechaDesde DATETIME,
	@FechaHasta DATETIME,
	@idCursoCicloLectivo NUMERIC
AS
BEGIN	
	SET NOCOUNT ON;

	DECLARE @parametroCantidad NUMERIC = 0
	DECLARE @nivelAprobacion NUMERIC = 0
	DECLARE @idAlumno NUMERIC = 0
	DECLARE @idAlumnoPrevio NUMERIC = 0
	DECLARE @cantidadAlumnos NUMERIC = 0
	DECLARE @cantidadMateriasAlumno NUMERIC = 0
	DECLARE @nombreAlumno VARCHAR(50) = ''
	DECLARE @apellidoAlumno VARCHAR(50) = ''
	DECLARE @asignatura VARCHAR(50) = ''
	DECLARE @promedio FLOAT = 0
	DECLARE @listaAlumnos VARCHAR(1000)=''
		
	--SELECT @parametroCantidad = parametroCantidad
	--FROM dbo.Indicadores 
	--WHERE nombreSP = 'Reprobadas'

	--SELECT @nivelAprobacion = MIN(CAST(valor AS NUMERIC))
	--FROM dbo.ValoresEscalaCalificacion
	--WHERE aprobado = 1 AND descripcion <> '%conceptual%'

	--DECLARE ALUMNOS_Cursor CURSOR FOR 
	SELECT
	  --ALC.idAlumnoCursoCicloLectivo, 
	  P.apellido AS [Apellido],
	  P.nombre AS [Nombre], 
	  ROUND(AVG(CAST(VES.nombre as float)),2,2) AS [Promedio]
	FROM	[AlumnoCursoCicloLectivo] AS ALC 
			INNER JOIN [CursosCicloLectivo] AS CCL ON ALC.idCursoCicloLectivo = CCL.idCursoCicloLectivo
			INNER JOIN [AsignaturaCicloLectivo] AS AC ON CCL.idCursoCicloLectivo = AC.idCursoCicloLectivo
			INNER JOIN [Asignatura] AS ASIG ON AC.idAsignatura = ASIG.idAsignatura
			INNER JOIN [Calificacion] AS CAL ON CAL.idAlumnoCursoCicloLectivo = ALC.idAlumnoCursoCicloLectivo and cal.idAsignaturaCicloLectivo=ac.idAsignatura
			INNER JOIN [ValoresEscalaCalificacion] AS VES ON CAL.idValorCalificacion = VES.idValorEscalaCalificacion
			INNER JOIN [CicloLectivo] AS CL ON CCL.idCicloLectivo = CL.idCicloLectivo AND CL.activo = 1
			INNER JOIN [Alumnos] AS A ON A.idAlumno = ALC.idAlumno
			INNER JOIN [Personas] AS P ON A.idPersona = P.idPersona
	WHERE 
		@idCursoCicloLectivo = CCL.idCursoCicloLectivo
		AND
		CAL.fecha BETWEEN @fechaDesde AND @fechaHasta
	GROUP BY ALC.idAlumnoCursoCicloLectivo, P.apellido, P.nombre
	--HAVING ROUND(AVG(CAST(VES.nombre as float)),2,2) < @nivelAprobacion
	ORDER BY 1

	--SET @idAlumnoPrevio = 0
	
	--CREATE TABLE #TablaTemporal (idalumno INT, Nombre VARCHAR(50), Apellido VARCHAR(50), Asignatura VARCHAR(50), Promedio FLOAT )

	--OPEN ALUMNOS_Cursor;
	--FETCH NEXT FROM ALUMNOS_Cursor INTO @idAlumno,@promedio,@nombreAlumno,@apellidoAlumno,@asignatura
	--WHILE @@FETCH_STATUS = 0
	--	BEGIN
	--		IF(@idAlumno <> @idAlumnoPrevio)
	--			BEGIN
	--				--IF (@cantidadMateriasAlumno > @parametroCantidad)
	--					SET @listaAlumnos = CAST( @idAlumnoPrevio AS VARCHAR) + ',' + @listaAlumnos
	--				--SET @cantidadMateriasAlumno = 0
	--			END
			
	--		SET	@cantidadMateriasAlumno = @cantidadMateriasAlumno + 1	
	--		SET @idAlumnoPrevio = @idAlumno
			
	--		INSERT INTO #TablaTemporal (
	--		idalumno,
	--		nombre,
	--		apellido,
	--		asignatura,
	--		promedio
	--	) VALUES ( 
	--		/* idAlumno - NUMERIC*/ @idAlumno,
	--		/* nombre - VARCHAR(50) */ @nombreAlumno,
	--		/* apellido - VARCHAR(50) */ @apellidoAlumno,
	--		/* asignatura - VARCHAR(50) */ @asignatura,
	--		/* promedio - FLOAT */ @promedio );
			
	--	  FETCH NEXT FROM ALUMNOS_Cursor INTO @idAlumno,@promedio,@nombreAlumno,@apellidoAlumno,@asignatura
	--	END;
	--	-- PARA EVALUAR COMO QUEDO EL ÚLTIMO ALUMNO
	--	IF (@cantidadMateriasAlumno > @parametroCantidad)
	--		SET @listaAlumnos =  CAST(@idAlumnoPrevio AS VARCHAR) + ',' + @listaAlumnos;
		
	--	DELETE FROM #TablaTemporal
	--	 WHERE idalumno NOT IN (SELECT * FROM [ArrayToTable] (@listaAlumnos))
		
	--	SELECT 
	--		   Apellido,
	--		   Nombre,
	--		   Asignatura,
	--		   Promedio 
	--	FROM #TablaTemporal ORDER BY apellido,nombre
		
	--	DROP TABLE #TablaTemporal
	--CLOSE ALUMNOS_Cursor;
	--DEALLOCATE ALUMNOS_Cursor;
END



GO
/****** Object:  StoredProcedure [dbo].[DatosIndicador_Reprobadas]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DatosIndicador_Reprobadas]	
	@FechaDesde DATETIME,
	@FechaHasta DATETIME,
	@idCursoCicloLectivo NUMERIC
AS
BEGIN	
	SET NOCOUNT ON;

	DECLARE @parametroCantidad NUMERIC = 0
	DECLARE @nivelAprobacion NUMERIC = 0
	DECLARE @idAlumno NUMERIC = 0
	DECLARE @idAlumnoPrevio NUMERIC = 0
	DECLARE @cantidadAlumnos NUMERIC = 0
	DECLARE @cantidadMateriasAlumno NUMERIC = 0
	DECLARE @nombreAlumno VARCHAR(50) = ''
	DECLARE @apellidoAlumno VARCHAR(50) = ''
	DECLARE @asignatura VARCHAR(50) = ''
	DECLARE @promedio FLOAT = 0
	DECLARE @listaAlumnos VARCHAR(1000)=''
		
	SELECT @parametroCantidad = parametroCantidad
	FROM dbo.Indicadores 
	WHERE nombreSP = 'Reprobadas'

	SELECT @nivelAprobacion = MIN(CAST(valor AS NUMERIC))
	FROM dbo.ValoresEscalaCalificacion
	WHERE aprobado = 1 AND descripcion <> '%conceptual%'

	DECLARE ALUMNOS_Cursor CURSOR FOR 
	SELECT
	  ALC.idAlumnoCursoCicloLectivo, 
	  ROUND(AVG(CAST(VES.nombre as float)),2,2) AS [promedio], 
	  P.nombre AS [alumno_nombre], 
	  P.apellido AS [alumno_apellido], 
	  ASIG.nombre AS [asignatura]
	FROM	[AlumnoCursoCicloLectivo] AS ALC 
			INNER JOIN [CursosCicloLectivo] AS CCL ON ALC.idCursoCicloLectivo = CCL.idCursoCicloLectivo
			INNER JOIN [AsignaturaCicloLectivo] AS AC ON CCL.idCursoCicloLectivo = AC.idCursoCicloLectivo
			INNER JOIN [Asignatura] AS ASIG ON AC.idAsignatura = ASIG.idAsignatura
			INNER JOIN [Calificacion] AS CAL ON CAL.idAlumnoCursoCicloLectivo = ALC.idAlumnoCursoCicloLectivo and cal.idAsignaturaCicloLectivo=ac.idAsignatura
			INNER JOIN [ValoresEscalaCalificacion] AS VES ON CAL.idValorCalificacion = VES.idValorEscalaCalificacion
			INNER JOIN [CicloLectivo] AS CL ON CCL.idCicloLectivo = CL.idCicloLectivo AND CL.activo = 1
			INNER JOIN [Alumnos] AS A ON A.idAlumno = ALC.idAlumno
			INNER JOIN [Personas] AS P ON A.idPersona = P.idPersona
	WHERE 
		@idCursoCicloLectivo = CCL.idCursoCicloLectivo
		AND
		CAL.fecha BETWEEN @fechaDesde AND @fechaHasta
	GROUP BY ALC.idAlumnoCursoCicloLectivo, AC.idAsignaturaCicloLectivo, P.nombre, P.apellido, ASIG.nombre
	HAVING ROUND(AVG(CAST(VES.nombre as float)),2,2) < @nivelAprobacion
	ORDER BY 1;

	SET @idAlumnoPrevio = 0
	
	CREATE TABLE #TablaTemporal (idalumno INT, Nombre VARCHAR(50), Apellido VARCHAR(50), Asignatura VARCHAR(50), Promedio FLOAT )

	OPEN ALUMNOS_Cursor;
	FETCH NEXT FROM ALUMNOS_Cursor INTO @idAlumno,@promedio,@nombreAlumno,@apellidoAlumno,@asignatura
	WHILE @@FETCH_STATUS = 0
		BEGIN
			IF(@idAlumno <> @idAlumnoPrevio)
				BEGIN
					IF (@cantidadMateriasAlumno >= @parametroCantidad)
						SET @listaAlumnos = CAST( @idAlumnoPrevio AS VARCHAR) + ',' + @listaAlumnos
					SET @cantidadMateriasAlumno = 0
				END
			
			SET	@cantidadMateriasAlumno = @cantidadMateriasAlumno + 1	
			SET @idAlumnoPrevio = @idAlumno
			
			INSERT INTO #TablaTemporal (
			idalumno,
			nombre,
			apellido,
			asignatura,
			promedio
		) VALUES ( 
			/* idAlumno - NUMERIC*/ @idAlumno,
			/* nombre - VARCHAR(50) */ @nombreAlumno,
			/* apellido - VARCHAR(50) */ @apellidoAlumno,
			/* asignatura - VARCHAR(50) */ @asignatura,
			/* promedio - FLOAT */ @promedio );
			
		  FETCH NEXT FROM ALUMNOS_Cursor INTO @idAlumno,@promedio,@nombreAlumno,@apellidoAlumno,@asignatura
		END;
		-- PARA EVALUAR COMO QUEDO EL ÚLTIMO ALUMNO
		IF (@cantidadMateriasAlumno > @parametroCantidad)
			SET @listaAlumnos =  CAST(@idAlumnoPrevio AS VARCHAR) + ',' + @listaAlumnos;
		
		DELETE FROM #TablaTemporal
		 WHERE idalumno NOT IN (SELECT * FROM [ArrayToTable] (@listaAlumnos))
		
		SELECT 
			   Apellido,
			   Nombre,
			   Asignatura,
			   Promedio 
		FROM #TablaTemporal ORDER BY apellido,nombre
		
		DROP TABLE #TablaTemporal
	CLOSE ALUMNOS_Cursor;
	DEALLOCATE ALUMNOS_Cursor;
END


GO
/****** Object:  StoredProcedure [dbo].[DatosIndicador_Sanciones]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DatosIndicador_Sanciones]

	@fechaDesde DATETIME,
	@fechaHasta DATETIME,
	@idCursoCicloLectivo NUMERIC
AS
BEGIN	
	SET NOCOUNT ON;

	DECLARE @parametroCantidad NUMERIC = 0
		
	SELECT @parametroCantidad = parametroCantidad
	FROM dbo.Indicadores 
	WHERE nombreSP = 'Sanciones'

		SELECT 
	P.apellido AS [Apellido],
	P.nombre AS [Nombre], 
	REPLICATE('0',(2 - LEN(CAST(DATEPART(dd,fecha) AS VARCHAR(2))))) + CAST(DATEPART(dd,fecha) AS VARCHAR(2))
	+ '/' +
	REPLICATE('0',(2 - LEN(CAST(DATEPART(mm,fecha) AS VARCHAR(2))))) + CAST(DATEPART(mm,fecha) AS VARCHAR(2))
	+ '/' +
	CAST(DATEPART(yyyy,fecha) AS VARCHAR(4)) AS Fecha,
	S.cantidad AS [Amonestaciones],
	MS.[Descripcion] AS [Motivo]
FROM	[AlumnoCursoCicloLectivo] AS ALC 
	INNER JOIN [CursosCicloLectivo] AS CCL ON ALC.idCursoCicloLectivo = CCL.idCursoCicloLectivo
	INNER JOIN [CicloLectivo] AS CL ON CCL.idCicloLectivo = CL.idCicloLectivo AND CL.activo = 1
	INNER JOIN [Sancion] AS S ON ALC.idAlumnoCursoCicloLectivo = S.idAlumnoCursoCicloLectivo
	INNER JOIN [TipoSancion] AS TP ON S.idTipoSancion = TP.idTipoSancion
	INNER JOIN [MotivoSancion] AS MS ON S.idMotivoSancion = MS.idMotivoSancion
	INNER JOIN [Alumnos] AS A ON A.idAlumno = ALC.idAlumno
	INNER JOIN [Personas] AS P ON A.idPersona = P.idPersona
WHERE 
	ALC.idAlumnoCursoCicloLectivo IN
	( 
		SELECT S.idAlumnoCursoCicloLectivo
		FROM Sancion AS S
			INNER JOIN dbo.TipoSancion AS TP ON S.idTipoSancion = TP.idTipoSancion
			INNER JOIN dbo.AlumnoCursoCicloLectivo AS ACCL ON ACCL.idAlumnoCursoCicloLectivo = S.idAlumnoCursoCicloLectivo
		WHERE S.fecha BETWEEN @FechaDesde AND @FechaHasta
			AND ACCL.idCursoCicloLectivo = @idCursoCicloLectivo
			AND S.idTipoSancion = 4
		GROUP BY S.idAlumnoCursoCicloLectivo
		HAVING SUM(S.cantidad) > @parametroCantidad
	)

END
GO
/****** Object:  StoredProcedure [dbo].[DatosIndicador_Sanciones_old]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DatosIndicador_Sanciones_old]

	@FechaDesde DATETIME,
	@FechaHasta DATETIME,
	@idCursoCicloLectivo NUMERIC
AS
BEGIN	
	SET NOCOUNT ON;

	DECLARE @parametroCantidad NUMERIC = 0
	DECLARE @idAlumno NUMERIC = 0
	DECLARE @idAlumnoPrevio NUMERIC = 0
	DECLARE @cantidadAlumnos NUMERIC = 0
	DECLARE @cantidadSancionesAlumno NUMERIC = 0
	DECLARE @nombreAlumno VARCHAR(50) = ''
	DECLARE @apellidoAlumno VARCHAR(50) = ''
	DECLARE @fecha DATE = GETDATE()
	DECLARE @listaAlumnos VARCHAR(1000)=''
	DECLARE @cantidadSanciones NUMERIC = 0
		
	SELECT @parametroCantidad = parametroCantidad
	FROM dbo.Indicadores 
	WHERE nombreSP = 'Sanciones'

	DECLARE ALUMNOS_Cursor CURSOR FOR 
	SELECT
	  ALC.idAlumnoCursoCicloLectivo, 
	  P.nombre AS [alumno_nombre], 
	  P.apellido AS [alumno_apellido],
	  S.fecha,
		SUM(S.cantidad)
	FROM	[AlumnoCursoCicloLectivo] AS ALC 
			INNER JOIN [CursosCicloLectivo] AS CCL ON ALC.idCursoCicloLectivo = CCL.idCursoCicloLectivo
			INNER JOIN [CicloLectivo] AS CL ON CCL.idCicloLectivo = CL.idCicloLectivo AND CL.activo = 1
			INNER JOIN [Sancion] AS S ON ALC.idAlumnoCursoCicloLectivo = S.idAlumnoCursoCicloLectivo
			INNER JOIN [TipoSancion] AS TP ON S.idTipoSancion = TP.idTipoSancion
			INNER JOIN [Alumnos] AS A ON A.idAlumno = ALC.idAlumno
			INNER JOIN [Personas] AS P ON A.idPersona = P.idPersona
	WHERE 
		@idCursoCicloLectivo = CCL.idCursoCicloLectivo
		AND
		S.fecha BETWEEN @fechaDesde AND @fechaHasta
	  AND S.idTipoSancion = 4  
	GROUP BY ALC.idAlumnoCursoCicloLectivo,P.nombre,P.apellido,S.fecha	
	ORDER BY 1;

	SET @idAlumnoPrevio = 0
	
	CREATE TABLE #TablaTemporal (idalumno INT, Nombre VARCHAR(50), Apellido VARCHAR(50), Fecha DATE, Cantidad INT )

	OPEN ALUMNOS_Cursor;
	FETCH NEXT FROM ALUMNOS_Cursor INTO @idAlumno,@nombreAlumno,@apellidoAlumno,@fecha, @cantidadSanciones
	WHILE @@FETCH_STATUS = 0
		BEGIN
			IF(@idAlumno <> @idAlumnoPrevio)
				BEGIN
					IF (@cantidadSancionesAlumno > @parametroCantidad)
						SET @listaAlumnos = CAST( @idAlumnoPrevio AS VARCHAR) + ',' + @listaAlumnos
					SET @cantidadSancionesAlumno = 0
				END
			
			SET	@cantidadSancionesAlumno = @cantidadSancionesAlumno + @cantidadSanciones	
			SET @idAlumnoPrevio = @idAlumno
			
			INSERT INTO #TablaTemporal (
				idalumno,
				Nombre,
				Apellido,
				Fecha,
				Cantidad
			) VALUES ( 
				/* idAlumno - NUMERIC*/ @idAlumno,
				/* nombre - VARCHAR(50) */ @nombreAlumno,
				/* apellido - VARCHAR(50) */ @apellidoAlumno,
				/* Fecha - DATE */ @fecha,
				@cantidadSancionesAlumno) ;
			
		  FETCH NEXT FROM ALUMNOS_Cursor INTO @idAlumno,@nombreAlumno,@apellidoAlumno,@fecha, @cantidadSancionesAlumno
		END;
		-- PARA EVALUAR COMO QUEDO EL ÚLTIMO ALUMNO
		IF (@cantidadSancionesAlumno > @parametroCantidad)
			SET @listaAlumnos =  CAST(@idAlumnoPrevio AS VARCHAR) + ',' + @listaAlumnos;
		
		DELETE FROM #TablaTemporal
		 WHERE idalumno NOT IN (SELECT * FROM [ArrayToTable] (@listaAlumnos))
		
		SELECT 
			   Apellido,
			   Nombre,
			   REPLICATE('0',(2 - LEN(CAST(DATEPART(dd,fecha) AS VARCHAR(2))))) + CAST(DATEPART(dd,fecha) AS VARCHAR(2))
			   + '/' +
			   REPLICATE('0',(2 - LEN(CAST(DATEPART(mm,fecha) AS VARCHAR(2))))) + CAST(DATEPART(mm,fecha) AS VARCHAR(2))
			   + '/' +
			   CAST(DATEPART(yyyy,fecha) AS VARCHAR(4)) AS Fecha
				 ,Cantidad
		FROM #TablaTemporal ORDER BY apellido,nombre,fecha ASC
		
		DROP TABLE #TablaTemporal
	CLOSE ALUMNOS_Cursor;
	DEALLOCATE ALUMNOS_Cursor;
END
GO
/****** Object:  StoredProcedure [dbo].[DEC_ConfigFuncionPreferencia_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 02/02/13
-- =============================================
CREATE PROCEDURE [dbo].[DEC_ConfigFuncionPreferencia_Select]
  @idConfigFuncionPreferencia NUMERIC = NULL
 ,@idFuncionPreferencia NUMERIC = NULL
 ,@idIndicador NUMERIC = NULL
 ,@idValorFuncionPreferencia NUMERIC = NULL
 ,@valorDefault NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    SELECT idConfigFuncionPreferencia
    ,idIndicador
	,idFuncionPreferencia
    ,idValorFuncionPreferencia
    ,valorDefault
    FROM DEC_ConfigFuncionPreferencia
    WHERE (@idConfigFuncionPreferencia IS NULL OR @idConfigFuncionPreferencia=idConfigFuncionPreferencia)
   AND (@idFuncionPreferencia IS NULL OR @idFuncionPreferencia=idFuncionPreferencia)
   AND (@idIndicador IS NULL OR @idIndicador=idIndicador)
   AND (@idValorFuncionPreferencia IS NULL OR @idValorFuncionPreferencia=idValorFuncionPreferencia)
   AND (@valorDefault IS NULL OR @valorDefault=valorDefault)
GO
/****** Object:  StoredProcedure [dbo].[DEC_ConfigFuncionPreferencia_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 26/02/13
-- =============================================
CREATE PROCEDURE [dbo].[DEC_ConfigFuncionPreferencia_Update]
  @idConfigFuncionPreferencia NUMERIC = NULL
 ,@valorDefault NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    UPDATE DEC_ConfigFuncionPreferencia SET 
     valorDefault = @valorDefault
    WHERE  @idConfigFuncionPreferencia=idConfigFuncionPreferencia

GO
/****** Object:  StoredProcedure [dbo].[DEC_FuncionPreferencia_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 02/02/13
-- =============================================
CREATE PROCEDURE [dbo].[DEC_FuncionPreferencia_Select]
  @idFuncionPreferencia NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@ayuda VARCHAR(1000) = NULL
AS
 SET NOCOUNT OFF;

    SELECT idFuncionPreferencia
    ,nombre
    ,ayuda
    FROM DEC_FuncionPreferencia
    WHERE (@idFuncionPreferencia IS NULL OR @idFuncionPreferencia=idFuncionPreferencia)
   AND (@nombre IS NULL OR @nombre=nombre)
   AND (@ayuda IS NULL OR @ayuda=ayuda)
GO
/****** Object:  StoredProcedure [dbo].[DEC_Indicador_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 02/02/13
-- =============================================
CREATE PROCEDURE [dbo].[DEC_Indicador_Select]
  @idIndicador NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@pesoDefault DECIMAL(4, 2) = NULL
 ,@escala VARCHAR(50) = NULL
 ,@pesoMinimo DECIMAL(4, 2) = NULL
 ,@pesoMaximo DECIMAL(4, 2) = NULL
 ,@maximiza BIT = NULL
AS
 SET NOCOUNT OFF;

    SELECT idIndicador
    ,nombre
    ,pesoDefault
    ,escala
    ,pesoMinimo
    ,pesoMaximo
    ,maximiza
    FROM DEC_Indicador
    WHERE (@idIndicador IS NULL OR @idIndicador=idIndicador)
   AND (@nombre IS NULL OR @nombre=nombre)
   AND (@pesoDefault IS NULL OR @pesoDefault=pesoDefault)
   AND (@escala IS NULL OR @escala=escala)
   AND (@pesoMinimo IS NULL OR @pesoMinimo=pesoMinimo)
   AND (@pesoMaximo IS NULL OR @pesoMaximo=pesoMaximo)
   AND (@maximiza IS NULL OR @maximiza=maximiza)
GO
/****** Object:  StoredProcedure [dbo].[DEC_Indicador_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 25/02/13
-- =============================================
CREATE PROCEDURE [dbo].[DEC_Indicador_Update]
  @idIndicador NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@pesoDefault DECIMAL(4, 2) = NULL
 ,@escala VARCHAR(50) = NULL
 ,@pesoMinimo DECIMAL(4, 2) = NULL
 ,@pesoMaximo DECIMAL(4, 2) = NULL
 ,@maximiza BIT = NULL
AS
 SET NOCOUNT OFF;

    UPDATE DEC_Indicador SET 
      nombre = @nombre
     ,pesoDefault = @pesoDefault
     ,escala = @escala
     ,pesoMinimo = @pesoMinimo
     ,pesoMaximo = @pesoMaximo
     ,maximiza = @maximiza
    WHERE @idIndicador=idIndicador
  
GO
/****** Object:  StoredProcedure [dbo].[DEC_ValorFuncionPreferencia_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 02/02/13
-- =============================================
CREATE PROCEDURE [dbo].[DEC_ValorFuncionPreferencia_Select]
  @idValorFuncionPreferencia NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
AS
 SET NOCOUNT OFF;

    SELECT idValorFuncionPreferencia
    ,nombre
    FROM DEC_ValorFuncionPreferencia
    WHERE (@idValorFuncionPreferencia IS NULL OR @idValorFuncionPreferencia=idValorFuncionPreferencia)
   AND (@nombre IS NULL OR @nombre=nombre)
GO
/****** Object:  StoredProcedure [dbo].[DetalleRegistroClases_Delete]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 19/08/12
-- =============================================
CREATE PROCEDURE [dbo].[DetalleRegistroClases_Delete]
   @idDetalleRegistroClases NUMERIC = NULL
  ,@idRegistroClases NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    DELETE FROM DetalleRegistroClases
    WHERE( @idDetalleRegistroClases IS NULL OR @idDetalleRegistroClases=idDetalleRegistroClases)
		AND
		( @idRegistroClases IS NULL OR @idRegistroClases=idRegistroClases)
GO
/****** Object:  StoredProcedure [dbo].[DetalleRegistroClases_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 19/08/12
-- =============================================
CREATE PROCEDURE [dbo].[DetalleRegistroClases_Insert]
  @idDetalleRegistroClases NUMERIC = NULL output
 ,@idRegistroClases NUMERIC = NULL
 ,@idTemaContenido NUMERIC = NULL
 ,@porcentaje INT = NULL
AS
 SET NOCOUNT OFF;

    INSERT INTO DetalleRegistroClases(
     idRegistroClases
    ,idTemaContenido
    ,porcentaje
 )
 VALUES(
     @idRegistroClases
    ,@idTemaContenido
    ,@porcentaje
 )
 
  SET @idDetalleRegistroClases = @@IDENTITY
 RETURN SCOPE_IDENTITY()
GO
/****** Object:  StoredProcedure [dbo].[DetalleRegistroClases_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 19/08/12
-- =============================================
CREATE PROCEDURE [dbo].[DetalleRegistroClases_Select]
  @idDetalleRegistroClases NUMERIC = NULL
 ,@idRegistroClases NUMERIC = NULL
 ,@idTemaContenido NUMERIC = NULL
 ,@porcentaje INT = NULL
AS
 SET NOCOUNT OFF;

    SELECT idDetalleRegistroClases
    ,idRegistroClases
    ,idTemaContenido
    ,porcentaje
    FROM DetalleRegistroClases
    WHERE (@idDetalleRegistroClases IS NULL OR @idDetalleRegistroClases=idDetalleRegistroClases)
   AND (@idRegistroClases IS NULL OR @idRegistroClases=idRegistroClases)
   AND (@idTemaContenido IS NULL OR @idTemaContenido=idTemaContenido)
   AND (@porcentaje IS NULL OR @porcentaje=porcentaje)
GO
/****** Object:  StoredProcedure [dbo].[DetalleRegistroClases_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 19/08/12
-- =============================================
CREATE PROCEDURE [dbo].[DetalleRegistroClases_Update]
  @idDetalleRegistroClases NUMERIC = NULL
 ,@idRegistroClases NUMERIC = NULL
 ,@idTemaContenido NUMERIC = NULL
 ,@porcentaje INT = NULL
AS
 SET NOCOUNT OFF;

    UPDATE DetalleRegistroClases SET 
      idRegistroClases = @idRegistroClases
     ,idTemaContenido = @idTemaContenido
     ,porcentaje = @porcentaje
    WHERE @idDetalleRegistroClases=idDetalleRegistroClases

GO
/****** Object:  StoredProcedure [dbo].[DiaHorario_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 20/07/11
-- =============================================
CREATE PROCEDURE [dbo].[DiaHorario_Insert]
  @idDiaHorario NUMERIC = 0 output 
 ,@idDiaSemana NUMERIC = NULL
 ,@idDiaHorarioTransaccional NUMERIC = NULL
 ,@idAsignatura NUMERIC = NULL
 ,@idCurso NUMERIC = NULL
 ,@idNivel NUMERIC = NULL
AS
 SET NOCOUNT OFF;

--SELECT @idAsignatura = Asignatura.idAsignatura
--FROM Asignatura
--WHERE Asignatura.idAsignaturaTransaccional = @idAsignatura

SELECT @idNivel = Nivel.idNivel
FROM Nivel
WHERE Nivel.idNivelTransaccional = @idNivel

SELECT @idCurso = Curso.idCurso
FROM Curso
WHERE Curso.idCursoTransaccional = @idCurso
AND Curso.idNivel =@idNivel

SELECT @idAsignatura = idAsignaturaCicloLectivo
FROM AsignaturaCicloLectivo
--WHERE AsignaturaCicloLectivo.idAsignatura = @idAsignatura
WHERE AsignaturaCicloLectivo.idAsignaturaCicloLectivoTransaccional = @idAsignatura
--WHERE @idAsignatura = idAsignatura and @idCurso = idCursoCicloLectivo

SELECT @idDiaHorario = idDiaHorario
FROM DiaHorario
WHERE DiaHorario.idDiaHorarioTransaccional = @idDiaHorarioTransaccional

IF (@idDiaHorario IS NULL OR @idDiaHorario=0)
  BEGIN
    INSERT INTO DiaHorario(
     idDiaSemana
    ,idDiaHorarioTransaccional
    ,idAsignaturaCicloLectivo
 )
 VALUES(
     @idDiaSemana
    ,@idDiaHorarioTransaccional
    ,@idAsignatura
 )
 SET @idDiaHorario = @@IDENTITY
 
 END
ELSE
BEGIN
	EXECUTE DiaHorario_Update
  @idDiaHorario  
 ,@idDiaSemana
 ,@idDiaHorarioTransaccional
 ,@idAsignatura
 ,@idCurso
 END
   RETURN SCOPE_IDENTITY()

GO
/****** Object:  StoredProcedure [dbo].[DiaHorario_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 20/07/11
-- =============================================
CREATE PROCEDURE [dbo].[DiaHorario_Select]
  @idDiaHorario NUMERIC = NULL
 ,@idDiaSemana NUMERIC = NULL
 ,@idModulo NUMERIC = NULL
 ,@idDiaHorarioTransaccional NUMERIC = NULL
 ,@idAsignaturaCurso NUMERIC = NULL
 ,@idCursoCicloLectivo NUMERIC = NULL

AS
 SET NOCOUNT OFF;

     SELECT idDiaHorario
    ,idDiaSemana
    ,idDiaHorarioTransaccional
    ,AsignaturaCicloLectivo.idAsignaturaCicloLectivo
    FROM DiaHorario
		INNER JOIN AsignaturaCicloLectivo 
		on DiaHorario.idAsignaturaCicloLectivo = AsignaturaCicloLectivo.idAsignaturaCicloLectivo
		--on DiaHorario.idAsignaturaCicloLectivo = AsignaturaCicloLectivo.idAsignatura
		WHERE 
		(@idDiaHorario IS NULL OR @idDiaHorario=idDiaHorario)
		AND
		(@idAsignaturaCurso IS NULL OR @idAsignaturaCurso=AsignaturaCicloLectivo.idAsignaturaCicloLectivo)
		AND
		(@idCursoCicloLectivo IS NULL OR @idCursoCicloLectivo=AsignaturaCicloLectivo.idCursoCicloLectivo)
	 

GO
/****** Object:  StoredProcedure [dbo].[DiaHorario_Select_bis]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 20/07/11
-- =============================================
CREATE PROCEDURE [dbo].[DiaHorario_Select_bis]
  @idDiaHorario NUMERIC = NULL
 ,@idDiaSemana NUMERIC = NULL
 ,@idModulo NUMERIC = NULL
 ,@idDiaHorarioTransaccional NUMERIC = NULL
 ,@idAsignaturaCurso NUMERIC = NULL
 ,@idCursoCicloLectivo NUMERIC = NULL
 ,@mes NUMERIC = NULL
AS
 SET NOCOUNT OFF;

     SELECT idDiaHorario
    ,idDiaSemana
    ,idDiaHorarioTransaccional
    ,AsignaturaCicloLectivo.idAsignaturaCicloLectivo
    FROM Feriados,
		DiaHorario,
		AsignaturaCicloLectivo 
		--on DiaHorario.idAsignaturaCicloLectivo = AsignaturaCicloLectivo.idAsignaturaCicloLectivo
		--on DiaHorario.idAsignaturaCicloLectivo = AsignaturaCicloLectivo.idAsignatura
		WHERE 
		(@idDiaHorario IS NULL OR @idDiaHorario=idDiaHorario)
		AND
		(@idAsignaturaCurso IS NULL OR @idAsignaturaCurso=AsignaturaCicloLectivo.idAsignaturaCicloLectivo)
		AND
		(@idCursoCicloLectivo IS NULL OR @idCursoCicloLectivo=AsignaturaCicloLectivo.idCursoCicloLectivo)
	 AND
		DiaHorario.idAsignaturaCicloLectivo = AsignaturaCicloLectivo.idAsignaturaCicloLectivo
  AND
		DiaHorario.idDiaSemana <> (
	 SELECT (datepart(dw,fecha) - 1)
	 FROM Feriados	
	 WHERE
	   datepart(yyyy,getdate()) = datepart(yyyy,fecha)
	 AND 
		datepart(mm,fecha) = @mes
	)
GO
/****** Object:  StoredProcedure [dbo].[DiaHorario_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 20/07/11
-- =============================================
CREATE PROCEDURE [dbo].[DiaHorario_Update]
  @idDiaHorario NUMERIC  
 ,@idDiaSemana NUMERIC = NULL
 ,@idDiaHorarioTransaccional NUMERIC = NULL
 ,@idAsignatura NUMERIC = NULL
 ,@idCurso NUMERIC = NULL
AS
 SET NOCOUNT OFF;


    UPDATE DiaHorario SET 
     idDiaSemana = @idDiaSemana
     ,idDiaHorarioTransaccional = @idDiaHorarioTransaccional
     ,idAsignaturaCicloLectivo = @idAsignatura
    WHERE (@idDiaHorario IS NULL OR @idDiaHorario=idDiaHorario)

GO
/****** Object:  StoredProcedure [dbo].[Docentes_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 12/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Docentes_Select]
  @idPersonal NUMERIC = NULL
	,@idPersona NUMERIC = NULL
  ,@legajo VARCHAR(50) = NULL
	,@fechaAlta DATE = NULL
	,@fechaBaja DATE = NULL
	,@cursoCicloLectivo NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    SELECT P.idPersonal
    ,P.idPersona
    ,P.legajo
    ,P.fechaAlta
    ,P.fechaBaja
    ,P.activo
    ,Pe.nombre
    ,Pe.apellido
		,Pe.username
    ,CCL.idCursoCicloLectivo
    FROM Personal P
		INNER JOIN CargosPersonal AS CP 
			ON P.idCargoPersonal = CP.idCargoPersonal
		INNER JOIN [Personas] AS Pe 
			ON P.idPersona = Pe.idPersona
		INNER JOIN [AsignaturaCicloLectivo] AS ACL
			ON P.idPersonal = ACL.idDocente
		INNER JOIN CursosCicloLectivo AS CCL
			ON CCL.idCursoCicloLectivo = ACL.idCursoCicloLectivo
		INNER JOIN CicloLectivo AS CL
			ON CL.idCicloLectivo = CCL.idCicloLectivo
    WHERE (@idPersonal IS NULL OR @idPersonal=P.idPersonal)
		AND (@idPersona IS NULL OR @idPersona=P.idPersona)
		AND (@legajo IS NULL OR @legajo=legajo)
		AND (@fechaAlta IS NULL OR @fechaAlta=P.fechaAlta)
		AND (@fechaBaja IS NULL OR @fechaBaja=P.fechaBaja)
		AND (CP.idCargoPersonal = 43)
		AND (CP.activo = 1)
		AND (CL.activo = 1)
		AND (@cursoCicloLectivo IS NULL OR @cursoCicloLectivo=CCL.idCursoCicloLectivo)



GO
/****** Object:  StoredProcedure [dbo].[Encuesta_Delete]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo
-- Create date: 18/11/12
-- =============================================
CREATE PROCEDURE [dbo].[Encuesta_Delete]
  @idEncuesta NUMERIC = NULL

AS
 SET NOCOUNT OFF;

 DELETE
 FROM RelEncuestaRol
 WHERE @idEncuesta=idEncuesta

 DELETE
 FROM Pregunta
 WHERE @idEncuesta=idEncuesta

 DELETE 
 FROM  Encuesta 
 WHERE @idEncuesta=idEncuesta
GO
/****** Object:  StoredProcedure [dbo].[Encuesta_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo
-- Create date: 12/08/12
-- =============================================
CREATE PROCEDURE [dbo].[Encuesta_Insert]
  @idEncuesta NUMERIC = NULL OUT
 ,@idAmbito NUMERIC = NULL
 ,@username VARCHAR(50) = NULL
 ,@nombre varchar(250) = NULL
 ,@objetivo VARCHAR(4000) = NULL
 ,@fechaCreacion DATE = NULL
 ,@fechaModificacion DATE = NULL
 ,@activo BIT = NULL
 ,@fechaLanzamiento DATE = NULL
 ,@fechaLimite DATE = NULL
 ,@idCursoCicloLectivo NUMERIC = NULL
 ,@idAsignaturaCicloLectivo NUMERIC = NULL
AS
 SET NOCOUNT OFF;

DECLARE @responsable NUMERIC = NULL
SELECT @responsable = idPersona
FROM Personas WHERE Personas.username = @username
 
    INSERT INTO Encuesta(
     responsable
    ,fechaCreacion
    ,fechaModificacion
    ,idAmbito
    ,objetivo
    ,activa
    ,nombre
    ,fechaLanzamiento
    ,fechaLimite
    ,idCursoCicloLectivo
    ,idAsignaturaCicloLectivo
 )
 VALUES(
     @responsable
    ,GETDATE()
    ,NULL
    ,@idAmbito
    ,@objetivo
    ,@activo
    ,@nombre
    ,@fechaLanzamiento
    ,@fechaLimite
    ,@idCursoCicloLectivo
    ,@idAsignaturaCicloLectivo
 )
 SET @idEncuesta = @@IDENTITY
GO
/****** Object:  StoredProcedure [dbo].[Encuesta_Lanzar]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 --=============================================
 --Author:  LAURA
 --Create date: 30/10/12
 --=============================================
CREATE PROCEDURE [dbo].[Encuesta_Lanzar] 
 @idEncuesta numeric = null

AS  
BEGIN   
declare @idPersona numeric = null
declare @idPersonaAnterior numeric = 0

declare @username varchar(50) = null
declare @idCurso numeric = null
declare @idAsignatura numeric = null
declare @roleName varchar(50) = null

--declare @idEncuesta numeric
--set @idEncuesta = 39

declare @idPersonaT numeric = null
declare @usernameT varchar(50) = null
declare @roleNameT varchar(50) = null

declare @idPersonaA numeric = null
declare @usernameA varchar(50) = null
declare @roleNameA varchar(50) = null

SET XACT_ABORT OFF ; -- > really important to set that to OFF

	DECLARE @Any_error int = 0

	SELECT 
		 @idCurso =  isnull(idCursoCicloLectivo,null)
		,@idAsignatura = isnull(idAsignaturaCicloLectivo,null)

	FROM Encuesta AS E 
		INNER JOIN AmbitoEncuesta AS AE ON E.idAmbito = AE.idAmbito

	WHERE 
		E.idEncuesta = @idEncuesta

	BEGIN TRANSACTION
	declare Cursor_Roles cursor for
	SELECT rol FROM RelEncuestaRol WHERE idEncuesta = @idEncuesta

	open Cursor_Roles

	fetch next from Cursor_Roles into @roleName
	  while @@fetch_status = 0
		begin
		
			declare Cursor_Alumnos cursor for
		
			SELECT distinct
					  PA.idPersona
					, vwP.RoleName
					, PT.idPersona
					, vwT.RoleName
			FROM TutorAlumno AS TA
				INNER JOIN Tutor AS T ON TA.idTutor = T.idTutor
				INNER JOIN Personas AS PT ON T.idPersona = PT.idPersona
				INNER JOIN Alumnos AS A ON TA.idAlumno = A.idAlumno
				INNER JOIN Personas PA ON A.idPersona = PA.idPersona
				INNER JOIN AlumnoCursoCicloLectivo AS AC ON A.idAlumno = AC.idAlumno
				INNER JOIN CursosCicloLectivo AS CCL ON AC.idCursoCicloLectivo = CCL.idCursoCicloLectivo
				INNER JOIN vw_PersonaUsuarioRol AS vwP ON vwP.UserName = PA.username
				LEFT JOIN vw_PersonaUsuarioRol AS vwT ON vwT.UserName = PT.username
			WHERE 
					(@idCurso is null or CCL.idCursoCicloLectivo = @idCurso)
				AND (@rolename IS NULL OR (@roleName = vwP.RoleName OR @roleName = vwT.RoleName))
			order by PA.idPersona
			open Cursor_Alumnos

			fetch next from Cursor_Alumnos into @idPersonaA,@roleNameA,@idPersonaT,@roleNameT
			  while @@fetch_status = 0
				begin
			
				if (@roleName = @roleNameA)
				begin
					set @idPersona = @idPersonaA
					set @username = @usernameA
				end
				else
				begin
					if (@roleName = @roleNameT)
					begin
						set @idPersona = @idPersonaT
						set @username = @usernameT
					end
				end

				if (@idPersonaAnterior <> @idPersona) 
				begin 
					--print @idpersona
					--print @roleName
					BEGIN TRY
						INSERT INTO [EncuestaUsuario]
					   ([idEncuesta]
					   ,[idPersona]
					   ,[fechaRespuesta])
						VALUES
					   (@idEncuesta
					   ,@idPersona
					   ,NULL)
					END TRY
					BEGIN CATCH /*required, but you dont have to do anything */ END CATCH
				end
				SET @idPersonaAnterior = @idPersona

			fetch next from Cursor_Alumnos into @idPersonaA,@roleNameA,@idPersonaT,@roleNameT
			end
			-- cerramos el cursor
			close Cursor_Alumnos
			DEALLOCATE Cursor_Alumnos

	fetch next from Cursor_Roles into @roleName
	end
	-- cerramos el cursor

	close Cursor_Roles
	DEALLOCATE Cursor_Roles

	update Encuesta set fechalanzamiento = getdate() where idEncuesta  = @idEncuesta

	ErrorHandler: 

		   IF @Any_error is null or @Any_error = 0 OR @Any_error=2627
		   BEGIN 
			   COMMIT TRAN
		   END
		   ELSE 
			   BEGIN 
				   ROLLBACK TRAN 
			   END
END
GO
/****** Object:  StoredProcedure [dbo].[Encuesta_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo
-- Create date: 12/08/12
-- =============================================
CREATE PROCEDURE [dbo].[Encuesta_Select]
  @idEncuesta NUMERIC = NULL
 ,@idAmbito NUMERIC = NULL
 ,@responsable VARCHAR(MAX) = NULL
 --,@fechaCreacion DATE = NULL
 --,@fechaModificacion DATE = NULL
 --,@nombre varchar(100) = NULL
 --,@objetivo VARCHAR(4000) = NULL
 ,@activa BIT = NULL
 --,@fechaLanzamiento DATE = NULL
 --,@fechaLimite DATE = NULL
 ,@idCursoCicloLectivo NUMERIC = NULL
 ,@idAsignaturaCicloLectivo NUMERIC = NULL
AS
 SET NOCOUNT OFF;

DECLARE @idPersona NUMERIC = NULL;

SELECT @idPersona = idPersona
FROM Personas
WHERE username = @responsable

    SELECT	 E.idEncuesta
			,E.idAmbito
			,E.responsable
			,P.nombre as 'nombreOrganizador'
			,P.apellido as 'apellidoOrganizador'
			,P.username
			,E.fechaCreacion
			,E.fechaModificacion
			,E.objetivo
			,E.nombre as 'nombreEncuesta'
			,E.activa
			,A.nombre as 'nombreAmbito'
			,E.fechaLanzamiento
			,E.fechaLimite
			,E.idCursoCicloLectivo
			,E.idAsignaturaCicloLectivo
			,(select count(idPersona) from EncuestaUsuario where idEncuesta=E.idEncuesta and fechaRespuesta is not null) AS 'Respuestas'
			,CCL.Curso
			,AG.nombre as 'Asignatura'
			,
			(
				CASE  
					WHEN (E.fechaLanzamiento < getdate() AND E.fechaLimite < getdate()) THEN 'Cerrada' 
					WHEN (E.fechaLanzamiento < getdate() AND E.fechaLimite > getdate()) THEN 'Lanzada' 
					WHEN (E.fechaLanzamiento IS NULL) THEN 'Creada' 
					ELSE 'ESTADO NO CONTROLADO' 
				END
			)
			as 'Estado'
    FROM Encuesta AS E
	INNER JOIN Personas AS P ON E.responsable = P.idPersona
	INNER JOIN AmbitoEncuesta AS A ON A.idAmbito = E.idAmbito
	LEFT JOIN vw_CursosCicloLectivoActual AS CCL on CCL.idCursoCicloLectivo = E.idCursoCicloLectivo
	LEFT JOIN AsignaturaCicloLectivo AS ACL on E.idAsignaturaCicloLectivo = ACL.idAsignaturaCicloLectivo
	LEFT JOIN Asignatura AS AG on ACL.idAsignatura = AG.idAsignatura
    WHERE (@idEncuesta IS NULL OR @idEncuesta=E.idEncuesta)
   AND (@idPersona IS NULL OR @idPersona=E.responsable)
   AND(@idAmbito IS NULL OR @idAmbito=E.idAmbito)
   AND(@activa IS NULL OR @activa=E.activa)
GO
/****** Object:  StoredProcedure [dbo].[Encuesta_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:  Pablo
-- Create date: 12/08/12
-- =============================================
CREATE PROCEDURE [dbo].[Encuesta_Update]
  @idEncuesta NUMERIC = NULL
 ,@idAmbito NUMERIC = NULL
 ,@username VARCHAR(50) = NULL
 ,@fechaCreacion DATE = NULL
 ,@fechaModificacion DATE = NULL
 ,@nombre varchar(100) = NULL
 ,@objetivo VARCHAR(4000) = NULL
 ,@activo BIT = NULL
 ,@fechaLanzamiento DATE = NULL
 ,@fechaLimite DATE = NULL
 ,@idCursoCicloLectivo NUMERIC = NULL
 ,@idAsignaturaCicloLectivo NUMERIC = NULL
AS
 SET NOCOUNT OFF;
 
 DECLARE @responsable NUMERIC = NULL
 SELECT @responsable = idPersona
 FROM Personas WHERE Personas.username = @username

 UPDATE Encuesta SET 
  responsable = @responsable
 ,idAmbito = @idAmbito
 ,fechaModificacion = getdate()
 ,nombre = @nombre
 ,objetivo = @objetivo
 ,activa = @activo
 ,fechaLanzamiento = @fechaLanzamiento
 ,fechaLimite = @fechaLimite
 ,idCursoCicloLectivo = @idCursoCicloLectivo
 ,idAsignaturaCicloLectivo = @idAsignaturaCicloLectivo
 WHERE @idEncuesta=idEncuesta
	

GO
/****** Object:  StoredProcedure [dbo].[Encuesta_ValidarPreguntas]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[Encuesta_ValidarPreguntas]
@idEncuesta numeric = null

as

select idpregunta from Pregunta
where idEncuesta = @idEncuesta
GO
/****** Object:  StoredProcedure [dbo].[EncuestaUsuario_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 09/10/12
-- =============================================
CREATE PROCEDURE [dbo].[EncuestaUsuario_Insert]
  @idEncuesta NUMERIC = NULL
 ,@username VARCHAR(50) = NULL
 ,@idPersona NUMERIC = NULL
 ,@fechaRespuesta DATETIME = NULL
AS
 SET NOCOUNT OFF;

SELECT @idPersona = idPersona
FROM Personas WHERE Personas.username = @username

 INSERT INTO EncuestaUsuario(
     idEncuesta
    ,idPersona
    ,fechaRespuesta
 )
 VALUES(
     @idEncuesta
    ,@idPersona
    ,@fechaRespuesta
 )
GO
/****** Object:  StoredProcedure [dbo].[EncuestaUsuario_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 09/10/12
-- =============================================
CREATE PROCEDURE [dbo].[EncuestaUsuario_Update]
  @idEncuesta NUMERIC = NULL
 ,@username VARCHAR(50) = NULL
 ,@idPersona NUMERIC = NULL
 ,@fechaRespuesta DATETIME = NULL
AS
 SET NOCOUNT OFF;

SELECT @idPersona = idPersona
FROM Personas WHERE Personas.username = @username

UPDATE EncuestaUsuario SET 
	fechaRespuesta = @fechaRespuesta
WHERE 
		@idEncuesta=idEncuesta
	AND @idPersona=idPersona
GO
/****** Object:  StoredProcedure [dbo].[EncuestaUsuarioDisponible_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 09/10/12
-- =============================================
CREATE PROCEDURE [dbo].[EncuestaUsuarioDisponible_Select]
  @idEncuesta NUMERIC = NULL
 ,@username VARCHAR(50) = NULL
 ,@fechaRespuesta DATETIME = NULL
 ,@fechaLimite DATETIME = NULL
AS
 SET NOCOUNT OFF;

DECLARE @idPersona NUMERIC = NULL
SELECT @idPersona = idPersona
FROM Personas WHERE Personas.username = @username


SELECT 
	 EU.idEncuesta
	,EU.idPersona
	,EU.fechaRespuesta
	,E.fechaLimite
	,E.nombre
	,E.idAmbito
	,AE.nombre AS 'nombreAmbito'
FROM EncuestaUsuario AS EU
	INNER JOIN Encuesta AS E ON E.idEncuesta = EU.idEncuesta 
		AND (E.fechaLimite > GETDATE() OR E.fechaLimite IS NULL)
		AND E.fechaLanzamiento IS NOT NULL
		AND EU.fechaRespuesta IS NULL
	INNER JOIN AmbitoEncuesta AS AE ON E.idAmbito = AE.idAmbito
WHERE (@idEncuesta IS NULL OR @idEncuesta=EU.idEncuesta)
AND (@idPersona IS NULL OR @idPersona=EU.idPersona)
AND (@fechaRespuesta IS NULL OR @fechaRespuesta=fechaRespuesta)
AND (@fechaLimite IS NULL OR @fechaLimite=E.fechaLimite)

GO
/****** Object:  StoredProcedure [dbo].[EscalaPonderacion_Delete]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo
-- Create date: 02/04/13
-- =============================================
CREATE PROCEDURE [dbo].[EscalaPonderacion_Delete]
  @idEscalaPonderacion NUMERIC = NULL
 
AS
 SET NOCOUNT OFF;

		EXEC ValorEscalaPonderacion_Delete NULL,@idEscalaPonderacion

    DELETE FROM EscalaPonderacion 
    WHERE @idEscalaPonderacion=idEscalaPonderacion
GO
/****** Object:  StoredProcedure [dbo].[EscalaPonderacion_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo
-- Create date: 12/08/12
-- =============================================
CREATE PROCEDURE [dbo].[EscalaPonderacion_Insert]
  @idEscalaPonderacion NUMERIC = NULL
 ,@nombre VARCHAR(250) = NULL
 ,@descripcion VARCHAR(4000) = NULL
AS
 SET NOCOUNT OFF;

IF (@idEscalaPonderacion=0 or @idEscalaPonderacion is null)
  BEGIN
    INSERT INTO EscalaPonderacion(
     nombre
    ,descripcion
	 )
	 VALUES(
		 @nombre
		,@descripcion
	 )
 SET @idEscalaPonderacion = @@IDENTITY
END
ELSE
BEGIN
EXECUTE [EscalaPonderacion_Update] 
	   @idEscalaPonderacion
	  ,@nombre
	  ,@descripcion
END
GO
/****** Object:  StoredProcedure [dbo].[EscalaPonderacion_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo
-- Create date: 12/08/12
-- =============================================
CREATE PROCEDURE [dbo].[EscalaPonderacion_Select]
  @idEscalaPonderacion NUMERIC = NULL
 ,@nombre VARCHAR(250) = NULL
 ,@descripcion VARCHAR(4000) = NULL
AS
 SET NOCOUNT OFF;

    SELECT idEscalaPonderacion
    ,nombre
    ,descripcion
		,(SELECT COUNT(E.idEncuesta)
			FROM Encuesta AS E
			INNER JOIN Pregunta AS P ON E.idEncuesta = P.idEncuesta AND
				P.idEscalaPonderacion = EscalaPonderacion.idEscalaPonderacion
			INNER JOIN CategoriaPregunta AS CP ON CP.idCategoria=P.idCategoria 
			) 'cantidadEncuestas'
    FROM EscalaPonderacion
    WHERE (@idEscalaPonderacion IS NULL OR @idEscalaPonderacion=idEscalaPonderacion)
   --AND (@nombre IS NULL OR @nombre=nombre)
GO
/****** Object:  StoredProcedure [dbo].[EscalaPonderacion_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo
-- Create date: 12/08/12
-- =============================================
CREATE PROCEDURE [dbo].[EscalaPonderacion_Update]
  @idEscalaPonderacion NUMERIC = NULL
 ,@nombre VARCHAR(250) = NULL
 ,@descripcion VARCHAR(4000) = NULL
AS
 SET NOCOUNT OFF;

    UPDATE EscalaPonderacion SET 
      nombre = @nombre
     ,descripcion = @descripcion
    WHERE (@idEscalaPonderacion IS NULL OR @idEscalaPonderacion=idEscalaPonderacion)
GO
/****** Object:  StoredProcedure [dbo].[EstadoNovedadAulica_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 18/09/12
-- =============================================
CREATE PROCEDURE [dbo].[EstadoNovedadAulica_Select]
  @idEstadoNovedadAulica NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@esFinal BIT = NULL
AS
 SET NOCOUNT OFF;

    SELECT idEstadoNovedadAulica
    ,nombre
    ,esFinal
    FROM EstadoNovedadAulica
    WHERE (@idEstadoNovedadAulica IS NULL OR @idEstadoNovedadAulica=idEstadoNovedadAulica)
   AND (@nombre IS NULL OR @nombre=nombre)
   AND (@esFinal IS NULL OR @esFinal=esFinal)
GO
/****** Object:  StoredProcedure [dbo].[Evaluacion_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 06/08/11
-- =============================================
CREATE PROCEDURE [dbo].[Evaluacion_Insert]
  @idEvaluacion NUMERIC = NULL
 ,@idAsignaturaCurso NUMERIC = NULL
 --,@idEvento NUMERIC = NULL
 ,@idEventoAgenda NUMERIC = NULL
 ,@idAgendaActividades NUMERIC = NULL
 ,@username VARCHAR(50) = NULL
 ,@idTipoEvento NUMERIC = NULL
 ,@fechaModificacion DATE = NULL
 ,@fechaEvento DATE = NULL
 ,@descripcion VARCHAR(4000) = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;
Declare @idEvento numeric
EXECUTE @idEvento = [EventoAgenda_Insert] 
   @idEventoAgenda
  ,@idAgendaActividades
  ,@username
  ,@idTipoEvento
  ,@fechaModificacion
  ,@fechaEvento
  ,@descripcion
  ,@activo

    INSERT INTO Evaluacion(
     idAsignaturaCurso
    ,idEvento
	 )
	 VALUES(
		 @idAsignaturaCurso
		,@idEvento
	 )
	SET @idEvaluacion = @@IDENTITY

GO
/****** Object:  StoredProcedure [dbo].[Evaluacion_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 14/08/11
-- =============================================
CREATE PROCEDURE [dbo].[Evaluacion_Select]
  @idEvaluacion NUMERIC = NULL
 ,@idAsignaturaCicloLectivo NUMERIC = NULL
 ,@idEvento NUMERIC = NULL
 ,@fechaDesde DATE = NULL
 ,@fechaHasta DATE = NULL
 ,@fechaEvento DATE = NULL
 ,@idAgendaActividad NUMERIC = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;

    SELECT 
     E.idEvaluacion
    ,E.idAsignaturaCurso
    ,E.idEvento
    ,TP.descripcion AS 'tipoEvento'
    ,EA.activo
    ,EA.descripcion
    ,EA.fechaAlta
    ,EA.fechaModificacion
    ,EA.fechaEvento
    ,EA.idAgendaActividades
    ,EA.idEventoAgenda
    ,EA.idOrganizador
    ,EA.idTipoEvento
    ,A.nombre AS 'asignatura'
    ,P.nombre
    ,P.apellido
    ,P.username AS 'usernameOrganizador'
    FROM EventoAgenda AS EA 
		INNER JOIN Evaluacion AS E ON EA.idEventoAgenda = E.idEvento
		INNER JOIN TipoEvento AS TP ON EA.idTipoEvento = TP.idTipoEvento
		INNER JOIN AsignaturaCicloLectivo AS AC ON E.idAsignaturaCurso = AC.idAsignaturaCicloLectivo
		INNER JOIN Asignatura AS A ON AC.idAsignatura = A.idAsignatura
		INNER JOIN Personas AS P ON EA.idOrganizador = P.idPersona
    WHERE 
		(@idEvaluacion IS NULL OR @idEvaluacion=E.idEvaluacion)
	AND (@idAsignaturaCicloLectivo IS NULL OR @idAsignaturaCicloLectivo=AC.idAsignaturaCicloLectivo)
	AND (@fechaDesde IS NULL OR EA.fechaEvento BETWEEN @fechaDesde AND @fechaHasta)
	AND (@fechaEvento IS NULL OR EA.fechaEvento = @fechaEvento)
	AND (@idAgendaActividad IS NULL OR @idAgendaActividad = EA.idAgendaActividades)
	AND (@activo IS NULL OR @activo = EA.activo )
	AND (@idEvento IS NULL OR @idEvento <> E.idEvento)

GO
/****** Object:  StoredProcedure [dbo].[Evaluacion_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 15/08/11
-- =============================================
CREATE PROCEDURE [dbo].[Evaluacion_Update]
  @idEvaluacion NUMERIC = NULL
 ,@idAsignaturaCurso NUMERIC = NULL
 ,@idEventoAgenda NUMERIC = NULL
 ,@idAgendaActividades NUMERIC = NULL
 ,@username VARCHAR(50) = NULL
 ,@idTipoEvento NUMERIC = NULL
 ,@fechaAlta DATE = NULL
 ,@fechaModificacion DATE = NULL
 ,@fechaEvento DATE = NULL
 ,@descripcion VARCHAR(4000) = NULL
 ,@activo BIT = NULL 
AS
 SET NOCOUNT OFF;

DECLARE @idOrganizador NUMERIC = NULL

SELECT @idOrganizador = idPersona
FROM Personas WHERE username = @username

	EXECUTE EventoAgenda_Update 
	 @idEventoAgenda
   ,@idAgendaActividades 
	 ,@idOrganizador 
	 ,@idTipoEvento 
	 ,@fechaModificacion 
	 ,@fechaEvento 
	 ,@descripcion 
	 ,@activo

    UPDATE Evaluacion SET 
      idAsignaturaCurso = @idAsignaturaCurso
     ,idEvento = @idEventoAgenda
    WHERE @idEvaluacion=idEvaluacion

GO
/****** Object:  StoredProcedure [dbo].[Evaluacion_VerificarDisponibilidadAgenda]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 28/08/11
-- =============================================
CREATE PROCEDURE [dbo].[Evaluacion_VerificarDisponibilidadAgenda]
 @fechaEvento DATE = NULL
 ,@idAsignaturaCurso NUMERIC = NULL
 ,@idAgendaActividad NUMERIC = NULL
 ,@idEventoAgenda NUMERIC = NULL
AS
 SET NOCOUNT OFF;

SELECT 
     EA.descripcion
    ,EA.fechaEvento
    ,EA.idAgendaActividades
    ,EA.idEventoAgenda
    ,EA.idTipoEvento
    FROM EventoAgenda AS EA
		INNER JOIN Evaluacion AS EV ON EA.idEventoAgenda = EV.idEvento
		INNER JOIN AgendaActividades AS AA ON AA.idAgendaActividad = EA.idAgendaActividades
    WHERE 
	    (@fechaEvento IS NULL OR EA.fechaEvento = @fechaEvento)
	AND (@idAsignaturaCurso IS NULL OR EV.idAsignaturaCurso = @idAsignaturaCurso)
	AND (@idAgendaActividad IS NULL OR EA.idAgendaActividades = @idAgendaActividad)
	AND (@idEventoAgenda IS NULL OR EA.idEventoAgenda <> @idEventoAgenda)	
	AND (EA.idTipoEvento = 1)
	AND (EA.activo = 1)

GO
/****** Object:  StoredProcedure [dbo].[EventoAgenda_BIS_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[EventoAgenda_BIS_Select]
	 @idAgendaActividad NUMERIC = NULL
	,@fechaDesde DATE = NULL
	,@fechaHasta DATE = NULL
	,@idEventoAgenda NUMERIC = NULL
	,@idTipoEvento NUMERIC = NULL
	,@username VARCHAR(50) = NULL
	,@fechaEvento DATE = NULL
	,@descripcion VARCHAR(4000) = NULL
	,@activo BIT = NULL
AS

--declare @idAgendaActividad int = 10516
--declare @fechaDesde DATE = '08/11/2013'
--declare	@fechaHasta DATE = '08/31/2013'


DECLARE @idPersona NUMERIC = NULL;

if (@username is not null)
begin
	SELECT @idPersona = idPersona
	FROM Personas
	WHERE username = @username
end

SELECT 
	 EA.[idEventoAgenda]
	,EA.[idAgendaActividades]
	,EA.[idOrganizador]
	,EA.[idTipoEvento]
	,EA.[fechaAlta]
	,EA.[fechaModificacion]
	,EA.[fechaEvento]
	,A.nombre + ' - ' + EA.[descripcion] AS 'descripcion'
	,EA.[activo]
	,TP.descripcion AS 'tipoEvento', P.nombre, P.apellido
INTO #EventosAgendaResult
FROM   
	EventoAgenda AS EA INNER JOIN
	Evaluacion AS E ON EA.idEventoAgenda = E.idEvento
	INNER JOIN TipoEvento AS TP 
		ON EA.idTipoEvento = TP.idTipoEvento
	INNER JOIN Personas AS P
		ON EA.idOrganizador = p.idPersona
	INNER JOIN AsignaturaCicloLectivo AS ACCL ON E.idAsignaturaCurso = ACCL.idAsignaturaCicloLectivo
	INNER JOIN Asignatura AS A ON ACCL.idAsignatura = A.idAsignatura
UNION

SELECT EA.*,TP.descripcion AS 'tipoEvento', P.nombre, P.apellido
FROM   
	EventoAgenda AS EA INNER JOIN
	Reunion AS R ON EA.idEventoAgenda = R.idEvento
	INNER JOIN TipoEvento AS TP 
		ON EA.idTipoEvento = TP.idTipoEvento
	INNER JOIN Personas AS P
		ON EA.idOrganizador = p.idPersona
	
UNION

SELECT EA.*,TP.descripcion AS 'tipoEvento', P.nombre, P.apellido
FROM   
	EventoAgenda AS EA INNER JOIN
	Excursion AS E ON EA.idEventoAgenda = E.idEvento
		INNER JOIN TipoEvento AS TP 
		ON EA.idTipoEvento = TP.idTipoEvento
	INNER JOIN Personas AS P
		ON EA.idOrganizador = p.idPersona

UNION

SELECT 
	 EA.[idEventoAgenda]
	,EA.[idAgendaActividades]
	,EA.[idOrganizador]
	,EA.[idTipoEvento]
	,EA.[fechaAlta]
	,EA.[fechaModificacion]
	,EA.[fechaEvento]
	,A.nombre + ' - ' + TRC.nombre  + ' - ' + EA.[descripcion] AS 'descripcion'
	,EA.[activo]
	,TP.descripcion AS 'tipoEvento', P.nombre, P.apellido
FROM   
	EventoAgenda AS EA INNER JOIN
	RegistroClases AS RC ON EA.idEventoAgenda = RC.idEventoAgenda
		INNER JOIN TipoRegistroClases AS TRC 
			ON TRC.idTipoRegistroClases = RC.idTipoRegistroClases
	INNER JOIN TipoEvento AS TP 
		ON EA.idTipoEvento = TP.idTipoEvento
	INNER JOIN Personas AS P
		ON EA.idOrganizador = p.idPersona
	INNER JOIN AsignaturaCicloLectivo AS ACCL ON RC.idAsignaturaCurso = ACCL.idAsignaturaCicloLectivo
	INNER JOIN Asignatura AS A ON ACCL.idAsignatura = A.idAsignatura

UNION 

    SELECT 
	     0 AS 'idEventoAgenda'
      ,0 AS 'idAgendaActividades'
      ,E.[idPersona] AS 'idOrganizador'
			,0 AS 'idTipoEvento'
			,GETDATE() AS 'fechaAlta'
			,GETDATE() AS 'fechaModificacion'
      ,E.[fecha] AS 'fechaEvento'
			,E.titulo AS 'descripcion'
			,E.activo
			,'Institucional' AS 'tipoEvento'
      ,P.nombre
      ,P.apellido
			
    FROM EventoInstitucional AS E 
		INNER JOIN Personas AS P ON E.idPersona = P.idPersona
		INNER JOIN TipoEventoInstitucional AS TE ON E.idTipoEvento = TE.idTipoEventoInstitucional 		
    WHERE (@idTipoEvento IS NULL OR @idTipoEvento=E.idTipoEvento)
   AND (@idPersona IS NULL OR @idPersona=E.idPersona)
   AND E.activo = 1
   AND (@fechaDesde IS NULL OR E.fecha >= @fechaDesde)
   AND (@fechaHasta IS NULL OR E.fecha <= @fechaHasta)

	
SELECT *
FROM #EventosAgendaResult
WHERE 
		(@idAgendaActividad IS NULL OR #EventosAgendaResult.idAgendaActividades = @idAgendaActividad)
	AND (@fechaDesde IS NULL OR @fechaDesde <= #EventosAgendaResult.fechaEvento)
	AND (@fechaHasta IS NULL OR @fechaHasta >= #EventosAgendaResult.fechaEvento)
	AND (@idPersona IS NULL OR @idPersona = #EventosAgendaResult.idOrganizador)

ORDER BY fechaEvento DESC
	
DROP TABLE #EventosAgendaResult
GO
/****** Object:  StoredProcedure [dbo].[EventoAgenda_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 01/08/11
-- =============================================
CREATE PROCEDURE [dbo].[EventoAgenda_Insert]
  @idEventoAgenda NUMERIC = NULL
 ,@idAgendaActividades NUMERIC = NULL
 ,@username VARCHAR(50) = NULL
 ,@idTipoEvento NUMERIC = NULL
 ,@fechaModificacion DATE = NULL
 ,@fechaEvento DATE = NULL
 ,@descripcion VARCHAR(4000) = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;

Declare @idOrganizador numeric = null
select @idOrganizador = idPersona
from Personas where Personas.username = @username 

--if (@idOrganizador <> null)
    INSERT INTO EventoAgenda(
     idAgendaActividades
    ,idOrganizador
    ,idTipoEvento 
    ,fechaAlta
    ,fechaModificacion
    ,fechaEvento
    ,descripcion
    ,activo
 )
 VALUES(
     @idAgendaActividades
    ,@idOrganizador
    ,@idTipoEvento 
    ,getdate()
    ,null
    ,@fechaEvento
    ,@descripcion
    ,@activo
 )
 SET @idEventoAgenda = @@IDENTITY
RETURN SCOPE_IDENTITY()

GO
/****** Object:  StoredProcedure [dbo].[EventoAgenda_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[EventoAgenda_Select]
	 @idAgendaActividad NUMERIC = NULL
	,@fechaDesde DATE = NULL
	,@fechaHasta DATE = NULL
	,@idEventoAgenda NUMERIC = NULL
	,@idTipoEvento NUMERIC = NULL
	,@username VARCHAR(50) = NULL
	,@fechaEvento DATE = NULL
	,@descripcion VARCHAR(4000) = NULL
	,@activo BIT = NULL
AS

--declare @idAgendaActividad int = 10516
--declare @fechaDesde DATE = '08/01/2013'
--declare	@fechaHasta DATE = '10/31/2013'
--declare @username VARCHAR(50) = NULL
--declare @idEventoAgenda NUMERIC = NULL
--declare @idTipoEvento NUMERIC = NULL
--declare @fechaEvento DATE = NULL
--declare @descripcion VARCHAR(4000) = NULL
--declare @activo BIT = NULL

DECLARE @idPersona NUMERIC = NULL;

if (@username is not null)
begin
	SELECT @idPersona = idPersona
	FROM Personas
	WHERE username = @username
end

CREATE TABLE #AgendaResult (
	 [idEventoAgenda] INT  NULL
	,[idAgendaActividades] INT  NULL
	,[idOrganizador] INT  NULL
	,[idTipoEvento] INT  NULL
	,[fechaAlta] DATE  NULL
	,[fechaModificacion] DATE NULL
	,[fechaEvento] DATE NULL
	,[descripcion] VARCHAR(MAX) NULL
	,[activo] BIT NULL
	,[tipoEvento] VARCHAR(MAX) NULL
	,[nombre] VARCHAR(MAX) NULL
	,[apellido] VARCHAR(MAX) NULL
) 

insert into  #AgendaResult
SELECT 
	 EA.[idEventoAgenda]
	,EA.[idAgendaActividades]
	,EA.[idOrganizador]
	,EA.[idTipoEvento]
	,EA.[fechaAlta]
	,EA.[fechaModificacion]
	,EA.[fechaEvento]
	,A.nombre + ' - ' + EA.[descripcion] AS 'descripcion'
	,EA.[activo]
	,TP.descripcion AS 'tipoEvento', P.nombre, P.apellido
FROM   
	EventoAgenda AS EA INNER JOIN
	Evaluacion AS E ON EA.idEventoAgenda = E.idEvento
	INNER JOIN TipoEvento AS TP 
		ON EA.idTipoEvento = TP.idTipoEvento
	INNER JOIN Personas AS P
		ON EA.idOrganizador = p.idPersona
	INNER JOIN AsignaturaCicloLectivo AS ACCL ON E.idAsignaturaCurso = ACCL.idAsignaturaCicloLectivo
	INNER JOIN Asignatura AS A ON ACCL.idAsignatura = A.idAsignatura

insert into  #AgendaResult
SELECT EA.*,TP.descripcion AS 'tipoEvento', P.nombre, P.apellido
FROM   
	EventoAgenda AS EA INNER JOIN
	Reunion AS R ON EA.idEventoAgenda = R.idEvento
	INNER JOIN TipoEvento AS TP 
		ON EA.idTipoEvento = TP.idTipoEvento
	INNER JOIN Personas AS P
		ON EA.idOrganizador = p.idPersona
	
insert into  #AgendaResult
SELECT EA.*,TP.descripcion AS 'tipoEvento', P.nombre, P.apellido
FROM   
	EventoAgenda AS EA INNER JOIN
	Excursion AS E ON EA.idEventoAgenda = E.idEvento
		INNER JOIN TipoEvento AS TP 
		ON EA.idTipoEvento = TP.idTipoEvento
	INNER JOIN Personas AS P
		ON EA.idOrganizador = p.idPersona

insert into  #AgendaResult
SELECT 
	 EA.[idEventoAgenda]
	,EA.[idAgendaActividades]
	,EA.[idOrganizador]
	,EA.[idTipoEvento]
	,EA.[fechaAlta]
	,EA.[fechaModificacion]
	,EA.[fechaEvento]
	,A.nombre + ' - ' + TRC.nombre  + ' - ' + EA.[descripcion] AS 'descripcion'
	,EA.[activo]
	,TP.descripcion AS 'tipoEvento', P.nombre, P.apellido
FROM   
	EventoAgenda AS EA INNER JOIN
	RegistroClases AS RC ON EA.idEventoAgenda = RC.idEventoAgenda
		INNER JOIN TipoRegistroClases AS TRC 
			ON TRC.idTipoRegistroClases = RC.idTipoRegistroClases
	INNER JOIN TipoEvento AS TP 
		ON EA.idTipoEvento = TP.idTipoEvento
	INNER JOIN Personas AS P
		ON EA.idOrganizador = p.idPersona
	INNER JOIN AsignaturaCicloLectivo AS ACCL ON RC.idAsignaturaCurso = ACCL.idAsignaturaCicloLectivo
	INNER JOIN Asignatura AS A ON ACCL.idAsignatura = A.idAsignatura

insert into  #AgendaResult
SELECT 
	 0 AS 'idEventoAgenda'
    ,0 AS 'idAgendaActividades'
    ,E.[idPersona] AS 'idOrganizador'
	,0 AS 'idTipoEvento'
	,GETDATE() AS 'fechaAlta'
	,GETDATE() AS 'fechaModificacion'
    ,E.[fecha] AS 'fechaEvento'
	,E.titulo AS 'descripcion'
	,E.activo
	,'Institucional' AS 'tipoEvento'
    ,P.nombre
    ,P.apellido
FROM EventoInstitucional AS E 
	INNER JOIN Personas AS P ON E.idPersona = P.idPersona
	INNER JOIN TipoEventoInstitucional AS TE ON E.idTipoEvento = TE.idTipoEventoInstitucional 		
WHERE 
	E.activo = 1
	
SELECT *
FROM #AgendaResult
WHERE 
		(@idAgendaActividad IS NULL OR (#AgendaResult.idTipoEvento = 0 OR #AgendaResult.idAgendaActividades = @idAgendaActividad))
	AND (@fechaDesde IS NULL OR @fechaDesde <= #AgendaResult.fechaEvento)
	AND (@fechaHasta IS NULL OR @fechaHasta >= #AgendaResult.fechaEvento)
	AND (@idPersona IS NULL OR @idPersona = #AgendaResult.idOrganizador)

ORDER BY fechaEvento DESC
	
DROP TABLE #AgendaResult;
GO
/****** Object:  StoredProcedure [dbo].[EventoAgenda_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 15/08/11
-- =============================================
CREATE PROCEDURE [dbo].[EventoAgenda_Update]
  @idEventoAgenda NUMERIC = NULL 
 ,@idAgendaActividades NUMERIC = NULL
 ,@idOrganizador NUMERIC = NULL
 ,@idTipoEvento NUMERIC = NULL
 ,@fechaModificacion DATE = NULL
 ,@fechaEvento DATE = NULL
 ,@descripcion VARCHAR(4000) = NULL
 ,@activo BIT = NULL

AS
 SET NOCOUNT OFF;

    UPDATE EventoAgenda SET 
     fechaModificacion = @fechaModificacion
     ,fechaEvento = @fechaEvento
     ,descripcion = @descripcion
     ,activo = @activo
    WHERE (@idEventoAgenda=idEventoAgenda)
    AND (@idOrganizador = idOrganizador)
GO
/****** Object:  StoredProcedure [dbo].[EventoAgenda_VerificarDisponibilidadAgenda]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 28/08/11
-- =============================================
CREATE PROCEDURE [dbo].[EventoAgenda_VerificarDisponibilidadAgenda]
 @fechaEvento DATE = NULL
 ,@eventoAgenda NUMERIC = NULL
 
AS
 SET NOCOUNT OFF;

SELECT 
     TP.descripcion AS 'tipoEvento'
    ,EA.activo
    ,EA.descripcion
    ,EA.fechaEvento
    ,EA.idAgendaActividades
    ,EA.idEventoAgenda
    ,EA.idTipoEvento
    FROM EventoAgenda AS EA 
		INNER JOIN TipoEvento AS TP ON EA.idTipoEvento = TP.idTipoEvento
		INNER JOIN Evaluacion AS EV ON EA.idEventoAgenda = EV.idEvento
		INNER JOIN Excursion AS EX ON EA.idEventoAgenda = EX.idEvento
		INNER JOIN AgendaActividades AS AA ON AA.idAgendaActividad = EA.idAgendaActividades
		--INNER JOIN AsignaturaCurso AS AC ON AC.idAsignaturaCurso = EV.idAsignaturaCurso
		--INNER JOIN Asignatura AS ASG ON ASG.idAsignatura = AC.idAsignatura
		--INNER JOIN CursosCicloLectivo AS CCL ON CCL.idCursoCicloLectivo = AA.idCursoCicloLectivo AND CCL.idCurso = AC.idCurso
    WHERE 
	    (@fechaEvento IS NULL OR EA.fechaEvento = @fechaEvento)
	--AND (@idAgendaActividad IS NULL OR EA.idAgendaActividades = @idAgendaActividad)
	--AND (@asignatura IS NULL OR ASG.idAsignatura = @asignatura)
	AND (@eventoAgenda IS NULL OR  
			EA.idEventoAgenda = @eventoAgenda OR
			EV.idEvento = @eventoAgenda OR 
			EX.idEvento = @eventoAgenda
			)
	AND (EA.idTipoEvento IN (1,3))
	AND (EA.activo = 1)

GO
/****** Object:  StoredProcedure [dbo].[EventoInstitucional_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 28/07/11
-- =============================================
CREATE PROCEDURE [dbo].[EventoInstitucional_Insert]
  @idEventoInstitucional NUMERIC = NULL OUTPUT
 ,@idTipoEvento NUMERIC = NULL
 ,@username VARCHAR(50) = NULL
 ,@fecha DATE = NULL
 ,@hora TIME = NULL
 ,@lugar VARCHAR(50) = NULL
 ,@titulo VARCHAR(50) = NULL
 ,@detalle VARCHAR(4000) = NULL
 ,@activo BIT = NULL
AS

 SET NOCOUNT OFF;
Declare @idPersona numeric = null
select @idPersona = idPersona
from Personas where Personas.username = @username 

--if (@idPersona <> null)
--begin
	INSERT INTO EventoInstitucional(
		 idTipoEvento
		,idPersona
		,fecha
		,hora
		,lugar
		,titulo
		,detalle
		,activo
	 )
	 VALUES(
		 @idTipoEvento
		,@idPersona
		,@fecha
		,@hora
		,@lugar
		,@titulo
		,@detalle
		,@activo
	)
	SET @idEventoInstitucional = @@IDENTITY
	--end
	RETURN SCOPE_IDENTITY()

GO
/****** Object:  StoredProcedure [dbo].[EventoInstitucional_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 24/07/11
-- =============================================
CREATE PROCEDURE [dbo].[EventoInstitucional_Select]
  @idEventoInstitucional NUMERIC = NULL
 ,@idTipoEvento NUMERIC = NULL
 ,@idPersona NUMERIC = NULL
 ,@fecha DATE = NULL
 ,@fechaDesde DATE = NULL
 ,@fechaHasta DATE = NULL
 ,@hora TIME = NULL
 ,@lugar VARCHAR(50) = NULL
 ,@titulo VARCHAR(50) = NULL
 ,@detalle VARCHAR(4000) = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;

    SELECT 
	   E.[idEventoInstitucional]
      ,E.[idTipoEvento]
      ,E.[idPersona]
      ,E.[fecha]
      ,E.[hora]
      ,E.[lugar]
      ,E.[titulo]
      ,E.[detalle]
      ,E.[activo]
      ,P.nombre as 'nombreOrganizador'
      ,P.apellido as 'apellidoOrganizador'
			,P.username as 'username'
      ,TE.descripcion as 'tipoEvento'
    FROM EventoInstitucional AS E 
		INNER JOIN Personas AS P ON E.idPersona = P.idPersona
		INNER JOIN TipoEventoInstitucional AS TE ON E.idTipoEvento = TE.idTipoEventoInstitucional 
		
    WHERE (@idEventoInstitucional IS NULL OR @idEventoInstitucional=E.idEventoInstitucional)
   AND (@idTipoEvento IS NULL OR @idTipoEvento=E.idTipoEvento)
   AND (@idPersona IS NULL OR @idPersona=E.idPersona)
   AND (@fecha IS NULL OR @fecha=E.fecha)
   AND (@hora IS NULL OR @hora=E.hora)
   AND (@lugar IS NULL OR @lugar=E.lugar)
   AND (@titulo IS NULL OR @titulo=E.titulo)
   AND (@detalle IS NULL OR @detalle=E.detalle)
   AND (@activo IS NULL OR @activo=E.activo)
   AND (@fechaDesde IS NULL OR E.fecha >= @fechaDesde)
   AND (@fechaHasta IS NULL OR E.fecha <= @fechaHasta)
   ORDER BY E.[fecha] DESC

GO
/****** Object:  StoredProcedure [dbo].[EventoInstitucional_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 31/07/11
-- =============================================
CREATE PROCEDURE [dbo].[EventoInstitucional_Update]
  @idEventoInstitucional NUMERIC = NULL
 ,@idTipoEvento NUMERIC = NULL
 ,@username varchar(50) = NULL
 ,@fecha DATE = NULL
 ,@hora TIME = NULL
 ,@lugar VARCHAR(50) = NULL
 ,@titulo VARCHAR(50) = NULL
 ,@detalle VARCHAR(4000) = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;

Declare @idPersona numeric = null
select @idPersona = idPersona
from Personas where Personas.username = @username 

--if (@idPersona <> null)
--begin
    UPDATE EventoInstitucional SET 
      idTipoEvento = @idTipoEvento
     ,idPersona = @idPersona
     ,fecha = @fecha
     ,hora = @hora
     ,lugar = @lugar
     ,titulo = @titulo
     ,detalle = @detalle
     ,activo = @activo
    WHERE  idEventoInstitucional =@idEventoInstitucional
--end

GO
/****** Object:  StoredProcedure [dbo].[Excursion_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 06/08/11
-- =============================================
CREATE PROCEDURE [dbo].[Excursion_Insert]
  @idExcursion NUMERIC = NULL
 ,@idEventoAgenda NUMERIC = NULL
 ,@horaDesde TIME = NULL
 ,@horaHasta TIME = NULL
 ,@destino VARCHAR(250) = NULL
 ,@idAgendaActividades NUMERIC = NULL
 ,@username VARCHAR(50) = NULL
 ,@idTipoEvento NUMERIC = NULL
 ,@fechaModificacion DATE = NULL
 ,@fechaEvento DATE = NULL
 ,@descripcion VARCHAR(4000) = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;

 Declare @idEvento numeric
 
 EXECUTE @idEvento = [EventoAgenda_Insert] 
   @idEventoAgenda
  ,@idAgendaActividades
  ,@username
  ,@idTipoEvento
  ,@fechaModificacion
  ,@fechaEvento
  ,@descripcion
  ,@activo
  
    INSERT INTO Excursion(
     idEvento
    ,horaDesde
    ,horaHasta
    ,destino
 )
 VALUES(
     @idEvento
    ,@horaDesde
    ,@horaHasta
    ,@destino
 )
 SET @idExcursion = @@IDENTITY

GO
/****** Object:  StoredProcedure [dbo].[Excursion_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 14/08/11
-- =============================================
CREATE PROCEDURE [dbo].[Excursion_Select]
  @idExcursion NUMERIC = NULL
 ,@idEvento NUMERIC = NULL
 ,@idAgendaActividad NUMERIC = NULL
 ,@fechaEvento DATE = NULL
 ,@fechaDesde DATETIME = NULL
 ,@fechaHasta DATETIME = NULL
 ,@activo BIT = NULL 
 
AS
 SET NOCOUNT OFF;

SELECT 
     TP.descripcion AS 'tipoEvento'
    ,EA.activo
    ,EA.descripcion
    ,EA.fechaAlta
    ,EA.fechaModificacion
    ,EA.fechaEvento
    ,EA.idAgendaActividades
    ,EA.idEventoAgenda
    ,EA.idOrganizador
    ,EA.idTipoEvento
    ,X.idExcursion
    ,X.horaDesde
    ,X.horaHasta
    ,X.destino
    ,P.nombre
    ,P.apellido
    ,P.username AS 'usernameOrganizador'
    FROM EventoAgenda AS EA 
		INNER JOIN TipoEvento AS TP ON EA.idTipoEvento = TP.idTipoEvento
		INNER JOIN Excursion AS X ON EA.idEventoAgenda = X.idEvento
		INNER JOIN Personas AS P ON EA.idOrganizador = P.idPersona
    WHERE 
		(@idEvento IS NULL OR @idEvento <> X.idEvento)
	AND (@fechaEvento IS NULL OR EA.fechaEvento = @fechaEvento)
	--AND (@fechaDesde IS NULL OR @fechaDesde >= EA.fechaEvento)
	--AND (@fechaHasta IS NULL OR @fechaHasta <= EA.fechaEvento)
	AND (@fechaDesde IS NULL OR (EA.fechaEvento BETWEEN @fechaDesde AND @fechaHasta))
	AND (@idAgendaActividad IS NULL OR @idAgendaActividad = EA.idAgendaActividades)
	AND (@activo IS NULL OR @activo = EA.activo)
	AND (@idExcursion IS NULL OR @idExcursion=X.idExcursion)

GO
/****** Object:  StoredProcedure [dbo].[Excursion_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 18/08/11
-- =============================================
CREATE PROCEDURE [dbo].[Excursion_Update]
  @idAgendaActividades NUMERIC = NULL
 ,@username VARCHAR(50) = NULL
 ,@idTipoEvento NUMERIC = NULL
 ,@fechaAlta DATE = NULL
 ,@fechaModificacion DATE = NULL
 ,@fechaEvento DATE = NULL
 ,@descripcion VARCHAR(4000) = NULL
 ,@activo BIT = NULL 
 ,@idExcursion NUMERIC = NULL
 ,@idEventoAgenda NUMERIC = NULL
 ,@horaDesde TIME = NULL
 ,@horaHasta TIME = NULL
 ,@destino VARCHAR(50) = NULL
AS
 SET NOCOUNT OFF;

DECLARE @idOrganizador NUMERIC = NULL

SELECT @idOrganizador = idPersona
FROM Personas WHERE username = @username

	EXECUTE EventoAgenda_Update 
	 @idEventoAgenda
   ,@idAgendaActividades 
	 ,@idOrganizador 
	 ,@idTipoEvento 
	 ,@fechaModificacion 
	 ,@fechaEvento 
	 ,@descripcion 
	 ,@activo

    UPDATE Excursion SET 
     horaDesde = @horaDesde
     ,horaHasta = @horaHasta
     ,destino = @destino
     ,idEvento = @idEventoAgenda
    WHERE @idExcursion=idExcursion

GO
/****** Object:  StoredProcedure [dbo].[Excursion_VerificarDisponibilidadAgenda]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 28/08/11
-- =============================================
CREATE PROCEDURE [dbo].[Excursion_VerificarDisponibilidadAgenda]
 @fechaEvento DATE = NULL
 ,@idAgendaActividad NUMERIC = NULL
 ,@idEventoAgenda NUMERIC = NULL
AS
 SET NOCOUNT OFF;

SELECT 
    EA.fechaEvento
    ,EA.idAgendaActividades
    ,EA.idEventoAgenda
    ,EA.idTipoEvento
    ,0 'idAsignaturaCurso'
    FROM EventoAgenda AS EA 
		INNER JOIN Excursion AS EV ON EA.idEventoAgenda = EV.idEvento
		INNER JOIN AgendaActividades AS AA ON AA.idAgendaActividad = EA.idAgendaActividades
    WHERE 
	    (@fechaEvento IS NULL OR EA.fechaEvento = @fechaEvento)
	AND (@idAgendaActividad IS NULL OR EA.idAgendaActividades = @idAgendaActividad)
	AND (@idEventoAgenda IS NULL OR EA.idEventoAgenda <> @idEventoAgenda)
	AND (EA.idTipoEvento = 3)
	AND (EA.activo = 1)

GO
/****** Object:  StoredProcedure [dbo].[Feriados_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 28/08/11
-- =============================================
CREATE PROCEDURE [dbo].[Feriados_Select]
  @idFeriado NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@fecha DATE = NULL
 ,@descripcion VARCHAR(400) = NULL
AS
 SET NOCOUNT OFF;

    SELECT idFeriado
    ,nombre
    ,fecha
    ,descripcion
    FROM Feriados
    WHERE (@idFeriado IS NULL OR @idFeriado=idFeriado)
   AND (@nombre IS NULL OR @nombre=nombre)
   AND (@fecha IS NULL OR @fecha=fecha)
   AND (@descripcion IS NULL OR @descripcion=descripcion)

GO
/****** Object:  StoredProcedure [dbo].[GetAsignaturasPorNivelesCicloLectivos_select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 22/10/11
-- =============================================

CREATE PROCEDURE [dbo].[GetAsignaturasPorNivelesCicloLectivos_select]

  @listaNivel VARCHAR(MAX) = NULL
 ,@listaCicloLectivo VARCHAR(MAX) = NULL
	AS
		SET NOCOUNT OFF;

	SELECT DISTINCT ACL.idAsignatura, A.nombre as 'nombreAsignatura'
	FROM Nivel N
		INNER JOIN Curso AS C ON N.idNivel=C.idNivel
		INNER JOIN CursosCicloLectivo AS CCL ON C.idCurso=CCL.idCurso
		INNER JOIN AsignaturaCicloLectivo AS ACL ON CCL.idCursoCicloLectivo=ACL.idCursoCicloLectivo
		INNER JOIN Asignatura AS A ON ACL.idAsignatura=A.idAsignatura
	WHERE (@listaNivel IS NULL OR N.idNivel IN (SELECT * FROM [ArrayToTable] (@listaNivel)))
	AND (@listaCicloLectivo IS NULL OR CCL.idCicloLectivo IN (SELECT * FROM [ArrayToTable] (@listaCicloLectivo)))

GO
/****** Object:  StoredProcedure [dbo].[HorariosCurso_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 19/08/2011
-- =============================================
CREATE PROCEDURE [dbo].[HorariosCurso_Select] 
	 @idDiaSemana NUMERIC = NULL
	,@idAsignaturaCurso NUMERIC = NULL
	,@idDiaHorario NUMERIC = NULL
AS

 SET NOCOUNT OFF;

SELECT
	   [DiaHorario].[idDiaHorario]
      ,[DiaHorario].[idDiaSemana]
      ,Modulo.idModulo
      ,Modulo.horaInicio
      ,Modulo.horaFinalizacion
      ,AsignaturaCicloLectivo.idAsignaturaCicloLectivo
  FROM [DiaHorario] 
	INNER JOIN Modulo on DiaHorario.idDiaHorario = Modulo.idDiaHorario
	INNER JOIN AsignaturaCicloLectivo 
		on AsignaturaCicloLectivo.idAsignatura = DiaHorario.idAsignaturaCicloLectivo
	WHERE 
		(@idDiaSemana IS NULL OR @idDiaSemana = [DiaHorario].[idDiaSemana])
		AND
		(@idDiaHorario IS NULL OR @idDiaHorario = [DiaHorario].[idDiaHorario])
		AND
		(@idAsignaturaCurso IS NULL OR @idAsignaturaCurso = AsignaturaCicloLectivo.idAsignaturaCicloLectivo)

GO
/****** Object:  StoredProcedure [dbo].[Indicadores_Inasistencias]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Indicadores_Inasistencias]	
	@FechaDesde DATETIME,
	@FechaHasta DATETIME,
	@idCursoCicloLectivo NUMERIC
AS
BEGIN	
	SET NOCOUNT ON;
	DECLARE @parametroCantidad NUMERIC
	DECLARE @cantidad NUMERIC
	DECLARE @cantDictados NUMERIC
	DECLARE @resultado NUMERIC
	
SELECT @parametroCantidad = parametroCantidad
FROM dbo.Indicadores 
WHERE nombreSP = 'Inasistencias'
	
	SELECT @cantidad = COUNT(ACCL.idAlumnoCursoCicloLectivo)
	
	FROM dbo.AlumnoCursoCicloLectivo AS ACCL
		
	WHERE 
		ACCL.idCursoCicloLectivo = @idCursoCicloLectivo
		AND
		ACCL.idAlumnoCursoCicloLectivo IN
		(
			SELECT ACCL2.idAlumnoCursoCicloLectivo
			FROM dbo.Asistencia AS A
				INNER JOIN dbo.AlumnoCursoCicloLectivo AS ACCL2 ON A.idAlumnoCursoCicloLectivo = ACCL2.idAlumnoCursoCicloLectivo
				INNER JOIN dbo.TipoAsistencia AS TA ON TA.idTipoAsistencia = A.idTipoAsistencia
			WHERE A.idTipoAsistencia <> 13
				AND
				ACCL.idCursoCicloLectivo = @idCursoCicloLectivo
				AND
				A.fecha BETWEEN @FechaDesde AND @FechaHasta

			GROUP BY ACCL2.idAlumnoCursoCicloLectivo

			HAVING SUM(TA.valor) > @parametroCantidad
		) 
	GROUP BY ACCL.idCursoCicloLectivo
	
	SELECT @cantidad
	
	END

GO
/****** Object:  StoredProcedure [dbo].[Indicadores_InasistenciasDocentes]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Indicadores_InasistenciasDocentes]
	@FechaDesde DATETIME,
	@FechaHasta DATETIME,
	@idCursoCicloLectivo NUMERIC
AS
BEGIN	
	SET NOCOUNT ON;
	DECLARE @parametroCantidad NUMERIC
	DECLARE @cantidad NUMERIC
	DECLARE @cantDictados NUMERIC
	DECLARE @resultado NUMERIC
	
	SELECT @cantidad = COUNT(ACL.idDocente)
	FROM
		dbo.RegistroClases AS RC 
			INNER JOIN dbo.TipoRegistroClases AS TRC ON RC.idTipoRegistroClases = TRC.idTipoRegistroClases
			INNER JOIN dbo.AsignaturaCicloLectivo AS ACL ON ACL.idAsignaturaCicloLectivo = RC.idAsignaturaCurso
			INNER JOIN dbo.EventoAgenda AS EA ON EA.idEventoAgenda = RC.idEventoAgenda
	WHERE TRC.idTipoRegistroClases IN (2,3,5)
		AND
		ACL.idCursoCicloLectivo = @idCursoCicloLectivo
		AND
		EA.fechaEvento BETWEEN @FechaDesde AND @FechaHasta
		AND ACL.idDocente IN 
	(
		SELECT ACL_.IdDocente
			FROM 
				dbo.RegistroClases AS RC_
				INNER JOIN dbo.AsignaturaCicloLectivo AS ACL_ ON ACL_.idAsignaturaCicloLectivo = RC_.idAsignaturaCurso
			WHERE 
				ACL_.idCursoCicloLectivo = @idCursoCicloLectivo
	)
	
	SELECT @cantidad
	
	END


GO
/****** Object:  StoredProcedure [dbo].[Indicadores_Planificacion]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Indicadores_Planificacion]	
	@FechaDesde DATETIME,
	@FechaHasta DATETIME,
	@idCursoCicloLectivo NUMERIC
AS
BEGIN	
	SET NOCOUNT ON;

--declare	@FechaDesde DATEtime = '02/25/2013 00:00:00.000'  
--declare	@FechaHasta DATEtime = '08/12/2013 00:00:00.000'
--declare	@idCursoCicloLectivo NUMERIC = 10507

	DECLARE @cantPlanificados NUMERIC
	DECLARE @cantDictados NUMERIC
	DECLARE @resultado NUMERIC

CREATE TABLE #TablaTemporal (
	planificados INT,
	dictados INT,
	asignatura VARCHAR (80)
) 

	INSERT INTO #TablaTemporal SELECT
		(
		SELECT
			COUNT (DISTINCT TPTC.idTemaContenido)
		FROM
			CicloLectivo AS CL
			INNER JOIN
		CursosCicloLectivo AS CCL ON CL.idCicloLectivo = CCL.idCicloLectivo AND CL.activo = 1
			INNER JOIN
		AsignaturaCicloLectivo AS ACL ON CCL.idCursoCicloLectivo = ACL.idCursoCicloLectivo
			INNER JOIN 
		Curricula AS C ON C.idAsignatura = ACL.idAsignatura
			INNER JOIN
		PlanificacionAnual AS PA ON PA.idCurricula = C.idCurricula
			INNER JOIN 
		TemaPlanificacionAnual AS TPA ON PA.idPlanificacionAnual = TPA.idPlanificacionAnual
			INNER JOIN
		TemaPlanificacionTemaContenido AS TPTC ON TPA.idTemaPlanificacion = TPTC.idTemaPlanificacion
			INNER JOIN 
		TemaContenido AS TC ON TC.idTemaContenido = TPTC.idTemaContenido AND TC.activo = 1

			WHERE
				TPA.fechaInicioEstimada BETWEEN @FechaDesde AND @FechaHasta
			AND TPA.fechaFinEstimada BETWEEN @FechaDesde AND @FechaHasta
			AND CCL.idCursoCicloLectivo = @idCursoCicloLectivo
			AND ACL.idAsignaturaCicloLectivo = ACL3.idAsignaturaCicloLectivo
		) AS 'planificados',
		ISNULL(
				(
				SELECT
					COUNT (DISTINCT DRC2.idTemaContenido)
				FROM
					CicloLectivo AS CL2
				INNER JOIN CursosCicloLectivo AS CCL2 ON CL2.idCicloLectivo = CCL2.idCicloLectivo
				AND CL2.activo = 1
				INNER JOIN AsignaturaCicloLectivo AS ACL2 ON CCL2.idCursoCicloLectivo = ACL2.idCursoCicloLectivo
				INNER JOIN Asignatura AS A ON ACL2.idAsignatura = A.idAsignatura
				LEFT OUTER JOIN RegistroClases AS RC ON RC.idAsignaturaCurso = ACL2.idAsignaturaCicloLectivo
				LEFT OUTER JOIN EventoAgenda AS EA2 ON RC.idEventoAgenda = EA2.idEventoAgenda
				LEFT OUTER JOIN DetalleRegistroClases AS DRC2 ON RC.idRegistroClases = DRC2.idRegistroClases
				LEFT OUTER JOIN	TemaContenido AS TC ON TC.idTemaContenido = DRC2.idTemaContenido AND TC.activo = 1
				WHERE
					ACL2.idAsignaturaCicloLectivo = ACL3.idAsignaturaCicloLectivo
				AND EA2.fechaEvento BETWEEN @FechaDesde AND @FechaHasta
				AND CCL2.idCursoCicloLectivo = @idCursoCicloLectivo
			
				GROUP BY
					ACL2.idAsignaturaCicloLectivo,
					A.nombre
			),
			0
		) AS 'dictados',
		A.nombre
	FROM
		CicloLectivo AS CL2
	INNER JOIN CursosCicloLectivo AS CCL2 ON CL2.idCicloLectivo = CCL2.idCicloLectivo
	AND CL2.activo = 1
	INNER JOIN AsignaturaCicloLectivo AS ACL3 ON CCL2.idCursoCicloLectivo = ACL3.idCursoCicloLectivo
	INNER JOIN Asignatura AS A ON ACL3.idAsignatura = A.idAsignatura
	WHERE
		CCL2.idCursoCicloLectivo = @idCursoCicloLectivo 

	SELECT
		@cantPlanificados = sum(planificados),@cantDictados = sum(dictados)
	FROM
		#TablaTemporal
	WHERE
		NOT (
			planificados = 0
			AND dictados = 0
		) 

	DROP TABLE #TablaTemporal
	
	SET @resultado = (@cantDictados / @cantPlanificados) * 100
	
	SELECT ISNULL(@resultado,0)
	
	END

GO
/****** Object:  StoredProcedure [dbo].[Indicadores_PromedioGeneral]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:  LAURA
-- Create date: 09/09/12
-- Parameters:
-- =============================================
CREATE PROCEDURE [dbo].[Indicadores_PromedioGeneral]
	 @idCursoCicloLectivo NUMERIC = NULL
	,@fechaDesde DATE = NULL
	,@fechaHasta DATE = NULL
AS
 SET NOCOUNT ON;

	SELECT
	 ROUND(AVG(CAST(VES.nombre as float)),2,2) as 'Promedio'
	FROM	[AlumnoCursoCicloLectivo] AS ALC 
			INNER JOIN [CursosCicloLectivo] AS CCL ON ALC.idCursoCicloLectivo = CCL.idCursoCicloLectivo
			INNER JOIN [AsignaturaCicloLectivo] AS AC ON CCL.idCursoCicloLectivo = AC.idCursoCicloLectivo
			INNER JOIN [Calificacion] AS CAL ON CAL.idAlumnoCursoCicloLectivo = ALC.idAlumnoCursoCicloLectivo and cal.idAsignaturaCicloLectivo=ac.idAsignatura
			INNER JOIN [ValoresEscalaCalificacion] AS VES ON CAL.idValorCalificacion = VES.idValorEscalaCalificacion
			INNER JOIN [CicloLectivo] AS CL ON CCL.idCicloLectivo = CL.idCicloLectivo AND CL.activo = 1
    WHERE 
	    @idCursoCicloLectivo = CCL.idCursoCicloLectivo
	    AND
	    CAL.fecha BETWEEN @fechaDesde AND @fechaHasta
	GROUP BY ALC.idCursoCicloLectivo


GO
/****** Object:  StoredProcedure [dbo].[Indicadores_Reprobadas]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Indicadores_Reprobadas]	
	@FechaDesde DATETIME,
	@FechaHasta DATETIME,
	@idCursoCicloLectivo NUMERIC
AS
BEGIN	
	SET NOCOUNT ON;

	DECLARE @parametroCantidad NUMERIC = 0
	DECLARE @nivelAprobacion NUMERIC = 0
	DECLARE @idAlumno NUMERIC = 0
	DECLARE @idAlumnoPrevio NUMERIC = 0
	DECLARE @cantidadAlumnos NUMERIC = 0
	DECLARE @cantidadMateriasAlumno NUMERIC = 0
	DECLARE @nombreAlumno VARCHAR(50) = ''
	DECLARE @apellidoAlumno VARCHAR(50) = ''
	DECLARE @asignatura VARCHAR(50) = ''
	DECLARE @promedio FLOAT = 0
	DECLARE @listaAlumnos VARCHAR(1000)=''
		
	SELECT @parametroCantidad = parametroCantidad
	FROM dbo.Indicadores 
	WHERE nombreSP = 'Reprobadas'

	SELECT @nivelAprobacion = MIN(CAST(valor AS NUMERIC))
	FROM dbo.ValoresEscalaCalificacion
	WHERE aprobado = 1 AND descripcion <> '%conceptual%'

	DECLARE ALUMNOS_Cursor CURSOR FOR 
	SELECT
	  ALC.idAlumnoCursoCicloLectivo
	FROM	[AlumnoCursoCicloLectivo] AS ALC 
			INNER JOIN [CursosCicloLectivo] AS CCL ON ALC.idCursoCicloLectivo = CCL.idCursoCicloLectivo
			INNER JOIN [AsignaturaCicloLectivo] AS AC ON CCL.idCursoCicloLectivo = AC.idCursoCicloLectivo
			INNER JOIN [Calificacion] AS CAL ON CAL.idAlumnoCursoCicloLectivo = ALC.idAlumnoCursoCicloLectivo and cal.idAsignaturaCicloLectivo=ac.idAsignatura
			INNER JOIN [ValoresEscalaCalificacion] AS VES ON CAL.idValorCalificacion = VES.idValorEscalaCalificacion
			INNER JOIN [CicloLectivo] AS CL ON CCL.idCicloLectivo = CL.idCicloLectivo AND CL.activo = 1
	WHERE 
		@idCursoCicloLectivo = CCL.idCursoCicloLectivo
		AND
		CAL.fecha BETWEEN @fechaDesde AND @fechaHasta
	GROUP BY ALC.idAlumnoCursoCicloLectivo, AC.idAsignaturaCicloLectivo
	HAVING ROUND(AVG(CAST(VES.nombre as float)),2,2) < @nivelAprobacion
	ORDER BY 1;

	SET @idAlumnoPrevio = 0
	
	CREATE TABLE #TablaTemporal (idalumno INT)

	OPEN ALUMNOS_Cursor;
	FETCH NEXT FROM ALUMNOS_Cursor INTO @idAlumno
	WHILE @@FETCH_STATUS = 0
		BEGIN
			IF(@idAlumno <> @idAlumnoPrevio)
				BEGIN
					IF (@cantidadMateriasAlumno >= @parametroCantidad)
					BEGIN
						--SET @cantidadAlumnos = @cantidadAlumnos + 1
						SET @listaAlumnos = CAST( @idAlumnoPrevio AS VARCHAR) + ',' + @listaAlumnos
					END
					SET @cantidadMateriasAlumno = 0
				END
			
			SET	@cantidadMateriasAlumno = @cantidadMateriasAlumno + 1	
			SET @idAlumnoPrevio = @idAlumno
			
			INSERT INTO #TablaTemporal (idalumno) VALUES ( /* idAlumno - NUMERIC*/ @idAlumno );
			
		  FETCH NEXT FROM ALUMNOS_Cursor INTO @idAlumno
		END;
		-- PARA EVALUAR COMO QUEDO EL ÚLTIMO ALUMNO
		IF (@cantidadMateriasAlumno > @parametroCantidad)
			BEGIN
				--SET @cantidadAlumnos = @cantidadAlumnos + 1
				SET @listaAlumnos = CAST( @idAlumnoPrevio AS VARCHAR) + ',' + @listaAlumnos
			END
		
		DELETE FROM #TablaTemporal
		 WHERE idalumno NOT IN (SELECT * FROM [ArrayToTable] (@listaAlumnos))
		
		SELECT @cantidadAlumnos = COUNT(*)
		FROM (SELECT DISTINCT idalumno
				FROM #TablaTemporal
				) AS Resultado
		
		DROP TABLE #TablaTemporal
		
		SELECT @cantidadAlumnos
	CLOSE ALUMNOS_Cursor;
	DEALLOCATE ALUMNOS_Cursor;
END


GO
/****** Object:  StoredProcedure [dbo].[Indicadores_Sanciones]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Indicadores_Sanciones]	
	@FechaDesde DATETIME,
	@FechaHasta DATETIME,
	@idCursoCicloLectivo NUMERIC
AS
BEGIN	
	SET NOCOUNT ON;
	
  DECLARE @parametroCantidad NUMERIC = 0
	DECLARE @sancionado NUMERIC = 0
		
	SELECT @parametroCantidad = parametroCantidad
	FROM dbo.Indicadores 
	WHERE nombreSP = 'Sanciones'

	DECLARE Sancionados_Cursor CURSOR FOR 
	SELECT COUNT(S.idAlumnoCursoCicloLectivo)
	FROM Sancion AS S
		INNER JOIN dbo.TipoSancion AS TP ON S.idTipoSancion = TP.idTipoSancion
		INNER JOIN dbo.AlumnoCursoCicloLectivo AS ACCL ON ACCL.idAlumnoCursoCicloLectivo = S.idAlumnoCursoCicloLectivo
	WHERE S.fecha BETWEEN @FechaDesde AND @FechaHasta
		AND ACCL.idCursoCicloLectivo = @idCursoCicloLectivo
		AND S.idTipoSancion = 4
	GROUP BY S.idAlumnoCursoCicloLectivo
	HAVING SUM(S.cantidad) > @parametroCantidad
	
	CREATE TABLE #TablaTemporal (var_aux INT)

	OPEN Sancionados_Cursor;
	FETCH NEXT FROM Sancionados_Cursor INTO @sancionado
	WHILE @@FETCH_STATUS = 0
		BEGIN	
			INSERT INTO #TablaTemporal(var_aux) VALUES (@sancionado) ;
			
		  FETCH NEXT FROM Sancionados_Cursor INTO @sancionado
		END;
		
		SELECT 
			   COUNT(*)
		FROM #TablaTemporal
		
		DROP TABLE #TablaTemporal
	CLOSE Sancionados_Cursor;
	DEALLOCATE Sancionados_Cursor;
END
GO
/****** Object:  StoredProcedure [dbo].[Indicadores_Sanciones_TBR]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Indicadores_Sanciones_TBR]	
	@FechaDesde DATETIME,
	@FechaHasta DATETIME,
	@idCursoCicloLectivo NUMERIC
AS
BEGIN	
	SET NOCOUNT ON;
	DECLARE @parametroCantidad NUMERIC
	DECLARE @cantidad NUMERIC
	DECLARE @cantDictados NUMERIC
	DECLARE @resultado NUMERIC
	
SELECT @parametroCantidad = parametroCantidad
FROM dbo.Indicadores 
WHERE nombreSP = 'Sanciones'
	
SELECT @cantidad = COUNT(S.idAlumnoCursoCicloLectivo)
FROM Sancion AS S
INNER JOIN dbo.TipoSancion AS TP ON S.idTipoSancion = TP.idTipoSancion
WHERE S.fecha BETWEEN @FechaDesde AND @FechaHasta
AND S.idAlumnoCursoCicloLectivo IN
(
	SELECT ACCL.idAlumnoCursoCicloLectivo
	FROM AlumnoCursoCicloLectivo AS ACCL
	WHERE 
			ACCL.idCursoCicloLectivo = @idCursoCicloLectivo
)
HAVING SUM(S.cantidad) > @parametroCantidad
	
SELECT @cantidad
	
END
GO
/****** Object:  StoredProcedure [dbo].[Indicadores_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Indicadores_Select]
  @idIndicador INT = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@invertirEscala BIT = NULL
 ,@parametroCantidad INT = NULL
 ,@diasHastaPrincipal INT = NULL
 ,@diasHastaIntermedio INT = NULL
 ,@diasHastaSecundario INT = NULL
 ,@verdeNivelPrincipal INT = NULL
 ,@verdeNivelIntermedio INT = NULL
 ,@verdeNivelSecundario INT = NULL
 ,@rojoNivelPrincipal INT = NULL
 ,@rojoNivelIntermedio INT = NULL
 ,@rojoNivelSecundario INT = NULL
AS
 SET NOCOUNT OFF;

  SELECT idIndicador
    ,CASE parametroCantidad
		WHEN ISNULL(parametroCantidad,NULL) THEN REPLACE(nombre,'X',parametroCantidad)
		ELSE nombre
	 END AS 'nombre'
    ,nombreSP
    ,invertirEscala
    ,parametroCantidad
    ,diasHastaPrincipal
    ,diasHastaIntermedio
    ,diasHastaSecundario
    ,verdeNivelPrincipal
    ,verdeNivelIntermedio
    ,verdeNivelSecundario
    ,rojoNivelPrincipal
    ,rojoNivelIntermedio
    ,rojoNivelSecundario
    FROM Indicadores
    WHERE (@idIndicador IS NULL OR @idIndicador=idIndicador)
   AND (@nombre IS NULL OR @nombre=nombre)
   AND (@invertirEscala IS NULL OR @invertirEscala=invertirEscala)
   AND (@parametroCantidad IS NULL OR @parametroCantidad=parametroCantidad)
   AND (@diasHastaPrincipal IS NULL OR @diasHastaPrincipal=diasHastaPrincipal)
   AND (@diasHastaIntermedio IS NULL OR @diasHastaIntermedio=diasHastaIntermedio)
   AND (@diasHastaSecundario IS NULL OR @diasHastaSecundario=diasHastaSecundario)
   AND (@verdeNivelPrincipal IS NULL OR @verdeNivelPrincipal=verdeNivelPrincipal)
   AND (@verdeNivelIntermedio IS NULL OR @verdeNivelIntermedio=verdeNivelIntermedio)
   AND (@verdeNivelSecundario IS NULL OR @verdeNivelSecundario=verdeNivelSecundario)
   AND (@rojoNivelPrincipal IS NULL OR @rojoNivelPrincipal=rojoNivelPrincipal)
   AND (@rojoNivelIntermedio IS NULL OR @rojoNivelIntermedio=rojoNivelIntermedio)
   AND (@rojoNivelSecundario IS NULL OR @rojoNivelSecundario=rojoNivelSecundario)

GO
/****** Object:  StoredProcedure [dbo].[Indicadores_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Indicadores_Update]
  @idIndicador INT = NULL
 ,@invertirEscala BIT = NULL
 ,@parametroCantidad INT = NULL
 ,@diasHastaPrincipal INT = NULL
 ,@diasHastaIntermedio INT = NULL
 ,@diasHastaSecundario INT = NULL
 ,@verdeNivelPrincipal INT = NULL
 ,@verdeNivelIntermedio INT = NULL
 ,@verdeNivelSecundario INT = NULL
 ,@rojoNivelPrincipal INT = NULL
 ,@rojoNivelIntermedio INT = NULL
 ,@rojoNivelSecundario INT = NULL
AS
 SET NOCOUNT OFF;

    UPDATE Indicadores SET 
      invertirEscala = @invertirEscala
     ,parametroCantidad = @parametroCantidad
     ,diasHastaPrincipal = @diasHastaPrincipal
     ,diasHastaIntermedio = @diasHastaIntermedio
     ,diasHastaSecundario = @diasHastaSecundario
     ,verdeNivelPrincipal = @verdeNivelPrincipal
     ,verdeNivelIntermedio = @verdeNivelIntermedio
     ,verdeNivelSecundario = @verdeNivelSecundario
     ,rojoNivelPrincipal = @rojoNivelPrincipal
     ,rojoNivelIntermedio = @rojoNivelIntermedio
     ,rojoNivelSecundario = @rojoNivelSecundario
    WHERE  @idIndicador=idIndicador
   

GO
/****** Object:  StoredProcedure [dbo].[Localidades_Delete]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 14/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Localidades_Delete]
  @idLocalidad NUMERIC = NULL
 ,@idLocalidadTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@descripcion VARCHAR(250) = NULL
 ,@idProvincia NUMERIC = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;

    DELETE FROM Localidades
    WHERE (@idLocalidad IS NULL OR @idLocalidad=idLocalidad)
   AND (@idLocalidadTransaccional IS NULL OR @idLocalidadTransaccional=idLocalidadTransaccional)
   AND (@nombre IS NULL OR @nombre=nombre)
   AND (@descripcion IS NULL OR @descripcion=descripcion)
   AND (@idProvincia IS NULL OR @idProvincia=idProvincia)
   AND (@activo IS NULL OR @activo=activo)

-- =====UPDATE==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[Localidades_Exists]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 14/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Localidades_Exists]
  @idLocalidad NUMERIC = NULL
 ,@idLocalidadTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@descripcion VARCHAR(250) = NULL
 ,@idProvincia NUMERIC = NULL
 ,@activo BIT = NULL
 ,@exists BIT OUT
AS
 SET NOCOUNT OFF;

    IF EXISTS (
 SELECT idLocalidad
    FROM Localidades
    WHERE (@idLocalidad IS NULL OR @idLocalidad=idLocalidad)
   AND (@idLocalidadTransaccional IS NULL OR @idLocalidadTransaccional=idLocalidadTransaccional)
   AND (@nombre IS NULL OR @nombre=nombre)
   AND (@descripcion IS NULL OR @descripcion=descripcion)
   AND (@idProvincia IS NULL OR @idProvincia=idProvincia)
   AND (@activo IS NULL OR @activo=activo)
 )
 SET @exists = 1
 ELSE SET @exists = 0

GO
/****** Object:  StoredProcedure [dbo].[Localidades_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 14/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Localidades_Insert]
  @idLocalidad NUMERIC = NULL output
 ,@idLocalidadTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@descripcion VARCHAR(250) = NULL
 ,@idProvincia NUMERIC = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;
	SELECT @idLocalidad=idLocalidad 
  FROM [dbo].[Localidades]
  WHERE [nombre]= @nombre
  
  SELECT @idProvincia=idProvincia 
  FROM [dbo].[Provincias]
  WHERE [idProvinciaTransaccional]= @idProvincia
  
  IF (@idLocalidad=0 or @idLocalidad is null)
  BEGIN
    INSERT INTO Localidades(
     idLocalidadTransaccional
    ,nombre
    ,descripcion
    ,idProvincia
    ,activo
 )
 VALUES(
     @idLocalidadTransaccional
    ,@nombre
    ,@descripcion
    ,@idProvincia
    ,@activo
 )
SET @idLocalidad = @@IDENTITY
   END
ELSE
BEGIN
	EXECUTE [Localidades_Update] 
	   @idLocalidad
	  ,@idLocalidadTransaccional
	  ,@nombre
	  ,@descripcion
	  ,@idProvincia
	  ,@activo
END

GO
/****** Object:  StoredProcedure [dbo].[Localidades_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 14/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Localidades_Select]
  @idLocalidad NUMERIC = NULL
 ,@idLocalidadTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@descripcion VARCHAR(250) = NULL
 ,@idProvincia NUMERIC = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;

    SELECT idLocalidad
    ,idLocalidadTransaccional
    ,nombre
    ,descripcion
    ,idProvincia
    ,activo
    FROM Localidades
    WHERE (@idLocalidad IS NULL OR @idLocalidad=idLocalidad)
   AND (@idLocalidadTransaccional IS NULL OR @idLocalidadTransaccional=idLocalidadTransaccional)
   AND (@nombre IS NULL OR @nombre=nombre)
   AND (@descripcion IS NULL OR @descripcion=descripcion)
   AND (@idProvincia IS NULL OR @idProvincia=idProvincia)
   AND (@activo IS NULL OR @activo=activo)

-- =====EXISTS==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[Localidades_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 14/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Localidades_Update]
  @idLocalidad NUMERIC = NULL
 ,@idLocalidadTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@descripcion VARCHAR(250) = NULL
 ,@idProvincia NUMERIC = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;

    UPDATE Localidades SET 
      idLocalidadTransaccional = @idLocalidadTransaccional
     ,nombre = @nombre
     ,descripcion = @descripcion
     ,idProvincia = @idProvincia
     ,activo = @activo
    WHERE (@idLocalidad IS NULL OR @idLocalidad=idLocalidad)
  
-- =====SELECT==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[Mensaje_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 31/08/11
-- =============================================
CREATE PROCEDURE [dbo].[Mensaje_Insert]
  @idMensaje NUMERIC = 0 output
 ,@username VARCHAR(50) = NULL  
 ,@asuntoMensaje VARCHAR(5000) = NULL
 ,@textoMensaje VARCHAR(5000) = NULL
 ,@fechaEnvio DATE = NULL
 ,@horaEnvio TIME = NULL
 ,@activo BIT = NULL
 ,@idCursoCicloLectivo NUMERIC = NULL
AS
 SET NOCOUNT OFF;

Declare @idPersonaRemitente numeric = null
select @idPersonaRemitente = idPersona
from Personas where Personas.username = @username 

    INSERT INTO Mensaje(
     asuntoMensaje
    ,textoMensaje
    ,fechaEnvio
    ,horaEnvio
    ,idPersonaRemitente
	,activo    
	,idCursoCicloLectivo
 )
 VALUES(
     @asuntoMensaje
    ,@textoMensaje
    ,@fechaEnvio
    ,@horaEnvio
    ,@idPersonaRemitente
    ,@activo
    ,@idCursoCicloLectivo
 )
 
SET @idMensaje = @@IDENTITY

GO
/****** Object:  StoredProcedure [dbo].[MensajeDestinatario_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 07/09/11
-- =============================================
CREATE PROCEDURE [dbo].[MensajeDestinatario_Update]
  @idMensajeDestinatario NUMERIC = NULL
 ,@leido BIT = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;

	UPDATE [MensajeDestinatarios]
	SET  [leido] = @leido
		,[activo] = @activo
	WHERE [MensajeDestinatarios].idMensajeDestinatario =  @idMensajeDestinatario

GO
/****** Object:  StoredProcedure [dbo].[MensajeDestinatarios_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 31/08/11
-- =============================================
CREATE PROCEDURE [dbo].[MensajeDestinatarios_Insert]
  @idMensajeDestinatario NUMERIC = NULL
 ,@idMensaje NUMERIC = NULL
 ,@idPersonaDestinatario NUMERIC = NULL
 ,@leido BIT = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;

    INSERT INTO MensajeDestinatarios(
     idMensaje
    ,idPersonaDestinatario
    ,leido
    ,activo
 )
 VALUES(
     @idMensaje
    ,@idPersonaDestinatario
    ,@leido
    ,@activo
 )
 SET @idMensajeDestinatario = @@IDENTITY

GO
/****** Object:  StoredProcedure [dbo].[MensajeEnviado_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 11/09/11
-- =============================================
CREATE PROCEDURE [dbo].[MensajeEnviado_Select]
  @idMensaje NUMERIC = NULL
 ,@asuntoMensaje VARCHAR(100) = NULL
 ,@textoMensaje VARCHAR(5000) = NULL
 ,@fechaEnvio DATE = NULL
 ,@horaEnvio TIME = NULL
 ,@activo BIT = NULL
 ,@usuarioRemitente VARCHAR(50) = NULL
 ,@leido BIT = NULL
AS
 SET NOCOUNT OFF;

Declare @idPersonaRemitente numeric = null
select @idPersonaRemitente = idPersona
from Personas where Personas.username = @usuarioRemitente 

SELECT 
		 Mensaje.idMensaje
		,MensajeDestinatarios.idMensajeDestinatario
		,MensajeDestinatarios.idPersonaDestinatario
		,Mensaje.idPersonaRemitente
		,Mensaje.asuntoMensaje
		,Mensaje.textoMensaje
		,Mensaje.fechaEnvio
		,Mensaje.horaEnvio
		,Mensaje.activo
		,P.nombre as 'nombreDestinatario'
		,P.apellido as 'apellidoDestinatario'
		,PR.nombre as 'nombreRemitente'
		,PR.apellido as 'apellidoRemitente'
		,MensajeDestinatarios.leido
		,case TPD.idTipoPersona
		when 1 THEN
			(select CargosPersonal.nombre 
			from CargosPersonal inner join Personal on CargosPersonal.idCargoPersonal = Personal.idCargoPersonal
				inner join Personas on Personal.idPersona = Personas.idPersona
			where Personas.idPersona = P.idPersona
			)
		ELSE
		TPD.nombre
    end as 'tipoPersonaDestinatario'
	INTO #MensajeResult
    FROM Mensaje INNER JOIN MensajeDestinatarios
		ON Mensaje.idMensaje = MensajeDestinatarios.idMensaje
		INNER JOIN Personas as P ON MensajeDestinatarios.idPersonaDestinatario = P.idPersona
		INNER JOIN Personas as PR ON Mensaje.idPersonaRemitente = PR.idPersona
		INNER JOIN TipoPersona AS TPD  on P.idTipoPersona = TPD.idTipoPersona
	WHERE 
		Mensaje.idCursoCicloLectivo IS NULL	
		AND Mensaje.activo = 1
UNION
		
	SELECT 
		 Mensaje.idMensaje
		,Mensaje.idMensaje AS 'idMensajeDestinatario'
		,Mensaje.idCursoCicloLectivo
		,Mensaje.idPersonaRemitente
		,Mensaje.asuntoMensaje
		,Mensaje.textoMensaje
		,Mensaje.fechaEnvio
		,Mensaje.horaEnvio
		,Mensaje.activo
		,N.nombre + ' - ' + C.nombre AS 'nombreDestinatario'
		,CL.nombre AS 'apellidoDestinatario'
		,PR.nombre as 'nombreRemitente'
		,PR.apellido as 'apellidoRemitente'
		,Mensaje.activo
		,'0' AS 'tipoPersonaDestinatario'
    FROM Mensaje 
		INNER JOIN Personas as PR ON Mensaje.idPersonaRemitente = PR.idPersona
		INNER JOIN CursosCicloLectivo AS CCL ON Mensaje.idCursoCicloLectivo = CCL.idCursoCicloLectivo
		INNER JOIN CicloLectivo AS CL ON CCL.idCicloLectivo = CL.idCicloLectivo
		INNER JOIN Curso AS C ON CCL.idCurso = C.idCurso
		INNER JOIN Nivel AS N ON C.idNivel = N.idNivel
	WHERE Mensaje.idCursoCicloLectivo IS NOT NULL
		AND Mensaje.activo = 1

SELECT *
FROM #MensajeResult
WHERE     
			(@idPersonaRemitente IS NULL OR @idPersonaRemitente= #MensajeResult.idPersonaRemitente)
		AND	(@idMensaje IS NULL OR @idMensaje=#MensajeResult.idMensaje)
		AND (@textoMensaje IS NULL OR @textoMensaje=#MensajeResult.textoMensaje)
		AND (@fechaEnvio IS NULL OR @fechaEnvio=#MensajeResult.fechaEnvio)
		AND (@horaEnvio IS NULL OR @horaEnvio=#MensajeResult.horaEnvio)
		AND (@leido IS NULL OR @leido=#MensajeResult.leido)
		--AND (#MensajeResult.activo = 1)
		
ORDER BY #MensajeResult.fechaEnvio DESC, #MensajeResult.horaEnvio DESC

DROP TABLE #MensajeResult

GO
/****** Object:  StoredProcedure [dbo].[MensajeRecibido_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 03/09/11
-- =============================================
CREATE PROCEDURE [dbo].[MensajeRecibido_Select]
  @idMensaje NUMERIC = NULL
 ,@idPersonaRemitente NUMERIC = NULL
 ,@asuntoMensaje VARCHAR(100) = NULL
 ,@textoMensaje VARCHAR(5000) = NULL
 ,@fechaEnvio DATE = NULL
 ,@horaEnvio TIME = NULL
 ,@activo BIT = NULL
 ,@usuarioDestino VARCHAR(50) = NULL
 ,@leido BIT = NULL
AS
 SET NOCOUNT OFF;

Declare @idPersonaDestinatario numeric = null
select @idPersonaDestinatario = idPersona
from Personas where Personas.username = @usuarioDestino 

    SELECT Mensaje.idMensaje
    ,MensajeDestinatarios.idMensajeDestinatario
    ,MensajeDestinatarios.idPersonaDestinatario
    ,Mensaje.idPersonaRemitente
    ,Mensaje.asuntoMensaje
    ,Mensaje.textoMensaje
    ,Mensaje.fechaEnvio
    ,Mensaje.horaEnvio
    ,Mensaje.activo
    ,P.nombre as 'nombreDestinatario'
    ,P.apellido as 'apellidoDestinatario'
    ,PR.nombre as 'nombreRemitente'
    ,PR.apellido as 'apellidoRemitente'
    ,MensajeDestinatarios.leido
    ,case TPR.idTipoPersona
    when 1 THEN
		(select CargosPersonal.nombre 
		from CargosPersonal inner join Personal on CargosPersonal.idCargoPersonal = Personal.idCargoPersonal
			inner join Personas on Personal.idPersona = Personas.idPersona
		where Personas.idPersona = PR.idPersona
		)
	ELSE
		TPR.nombre
    end as 'tipoPersonaRemitente'
    ,case TPD.idTipoPersona
    when 1 THEN
		(select CargosPersonal.nombre 
		from CargosPersonal inner join Personal on CargosPersonal.idCargoPersonal = Personal.idCargoPersonal
			inner join Personas on Personal.idPersona = Personas.idPersona
		where Personas.idPersona = P.idPersona
		)
	ELSE
		TPD.nombre
    end as 'tipoPersonaDestinatario'
    FROM Mensaje INNER JOIN MensajeDestinatarios
		ON Mensaje.idMensaje = MensajeDestinatarios.idMensaje
		INNER JOIN Personas as P ON MensajeDestinatarios.idPersonaDestinatario = P.idPersona
		INNER JOIN Personas as PR ON Mensaje.idPersonaRemitente = PR.idPersona
		INNER JOIN TipoPersona AS TPR  on PR.idTipoPersona = TPR.idTipoPersona
		INNER JOIN TipoPersona AS TPD  on P.idTipoPersona = TPD.idTipoPersona
    WHERE (@idPersonaDestinatario IS NULL OR @idPersonaDestinatario= MensajeDestinatarios.idPersonaDestinatario)
	AND	(@idMensaje IS NULL OR @idMensaje=Mensaje.idMensaje)
	AND (@idPersonaRemitente IS NULL OR @idPersonaRemitente=Mensaje.idPersonaRemitente)
	AND (@textoMensaje IS NULL OR @textoMensaje=Mensaje.textoMensaje)
	AND (@fechaEnvio IS NULL OR @fechaEnvio=Mensaje.fechaEnvio)
	AND (@horaEnvio IS NULL OR @horaEnvio=Mensaje.horaEnvio)
	AND (@activo IS NULL OR @activo=Mensaje.activo)
	AND (@leido IS NULL OR @leido=MensajeDestinatarios.leido)
	AND (MensajeDestinatarios.activo = 1)
ORDER BY Mensaje.fechaEnvio DESC, Mensaje.horaEnvio DESC

GO
/****** Object:  StoredProcedure [dbo].[MensajeRemitente_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 07/09/11
-- =============================================
CREATE PROCEDURE [dbo].[MensajeRemitente_Update]
  @idMensaje NUMERIC = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;

	UPDATE [Mensaje]
	SET activo = @activo
	WHERE [Mensaje].idMensaje =  @idMensaje

GO
/****** Object:  StoredProcedure [dbo].[MensajesDestinatario_DesactivarLista]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 17/09/11
-- =============================================
CREATE PROCEDURE [dbo].[MensajesDestinatario_DesactivarLista]
  @listaMensajes VARCHAR(MAX) 
 ,@leido BIT = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;

	UPDATE [MensajeDestinatarios]
	SET  [leido] = @leido
		,[activo] = @activo
	WHERE [MensajeDestinatarios].idMensajeDestinatario IN (SELECT * FROM [ArrayToTable] (@listaMensajes))

GO
/****** Object:  StoredProcedure [dbo].[MensajesRemitente_DesactivarLista]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 17/09/11
-- =============================================
CREATE PROCEDURE [dbo].[MensajesRemitente_DesactivarLista]
  @listaMensajes VARCHAR(MAX) 
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;

	UPDATE [Mensaje]
	SET  [activo] = @activo
	WHERE [Mensaje].idMensaje IN (SELECT * FROM [ArrayToTable] (@listaMensajes))

GO
/****** Object:  StoredProcedure [dbo].[Modulo_Delete]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 22/07/11
-- =============================================
CREATE PROCEDURE [dbo].[Modulo_Delete]
  @idModulo NUMERIC = NULL
 ,@horaInicio DATETIME = NULL
 ,@horaFinalizacion DATETIME = NULL
AS
 SET NOCOUNT OFF;

    DELETE FROM Modulo
    WHERE (@idModulo IS NULL OR @idModulo=idModulo)

-- =====UPDATE==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[Modulo_Exists]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 22/07/11
-- =============================================
CREATE PROCEDURE [dbo].[Modulo_Exists]
  @idModulo NUMERIC = NULL
 ,@horaInicio DATETIME = NULL
 ,@horaFinalizacion DATETIME = NULL
 ,@exists BIT OUT
AS
 SET NOCOUNT OFF;

    IF EXISTS (
 SELECT idModulo
    FROM Modulo
    WHERE (@idModulo IS NULL OR @idModulo=idModulo)
 )
 SET @exists = 1
 ELSE SET @exists = 0

GO
/****** Object:  StoredProcedure [dbo].[Modulo_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 22/07/11
-- =============================================
CREATE PROCEDURE [dbo].[Modulo_Insert]
  @idModulo NUMERIC = NULL
 ,@horaInicio TIME = NULL
 ,@horaFinalizacion TIME = NULL
 ,@exists BIT = 0
 ,@idDiaHorario NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    
     
 --IF EXISTS (
--	SELECT idModulo
--	FROM Modulo
--	WHERE (horaInicio = @horaInicio AND horaFinalizacion = @horaFinalizacion)
-- )
-- SET @exists = 1
-- ELSE SET @exists = 0

-- IF ((@idModulo IS NULL OR @idModulo=0) AND @exists = 0)
  IF (@idModulo IS NULL OR @idModulo=0) 
  BEGIN
    INSERT INTO Modulo(
     horaInicio
    ,horaFinalizacion
    ,idDiaHorario
 )
 VALUES(
     @horaInicio
    ,@horaFinalizacion
    ,@idDiaHorario
 )
  SET @idModulo = @@IDENTITY
  END

ELSE
BEGIN
	EXECUTE Modulo_Update
     @idModulo
    ,@horaInicio
    ,@horaFinalizacion
    ,@idDiaHorario
END
   RETURN SCOPE_IDENTITY()	
 

-- =====DELETE==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[Modulo_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 22/07/11
-- =============================================
CREATE PROCEDURE [dbo].[Modulo_Select]
  @idModulo NUMERIC = NULL
 ,@horaInicio DATETIME = NULL
 ,@horaFinalizacion DATETIME = NULL
AS
 SET NOCOUNT OFF;

    SELECT idModulo
    ,horaInicio
    ,horaFinalizacion
    FROM Modulo
    WHERE (@idModulo IS NULL OR @idModulo=idModulo)

GO
/****** Object:  StoredProcedure [dbo].[Modulo_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 22/07/11
-- =============================================
CREATE PROCEDURE [dbo].[Modulo_Update]
  @idModulo NUMERIC = NULL
 ,@horaInicio TIME = NULL
 ,@horaFinalizacion TIME = NULL
 ,@idDiaHorario NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    UPDATE Modulo SET 
     horaInicio = @horaInicio
     ,horaFinalizacion = @horaFinalizacion
     ,idDiaHorario = @idDiaHorario
    WHERE (@idModulo IS NULL OR @idModulo=idModulo)

-- =====SELECT==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[MotivoCitacion_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 27/08/11
-- =============================================
CREATE PROCEDURE [dbo].[MotivoCitacion_Select]
  @idMotivoCitacion NUMERIC = NULL
 ,@descripcion VARCHAR(50) = NULL
AS
 SET NOCOUNT OFF;

    SELECT idMotivoCitacion
    ,descripcion
    FROM MotivoCitacion
    WHERE (@idMotivoCitacion IS NULL OR @idMotivoCitacion=idMotivoCitacion)
	AND (@descripcion IS NULL OR @descripcion=descripcion)

GO
/****** Object:  StoredProcedure [dbo].[MotivoSancion_Delete]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 28/06/11
-- =============================================
CREATE PROCEDURE [dbo].[MotivoSancion_Delete]
  @idMotivoSancion INT = NULL
 ,@idMotivoSancionTransaccional INT = NULL
 ,@descripcion VARCHAR(50) = NULL
AS
 SET NOCOUNT OFF;

    DELETE FROM MotivoSancion
    WHERE (@idMotivoSancion IS NULL OR @idMotivoSancion=idMotivoSancion)
 
-- =====UPDATE==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[MotivoSancion_Exists]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 28/06/11
-- =============================================
CREATE PROCEDURE [dbo].[MotivoSancion_Exists]
  @idMotivoSancion INT = NULL
 ,@idMotivoSancionTransaccional INT = NULL
 ,@descripcion VARCHAR(50) = NULL
 ,@exists BIT OUT
AS
 SET NOCOUNT OFF;

    IF EXISTS (
 SELECT idMotivoSancion
    FROM MotivoSancion
    WHERE (@idMotivoSancion IS NULL OR @idMotivoSancion=idMotivoSancion)
 )
 SET @exists = 1
 ELSE SET @exists = 0

GO
/****** Object:  StoredProcedure [dbo].[MotivoSancion_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 28/06/11
-- =============================================
CREATE PROCEDURE [dbo].[MotivoSancion_Insert]
  @idMotivoSancion INT = NULL
 ,@idMotivoSancionTransaccional INT = NULL
 ,@descripcion VARCHAR(50) = NULL
AS
 SET NOCOUNT OFF;

SELECT @idMotivoSancion = idMotivoSancion
FROM MotivoSancion
WHERE @idMotivoSancionTransaccional = idMotivoSancionTransaccional

IF(@idMotivoSancion is NULL OR @idMotivoSancion = 0)
	BEGIN
		INSERT INTO MotivoSancion(
		idMotivoSancionTransaccional
		,descripcion
		)
		VALUES(
		 @idMotivoSancionTransaccional
		,@descripcion
		)
		SET @idMotivoSancion = @@IDENTITY
	END
ELSE 
 BEGIN
	 EXECUTE [MotivoSancion_Update]
	   @idMotivoSancion
	  ,@idMotivoSancionTransaccional
	  ,@descripcion
  END

GO
/****** Object:  StoredProcedure [dbo].[MotivoSancion_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 28/06/11
-- =============================================
CREATE PROCEDURE [dbo].[MotivoSancion_Select]
  @idMotivoSancion INT = NULL
 ,@idMotivoSancionTransaccional INT = NULL
 ,@descripcion VARCHAR(50) = NULL
AS
 SET NOCOUNT OFF;

    SELECT idMotivoSancion
    ,idMotivoSancionTransaccional
    ,descripcion
    FROM MotivoSancion
    WHERE 
		(@idMotivoSancion IS NULL OR @idMotivoSancion=idMotivoSancion)
    AND	(@descripcion IS NULL OR descripcion like '%' + @descripcion + '%')

GO
/****** Object:  StoredProcedure [dbo].[MotivoSancion_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 28/06/11
-- =============================================
CREATE PROCEDURE [dbo].[MotivoSancion_Update]
  @idMotivoSancion INT = NULL
 ,@idMotivoSancionTransaccional INT = NULL
 ,@descripcion VARCHAR(50) = NULL
AS
 SET NOCOUNT OFF;

    UPDATE MotivoSancion SET 
     idMotivoSancionTransaccional = @idMotivoSancionTransaccional
     ,descripcion = @descripcion
    WHERE (@idMotivoSancion IS NULL OR @idMotivoSancion=idMotivoSancion)
  

-- =====SELECT==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[Nivel_Delete]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 20/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Nivel_Delete]
  @idNivel NUMERIC = NULL
 ,@idNivelTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
AS
 SET NOCOUNT OFF;

    DELETE FROM Nivel
    WHERE (@idNivel IS NULL OR @idNivel=idNivel)

-- =====UPDATE==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[Nivel_Exists]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 20/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Nivel_Exists]
  @idNivel NUMERIC = NULL
 ,@idNivelTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@exists BIT OUT
AS
 SET NOCOUNT OFF;

    IF EXISTS (
 SELECT idNivel
    FROM Nivel
    WHERE (@idNivel IS NULL OR @idNivel=idNivel)
   AND (@idNivelTransaccional IS NULL OR @idNivelTransaccional=idNivelTransaccional)
   AND (@nombre IS NULL OR @nombre=nombre)
 )
 SET @exists = 1
 ELSE SET @exists = 0

GO
/****** Object:  StoredProcedure [dbo].[Nivel_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 20/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Nivel_Insert]
  @idNivel NUMERIC = NULL
 ,@idNivelTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
AS
 SET NOCOUNT OFF;

SELECT @idNivel=idNivel 
  FROM [dbo].[Nivel]
  WHERE [idNivelTransaccional]= @idNivelTransaccional

if (@idNivel is null or @idNivel =0)
begin
    INSERT INTO Nivel(
     idNivelTransaccional
    ,nombre
 )
 VALUES(
     @idNivelTransaccional
    ,@nombre
 )
 SET @idNivel = @@IDENTITY
 end
 else
 begin
  execute Nivel_Update
  @idNivel 
 ,@idNivelTransaccional 
 ,@nombre 
 end

-- =====DELETE==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[Nivel_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 20/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Nivel_Select]
  @idNivel NUMERIC = NULL
 ,@idNivelTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
AS
 SET NOCOUNT OFF;

    SELECT idNivel
    ,idNivelTransaccional
    ,nombre
    FROM Nivel
    WHERE (@idNivel IS NULL OR @idNivel=idNivel)
   AND (@idNivelTransaccional IS NULL OR @idNivelTransaccional=idNivelTransaccional)
   AND (@nombre IS NULL OR @nombre=nombre)

-- =====EXISTS==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[Nivel_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 20/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Nivel_Update]
  @idNivel NUMERIC = NULL
 ,@idNivelTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
AS
 SET NOCOUNT OFF;

    UPDATE Nivel SET 
      idNivelTransaccional = @idNivelTransaccional
     ,nombre = @nombre
    WHERE (@idNivel IS NULL OR @idNivel=idNivel)

-- =====SELECT==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[NivelAprobacion_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 23/10/11
-- =============================================
CREATE PROCEDURE [dbo].[NivelAprobacion_Select]

AS

SET NOCOUNT OFF;

	SELECT Top 1 MIN(valor) as 'valor'
		,[idValorEscalaCalificacion]
		,[nombre]
		,[descripcion]
		,[activo]
		,[aprobado]
	FROM [ValoresEscalaCalificacion]
	WHERE descripcion <> '%conceptual%' and aprobado = 1 and activo = 1
	GROUP BY 
		 [idValorEscalaCalificacion]
		,[nombre]
		,[descripcion]
		,[activo]
		,[aprobado]

GO
/****** Object:  StoredProcedure [dbo].[NivelesCursoCicloLectivo_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 03/10/11
-- =============================================
CREATE PROCEDURE [dbo].[NivelesCursoCicloLectivo_Select]
  @idCursoCicloLectivo NUMERIC = NULL
 ,@idCurso NUMERIC = NULL
 ,@idNivel NUMERIC = NULL
 ,@idCicloLectivo NUMERIC = NULL
 ,@username VARCHAR(50) = NULL
AS
SELECT DISTINCT
	 Nivel.idNivel
	,Nivel.nombre
from Nivel 
inner join Curso as C on C.idNivel = Nivel.idNivel
inner join cursosCicloLectivo as ccl on ccl.idCurso = c.idCurso
WHERE 
		(@idCursoCicloLectivo IS NULL OR @idCursoCicloLectivo=CCL.idCursoCicloLectivo)
	AND (@idCurso IS NULL OR @idCurso=CCL.idCurso)
	AND (@idCicloLectivo IS NULL OR @idCicloLectivo=CCL.idCicloLectivo)
	--AND (@username IS NULL OR @username = P.username)
	AND (@idNivel IS NULL OR @idNivel=Nivel.idNivel)

GO
/****** Object:  StoredProcedure [dbo].[NovedadAulica_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 18/09/12
-- =============================================
CREATE PROCEDURE [dbo].[NovedadAulica_Insert]
  @idNovedadAulica NUMERIC = NULL output
 ,@idNovedadAulicaPrincipal NUMERIC = NULL
 ,@fecha DATE = NULL
 ,@hora TIME = NULL
 ,@idPersona NUMERIC = NULL
 ,@observaciones VARCHAR(1000) = NULL
 ,@idCursoCicloLectivo NUMERIC = NULL
 ,@idTipoNovedad NUMERIC = NULL
 ,@idEstadoNovedad NUMERIC = NULL
 ,@usuarioCreador VARCHAR(50) = NULL
AS
 SET NOCOUNT OFF;

select @idPersona = idPersona
from Personas where Personas.username = @usuarioCreador 

    INSERT INTO NovedadAulica(
     idNovedadAulicaPrincipal
    ,fecha
    ,hora
    ,idPersona
    ,observaciones
    ,idCursoCicloLectivo
    ,idTipoNovedad
    ,idEstadoNovedad
 )
 VALUES(
     @idNovedadAulicaPrincipal
    ,@fecha
    ,@hora
    ,@idPersona
    ,@observaciones
    ,@idCursoCicloLectivo
    ,@idTipoNovedad
    ,@idEstadoNovedad
 )
 
 IF (@idNovedadAulicaPrincipal >0)
	BEGIN
	
	UPDATE NovedadAulica SET idEstadoNovedad = @idEstadoNovedad
		WHERE idNovedadAulicaPrincipal = @idNovedadAulicaPrincipal 
			OR idNovedadAulica = @idNovedadAulicaPrincipal 
	
	END
	 
	SET @idNovedadAulica = @@IDENTITY
	RETURN SCOPE_IDENTITY()

GO
/****** Object:  StoredProcedure [dbo].[NovedadAulica_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 21/09/12
-- =============================================
CREATE PROCEDURE [dbo].[NovedadAulica_Select]
  @idNovedadAulica NUMERIC = NULL
 ,@idNovedadAulicaPrincipal NUMERIC = NULL
 ,@fecha DATE = NULL
 ,@hora TIME = NULL
 ,@idPersona NUMERIC = NULL
 ,@observaciones VARCHAR(1000) = NULL
 ,@idCursoCicloLectivo NUMERIC = NULL
 ,@idTipoNovedad NUMERIC = NULL
 ,@idEstadoNovedad NUMERIC = NULL
 ,@usuario VARCHAR(50) = NULL
AS
 SET NOCOUNT OFF;

    SELECT 
		NA.idNovedadAulica,
		NA.idNovedadAulicaPrincipal,
		NA.fecha,
		NA.hora,
		NA.idPersona,
		NA.observaciones,
		NA.idCursoCicloLectivo,
		NA.idTipoNovedad,
		NA.idEstadoNovedad,
		TPA.descripcion AS 'tipoNovedad',
		ENA.nombre AS 'estadoNovedad',
		ENA.esFinal AS 'esFinal',
		P.nombre AS 'nombrePersona',
		P.apellido AS 'apellidoPersona',
		CASE TPD.idTipoPersona
		WHEN 1 THEN
			(SELECT	CargosPersonal.nombre 
				FROM CargosPersonal inner join Personal ON CargosPersonal.idCargoPersonal = Personal.idCargoPersonal
				INNER JOIN Personas ON Personal.idPersona = Personas.idPersona
				WHERE Personas.idPersona = P.idPersona
			)
		ELSE
			TPD.nombre
		END	AS 'tipoPersona'
    FROM NovedadAulica AS NA
		INNER JOIN dbo.TipoNovedadAulica AS TPA ON NA.idTipoNovedad = TPA.idTipoNovedadAulica
		INNER JOIN dbo.EstadoNovedadAulica AS ENA ON NA.idEstadoNovedad = ENA.idEstadoNovedadAulica
		INNER JOIN dbo.Personas AS P ON NA.idPersona = P.idPersona
		INNER JOIN dbo.TipoPersona AS TPD  on P.idTipoPersona = TPD.idTipoPersona
    WHERE (@idNovedadAulica IS NULL OR @idNovedadAulica=NA.idNovedadAulica)
   AND (@idNovedadAulicaPrincipal IS NULL OR
   ( @idNovedadAulicaPrincipal=NA.idNovedadAulicaPrincipal
		OR ( NA.idNovedadAulica = @idNovedadAulicaPrincipal AND NA.idNovedadAulicaPrincipal IS NULL ) 
	)
   )
   AND (@fecha IS NULL OR @fecha=NA.fecha)
   AND (@hora IS NULL OR @hora=NA.hora)
   AND (@usuario IS NULL OR @usuario=P.username)
   AND (@observaciones IS NULL OR @observaciones=NA.observaciones)
   AND (@idCursoCicloLectivo IS NULL OR @idCursoCicloLectivo=NA.idCursoCicloLectivo)
   AND (@idTipoNovedad IS NULL OR @idTipoNovedad=NA.idTipoNovedad)
   AND (@idEstadoNovedad IS NULL OR @idEstadoNovedad=NA.idEstadoNovedad)
   
   ORDER BY fecha ASC,hora ASC
GO
/****** Object:  StoredProcedure [dbo].[NovedadAulicaIndicadores_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NovedadAulicaIndicadores_Select]
@idCursoCicloLectivo NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    SELECT TOP 5
		NA.idNovedadAulica,
		NA.idNovedadAulicaPrincipal,
		NA.fecha,
		NA.hora,
		NA.idPersona,
		NA.observaciones,
		NA.idCursoCicloLectivo,
		NA.idTipoNovedad,
		NA.idEstadoNovedad,
		TPA.descripcion AS 'tipoNovedad',
		ENA.nombre AS 'estadoNovedad',
		ENA.esFinal AS 'esFinal',
		P.nombre AS 'nombrePersona',
		P.apellido AS 'apellidoPersona',
		CASE TPD.idTipoPersona
		WHEN 1 THEN
			(SELECT	CargosPersonal.nombre 
				FROM CargosPersonal inner join Personal ON CargosPersonal.idCargoPersonal = Personal.idCargoPersonal
				INNER JOIN Personas ON Personal.idPersona = Personas.idPersona
				WHERE Personas.idPersona = P.idPersona
			)
		ELSE
			TPD.nombre
		END	AS 'tipoPersona'
    FROM NovedadAulica AS NA
		INNER JOIN dbo.TipoNovedadAulica AS TPA ON NA.idTipoNovedad = TPA.idTipoNovedadAulica
		INNER JOIN dbo.EstadoNovedadAulica AS ENA ON NA.idEstadoNovedad = ENA.idEstadoNovedadAulica
		INNER JOIN dbo.Personas AS P ON NA.idPersona = P.idPersona
		INNER JOIN dbo.TipoPersona AS TPD  on P.idTipoPersona = TPD.idTipoPersona
    WHERE 
		@idCursoCicloLectivo=NA.idCursoCicloLectivo
		AND 
		DATEDIFF(d, NA.fecha,GETDATE())<7
	ORDER BY fecha ASC,hora ASC

GO
/****** Object:  StoredProcedure [dbo].[NovedadAulicaPrincipales_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 21/09/12
-- =============================================
CREATE PROCEDURE [dbo].[NovedadAulicaPrincipales_Select]
  @idNovedadAulica NUMERIC = NULL
 ,@idNovedadAulicaPrincipal NUMERIC = NULL
 ,@fecha DATE = NULL
 ,@hora TIME = NULL
 ,@idPersona NUMERIC = NULL
 ,@observaciones VARCHAR(1000) = NULL
 ,@idCursoCicloLectivo NUMERIC = NULL
 ,@idTipoNovedad NUMERIC = NULL
 ,@idEstadoNovedad NUMERIC = NULL
 ,@usuario VARCHAR(50) = NULL
AS
 SET NOCOUNT OFF;

    SELECT 
		NA.idNovedadAulica,
		NA.idNovedadAulicaPrincipal,
		NA.fecha,
		NA.hora,
		NA.idPersona,
		NA.observaciones,
		NA.idCursoCicloLectivo,
		NA.idTipoNovedad,
		NA.idEstadoNovedad,
		TPA.descripcion AS 'tipoNovedad',
		ENA.nombre AS 'estadoNovedad',
		ENA.esFinal AS 'esFinal' ,
		P.nombre AS 'nombrePersona',
		P.apellido AS 'apellidoPersona',
		CASE TPD.idTipoPersona
		WHEN 1 THEN
			(SELECT	CargosPersonal.nombre 
				FROM CargosPersonal inner join Personal ON CargosPersonal.idCargoPersonal = Personal.idCargoPersonal
				INNER JOIN Personas ON Personal.idPersona = Personas.idPersona
				WHERE Personas.idPersona = P.idPersona
			)
		ELSE
			TPD.nombre
		END	AS 'tipoPersona'
    FROM NovedadAulica AS NA
		INNER JOIN dbo.TipoNovedadAulica AS TPA ON NA.idTipoNovedad = TPA.idTipoNovedadAulica
		INNER JOIN dbo.EstadoNovedadAulica AS ENA ON NA.idEstadoNovedad = ENA.idEstadoNovedadAulica
		INNER JOIN dbo.Personas AS P ON NA.idPersona = P.idPersona
		INNER JOIN dbo.TipoPersona AS TPD  on P.idTipoPersona = TPD.idTipoPersona
    WHERE (@idNovedadAulica IS NULL OR @idNovedadAulica=NA.idNovedadAulica)
   --AND (@idNovedadAulicaPrincipal IS NULL OR @idNovedadAulicaPrincipal=NA.idNovedadAulicaPrincipal)
   AND (@fecha IS NULL OR @fecha=NA.fecha)
   AND (@hora IS NULL OR @hora=NA.hora)
   AND (@usuario IS NULL OR @usuario=P.username)
   AND (@observaciones IS NULL OR @observaciones=NA.observaciones)
   AND (@idCursoCicloLectivo IS NULL OR @idCursoCicloLectivo=NA.idCursoCicloLectivo)
   AND (@idTipoNovedad IS NULL OR @idTipoNovedad=NA.idTipoNovedad)
   AND (@idEstadoNovedad IS NULL OR @idEstadoNovedad=NA.idEstadoNovedad)
   
   AND NA.idNovedadAulicaPrincipal IS NULL
   
   ORDER BY fecha ASC,hora ASC
GO
/****** Object:  StoredProcedure [dbo].[Orientacion_Delete]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- ALTER date: 22/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Orientacion_Delete]
  @idOrientacion NUMERIC = NULL
 ,@idOrientacionTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@descripcion VARCHAR(250) = NULL
 ,@competencias VARCHAR(200) = NULL
AS
 SET NOCOUNT OFF;

    DELETE FROM Orientacion
    WHERE (@idOrientacion IS NULL OR @idOrientacion=idOrientacion)

-- =====UPDATE==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[Orientacion_Exists]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- ALTER date: 22/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Orientacion_Exists]
  @idOrientacion NUMERIC = NULL
 ,@idOrientacionTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL 
 ,@descripcion VARCHAR(250) = NULL
 ,@competencias VARCHAR(200) = NULL
 ,@exists BIT OUT
AS
 SET NOCOUNT OFF;

    IF EXISTS (
 SELECT idOrientacion
    FROM Orientacion
    WHERE (@idOrientacion IS NULL OR @idOrientacion=idOrientacion)
   AND (@idOrientacionTransaccional IS NULL OR @idOrientacionTransaccional=idOrientacionTransaccional)
   AND (@nombre IS NULL OR @nombre=nombre)
   AND (@descripcion IS NULL OR @descripcion=descripcion)
   AND (@competencias IS NULL OR @competencias=competencias)
 )
 SET @exists = 1
 ELSE SET @exists = 0

GO
/****** Object:  StoredProcedure [dbo].[Orientacion_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- ALTER date: 22/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Orientacion_Insert]
  @idOrientacion NUMERIC = NULL
 ,@idOrientacionTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@descripcion VARCHAR(250) = NULL
 ,@competencias VARCHAR(200) = NULL
AS
 SET NOCOUNT OFF;

SELECT @idOrientacion=idOrientacion 
  FROM [dbo].[Orientacion]
  WHERE [idOrientacionTransaccional]= @idOrientacionTransaccional

if (@idOrientacion is null or @idOrientacion=0)
begin
    INSERT INTO Orientacion(
     idOrientacionTransaccional
    ,nombre
    ,descripcion
    ,competencias
 )
 VALUES(
     @idOrientacionTransaccional
    ,@nombre
    ,@descripcion
    ,@competencias
 )
  SET @idOrientacion = @@IDENTITY
 end
else

begin
execute Orientacion_Update
  @idOrientacion 
 ,@idOrientacionTransaccional
 ,@nombre
 ,@descripcion 
 ,@competencias
end
GO
/****** Object:  StoredProcedure [dbo].[Orientacion_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- ALTER date: 22/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Orientacion_Select]
  @idOrientacion NUMERIC = NULL
 ,@idOrientacionTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@descripcion VARCHAR(250) = NULL
 ,@competencias VARCHAR(200) = NULL
AS
 SET NOCOUNT OFF;

    SELECT idOrientacion
    ,idOrientacionTransaccional
    ,nombre
    ,descripcion
    ,competencias
    FROM Orientacion
    WHERE (@idOrientacion IS NULL OR @idOrientacion=idOrientacion)
   AND (@idOrientacionTransaccional IS NULL OR @idOrientacionTransaccional=idOrientacionTransaccional)
   AND (@nombre IS NULL OR @nombre=nombre)
   AND (@descripcion IS NULL OR @descripcion=descripcion)
   AND (@competencias IS NULL OR @competencias=competencias)

-- =====EXISTS==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[Orientacion_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- ALTER date: 22/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Orientacion_Update]
  @idOrientacion NUMERIC = NULL
 ,@idOrientacionTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@descripcion VARCHAR(250) = NULL
 ,@competencias VARCHAR(200) = NULL
AS
 SET NOCOUNT OFF;

    UPDATE Orientacion SET 
      idOrientacionTransaccional = @idOrientacionTransaccional
     ,nombre=@nombre
     ,descripcion = @descripcion
     ,competencias = @competencias
    WHERE (@idOrientacion IS NULL OR @idOrientacion=idOrientacion)
  

-- =====SELECT==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[Paginas_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 22/07/11
-- =============================================
CREATE PROCEDURE [dbo].[Paginas_Insert]
  @idPagina NUMERIC = NULL OUTPUT
 ,@titulo VARCHAR(50) = NULL
 ,@url VARCHAR(250) = NULL
AS
 SET NOCOUNT OFF;

    INSERT INTO Paginas(
     titulo
    ,url
 )
 VALUES(
     @titulo
    ,@url
 )
 SET @idPagina = @@IDENTITY
 RETURN SCOPE_IDENTITY()

GO
/****** Object:  StoredProcedure [dbo].[Paginas_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 22/07/11
-- =============================================
CREATE PROCEDURE [dbo].[Paginas_Select]
  @idPagina NUMERIC = NULL
 ,@titulo VARCHAR(50) = NULL
 ,@url VARCHAR(250) = NULL
AS
 SET NOCOUNT OFF;

    SELECT idPagina
    ,titulo
    ,url
    FROM Paginas
    WHERE (@idPagina IS NULL OR @idPagina=idPagina)
   AND (@titulo IS NULL OR @titulo=titulo)
   AND (@url IS NULL OR @url=url)
   ORDER BY titulo

GO
/****** Object:  StoredProcedure [dbo].[Paginas_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 22/07/11
-- =============================================
CREATE PROCEDURE [dbo].[Paginas_Update]
  @idPagina NUMERIC = NULL
 ,@titulo VARCHAR(50) = NULL
 ,@url VARCHAR(250) = NULL
AS
 SET NOCOUNT OFF;

    UPDATE Paginas SET 
      titulo = @titulo
     ,url = @url
    WHERE @idPagina=idPagina

GO
/****** Object:  StoredProcedure [dbo].[Paises_Delete]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 14/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Paises_Delete]
  @idPais NUMERIC = NULL
 ,@idPaisTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@descripcion VARCHAR(250) = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;

    DELETE FROM Paises
    WHERE (@idPais IS NULL OR @idPais=idPais)

-- =====UPDATE==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[Paises_Exists]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 14/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Paises_Exists]
  @idPais NUMERIC = NULL
 ,@idPaisTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@descripcion VARCHAR(250) = NULL
 ,@activo BIT = NULL
 ,@exists BIT OUT
AS
 SET NOCOUNT OFF;

    IF EXISTS (
 SELECT idPais
    FROM Paises
    WHERE (@idPais IS NULL OR @idPais=idPais)
   AND (@idPaisTransaccional IS NULL OR @idPaisTransaccional=idPaisTransaccional)
   AND (@nombre IS NULL OR @nombre=nombre)
   AND (@descripcion IS NULL OR @descripcion=descripcion)
   AND (@activo IS NULL OR @activo=activo)
 )
 SET @exists = 1
 ELSE SET @exists = 0

GO
/****** Object:  StoredProcedure [dbo].[Paises_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 14/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Paises_Insert]
  @idPais NUMERIC = NULL output
 ,@idPaisTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@descripcion VARCHAR(250) = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;
 
  SELECT @idPais=idPais 
  FROM [dbo].[Paises]
  WHERE [idPaisTransaccional]= @idPaisTransaccional
  
  IF (@idPais=0 or @idPais is null)
  BEGIN
    INSERT INTO Paises(
     idPaisTransaccional
    ,nombre
    ,descripcion
    ,activo
 )
 VALUES(
     @idPaisTransaccional
    ,@nombre
    ,@descripcion
    ,@activo
 )
 SET @idPais = @@IDENTITY
	END
	ELSE
	BEGIN
	EXECUTE [Paises_Update] 
	   @idPais
	  ,@idPaisTransaccional
	  ,@nombre
	  ,@descripcion
	  ,@activo
	END

GO
/****** Object:  StoredProcedure [dbo].[Paises_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Paises_Select]
  @idPais NUMERIC = NULL
 ,@idPaisTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@descripcion VARCHAR(250) = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;

    SELECT idPais
    ,idPaisTransaccional
    ,nombre
    ,descripcion
    ,activo
    FROM Paises
    WHERE (@idPais IS NULL OR @idPais=idPais)
   AND (@idPaisTransaccional IS NULL OR @idPaisTransaccional=idPaisTransaccional)
   AND (@nombre IS NULL OR @nombre=nombre)
   AND (@descripcion IS NULL OR @descripcion=descripcion)
   AND (@activo IS NULL OR @activo=activo)

-- =====EXISTS==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[Paises_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 14/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Paises_Update]
  @idPais NUMERIC = NULL
 ,@idPaisTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@descripcion VARCHAR(250) = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;

    UPDATE Paises SET 
      idPaisTransaccional = @idPaisTransaccional
     ,nombre = @nombre
     ,descripcion = @descripcion
     ,activo = @activo
    WHERE (@idPais IS NULL OR @idPais=idPais)

-- =====SELECT==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[Periodo_Delete]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 23/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Periodo_Delete]
  @idPeriodo NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    DELETE FROM Periodo
    WHERE (@idPeriodo IS NULL OR @idPeriodo=idPeriodo)

-- =====UPDATE==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[Periodo_Exists]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 23/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Periodo_Exists]
  @idPeriodo NUMERIC = NULL
 ,@idPeriodoTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@idCicloLectivo NUMERIC = NULL
 ,@fechaInicio DATE = NULL
 ,@fechaFin DATE = NULL
 ,@exists BIT OUT
AS
 SET NOCOUNT OFF;

    IF EXISTS (
 SELECT idPeriodo
    FROM Periodo
    WHERE (@idPeriodo IS NULL OR @idPeriodo=idPeriodo)
   AND (@idPeriodoTransaccional IS NULL OR @idPeriodoTransaccional=idPeriodoTransaccional)
   AND (@nombre IS NULL OR @nombre=nombre)
   AND (@idCicloLectivo IS NULL OR @idCicloLectivo=idCicloLectivo)
   AND (@fechaInicio IS NULL OR @fechaInicio=fechaInicio)
   AND (@fechaFin IS NULL OR @fechaFin=fechaFin)
 )
 SET @exists = 1
 ELSE SET @exists = 0

GO
/****** Object:  StoredProcedure [dbo].[Periodo_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 23/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Periodo_Insert]
  @idPeriodo NUMERIC = NULL
 ,@idPeriodoTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@idCicloLectivo NUMERIC = NULL
 ,@fechaInicio DATE = NULL
 ,@fechaFin DATE = NULL
AS
 SET NOCOUNT OFF;
	SELECT @idPeriodo=idPeriodo	
	FROM Periodo
	WHERE idPeriodoTransaccional=@idPeriodoTransaccional
	
	SELECT @idCicloLectivo = idCicloLectivo
	FROM CicloLectivo
	WHERE idCicloLectivoTransaccional= @idCicloLectivo
	
IF (@idPeriodo IS NULL OR @idPeriodo = 0 )
	BEGIN
		INSERT INTO Periodo(
		 idPeriodoTransaccional
		,nombre
		,idCicloLectivo
		,fechaInicio
		,fechaFin
	 )
	 VALUES(
		 @idPeriodoTransaccional
		,@nombre
		,@idCicloLectivo
		,@fechaInicio
		,@fechaFin
	 )
	 SET @idPeriodo = @@IDENTITY
	END
	ELSE
	BEGIN
		EXECUTE Periodo_Update
				  @idPeriodo
				 ,@idPeriodoTransaccional
				 ,@nombre
				 ,@idCicloLectivo
				 ,@fechaInicio
				 ,@fechaFin
	END
-- =====DELETE==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[Periodo_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 23/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Periodo_Select]
  @idPeriodo NUMERIC = NULL
 ,@idPeriodoTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@idCicloLectivo NUMERIC = NULL
 ,@fechaInicio DATE = NULL
 ,@fechaFin DATE = NULL
AS
 SET NOCOUNT OFF;

    SELECT idPeriodo
    ,idPeriodoTransaccional
    ,nombre
    ,idCicloLectivo
    ,fechaInicio
    ,fechaFin
    FROM Periodo
    WHERE (@idPeriodo IS NULL OR @idPeriodo=idPeriodo)
   AND (@idPeriodoTransaccional IS NULL OR @idPeriodoTransaccional=idPeriodoTransaccional)
   AND (@nombre IS NULL OR @nombre=nombre)
   AND (@idCicloLectivo IS NULL OR @idCicloLectivo=idCicloLectivo)
   AND (@fechaInicio IS NULL OR @fechaInicio=fechaInicio)
   AND (@fechaFin IS NULL OR @fechaFin=fechaFin)

-- =====EXISTS==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[Periodo_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 23/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Periodo_Update]
  @idPeriodo NUMERIC = NULL
 ,@idPeriodoTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@idCicloLectivo NUMERIC = NULL
 ,@fechaInicio DATE = NULL
 ,@fechaFin DATE = NULL
AS
 SET NOCOUNT OFF;

    UPDATE Periodo SET 
      idPeriodoTransaccional = @idPeriodoTransaccional
     ,nombre = @nombre
     ,idCicloLectivo = @idCicloLectivo
     ,fechaInicio = @fechaInicio
     ,fechaFin = @fechaFin
    WHERE (@idPeriodo IS NULL OR @idPeriodo=idPeriodo)

-- =====SELECT==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[Personal_Delete]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 14/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Personal_Delete]
  @idPersonal NUMERIC = NULL
 ,@idPersonalTransaccional NUMERIC = NULL
 ,@idPersona NUMERIC = NULL
 ,@legajo VARCHAR(50) = NULL
 ,@fechaAlta DATE = NULL
 ,@fechaBaja DATE = NULL
 ,@activo BIT = NULL
 ,@idCargoPersonal NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    DELETE FROM Personal
    WHERE (@idPersonal IS NULL OR @idPersonal=idPersonal)
   AND (@idPersonalTransaccional IS NULL OR @idPersonalTransaccional=idPersonalTransaccional)
   AND (@idPersona IS NULL OR @idPersona=idPersona)
   AND (@legajo IS NULL OR @legajo=legajo)
   AND (@fechaAlta IS NULL OR @fechaAlta=fechaAlta)
   AND (@fechaBaja IS NULL OR @fechaBaja=fechaBaja)
   AND (@activo IS NULL OR @activo=activo)
   AND (@idCargoPersonal IS NULL OR @idCargoPersonal=idCargoPersonal)

-- =====UPDATE==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[Personal_Exists]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 14/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Personal_Exists]
  @idPersonal NUMERIC = NULL
 ,@idPersonalTransaccional NUMERIC = NULL
 ,@idPersona NUMERIC = NULL
 ,@legajo VARCHAR(50) = NULL
 ,@fechaAlta DATE = NULL
 ,@fechaBaja DATE = NULL
 ,@activo BIT = NULL
 ,@idCargoPersonal NUMERIC = NULL
 ,@exists BIT OUT
AS
 SET NOCOUNT OFF;

    IF EXISTS (
 SELECT idPersonal
    FROM Personal
    WHERE (@idPersonal IS NULL OR @idPersonal=idPersonal)
   AND (@idPersonalTransaccional IS NULL OR @idPersonalTransaccional=idPersonalTransaccional)
   AND (@idPersona IS NULL OR @idPersona=idPersona)
   AND (@legajo IS NULL OR @legajo=legajo)
   AND (@fechaAlta IS NULL OR @fechaAlta=fechaAlta)
   AND (@fechaBaja IS NULL OR @fechaBaja=fechaBaja)
   AND (@activo IS NULL OR @activo=activo)
   AND (@idCargoPersonal IS NULL OR @idCargoPersonal=idCargoPersonal)
 )
 SET @exists = 1
 ELSE SET @exists = 0

GO
/****** Object:  StoredProcedure [dbo].[Personal_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 14/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Personal_Insert]
  @idPersonal NUMERIC = NULL output
 ,@idPersonalTransaccional NUMERIC = NULL
 ,@idPersona NUMERIC = NULL
 ,@legajo VARCHAR(50) = NULL
 ,@fechaAlta DATE = NULL
 ,@fechaBaja DATE = NULL
 ,@activo BIT = NULL
 ,@idCargoPersonal NUMERIC = NULL
AS
 SET NOCOUNT OFF;
 
	SELECT @activo=activo
	FROM [Personas]
	WHERE [idPersona]= @idPersona
	
	IF (@activo = 0)
	BEGIN 
		SELECT @fechaBaja=GETDATE()
	END
 
	SELECT @idCargoPersonal=idCargoPersonal 
	FROM [CargosPersonal]
	WHERE [idCargoPersonalTransaccional]= @idCargoPersonal
 
	SELECT @idPersonal=idPersonal
	FROM [Personal]
	WHERE [idPersona]= @idPersona AND [idCargoPersonal]=@idCargoPersonal
  
	--select @idPersonal=idPersonal
	--from Personal inner join CargosPersonal on Personal.idCargoPersonal=CargosPersonal.idCargoPersonal
	--where idPersonalTransaccional=@idPersonalTransaccional
	--	and CargosPersonal.idCargoPersonalTransaccional=@idCargoPersonal
			

IF (@idPersonal IS NULL OR @idPersonal = 0)
	BEGIN
		INSERT INTO Personal(
			 idPersonalTransaccional
			,idPersona
			,legajo
			,fechaAlta
			,fechaBaja
			,activo
			,idCargoPersonal
			)
		 VALUES(
			 @idPersonalTransaccional
			,@idPersona
			,@legajo
			,GETDATE()
			,@fechaBaja
			,@activo
			,@idCargoPersonal
		 )
		SET @idPersonal = @@IDENTITY
	   END
ELSE
	BEGIN
		EXECUTE [Personal_Update]
			  @idPersonal
			 ,@idPersonalTransaccional
			 ,@idPersona
			 ,@legajo
			 ,@fechaAlta 
			 ,@fechaBaja 
			 ,@activo 
			 ,@idCargoPersonal
	END

GO
/****** Object:  StoredProcedure [dbo].[Personal_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 14/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Personal_Select]
  @idPersonal NUMERIC = NULL
 ,@idPersonalTransaccional NUMERIC = NULL
 ,@idPersona NUMERIC = NULL
 ,@legajo VARCHAR(50) = NULL
 ,@fechaAlta DATE = NULL
 ,@fechaBaja DATE = NULL
 ,@activo BIT = NULL
 ,@idCargoPersonal NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    SELECT idPersonal
    ,idPersonalTransaccional
    ,idPersona
    ,legajo
    ,fechaAlta
    ,fechaBaja
    ,activo
    ,idCargoPersonal
    FROM Personal
    WHERE (@idPersonal IS NULL OR @idPersonal=idPersonal)
   AND (@idPersonalTransaccional IS NULL OR @idPersonalTransaccional=idPersonalTransaccional)
   AND (@idPersona IS NULL OR @idPersona=idPersona)
   AND (@legajo IS NULL OR @legajo=legajo)
   AND (@fechaAlta IS NULL OR @fechaAlta=fechaAlta)
   AND (@fechaBaja IS NULL OR @fechaBaja=fechaBaja)
   AND (@activo IS NULL OR @activo=activo)
   AND (@idCargoPersonal IS NULL OR @idCargoPersonal=idCargoPersonal)

-- =====EXISTS==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[Personal_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 14/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Personal_Update]
  @idPersonal NUMERIC = NULL
 ,@idPersonalTransaccional NUMERIC = NULL
 ,@idPersona NUMERIC = NULL
 ,@legajo VARCHAR(50) = NULL
 ,@fechaAlta DATE = NULL
 ,@fechaBaja DATE = NULL
 ,@activo BIT = NULL
 ,@idCargoPersonal NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    UPDATE Personal SET 
      idPersonalTransaccional = @idPersonalTransaccional
     ,idPersona = @idPersona
     ,legajo = @legajo
     ,fechaAlta = @fechaAlta
     ,fechaBaja = @fechaBaja
     ,activo = @activo
     ,idCargoPersonal = @idCargoPersonal
    WHERE (@idPersonal IS NULL OR @idPersonal=idPersonal)
   

-- =====SELECT==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[Personas_Delete]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 14/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Personas_Delete]
  @idPersona NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    DELETE FROM Personas
    WHERE (@idPersona IS NULL OR @idPersona=idPersona)

GO
/****** Object:  StoredProcedure [dbo].[Personas_Exists]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- ALTER date: 12/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Personas_Exists]
  @idPersona NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@apellido VARCHAR(50) = NULL
 ,@numeroDocumento NUMERIC = NULL
 ,@idTipoDocumento NUMERIC = NULL
 ,@domicilio VARCHAR(250) = NULL
 ,@barrio VARCHAR(50) = NULL
 ,@idLocalidad NUMERIC = NULL
 ,@sexo CHAR = NULL
 ,@fechaNacimiento DATE = NULL
 ,@telefonoFijo VARCHAR(20) = NULL
 ,@telefonoCelular VARCHAR(20) = NULL
 ,@telefonoCelularAlternativo VARCHAR(20) = NULL
 ,@email VARCHAR(50) = NULL
 ,@activo BIT = NULL
 ,@username VARCHAR(50) = NULL
 ,@exists BIT OUT
 ,@idTipoPersona NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    IF EXISTS (
 SELECT idPersona
    FROM Personas
    WHERE (@idPersona IS NULL OR @idPersona=idPersona)
   AND (@nombre IS NULL OR @nombre=nombre)
   AND (@apellido IS NULL OR @apellido=apellido)
   AND (@numeroDocumento IS NULL OR @numeroDocumento=numeroDocumento)
   AND (@idTipoDocumento IS NULL OR @idTipoDocumento=idTipoDocumento)
   AND (@domicilio IS NULL OR @domicilio=domicilio)
   AND (@barrio IS NULL OR @barrio=barrio)
   AND (@idLocalidad IS NULL OR @idLocalidad=idLocalidad)
   AND (@sexo IS NULL OR @sexo=sexo)
   AND (@fechaNacimiento IS NULL OR @fechaNacimiento=fechaNacimiento)
   AND (@telefonoFijo IS NULL OR @telefonoFijo=telefonoFijo)
   AND (@telefonoCelular IS NULL OR @telefonoCelular=telefonoCelular)
   AND (@telefonoCelularAlternativo IS NULL OR @telefonoCelularAlternativo=telefonoCelularAlternativo)
   AND (@email IS NULL OR @email=email)
   AND (@activo IS NULL OR @activo=activo)
   AND (@username IS NULL OR @username = username)
   AND (@idTipoPersona IS NULL OR @idTipoPersona = idTipoPersona)
 )
 SET @exists = 1
 ELSE SET @exists = 0

GO
/****** Object:  StoredProcedure [dbo].[Personas_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Personas_Insert]
  	
  	@idPersona numeric (18, 0) = 0 output ,
  	@idPersonaTransaccional numeric (18, 0),
  	@nombre varchar (50),
  	@apellido varchar (50),
  	@numeroDocumento numeric (18, 0),
  	@idTipoDocumento numeric (18, 0),
  	@domicilio varchar (250) =null,
  	@barrio varchar(50) = null,
  	@idLocalidad numeric (18, 0)=null,
  	@localidad varchar(50)=null,
  	@sexo char (1) =null,
  	@fechaNacimiento date =null,
  	@telefonoFijo varchar (20)=  null,
  	@telefonoCelular varchar (20)=  null,
  	@telefonoCelularAlternativo varchar (20)=  null,
  	@email varchar (50)=  null,
  	@activo bit,
  	@username varchar(50) = null,
  	@idTipoPersona numeric (18,0) = null,
  	@idCargoPersonal numeric (18,0) = null
  	
  AS
  SET NOCOUNT ON 
  
	IF (@idTipoPersona = 1)
	--Personal
	BEGIN
		SELECT @idCargoPersonal=idCargoPersonal 
		FROM [CargosPersonal]
		WHERE [idCargoPersonalTransaccional]= @idCargoPersonal

		SELECT @idPersona = P.idPersona 
		FROM [Personal] as P inner join [Personas] as Ps on P.idPersona = Ps.idPersona
		WHERE P.idPersonalTransaccional = @idPersonaTransaccional
			AND @idCargoPersonal = p.idCargoPersonal
	END
  ELSE
	BEGIN
		IF (@idTipoPersona = 2)
		--Alumno = 2,
			BEGIN
				SELECT @idPersona = P.idPersona 
				FROM [Personas] as P inner join [Alumnos] as A on P.idPersona = A.idPersona
				WHERE A.idAlumnoTransaccional = @idPersonaTransaccional
			END
		ELSE
			BEGIN
			IF (@idTipoPersona = 3)
			--Tutor = 3
				BEGIN
					SELECT @idPersona = P.idPersona 
					FROM [Personas] as P inner join [Tutor] as T on P.idPersona = T.idPersona
					WHERE T.idTutorTransaccional = @idPersonaTransaccional
				END
			END
	END
  
  SELECT @idTipoDocumento=idTipoDocumento 
  FROM [dbo].[TipoDocumento]
  WHERE [idTipoDocumentoTransaccional]= @idTipoDocumento
  
  SELECT Top 1 @idLocalidad=idLocalidad 
  FROM [dbo].[Localidades]
  WHERE [nombre]= @localidad 
  
  IF (@idPersona is null OR @idPersona=0)
  BEGIN
  INSERT INTO  [dbo].[Personas]
  (
		[nombre],
		[apellido],
		[numeroDocumento],
		[idTipoDocumento],
		[domicilio],
		[barrio],
		[idLocalidad],
		[sexo],
		[fechaNacimiento],
		[telefonoFijo],
		[telefonoCelular],
		[telefonoCelularAlternativo],
		[email],
		[activo],
		[username],
		[idTipoPersona]
  )
  VALUES
  (
		@nombre,
		@apellido,
		@numeroDocumento,
		@idTipoDocumento,
		@domicilio,
		@barrio,
		@idLocalidad,
		@sexo,
		@fechaNacimiento,
		@telefonoFijo,
		@telefonoCelular,
		@telefonoCelularAlternativo,
		@email,
		@activo,
		@username,
		@idTipoPersona
   )
    SET @idPersona = @@IDENTITY
END
ELSE
BEGIN
	EXECUTE [Personas_Update] 
	   @idPersona
	  ,@nombre
	  ,@apellido
	  ,@numeroDocumento
	  ,@idTipoDocumento
	  ,@domicilio
	  ,@barrio
	  ,@idLocalidad
	  ,@sexo
	  ,@fechaNacimiento
	  ,@telefonoFijo
	  ,@telefonoCelular
	  ,@telefonoCelularAlternativo
	  ,@email
	  ,@activo
	  ,@username
	  ,@idTipoPersona
END
   --RETURN SCOPE_IDENTITY()

GO
/****** Object:  StoredProcedure [dbo].[Personas_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- ALTER date: 12/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Personas_Select]
  @idPersona NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@apellido VARCHAR(50) = NULL
 ,@numeroDocumento NUMERIC = NULL
 ,@idTipoDocumento NUMERIC = NULL
 ,@domicilio VARCHAR(250) = NULL
 ,@barrio VARCHAR(50) = NULL
 ,@idLocalidad NUMERIC = NULL
 ,@sexo CHAR = NULL
 ,@fechaNacimiento DATE = NULL
 ,@telefonoFijo VARCHAR(20) = NULL
 ,@telefonoCelular VARCHAR(20) = NULL
 ,@telefonoCelularAlternativo VARCHAR(20) = NULL
 ,@email VARCHAR(50) = NULL
 ,@activo BIT = NULL
 ,@username VARCHAR(50) = NULL
 ,@idTipoPersona NUMERIC = 0
 ,@bNoRegistrado BIT = NULL
AS
 SET NOCOUNT OFF;

IF (@idTipoPersona = 0 )
BEGIN
    SELECT P.idPersona
    ,P.nombre
    ,P.apellido
    ,P.numeroDocumento
    ,P.idTipoDocumento
    ,P.domicilio
    ,P.barrio
    ,P.idLocalidad
    ,P.sexo
    ,P.fechaNacimiento
    ,P.telefonoFijo
    ,P.telefonoCelular
    ,P.telefonoCelularAlternativo
    ,P.email
    ,P.activo
    ,P.username
    ,P.idTipoPersona
    --,TipoPersona.nombre as 'tipoPersona'
    ,
    case TipoPersona.idTipoPersona
    when 1 THEN
		(select CargosPersonal.nombre 
		from CargosPersonal inner join Personal on CargosPersonal.idCargoPersonal = Personal.idCargoPersonal
			inner join Personas on Personal.idPersona = Personas.idPersona
		where Personas.idPersona = P.idPersona
		)
	ELSE
		TipoPersona.nombre
    end as 'tipoPersona'
    FROM Personas as P inner join TipoPersona on P.idTipoPersona = TipoPersona.idTipoPersona
    WHERE 
    (@idPersona IS NULL OR @idPersona=P.idPersona)
   AND (@nombre IS NULL OR P.nombre LIKE '%' + @nombre + '%')
   AND (@apellido IS NULL OR P.apellido LIKE '%' + @apellido + '%')
   AND (@numeroDocumento IS NULL OR @numeroDocumento=numeroDocumento)
   AND (@idTipoDocumento IS NULL OR @idTipoDocumento=P.idTipoDocumento)
   AND (@domicilio IS NULL OR P.domicilio LIKE '%' + @domicilio + '%')
   AND (@barrio IS NULL OR  P.barrio LIKE '%' + @barrio + '%')
   AND (@idLocalidad IS NULL OR @idLocalidad=P.idLocalidad)
   AND (@sexo IS NULL OR @sexo=P.sexo)
   AND (@fechaNacimiento IS NULL OR @fechaNacimiento = CONVERT(DATE,P.fechaNacimiento,103))
   AND (@telefonoFijo IS NULL OR @telefonoFijo=P.telefonoFijo)
   AND (@telefonoCelular IS NULL OR @telefonoCelular=P.telefonoCelular)
   AND (@telefonoCelularAlternativo IS NULL OR @telefonoCelularAlternativo=P.telefonoCelularAlternativo)
   AND (@email IS NULL OR @email=P.email)
   AND (@activo IS NULL OR @activo=P.activo)
   AND (@username IS NULL OR @username = P.username )
   AND (@bNoRegistrado IS NULL OR P.username = '' OR P.username IS NULL)
   --AND (@idTipoPersona IS NULL OR @idTipoPersona = idTipoPersona)
END
ELSE
BEGIN
	-- PERSONAL
	IF (@idTipoPersona = 1)
	BEGIN
		EXECUTE [PersonasPersonal_Select]
			  @idPersona
			 ,@nombre 
			 ,@apellido
			 ,@numeroDocumento
			 ,@idTipoDocumento 
			 ,@domicilio 
			 ,@barrio
			 ,@idLocalidad
			 ,@sexo 
			 ,@fechaNacimiento 
			 ,@telefonoFijo 
			 ,@telefonoCelular
			 ,@telefonoCelularAlternativo
			 ,@email 
			 ,@activo
			 ,@username 
	END
	ELSE
		BEGIN
		-- ALUMNOS
		IF (@idTipoPersona = 2)
		BEGIN
			EXECUTE [PersonasAlumnos_Select]
				  @idPersona
				 ,@nombre 
				 ,@apellido
				 ,@numeroDocumento
				 ,@idTipoDocumento 
				 ,@domicilio 
				 ,@barrio
				 ,@idLocalidad
				 ,@sexo 
				 ,@fechaNacimiento 
				 ,@telefonoFijo 
				 ,@telefonoCelular
				 ,@telefonoCelularAlternativo
				 ,@email 
				 ,@activo
				 ,@username 
		END
		ELSE
		BEGIN
		-- TUTORES
			IF (@idTipoPersona = 3)
			BEGIN
				EXECUTE [PersonasTutores_Select]
					  @idPersona
					 ,@nombre 
					 ,@apellido
					 ,@numeroDocumento
					 ,@idTipoDocumento 
					 ,@domicilio 
					 ,@barrio
					 ,@idLocalidad
					 ,@sexo 
					 ,@fechaNacimiento 
					 ,@telefonoFijo 
					 ,@telefonoCelular
					 ,@telefonoCelularAlternativo
					 ,@email 
					 ,@activo
					 ,@username	
			END
		END
	END
END

GO
/****** Object:  StoredProcedure [dbo].[Personas_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- ALTER date: 12/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Personas_Update]
  @idPersona NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@apellido VARCHAR(50) = NULL
 ,@numeroDocumento NUMERIC = NULL
 ,@idTipoDocumento NUMERIC = NULL
 ,@domicilio VARCHAR(250) = NULL
 ,@barrio VARCHAR(50) = NULL
 ,@idLocalidad NUMERIC = NULL
 ,@sexo CHAR = NULL
 ,@fechaNacimiento DATE = NULL
 ,@telefonoFijo VARCHAR(20) = NULL
 ,@telefonoCelular VARCHAR(20) = NULL
 ,@telefonoCelularAlternativo VARCHAR(20) = NULL
 ,@email VARCHAR(50) = NULL
 ,@activo BIT = NULL
 ,@username VARCHAR(50) = NULL
 ,@idTipoPersona NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    UPDATE Personas SET 
     -- nombre = @nombre
     --,apellido = @apellido
     --,
     numeroDocumento = @numeroDocumento
     ,idTipoDocumento = @idTipoDocumento
     ,domicilio = @domicilio
     ,barrio = @barrio
     ,idLocalidad = @idLocalidad
     ,sexo = @sexo
     ,fechaNacimiento = @fechaNacimiento
     ,telefonoFijo = @telefonoFijo
     ,telefonoCelular = @telefonoCelular
     ,telefonoCelularAlternativo = @telefonoCelularAlternativo
     ,email = @email
     ,activo = @activo
     --,username = @username
     ,idTipoPersona = @idTipoPersona
    WHERE idPersona=@idPersona

GO
/****** Object:  StoredProcedure [dbo].[Personas_Username_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- CREATE date: 12/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Personas_Username_Update]
  @idPersona NUMERIC = NULL
 ,@username VARCHAR(50) = NULL
AS
 SET NOCOUNT OFF;

    UPDATE Personas SET 
     username = @username
    WHERE idPersona=@idPersona

GO
/****** Object:  StoredProcedure [dbo].[PersonasAlumnos_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: LAU
-- CREATE date: 28/06/11
-- =============================================
CREATE PROCEDURE [dbo].[PersonasAlumnos_Select]
  @idPersona NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@apellido VARCHAR(50) = NULL
 ,@numeroDocumento NUMERIC = NULL
 ,@idTipoDocumento NUMERIC = NULL
 ,@domicilio VARCHAR(250) = NULL
 ,@barrio VARCHAR(50) = NULL
 ,@idLocalidad NUMERIC = NULL
 ,@sexo CHAR = NULL
 ,@fechaNacimiento DATE = NULL
 ,@telefonoFijo VARCHAR(20) = NULL
 ,@telefonoCelular VARCHAR(20) = NULL
 ,@telefonoCelularAlternativo VARCHAR(20) = NULL
 ,@email VARCHAR(50) = NULL
 ,@activo BIT = NULL
 ,@username VARCHAR(50) = NULL
 ,@idTipoPersona int = 0
AS
 SET NOCOUNT OFF;

    SELECT P.idPersona
    ,P.nombre
    ,P.apellido
    ,P.numeroDocumento
    ,P.idTipoDocumento
    ,P.domicilio
    ,P.barrio
    ,P.idLocalidad
    ,P.sexo
    ,P.fechaNacimiento
    ,P.telefonoFijo
    ,P.telefonoCelular
    ,P.telefonoCelularAlternativo
    ,P.email
    ,P.activo
    ,P.username
    ,A.legajo
    ,TipoPersona.nombre as 'tipoPersona'
    FROM Personas AS P 
		INNER JOIN Alumnos AS A ON P.idPersona = A.idPersona
		INNER JOIN TipoPersona on P.idTipoPersona = TipoPersona.idTipoPersona
    WHERE (@idPersona IS NULL OR @idPersona=P.idPersona)
   AND (@nombre IS NULL OR P.nombre LIKE '%' + @nombre + '%')
   AND (@apellido IS NULL OR P.apellido LIKE '%' + @apellido + '%')
   AND (@numeroDocumento IS NULL OR @numeroDocumento=P.numeroDocumento)
   AND (@idTipoDocumento IS NULL OR @idTipoDocumento=P.idTipoDocumento)
   AND (@domicilio IS NULL OR P.domicilio LIKE '%' + @domicilio + '%')
   AND (@barrio IS NULL OR P.barrio LIKE '%' + @barrio + '%')
   AND (@idLocalidad IS NULL OR @idLocalidad=P.idLocalidad)
   AND (@sexo IS NULL OR @sexo=P.sexo)
   AND (@fechaNacimiento IS NULL OR @fechaNacimiento=P.fechaNacimiento)
   AND (@telefonoFijo IS NULL OR @telefonoFijo=P.telefonoFijo)
   AND (@telefonoCelular IS NULL OR @telefonoCelular=P.telefonoCelular)
   AND (@telefonoCelularAlternativo IS NULL OR @telefonoCelularAlternativo=P.telefonoCelularAlternativo)
   AND (@email IS NULL OR @email=P.email)
   AND (@activo IS NULL OR @activo=P.activo)
   AND (P.username IS NULL OR @username = P.username )

GO
/****** Object:  StoredProcedure [dbo].[PersonasPersonal_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: LAU
-- CREATE date: 28/06/11
-- =============================================
CREATE PROCEDURE [dbo].[PersonasPersonal_Select]
  @idPersona NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@apellido VARCHAR(50) = NULL
 ,@numeroDocumento NUMERIC = NULL
 ,@idTipoDocumento NUMERIC = NULL
 ,@domicilio VARCHAR(250) = NULL
 ,@barrio VARCHAR(50) = NULL
 ,@idLocalidad NUMERIC = NULL
 ,@sexo CHAR = NULL
 ,@fechaNacimiento DATE = NULL
 ,@telefonoFijo VARCHAR(20) = NULL
 ,@telefonoCelular VARCHAR(20) = NULL
 ,@telefonoCelularAlternativo VARCHAR(20) = NULL
 ,@email VARCHAR(50) = NULL
 ,@activo BIT = NULL
 ,@username VARCHAR(50) = NULL
 ,@idTipoPersona int = 0
AS
 SET NOCOUNT OFF;

    SELECT P.idPersona
    ,P.nombre
    ,P.apellido
    ,P.numeroDocumento
    ,P.idTipoDocumento
    ,P.domicilio
    ,P.barrio
    ,P.idLocalidad
    ,P.sexo
    ,P.fechaNacimiento
    ,P.telefonoFijo
    ,P.telefonoCelular
    ,P.telefonoCelularAlternativo
    ,P.email
    ,P.activo
    ,P.username
    ,CP.descripcion AS 'Cargo'
    ,case TipoPersona.idTipoPersona
    when 1 THEN
		(select CargosPersonal.nombre 
		from CargosPersonal inner join Personal on CargosPersonal.idCargoPersonal = Personal.idCargoPersonal
			inner join Personas on Personal.idPersona = Personas.idPersona
		where Personas.idPersona = P.idPersona
		)
	ELSE
		TipoPersona.nombre
    end as 'tipoPersona'
    FROM Personas AS P 
		INNER JOIN Personal AS PR ON P.idPersona = PR.idPersona
		INNER JOIN CargosPersonal AS CP ON PR.idCargoPersonal = CP.idCargoPersonal
		INNER JOIN TipoPersona on P.idTipoPersona = TipoPersona.idTipoPersona
    WHERE (@idPersona IS NULL OR @idPersona=P.idPersona)
   AND (@nombre IS NULL OR P.nombre LIKE '%' + @nombre + '%')
   AND (@apellido IS NULL OR P.apellido LIKE '%' + @apellido + '%')
   AND (@numeroDocumento IS NULL OR @numeroDocumento=P.numeroDocumento)
   AND (@idTipoDocumento IS NULL OR @idTipoDocumento=P.idTipoDocumento)
   AND (@domicilio IS NULL OR P.domicilio LIKE '%' + @domicilio + '%')
   AND (@barrio IS NULL OR P.barrio LIKE '%' + @barrio + '%')
   AND (@idLocalidad IS NULL OR @idLocalidad=P.idLocalidad)
   AND (@sexo IS NULL OR @sexo=P.sexo)
   AND (@fechaNacimiento IS NULL OR @fechaNacimiento=P.fechaNacimiento)
   AND (@telefonoFijo IS NULL OR @telefonoFijo=P.telefonoFijo)
   AND (@telefonoCelular IS NULL OR @telefonoCelular=P.telefonoCelular)
   AND (@telefonoCelularAlternativo IS NULL OR @telefonoCelularAlternativo=P.telefonoCelularAlternativo)
   AND (@email IS NULL OR @email=P.email)
   AND (@activo IS NULL OR @activo=P.activo)
   AND (P.username IS NULL OR @username = P.username )

GO
/****** Object:  StoredProcedure [dbo].[PersonasTutores_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: LAU
-- CREATE date: 28/06/11
-- =============================================
CREATE PROCEDURE [dbo].[PersonasTutores_Select]
  @idPersona NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@apellido VARCHAR(50) = NULL
 ,@numeroDocumento NUMERIC = NULL
 ,@idTipoDocumento NUMERIC = NULL
 ,@domicilio VARCHAR(250) = NULL
 ,@barrio VARCHAR(50) = NULL
 ,@idLocalidad NUMERIC = NULL
 ,@sexo CHAR = NULL
 ,@fechaNacimiento DATE = NULL
 ,@telefonoFijo VARCHAR(20) = NULL
 ,@telefonoCelular VARCHAR(20) = NULL
 ,@telefonoCelularAlternativo VARCHAR(20) = NULL
 ,@email VARCHAR(50) = NULL
 ,@activo BIT = NULL
 ,@username VARCHAR(50) = NULL
AS
 SET NOCOUNT OFF;

    SELECT P.idPersona
    ,P.nombre
    ,P.apellido
    ,P.numeroDocumento
    ,P.idTipoDocumento
    ,P.domicilio
    ,P.barrio
    ,P.idLocalidad
    ,P.sexo
    ,P.fechaNacimiento
    ,P.telefonoFijo
    ,P.telefonoCelular
    ,P.telefonoCelularAlternativo
    ,P.email
    ,P.activo
    ,P.username
    ,TP.descripcion AS 'TipoTutor'
    ,TipoPersona.nombre as 'tipoPersona'
    FROM Personas AS P 
		INNER JOIN Tutor AS T ON P.idPersona = T.idPersona
		INNER JOIN TipoTutor AS TP ON T.idTipoTutor = TP.idTipoTutor
		INNER JOIN TipoPersona on P.idTipoPersona = TipoPersona.idTipoPersona
    WHERE (@idPersona IS NULL OR @idPersona=P.idPersona)
   AND (@nombre IS NULL OR P.nombre LIKE '%' + @nombre + '%')
   AND (@apellido IS NULL OR P.apellido LIKE '%' + @apellido + '%')
   AND (@numeroDocumento IS NULL OR @numeroDocumento=P.numeroDocumento)
   AND (@idTipoDocumento IS NULL OR @idTipoDocumento=P.idTipoDocumento)
   AND (@domicilio IS NULL OR P.domicilio LIKE '%' + @domicilio + '%')
   AND (@barrio IS NULL OR P.barrio LIKE '%' + @barrio + '%')
   AND (@idLocalidad IS NULL OR @idLocalidad=P.idLocalidad)
   AND (@sexo IS NULL OR @sexo=P.sexo)
   AND (@fechaNacimiento IS NULL OR @fechaNacimiento=P.fechaNacimiento)
   AND (@telefonoFijo IS NULL OR @telefonoFijo=P.telefonoFijo)
   AND (@telefonoCelular IS NULL OR @telefonoCelular=P.telefonoCelular)
   AND (@telefonoCelularAlternativo IS NULL OR @telefonoCelularAlternativo=P.telefonoCelularAlternativo)
   AND (@email IS NULL OR @email=P.email)
   AND (@activo IS NULL OR @activo=P.activo)
   AND (P.username IS NULL OR @username = P.username )

GO
/****** Object:  StoredProcedure [dbo].[PlanificacionAnual_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 04/06/12
-- =============================================
CREATE PROCEDURE [dbo].[PlanificacionAnual_Insert]
  @idPlanificacionAnual NUMERIC = NULL OUTPUT
 ,@idCurricula NUMERIC = NULL
 ,@idCreador NUMERIC = NULL
 ,@idCicloLectivo NUMERIC = NULL
 ,@fechaCreacion DATE = NULL
 ,@fechaAprobada DATE = NULL
 ,@solicitarAprobacion BIT = NULL
 ,@observaciones VARCHAR(50) = NULL
 ,@usuarioCreador VARCHAR(50) = NULL
 
AS
 SET NOCOUNT OFF;

select @idCreador = idPersona
from Personas where Personas.username = @usuarioCreador 

    INSERT INTO PlanificacionAnual(
     idCurricula
    ,idCreador
    ,idCicloLectivo
	,fechaCreacion
    ,fechaAprobada
    ,solicitarAprobacion
    ,observaciones
 )
 VALUES(
     @idCurricula
    ,@idCreador
	,@idCicloLectivo
	,getdate()
    ,@fechaAprobada
    ,@solicitarAprobacion
    ,@observaciones
 )
 
 SET @idPlanificacionAnual = @@IDENTITY
 RETURN SCOPE_IDENTITY()
GO
/****** Object:  StoredProcedure [dbo].[PlanificacionAnual_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:  LAURA
-- Create date: 25/03/13
-- =============================================
CREATE PROCEDURE [dbo].[PlanificacionAnual_Select]
  @idPlanificacionAnual NUMERIC = NULL
 ,@idCurricula NUMERIC = NULL
 ,@idCreador NUMERIC = NULL
 ,@idCicloLectivo NUMERIC = NULL
 ,@idNivel NUMERIC = NULL
 ,@idAsignatura NUMERIC = NULL
 ,@fechaCreacion DATE = NULL
 ,@fechaAprobada DATE = NULL
 ,@solicitarAprobacion BIT = NULL
 ,@observaciones VARCHAR(50) = NULL
 --,@PendienteAprobacion BIT = NULL
AS
 SET NOCOUNT OFF;

    SELECT DISTINCT PA.idPlanificacionAnual
    ,PA.idCurricula
    ,PA.idCreador
	,PA.idCicloLectivo
    ,PA.fechaCreacion
	,PA.fechaAprobada
    ,PA.solicitarAprobacion
    ,PA.observaciones
	,C.idAsignatura
	,A.nombre AS 'Asignatura'
	,C.idNivel
	,N.nombre AS 'Nivel'
	,C.idOrientacion 
	,O.nombre AS 'Orientacion'
	,P.nombre AS 'nombreCreador'
	,P.apellido AS 'apellidoCreador'
	,CL.nombre AS 'cicloLectivo'
	,dbo.PorcentajePlanificacion_Calculo (C.idCurricula) as 'Porcentaje'
    FROM PlanificacionAnual AS PA 
		INNER JOIN Curricula AS C ON PA.idCurricula = C.idCurricula
		INNER JOIN Asignatura AS A ON C.idAsignatura = A.idAsignatura
		INNER JOIN Nivel AS N ON C.idNivel = N.idNivel
		INNER JOIN Orientacion AS O ON C.idOrientacion = O.idOrientacion
		INNER JOIN CicloLectivo AS CL ON PA.idCicloLectivo = CL.idCicloLectivo	
		INNER JOIN Personas AS P ON PA.idCreador = P.idPersona
    WHERE (@idPlanificacionAnual IS NULL OR @idPlanificacionAnual=PA.idPlanificacionAnual)
   AND (@idCurricula IS NULL OR @idCurricula=PA.idCurricula)
   AND (@idCreador IS NULL OR @idCreador=PA.idCreador)
   AND (@fechaAprobada IS NULL OR @fechaAprobada=PA.fechaAprobada)
   AND (@solicitarAprobacion IS NULL OR @solicitarAprobacion=PA.solicitarAprobacion)
   AND (@observaciones IS NULL OR @observaciones=PA.observaciones)
   AND (@idCicloLectivo IS NULL OR @idCicloLectivo  = PA.idCicloLectivo)
   AND (@idNivel IS NULL OR @idNivel  = N.idNivel)
   AND (@idAsignatura IS NULL OR @idAsignatura  = A.idAsignatura)
   ----AND (@PendienteAprobacion IS NULL OR (PA.solicitarAprobacion = 1 AND PA.fechaAprobada IS NULL ))




GO
/****** Object:  StoredProcedure [dbo].[PlanificacionAnual_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 11/06/12
-- =============================================
CREATE PROCEDURE [dbo].[PlanificacionAnual_Update]
  @idPlanificacionAnual NUMERIC = NULL
 ,@fechaAprobada DATE = NULL
 ,@solicitarAprobacion BIT = NULL
 ,@observaciones VARCHAR(50) = NULL
AS
 SET NOCOUNT OFF;

    UPDATE PlanificacionAnual SET 
      
      fechaAprobada = @fechaAprobada
     ,solicitarAprobacion = @solicitarAprobacion
     ,observaciones = @observaciones
    WHERE @idPlanificacionAnual=idPlanificacionAnual

GO
/****** Object:  StoredProcedure [dbo].[PorcentajePlanificacion_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PorcentajePlanificacion_Select]
@idCurricula INT = null

as

--DECLARE @idCurricula int = 10013
declare @dictados int = 0
declare @planificados int = 0

select @planificados = count(TC.idTemaContenido) 
from TemaContenido as TC
	INNER JOIN Contenido as C on TC.idContenido = C.idContenido and C.idCurricula = @idCurricula

select @dictados = count(TC.idTemaContenido)
from TemaPlanificacionTemaContenido AS TPTC 
	inner join TemaContenido AS TC on TPTC.idTemaContenido = TC.idTemaContenido and TPTC.obligatorio = 1
	inner join Contenido as C on tc.idContenido = C.idContenido and C.idCurricula = @idCurricula

--select @dictados
--select @planificados

if (@planificados > 0 )
	select (@dictados * 100 / @planificados)
else
	select 0
GO
/****** Object:  StoredProcedure [dbo].[Pregunta_Delete]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo
-- Create date: 09/09/12
-- =============================================
CREATE PROCEDURE [dbo].[Pregunta_Delete]
  @idPregunta NUMERIC = NULL
 ,@idEncuesta NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    DELETE FROM Pregunta
    WHERE (@idPregunta IS NULL OR @idPregunta=idPregunta)
   AND (@idEncuesta IS NULL OR @idEncuesta=idEncuesta)
GO
/****** Object:  StoredProcedure [dbo].[Pregunta_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo
-- Create date: 12/08/12
-- =============================================
CREATE PROCEDURE [dbo].[Pregunta_Insert]
  @idPregunta NUMERIC = NULL OUT
 ,@textoPregunta VARCHAR(250) = NULL
 ,@idCategoria NUMERIC = NULL
 ,@objetivo VARCHAR(4000) = NULL
 ,@idEscalaPonderacion NUMERIC = NULL
 ,@peso NUMERIC = NULL
 ,@idEncuesta NUMERIC = NULL
 
AS
 SET NOCOUNT OFF;

IF (@idPregunta=0 or @idPregunta is null)
  BEGIN 
    INSERT INTO Pregunta(
	   textoPregunta
    ,idCategoria
    ,objetivo
		,idEncuesta
	  ,idEscalaPonderacion
	  ,peso
	 )
	 VALUES(
		@textoPregunta
		,@idCategoria
		,@objetivo
		,@idEncuesta
		,@idEscalaPonderacion
		,@peso
	 )
 SET @idPregunta = @@IDENTITY
END
GO
/****** Object:  StoredProcedure [dbo].[Pregunta_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo
-- Create date: 12/08/12
-- =============================================
CREATE PROCEDURE [dbo].[Pregunta_Select]
  @idPregunta NUMERIC = NULL
 ,@textoPregunta VARCHAR(250) = NULL
 ,@idCategoria NUMERIC = NULL
 ,@objetivo VARCHAR(4000) = NULL
 ,@idEncuesta NUMERIC = NULL
 ,@idEscala NUMERIC = NULL
AS
 SET NOCOUNT OFF;

SELECT P.idPregunta
,P.textoPregunta
,P.idCategoria
,P.objetivo
,P.peso
,C.nombre as 'categoria'
,E.idEscalaPonderacion
,E.descripcion as 'descripcionEscala'
,E.nombre as 'escala'
FROM Pregunta AS P
INNER JOIN CategoriaPregunta AS C ON P.idCategoria = C.idCategoria
INNER JOIN EscalaPonderacion AS E ON P.idEscalaPonderacion = E.idEscalaPonderacion
WHERE (@idPregunta IS NULL OR @idPregunta=P.idPregunta)
AND (@textoPregunta IS NULL OR @textoPregunta=P.textoPregunta)
AND (@idCategoria IS NULL OR @idCategoria=P.idCategoria)
AND (@objetivo IS NULL OR @objetivo=P.objetivo)
AND (@idEncuesta IS NULL OR @idEncuesta=P.idEncuesta)
AND (@idEscala IS NULL OR @idEscala=E.idEscalaPonderacion)
GO
/****** Object:  StoredProcedure [dbo].[Pregunta_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo
-- Create date: 12/08/12
-- =============================================
CREATE PROCEDURE [dbo].[Pregunta_Update]
  @idPregunta NUMERIC = NULL
 ,@textoPregunta VARCHAR(250) = NULL
 ,@idCategoria NUMERIC = NULL
 ,@objetivo VARCHAR(4000) = NULL
 ,@idEscalaPonderacion NUMERIC = NULL
 ,@peso NUMERIC(18,2) = NULL
 ,@idEncuesta NUMERIC = NULL

AS
 SET NOCOUNT OFF;

UPDATE Pregunta SET 
  textoPregunta = @textoPregunta
 ,idCategoria = @idCategoria
 ,objetivo = @objetivo
 ,peso = @peso
 ,idEncuesta = @idEncuesta
 ,idEscalaPonderacion = @idEscalaPonderacion
WHERE @idPregunta=idPregunta
AND @idEncuesta=idEncuesta
GO
/****** Object:  StoredProcedure [dbo].[ProcesosEjecutados_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 25/09/11
-- =============================================
CREATE PROCEDURE [dbo].[ProcesosEjecutados_Insert]
  @idProcesoEjecutado NUMERIC = NULL
 ,@fechaEjecucion DATETIME = NULL
 ,@resultado BIT = NULL
 ,@descripcionError varchar(1000) = NULL
 ,@idProcesoAutomatico NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    INSERT INTO ProcesosEjecutados(
     fechaEjecucion
    ,resultado
    ,descripcionError
    ,idProcesoAutomatico
 )
 VALUES(
     @fechaEjecucion
    ,@resultado
    ,@descripcionError
    ,@idProcesoAutomatico
 )

GO
/****** Object:  StoredProcedure [dbo].[Provincias_Delete]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 14/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Provincias_Delete]
  @idProvincia NUMERIC = NULL
 ,@idProvinciaTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@descripcion VARCHAR(250) = NULL
 ,@idPais NUMERIC = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;

    DELETE FROM Provincias
    WHERE (@idProvincia IS NULL OR @idProvincia=idProvincia)
   AND (@idProvinciaTransaccional IS NULL OR @idProvinciaTransaccional=idProvinciaTransaccional)
   AND (@nombre IS NULL OR @nombre=nombre)
   AND (@descripcion IS NULL OR @descripcion=descripcion)
   AND (@idPais IS NULL OR @idPais=idPais)
   AND (@activo IS NULL OR @activo=activo)

-- =====UPDATE==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[Provincias_Exists]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 14/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Provincias_Exists]
  @idProvincia NUMERIC = NULL 
 ,@idProvinciaTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@descripcion VARCHAR(250) = NULL
 ,@idPais NUMERIC = NULL
 ,@activo BIT = NULL
 ,@exists BIT OUT
AS
 SET NOCOUNT OFF;

    IF EXISTS (
 SELECT idProvincia
    FROM Provincias
    WHERE (@idProvincia IS NULL OR @idProvincia=idProvincia)
   AND (@idProvinciaTransaccional IS NULL OR @idProvinciaTransaccional=idProvinciaTransaccional)
   AND (@nombre IS NULL OR @nombre=nombre)
   AND (@descripcion IS NULL OR @descripcion=descripcion)
   AND (@idPais IS NULL OR @idPais=idPais)
   AND (@activo IS NULL OR @activo=activo)
 )
 SET @exists = 1
 ELSE SET @exists = 0

GO
/****** Object:  StoredProcedure [dbo].[Provincias_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 14/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Provincias_Insert]
  @idProvincia NUMERIC = NULL output
 ,@idProvinciaTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@descripcion VARCHAR(250) = NULL
 ,@idPais NUMERIC = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;
SELECT @idProvincia=idProvincia 
  FROM [dbo].[Provincias]
  WHERE [idProvinciaTransaccional]= @idProvinciaTransaccional
  
  SELECT @idPais=idPais
  FROM [dbo].[Paises]
  WHERE [idPaisTransaccional]= @idPais
  
  IF (@idProvincia=0 or @idProvincia is null)
  BEGIN
    INSERT INTO Provincias(
     idProvinciaTransaccional
    ,nombre
    ,descripcion
    ,idPais
    ,activo
 )
 VALUES(
     @idProvinciaTransaccional
    ,@nombre
    ,@descripcion
    ,@idPais
    ,@activo
 )
 SET @idProvincia = @@IDENTITY
END
ELSE
BEGIN
EXECUTE [Provincias_Update] 
	   @idProvincia
	  ,@idProvinciaTransaccional
	  ,@nombre
	  ,@descripcion
	  ,@idPais
	  ,@activo

END

GO
/****** Object:  StoredProcedure [dbo].[Provincias_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 14/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Provincias_Select]
  @idProvincia NUMERIC = NULL
 ,@idProvinciaTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@descripcion VARCHAR(250) = NULL
 ,@idPais NUMERIC = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;

    SELECT idProvincia
    ,idProvinciaTransaccional
    ,nombre
    ,descripcion
    ,idPais
    ,activo
    FROM Provincias
    WHERE (@idProvincia IS NULL OR @idProvincia=idProvincia)
   AND (@idProvinciaTransaccional IS NULL OR @idProvinciaTransaccional=idProvinciaTransaccional)
   AND (@nombre IS NULL OR @nombre=nombre)
   AND (@descripcion IS NULL OR @descripcion=descripcion)
   AND (@idPais IS NULL OR @idPais=idPais)
   AND (@activo IS NULL OR @activo=activo)

-- =====EXISTS==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[Provincias_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 14/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Provincias_Update]
  @idProvincia NUMERIC = NULL
 ,@idProvinciaTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@descripcion VARCHAR(250) = NULL
 ,@idPais NUMERIC = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;

    UPDATE Provincias SET 
      idProvinciaTransaccional = @idProvinciaTransaccional
     ,nombre = @nombre
     ,descripcion = @descripcion
     ,idPais = @idPais
     ,activo = @activo
    WHERE (@idProvinciaTransaccional IS NULL OR @idProvinciaTransaccional=idProvinciaTransaccional)
   AND (@nombre IS NULL OR @nombre=nombre)
   AND (@descripcion IS NULL OR @descripcion=descripcion)
   AND (@idPais IS NULL OR @idPais=idPais)
   AND (@activo IS NULL OR @activo=activo)

-- =====SELECT==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[RegistroClases_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:  LAURA PASTORINO
-- Create date: 19/08/12
-- =============================================
CREATE PROCEDURE [dbo].[RegistroClases_Insert]
  @idRegistroClases NUMERIC = NULL output
 ,@idAsignaturaCurso NUMERIC = NULL
 ,@idEventoAgenda NUMERIC = NULL
 ,@idAgendaActividades NUMERIC = NULL
 ,@username VARCHAR(50) = NULL
 ,@idTipoEvento NUMERIC = NULL
 ,@fechaModificacion DATE = NULL
 ,@fechaEvento DATE = NULL
 ,@descripcion VARCHAR(4000) = NULL
 ,@activo BIT = NULL
 ,@idTipoRegistroClases NUMERIC = NULL
 ,@observaciones VARCHAR(4000) = NULL
AS
 SET NOCOUNT OFF;
Declare @idEvento numeric
EXECUTE @idEvento = [EventoAgenda_Insert] 
   @idEventoAgenda
  ,@idAgendaActividades
  ,@username
  ,@idTipoEvento
  ,@fechaModificacion
  ,@fechaEvento
  ,@descripcion
  ,@activo

    INSERT INTO RegistroClases(
     idEventoAgenda
    ,idTipoRegistroClases
    ,idAsignaturaCurso
    ,observaciones
 )
 VALUES(
     @idEvento
    ,@idTipoRegistroClases
    ,@idAsignaturaCurso
    ,@observaciones
 )
	SET @idRegistroClases = @@IDENTITY
 RETURN SCOPE_IDENTITY()

GO
/****** Object:  StoredProcedure [dbo].[RegistroClases_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[RegistroClases_Select]
  @idRegistroClases NUMERIC = NULL
 ,@idAsignaturaCicloLectivo NUMERIC = NULL
 ,@idEvento NUMERIC = NULL
 ,@fechaDesde DATE = NULL
 ,@fechaHasta DATE = NULL
 ,@fechaEvento DATE = NULL
 ,@idAgendaActividad NUMERIC = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;

    SELECT 
     RC.idRegistroClases
    ,RC.idAsignaturaCurso
    ,RC.idEventoAgenda
    ,RC.idTipoRegistroClases
    ,TP.descripcion AS 'tipoEvento'
    ,EA.activo
    ,EA.descripcion
    ,EA.fechaAlta
    ,EA.fechaModificacion
    ,EA.fechaEvento
    ,EA.idAgendaActividades
    ,EA.idEventoAgenda
    ,EA.idOrganizador
    ,EA.idTipoEvento
    ,A.nombre AS 'asignatura'
    ,P.nombre
    ,P.apellido
    ,P.username AS 'usernameOrganizador'
    ,TRC.nombre AS 'tipoRegistroClases'
    FROM EventoAgenda AS EA 
		INNER JOIN RegistroClases AS RC ON EA.idEventoAgenda = RC.idEventoAgenda
		INNER JOIN TipoEvento AS TP ON EA.idTipoEvento = TP.idTipoEvento
		INNER JOIN AsignaturaCicloLectivo AS AC ON RC.idAsignaturaCurso = AC.idAsignaturaCicloLectivo
		INNER JOIN Asignatura AS A ON AC.idAsignatura = A.idAsignatura
		INNER JOIN Personas AS P ON EA.idOrganizador = P.idPersona
		INNER JOIN dbo.TipoRegistroClases AS TRC ON TRC.idTipoRegistroClases = RC.idTipoRegistroClases
    WHERE 
		(@idRegistroClases IS NULL OR @idRegistroClases=RC.idRegistroClases)
	AND (@idAsignaturaCicloLectivo IS NULL OR @idAsignaturaCicloLectivo=AC.idAsignaturaCicloLectivo)
	AND (@fechaDesde IS NULL OR EA.fechaEvento BETWEEN @fechaDesde AND @fechaHasta)
	AND (@fechaEvento IS NULL OR EA.fechaEvento = @fechaEvento)
	AND (@idAgendaActividad IS NULL OR @idAgendaActividad = EA.idAgendaActividades)
	AND (@activo IS NULL OR @activo = EA.activo )
	AND (@idEvento IS NULL OR @idEvento <> RC.idEventoAgenda)

	ORDER BY EA.fechaEvento DESC 


GO
/****** Object:  StoredProcedure [dbo].[RegistroClases_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:  LAURA
-- Create date: 15/08/11
-- =============================================
CREATE PROCEDURE [dbo].[RegistroClases_Update]
  @idRegistroClases NUMERIC = NULL
 ,@idAsignaturaCurso NUMERIC = NULL
 ,@idEventoAgenda NUMERIC = NULL
 ,@idAgendaActividades NUMERIC = NULL
 ,@username VARCHAR(50) = NULL
 ,@idTipoEvento NUMERIC = NULL
 ,@fechaAlta DATE = NULL
 ,@fechaModificacion DATE = NULL
 ,@fechaEvento DATE = NULL
 ,@descripcion VARCHAR(4000) = NULL
 ,@activo BIT = NULL 
 ,@idTipoRegistroClases NUMERIC = NULL
 ,@observaciones VARCHAR(4000) = NULL
AS
 SET NOCOUNT OFF;

DECLARE @idOrganizador NUMERIC = NULL

SELECT @idOrganizador = idPersona
FROM Personas WHERE username = @username

	EXECUTE EventoAgenda_Update 
	 @idEventoAgenda
   ,@idAgendaActividades 
	 ,@idOrganizador 
	 ,@idTipoEvento 
	 ,@fechaModificacion 
	 ,@fechaEvento 
	 ,@descripcion 
	 ,@activo

	UPDATE RegistroClases SET 
      idEventoAgenda = @idEventoAgenda
     ,idTipoRegistroClases = @idTipoRegistroClases
     ,idAsignaturaCurso = @idAsignaturaCurso
     ,observaciones = @observaciones
    WHERE @idRegistroClases=idRegistroClases


GO
/****** Object:  StoredProcedure [dbo].[RegistroClases_VerificarDisponibilidadAgenda]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:  Laura
-- Create date: 20/08/12
-- =============================================
CREATE PROCEDURE [dbo].[RegistroClases_VerificarDisponibilidadAgenda]
  @fechaEvento DATE = NULL
 ,@idAsignaturaCurso NUMERIC = NULL
 ,@idAgendaActividad NUMERIC = NULL
 ,@idEventoAgenda NUMERIC = NULL
AS
 SET NOCOUNT OFF;

SELECT 
     EA.descripcion
    ,EA.fechaEvento
    ,EA.idAgendaActividades
    ,EA.idEventoAgenda
    ,EA.idTipoEvento
    FROM EventoAgenda AS EA
		INNER JOIN RegistroClases AS RC ON EA.idEventoAgenda = RC.idEventoAgenda
		INNER JOIN AgendaActividades AS AA ON AA.idAgendaActividad = EA.idAgendaActividades
    WHERE 
	    (@fechaEvento IS NULL OR EA.fechaEvento = @fechaEvento)
	AND (@idAsignaturaCurso IS NULL OR RC.idAsignaturaCurso = @idAsignaturaCurso)
	AND (@idAgendaActividad IS NULL OR EA.idAgendaActividades = @idAgendaActividad)
	AND (@idEventoAgenda IS NULL OR EA.idEventoAgenda <> @idEventoAgenda)	
	AND (EA.idTipoEvento = 1)
	AND (EA.activo = 1)


GO
/****** Object:  StoredProcedure [dbo].[RelEncuestaRol_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 28/10/12
-- =============================================
CREATE PROCEDURE [dbo].[RelEncuestaRol_Insert]
  @idEncuesta NUMERIC = NULL
 ,@rol VARCHAR(50) = NULL
AS
 SET NOCOUNT OFF;

    INSERT INTO RelEncuestaRol(
     idEncuesta
    ,rol
 )
 VALUES(
     @idEncuesta
    ,@rol
 )
GO
/****** Object:  StoredProcedure [dbo].[RelEncuestaRol_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 28/10/12
-- =============================================
CREATE PROCEDURE [dbo].[RelEncuestaRol_Select]
  @idEncuesta NUMERIC = NULL
 ,@rol VARCHAR(50) = NULL
AS
 SET NOCOUNT OFF;

    SELECT idEncuesta
		,rol as [rolName]
    FROM RelEncuestaRol
    WHERE (@idEncuesta IS NULL OR @idEncuesta=idEncuesta)
   AND (@rol IS NULL OR @rol=rol)
GO
/****** Object:  StoredProcedure [dbo].[RelPlanificacionCurso_Delete]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 02/04/13
-- =============================================
CREATE PROCEDURE [dbo].[RelPlanificacionCurso_Delete]
  @idPlanificacionAnual NUMERIC = NULL
 ,@idCursoCicloLectivo NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    DELETE FROM RelPlanificacionCurso
    WHERE (@idPlanificacionAnual IS NULL OR @idPlanificacionAnual=idPlanificacionAnual)
   AND (@idCursoCicloLectivo IS NULL OR @idCursoCicloLectivo=idCursoCicloLectivo)
GO
/****** Object:  StoredProcedure [dbo].[RelPlanificacionCurso_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 02/04/13
-- =============================================
CREATE PROCEDURE [dbo].[RelPlanificacionCurso_Insert]
  @idPlanificacionAnual NUMERIC = NULL
 ,@idCursoCicloLectivo NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    INSERT INTO RelPlanificacionCurso(
     idPlanificacionAnual
    ,idCursoCicloLectivo
 )
 VALUES(
     @idPlanificacionAnual
    ,@idCursoCicloLectivo
 )
GO
/****** Object:  StoredProcedure [dbo].[RelPlanificacionCurso_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 02/04/13
-- =============================================
CREATE PROCEDURE [dbo].[RelPlanificacionCurso_Select]
  @idPlanificacionAnual NUMERIC = NULL
 ,@idCursoCicloLectivo NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    SELECT RPC.idPlanificacionAnual
    ,RPC.idCursoCicloLectivo
	,C.nombre AS 'curso'
    FROM RelPlanificacionCurso AS RPC
		INNER JOIN CursosCicloLectivo AS CCL ON RPC.idCursoCicloLectivo = CCL.idCursoCicloLectivo
		INNER JOIN Curso AS C ON C.idCurso = CCL.idCurso
    WHERE (@idPlanificacionAnual IS NULL OR @idPlanificacionAnual=RPC.idPlanificacionAnual)
   AND (@idCursoCicloLectivo IS NULL OR @idCursoCicloLectivo=RPC.idCursoCicloLectivo)
GO
/****** Object:  StoredProcedure [dbo].[Reporte_Accesos]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Reporte_Accesos]
	-- Add the parameters for the stored procedure here
  @idAcceso NUMERIC = NULL
 ,@idPagina NUMERIC = NULL
 ,@fecha DATE = NULL
 ,@hora TIME = NULL
 ,@username VARCHAR(50) = NULL
 ,@fechaDesde DATE = NULL
 ,@fechaHasta DATE  = NULL
 ,@listaRoles VARCHAR(MAX) = NULL
 ,@listaPaginas VARCHAR(MAX) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    SELECT
     A.fecha
    --,A.username
    ,P.titulo
    ,R.RoleName
    ,count(A.idAcceso) AS 'cantidadAccesos'
    FROM [Accesos] AS A 
		INNER JOIN [Paginas] AS P ON 
			A.[idPagina] = P.[idPagina]
		LEFT JOIN [EDUAR_aspnet_services].[dbo].[aspnet_Users] AS U ON 
			A.username = U.UserName
		LEFT JOIN [EDUAR_aspnet_services].[dbo].[aspnet_UsersInRoles] AS UR
			ON U.UserId = UR.UserId
		LEFT JOIN [EDUAR_aspnet_services].[dbo].[aspnet_Roles] AS R
			ON UR.RoleId = R.RoleId
    WHERE (@idAcceso IS NULL OR @idAcceso=A.idAcceso)
	   AND (@idPagina IS NULL OR @idPagina=A.idPagina)
	   AND (@fecha IS NULL OR @fecha=A.fecha)
	   AND (@hora IS NULL OR @hora=A.hora)
	   AND (@username IS NULL OR @username=A.username)
	   AND (@fechaDesde IS NULL OR A.fecha >= @fechaDesde)
	   AND (@fechaHasta IS NULL OR A.fecha <= @fechaHasta)
	   AND (@listaRoles IS NULL OR R.RoleName IN (SELECT * FROM [ArrayToTable] (@listaRoles)))
	   AND (@listaPaginas IS NULL OR P.idPagina IN (SELECT * FROM [ArrayToTable] (@listaPaginas)))
	GROUP BY
	     A.fecha
		--,A.username
		,P.titulo
		,R.RoleName
	ORDER BY A.fecha DESC, 'cantidadAccesos' DESC
END

GO
/****** Object:  StoredProcedure [dbo].[Reporte_AnalisisPromedios-CicloLectivo]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 13/07/11
-- Parameters:
		-- idAsignatura: AsignaturaCicloLectivo
		-- idCurso: CursosCicloLectivo
		-- idCicloLectivo = CicloLectivo
		-- idNivel = Nivel
-- =============================================
CREATE PROCEDURE [dbo].[Reporte_AnalisisPromedios-CicloLectivo]
	@listaCicloLectivo VARCHAR(MAX) = NULL
AS
 SET NOCOUNT OFF;
 
	SELECT
	CL.nombre as 'CicloLectivo'
	  ,round(AVG(cast(VES.valor as float)),2,2) as 'Promedio'
   
	FROM [Calificacion] As CAL
			INNER JOIN [AsignaturaCicloLectivo] AS ACL ON CAL.idAsignaturaCicloLectivo = ACL.idAsignatura
			INNER JOIN [CursosCicloLectivo] AS CCL ON CCL.idCursoCicloLectivo=ACL.idCursoCicloLectivo
			INNER JOIN [Asignatura] AS A ON ACL.idAsignatura = A.idAsignatura
			INNER JOIN [CicloLectivo] AS CL ON CCL.idCicloLectivo = CL.idCicloLectivo
			INNER JOIN [Curso] AS CUR ON CCL.idCurso = CUR.idCurso
			INNER JOIN [Nivel] as NIV on CUR.idNivel = NIV.idNivel
			INNER JOIN [ValoresEscalaCalificacion] AS VES ON CAL.idValorCalificacion = VES.idValorEscalaCalificacion
			INNER JOIN [AlumnoCursoCicloLectivo] AS ACCL ON CAL.idAlumnoCursoCicloLectivo=ACCL.idAlumnoCursoCicloLectivo
													AND CCL.idCursoCicloLectivo=ACCL.idCursoCicloLectivo
													
  WHERE 
		(@listaCicloLectivo IS NULL OR CL.idCicloLectivo IN (SELECT * FROM [ArrayToTable] (@listaCicloLectivo)))
	GROUP BY CL.nombre

GO
/****** Object:  StoredProcedure [dbo].[Reporte_AnalisisPromedios-CicloLectivo-Alumnos]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 13/07/11
-- Parameters:
		-- idAsignatura: AsignaturaCicloLectivo
		-- idCurso: CursosCicloLectivo
		-- idCicloLectivo = CicloLectivo
		-- idNivel = Nivel
-- =============================================
CREATE PROCEDURE [dbo].[Reporte_AnalisisPromedios-CicloLectivo-Alumnos]
	@listaCicloLectivo VARCHAR(MAX) = NULL
	,@listaAlumnos VARCHAR(MAX) = NULL
AS
 SET NOCOUNT OFF;
 
	SELECT
	CL.nombre as 'CicloLectivo'
	,PER.apellido+', '+PER.nombre as 'Alumno'
	  ,round(AVG(cast(VES.valor as float)),2,2) as 'Promedio'
   
	FROM [Calificacion] As CAL
			INNER JOIN [AsignaturaCicloLectivo] AS ACL ON CAL.idAsignaturaCicloLectivo = ACL.idAsignatura
			INNER JOIN [CursosCicloLectivo] AS CCL ON CCL.idCursoCicloLectivo=ACL.idCursoCicloLectivo
			INNER JOIN [Asignatura] AS A ON ACL.idAsignatura = A.idAsignatura
			INNER JOIN [CicloLectivo] AS CL ON CCL.idCicloLectivo = CL.idCicloLectivo
			INNER JOIN [Curso] AS CUR ON CCL.idCurso = CUR.idCurso
			INNER JOIN [Nivel] as NIV on CUR.idNivel = NIV.idNivel
			INNER JOIN [ValoresEscalaCalificacion] AS VES ON CAL.idValorCalificacion = VES.idValorEscalaCalificacion
			INNER JOIN [AlumnoCursoCicloLectivo] AS ACCL ON CAL.idAlumnoCursoCicloLectivo=ACCL.idAlumnoCursoCicloLectivo
													AND CCL.idCursoCicloLectivo=ACCL.idCursoCicloLectivo
			INNER JOIN [Alumnos] AS ALU ON ACCL.idAlumno=ALU.idAlumno
			INNER JOIN [Personas] AS PER ON ALU.idPersona=PER.idPersona
													
  WHERE 
		(@listaCicloLectivo IS NULL OR CL.idCicloLectivo IN (SELECT * FROM [ArrayToTable] (@listaCicloLectivo)))
		AND (@listaAlumnos IS NULL OR ALU.idAlumno IN (SELECT * FROM [ArrayToTable] (@listaAlumnos)))
	
	GROUP BY CL.nombre,PER.apellido+', '+PER.nombre

	ORDER BY 1,2

GO
/****** Object:  StoredProcedure [dbo].[Reporte_AnalisisPromedios-CicloLectivo-Asignatura]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 13/07/11
-- Parameters:
		-- idAsignatura: AsignaturaCicloLectivo
		-- idCurso: CursosCicloLectivo
		-- idCicloLectivo = CicloLectivo
		-- idNivel = Nivel
-- =============================================
CREATE PROCEDURE [dbo].[Reporte_AnalisisPromedios-CicloLectivo-Asignatura]
	@listaCicloLectivo VARCHAR(MAX) = NULL
	,@listaAsignaturas VARCHAR(MAX) = NULL
AS
 SET NOCOUNT OFF;
 
	SELECT
	CL.nombre as 'CicloLectivo'
	,A.nombre as 'Asignatura'
	  ,round(AVG(cast(VES.valor as float)),2,2) as 'Promedio'
   
	FROM [Calificacion] As CAL
			INNER JOIN [AsignaturaCicloLectivo] AS ACL ON CAL.idAsignaturaCicloLectivo = ACL.idAsignatura
			INNER JOIN [CursosCicloLectivo] AS CCL ON CCL.idCursoCicloLectivo=ACL.idCursoCicloLectivo
			INNER JOIN [Asignatura] AS A ON ACL.idAsignatura = A.idAsignatura
			INNER JOIN [CicloLectivo] AS CL ON CCL.idCicloLectivo = CL.idCicloLectivo
			INNER JOIN [Curso] AS CUR ON CCL.idCurso = CUR.idCurso
			INNER JOIN [Nivel] as NIV on CUR.idNivel = NIV.idNivel
			INNER JOIN [ValoresEscalaCalificacion] AS VES ON CAL.idValorCalificacion = VES.idValorEscalaCalificacion
			INNER JOIN [AlumnoCursoCicloLectivo] AS ACCL ON CAL.idAlumnoCursoCicloLectivo=ACCL.idAlumnoCursoCicloLectivo
													AND CCL.idCursoCicloLectivo=ACCL.idCursoCicloLectivo
													
  WHERE 
			(@listaAsignaturas IS NULL OR A.idAsignatura IN (SELECT * FROM [ArrayToTable] (@listaAsignaturas)))
		AND (@listaCicloLectivo IS NULL OR CL.idCicloLectivo IN (SELECT * FROM [ArrayToTable] (@listaCicloLectivo)))
	GROUP BY CL.nombre,A.nombre

GO
/****** Object:  StoredProcedure [dbo].[Reporte_AnalisisPromedios-CicloLectivo-Asignatura-Alumnos]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 13/07/11
-- Parameters:
		-- idAsignatura: AsignaturaCicloLectivo
		-- idCurso: CursosCicloLectivo
		-- idCicloLectivo = CicloLectivo
		-- idNivel = Nivel
-- =============================================
CREATE PROCEDURE [dbo].[Reporte_AnalisisPromedios-CicloLectivo-Asignatura-Alumnos]
	@listaCicloLectivo VARCHAR(MAX) = NULL
	,@listaAsignaturas VARCHAR(MAX) = NULL
	,@listaAlumnos VARCHAR(MAX) = NULL
AS
 SET NOCOUNT OFF;
 
	SELECT
	CL.nombre as 'CicloLectivo'
	,A.nombre as 'Asignatura'
	,PER.apellido+', '+PER.nombre as 'Alumno'
	  ,round(AVG(cast(VES.valor as float)),2,2) as 'Promedio'
   
	FROM [Calificacion] As CAL
			INNER JOIN [AsignaturaCicloLectivo] AS ACL ON CAL.idAsignaturaCicloLectivo = ACL.idAsignatura
			INNER JOIN [CursosCicloLectivo] AS CCL ON CCL.idCursoCicloLectivo=ACL.idCursoCicloLectivo
			INNER JOIN [Asignatura] AS A ON ACL.idAsignatura = A.idAsignatura
			INNER JOIN [CicloLectivo] AS CL ON CCL.idCicloLectivo = CL.idCicloLectivo
			INNER JOIN [Curso] AS CUR ON CCL.idCurso = CUR.idCurso
			INNER JOIN [Nivel] as NIV on CUR.idNivel = NIV.idNivel
			INNER JOIN [ValoresEscalaCalificacion] AS VES ON CAL.idValorCalificacion = VES.idValorEscalaCalificacion
			INNER JOIN [AlumnoCursoCicloLectivo] AS ACCL ON CAL.idAlumnoCursoCicloLectivo=ACCL.idAlumnoCursoCicloLectivo
													AND CCL.idCursoCicloLectivo=ACCL.idCursoCicloLectivo
			INNER JOIN [Alumnos] AS ALU ON ACCL.idAlumno=ALU.idAlumno
			INNER JOIN [Personas] AS PER ON ALU.idPersona=PER.idPersona
													
  WHERE 
			(@listaAsignaturas IS NULL OR A.idAsignatura IN (SELECT * FROM [ArrayToTable] (@listaAsignaturas)))
		AND (@listaCicloLectivo IS NULL OR CL.idCicloLectivo IN (SELECT * FROM [ArrayToTable] (@listaCicloLectivo)))
		AND (@listaAlumnos IS NULL OR ALU.idAlumno IN (SELECT * FROM [ArrayToTable] (@listaAlumnos)))

	GROUP BY CL.nombre,A.nombre,PER.apellido+', '+PER.nombre

	ORDER BY 1,2,3
GO
/****** Object:  StoredProcedure [dbo].[Reporte_AnalisisPromedios-CicloLectivo-Nivel]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 13/07/11
-- Parameters:
		-- idAsignatura: AsignaturaCicloLectivo
		-- idCurso: CursosCicloLectivo
		-- idCicloLectivo = CicloLectivo
		-- idNivel = Nivel
-- =============================================
CREATE PROCEDURE [dbo].[Reporte_AnalisisPromedios-CicloLectivo-Nivel]
	@listaCicloLectivo VARCHAR(MAX) = NULL
	,@listaNivel VARCHAR(MAX) = NULL
AS
 SET NOCOUNT OFF;
 
	SELECT
	CL.nombre as 'CicloLectivo'
	,NIV.nombre+' '+CUR.nombre as 'Curso'	
	  ,round(AVG(cast(VES.valor as float)),2,2) as 'Promedio'
   
	FROM [Calificacion] As CAL
			INNER JOIN [AsignaturaCicloLectivo] AS ACL ON CAL.idAsignaturaCicloLectivo = ACL.idAsignatura
			INNER JOIN [CursosCicloLectivo] AS CCL ON CCL.idCursoCicloLectivo=ACL.idCursoCicloLectivo
			INNER JOIN [Asignatura] AS A ON ACL.idAsignatura = A.idAsignatura
			INNER JOIN [CicloLectivo] AS CL ON CCL.idCicloLectivo = CL.idCicloLectivo
			INNER JOIN [Curso] AS CUR ON CCL.idCurso = CUR.idCurso
			INNER JOIN [Nivel] as NIV on CUR.idNivel = NIV.idNivel
			INNER JOIN [ValoresEscalaCalificacion] AS VES ON CAL.idValorCalificacion = VES.idValorEscalaCalificacion
			INNER JOIN [AlumnoCursoCicloLectivo] AS ACCL ON CAL.idAlumnoCursoCicloLectivo=ACCL.idAlumnoCursoCicloLectivo
													AND CCL.idCursoCicloLectivo=ACCL.idCursoCicloLectivo
													
  WHERE 
		(@listaCicloLectivo IS NULL OR CL.idCicloLectivo IN (SELECT * FROM [ArrayToTable] (@listaCicloLectivo)))
		AND (@listaNivel IS NULL OR NIV.idNivel IN (SELECT * FROM [ArrayToTable] (@listaNivel)))
	GROUP BY CL.nombre,NIV.nombre+' '+CUR.nombre
GO
/****** Object:  StoredProcedure [dbo].[Reporte_AnalisisPromedios-CicloLectivo-Nivel-Alumnos]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 13/07/11
-- Parameters:
		-- idAsignatura: AsignaturaCicloLectivo
		-- idCurso: CursosCicloLectivo
		-- idCicloLectivo = CicloLectivo
		-- idNivel = Nivel
-- =============================================
CREATE PROCEDURE [dbo].[Reporte_AnalisisPromedios-CicloLectivo-Nivel-Alumnos]
	@listaCicloLectivo VARCHAR(MAX) = NULL
	,@listaNivel VARCHAR(MAX) = NULL
	,@listaAlumnos VARCHAR(MAX) = NULL
AS
 SET NOCOUNT OFF;
 
	SELECT
	CL.nombre as 'CicloLectivo'
	,NIV.nombre+' '+CUR.nombre as 'Curso'	
	,PER.apellido+', '+PER.nombre as 'Alumno'
	  ,round(AVG(cast(VES.valor as float)),2,2) as 'Promedio'
   
	FROM [Calificacion] As CAL
			INNER JOIN [AsignaturaCicloLectivo] AS ACL ON CAL.idAsignaturaCicloLectivo = ACL.idAsignatura
			INNER JOIN [CursosCicloLectivo] AS CCL ON CCL.idCursoCicloLectivo=ACL.idCursoCicloLectivo
			INNER JOIN [Asignatura] AS A ON ACL.idAsignatura = A.idAsignatura
			INNER JOIN [CicloLectivo] AS CL ON CCL.idCicloLectivo = CL.idCicloLectivo
			INNER JOIN [Curso] AS CUR ON CCL.idCurso = CUR.idCurso
			INNER JOIN [Nivel] as NIV on CUR.idNivel = NIV.idNivel
			INNER JOIN [ValoresEscalaCalificacion] AS VES ON CAL.idValorCalificacion = VES.idValorEscalaCalificacion
			INNER JOIN [AlumnoCursoCicloLectivo] AS ACCL ON CAL.idAlumnoCursoCicloLectivo=ACCL.idAlumnoCursoCicloLectivo
													AND CCL.idCursoCicloLectivo=ACCL.idCursoCicloLectivo
			INNER JOIN [Alumnos] AS ALU ON ACCL.idAlumno=ALU.idAlumno
			INNER JOIN [Personas] AS PER ON ALU.idPersona=PER.idPersona
													
  WHERE 
		(@listaCicloLectivo IS NULL OR CL.idCicloLectivo IN (SELECT * FROM [ArrayToTable] (@listaCicloLectivo)))
		AND (@listaNivel IS NULL OR NIV.idNivel IN (SELECT * FROM [ArrayToTable] (@listaNivel)))
		AND (@listaAlumnos IS NULL OR ALU.idAlumno IN (SELECT * FROM [ArrayToTable] (@listaAlumnos)))
	
	GROUP BY CL.nombre,NIV.nombre+' '+CUR.nombre,PER.apellido+', '+PER.nombre

	ORDER BY 1,2,3
GO
/****** Object:  StoredProcedure [dbo].[Reporte_AnalisisPromedios-CicloLectivo-Nivel-Asignatura]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 13/07/11
-- Parameters:
		-- idAsignatura: AsignaturaCicloLectivo
		-- idCurso: CursosCicloLectivo
		-- idCicloLectivo = CicloLectivo
		-- idNivel = Nivel
-- =============================================
CREATE PROCEDURE [dbo].[Reporte_AnalisisPromedios-CicloLectivo-Nivel-Asignatura]
	@listaCicloLectivo VARCHAR(MAX) = NULL
	,@listaNivel VARCHAR(MAX) = NULL
	,@listaAsignaturas VARCHAR(MAX) = NULL
AS
 SET NOCOUNT OFF;
 
	SELECT
	CL.nombre as 'CicloLectivo'
	,NIV.nombre+' '+CUR.nombre as 'Curso'	
	,A.nombre as 'Asignatura'
	  ,round(AVG(cast(VES.valor as float)),2,2) as 'Promedio'
   
	FROM [Calificacion] As CAL
			INNER JOIN [AsignaturaCicloLectivo] AS ACL ON CAL.idAsignaturaCicloLectivo = ACL.idAsignatura
			INNER JOIN [CursosCicloLectivo] AS CCL ON CCL.idCursoCicloLectivo=ACL.idCursoCicloLectivo
			INNER JOIN [Asignatura] AS A ON ACL.idAsignatura = A.idAsignatura
			INNER JOIN [CicloLectivo] AS CL ON CCL.idCicloLectivo = CL.idCicloLectivo
			INNER JOIN [Curso] AS CUR ON CCL.idCurso = CUR.idCurso
			INNER JOIN [Nivel] as NIV on CUR.idNivel = NIV.idNivel
			INNER JOIN [ValoresEscalaCalificacion] AS VES ON CAL.idValorCalificacion = VES.idValorEscalaCalificacion
			INNER JOIN [AlumnoCursoCicloLectivo] AS ACCL ON CAL.idAlumnoCursoCicloLectivo=ACCL.idAlumnoCursoCicloLectivo
													AND CCL.idCursoCicloLectivo=ACCL.idCursoCicloLectivo
													
  WHERE 
			(@listaAsignaturas IS NULL OR A.idAsignatura IN (SELECT * FROM [ArrayToTable] (@listaAsignaturas)))
		AND (@listaCicloLectivo IS NULL OR CL.idCicloLectivo IN (SELECT * FROM [ArrayToTable] (@listaCicloLectivo)))
		AND (@listaNivel IS NULL OR NIV.idNivel IN (SELECT * FROM [ArrayToTable] (@listaNivel)))
	GROUP BY CL.nombre,NIV.nombre+' '+CUR.nombre,A.nombre

GO
/****** Object:  StoredProcedure [dbo].[Reporte_AnalisisPromedios-CicloLectivo-Nivel-Asignatura-Alumnos]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 13/07/11
-- Parameters:
		-- idAsignatura: AsignaturaCicloLectivo
		-- idCurso: CursosCicloLectivo
		-- idCicloLectivo = CicloLectivo
		-- idNivel = Nivel
-- =============================================
CREATE PROCEDURE [dbo].[Reporte_AnalisisPromedios-CicloLectivo-Nivel-Asignatura-Alumnos]
	@listaCicloLectivo VARCHAR(MAX) = NULL
	,@listaNivel VARCHAR(MAX) = NULL
	,@listaAsignaturas VARCHAR(MAX) = NULL
	,@listaAlumnos VARCHAR(MAX) = NULL
AS
 SET NOCOUNT OFF;
 
	SELECT
	CL.nombre as 'CicloLectivo'
	,NIV.nombre+' '+CUR.nombre as 'Curso'	
	,A.nombre as 'Asignatura'
	,PER.apellido+', '+PER.nombre as 'Alumno'
	  ,round(AVG(cast(VES.valor as float)),2,2) as 'Promedio'
   
	FROM [Calificacion] As CAL
			INNER JOIN [AsignaturaCicloLectivo] AS ACL ON CAL.idAsignaturaCicloLectivo = ACL.idAsignatura
			INNER JOIN [CursosCicloLectivo] AS CCL ON CCL.idCursoCicloLectivo=ACL.idCursoCicloLectivo
			INNER JOIN [Asignatura] AS A ON ACL.idAsignatura = A.idAsignatura
			INNER JOIN [CicloLectivo] AS CL ON CCL.idCicloLectivo = CL.idCicloLectivo
			INNER JOIN [Curso] AS CUR ON CCL.idCurso = CUR.idCurso
			INNER JOIN [Nivel] as NIV on CUR.idNivel = NIV.idNivel
			INNER JOIN [ValoresEscalaCalificacion] AS VES ON CAL.idValorCalificacion = VES.idValorEscalaCalificacion
			INNER JOIN [AlumnoCursoCicloLectivo] AS ACCL ON CAL.idAlumnoCursoCicloLectivo=ACCL.idAlumnoCursoCicloLectivo
													AND CCL.idCursoCicloLectivo=ACCL.idCursoCicloLectivo
			INNER JOIN [Alumnos] AS ALU ON ACCL.idAlumno=ALU.idAlumno
			INNER JOIN [Personas] AS PER ON ALU.idPersona=PER.idPersona
													
  WHERE 
			(@listaAsignaturas IS NULL OR A.idAsignatura IN (SELECT * FROM [ArrayToTable] (@listaAsignaturas)))
		AND (@listaCicloLectivo IS NULL OR CL.idCicloLectivo IN (SELECT * FROM [ArrayToTable] (@listaCicloLectivo)))
		AND (@listaNivel IS NULL OR NIV.idNivel IN (SELECT * FROM [ArrayToTable] (@listaNivel)))
		AND (@listaAlumnos IS NULL OR ALU.idAlumno IN (SELECT * FROM [ArrayToTable] (@listaAlumnos)))
	
	GROUP BY CL.nombre,NIV.nombre+' '+CUR.nombre,A.nombre,PER.apellido+', '+PER.nombre

	ORDER BY 1,2,3,4
GO
/****** Object:  StoredProcedure [dbo].[Reporte_AnalisisRespuestasTextuales]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 13/11/12
-- Parameters:
-- idCursoCicloLectivo
-- idAsignaturaCicloLectivo
-- idAmbito
-- =============================================
CREATE PROCEDURE [dbo].[Reporte_AnalisisRespuestasTextuales]
  @idEncuesta NUMERIC = NULL

AS
 SET NOCOUNT OFF;

BEGIN
	SELECT SUM(V.Cantidad) AS 'Total', V.Status, V.idEncuesta
	FROM vw_AnalisisRespuestasTextuales AS V 
	WHERE (@idEncuesta IS NULL OR @idEncuesta=V.idEncuesta)
	GROUP BY V.Status, V.idEncuesta
END
GO
/****** Object:  StoredProcedure [dbo].[Reporte_CalificacionesPeriodoAlumno]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 14/07/11
-- Parameters:
-- idAlumno: Alumno
-- periodo: Año 
-- =============================================
CREATE PROCEDURE [dbo].[Reporte_CalificacionesPeriodoAlumno]
	 @idAlumno NUMERIC = NULL
	,@fecha DATETIME = NULL
	,@idCurso NUMERIC = NULL
	,@idAsignatura NUMERIC = NULL
	,@idCicloLectivo NUMERIC = NULL
	,@idInstanciaEvaluacion NUMERIC = NULL
	,@fechaDesde DATETIME = NULL
	,@fechaHasta DATETIME = NULL 
	,@usuario VARCHAR(50) = NULL
	,@idPeriodo NUMERIC = NULL
	,@listaAsignaturas VARCHAR(MAX) = NULL
AS
 SET NOCOUNT OFF;
 
	SELECT  
	 per.apellido+' '+per.nombre as 'Nombre'
    ,N.nombre+ ' ' + cur.nombre as 'Curso'
	,cal.fecha as 'Fecha'
	,A.nombre as 'Asignatura'
	,VES.nombre as 'Calificacion'
    FROM	[AlumnoCursoCicloLectivo] AS ALC 
			INNER JOIN [CursosCicloLectivo] AS CCL ON ALC.idCursoCicloLectivo = CCL.idCursoCicloLectivo
			INNER JOIN [AsignaturaCicloLectivo] AS AC ON CCL.idCursoCicloLectivo = AC.idCursoCicloLectivo
			INNER JOIN [Calificacion] AS CAL ON CAL.idAlumnoCursoCicloLectivo = ALC.idAlumnoCursoCicloLectivo
				and cal.idAsignaturaCicloLectivo = ac.idAsignatura
			INNER JOIN [Asignatura] AS A ON AC.idAsignatura = a.idAsignatura
			INNER JOIN [Alumnos] AS ALU ON ALC.idAlumno = ALU.idAlumno
			INNER JOIN [Personas] AS PER ON ALU.idPersona = PER.idPersona
			INNER JOIN [ValoresEscalaCalificacion] AS VES ON CAL.idValorCalificacion = VES.idValorEscalaCalificacion
			INNER JOIN [CicloLectivo] AS CL ON CCL.idCicloLectivo = CL.idCicloLectivo
			INNER JOIN [Curso] AS CUR ON CCL.idCurso = CUR.idCurso
			INNER JOIN Nivel as N on CUR.idNivel = N.idNivel
			INNER JOIN [Periodo] AS P ON CAL.idPeriodo = P.idPeriodo 
			INNER JOIN [Personal] AS PD ON AC.idDocente = PD.idPersonal
			INNER JOIN [Personas] AS PP ON PD.idPersona = PP.idPersona
    WHERE 
		(@idAlumno IS NULL OR @idAlumno=ALU.idAlumno)
		AND 
		(@fecha IS NULL OR @fecha= CAL.fecha)
		AND
		(@idCurso IS NULL OR @idCurso=CCL.idCursoCicloLectivo)
		AND
		(@idAsignatura IS NULL OR @idAsignatura=AC.idAsignaturaCicloLectivo)
	    AND 
	    (@fechaDesde IS NULL OR CAL.fecha >= @fechaDesde)
	    AND 
	    (@fechaHasta IS NULL OR CAL.fecha <= @fechaHasta)
	    AND 
	    (@idCicloLectivo IS NULL OR @idCicloLectivo=CL.idCicloLectivo)
	    AND 
	    (@usuario IS NULL OR @usuario = PP.username)
	    AND year(CAL.fecha) = year(CL.fechaInicio)
	    AND
		(@idPeriodo IS NULL OR @idPeriodo=P.idPeriodo)
	    AND (@listaAsignaturas IS NULL OR AC.idAsignaturaCicloLectivo IN (SELECT * FROM [ArrayToTable] (@listaAsignaturas)))
	ORDER BY 1

GO
/****** Object:  StoredProcedure [dbo].[Reporte_ComparativoCalificacionesConsolidado]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 13/07/11
-- Parameters:
		-- idAsignatura: AsignaturaCicloLectivo
		-- idCurso: CursosCicloLectivo
		-- idCicloLectivo = CicloLectivo
		-- idNivel = Nivel
-- =============================================
CREATE PROCEDURE [dbo].[Reporte_ComparativoCalificacionesConsolidado]
	@idAsignatura NUMERIC = NULL
	--,@idCurso NUMERIC = NULL
	--,@idCicloLectivo NUMERIC = NULL
	,@idNivel NUMERIC = NULL
	,@listaCicloLectivo VARCHAR(MAX) = NULL
	,@listaAsignaturas VARCHAR(MAX) = NULL
AS
 SET NOCOUNT OFF;
 
	SELECT
	A.nombre as 'Asignatura'
	,CL.nombre as 'CicloLectivo'
	,NIV.nombre+' '+CUR.nombre as 'Curso'
    ,round(AVG(cast(VES.valor as float)),2,2) as 'Promedio'
    FROM [Calificacion] As CAL
			INNER JOIN [AsignaturaCicloLectivo] AS ACL ON CAL.idAsignaturaCicloLectivo = ACL.idAsignatura
			INNER JOIN [CursosCicloLectivo] AS CCL ON CCL.idCursoCicloLectivo=ACL.idCursoCicloLectivo
			INNER JOIN [Asignatura] AS A ON ACL.idAsignatura = A.idAsignatura
			INNER JOIN [CicloLectivo] AS CL ON CCL.idCicloLectivo = CL.idCicloLectivo
			INNER JOIN [Curso] AS CUR ON CCL.idCurso = CUR.idCurso
			INNER JOIN [Nivel] as NIV on CUR.idNivel = NIV.idNivel
			INNER JOIN [ValoresEscalaCalificacion] AS VES ON CAL.idValorCalificacion = VES.idValorEscalaCalificacion
			INNER JOIN [AlumnoCursoCicloLectivo] AS ACCL ON CAL.idAlumnoCursoCicloLectivo=ACCL.idAlumnoCursoCicloLectivo
													AND CCL.idCursoCicloLectivo=ACCL.idCursoCicloLectivo
													
    WHERE 
			--(@idAsignatura IS NULL OR @idAsignatura=A.idAsignatura)
			(@listaAsignaturas IS NULL OR A.idAsignatura IN (SELECT * FROM [ArrayToTable] (@listaAsignaturas)))
		AND (@listaCicloLectivo IS NULL OR CL.idCicloLectivo IN (SELECT * FROM [ArrayToTable] (@listaCicloLectivo)))
		--AND (@idCicloLectivo IS NULL OR @idCicloLectivo=CL.idCicloLectivo)
		--AND (@idCurso IS NULL OR @idCurso=CUR.idCurso)
		AND (@idNivel IS NULL OR @idNivel=CUR.idNivel)
	GROUP BY NIV.nombre+' '+CUR.nombre,A.nombre,CL.nombre
	ORDER BY 2,3,1

GO
/****** Object:  StoredProcedure [dbo].[Reporte_ConsolidadoInasistenciasPeriodo]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 13/07/11
-- Parameters:
-- idAlumno: Alumno
-- periodo: Año 
-- =============================================
CREATE PROCEDURE [dbo].[Reporte_ConsolidadoInasistenciasPeriodo]
	 @idAlumno NUMERIC = NULL
	,@idCicloLectivo NUMERIC = NULL
	,@idPeriodo NUMERIC = 0
	,@idCurso NUMERIC = NULL
	,@listaTiposAsistencia VARCHAR(MAX) = NULL	
AS
 SET NOCOUNT OFF;

if (@idPeriodo <= 0 OR @idPeriodo IS NULL)
BEGIN 
	SELECT
	 PER.apellido+' '+PER.nombre as 'Alumno'
	,'Ciclo Lectivo '+cast(datepart(year,CL.fechaInicio) as varchar) as 'Periodo'
	,SUM(TA.valor) as 'Inasistencias'
	,NIV.nombre as 'Nivel'
	,TA.descripcion as 'Motivo'
    FROM 
		[Asistencia] AS A 
			INNER JOIN [TipoAsistencia] AS TA ON TA.idTipoAsistencia = A.idTipoAsistencia
			INNER JOIN [AlumnoCursoCicloLectivo] AS ACCL ON A.idAlumnoCursoCicloLectivo = ACCL.idAlumnoCursoCicloLectivo
			INNER JOIN [Alumnos] AS ALU ON ACCL.idAlumno = ALU.idAlumno
			INNER JOIN [Personas] AS PER ON ALU.idPersona = PER.idPersona
			INNER JOIN [CursosCicloLectivo] AS CCL ON ACCL.idCursoCicloLectivo = CCL.idCursoCicloLectivo
			INNER JOIN [CicloLectivo] AS CL ON CCL.idCicloLectivo = CL.idCicloLectivo
			INNER JOIN [Curso] AS CUR ON CUR.idCurso = CCL.idCurso
			--INNER JOIN [Periodo] AS PRD ON PRD.idCicloLectivo = CL.idCicloLectivo
			INNER JOIN [Nivel] AS NIV ON CUR.idNivel = NIV.idNivel
    WHERE 
		(@idAlumno IS NULL OR @idAlumno=ALU.idAlumno)
	    AND (@idCicloLectivo IS NULL OR @idCicloLectivo=CL.idCicloLectivo)
	    AND (@idCurso IS NULL OR @idCurso=CCL.idCursoCicloLectivo)
	    AND A.fecha BETWEEN CL.fechaInicio AND CL.fechaFin
	    AND (@listaTiposAsistencia IS NULL OR a.idTipoAsistencia IN (SELECT * FROM [ArrayToTable] (@listaTiposAsistencia)))
	GROUP BY PER.apellido+' '+PER.nombre,'Ciclo Lectivo '+cast(datepart(year,CL.fechaInicio) as varchar), NIV.nombre, TA.descripcion
END

if (@idPeriodo > 0)
BEGIN 
	SELECT 
		PER.apellido+' '+PER.nombre as 'Alumno'
		,PRD.nombre as 'Periodo'
		,SUM(TA.valor) as 'Inasistencias'
		,NIV.nombre as 'Nivel'
		,TA.descripcion as 'Motivo'
    FROM 
		[Asistencia] AS A 
			INNER JOIN [TipoAsistencia] AS TA ON TA.idTipoAsistencia = A.idTipoAsistencia
			INNER JOIN [AlumnoCursoCicloLectivo] AS ACCL ON A.idAlumnoCursoCicloLectivo = ACCL.idAlumnoCursoCicloLectivo
			INNER JOIN [Alumnos] AS ALU ON ACCL.idAlumno = ALU.idAlumno
			INNER JOIN [Personas] AS PER ON ALU.idPersona = PER.idPersona
			INNER JOIN [CursosCicloLectivo] AS CCL ON ACCL.idCursoCicloLectivo = CCL.idCursoCicloLectivo
			INNER JOIN [CicloLectivo] AS CL ON CCL.idCicloLectivo = CL.idCicloLectivo
			INNER JOIN [Curso] AS CUR ON CUR.idCurso = CCL.idCurso
			INNER JOIN [Periodo] AS PRD ON PRD.idCicloLectivo = CL.idCicloLectivo
			INNER JOIN [Nivel] AS NIV ON CUR.idNivel = NIV.idNivel
    WHERE 
		(@idAlumno IS NULL OR @idAlumno=ALU.idAlumno)
	    AND (@idCicloLectivo IS NULL OR @idCicloLectivo=CL.idCicloLectivo)
	    AND (@idPeriodo IS NULL OR @idPeriodo=PRD.idPeriodo)
	    AND (@idCurso IS NULL OR @idCurso=CCL.idCursoCicloLectivo)
	    AND A.fecha BETWEEN PRD.fechaInicio AND PRD.fechaFin
	    AND (@listaTiposAsistencia IS NULL OR a.idTipoAsistencia IN (SELECT * FROM [ArrayToTable] (@listaTiposAsistencia)))
	GROUP BY PER.apellido+' '+PER.nombre, PRD.nombre, NIV.nombre, TA.descripcion
END










	--SELECT PER.apellido+', '+PER.nombre as 'Alumno'
	--,
	--CASE @idPeriodo
	--     WHEN 0 THEN 'Ciclo Lectivo '+cast(datepart(year,@fechaInicio) as varchar)
	--     ELSE PRD.nombre
	-- END
	-- AS 'Periodo'
	--,SUM(TA.valor) as 'Inasistencias'
	--,NIV.nombre as 'Nivel'
	--,TA.descripcion as 'Motivo'
 --   FROM 
	--	[Asistencia] AS A 
	--		INNER JOIN [TipoAsistencia] AS TA ON TA.idTipoAsistencia = A.idTipoAsistencia
	--		INNER JOIN [AlumnoCursoCicloLectivo] AS ACCL ON A.idAlumnoCursoCicloLectivo = ACCL.idAlumnoCursoCicloLectivo
	--		INNER JOIN [Alumnos] AS ALU ON ACCL.idAlumno = ALU.idAlumno
	--		INNER JOIN [Personas] AS PER ON ALU.idPersona = PER.idPersona
	--		INNER JOIN [CursosCicloLectivo] AS CCL ON ACCL.idCursoCicloLectivo = CCL.idCursoCicloLectivo
	--		INNER JOIN [CicloLectivo] AS CL ON CCL.idCicloLectivo = CL.idCicloLectivo
	--		INNER JOIN [Curso] AS CUR ON CUR.idCurso = CCL.idCurso
	--		INNER JOIN [Periodo] AS PRD ON PRD.idCicloLectivo = CL.idCicloLectivo
	--		INNER JOIN [Nivel] AS NIV ON CUR.idNivel = NIV.idNivel
 --   WHERE 
	--	(@idAlumno IS NULL OR @idAlumno=ALU.idAlumno)
	--    AND (@idCicloLectivo IS NULL OR @idCicloLectivo=CL.idCicloLectivo)
	--    AND (@idCurso IS NULL OR @idCurso=CUR.idCurso)
	--    AND (A.fecha BETWEEN CL.fechaInicio AND CL.fechaFin)
	--    AND (TA.idTipoAsistenciaTransaccional <> 1)
	--GROUP BY PER.apellido+', '+PER.nombre, 
	--CASE @idPeriodo
	--   WHEN 0 THEN 'Ciclo Lectivo '+cast(datepart(year,CL.fechaInicio) as varchar) 
	--   ELSE PRD.nombre 
	--END

GO
/****** Object:  StoredProcedure [dbo].[Reporte_ConsolidadoSancionesPeriodo]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 13/07/11
-- Parameters:
-- idAlumno: Alumno
-- periodo: Año 
-- =============================================
CREATE PROCEDURE [dbo].[Reporte_ConsolidadoSancionesPeriodo]
	 @idAlumno NUMERIC = NULL
	,@idCicloLectivo NUMERIC = NULL
	,@idPeriodo NUMERIC = NULL
	,@idCurso NUMERIC = NULL
	,@listaTiposSancion VARCHAR(MAX) = NULL
	,@listaMotivoSancion VARCHAR(MAX) = NULL
AS
 SET NOCOUNT OFF;
 
Declare @fechaInicio date = null
Declare @fechaFin date = null

select 
	@fechaInicio = min(fechaInicio), 
	@fechaFin = max(fechaFin)
from periodo
where idciclolectivo=@idCicloLectivo

if (@idPeriodo <= 0 OR @idPeriodo IS NULL)
BEGIN 
	SELECT 
	 PER.apellido+' '+PER.nombre as 'Alumno'
	,'Ciclo Lectivo '+cast(datepart(year,@fechaInicio) as varchar) as 'Periodo'
	,SUM(S.cantidad) as 'Sanciones'
	, MS.descripcion as 'Motivo'
	, TS.nombre as 'Tipo'
    FROM 
		[Sancion] AS S 
				INNER JOIN [AlumnoCursoCicloLectivo] AS ALC ON S.idAlumnoCursoCicloLectivo = ALC.idAlumnoCursoCicloLectivo
				INNER JOIN [Alumnos] AS ALU ON ALC.idAlumno = ALU.idAlumno
				INNER JOIN [Personas] AS PER ON ALU.idPersona = PER.idPersona
				INNER JOIN CursosCicloLectivo AS CCL ON ALC.idCursoCicloLectivo = CCL.idCursoCicloLectivo
				INNER JOIN [Curso] AS CUR ON CUR.idCurso = CCL.idCurso
				INNER JOIN [CicloLectivo] AS CL ON CCL.idCicloLectivo = CL.idCicloLectivo
				INNER JOIN [MotivoSancion] AS MS ON S.idMotivoSancion  = MS.idMotivoSancion
				INNER JOIN [TipoSancion] AS TS ON S.idTipoSancion = TS.idTipoSancion
    WHERE 
		(@idAlumno IS NULL OR @idAlumno=ALU.idAlumno)
	    AND (@idCicloLectivo IS NULL OR @idCicloLectivo=CL.idCicloLectivo)
	    AND (@idCurso IS NULL OR @idCurso=CCL.idCursoCicloLectivo)
	    AND S.fecha BETWEEN @fechaInicio AND @fechaFin
		AND (@listaTiposSancion IS NULL OR S.idTipoSancion IN (SELECT * FROM [ArrayToTable] (@listaTiposSancion)))
		AND (@listaMotivoSancion IS NULL OR S.idMotivoSancion IN (SELECT * FROM [ArrayToTable] (@listaMotivoSancion)))	    
	GROUP BY PER.apellido+' '+PER.nombre, MS.descripcion, TS.nombre
END

if (@idPeriodo > 0)
BEGIN 
	SELECT 
		PER.apellido+' '+PER.nombre as 'Alumno'
		,PRD.nombre as 'Periodo'
		,SUM(S.cantidad) as 'Sanciones'
		, MS.descripcion as 'Motivo'
		, TS.nombre as 'Tipo'
    FROM 
		[Sancion] AS S 
			INNER JOIN [AlumnoCursoCicloLectivo] AS ALC ON S.idAlumnoCursoCicloLectivo = ALC.idAlumnoCursoCicloLectivo
			INNER JOIN [Alumnos] AS ALU ON ALC.idAlumno = ALU.idAlumno
			INNER JOIN [Personas] AS PER ON ALU.idPersona = PER.idPersona
     		INNER JOIN [CursosCicloLectivo] AS CCL ON ALC.idCursoCicloLectivo = CCL.idCursoCicloLectivo
			INNER JOIN [Curso] AS CUR ON CUR.idCurso = CCL.idCurso
			INNER JOIN [CicloLectivo] AS CL ON CCL.idCicloLectivo = CL.idCicloLectivo
			INNER JOIN [Periodo] AS PRD ON PRD.idCicloLectivo = CL.idCicloLectivo	
			INNER JOIN [MotivoSancion] AS MS ON S.idMotivoSancion  = MS.idMotivoSancion
			INNER JOIN [TipoSancion] AS TS ON S.idTipoSancion = TS.idTipoSancion
    WHERE 
		(@idAlumno IS NULL OR @idAlumno=ALU.idAlumno)
	    AND (@idCicloLectivo IS NULL OR @idCicloLectivo=CL.idCicloLectivo)
	    AND (@idPeriodo IS NULL OR @idPeriodo=PRD.idPeriodo)
	    AND (@idCurso IS NULL OR @idCurso=CCL.idCursoCicloLectivo)
	    AND S.fecha BETWEEN PRD.fechaInicio AND PRD.fechaFin
		AND (@listaTiposSancion IS NULL OR S.idTipoSancion IN (SELECT * FROM [ArrayToTable] (@listaTiposSancion)))
		AND (@listaMotivoSancion IS NULL OR S.idMotivoSancion IN (SELECT * FROM [ArrayToTable] (@listaMotivoSancion)))	    	    
	GROUP BY PER.apellido+' '+PER.nombre, PRD.nombre, MS.descripcion, TS.nombre
END

GO
/****** Object:  StoredProcedure [dbo].[Reporte_EncuestaAnalisisSumarizado]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[Reporte_EncuestaAnalisisSumarizado]

  @idEncuesta numeric = null
 ,@rolesParam VARCHAR(MAX) = NULL
 AS
DECLARE @idPregunta numeric = null
DECLARE @textoPregunta varchar(max) = null
DECLARE @idEscalaPonderacion numeric = null
DECLARE @peso numeric = null
DECLARE @totalPesos numeric = null

DECLARE @cant1 numeric = null
DECLARE @cant2 numeric = null
DECLARE @cant3 numeric = null
DECLARE @cant4 numeric = null
DECLARE @cant5 numeric = null

DECLARE @idx numeric = null

SELECT @totalPesos = SUM(Ps.peso) FROM Pregunta AS Ps WHERE Ps.idEncuesta = @idEncuesta

CREATE TABLE #TablaTemporal (idPregunta INT, textoPregunta VARCHAR(max), idEscalaPonderacion INT, relevancia numeric(18,2), cant1 INT, cant2 INT, cant3 INT, cant4 INT, cant5 INT )

DECLARE Cursor_Preguntas cursor for
SELECT 
	   P.idPregunta
	 , P.textoPregunta
	 , P.idEscalaPonderacion
	 , P.peso
FROM Encuesta AS E 
	INNER JOIN Pregunta AS P ON E.idEncuesta = P.idEncuesta AND E.idEncuesta = @idEncuesta
WHERE 
	P.idEscalaPonderacion <> 3
	
open Cursor_Preguntas

fetch next from Cursor_Preguntas into @idPregunta,@textoPregunta,@idEscalaPonderacion,@peso
  while @@fetch_status = 0
	begin
						
		SELECT
			@cant1 = COUNT(R.valorRespuestaSeleccion)
		from RespuestaPregunta as R
		where R.idPregunta = @idPregunta and R.valorRespuestaSeleccion = 1
		AND (@rolesParam IS NULL OR R.RoleName IN (SELECT * FROM [ArrayToTable] (@rolesParam)))

		SELECT
			@cant2 = COUNT(R.valorRespuestaSeleccion)
		from RespuestaPregunta as R
		where R.idPregunta = @idPregunta and R.valorRespuestaSeleccion = 2
		AND (@rolesParam IS NULL OR R.RoleName IN (SELECT * FROM [ArrayToTable] (@rolesParam)))

		SELECT
			@cant3 = COUNT(R.valorRespuestaSeleccion)
		from RespuestaPregunta as R
		where R.idPregunta = @idPregunta and R.valorRespuestaSeleccion = 3
		AND (@rolesParam IS NULL OR R.RoleName IN (SELECT * FROM [ArrayToTable] (@rolesParam)))

		SELECT
			@cant4 = COUNT(R.valorRespuestaSeleccion)
		from RespuestaPregunta as R
		where R.idPregunta = @idPregunta and R.valorRespuestaSeleccion = 4
		AND (@rolesParam IS NULL OR R.RoleName IN (SELECT * FROM [ArrayToTable] (@rolesParam)))

		SELECT
			@cant5 = COUNT(R.valorRespuestaSeleccion)
		from RespuestaPregunta as R
		where R.idPregunta = @idPregunta and R.valorRespuestaSeleccion = 5
		AND (@rolesParam IS NULL OR R.RoleName IN (SELECT * FROM [ArrayToTable] (@rolesParam)))

		insert into #TablaTemporal values (@idPregunta,@textoPregunta,@idEscalaPonderacion,@peso/@totalPesos * 100,@cant1,@cant2,@cant3,@cant4,@cant5)

fetch next from Cursor_Preguntas into @idPregunta,@textoPregunta,@idEscalaPonderacion,@peso
end
-- cerramos el cursor
close Cursor_Preguntas
DEALLOCATE Cursor_Preguntas

select * from  #TablaTemporal
ORDER BY 4 DESC

drop table  #TablaTemporal

GO
/****** Object:  StoredProcedure [dbo].[Reporte_EncuestaAnalisisSumarizado_BIS]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Reporte_EncuestaAnalisisSumarizado_BIS]
  @idEncuesta NUMERIC = NULL
 ,@rolesParam VARCHAR(MAX) = NULL
AS

DECLARE @idValorEscalaPonderacion NUMERIC = NULL
DECLARE @idPregunta NUMERIC =  NULL
DECLARE @textoPregunta VARCHAR(MAX) = NULL	
DECLARE @idEscalaPonderacion NUMERIC = NULL
DECLARE @peso NUMERIC = NULL
DECLARE @totalPesos NUMERIC = NULL
DECLARE @cantidad NUMERIC = NULL
DECLARE @cantValoresPosibles NUMERIC = NULL
DECLARE @contador INT = NULL
DECLARE @cadenaValoresSeleccionados VARCHAR(MAX) = NULL
DECLARE @cadenaAuxiliar VARCHAR(MAX) = NULL

SELECT @totalPesos = SUM(Ps.peso) FROM Pregunta AS Ps WHERE Ps.idEncuesta = @idEncuesta

CREATE TABLE #TablaTemporal (idPregunta INT, textoPregunta VARCHAR(MAX), idEscalaPonderacion INT, relevancia NUMERIC(18,2), cadenaValores varchar(max))

DECLARE Cursor_Preguntas CURSOR FOR
SELECT 
	   P.idPregunta
	 , P.textoPregunta
	 , P.idEscalaPonderacion
	 , P.peso
FROM Encuesta AS E 
	INNER JOIN Pregunta AS P ON E.idEncuesta = P.idEncuesta AND E.idEncuesta = @idEncuesta
WHERE 
	P.idEscalaPonderacion <> 3
	
OPEN Cursor_Preguntas

FETCH NEXT FROM Cursor_Preguntas into @idPregunta,@textoPregunta,@idEscalaPonderacion,@peso
  WHILE @@fetch_status = 0
	BEGIN
		--SET @contador = 0
		SET @cadenaValoresSeleccionados = ''
		
		BEGIN
     		DECLARE Valores_Pregunta CURSOR FOR

			select ValorEscalaPonderacion.idValorEscalaPonderacion
			from EscalaPonderacion 
				inner join Pregunta 
					on EscalaPonderacion.idEscalaPonderacion = Pregunta.idEscalaPonderacion
				inner join ValorEscalaPonderacion 
					on EscalaPonderacion.idEscalaPonderacion = ValorEscalaPonderacion.idEscalaPonderacion
				and idPregunta = @idPregunta
				--AND (@rolesParam IS NULL OR R.RoleName IN (SELECT * FROM [ArrayToTable] (@rolesParam)))
			
			open Valores_Pregunta
			FETCH NEXT FROM Valores_Pregunta into @idValorEscalaPonderacion
			WHILE @@fetch_status = 0	
				begin
				SELECT
					@cantidad = isnull(COUNT(R.idRespuesta),0)
				FROM RespuestaPregunta AS R
					INNER JOIN Encuesta AS E ON E.idEncuesta = R.idEncuesta
					INNER JOIN Pregunta AS P ON P.idPregunta=R.idPregunta
				WHERE R.idPregunta = @idPregunta 
				AND R.valorRespuestaSeleccion = @idValorEscalaPonderacion
				AND (@rolesParam IS NULL OR R.RoleName IN (SELECT * FROM [ArrayToTable] (@rolesParam)))
				
				SET @cadenaAuxiliar = concat('[',cast(@idValorEscalaPonderacion as varchar) + '-' + cast(@cantidad as varchar),']')
	    		SET @cadenaValoresSeleccionados = concat(@cadenaValoresSeleccionados,@cadenaAuxiliar)
			
			FETCH NEXT FROM Valores_Pregunta INTO @idValorEscalaPonderacion
			
			END
			-- cerramos el cursor
			CLOSE Valores_Pregunta
			DEALLOCATE Valores_Pregunta

		END

		INSERT INTO #TablaTemporal VALUES (@idPregunta,@textoPregunta,@idEscalaPonderacion,@peso/@totalPesos * 100,@cadenaValoresSeleccionados)

	FETCH NEXT FROM Cursor_Preguntas INTO @idPregunta,@textoPregunta,@idEscalaPonderacion,@peso
END
-- cerramos el cursor
CLOSE Cursor_Preguntas
DEALLOCATE Cursor_Preguntas

SELECT * FROM #TablaTemporal
ORDER BY 4 DESC

DROP TABLE #TablaTemporal
GO
/****** Object:  StoredProcedure [dbo].[Reporte_EncuestaAnalisisTextuales]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[Reporte_EncuestaAnalisisTextuales]

   @idEncuesta numeric = null
  ,@rolesParam VARCHAR(MAX) = NULL
 AS

--DECLARE  @idEncuesta numeric = null
--DECLARE  @rolesParam VARCHAR(MAX) = NULL

--set @idEncuesta = 39


DECLARE @idPregunta numeric = null
DECLARE @textoPregunta varchar(max) = null
DECLARE @peso numeric = null
DECLARE @totalPesos numeric = null

DECLARE @respuestasEsperadas numeric = 0
DECLARE @respuestasObtenidas numeric = 0
DECLARE @porcentaje numeric (5,2) = 0
DECLARE @relevancia numeric (5,2) = 0

DECLARE @idx numeric = null

SELECT @totalPesos = SUM(Ps.peso) FROM Pregunta AS Ps WHERE Ps.idEncuesta = @idEncuesta

CREATE TABLE #TablaTemporalTextual (idPregunta INT, textoPregunta VARCHAR(max), respuestasEsperadas INT, respuestasObtenidas INT, porcentaje numeric (5,2), relevancia numeric (5,2) )

DECLARE Cursor_Preguntas cursor for
SELECT 
	   P.idPregunta
	 , P.textoPregunta
	 , P.peso
FROM Encuesta AS E 
	INNER JOIN Pregunta AS P ON E.idEncuesta = P.idEncuesta AND E.idEncuesta = @idEncuesta
WHERE 
	P.idEscalaPonderacion = 3

open Cursor_Preguntas

fetch next from Cursor_Preguntas into @idPregunta,@textoPregunta,@peso
  while @@fetch_status = 0
	begin
						
		SELECT
			@respuestasEsperadas = SUM(R.Cantidad)
		from vw_AnalisisRespuestasTextuales as R
		where R.idEncuesta = @idEncuesta and R.Status = 'Encuestas Respondidas'
			AND (@rolesParam IS NULL OR R.RoleName IN (SELECT * FROM [ArrayToTable] (@rolesParam)))
		
		SELECT
			@respuestasObtenidas = COUNT(R.Respuesta)
		from vw_respuestas_encuesta as R
		where R.idPregunta = @idPregunta and R.idEncuesta = @idEncuesta
			AND (@rolesParam IS NULL OR R.RoleName IN (SELECT * FROM [ArrayToTable] (@rolesParam)))
		
		insert into #TablaTemporalTextual values (@idPregunta,@textoPregunta,@respuestasEsperadas,@respuestasObtenidas,@respuestasObtenidas/@respuestasEsperadas * 100,@peso/@totalPesos * 100)

fetch next from Cursor_Preguntas into @idPregunta,@textoPregunta,@peso
end
-- cerramos el cursor
close Cursor_Preguntas
DEALLOCATE Cursor_Preguntas

select * from  #TablaTemporalTextual

drop table  #TablaTemporalTextual

GO
/****** Object:  StoredProcedure [dbo].[Reporte_EncuestasPorStatus]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 13/11/12
-- Parameters:
-- idCursoCicloLectivo
-- idAsignaturaCicloLectivo
-- listaRoles
-- =============================================
CREATE PROCEDURE [dbo].[Reporte_EncuestasPorStatus]
  @idEncuesta NUMERIC = NULL
 ,@idCursoCicloLectivo NUMERIC = NULL
 ,@idAsignaturaCicloLectivo NUMERIC = NULL
 ,@listaRoles VARCHAR(MAX) = NULL

AS
 SET NOCOUNT OFF;

IF(@listaRoles IS NOT NULL)
BEGIN
	SELECT SUM(V.Cantidad) AS 'Total', V.Status, V.idEncuesta, V.Rol
	FROM vw_StatusEncuestas AS V 
	INNER JOIN AmbitoEncuesta AS AE ON V.idAmbito=AE.idAmbito
	WHERE (@idCursoCicloLectivo IS NULL OR @idCursoCicloLectivo=V.idCursoCicloLectivo)
		AND (@idAsignaturaCicloLectivo IS NULL OR @idAsignaturaCicloLectivo=V.idAsignaturaCicloLectivo)
		AND (@idEncuesta IS NULL OR @idEncuesta=V.idEncuesta)
	  AND (V.Rol IN (SELECT * FROM [ArrayToTable] (@listaRoles)))
	GROUP BY V.Status, V.idEncuesta, V.Rol
END
ELSE
BEGIN
	SELECT SUM(Cantidad) AS 'Total', V.Status, V.idEncuesta
	FROM vw_StatusEncuestas AS V
	INNER JOIN AmbitoEncuesta AS AE ON V.idAmbito=AE.idAmbito
	WHERE (@idCursoCicloLectivo IS NULL OR @idCursoCicloLectivo=idCursoCicloLectivo)
		AND (@idAsignaturaCicloLectivo IS NULL OR @idAsignaturaCicloLectivo=idAsignaturaCicloLectivo)
		AND (@idEncuesta IS NULL OR @idEncuesta=idEncuesta)
	GROUP BY V.Status, V.idEncuesta
END
GO
/****** Object:  StoredProcedure [dbo].[Reporte_InasistenciasPeriodoAlumno]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 13/07/11
-- Parameters:
-- idAlumno: Alumno
-- periodo: Año 
-- =============================================
CREATE PROCEDURE [dbo].[Reporte_InasistenciasPeriodoAlumno]
  @idAlumno NUMERIC = NULL
 ,@idCurso NUMERIC = NULL
 ,@idCicloLectivo NUMERIC = NULL
 ,@idPeriodo NUMERIC = NULL 
 ,@fechaDesde DATETIME = NULL
 ,@fechaHasta DATETIME = NULL
 ,@username VARCHAR(50) = NULL
 ,@usernamePreceptor VARCHAR(50) = NULL
 ,@listaTiposAsistencia VARCHAR(MAX) = NULL
AS
 SET NOCOUNT OFF;

SELECT DISTINCT 
	per.apellido+' '+per.nombre as 'Nombre'
	,Niv.nombre+ ' ' + cur.nombre as 'Curso'
    ,A.fecha as 'Fecha'
	,ta.descripcion as 'Descripcion'
FROM 
    [Asistencia] AS A 
	INNER JOIN [AlumnoCursoCicloLectivo] AS ALC ON A.idAlumnoCursoCicloLectivo = ALC.idAlumnoCursoCicloLectivo
	INNER JOIN [TipoAsistencia] AS TA ON TA.idTipoAsistencia = A.idTipoAsistencia
	INNER JOIN [Alumnos] AS ALU ON ALC.idAlumno = ALU.idAlumno
	INNER JOIN [Personas] AS PER ON ALU.idPersona = PER.idPersona
	INNER JOIN [CursosCicloLectivo] AS CCL ON ALC.idCursoCicloLectivo = CCL.idCursoCicloLectivo
	INNER JOIN [CicloLectivo] AS CL ON CCL.idCicloLectivo = CL.idCicloLectivo
	INNER JOIN [Curso] AS CUR ON CUR.idCurso = CCL.idCurso
	INNER JOIN [Periodo] AS PRD ON PRD.idCicloLectivo = CL.idCicloLectivo
	INNER JOIN [Nivel] AS NIV ON CUR.idNivel = NIV.idNivel
	INNER JOIN Personal AS PeP ON CCL.idPreceptor = PeP.idPersonal
	INNER JOIN Personas AS PPe ON PeP.idPersona = PPe.idPersona
WHERE 
	(@idAlumno IS NULL OR alu.idAlumno=@idAlumno)
	AND (@idCurso IS NULL OR CCL.idCursoCicloLectivo=@idCurso)
	AND (@idCicloLectivo IS NULL OR @idCicloLectivo=cl.idCicloLectivo)
	AND (@idPeriodo IS NULL OR @idPeriodo=PRD.idPeriodo)
    AND (@fechaDesde IS NULL OR A.fecha >= @fechaDesde)
    AND (@fechaHasta IS NULL OR A.fecha <= @fechaHasta)
    AND (@listaTiposAsistencia IS NULL OR a.idTipoAsistencia IN (SELECT * FROM [ArrayToTable] (@listaTiposAsistencia)))
	AND (@usernamePreceptor IS NULL OR PPe.username = @usernamePreceptor)
    AND (@username IS NULL OR Alu.idAlumno in 
		(
			SELECT distinct ALU.idAlumno
			FROM 
				[AlumnoCursoCicloLectivo] AS ALC 
					INNER JOIN CursosCicloLectivo AS CCL ON ALC.idCursoCicloLectivo = CCL.idCursoCicloLectivo
					INNER JOIN [AsignaturaCicloLectivo] AS AC ON ALC.idCursoCicloLectivo = AC.idCursoCicloLectivo
					INNER JOIN [Asignatura] AS A ON AC.idAsignatura = A.idAsignatura
					INNER JOIN [Alumnos] AS ALU ON ALC.idAlumno = ALU.idAlumno
					INNER JOIN [CicloLectivo] AS CL ON CCL.idCicloLectivo = CL.idCicloLectivo
					INNER JOIN Curso AS CUR ON CCL.idCurso = CUR.idCurso
					INNER JOIN Personal AS PD ON AC.idDocente = PD.idPersonal
					INNER JOIN Personas AS PP ON PD.idPersona = PP.idPersona
					
			WHERE pp.username = @username
		)
    )
   order by A.fecha asc


GO
/****** Object:  StoredProcedure [dbo].[Reporte_Indicadores]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Reporte_Indicadores] 
@idCursoCicoloLectivo INT = NULL

AS

SELECT DISTINCT
	 PER.apellido,
	 PER.nombre,
	 --PER.idPersona,
	 ALU.idAlumno,
	(select round(AVG(cast(VES.valor as float)),2,2) 
	FROM	[AlumnoCursoCicloLectivo] AS ALC 
		INNER JOIN [CursosCicloLectivo] AS CCL ON ALC.idCursoCicloLectivo = CCL.idCursoCicloLectivo
		INNER JOIN [AsignaturaCicloLectivo] AS AC ON CCL.idCursoCicloLectivo = AC.idCursoCicloLectivo
		INNER JOIN [Calificacion] AS CAL ON CAL.idAlumnoCursoCicloLectivo = ALC.idAlumnoCursoCicloLectivo and cal.idAsignaturaCicloLectivo=ac.idAsignatura
		INNER JOIN [Alumnos] AS ALU ON ALC.idAlumno = ALU.idAlumno
		INNER JOIN [ValoresEscalaCalificacion] AS VES ON CAL.idValorCalificacion = VES.idValorEscalaCalificacion
		where alu.idPersona = per.idPersona
		group by alu.idPersona
		) as 'Promedio',
		ISNULL((SELECT
			SUM(TA.valor)
	    FROM 
			[Asistencia] AS A 
			INNER JOIN [TipoAsistencia] AS TA ON TA.idTipoAsistencia = A.idTipoAsistencia
			INNER JOIN [AlumnoCursoCicloLectivo] AS ACCL ON A.idAlumnoCursoCicloLectivo = ACCL.idAlumnoCursoCicloLectivo
			INNER JOIN [Alumnos] AS ALU ON ACCL.idAlumno = ALU.idAlumno
		WHERE ALU.idPersona = PER.idPersona
		GROUP BY ALU.idPersona
		),0) as 'Inasistencias',
		ISNULL((SELECT 
			SUM(S.cantidad)
	    FROM 
			[Sancion] AS S 
				INNER JOIN [AlumnoCursoCicloLectivo] AS ALC ON S.idAlumnoCursoCicloLectivo = ALC.idAlumnoCursoCicloLectivo
				INNER JOIN [Alumnos] AS ALU ON ALC.idAlumno = ALU.idAlumno
		WHERE alu.idPersona = per.idPersona
		-- SOLO TENGO EN CUENTA AMONESTACIONES
		AND S.idTipoSancion = 4  
		GROUP BY alu.idPersona		
		),0) as 'Sanciones'
	FROM	
		[Alumnos] AS ALU 
		INNER JOIN [Personas] AS PER ON ALU.idPersona = PER.idPersona
		INNER JOIN [AlumnoCursoCicloLectivo] AS ACCL ON ALU.idAlumno = ACCL.idAlumno
		INNER JOIN [CursosCicloLectivo] AS CCL ON ACCL.idCursoCicloLectivo = CCL.idCursoCicloLectivo
	WHERE @idCursoCicoloLectivo IS NULL OR @idCursoCicoloLectivo = CCL.idCursoCicloLectivo
	ORDER BY ALU.idAlumno


GO
/****** Object:  StoredProcedure [dbo].[Reporte_PromedioCalificacionesPeriodo]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 14/07/11
-- Parameters:
-- idAlumno: Alumno
-- periodo: Año 
-- =============================================
CREATE PROCEDURE [dbo].[Reporte_PromedioCalificacionesPeriodo]
	 @idAlumno NUMERIC = NULL
	,@idAsignatura NUMERIC = NULL
	,@idCicloLectivo NUMERIC = NULL
	,@idPeriodo NUMERIC = 0
	,@idCurso NUMERIC = NULL
	,@listaAsignaturas VARCHAR(MAX) = NULL
AS
 SET NOCOUNT OFF;

SELECT DISTINCT
	 PER.apellido+' '+PER.nombre as 'Alumno'
	,A.nombre as 'Asignatura'
	,
	 CASE @idPeriodo
	 WHEN 0 THEN 'Ciclo Lectivo '+cast(datepart(year,CL.fechaInicio) as varchar) 
	 ELSE P.nombre 
	 END	
	 AS 'Periodo'
	,round(AVG(cast(VES.nombre as float)),2,2) as 'Promedio'
   FROM	[AlumnoCursoCicloLectivo] AS ALC 
			INNER JOIN [CursosCicloLectivo] AS CCL ON ALC.idCursoCicloLectivo = CCL.idCursoCicloLectivo
			INNER JOIN [AsignaturaCicloLectivo] AS AC ON CCL.idCursoCicloLectivo = AC.idCursoCicloLectivo
			INNER JOIN [Calificacion] AS CAL ON CAL.idAlumnoCursoCicloLectivo = ALC.idAlumnoCursoCicloLectivo and cal.idAsignaturaCicloLectivo=ac.idAsignatura
			INNER JOIN [Asignatura] AS A ON AC.idAsignatura = a.idAsignatura
			INNER JOIN [Alumnos] AS ALU ON ALC.idAlumno = ALU.idAlumno
			INNER JOIN [Personas] AS PER ON ALU.idPersona = PER.idPersona
			INNER JOIN [ValoresEscalaCalificacion] AS VES ON CAL.idValorCalificacion = VES.idValorEscalaCalificacion
			INNER JOIN [CicloLectivo] AS CL ON CCL.idCicloLectivo = CL.idCicloLectivo
			INNER JOIN [Curso] AS CUR ON CCL.idCurso = CUR.idCurso
			INNER JOIN [Nivel] as N on CUR.idNivel = N.idNivel
			INNER JOIN [Periodo] AS P ON CAL.idPeriodo = P.idPeriodo 
			INNER JOIN [Personal] AS PD ON AC.idDocente = PD.idPersonal
			INNER JOIN [Personas] AS PP ON PD.idPersona = PP.idPersona
    WHERE 
			(@idAlumno IS NULL OR @idAlumno=ALU.idAlumno)
		--AND (@idAsignatura IS NULL OR @idAsignatura=AC.idAsignaturaCicloLectivo)
	    AND (@idCicloLectivo IS NULL OR @idCicloLectivo=CCL.idCicloLectivo)
	    AND (@idCurso IS NULL OR @idCurso=CCL.idCursoCicloLectivo)
	    AND (@idPeriodo = 0 OR @idPeriodo= P.idPeriodo)
		AND year(CAL.fecha) = year(CL.fechaInicio)
		AND (@listaAsignaturas IS NULL OR AC.idAsignaturaCicloLectivo IN (SELECT * FROM [ArrayToTable] (@listaAsignaturas)))
	GROUP BY PER.apellido+' '+PER.nombre, A.nombre,
		CASE @idPeriodo
		 WHEN 0 THEN 'Ciclo Lectivo '+cast(datepart(year,CL.fechaInicio) as varchar) 
		 ELSE P.nombre 
		 END

GO
/****** Object:  StoredProcedure [dbo].[Reporte_PromedioHistoricoCalificacionesAlumno]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 14/07/11
-- Parameters:
-- idAlumno: Alumno
-- periodo: Año 
-- =============================================
CREATE PROCEDURE [dbo].[Reporte_PromedioHistoricoCalificacionesAlumno]
	 @idAlumno NUMERIC = NULL
	,@idAsignatura NUMERIC = NULL
	,@idCurso NUMERIC = NULL
	,@idCicloLectivo NUMERIC = NULL
	,@idNivel NUMERIC = NULL
AS
 SET NOCOUNT OFF;
 
	SELECT 
	 PA.apellido+' '+PA.nombre as 'alumno'
	,ASI.nombre as 'asignatura'
	,CL.nombre as 'periodo'
	,round(AVG(cast(VES.nombre as float)),2,2) as 'promedio'
	,CCL.idCurso
	,CCL.idCicloLectivo
	,CUR.idNivel
    FROM Calificacion as C 
		INNER JOIN ValoresEscalaCalificacion as VES on C.idValorCalificacion = VES.idValorEscalaCalificacion
		INNER JOIN Alumnos as A on C.idAlumno = A.idAlumno
		INNER JOIN Personas as PA on A.idPersona = PA.idPersona
		INNER JOIN AsignaturaCurso AS AC ON C.idAsignaturaCurso = AC.idAsignaturaCurso
		INNER JOIN Asignatura AS ASI ON AC.idAsignatura = ASI.idAsignatura
		INNER JOIN CursosCicloLectivo AS CCL ON AC.idCurso = CCL.idCurso AND AC.idCicloLectivo = CCL.idCicloLectivo
		INNER JOIN CicloLectivo AS CL ON CL.idCicloLectivo=AC.idCicloLectivo
		INNER JOIN AlumnoCursoCicloLectivo AS ACCL ON ACCL.idAlumno=A.idAlumno AND ACCL.idCursoCicloLectivo=CCL.idCursoCicloLectivo
		INNER JOIN Periodo AS P ON P.idPeriodo=C.idPeriodo AND P.idCicloLectivo=CL.idCicloLectivo 		
		INNER JOIN Curso AS CUR ON CCL.idCurso = CUR.idCurso AND CUR.idCurso=AC.idCurso
    WHERE 
			(@idAlumno IS NULL OR @idAlumno=A.idAlumno)
		AND (@idAsignatura IS NULL OR @idAsignatura=AC.idAsignatura)
		--AND (@idAsignatura IS NULL OR @idAsignatura=AC.idAsignaturaCurso)
		AND (@idCicloLectivo IS NULL OR @idCicloLectivo=CL.idCicloLectivo)
		AND (@idCurso IS NULL OR @idCurso=CUR.idCurso)
		AND (@idNivel IS NULL OR @idNivel=CUR.idNivel)
	    --AND VES.idEscalaCalificacion = 1
	GROUP BY CL.nombre,PA.apellido+' '+PA.nombre, ASI.nombre,CCL.idCurso,CCL.idCicloLectivo,CUR.idNivel
	ORDER BY CL.nombre,PA.apellido+' '+PA.nombre, ASI.nombre
	
GO
/****** Object:  StoredProcedure [dbo].[Reporte_SancionesPeriodoAlumno]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 13/07/11
-- Parameters:
-- idAlumno: Alumno
-- periodo: Año 
-- =============================================
CREATE PROCEDURE [dbo].[Reporte_SancionesPeriodoAlumno]
  @idAlumno NUMERIC = NULL
 ,@fechaDesde DATE = NULL
 ,@fechaHasta DATE = NULL
 ,@idCurso NUMERIC = NULL
 ,@idCicloLectivo NUMERIC = NULL
 ,@username VARCHAR(50) = NULL
 ,@listaTiposSancion VARCHAR(MAX) = NULL
 ,@listaMotivoSancion VARCHAR(MAX) = NULL
AS
 SET NOCOUNT OFF;

SELECT DISTINCT per.apellido+' '+per.nombre as 'nombre'
	,san.fecha
	,san.cantidad
	,ts.nombre as 'tipo'
	,ms.descripcion as 'motivo'
	
FROM Sancion san
	INNER JOIN TipoSancion AS ts ON ts.idTipoSancion = san.idTipoSancion 
	INNER JOIN MotivoSancion AS ms ON ms.idMotivoSancion = san.idMotivoSancion
	INNER JOIN AlumnoCursoCicloLectivo AS AC ON AC.idAlumnoCursoCicloLectivo = san.idAlumnoCursoCicloLectivo
	INNER JOIN Alumnos AS alu ON alu.idAlumno = AC.idAlumno
	INNER JOIN Personas AS per ON alu.idPersona = per.idPersona
    INNER JOIN CursosCicloLectivo AS CCL ON AC.idCursoCicloLectivo = CCL.idCursoCicloLectivo
    INNER JOIN CicloLectivo AS CL ON cl.idCicloLectivo = ccl.idCicloLectivo
    INNER JOIN Curso AS Cur ON cur.idCurso = CCL.idCurso
WHERE 
   (@idAlumno IS NULL OR alu.idAlumno = @idAlumno)
   AND (@fechaDesde IS NULL OR san.fecha >= @fechaDesde)
   AND (@fechaHasta IS NULL OR san.fecha <= @fechaHasta)
   AND (@idCurso IS NULL OR ccl.idCursoCicloLectivo = @idCurso)
   AND (@idCicloLectivo IS NULL OR cl.idCicloLectivo = @idCicloLectivo)
   
   AND (@listaTiposSancion IS NULL OR SAN.idTipoSancion IN (SELECT * FROM [ArrayToTable] (@listaTiposSancion)))
   AND (@listaMotivoSancion IS NULL OR SAN.idMotivoSancion IN (SELECT * FROM [ArrayToTable] (@listaMotivoSancion)))
   AND (@username IS NULL OR alu.idAlumno in 
    (
	SELECT ALU.idAlumno
    FROM 
		[AlumnoCursoCicloLectivo] AS ALC 
			INNER JOIN CursosCicloLectivo AS CCL 
				ON ALC.idCursoCicloLectivo = CCL.idCursoCicloLectivo
			INNER JOIN [AsignaturaCicloLectivo] AS AC 
				ON CCL.idCursoCicloLectivo = AC.idCursoCicloLectivo
			INNER JOIN [Asignatura] AS A 
				ON AC.idAsignatura = A.idAsignatura
			INNER JOIN [Alumnos] AS ALU ON ALC.idAlumno = ALU.idAlumno
			INNER JOIN [CicloLectivo] AS CL ON CCL.idCicloLectivo = CL.idCicloLectivo
			INNER JOIN Curso AS CUR ON CCL.idCurso = CUR.idCurso
			INNER JOIN Personal AS PD ON AC.idDocente = PD.idPersonal
			INNER JOIN Personas AS PP ON PD.idPersona = PP.idPersona
			INNER JOIN Personal AS PeP ON CCL.idPreceptor = PeP.idPersonal
			INNER JOIN Personas AS PPe ON PeP.idPersona = PPe.idPersona
	WHERE pp.username = @username OR PPe.username = @username
    )
    )

GO
/****** Object:  StoredProcedure [dbo].[RespuestaPregunta_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 07/10/12
-- =============================================
CREATE PROCEDURE [dbo].[RespuestaPregunta_Insert]
  @idRespuesta NUMERIC = NULL OUT
 ,@idPregunta NUMERIC = NULL
 ,@username VARCHAR(50) = NULL
 ,@idEncuesta NUMERIC = NULL
 ,@valorRespuestaSeleccion NUMERIC = NULL
 ,@valorRespuestaTextual VARCHAR(4000) = NULL

AS
 SET NOCOUNT OFF;

DECLARE @rolename VARCHAR(50) = NULL
SELECT @rolename = rolename
FROM vw_PersonaUsuarioRol
WHERE username = @username

	INSERT INTO RespuestaPregunta(
		idPregunta
		,idEncuesta
		,valorRespuestaSeleccion
		,valorRespuestaTextual
		,rolename
	 )
	 VALUES(
		@idPregunta
		,@idEncuesta
		,@valorRespuestaSeleccion
		,@valorRespuestaTextual
		,@rolename
	 )
SET @idRespuesta = @@IDENTITY
GO
/****** Object:  StoredProcedure [dbo].[RespuestasEncuesta_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 07/10/12
-- =============================================
CREATE PROCEDURE [dbo].[RespuestasEncuesta_Select]
  @idRespuesta NUMERIC = NULL
 ,@idPregunta NUMERIC = NULL
 ,@username VARCHAR(50) = NULL
 --,@idRespuestaIndividual NUMERIC = NULL
 ,@idEncuesta NUMERIC = NULL
 ,@valorRespuestaSeleccion NUMERIC = NULL
 ,@valorRespuestaTextual VARCHAR(4000) = NULL
AS
 SET NOCOUNT OFF;

    SELECT 
	   idRespuesta,
	   idPregunta,
	   idEncuesta,
	   valorRespuestaSeleccion,
	   valorRespuestaTextual
    FROM RespuestaPregunta
    WHERE (@idRespuesta IS NULL OR @idRespuesta=idRespuesta)
		AND (@idPregunta IS NULL OR @idPregunta=idPregunta)
		AND (@idEncuesta IS NULL OR @idEncuesta=idEncuesta)

GO
/****** Object:  StoredProcedure [dbo].[RespuestasTextuales_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 26/11/12
-- =============================================
CREATE PROCEDURE [dbo].[RespuestasTextuales_Select]
  @idEncuesta NUMERIC = NULL
 ,@idPregunta NUMERIC = NULL
 ,@rolesParam VARCHAR(MAX) = NULL 
AS
 SET NOCOUNT OFF;

    SELECT 
	   idPregunta,
	   idEncuesta,
	   valorRespuestaTextual
    FROM vw_respuestasTextuales
    WHERE (@idEncuesta IS NULL OR @idEncuesta=idEncuesta)
		AND (@idPregunta IS NULL OR @idPregunta=idPregunta)
		AND (@rolesParam IS NULL OR roleName IN (SELECT * FROM [ArrayToTable] (@rolesParam)))

GO
/****** Object:  StoredProcedure [dbo].[Reunion_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 01/08/11
-- =============================================
CREATE PROCEDURE [dbo].[Reunion_Insert]
  @idReunion NUMERIC = NULL
 ,@horario TIME = NULL
 ,@idEventoAgenda NUMERIC = NULL
 ,@idAgendaActividades NUMERIC = NULL
 ,@username VARCHAR(50) = NULL
 ,@idTipoEvento NUMERIC = NULL
 ,@fechaModificacion DATE = NULL
 ,@fechaEvento DATE = NULL
 ,@descripcion VARCHAR(4000) = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;

Declare @idEvento numeric
EXECUTE @idEvento = [EventoAgenda_Insert] 
   @idEventoAgenda
  ,@idAgendaActividades
  ,@username
  ,@idTipoEvento
  ,@fechaModificacion
  ,@fechaEvento
  ,@descripcion
  ,@activo
  
    INSERT INTO Reunion(
    idEvento
    ,horario
 )
 VALUES(
    @idEvento
    ,@horario
 )
 SET @idReunion = @@IDENTITY

GO
/****** Object:  StoredProcedure [dbo].[Reunion_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 14/08/11
-- =============================================
CREATE PROCEDURE [dbo].[Reunion_Select]
  @idReunion NUMERIC = NULL
 ,@idEvento NUMERIC = NULL
 ,@horario TIME = NULL
 ,@idAgendaActividad NUMERIC = NULL
 ,@fechaEvento DATE = NULL
 ,@fechaDesde DATE = NULL
 ,@fechaHasta DATE = NULL
 ,@activo BIT = NULL
 
AS
 SET NOCOUNT OFF;

SELECT 
     TP.descripcion AS 'tipoEvento'
    ,EA.activo
    ,EA.descripcion
    ,EA.fechaAlta
    ,EA.fechaModificacion
    ,EA.fechaEvento
    ,EA.idAgendaActividades
    ,EA.idEventoAgenda
    ,EA.idOrganizador
    ,EA.idTipoEvento
    ,R.idReunion
    ,R.horario
    ,P.nombre
    ,P.apellido
    ,P.username AS 'usernameOrganizador'
    FROM EventoAgenda AS EA 
		INNER JOIN TipoEvento AS TP ON EA.idTipoEvento = TP.idTipoEvento
		INNER JOIN Reunion AS R ON EA.idEventoAgenda = R.idEvento
		INNER JOIN Personas AS P ON EA.idOrganizador = P.idPersona
    WHERE 
		(@idEvento IS NULL OR @idEvento <> R.idEvento)
	AND (@fechaEvento IS NULL OR EA.fechaEvento = @fechaEvento)
	--AND (@fechaDesde IS NULL OR @fechaDesde >= EA.fechaEvento)
	--AND (@fechaHasta IS NULL OR @fechaHasta <= EA.fechaEvento)
	AND (@fechaDesde IS NULL OR (EA.fechaEvento BETWEEN @fechaDesde AND @fechaHasta))
	AND (@idAgendaActividad IS NULL OR @idAgendaActividad = EA.idAgendaActividades)
	AND (@activo IS NULL OR @activo = EA.activo)
	AND (@horario IS NULL OR @horario=R.horario)
	AND (@idReunion IS NULL OR @idReunion=R.idReunion)

GO
/****** Object:  StoredProcedure [dbo].[Reunion_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 18/08/11
-- =============================================
CREATE PROCEDURE [dbo].[Reunion_Update]
@idAgendaActividades NUMERIC = NULL
 ,@username VARCHAR(50) = NULL
 ,@idTipoEvento NUMERIC = NULL
 ,@fechaAlta DATE = NULL
 ,@fechaModificacion DATE = NULL
 ,@fechaEvento DATE = NULL
 ,@descripcion VARCHAR(4000) = NULL
 ,@activo BIT = NULL 
 ,@idReunion NUMERIC = NULL
 ,@idEventoAgenda NUMERIC = NULL
 ,@horario TIME = NULL
AS
 SET NOCOUNT OFF;

DECLARE @idOrganizador NUMERIC = NULL

SELECT @idOrganizador = idPersona
FROM Personas WHERE username = @username

	EXECUTE EventoAgenda_Update 
	 @idEventoAgenda
   ,@idAgendaActividades 
	 ,@idOrganizador 
	 ,@idTipoEvento 
	 ,@fechaModificacion 
	 ,@fechaEvento 
	 ,@descripcion 
	 ,@activo

    UPDATE Reunion SET 
     horario = @horario
    WHERE @idReunion=idReunion


GO
/****** Object:  StoredProcedure [dbo].[ReunionValidarDisponibilidad_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo
-- Create date: 25/08/11
-- =============================================
CREATE PROCEDURE [dbo].[ReunionValidarDisponibilidad_Select]
  @fecha DATE = NULL
 ,@idEventoAgenda NUMERIC = NULL
AS
 SET NOCOUNT OFF;
	
	SELECT 
		 R.idReunion
		,EA.fechaEvento
		,R.horario
		,EA.idOrganizador
		,EA.descripcion
		,EA.activo
    FROM Reunion AS R 
		INNER JOIN EventoAgenda AS EA ON R.idEvento = EA.idEventoAgenda
		INNER JOIN TipoEvento AS TP ON EA.idTipoEvento = TP.idTipoEvento
    WHERE 
			(@fecha IS NULL OR @fecha=EA.fechaEvento)
		AND	(@idEventoAgenda IS NULL OR @idEventoAgenda <> EA.idEventoAgenda)
		AND	(EA.activo = 1)
		--AND (TP.descripcion='Reunión')

GO
/****** Object:  StoredProcedure [dbo].[Sancion_Delete]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 02/07/11
-- =============================================
CREATE PROCEDURE [dbo].[Sancion_Delete]
  @idSancion INT = NULL
 ,@idSancionTransaccional INT = NULL
 ,@cantidad INT = NULL
 ,@fecha DATE = NULL
 ,@idMotivoSancion INT = NULL
 ,@idTipoSancion INT = NULL
AS
 SET NOCOUNT OFF;

    DELETE FROM Sancion
    WHERE (@idSancion IS NULL OR @idSancion=idSancion)
   
-- =====UPDATE==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[Sancion_Exists]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 02/07/11
-- =============================================
CREATE PROCEDURE [dbo].[Sancion_Exists]
  @idSancion INT = NULL
 ,@idSancionTransaccional INT = NULL
 ,@cantidad INT = NULL
 ,@fecha DATE = NULL
 ,@idMotivoSancion INT = NULL
 ,@idTipoSancion INT = NULL
 ,@exists BIT OUT
AS
 SET NOCOUNT OFF;

    IF EXISTS (
 SELECT idSancion
    FROM Sancion
    WHERE (@idSancion IS NULL OR @idSancion=idSancion)
   )
 SET @exists = 1
 ELSE SET @exists = 0

GO
/****** Object:  StoredProcedure [dbo].[Sancion_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 02/07/11
-- =============================================
CREATE PROCEDURE [dbo].[Sancion_Insert]
  @idSancion INT = NULL
 ,@idSancionTransaccional INT = NULL
 ,@cantidad INT = NULL
 ,@fecha DATE = NULL
 ,@idMotivoSancion INT = NULL
 ,@idTipoSancion INT = NULL
 ,@idAlumno INT = NULL
AS
 SET NOCOUNT OFF;
 
 select @idAlumno=ac.idAlumnoCursoCicloLectivo
 from AlumnoCursoCicloLectivo as AC inner join Alumnos A on Ac.idAlumno = A.idAlumno
 where Ac.idAlumnoCursoCicloLectivoTransaccional=@idAlumno
 
SELECT @idMotivoSancion = idMotivoSancion
FROM MotivoSancion
WHERE idMotivoSancionTransaccional=@idMotivoSancion

SELECT @idTipoSancion = idTipoSancion
FROM TipoSancion
WHERE idTipoSancionTransaccional = @idTipoSancion

SELECT @idSancion = idSancion
FROM Sancion
WHERE idSancionTransaccional = @idSancionTransaccional

IF(@idSancion is NULL OR @idSancion = 0)
	BEGIN
		INSERT INTO Sancion(
		 idSancionTransaccional
		,cantidad
		,fecha
		,idMotivoSancion
		,idTipoSancion
		,idAlumnoCursoCicloLectivo
		,fechaCarga
		 )
		VALUES(
		 @idSancionTransaccional
		,@cantidad
		,@fecha
		,@idMotivoSancion
		,@idTipoSancion
		,@idAlumno
		,GETDATE()
		 )
		SET  @idSancion = @@IDENTITY
	END
 ELSE 
	BEGIN
		 EXECUTE [Sancion_Update]
			 @idSancion
			,@idSancionTransaccional
			,@cantidad
			,@fecha
			,@idMotivoSancion
			,@idTipoSancion
			,@idAlumno
	END

GO
/****** Object:  StoredProcedure [dbo].[Sancion_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 02/07/11
-- =============================================
CREATE PROCEDURE [dbo].[Sancion_Select]
  @idSancion INT = NULL
 ,@idSancionTransaccional INT = NULL
 ,@cantidad INT = NULL
 ,@fecha DATE = NULL
 ,@idMotivoSancion INT = NULL
 ,@idTipoSancion INT = NULL
AS
 SET NOCOUNT OFF;

    SELECT idSancion
    ,idSancionTransaccional
    ,cantidad
    ,fecha
    ,idMotivoSancion
    ,idTipoSancion
    FROM Sancion
    WHERE (@idSancion IS NULL OR @idSancion=idSancion)
   
-- =====EXISTS==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[Sancion_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 02/07/11
-- =============================================
CREATE PROCEDURE [dbo].[Sancion_Update]
  @idSancion INT = NULL
 ,@idSancionTransaccional INT = NULL
 ,@cantidad INT = NULL
 ,@fecha DATE = NULL
 ,@idMotivoSancion INT = NULL
 ,@idTipoSancion INT = NULL
 ,@idAlumno INT = NULL
AS
 SET NOCOUNT OFF;

    UPDATE Sancion SET 
      idSancionTransaccional = @idSancionTransaccional
     ,cantidad = @cantidad
     ,fecha = @fecha
     ,idMotivoSancion = @idMotivoSancion
     ,idTipoSancion = @idTipoSancion
     ,idAlumnoCursoCicloLectivo = @idAlumno
    WHERE @idSancion=idSancion

GO
/****** Object:  StoredProcedure [dbo].[SancionInformeMail_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:  LAURA
-- Create date: 25/09/11
-- =============================================
CREATE PROCEDURE [dbo].[SancionInformeMail_Select]
  @fechaCarga DATE = NULL
 ,@idProcesoAutomatico INT = NULL
AS
 SET NOCOUNT OFF;

SELECT @fechaCarga= isnull(MAX(fechaEjecucion),null)
FROM ProcesosEjecutados
WHERE idProcesoAutomatico = @idProcesoAutomatico and resultado=1

	SELECT DISTINCT
		 A.fecha
		,P.nombre as 'nombreAlumno'
		,P.apellido as 'apellidoAlumno'
		,PT.nombre as 'nombreTutor'
		,PT.apellido as 'apellidoTutor'
		,PT.email
		,TS.nombre AS 'tipoSancion'
		,MS.descripcion as 'motivoSancion'
		,PT.idPersona AS 'idPersonaTutor'
		,P.idPersona as 'idPersonaAlumno'
		,A.cantidad
    FROM 
		Sancion AS A 
		INNER JOIN AlumnoCursoCicloLectivo AS ACCL ON A.idAlumnoCursoCicloLectivo = ACCL.idAlumnoCursoCicloLectivo
		INNER JOIN Alumnos AS AL ON ACCL.idAlumno = AL.idAlumno
		INNER JOIN Personas AS P ON AL.idPersona = P.idPersona
		INNER JOIN TutorAlumno AS TA ON AL.idAlumno = TA.idAlumno
		INNER JOIN TipoSancion AS TS ON A.idTipoSancion = TS.idTipoSancion
		INNER JOIN MotivoSancion AS MS ON A.idMotivoSancion = MS.idMotivoSancion
		LEFT JOIN Tutor AS T ON TA.idTutor = T.idTutor
		LEFT JOIN Personas AS PT ON T.idPersona = PT.idPersona
	WHERE 
		PT.email <> ''
		AND
	    (@fechaCarga IS NULL OR @fechaCarga=fechaCarga)
	    AND A.fecha BETWEEN 
				(SELECT fechaInicio from CicloLectivo where activo = 1) 
				and 
				(SELECT fechaFin from CicloLectivo where activo = 1)  
	ORDER BY PT.idPersona


GO
/****** Object:  StoredProcedure [dbo].[SancionInformeSMS_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





-- =============================================
-- Author:  LAURA
-- Create date: 25/09/11
-- =============================================
CREATE PROCEDURE [dbo].[SancionInformeSMS_Select]
  @fechaCarga DATE = NULL
 ,@idProcesoAutomatico INT = NULL
AS
 SET NOCOUNT OFF;

SELECT @fechaCarga= isnull(MAX(fechaEjecucion),null)
FROM ProcesosEjecutados
WHERE idProcesoAutomatico = @idProcesoAutomatico and resultado=1

	SELECT DISTINCT
		 A.fecha
		,P.nombre as 'nombreAlumno'
		,P.apellido as 'apellidoAlumno'
		,PT.nombre as 'nombreTutor'
		,PT.apellido as 'apellidoTutor'
		,PT.email
		,PT.telefonoCelular
		,TS.nombre AS 'tipoSancion'
		,MS.descripcion as 'motivoSancion'
		,PT.idPersona AS 'idPersonaTutor'
		,P.idPersona as 'idPersonaAlumno'
		,A.cantidad
    FROM 
		Sancion AS A 
		INNER JOIN AlumnoCursoCicloLectivo AS ACCL ON A.idAlumnoCursoCicloLectivo = ACCL.idAlumnoCursoCicloLectivo
		INNER JOIN Alumnos AS AL ON ACCL.idAlumno = AL.idAlumno
		INNER JOIN Personas AS P ON AL.idPersona = P.idPersona
		INNER JOIN TutorAlumno AS TA ON AL.idAlumno = TA.idAlumno
		INNER JOIN TipoSancion AS TS ON A.idTipoSancion = TS.idTipoSancion
		INNER JOIN MotivoSancion AS MS ON A.idMotivoSancion = MS.idMotivoSancion
		LEFT JOIN Tutor AS T ON TA.idTutor = T.idTutor
		LEFT JOIN Personas AS PT ON T.idPersona = PT.idPersona
	WHERE 
		PT.telefonoCelular <> ''
		AND
	    (@fechaCarga IS NULL OR @fechaCarga=fechaCarga)
	    -- Como no hay Sanciones cargadas en la ultima semana, dejamos a 200 dias:
	    AND A.fecha BETWEEN 
				(SELECT dateadd(d,-14,GETDATE())) 
				and 
				(SELECT GETDATE())  
	ORDER BY PT.idPersona






GO
/****** Object:  StoredProcedure [dbo].[TemaContenido_Delete]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 06/05/12
-- =============================================
CREATE PROCEDURE [dbo].[TemaContenido_Delete]
  @idTemaContenido NUMERIC = NULL
 ,@idContenido NUMERIC = NULL
 ,@usernameBaja VARCHAR(50) = NULL
AS
 SET NOCOUNT OFF;

 Declare @idPersonaBaja numeric = null
select @idPersonaBaja = idPersona
from Personas where Personas.username = @usernameBaja

    UPDATE TemaContenido SET 
		 fechaBaja = GETDATE()
		,idPersonaBaja = @idPersonaBaja
		,activo = 0
	WHERE (@idContenido IS NULL OR idContenido = @idContenido)
		AND 
		  (@idTemaContenido IS NULL OR idTemaContenido = @idTemaContenido)
		AND activo = 1
GO
/****** Object:  StoredProcedure [dbo].[TemaContenido_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 06/05/12
-- =============================================
CREATE PROCEDURE [dbo].[TemaContenido_Insert]
  @idTemaContenido NUMERIC = NULL
 ,@idContenido NUMERIC = NULL
 ,@titulo VARCHAR(100) = NULL
 ,@detalle VARCHAR(1000) = NULL
 ,@obligatorio BIT = NULL
 ,@activo BIT = NULL
 ,@fechaBaja DATE = NULL
 ,@idPersonaBaja NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    INSERT INTO TemaContenido(
     idContenido
    ,titulo
    ,detalle
    ,obligatorio
    ,activo
 )
 VALUES(
     @idContenido
    ,@titulo
    ,@detalle
    ,@obligatorio
    ,1
 )
GO
/****** Object:  StoredProcedure [dbo].[TemaContenido_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:  LAURA
-- Create date: 24/03/13
-- =============================================
CREATE PROCEDURE [dbo].[TemaContenido_Select]
  @idTemaContenido NUMERIC = NULL
 ,@idContenido NUMERIC = NULL
 ,@idCurricula NUMERIC = NULL
 ,@titulo VARCHAR(100) = NULL
 ,@detalle VARCHAR(1000) = NULL
 ,@obligatorio BIT = NULL
 ,@activo BIT = NULL
 ,@fechaBaja DATE = NULL
 ,@idPersonaBaja NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    SELECT TC.idTemaContenido
    ,TC.idContenido
    ,TC.titulo
    ,TC.detalle
    ,TC.obligatorio
    ,TC.activo
    ,TC.fechaBaja
    ,TC.idPersonaBaja
    ,C.descripcion as 'contenido'
    FROM TemaContenido AS TC
		INNER JOIN Contenido AS C ON TC.idContenido = C.idContenido
   AND (@idTemaContenido IS NULL OR @idTemaContenido=TC.idTemaContenido)
   AND (@idContenido IS NULL OR @idContenido=TC.idContenido)
   AND (@titulo IS NULL OR @titulo like '%' + TC.titulo + '%')
   AND (@detalle IS NULL OR @detalle like '%' + TC.detalle + '%')
   AND (@obligatorio IS NULL OR @obligatorio=TC.obligatorio)
   AND (@activo IS NULL OR @activo=TC.activo)
   AND (@fechaBaja IS NULL OR @fechaBaja=TC.fechaBaja)
   AND (@idPersonaBaja IS NULL OR @idPersonaBaja=TC.idPersonaBaja)
   AND (@idCurricula IS NULL OR @idCurricula=C.idCurricula)



GO
/****** Object:  StoredProcedure [dbo].[TemaContenido_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 06/05/12
-- =============================================
CREATE PROCEDURE [dbo].[TemaContenido_Update]
  @idTemaContenido NUMERIC = NULL
 ,@idContenido NUMERIC = NULL
 ,@titulo VARCHAR(100) = NULL
 ,@detalle VARCHAR(1000) = NULL
 ,@obligatorio BIT = NULL
AS
 SET NOCOUNT OFF;

    UPDATE TemaContenido SET 
      idContenido = @idContenido
     ,titulo = @titulo
     ,detalle = @detalle
     ,obligatorio = @obligatorio
    WHERE @idTemaContenido=idTemaContenido

GO
/****** Object:  StoredProcedure [dbo].[TemaContenidoByTemaPlanificacion_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





-- =============================================
-- Author:  LAURA
-- Create date: 06/05/12
-- =============================================
CREATE PROCEDURE [dbo].[TemaContenidoByTemaPlanificacion_Select]
  @idTemaContenido NUMERIC = NULL
 ,@idContenido NUMERIC = NULL
 ,@titulo VARCHAR(100) = NULL
 ,@detalle VARCHAR(1000) = NULL
 ,@obligatorio BIT = NULL
 ,@idAsignaturaCicloLectivo NUMERIC = NULL
 ,@idTemaPlanificacion NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    SELECT TC.idTemaContenido
    ,TC.idContenido
    ,TC.titulo
    ,TC.detalle
    ,TC.obligatorio
    FROM TemaContenido AS TC
		INNER JOIN Contenido AS C ON TC.idContenido = C.idContenido
		INNER JOIN AsignaturaCicloLectivo AS ACC ON C.idAsignaturaCicloLectivo = ACC.idAsignaturaCicloLectivo
		INNER JOIN TemaPlanificacionTemaContenido AS TPTC ON TC.idTemaContenido = TPTC.idTemaContenido
    WHERE (@idTemaContenido IS NULL OR @idTemaContenido=TC.idTemaContenido)
   AND (@idContenido IS NULL OR @idContenido=TC.idContenido)
   AND (@titulo IS NULL OR @titulo like '%' + TC.titulo + '%')
   AND (@detalle IS NULL OR @detalle like '%' + TC.detalle + '%')
   AND (@obligatorio IS NULL OR @obligatorio=TC.obligatorio)
   AND (@idAsignaturaCicloLectivo IS NULL OR @idAsignaturaCicloLectivo=ACC.idAsignaturaCicloLectivo)
   AND (@idTemaPlanificacion IS NULL OR @idTemaPlanificacion=TPTC.idTemaPlanificacion)
   ORDER BY TC.idTemaContenido, TC.titulo





GO
/****** Object:  StoredProcedure [dbo].[TemaContenidoByTemaPlanificacionAtrasado_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:  LAURA
-- Create date: 06/05/12
-- =============================================
CREATE PROCEDURE [dbo].[TemaContenidoByTemaPlanificacionAtrasado_Select]
 @idTemaPlanificacion NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    SELECT distinct TC.idTemaContenido
    ,TC.idContenido
    ,C.descripcion + ' - ' + TC.titulo as [titulo]
    ,TC.detalle
    ,TC.obligatorio
    FROM TemaContenido AS TC
		INNER JOIN Contenido AS C ON TC.idContenido = C.idContenido
		INNER JOIN TemaPlanificacionTemaContenido AS TPTC ON TC.idTemaContenido = TPTC.idTemaContenido
			AND @idTemaPlanificacion=TPTC.idTemaPlanificacion
    WHERE
      TC.idTemaContenido not in (Select DRC.idTemaContenido
							FROM DetalleRegistroClases AS DRC)
   ORDER BY TC.idTemaContenido
    ,TC.idContenido
    ,C.descripcion + ' - ' + TC.titulo
    ,TC.detalle
    ,TC.obligatorio









GO
/****** Object:  StoredProcedure [dbo].[TemaContenidoPorTemaPlanificacion_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:  LAURA
-- Create date: 24/03/13
-- =============================================
CREATE PROCEDURE [dbo].[TemaContenidoPorTemaPlanificacion_Select]
  @idTemaContenido NUMERIC = NULL
 ,@idContenido NUMERIC = NULL
 ,@idCurricula NUMERIC = NULL
 ,@titulo VARCHAR(100) = NULL
 ,@detalle VARCHAR(1000) = NULL
 ,@obligatorio BIT = NULL
 ,@activo BIT = NULL
 ,@fechaBaja DATE = NULL
 ,@idPersonaBaja NUMERIC = NULL
 ,@idTemaPlanificacion NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    SELECT TC.idTemaContenido
    ,TC.idContenido
    ,TC.titulo
    ,TC.detalle
    ,TC.obligatorio
    ,TC.activo
    ,TC.fechaBaja
    ,TC.idPersonaBaja
    ,C.descripcion as 'contenido'
    FROM TemaContenido AS TC
		INNER JOIN Contenido AS C ON TC.idContenido = C.idContenido
		INNER JOIN TemaPlanificacionTemaContenido AS TPTC ON TC.idTemaContenido = TPTC.idTemaContenido
   AND (@idTemaContenido IS NULL OR @idTemaContenido=TC.idTemaContenido)
   AND (@idContenido IS NULL OR @idContenido=TC.idContenido)
   AND (@titulo IS NULL OR @titulo like '%' + TC.titulo + '%')
   AND (@detalle IS NULL OR @detalle like '%' + TC.detalle + '%')
   AND (@obligatorio IS NULL OR @obligatorio=TC.obligatorio)
   AND (@activo IS NULL OR @activo=TC.activo)
   AND (@fechaBaja IS NULL OR @fechaBaja=TC.fechaBaja)
   AND (@idPersonaBaja IS NULL OR @idPersonaBaja=TC.idPersonaBaja)
   AND (@idCurricula IS NULL OR @idCurricula=C.idCurricula)
   AND (@idTemaPlanificacion IS NULL OR @idTemaPlanificacion=TPTC.idTemaPlanificacion)



GO
/****** Object:  StoredProcedure [dbo].[TemaPlanificacionAnual_Delete]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 06/08/12
-- =============================================
CREATE PROCEDURE [dbo].[TemaPlanificacionAnual_Delete]
  @idTemaPlanificacion NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    DELETE FROM TemaPlanificacionAnual
    WHERE @idTemaPlanificacion=idTemaPlanificacion

GO
/****** Object:  StoredProcedure [dbo].[TemaPlanificacionAnual_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 11/06/12
-- =============================================
CREATE PROCEDURE [dbo].[TemaPlanificacionAnual_Insert]
  @idTemaPlanificacion NUMERIC = NULL OUTPUT
 ,@idPlanificacionAnual NUMERIC = NULL
 ,@contenidosConceptuales VARCHAR(MAX) = NULL
 ,@contenidosProcedimentales VARCHAR(MAX) = NULL
 ,@contenidosActitudinales VARCHAR(MAX) = NULL
 ,@estrategiasAprendizaje VARCHAR(MAX) = NULL
 ,@criteriosEvaluacion VARCHAR(1000) = NULL
 ,@instrumentosEvaluacion VARCHAR(1000) = NULL
 ,@fechaInicioEstimada DATE = NULL
 ,@fechaFinEstimada DATE = NULL
 ,@observaciones VARCHAR(50) = NULL
AS
 SET NOCOUNT OFF;

    INSERT INTO TemaPlanificacionAnual(
     idPlanificacionAnual
    ,contenidosConceptuales
    ,contenidosProcedimentales
    ,contenidosActitudinales
    ,estrategiasAprendizaje
    ,criteriosEvaluacion
    ,instrumentosEvaluacion
    ,fechaInicioEstimada
    ,fechaFinEstimada
    ,observaciones
 )
 VALUES(
     @idPlanificacionAnual
    ,@contenidosConceptuales
    ,@contenidosProcedimentales
    ,@contenidosActitudinales
    ,@estrategiasAprendizaje
    ,@criteriosEvaluacion
    ,@instrumentosEvaluacion
    ,@fechaInicioEstimada
    ,@fechaFinEstimada
    ,@observaciones
 )
 
  SET @idTemaPlanificacion = @@IDENTITY
	RETURN SCOPE_IDENTITY()
GO
/****** Object:  StoredProcedure [dbo].[TemaPlanificacionAnual_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 26/03/13
-- =============================================
CREATE PROCEDURE [dbo].[TemaPlanificacionAnual_Select]
  @idTemaPlanificacion NUMERIC = NULL
 ,@idPlanificacionAnual NUMERIC = NULL
 ,@idCurricula NUMERIC = NULL
 ,@contenidosConceptuales VARCHAR(MAX) = NULL
 ,@contenidosProcedimentales VARCHAR(MAX) = NULL
 ,@contenidosActitudinales VARCHAR(MAX) = NULL
 ,@estrategiasAprendizaje VARCHAR(MAX) = NULL
 ,@criteriosEvaluacion VARCHAR(1000) = NULL
 ,@instrumentosEvaluacion VARCHAR(1000) = NULL
 ,@fechaInicioEstimada DATE = NULL
 ,@fechaFinEstimada DATE = NULL
 ,@observaciones VARCHAR(50) = NULL
AS
 SET NOCOUNT OFF;

    SELECT TPA.idTemaPlanificacion
    ,TPA.idPlanificacionAnual
    ,TPA.contenidosConceptuales
    ,TPA.contenidosProcedimentales
    ,TPA.contenidosActitudinales
    ,TPA.estrategiasAprendizaje
    ,TPA.criteriosEvaluacion
    ,TPA.instrumentosEvaluacion
    ,TPA.fechaInicioEstimada
    ,TPA.fechaFinEstimada
    ,TPA.observaciones
    ,PA.fechaAprobada 
    FROM TemaPlanificacionAnual AS TPA
		INNER JOIN PlanificacionAnual AS PA ON TPA.idPlanificacionAnual = PA.idPlanificacionAnual
		INNER JOIN Curricula AS C ON PA.idCurricula = C.idCurricula
    WHERE (@idTemaPlanificacion IS NULL OR @idTemaPlanificacion=TPA.idTemaPlanificacion)
   AND (@idPlanificacionAnual IS NULL OR @idPlanificacionAnual=TPA.idPlanificacionAnual)
   AND (@contenidosConceptuales IS NULL OR @contenidosConceptuales=contenidosConceptuales)
   AND (@contenidosProcedimentales IS NULL OR @contenidosProcedimentales=contenidosProcedimentales)
   AND (@contenidosActitudinales IS NULL OR @contenidosActitudinales=contenidosActitudinales)
   AND (@estrategiasAprendizaje IS NULL OR @estrategiasAprendizaje=estrategiasAprendizaje)
   AND (@criteriosEvaluacion IS NULL OR @criteriosEvaluacion=criteriosEvaluacion)
   AND (@instrumentosEvaluacion IS NULL OR @instrumentosEvaluacion=instrumentosEvaluacion)
   AND (@fechaInicioEstimada IS NULL OR @fechaInicioEstimada=fechaInicioEstimada)
   AND (@fechaFinEstimada IS NULL OR @fechaFinEstimada=fechaFinEstimada)
   AND (@observaciones IS NULL OR @observaciones=TPA.observaciones)
   AND (@idCurricula IS NULL OR @idCurricula=C.idCurricula)

   order by TPA.fechaInicioEstimada,TPA.fechaFinEstimada,TPA.contenidosConceptuales
GO
/****** Object:  StoredProcedure [dbo].[TemaPlanificacionAnual_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 18/06/12
-- =============================================
CREATE PROCEDURE [dbo].[TemaPlanificacionAnual_Update]
  @idTemaPlanificacion NUMERIC = NULL
 ,@idPlanificacionAnual NUMERIC = NULL
 ,@contenidosConceptuales VARCHAR(MAX) = NULL
 ,@contenidosProcedimentales VARCHAR(MAX) = NULL
 ,@contenidosActitudinales VARCHAR(MAX) = NULL
 ,@estrategiasAprendizaje VARCHAR(MAX) = NULL
 ,@criteriosEvaluacion VARCHAR(1000) = NULL
 ,@instrumentosEvaluacion VARCHAR(1000) = NULL
 ,@fechaInicioEstimada DATE = NULL
 ,@fechaFinEstimada DATE = NULL
 ,@observaciones VARCHAR(50) = NULL
AS
 SET NOCOUNT OFF;

    UPDATE TemaPlanificacionAnual SET 
      idPlanificacionAnual = @idPlanificacionAnual
     ,contenidosConceptuales = @contenidosConceptuales
     ,contenidosProcedimentales = @contenidosProcedimentales
     ,contenidosActitudinales = @contenidosActitudinales
     ,estrategiasAprendizaje = @estrategiasAprendizaje
     ,criteriosEvaluacion = @criteriosEvaluacion
     ,instrumentosEvaluacion = @instrumentosEvaluacion
     ,fechaInicioEstimada = @fechaInicioEstimada
     ,fechaFinEstimada = @fechaFinEstimada
     ,observaciones = @observaciones
    WHERE @idTemaPlanificacion=idTemaPlanificacion
   
GO
/****** Object:  StoredProcedure [dbo].[TemaPlanificacionAnualAtrasados_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:  LAURA
-- Create date: 31/05/12
-- =============================================
CREATE PROCEDURE [dbo].[TemaPlanificacionAnualAtrasados_Select]

  @idCursoCicloLectivo NUMERIC = NULL
 ,@idAsignaturaCicloLectivo NUMERIC = NULL

AS
 SET NOCOUNT OFF;

 declare  @idCurricula NUMERIC = NULL
 
select @idCurricula = PA.idCurricula
 from
	RelPlanificacionCurso as RPC 
		inner join AsignaturaCicloLectivo AS ACL on rpc.idCursoCicloLectivo = acl.idCursoCicloLectivo
			and (@idAsignaturaCicloLectivo IS NULL OR acl.idAsignaturaCicloLectivo = @idAsignaturaCicloLectivo)
			AND (@idCursoCicloLectivo IS NULL OR ACL.idCursoCicloLectivo = @idCursoCicloLectivo)
		inner join PlanificacionAnual as PA on pa.idPlanificacionAnual = RPC.idPlanificacionAnual
		inner join Curricula as C on c.idCurricula = pa.idCurricula
			and c.idAsignatura = acl.idAsignatura

    SELECT Distinct TPA.idTemaPlanificacion
    ,TPA.idPlanificacionAnual
    ,TPA.contenidosConceptuales
    ,TPA.contenidosProcedimentales
    ,TPA.contenidosActitudinales
    ,TPA.estrategiasAprendizaje
    ,TPA.criteriosEvaluacion
    ,TPA.instrumentosEvaluacion
    ,TPA.fechaInicioEstimada
    ,TPA.fechaFinEstimada
    ,TPA.observaciones
    ,A.nombre AS 'Asignatura'
    ,PA.fechaAprobada 
    FROM TemaPlanificacionAnual AS TPA
		INNER JOIN PlanificacionAnual AS PA ON PA.idPlanificacionAnual = TPA.idPlanificacionAnual
			AND PA.idCurricula = @idCurricula
		INNER JOIN Curricula AS C ON PA.idCurricula = C.idCurricula
		INNER JOIN Asignatura AS a ON A.idAsignatura = c.idAsignatura
		INNER JOIN TemaPlanificacionTemaContenido as TPTC on TPTC.idTemaPlanificacion = TPA.idTemaPlanificacion
		INNER JOIN TemaContenido AS TC on TPTC.idTemaContenido = TC.idTemaContenido
		INNER JOIN Contenido AS Cont on TC.idContenido = Cont.idContenido
		
    WHERE 
		TPA.fechaFinEstimada < CURRENT_TIMESTAMP
	AND Cont.activo = 1
	AND TC.idTemaContenido not in (Select DRC.idTemaContenido
							FROM DetalleRegistroClases AS DRC)
	ORDER BY 
		 TPA.fechaInicioEstimada
		,TPA.fechaFinEstimada
		,TPA.idTemaPlanificacion
		,TPA.idPlanificacionAnual
		,TPA.contenidosConceptuales
		,TPA.contenidosProcedimentales
		,TPA.contenidosActitudinales
		,TPA.estrategiasAprendizaje
		,TPA.criteriosEvaluacion
		,TPA.instrumentosEvaluacion
		,TPA.observaciones
		,A.nombre
		,PA.fechaAprobada 


GO
/****** Object:  StoredProcedure [dbo].[TemaPlanificacionTemaContenido_Delete]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 14/08/12
-- =============================================
CREATE PROCEDURE [dbo].[TemaPlanificacionTemaContenido_Delete]
  @idTemaPlanificacion NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    DELETE FROM TemaPlanificacionTemaContenido
    WHERE @idTemaPlanificacion=idTemaPlanificacion

GO
/****** Object:  StoredProcedure [dbo].[TemaPlanificacionTemaContenido_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 14/08/12
-- =============================================
CREATE PROCEDURE [dbo].[TemaPlanificacionTemaContenido_Insert]
  @idTemaPlanificacion NUMERIC = NULL
 ,@idTemaContenido NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    INSERT INTO TemaPlanificacionTemaContenido(
     idTemaPlanificacion
    ,idTemaContenido
 )
 VALUES(
     @idTemaPlanificacion
    ,@idTemaContenido
 )
GO
/****** Object:  StoredProcedure [dbo].[TemaPlanificacionTemaContenido_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 14/08/12
-- =============================================
CREATE PROCEDURE [dbo].[TemaPlanificacionTemaContenido_Select]
  @idTemaPlanificacion NUMERIC = NULL
 ,@idTemaContenido NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    SELECT idTemaPlanificacion
    ,idTemaContenido
	,obligatorio
    FROM TemaPlanificacionTemaContenido 
    WHERE (@idTemaPlanificacion IS NULL OR @idTemaPlanificacion=idTemaPlanificacion)
   AND (@idTemaContenido IS NULL OR @idTemaContenido=idTemaContenido)


GO
/****** Object:  StoredProcedure [dbo].[TemaPlanificacionTemaContenidoDesactivado_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 24/03/13
-- =============================================
CREATE PROCEDURE [dbo].[TemaPlanificacionTemaContenidoDesactivado_Select]
  @idTemaPlanificacion NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    SELECT TC.idTemaContenido
    ,TC.idContenido
    ,TC.titulo
    ,TC.detalle
    ,TC.obligatorio
    ,TC.activo
    ,TC.fechaBaja
    ,TC.idPersonaBaja
    ,C.descripcion as 'contenido'
    FROM TemaContenido AS TC
		INNER JOIN Contenido AS C ON TC.idContenido = C.idContenido 
		INNER JOIN TemaPlanificacionTemaContenido AS TPTC ON TC.idTemaContenido = TPTC.idTemaContenido
			AND TPTC.idTemaPlanificacion = @idTemaPlanificacion AND TC.activo = 0
GO
/****** Object:  StoredProcedure [dbo].[TipoAsistencia_Delete]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 26/06/11
-- =============================================
CREATE PROCEDURE [dbo].[TipoAsistencia_Delete]
  @idTipoAsistencia INT = NULL
 ,@descripcion VARCHAR(50) = NULL
 ,@grupo VARCHAR(50) = NULL
 ,@valor DECIMAL(18, 2) = NULL
 ,@idTipoAsistenciaTransaccional INT = NULL
AS
 SET NOCOUNT OFF;

    DELETE FROM TipoAsistencia
    WHERE (@idTipoAsistencia IS NULL OR @idTipoAsistencia=idTipoAsistencia)

-- =====UPDATE==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[TipoAsistencia_Exists]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 26/06/11
-- =============================================
CREATE PROCEDURE [dbo].[TipoAsistencia_Exists]
  @idTipoAsistencia INT = NULL
 ,@descripcion VARCHAR(50) = NULL
 ,@valor DECIMAL(18, 2) = NULL
 ,@idTipoAsistenciaTransaccional INT = NULL
 ,@exists BIT OUT
AS
 SET NOCOUNT OFF;

    IF EXISTS (
 SELECT idTipoAsistencia
    FROM TipoAsistencia
    WHERE (@idTipoAsistencia IS NULL OR @idTipoAsistencia=idTipoAsistencia)
   AND (@descripcion IS NULL OR @descripcion=descripcion)
   AND (@valor IS NULL OR @valor=valor)
   AND (@idTipoAsistenciaTransaccional IS NULL OR @idTipoAsistenciaTransaccional=idTipoAsistenciaTransaccional)
 )
 SET @exists = 1
 ELSE SET @exists = 0

GO
/****** Object:  StoredProcedure [dbo].[TipoAsistencia_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 26/06/11
-- =============================================
CREATE PROCEDURE [dbo].[TipoAsistencia_Insert]
  @idTipoAsistencia INT = NULL
 ,@descripcion VARCHAR(50) = NULL
 ,@valor DECIMAL(18, 2) = NULL
 ,@idTipoAsistenciaTransaccional INT = NULL
AS
 SET NOCOUNT OFF;

SELECT @idTipoAsistencia = idTipoAsistencia
FROM TipoAsistencia
WHERE @idTipoAsistenciaTransaccional = idTipoAsistenciaTransaccional

IF(@idTipoAsistencia is NULL OR @idTipoAsistencia = 0)
BEGIN
    INSERT INTO TipoAsistencia(
     descripcion
    ,valor
    ,idTipoAsistenciaTransaccional
 )
 VALUES(
     @descripcion
    ,@valor
    ,@idTipoAsistenciaTransaccional
 )
 SET  @idTipoAsistencia = @@IDENTITY
END
ELSE
BEGIN
	EXECUTE [TipoAsistencia_Update] 
   @idTipoAsistencia
  ,@descripcion
  ,@valor
  ,@idTipoAsistenciaTransaccional
END

GO
/****** Object:  StoredProcedure [dbo].[TipoAsistencia_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 26/06/11
-- =============================================
CREATE PROCEDURE [dbo].[TipoAsistencia_Select]
  @idTipoAsistencia INT = NULL
 ,@descripcion VARCHAR(50) = NULL
 ,@grupo VARCHAR(50) = NULL
 ,@valor DECIMAL(18, 2) = NULL
 ,@idTipoAsistenciaTransaccional INT = NULL
AS
 SET NOCOUNT OFF;

    SELECT idTipoAsistencia
    ,descripcion
    --,grupo
    ,valor
    ,idTipoAsistenciaTransaccional
    FROM TipoAsistencia
    WHERE 
    idTipoAsistencia <> 13
    AND (@idTipoAsistencia IS NULL OR @idTipoAsistencia=idTipoAsistencia)
	AND (@descripcion IS NULL OR @descripcion=descripcion)
	AND (@valor IS NULL OR @valor=valor)
	AND (@idTipoAsistenciaTransaccional IS NULL OR @idTipoAsistenciaTransaccional=idTipoAsistenciaTransaccional)

GO
/****** Object:  StoredProcedure [dbo].[TipoAsistencia_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 26/06/11
-- =============================================
CREATE PROCEDURE [dbo].[TipoAsistencia_Update]
  @idTipoAsistencia INT = NULL
 ,@descripcion VARCHAR(50) = NULL
 ,@valor DECIMAL(18, 2) = NULL
 ,@idTipoAsistenciaTransaccional INT = NULL
AS
 SET NOCOUNT OFF;

    UPDATE TipoAsistencia SET 
      descripcion = @descripcion
     ,valor = @valor
     ,idTipoAsistenciaTransaccional = @idTipoAsistenciaTransaccional
    WHERE idTipoAsistencia=@idTipoAsistencia


-- =====SELECT==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[TipoDocumento_Delete]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 14/06/11
-- =============================================
CREATE PROCEDURE [dbo].[TipoDocumento_Delete]
  @idTipoDocumento NUMERIC = NULL
 ,@idTipoDocumentoTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@descripcion VARCHAR(250) = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;

    DELETE FROM TipoDocumento
    WHERE (@idTipoDocumento IS NULL OR @idTipoDocumento=idTipoDocumento)

-- =====UPDATE==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[TipoDocumento_Exists]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 14/06/11
-- =============================================
CREATE PROCEDURE [dbo].[TipoDocumento_Exists]
  @idTipoDocumento NUMERIC = NULL
 ,@idTipoDocumentoTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@descripcion VARCHAR(250) = NULL
 ,@activo BIT = NULL
 ,@exists BIT OUT
AS
 SET NOCOUNT OFF;

    IF EXISTS (
 SELECT idTipoDocumento
    FROM TipoDocumento
    WHERE (@idTipoDocumento IS NULL OR @idTipoDocumento=idTipoDocumento)
   AND (@idTipoDocumentoTransaccional IS NULL OR @idTipoDocumentoTransaccional=idTipoDocumentoTransaccional)
   AND (@nombre IS NULL OR @nombre=nombre)
   AND (@descripcion IS NULL OR @descripcion=descripcion)
   AND (@activo IS NULL OR @activo=activo)
 )
 SET @exists = 1
 ELSE SET @exists = 0

GO
/****** Object:  StoredProcedure [dbo].[TipoDocumento_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 14/06/11
-- =============================================
CREATE PROCEDURE [dbo].[TipoDocumento_Insert]
  @idTipoDocumento NUMERIC = NULL output
 ,@idTipoDocumentoTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@descripcion VARCHAR(250) = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;
SELECT @idTipoDocumento=idTipoDocumento 
  FROM [dbo].[TipoDocumento]
  WHERE [idTipoDocumentoTransaccional]= @idTipoDocumentoTransaccional
  
  IF (@idTipoDocumento=0 or @idTipoDocumento is null)
  BEGIN
    INSERT INTO TipoDocumento(
     idTipoDocumentoTransaccional
    ,nombre
    ,descripcion
    ,activo
 )
 VALUES(
     @idTipoDocumentoTransaccional
    ,@nombre
    ,@descripcion
    ,@activo
 )
SET @idTipoDocumento = @@IDENTITY
   END
ELSE
BEGIN
	EXECUTE [TipoDocumento_Update] 
	   @idTipoDocumento
	  ,@idTipoDocumentoTransaccional
	  ,@nombre
	  ,@descripcion
	  ,@activo
END

GO
/****** Object:  StoredProcedure [dbo].[TipoDocumento_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 14/06/11
-- =============================================
CREATE PROCEDURE [dbo].[TipoDocumento_Select]
  @idTipoDocumento NUMERIC = NULL
 ,@idTipoDocumentoTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@descripcion VARCHAR(250) = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;

    SELECT idTipoDocumento
    ,idTipoDocumentoTransaccional
    ,nombre
    ,descripcion
    ,activo
    FROM TipoDocumento
    WHERE (@idTipoDocumento IS NULL OR @idTipoDocumento=idTipoDocumento)
   AND (@idTipoDocumentoTransaccional IS NULL OR @idTipoDocumentoTransaccional=idTipoDocumentoTransaccional)
   AND (@nombre IS NULL OR @nombre=nombre)
   AND (@descripcion IS NULL OR @descripcion=descripcion)
   AND (@activo IS NULL OR @activo=activo)

-- =====EXISTS==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[TipoDocumento_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 14/06/11
-- =============================================
CREATE PROCEDURE [dbo].[TipoDocumento_Update]
  @idTipoDocumento NUMERIC = NULL
 ,@idTipoDocumentoTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@descripcion VARCHAR(250) = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;

    UPDATE TipoDocumento SET 
      idTipoDocumentoTransaccional = @idTipoDocumentoTransaccional
     ,nombre = @nombre
     ,descripcion = @descripcion
     ,activo = @activo
    WHERE (@idTipoDocumento IS NULL OR @idTipoDocumento=idTipoDocumento)
   

-- =====SELECT==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[TipoEventoInstitucional_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 30/07/11
-- =============================================
CREATE PROCEDURE [dbo].[TipoEventoInstitucional_Select]
  @idTipoEventoInstitucional NUMERIC = NULL
 ,@descripcion VARCHAR(150) = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;

    SELECT idTipoEventoInstitucional
    ,descripcion
    ,activo
    FROM TipoEventoInstitucional
    WHERE (@idTipoEventoInstitucional IS NULL OR @idTipoEventoInstitucional=idTipoEventoInstitucional)
   AND (@descripcion IS NULL OR @descripcion=descripcion)
   AND (@activo IS NULL OR @activo=activo)

GO
/****** Object:  StoredProcedure [dbo].[TipoEventoInstitucional_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 31/07/11
-- =============================================
CREATE PROCEDURE [dbo].[TipoEventoInstitucional_Update]
  @idTipoEventoInstitucional NUMERIC = NULL
 ,@descripcion VARCHAR(150) = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;

    UPDATE TipoEventoInstitucional SET 
     descripcion = @descripcion
     ,activo = @activo
    WHERE (@idTipoEventoInstitucional IS NULL OR @idTipoEventoInstitucional=idTipoEventoInstitucional)
   AND (@descripcion IS NULL OR @descripcion=descripcion)
   AND (@activo IS NULL OR @activo=activo)

GO
/****** Object:  StoredProcedure [dbo].[TipoNovedadAulica_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 18/09/12
-- =============================================
CREATE PROCEDURE [dbo].[TipoNovedadAulica_Select]
  @idTipoNovedadAulica NUMERIC = NULL
 ,@descripcion VARCHAR(50) = NULL
AS
 SET NOCOUNT OFF;

    SELECT idTipoNovedadAulica
    ,descripcion
    FROM TipoNovedadAulica
    WHERE (@idTipoNovedadAulica IS NULL OR @idTipoNovedadAulica=idTipoNovedadAulica)
   AND (@descripcion IS NULL OR @descripcion=descripcion)
GO
/****** Object:  StoredProcedure [dbo].[TipoPersonaRol_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 14/06/11
-- =============================================
CREATE PROCEDURE [dbo].[TipoPersonaRol_Select]
  @idTipoPersonaRol NUMERIC = NULL
 ,@idTipoPersona NUMERIC = NULL
 ,@nombreRol VARCHAR(50) = NULL
AS
 SET NOCOUNT OFF;

    SELECT idTipoPersonaRol
    ,idTipoPersona
    ,nombreRol
    FROM TipoPersonaRol
    WHERE (@idTipoPersonaRol IS NULL OR @idTipoPersonaRol=idTipoPersonaRol)
   AND (@idTipoPersona IS NULL OR @idTipoPersona=idTipoPersona)
   AND (@nombreRol IS NULL OR @nombreRol=nombreRol)

-- =====EXISTS==================================
SET ANSI_NULLS ON


GO
/****** Object:  StoredProcedure [dbo].[TipoRegistroClases_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 19/08/12
-- =============================================
CREATE PROCEDURE [dbo].[TipoRegistroClases_Select]
  @idTipoRegistroClases NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
AS
 SET NOCOUNT OFF;

    SELECT idTipoRegistroClases
    ,nombre
    FROM TipoRegistroClases
    WHERE (@idTipoRegistroClases IS NULL OR @idTipoRegistroClases=idTipoRegistroClases)
   AND (@nombre IS NULL OR @nombre=nombre)
GO
/****** Object:  StoredProcedure [dbo].[TipoSancion_Delete]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 28/06/11
-- =============================================
CREATE PROCEDURE [dbo].[TipoSancion_Delete]
  @idTipoSancion INT = NULL
 ,@idTipoSancionTransaccional INT = NULL
 ,@nombre NCHAR = NULL
 ,@descripcion NCHAR = NULL
AS
 SET NOCOUNT OFF;

    DELETE FROM TipoSancion
    WHERE (@idTipoSancion IS NULL OR @idTipoSancion=idTipoSancion)
 

-- =====UPDATE==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[TipoSancion_Exists]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 28/06/11
-- =============================================
CREATE PROCEDURE [dbo].[TipoSancion_Exists]
  @idTipoSancion INT = NULL
 ,@idTipoSancionTransaccional INT = NULL
 ,@nombre NCHAR = NULL
 ,@descripcion NCHAR = NULL
 ,@exists BIT OUT
AS
 SET NOCOUNT OFF;

    IF EXISTS (
 SELECT idTipoSancion
    FROM TipoSancion
    WHERE (@idTipoSancion IS NULL OR @idTipoSancion=idTipoSancion)
   AND (@idTipoSancionTransaccional IS NULL OR @idTipoSancionTransaccional=idTipoSancionTransaccional)
   AND (@nombre IS NULL OR @nombre=nombre)
   AND (@descripcion IS NULL OR @descripcion=descripcion)
 )
 SET @exists = 1
 ELSE SET @exists = 0

GO
/****** Object:  StoredProcedure [dbo].[TipoSancion_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 28/06/11
-- =============================================
CREATE PROCEDURE [dbo].[TipoSancion_Insert]
  @idTipoSancion INT = NULL 
 ,@idTipoSancionTransaccional INT = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@descripcion VARCHAR(250) = NULL
AS
 SET NOCOUNT OFF;

SELECT @idTipoSancion = idTipoSancion
FROM TipoSancion
WHERE @idTipoSancionTransaccional = idTipoSancionTransaccional

IF(@idTipoSancion is NULL OR @idTipoSancion = 0)
BEGIN
    INSERT INTO TipoSancion(
     idTipoSancionTransaccional
    ,nombre
    ,descripcion
	)
 VALUES(
     @idTipoSancionTransaccional
    ,@nombre
    ,@descripcion
	)
  SET  @idTipoSancion = @@IDENTITY
END
ELSE 
	BEGIN
		 EXECUTE [TipoSancion_Update]
		 @idTipoSancion
		,@idTipoSancionTransaccional
		,@nombre
		,@descripcion
	END  

SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[TipoSancion_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 28/06/11
-- =============================================
CREATE PROCEDURE [dbo].[TipoSancion_Select]
  @idTipoSancion INT = NULL
 ,@idTipoSancionTransaccional INT = NULL
 ,@nombre NCHAR = NULL
 ,@descripcion NCHAR = NULL
AS
 SET NOCOUNT OFF;

    SELECT idTipoSancion
    ,idTipoSancionTransaccional
    ,nombre
    ,descripcion
    FROM TipoSancion
    WHERE (@idTipoSancion IS NULL OR @idTipoSancion=idTipoSancion)


-- =====EXISTS==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[TipoSancion_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 15/07/11
-- =============================================
CREATE PROCEDURE [dbo].[TipoSancion_Update]
  @idTipoSancion INT = NULL
 ,@idTipoSancionTransaccional INT = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@descripcion VARCHAR(250) = NULL
AS
 SET NOCOUNT OFF;

    UPDATE TipoSancion SET 
      idTipoSancionTransaccional = @idTipoSancionTransaccional
     ,nombre = @nombre
     ,descripcion = @descripcion
    WHERE @idTipoSancion=idTipoSancion

GO
/****** Object:  StoredProcedure [dbo].[TipoTutor_Delete]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 26/06/11
-- =============================================
CREATE PROCEDURE [dbo].[TipoTutor_Delete]
  @idTipoTutor NUMERIC = NULL
 ,@descripcion VARCHAR(50) = NULL
 ,@idTipoTutorTransaccional NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    DELETE FROM TipoTutor
    WHERE (@idTipoTutor IS NULL OR @idTipoTutor=idTipoTutor)
   AND (@descripcion IS NULL OR @descripcion=descripcion)
   AND (@idTipoTutorTransaccional IS NULL OR @idTipoTutorTransaccional=idTipoTutorTransaccional)

GO
/****** Object:  StoredProcedure [dbo].[TipoTutor_Exists]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 26/06/11
-- =============================================
CREATE PROCEDURE [dbo].[TipoTutor_Exists]
  @idTipoTutor NUMERIC = NULL
 ,@descripcion VARCHAR(50) = NULL
 ,@idTipoTutorTransaccional NUMERIC = NULL
 ,@exists BIT OUT
AS
 SET NOCOUNT OFF;

    IF EXISTS (
 SELECT idTipoTutor
    FROM TipoTutor
    WHERE (@idTipoTutor IS NULL OR @idTipoTutor=idTipoTutor)
   AND (@descripcion IS NULL OR @descripcion=descripcion)
   AND (@idTipoTutorTransaccional IS NULL OR @idTipoTutorTransaccional=idTipoTutorTransaccional)
 )
 SET @exists = 1
 ELSE SET @exists = 0

GO
/****** Object:  StoredProcedure [dbo].[TipoTutor_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 26/06/11
-- =============================================
CREATE PROCEDURE [dbo].[TipoTutor_Insert]
  @idTipoTutor NUMERIC = NULL
 ,@descripcion VARCHAR(50) = NULL
 ,@idTipoTutorTransaccional NUMERIC = NULL
AS
 SET NOCOUNT OFF;

Select @idTipoTutor= idTipoTutor
from [TipoTutor]
where idTipoTutorTransaccional = @idTipoTutorTransaccional
if (@idTipoTutor is null OR @idTipoTutor = 0)
	begin
		INSERT INTO TipoTutor(
		 descripcion
		,idTipoTutorTransaccional
	 )
	 VALUES(
		 @descripcion
		,@idTipoTutorTransaccional
	 )
	 set @idTipoTutor = @@IDENTITY
	end
 else
	 begin
		Execute [TipoTutor_Update]
		 @idTipoTutor
		,@descripcion 
		,@idTipoTutorTransaccional 
	 end
 
-- =====DELETE==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[TipoTutor_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 26/06/11
-- =============================================
CREATE PROCEDURE [dbo].[TipoTutor_Select]
  @idTipoTutor NUMERIC = NULL
 ,@descripcion VARCHAR(50) = NULL
 ,@idTipoTutorTransaccional NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    SELECT idTipoTutor
    ,descripcion
    ,idTipoTutorTransaccional
    FROM TipoTutor
    WHERE (@idTipoTutor IS NULL OR @idTipoTutor=idTipoTutor)
   AND (@descripcion IS NULL OR @descripcion=descripcion)
   AND (@idTipoTutorTransaccional IS NULL OR @idTipoTutorTransaccional=idTipoTutorTransaccional)


GO
/****** Object:  StoredProcedure [dbo].[TipoTutor_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 26/06/11
-- =============================================
CREATE PROCEDURE [dbo].[TipoTutor_Update]
  @idTipoTutor NUMERIC = NULL
 ,@descripcion VARCHAR(50) = NULL
 ,@idTipoTutorTransaccional NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    UPDATE TipoTutor SET 
      descripcion = @descripcion
     ,idTipoTutorTransaccional = @idTipoTutorTransaccional
    WHERE (@idTipoTutor IS NULL OR @idTipoTutor=idTipoTutor)
-- =====SELECT=================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[Tutor_Delete]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 26/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Tutor_Delete]
  @idTutor NUMERIC = NULL
 ,@idTutorTransaccional NUMERIC = NULL
 ,@idPersona NUMERIC = NULL
 ,@telefonoTrabajo VARCHAR(20) = NULL
 ,@idTipoTutor NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    DELETE FROM Tutor
    WHERE (@idTutor IS NULL OR @idTutor=idTutor)
   AND (@idTutorTransaccional IS NULL OR @idTutorTransaccional=idTutorTransaccional)
   AND (@idPersona IS NULL OR @idPersona=idPersona)
   AND (@telefonoTrabajo IS NULL OR @telefonoTrabajo=telefonoTrabajo)
   AND (@idTipoTutor IS NULL OR @idTipoTutor=idTipoTutor)

GO
/****** Object:  StoredProcedure [dbo].[Tutor_Exists]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 26/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Tutor_Exists]
  @idTutor NUMERIC = NULL
 ,@idTutorTransaccional NUMERIC = NULL
 ,@idPersona NUMERIC = NULL
 ,@telefonoTrabajo VARCHAR(20) = NULL
 ,@idTipoTutor NUMERIC = NULL
 ,@exists BIT OUT
AS
 SET NOCOUNT OFF;

    IF EXISTS (
 SELECT idTutor
    FROM Tutor
    WHERE (@idTutor IS NULL OR @idTutor=idTutor)
   AND (@idTutorTransaccional IS NULL OR @idTutorTransaccional=idTutorTransaccional)
   AND (@idPersona IS NULL OR @idPersona=idPersona)
   AND (@telefonoTrabajo IS NULL OR @telefonoTrabajo=telefonoTrabajo)
   AND (@idTipoTutor IS NULL OR @idTipoTutor=idTipoTutor)
 )
 SET @exists = 1
 ELSE SET @exists = 0

GO
/****** Object:  StoredProcedure [dbo].[Tutor_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 26/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Tutor_Insert]
  @idTutor NUMERIC = NULL output
 ,@idTutorTransaccional NUMERIC = NULL
 ,@idPersona NUMERIC = NULL
 ,@telefonoTrabajo VARCHAR(20) = NULL
 ,@idTipoTutor NUMERIC = NULL
AS
 SET NOCOUNT OFF;

 SELECT @idTutor=idTutor 
 FROM [dbo].[Tutor]
 WHERE [idTutorTransaccional]= @idTutorTransaccional
  
 SELECT @idPersona=idPersona 
 FROM [dbo].[Personas]
 WHERE [idPersona]= @idPersona
 
 select @idTipoTutor=idTipoTutor
 from [TipoTutor]
 where idTipoTutorTransaccional = @idTipoTutor
 
 
 IF (@idTutor IS NULL OR @idTutor=0)
  BEGIN
    INSERT INTO Tutor(
     idTutorTransaccional
    ,idPersona
    ,telefonoTrabajo
    ,idTipoTutor
 )
 VALUES(
     @idTutorTransaccional
    ,@idPersona
    ,@telefonoTrabajo
    ,@idTipoTutor
 )
  SET @idTutor = @@IDENTITY
END
ELSE
BEGIN
	EXECUTE Tutor_Update
	 @idTutor
	,@idTutorTransaccional
    ,@idPersona
    ,@telefonoTrabajo
    ,@idTipoTutor
END
   RETURN SCOPE_IDENTITY()

-- =====DELETE==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[Tutor_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 26/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Tutor_Select]
  @idTutor NUMERIC = NULL,
  @idTutorTransaccional NUMERIC = NULL
 ,@idPersona NUMERIC = NULL
 ,@telefonoTrabajo VARCHAR(20) = NULL
 ,@idTipoTutor NUMERIC = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;

    SELECT T.idTutor
    ,T.idTutorTransaccional
	,T.idPersona
    ,T.telefonoTrabajo
    ,T.idTipoTutor
    ,P.nombre
    ,P.apellido
    ,P.activo
    ,P.fechaNacimiento
    FROM Tutor AS T INNER JOIN Personas AS P ON T.idPersona = P.idPersona
    WHERE (@idTutor IS NULL OR @idTutor=T.idTutor)
   AND (@idTutorTransaccional IS NULL OR @idTutorTransaccional=T.idTutorTransaccional)
   AND (@idPersona IS NULL OR @idPersona=T.idPersona)
   AND (@telefonoTrabajo IS NULL OR @telefonoTrabajo=T.telefonoTrabajo)
   AND (@idTipoTutor IS NULL OR @idTipoTutor=T.idTipoTutor)
   AND (@activo IS NULL OR @activo = P.activo)
   order by p.apellido, p.nombre

GO
/****** Object:  StoredProcedure [dbo].[Tutor_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 26/06/11
-- =============================================
CREATE PROCEDURE [dbo].[Tutor_Update]
  @idTutor NUMERIC = NULL
 ,@idTutorTransaccional NUMERIC = NULL
 ,@idPersona NUMERIC = NULL
 ,@telefonoTrabajo VARCHAR(20) = NULL
 ,@idTipoTutor NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    UPDATE Tutor SET 
      idTutorTransaccional = @idTutorTransaccional
     ,idPersona = @idPersona
     ,telefonoTrabajo = @telefonoTrabajo
     ,idTipoTutor = @idTipoTutor
    WHERE (@idTutor IS NULL OR @idTutor=idTutor)
  

-- =====SELECT=================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[TutorAlumno_Delete]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 05/07/11
-- =============================================
CREATE PROCEDURE [dbo].[TutorAlumno_Delete]
  @idAlumno NUMERIC = NULL
 ,@idTutor NUMERIC = NULL
 ,@idTutorAlumno NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    DELETE FROM TutorAlumno
    WHERE (@idAlumno IS NULL OR @idAlumno=idAlumno)
   AND (@idTutor IS NULL OR @idTutor=idTutor)
   AND (@idTutorAlumno IS NULL OR @idTutorAlumno=idTutorAlumno)

GO
/****** Object:  StoredProcedure [dbo].[TutorAlumno_Exists]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 05/07/11
-- =============================================
CREATE PROCEDURE [dbo].[TutorAlumno_Exists]
  @idAlumno NUMERIC = NULL
 ,@idTutor NUMERIC = NULL
 ,@idTutorAlumno NUMERIC = NULL
 ,@exists BIT OUT
AS
 SET NOCOUNT OFF;

    IF EXISTS (
 SELECT idTutorAlumno
    FROM TutorAlumno
    WHERE (@idAlumno IS NULL OR @idAlumno=idAlumno)
   AND (@idTutor IS NULL OR @idTutor=idTutor)
   AND (@idTutorAlumno IS NULL OR @idTutorAlumno=idTutorAlumno)
 )
 SET @exists = 1
 ELSE SET @exists = 0

GO
/****** Object:  StoredProcedure [dbo].[TutorAlumno_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 05/07/11
-- =============================================
CREATE PROCEDURE [dbo].[TutorAlumno_Insert]
  @idAlumno NUMERIC = NULL
 ,@idTutor NUMERIC = NULL
 ,@idTutorAlumno NUMERIC = NULL

AS
 SET NOCOUNT OFF;

Select @idTutor= idTutor
from [Tutor]
where idTutorTransaccional = @idTutor

Select @idAlumno= idAlumno
from [Alumnos]
where idAlumnoTransaccional = @idAlumno

Select @idTutorAlumno= idTutorAlumno
from [TutorAlumno]
where idTutor = @idTutor
and idAlumno = @idAlumno

if (@idTutorAlumno is null OR @idTutorAlumno = 0)
	begin	
		INSERT INTO TutorAlumno(
		 idAlumno
		,idTutor
	 )
	 VALUES (
	 @idAlumno
	 ,@idTutor
	 )
	 SET @idTutorAlumno = @@IDENTITY
	end
 else
	 begin
		Execute [TutorAlumno_Update]
		 @idAlumno
		,@idTutor
		,@idTutorAlumno
	 end
-- =====DELETE==================================
SET ANSI_NULLS ON

GO
/****** Object:  StoredProcedure [dbo].[TutorAlumno_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 05/07/11
-- =============================================
CREATE PROCEDURE [dbo].[TutorAlumno_Select]
  @idAlumno NUMERIC = NULL
 ,@idTutor NUMERIC = NULL
 ,@idTutorAlumno NUMERIC = NULL
 ,@username VARCHAR(50) = NULL
 ,@idCurso NUMERIC = NULL
 ,@idCicloLectivo NUMERIC = NULL


AS
 SET NOCOUNT OFF;

    SELECT TA.idAlumno
		,TA.idTutor
		,TA.idTutorAlumno
		,PA.nombre
		,PA.apellido
		,PA.username
    FROM TutorAlumno AS TA
		INNER JOIN Tutor AS T ON TA.idTutor = T.idTutor
		INNER JOIN Personas AS PT ON T.idPersona = PT.idPersona
		INNER JOIN Alumnos AS A ON TA.idAlumno = A.idAlumno
		INNER JOIN Personas PA ON A.idPersona = PA.idPersona
		
		INNER JOIN [AlumnoCursoCicloLectivo] AS AC 
			ON A.idAlumno = AC.idAlumno
		INNER JOIN [CursosCicloLectivo] AS CCL 
			ON AC.idCursoCicloLectivo = CCL.idCursoCicloLectivo
		INNER JOIN CicloLectivo AS CL
			ON CCL.idCicloLectivo = CL.idCicloLectivo
		INNER JOIN Curso AS C
			ON CCL.idCurso = C.idCurso
		INNER JOIN Nivel AS N
			ON C.idNivel = N.idNivel
	
    WHERE (@idAlumno IS NULL OR @idAlumno=TA.idAlumno)
		AND (@idTutor IS NULL OR @idTutor=TA.idTutor)
		AND (@idTutorAlumno IS NULL OR @idTutorAlumno=TA.idTutorAlumno)
		AND (@username IS NULL OR @username=PT.username)
		AND (@idCurso IS NULL OR @idCurso = CCL.idCursoCicloLectivo)
		AND (@idCicloLectivo IS NULL OR @idCicloLectivo = CCL.idCicloLectivo)



GO
/****** Object:  StoredProcedure [dbo].[TutorAlumno_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo Nicoliello
-- Create date: 05/07/11
-- =============================================
CREATE PROCEDURE [dbo].[TutorAlumno_Update]
  @idAlumno NUMERIC = NULL
 ,@idTutor NUMERIC = NULL
 ,@idTutorAlumno NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    UPDATE TutorAlumno SET 
      idAlumno = @idAlumno
     ,idTutor = @idTutor
    WHERE (@idAlumno IS NULL OR @idAlumno=idAlumno)
   AND (@idTutor IS NULL OR @idTutor=idTutor)
   AND (@idTutorAlumno IS NULL OR @idTutorAlumno=idTutorAlumno)

GO
/****** Object:  StoredProcedure [dbo].[TutorAlumnosPorCurso_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 12/06/11
-- =============================================
CREATE PROCEDURE [dbo].[TutorAlumnosPorCurso_Select]
  @idCurso NUMERIC = NULL
 ,@idAlumno NUMERIC = NULL
 ,@idPersona NUMERIC = NULL
 ,@legajo VARCHAR(50) = NULL
 ,@fechaAlta DATE = NULL
 ,@fechaBaja DATE = NULL
 ,@username VARCHAR(50) = NULL
 ,@usernameTutor VARCHAR(50) = NULL
 ,@idCicloLectivo NUMERIC = NULL
AS
 SET NOCOUNT OFF;

    SELECT distinct A.idAlumno
		,P.apellido
		,P.nombre
		,A.idPersona
		,A.legajo
		,A.fechaAlta
		,A.fechaBaja
		,P.activo
		,CCL.idCursoCicloLectivo
		,N.nombre AS 'nivel'
		,C.nombre AS 'curso'		
    FROM Alumnos A
		INNER JOIN [AlumnoCursoCicloLectivo] AS AC 
			ON A.idAlumno = AC.idAlumno
		INNER JOIN [Personas] AS P 
			ON A.idPersona = P.idPersona
		INNER JOIN [CursosCicloLectivo] AS CCL 
			ON AC.idCursoCicloLectivo = CCL.idCursoCicloLectivo
		INNER JOIN CicloLectivo AS CL
			ON CCL.idCicloLectivo = CL.idCicloLectivo
		INNER JOIN TutorAlumno AS TA 
			ON A.idAlumno = TA.idAlumno
		INNER JOIN Tutor AS T 
			ON TA.idTutor = T.idTutor
		INNER JOIN Personas AS PT
			ON T.idPersona = PT.idPersona
		INNER JOIN Curso AS C
			ON CCL.idCurso = C.idCurso
		INNER JOIN Nivel AS N
			ON C.idNivel = N.idNivel
    WHERE (@idAlumno IS NULL OR @idAlumno=A.idAlumno)
		AND (@idPersona IS NULL OR @idPersona=A.idPersona)
		AND (@legajo IS NULL OR @legajo=A.legajo)
		AND (@fechaAlta IS NULL OR @fechaAlta=A.fechaAlta)
		AND (@fechaBaja IS NULL OR @fechaBaja=A.fechaBaja)
		AND (@idCurso IS NULL OR @idCurso = CCL.idCursoCicloLectivo)
		AND (@idCicloLectivo IS NULL OR @idCicloLectivo = CCL.idCicloLectivo)
		AND (@username IS NULL OR @username = P.username)
		AND (@usernameTutor IS NULL OR @usernameTutor = PT.username)

GO
/****** Object:  StoredProcedure [dbo].[TutoresPorCurso_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  LAURA
-- Create date: 27/08/11
-- =============================================
CREATE PROCEDURE [dbo].[TutoresPorCurso_Select]
  @idCursoCicloLectivo NUMERIC = NULL
 ,@idCicloLectivo NUMERIC = NULL
 ,@activo BIT = NULL
AS
 SET NOCOUNT OFF;
 
 IF @idCicloLectivo IS NULL
BEGIN
	SELECT @idCicloLectivo = idCicloLectivo
	FROM CicloLectivo Where activo = 1
END 
 
	SELECT DISTINCT 
	TutorAlumno.idTutor, 
	Personas.idPersona,
	Personas.nombre, 
	Personas.apellido
	FROM 
		Alumnos 
		INNER JOIN AlumnoCursoCicloLectivo ON Alumnos.idAlumno = AlumnoCursoCicloLectivo.idAlumno 
		INNER JOIN CursosCicloLectivo AS CCL  ON AlumnoCursoCicloLectivo.idCursoCicloLectivo = CCL.idCursoCicloLectivo
		INNER JOIN TutorAlumno ON Alumnos.idAlumno = TutorAlumno.idAlumno 
		INNER JOIN Tutor ON TutorAlumno.idTutor = Tutor.idTutor 
		INNER JOIN Personas ON Tutor.idPersona = Personas.idPersona 
	WHERE
		(@activo IS NULL OR @activo = Personas.activo)
		AND
		(@idCursoCicloLectivo IS NULL OR @idCursoCicloLectivo = CCL.idCursoCicloLectivo)
		AND
		(@idCicloLectivo IS NULL OR @idCicloLectivo = CCL.idCicloLectivo)

GO
/****** Object:  StoredProcedure [dbo].[ValorEscalaPonderacion_Delete]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo
-- Create date: 26/03/13
-- idEscalaPonderacion es mandatorio
-- =============================================
CREATE PROCEDURE [dbo].[ValorEscalaPonderacion_Delete]
  @idValorEscalaPonderacion NUMERIC = NULL
	,@idEscalaPonderacion NUMERIC = NULL

AS
 SET NOCOUNT OFF;

DECLARE @ordenBorrado INT = 0;

IF @idValorEscalaPonderacion <> 0
	BEGIN	
		SELECT @ordenBorrado = orden
		FROM ValorEscalaPonderacion
		WHERE @idValorEscalaPonderacion=idValorEscalaPonderacion
	END

	DELETE FROM ValorEscalaPonderacion
	WHERE
		@idEscalaPonderacion = idEscalaPonderacion
		AND (@idValorEscalaPonderacion IS NULL OR @idValorEscalaPonderacion=idValorEscalaPonderacion)

IF @idValorEscalaPonderacion <> 0
	BEGIN	
		UPDATE ValorEscalaPonderacion
		SET orden = orden - 1
		WHERE orden > @ordenBorrado
	END
GO
/****** Object:  StoredProcedure [dbo].[ValorEscalaPonderacion_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo
-- Create date: 18/03/13
-- =============================================
CREATE PROCEDURE [dbo].[ValorEscalaPonderacion_Insert]
 @nombre VARCHAR(250) = NULL
 ,@descripcion VARCHAR(4000) = NULL
 ,@idEscalaPonderacion NUMERIC = NULL
 ,@idValorEscalaPonderacion NUMERIC = NULL OUTPUT
AS
 SET NOCOUNT OFF;

    INSERT INTO ValorEscalaPonderacion(
     nombre
		,descripcion
		,idEscalaPonderacion
		,orden
 )
 VALUES(
    @nombre 
		,@descripcion
		,@idEscalaPonderacion
		,(SELECT COUNT(*) FROM ValorEscalaPonderacion WHERE idEscalaPonderacion=@idEscalaPonderacion)+1
 )
SET @idValorEscalaPonderacion = @@IDENTITY
GO
/****** Object:  StoredProcedure [dbo].[ValorEscalaPonderacion_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo
-- Create date: 18/03/13
-- =============================================
CREATE PROCEDURE [dbo].[ValorEscalaPonderacion_Select]
 @idEscalaPonderacion NUMERIC = NULL
AS
 SET NOCOUNT OFF;
DECLARE @cantidad NUMERIC
/*
	SELECT @cantidad = 
			COUNT(*)
			FROM Encuesta AS E
			INNER JOIN Pregunta AS P ON E.idEncuesta = P.idEncuesta
			INNER JOIN CategoriaPregunta AS CP ON CP.idCategoria=P.idCategoria
			WHERE @idEscalaPonderacion = P.idEscalaPonderacion
*/

	SELECT 
		 idValorEscalaPonderacion
		,nombre
		,descripcion
		,idEscalaPonderacion
		,orden
		,(SELECT COUNT(*)
			FROM ValorEscalaPonderacion
			WHERE idEscalaPonderacion = @idEscalaPonderacion) as 'cantidadValores'
		,(SELECT COUNT(E.idencuesta)
			FROM Encuesta AS E
			INNER JOIN Pregunta AS P ON E.idEncuesta = P.idEncuesta AND ValorEscalaPonderacion.idEscalaPonderacion = P.idEscalaPonderacion
			INNER JOIN CategoriaPregunta AS CP ON CP.idCategoria=P.idCategoria ) as 'cantidadEncuestas'
	FROM ValorEscalaPonderacion
	WHERE idEscalaPonderacion = @idEscalaPonderacion
	ORDER BY orden

GO
/****** Object:  StoredProcedure [dbo].[ValorEscalaPonderacion_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo
-- Create date: 18/03/13
-- =============================================
CREATE PROCEDURE [dbo].[ValorEscalaPonderacion_Update]
	@idValorEscalaPonderacion NUMERIC = NULL
	,@nombre VARCHAR(250) = NULL
	,@descripcion VARCHAR(4000) = NULL
	,@idEscalaPonderacion NUMERIC = NULL
	,@orden NUMERIC = NULL

AS
 SET NOCOUNT OFF;

    UPDATE ValorEscalaPonderacion
		SET
			nombre = @nombre
			,descripcion = @descripcion
			,idEscalaPonderacion = @idEscalaPonderacion
			,orden = @orden
		WHERE
			idValorEscalaPonderacion = @idValorEscalaPonderacion
			AND idEscalaPonderacion = @idEscalaPonderacion
GO
/****** Object:  StoredProcedure [dbo].[ValoresEscalaCalificacion_Delete]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Pablo
-- Create date: 26/03/13
-- =============================================
CREATE PROCEDURE [dbo].[ValoresEscalaCalificacion_Delete]
  @idValorEscalaCalificacion NUMERIC = NULL

AS
 SET NOCOUNT OFF;

DELETE FROM ValoresEscalaCalificacion
WHERE (@idValorEscalaCalificacion IS NULL OR @idValorEscalaCalificacion=idValorEscalaCalificacion)
GO
/****** Object:  StoredProcedure [dbo].[ValoresEscalaCalificacion_Exists]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 12/06/11
-- =============================================
CREATE PROCEDURE [dbo].[ValoresEscalaCalificacion_Exists]
  @idValorEscalaCalificacion NUMERIC = NULL
 ,@idValorEscalaCalificacionTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@descripcion VARCHAR(250) = NULL
 ,@valor VARCHAR(50) = NULL
 ,@activo BIT = NULL
 ,@aprobado BIT = NULL
 ,@exists BIT OUT
AS
 SET NOCOUNT OFF;

    IF EXISTS (
 SELECT idValorEscalaCalificacion
    FROM ValoresEscalaCalificacion
    WHERE (@idValorEscalaCalificacion IS NULL OR @idValorEscalaCalificacion=idValorEscalaCalificacion)
   AND (@idValorEscalaCalificacionTransaccional IS NULL OR @idValorEscalaCalificacionTransaccional=idValorEscalaCalificacionTransaccional)
   AND (@nombre IS NULL OR @nombre=nombre)
   AND (@descripcion IS NULL OR @descripcion=descripcion)
   AND (@valor IS NULL OR @valor=valor)
   AND (@activo IS NULL OR @activo=activo)
   AND (@aprobado IS NULL OR @aprobado=aprobado)
 )
 SET @exists = 1
 ELSE SET @exists = 0

GO
/****** Object:  StoredProcedure [dbo].[ValoresEscalaCalificacion_Insert]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 12/06/11
-- =============================================
CREATE PROCEDURE [dbo].[ValoresEscalaCalificacion_Insert]
  @idValorEscalaCalificacion NUMERIC = NULL output
 ,@idValorEscalaCalificacionTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@descripcion VARCHAR(250) = NULL
 ,@valor VARCHAR(50) = NULL
 ,@activo BIT = NULL
 ,@aprobado BIT = NULL
AS
 SET NOCOUNT OFF;

  SELECT @idValorEscalaCalificacion=idValorEscalaCalificacion 
  FROM [dbo].[ValoresEscalaCalificacion]
  WHERE [idValorEscalaCalificacionTransaccional]= @idValorEscalaCalificacionTransaccional
  
  IF (@idValorEscalaCalificacion=0 or @idValorEscalaCalificacion is null)
  BEGIN

    INSERT INTO ValoresEscalaCalificacion(
    idValorEscalaCalificacionTransaccional
    ,nombre
    ,descripcion
    ,valor
    ,activo
    ,aprobado
 )
 VALUES(
     @idValorEscalaCalificacionTransaccional
    ,@nombre
    ,@descripcion
    ,@valor
    ,@activo
    ,@aprobado
 )
 SET @idValorEscalaCalificacion = @@IDENTITY
END
ELSE
BEGIN
	EXECUTE [ValoresEscalaCalificacion_Update] 
	   @idValorEscalaCalificacion
	  ,@idValorEscalaCalificacionTransaccional
	  ,@nombre
	  ,@descripcion
	  ,@valor
	  ,@activo
	  ,@aprobado
END

GO
/****** Object:  StoredProcedure [dbo].[ValoresEscalaCalificacion_Select]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 12/06/11
-- =============================================
CREATE PROCEDURE [dbo].[ValoresEscalaCalificacion_Select]
  @idValorEscalaCalificacion NUMERIC = NULL
 ,@idValorEscalaCalificacionTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@descripcion VARCHAR(250) = NULL
 ,@valor VARCHAR(50) = NULL
 ,@activo BIT = NULL
 ,@aprobado BIT = NULL
AS
 SET NOCOUNT OFF;

    SELECT idValorEscalaCalificacion
    ,idValorEscalaCalificacionTransaccional
    ,nombre
    ,descripcion
    ,valor
    ,activo
    ,aprobado
    FROM ValoresEscalaCalificacion
    WHERE (@idValorEscalaCalificacion IS NULL OR @idValorEscalaCalificacion=idValorEscalaCalificacion)
   AND (@idValorEscalaCalificacionTransaccional IS NULL OR @idValorEscalaCalificacionTransaccional=idValorEscalaCalificacionTransaccional)
   AND (@nombre IS NULL OR @nombre=nombre)
   AND (@descripcion IS NULL OR @descripcion=descripcion)
   AND (@valor IS NULL OR @valor=valor)
   AND (@activo IS NULL OR @activo=activo)
   AND (@aprobado IS NULL OR @aprobado=aprobado)

GO
/****** Object:  StoredProcedure [dbo].[ValoresEscalaCalificacion_Update]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TU_NOMBRE
-- Create date: 12/06/11
-- =============================================
CREATE PROCEDURE [dbo].[ValoresEscalaCalificacion_Update]
  @idValorEscalaCalificacion NUMERIC = NULL
 ,@idValorEscalaCalificacionTransaccional NUMERIC = NULL
 ,@nombre VARCHAR(50) = NULL
 ,@descripcion VARCHAR(250) = NULL
 ,@valor VARCHAR(50) = NULL
 ,@activo BIT = NULL
 ,@aprobado BIT = NULL
AS
 SET NOCOUNT OFF;

    UPDATE ValoresEscalaCalificacion SET 
      idValorEscalaCalificacionTransaccional = @idValorEscalaCalificacionTransaccional
     ,nombre = @nombre
     ,descripcion = @descripcion
     ,valor = @valor
     ,activo = @activo
     ,aprobado = @aprobado
    WHERE (@idValorEscalaCalificacion IS NULL OR @idValorEscalaCalificacion=idValorEscalaCalificacion)
   AND (@idValorEscalaCalificacionTransaccional IS NULL OR @idValorEscalaCalificacionTransaccional=idValorEscalaCalificacionTransaccional)
   AND (@nombre IS NULL OR @nombre=nombre)
   AND (@descripcion IS NULL OR @descripcion=descripcion)
   AND (@valor IS NULL OR @valor=valor)
   AND (@activo IS NULL OR @activo=activo)
   AND (@aprobado IS NULL OR @aprobado=aprobado)

GO
/****** Object:  UserDefinedFunction [dbo].[ArrayToTable]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[ArrayToTable] (@delimStr NVARCHAR(max))
RETURNS 

@StrValTable TABLE 
(
    -- Add the column definitions for the TABLE variable here
    StrVal VARCHAR(20) 
)
AS
BEGIN
    -- Fill the table variable with the rows for your result set
    DECLARE @strlist NVARCHAR(max), @pos INT, @delim CHAR, @lstr NVARCHAR(max)
    SET @strlist = ISNULL(@delimStr,'')
    SET @delim = ','

    WHILE ((len(@strlist) > 0) and (@strlist <> ''))
    BEGIN
        SET @pos = charindex(@delim, @strlist)
        
        IF @pos > 0
           BEGIN
              SET @lstr = substring(@strlist, 1, @pos-1)
              SET @strlist = ltrim(substring(@strlist,charindex(@delim, @strlist)+1, 8000))
           END
        ELSE
           BEGIN
              SET @lstr = @strlist
              SET @strlist = ''
           END

        INSERT @StrValTable VALUES (@lstr)
    END
        RETURN 
    END

GO
/****** Object:  UserDefinedFunction [dbo].[PorcentajePlanificacion_Calculo]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LAURA
-- Create date: 16/09/2013
-- Description:	Calcular porcentaje de Cobertura de Planificación
-- =============================================
CREATE FUNCTION [dbo].[PorcentajePlanificacion_Calculo]
(
	-- Add the parameters for the function here
	@idCurricula INT
)
RETURNS DECIMAL(3,0)
AS
BEGIN
	DECLARE @Saldo DECIMAL(3,0), 
	@Return DECIMAL(3,0)

	DECLARE @dictados int = 0
	DECLARE @planificados int = 0

	select @planificados = count(distinct TC.idTemaContenido) 
	from TemaContenido as TC
		INNER JOIN Contenido as C on TC.idContenido = C.idContenido and C.idCurricula = @idCurricula 
		and C.activo = 1
		and TC.activo = 1

	select @dictados = count(distinct TC.idTemaContenido)
	from TemaPlanificacionTemaContenido AS TPTC 
		inner join TemaContenido AS TC on TPTC.idTemaContenido = TC.idTemaContenido and TPTC.obligatorio = 1
		inner join Contenido as C on tc.idContenido = C.idContenido and C.idCurricula = @idCurricula

	if (@planificados > 0 )
		SET @Return = (@dictados * 100 / @planificados)
	else
		SET @Return = 0

	RETURN @Return

END

GO
/****** Object:  Table [dbo].[Accesos]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Accesos](
	[idAcceso] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[idPagina] [numeric](18, 0) NOT NULL,
	[fecha] [date] NOT NULL,
	[hora] [time](7) NOT NULL,
	[username] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Accesos] PRIMARY KEY CLUSTERED 
(
	[idAcceso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AgendaActividades]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AgendaActividades](
	[idAgendaActividad] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[idCursoCicloLectivo] [numeric](18, 0) NOT NULL,
	[descripcion] [varchar](250) NULL,
	[fechaCreacion] [date] NOT NULL,
	[activo] [bit] NOT NULL,
 CONSTRAINT [PK_AgendaActividades] PRIMARY KEY CLUSTERED 
(
	[idAgendaActividad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AlumnoCursoCicloLectivo]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AlumnoCursoCicloLectivo](
	[idAlumnoCursoCicloLectivo] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[idAlumnoCursoCicloLectivoTransaccional] [numeric](18, 0) NOT NULL,
	[idAlumno] [numeric](18, 0) NOT NULL,
	[idCursoCicloLectivo] [numeric](18, 0) NOT NULL,
 CONSTRAINT [PK_AlumnoCursoCicloLectivo] PRIMARY KEY CLUSTERED 
(
	[idAlumnoCursoCicloLectivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Alumnos]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Alumnos](
	[idAlumno] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[idAlumnoTransaccional] [numeric](18, 0) NULL,
	[idPersona] [numeric](18, 0) NOT NULL,
	[legajo] [varchar](50) NULL,
	[fechaAlta] [date] NULL,
	[fechaBaja] [date] NULL,
	[esEmancipado] [bit] NULL,
 CONSTRAINT [PK_Alumnos] PRIMARY KEY CLUSTERED 
(
	[idAlumno] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AmbitoEncuesta]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AmbitoEncuesta](
	[idAmbito] [numeric](18, 0) NOT NULL,
	[nombre] [varchar](100) NULL,
	[descripcion] [varchar](4000) NULL,
PRIMARY KEY CLUSTERED 
(
	[idAmbito] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Asignatura]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Asignatura](
	[idAsignatura] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[idAsignaturaTransaccional] [numeric](18, 0) NULL,
	[nombre] [varchar](80) NULL,
 CONSTRAINT [PK_Asignatura] PRIMARY KEY CLUSTERED 
(
	[idAsignatura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AsignaturaCicloLectivo]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AsignaturaCicloLectivo](
	[idAsignaturaCicloLectivo] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[idAsignaturaCicloLectivoTransaccional] [numeric](18, 0) NOT NULL,
	[idAsignatura] [numeric](18, 0) NOT NULL,
	[idDocente] [numeric](18, 0) NOT NULL,
	[idCursoCicloLectivo] [numeric](18, 0) NOT NULL,
	[idHorario] [numeric](18, 0) NULL,
 CONSTRAINT [PK_AsignaturaCurso] PRIMARY KEY CLUSTERED 
(
	[idAsignaturaCicloLectivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AsignaturaNivel]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AsignaturaNivel](
	[idAsignaturaNivel] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[idAsignaturaNivelTransaccional] [numeric](18, 0) NOT NULL,
	[idAsignatura] [numeric](18, 0) NOT NULL,
	[idNivel] [numeric](18, 0) NOT NULL,
	[idOrientacion] [numeric](18, 0) NOT NULL,
	[cargaHoraria] [nchar](10) NOT NULL,
 CONSTRAINT [PK_AsignaturaNivel] PRIMARY KEY CLUSTERED 
(
	[idAsignaturaNivel] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Asistencia]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Asistencia](
	[idAsistencia] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[idAsistenciaTransaccional] [numeric](18, 0) NOT NULL,
	[idAlumnoCursoCicloLectivo] [numeric](18, 0) NOT NULL,
	[idTipoAsistencia] [int] NULL,
	[fecha] [date] NOT NULL,
	[fechaCarga] [date] NULL,
 CONSTRAINT [PK_Asistencia] PRIMARY KEY CLUSTERED 
(
	[idAsistencia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BibliografiaRecomendada]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BibliografiaRecomendada](
	[idBibliografia] [numeric](18, 0) NOT NULL,
	[tituloLibro] [varchar](100) NULL,
	[isbn] [varchar](50) NULL,
	[autor] [varchar](50) NULL,
	[edicion] [varchar](50) NULL,
 CONSTRAINT [PK_BibliografiaRecomendada] PRIMARY KEY CLUSTERED 
(
	[idBibliografia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Calificacion]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Calificacion](
	[idCalificacion] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[idCalificacionTransaccional] [numeric](18, 0) NOT NULL,
	[observaciones] [nvarchar](50) NULL,
	[fecha] [date] NULL,
	[idValorCalificacion] [numeric](18, 0) NULL,
	[idAlumnoCursoCicloLectivo] [numeric](18, 0) NULL,
	[idAsignaturaCicloLectivo] [numeric](18, 0) NULL,
	[idPeriodo] [numeric](18, 0) NULL,
	[idInstanciaCalificacion] [numeric](18, 0) NULL,
 CONSTRAINT [PK_Calificacion] PRIMARY KEY CLUSTERED 
(
	[idCalificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CargosPersonal]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CargosPersonal](
	[idCargoPersonal] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[idCargoPersonalTransaccional] [numeric](18, 0) NULL,
	[nombre] [varchar](50) NOT NULL,
	[descripcion] [varchar](250) NULL,
	[activo] [bit] NOT NULL,
 CONSTRAINT [PK_CargosPersonal] PRIMARY KEY CLUSTERED 
(
	[idCargoPersonal] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CategoriaPregunta]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CategoriaPregunta](
	[idCategoria] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](250) NULL,
	[descripcion] [varchar](4000) NULL,
	[idAmbito] [numeric](18, 0) NULL,
 CONSTRAINT [PK_CategoriaPregunta] PRIMARY KEY CLUSTERED 
(
	[idCategoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CicloLectivo]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CicloLectivo](
	[idCicloLectivo] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[idCicloLectivoTransaccional] [numeric](18, 0) NULL,
	[nombre] [varchar](50) NULL,
	[fechaInicio] [date] NULL,
	[fechaFin] [date] NULL,
	[activo] [bit] NOT NULL,
 CONSTRAINT [PK_CicloLectivo] PRIMARY KEY CLUSTERED 
(
	[idCicloLectivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Citacion]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Citacion](
	[idCitacion] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[fecha] [date] NOT NULL,
	[hora] [time](7) NOT NULL,
	[idOrganizador] [numeric](18, 0) NOT NULL,
	[idMotivoCitacion] [numeric](18, 0) NOT NULL,
	[idTutor] [numeric](18, 0) NOT NULL,
	[detalle] [varchar](250) NOT NULL,
	[activo] [bit] NOT NULL,
	[idCurso] [numeric](18, 0) NULL,
 CONSTRAINT [PK_Citacion] PRIMARY KEY CLUSTERED 
(
	[idCitacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Configuraciones]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Configuraciones](
	[idConfiguracion] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](50) NOT NULL,
	[descripcion] [nvarchar](250) NULL,
	[valor] [nvarchar](250) NOT NULL,
	[activo] [bit] NOT NULL,
 CONSTRAINT [PK_EDUAR_Configuraciones] PRIMARY KEY CLUSTERED 
(
	[idConfiguracion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ConfiguracionesEstablecimiento]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConfiguracionesEstablecimiento](
	[idConfiguracionEstablecimiento] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](50) NOT NULL,
	[descripcion] [nvarchar](250) NULL,
	[valor] [nvarchar](250) NOT NULL,
	[activo] [bit] NOT NULL,
 CONSTRAINT [PK_ConfiguracionesEstablecimiento] PRIMARY KEY CLUSTERED 
(
	[idConfiguracionEstablecimiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Contenido]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Contenido](
	[idContenido] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[idCurricula] [numeric](18, 0) NULL,
	[descripcion] [varchar](50) NULL,
	[activo] [bit] NOT NULL,
	[fechaBaja] [date] NULL,
	[idPersonaBaja] [numeric](18, 0) NULL,
 CONSTRAINT [PK_Contenido] PRIMARY KEY CLUSTERED 
(
	[idContenido] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Curricula]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Curricula](
	[idCurricula] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[fechaAlta] [date] NOT NULL,
	[fechaModificacion] [date] NULL,
	[idPersonaAlta] [numeric](18, 0) NOT NULL,
	[idPersonaModificacion] [numeric](18, 0) NULL,
	[idAsignatura] [numeric](18, 0) NOT NULL,
	[idNivel] [numeric](18, 0) NOT NULL,
	[idOrientacion] [numeric](18, 0) NOT NULL,
 CONSTRAINT [PK_Curricula] PRIMARY KEY CLUSTERED 
(
	[idCurricula] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Curso]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Curso](
	[idCurso] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[idCursoTransaccional] [numeric](18, 0) NULL,
	[nombre] [varchar](50) NULL,
	[idNivel] [numeric](18, 0) NULL,
 CONSTRAINT [PK_Curso] PRIMARY KEY CLUSTERED 
(
	[idCurso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CursosCicloLectivo]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CursosCicloLectivo](
	[idCursoCicloLectivo] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[idCursoCicloLectivoTransaccional] [numeric](18, 0) NULL,
	[idCurso] [numeric](18, 0) NOT NULL,
	[idCicloLectivo] [numeric](18, 0) NOT NULL,
	[idPreceptor] [numeric](18, 0) NULL,
	[idOrientacion] [numeric](18, 0) NULL,
 CONSTRAINT [PK_CursosCicloLectivo] PRIMARY KEY CLUSTERED 
(
	[idCursoCicloLectivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[DetalleRegistroClases]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetalleRegistroClases](
	[idDetalleRegistroClases] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[idRegistroClases] [numeric](18, 0) NOT NULL,
	[idTemaContenido] [numeric](18, 0) NOT NULL,
	[porcentaje] [int] NULL,
 CONSTRAINT [PK_DetalleRegistroClases] PRIMARY KEY CLUSTERED 
(
	[idDetalleRegistroClases] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DiaHorario]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DiaHorario](
	[idDiaHorario] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[idDiaSemana] [numeric](18, 0) NOT NULL,
	[idDiaHorarioTransaccional] [numeric](18, 0) NOT NULL,
	[idAsignaturaCicloLectivo] [numeric](18, 0) NOT NULL,
 CONSTRAINT [PK_DiaHorario] PRIMARY KEY CLUSTERED 
(
	[idDiaHorario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Encuesta]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Encuesta](
	[idEncuesta] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[responsable] [numeric](18, 0) NULL,
	[fechaCreacion] [date] NULL,
	[fechaModificacion] [date] NULL,
	[idAmbito] [numeric](18, 0) NOT NULL,
	[objetivo] [varchar](4000) NULL,
	[activa] [bit] NOT NULL,
	[nombre] [varchar](250) NULL,
	[fechaLanzamiento] [date] NULL,
	[fechaLimite] [date] NULL,
	[idCursoCicloLectivo] [numeric](18, 0) NULL,
	[idAsignaturaCicloLectivo] [numeric](18, 0) NULL,
 CONSTRAINT [PK_Encuesta] PRIMARY KEY CLUSTERED 
(
	[idEncuesta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EncuestaUsuario]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EncuestaUsuario](
	[idEncuesta] [numeric](18, 0) NOT NULL,
	[idPersona] [numeric](18, 0) NOT NULL,
	[fechaRespuesta] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idEncuesta] ASC,
	[idPersona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EscalaPonderacion]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EscalaPonderacion](
	[idEscalaPonderacion] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](250) NULL,
	[descripcion] [varchar](4000) NULL,
 CONSTRAINT [PK__EscalaPo__ACF460B233AA9866] PRIMARY KEY CLUSTERED 
(
	[idEscalaPonderacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EstadoNovedadAulica]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EstadoNovedadAulica](
	[idEstadoNovedadAulica] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NULL,
	[esFinal] [bit] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Evaluacion]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Evaluacion](
	[idEvaluacion] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[idAsignaturaCurso] [numeric](18, 0) NOT NULL,
	[idEvento] [numeric](18, 0) NOT NULL,
 CONSTRAINT [PK_Evaluacion] PRIMARY KEY CLUSTERED 
(
	[idEvaluacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EventoAgenda]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EventoAgenda](
	[idEventoAgenda] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[idAgendaActividades] [numeric](18, 0) NULL,
	[idOrganizador] [numeric](18, 0) NULL,
	[idTipoEvento] [numeric](18, 0) NULL,
	[fechaAlta] [date] NULL,
	[fechaModificacion] [date] NULL,
	[fechaEvento] [date] NULL,
	[descripcion] [varchar](4000) NULL,
	[activo] [bit] NULL,
 CONSTRAINT [PK_EventoAgenda] PRIMARY KEY CLUSTERED 
(
	[idEventoAgenda] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EventoInstitucional]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EventoInstitucional](
	[idEventoInstitucional] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[idTipoEvento] [numeric](18, 0) NOT NULL,
	[idPersona] [numeric](18, 0) NOT NULL,
	[fecha] [date] NOT NULL,
	[hora] [time](7) NOT NULL,
	[lugar] [varchar](50) NOT NULL,
	[titulo] [varchar](50) NOT NULL,
	[detalle] [varchar](4000) NOT NULL,
	[activo] [bit] NOT NULL,
 CONSTRAINT [PK_Evento_institucional] PRIMARY KEY CLUSTERED 
(
	[idEventoInstitucional] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Excursion]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Excursion](
	[idExcursion] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[idEvento] [numeric](18, 0) NULL,
	[horaDesde] [time](7) NULL,
	[horaHasta] [time](7) NULL,
	[destino] [varchar](250) NULL,
 CONSTRAINT [PK_Excursion] PRIMARY KEY CLUSTERED 
(
	[idExcursion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Feriados]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Feriados](
	[idFeriado] [numeric](18, 0) NOT NULL,
	[nombre] [varchar](250) NULL,
	[fecha] [date] NULL,
	[descripcion] [varchar](4000) NULL,
PRIMARY KEY CLUSTERED 
(
	[idFeriado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Indicadores]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Indicadores](
	[idIndicador] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[nombreSP] [varchar](50) NULL,
	[invertirEscala] [bit] NOT NULL,
	[parametroCantidad] [int] NULL,
	[diasHastaPrincipal] [int] NOT NULL,
	[diasHastaIntermedio] [int] NOT NULL,
	[diasHastaSecundario] [int] NOT NULL,
	[verdeNivelPrincipal] [int] NOT NULL,
	[verdeNivelIntermedio] [int] NOT NULL,
	[verdeNivelSecundario] [int] NOT NULL,
	[rojoNivelPrincipal] [int] NOT NULL,
	[rojoNivelIntermedio] [int] NOT NULL,
	[rojoNivelSecundario] [int] NOT NULL,
 CONSTRAINT [PK_Indicadores] PRIMARY KEY CLUSTERED 
(
	[idIndicador] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Localidades]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Localidades](
	[idLocalidad] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[idLocalidadTransaccional] [numeric](18, 0) NULL,
	[nombre] [varchar](50) NOT NULL,
	[descripcion] [varchar](250) NULL,
	[idProvincia] [numeric](18, 0) NOT NULL,
	[activo] [bit] NOT NULL,
 CONSTRAINT [PK_Localidades] PRIMARY KEY CLUSTERED 
(
	[idLocalidad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Mensaje]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Mensaje](
	[idMensaje] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[idPersonaRemitente] [numeric](18, 0) NOT NULL,
	[idCursoCicloLectivo] [numeric](18, 0) NULL,
	[asuntoMensaje] [varchar](100) NULL,
	[textoMensaje] [varchar](5000) NOT NULL,
	[fechaEnvio] [date] NOT NULL,
	[horaEnvio] [time](7) NOT NULL,
	[activo] [bit] NOT NULL,
 CONSTRAINT [PK_Mensaje] PRIMARY KEY CLUSTERED 
(
	[idMensaje] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MensajeDestinatarios]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MensajeDestinatarios](
	[idMensajeDestinatario] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[idMensaje] [numeric](18, 0) NOT NULL,
	[idPersonaDestinatario] [numeric](18, 0) NOT NULL,
	[leido] [bit] NOT NULL,
	[activo] [bit] NOT NULL,
 CONSTRAINT [PK_MensajeDestinatarios] PRIMARY KEY CLUSTERED 
(
	[idMensajeDestinatario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Modulo]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Modulo](
	[idModulo] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[horaInicio] [time](7) NOT NULL,
	[horaFinalizacion] [time](7) NOT NULL,
	[idDiaHorario] [numeric](18, 0) NULL,
 CONSTRAINT [PK_Modulo] PRIMARY KEY CLUSTERED 
(
	[idModulo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MotivoCitacion]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MotivoCitacion](
	[idMotivoCitacion] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](50) NULL,
 CONSTRAINT [PK_Motivo_Citacion] PRIMARY KEY CLUSTERED 
(
	[idMotivoCitacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MotivoSancion]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MotivoSancion](
	[idMotivoSancion] [int] IDENTITY(1,1) NOT NULL,
	[idMotivoSancionTransaccional] [int] NULL,
	[descripcion] [varchar](250) NULL,
 CONSTRAINT [PK_MotivoSancion] PRIMARY KEY CLUSTERED 
(
	[idMotivoSancion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Nivel]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Nivel](
	[idNivel] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[idNivelTransaccional] [numeric](18, 0) NULL,
	[nombre] [varchar](50) NULL,
 CONSTRAINT [PK_Nivel] PRIMARY KEY CLUSTERED 
(
	[idNivel] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[NovedadAulica]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NovedadAulica](
	[idNovedadAulica] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[idNovedadAulicaPrincipal] [numeric](18, 0) NULL,
	[fecha] [date] NOT NULL,
	[hora] [time](7) NOT NULL,
	[idPersona] [numeric](18, 0) NOT NULL,
	[observaciones] [varchar](1000) NOT NULL,
	[idCursoCicloLectivo] [numeric](18, 0) NOT NULL,
	[idTipoNovedad] [numeric](18, 0) NULL,
	[idEstadoNovedad] [numeric](18, 0) NULL,
 CONSTRAINT [PK_NovedadAulica] PRIMARY KEY CLUSTERED 
(
	[idNovedadAulica] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Orientacion]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Orientacion](
	[idOrientacion] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[idOrientacionTransaccional] [numeric](18, 0) NULL,
	[nombre] [varchar](50) NULL,
	[descripcion] [varchar](250) NULL,
	[competencias] [varchar](200) NULL,
 CONSTRAINT [PK_Orientacion] PRIMARY KEY CLUSTERED 
(
	[idOrientacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Paginas]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Paginas](
	[idPagina] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[titulo] [varchar](50) NOT NULL,
	[url] [varchar](250) NULL,
 CONSTRAINT [PK_Paginas] PRIMARY KEY CLUSTERED 
(
	[idPagina] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Paises]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Paises](
	[idPais] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[idPaisTransaccional] [numeric](18, 0) NULL,
	[nombre] [varchar](50) NOT NULL,
	[descripcion] [varchar](250) NULL,
	[activo] [bit] NOT NULL,
 CONSTRAINT [PK_Paises] PRIMARY KEY CLUSTERED 
(
	[idPais] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Periodo]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Periodo](
	[idPeriodo] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[idPeriodoTransaccional] [numeric](18, 0) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[idCicloLectivo] [numeric](18, 0) NOT NULL,
	[fechaInicio] [date] NOT NULL,
	[fechaFin] [date] NOT NULL,
 CONSTRAINT [PK_Periodo] PRIMARY KEY CLUSTERED 
(
	[idPeriodo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Personal]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Personal](
	[idPersonal] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[idPersonalTransaccional] [numeric](18, 0) NULL,
	[idPersona] [numeric](18, 0) NOT NULL,
	[legajo] [varchar](50) NULL,
	[fechaAlta] [date] NULL,
	[fechaBaja] [date] NULL,
	[activo] [bit] NOT NULL,
	[idCargoPersonal] [numeric](18, 0) NOT NULL,
 CONSTRAINT [PK_Personal] PRIMARY KEY CLUSTERED 
(
	[idPersonal] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Personas]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Personas](
	[idPersona] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[apellido] [varchar](50) NOT NULL,
	[numeroDocumento] [numeric](18, 0) NOT NULL,
	[idTipoDocumento] [numeric](18, 0) NOT NULL,
	[domicilio] [varchar](250) NULL,
	[barrio] [varchar](50) NULL,
	[idLocalidad] [numeric](18, 0) NULL,
	[sexo] [char](1) NULL,
	[fechaNacimiento] [date] NULL,
	[telefonoFijo] [varchar](20) NULL,
	[telefonoCelular] [varchar](20) NULL,
	[telefonoCelularAlternativo] [varchar](20) NULL,
	[email] [varchar](50) NULL,
	[activo] [bit] NOT NULL,
	[username] [varchar](50) NULL,
	[idTipoPersona] [numeric](18, 0) NOT NULL,
 CONSTRAINT [PK_Personas] PRIMARY KEY CLUSTERED 
(
	[idPersona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PlanificacionAnual]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PlanificacionAnual](
	[idPlanificacionAnual] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[idCurricula] [numeric](18, 0) NULL,
	[idCreador] [numeric](18, 0) NULL,
	[idCicloLectivo] [numeric](18, 0) NULL,
	[fechaCreacion] [date] NULL,
	[fechaAprobada] [date] NULL,
	[solicitarAprobacion] [bit] NOT NULL,
	[observaciones] [varchar](50) NULL,
 CONSTRAINT [PK_PlanificacionAnual] PRIMARY KEY CLUSTERED 
(
	[idPlanificacionAnual] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Pregunta]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Pregunta](
	[idPregunta] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[textoPregunta] [varchar](250) NULL,
	[idCategoria] [numeric](18, 0) NULL,
	[objetivo] [varchar](4000) NULL,
	[idEncuesta] [numeric](18, 0) NOT NULL,
	[idEscalaPonderacion] [numeric](18, 0) NOT NULL,
	[peso] [numeric](18, 2) NULL,
 CONSTRAINT [PK_Pregunta] PRIMARY KEY CLUSTERED 
(
	[idPregunta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProcesosAutomaticos]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProcesosAutomaticos](
	[idProcesoAutomatico] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](50) NOT NULL,
	[descripcion] [nvarchar](250) NULL,
	[activo] [bit] NOT NULL,
	[emailNotificacion] [varchar](50) NULL,
 CONSTRAINT [PK_EDUAR_ProcesosAutomaticos] PRIMARY KEY CLUSTERED 
(
	[idProcesoAutomatico] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProcesosEjecutados]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProcesosEjecutados](
	[idProcesoEjecutado] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[fechaEjecucion] [datetime] NOT NULL,
	[resultado] [bit] NOT NULL,
	[descripcionError] [varchar](1000) NULL,
	[idProcesoAutomatico] [numeric](18, 0) NOT NULL,
 CONSTRAINT [PK_EDUAR_ProcesosEjecutados] PRIMARY KEY CLUSTERED 
(
	[idProcesoEjecutado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Provincias]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Provincias](
	[idProvincia] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[idProvinciaTransaccional] [numeric](18, 0) NULL,
	[nombre] [varchar](50) NOT NULL,
	[descripcion] [varchar](250) NULL,
	[idPais] [numeric](18, 0) NOT NULL,
	[activo] [bit] NOT NULL,
 CONSTRAINT [PK_Provincias] PRIMARY KEY CLUSTERED 
(
	[idProvincia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RegistroClases]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RegistroClases](
	[idRegistroClases] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[idEventoAgenda] [numeric](18, 0) NOT NULL,
	[idTipoRegistroClases] [numeric](18, 0) NOT NULL,
	[idAsignaturaCurso] [numeric](18, 0) NOT NULL,
	[observaciones] [varchar](4000) NULL,
 CONSTRAINT [PK_RegistroClases] PRIMARY KEY CLUSTERED 
(
	[idRegistroClases] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RelAmbitoRol]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RelAmbitoRol](
	[idAmbito] [numeric](18, 0) NOT NULL,
	[rolName] [varchar](50) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RelEncuestaRol]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RelEncuestaRol](
	[idEncuesta] [numeric](18, 0) NOT NULL,
	[rol] [varchar](50) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RelPlanificacionCurso]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RelPlanificacionCurso](
	[idPlanificacionAnual] [numeric](18, 0) NOT NULL,
	[idCursoCicloLectivo] [numeric](18, 0) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RespuestaPregunta]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RespuestaPregunta](
	[idRespuesta] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[idPregunta] [numeric](18, 0) NOT NULL,
	[idEncuesta] [numeric](18, 0) NOT NULL,
	[valorRespuestaSeleccion] [numeric](18, 0) NULL,
	[valorRespuestaTextual] [varchar](4000) NULL,
	[roleName] [varchar](50) NULL,
 CONSTRAINT [PK_RespuestaPregunta] PRIMARY KEY CLUSTERED 
(
	[idRespuesta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Reunion]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reunion](
	[idReunion] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[idEvento] [numeric](18, 0) NULL,
	[horario] [time](7) NULL,
 CONSTRAINT [PK_Reunion] PRIMARY KEY CLUSTERED 
(
	[idReunion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Sancion]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sancion](
	[idSancion] [int] IDENTITY(1,1) NOT NULL,
	[idSancionTransaccional] [int] NOT NULL,
	[cantidad] [int] NOT NULL,
	[fecha] [date] NOT NULL,
	[idMotivoSancion] [int] NOT NULL,
	[idTipoSancion] [int] NOT NULL,
	[idAlumnoCursoCicloLectivo] [numeric](18, 0) NULL,
	[fechaCarga] [date] NULL,
 CONSTRAINT [PK_Sancion] PRIMARY KEY CLUSTERED 
(
	[idSancion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TemaContenido]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TemaContenido](
	[idTemaContenido] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[idContenido] [numeric](18, 0) NOT NULL,
	[titulo] [varchar](100) NOT NULL,
	[detalle] [varchar](1000) NULL,
	[obligatorio] [bit] NOT NULL,
	[activo] [bit] NOT NULL,
	[fechaBaja] [date] NULL,
	[idPersonaBaja] [numeric](18, 0) NULL,
 CONSTRAINT [PK_TemaContenido] PRIMARY KEY CLUSTERED 
(
	[idTemaContenido] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TemaPlanificacionAnual]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TemaPlanificacionAnual](
	[idTemaPlanificacion] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[idPlanificacionAnual] [numeric](18, 0) NOT NULL,
	[contenidosConceptuales] [varchar](max) NULL,
	[contenidosProcedimentales] [varchar](max) NULL,
	[contenidosActitudinales] [varchar](max) NULL,
	[estrategiasAprendizaje] [varchar](max) NULL,
	[criteriosEvaluacion] [varchar](1000) NULL,
	[instrumentosEvaluacion] [varchar](1000) NULL,
	[fechaInicioEstimada] [date] NULL,
	[fechaFinEstimada] [date] NULL,
	[observaciones] [varchar](50) NULL,
 CONSTRAINT [PK_TemaPlanificacionAnual] PRIMARY KEY CLUSTERED 
(
	[idTemaPlanificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TemaPlanificacionTemaContenido]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TemaPlanificacionTemaContenido](
	[idTemaPlanificacion] [numeric](18, 0) NOT NULL,
	[idTemaContenido] [numeric](18, 0) NOT NULL,
	[obligatorio] [bit] NOT NULL,
 CONSTRAINT [PK_TemaPlanificacionTemaContenido] PRIMARY KEY CLUSTERED 
(
	[idTemaPlanificacion] ASC,
	[idTemaContenido] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TipoAsistencia]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TipoAsistencia](
	[idTipoAsistencia] [int] IDENTITY(1,1) NOT NULL,
	[idTipoAsistenciaTransaccional] [int] NULL,
	[descripcion] [varchar](50) NULL,
	[valor] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_TipoAsistencia] PRIMARY KEY CLUSTERED 
(
	[idTipoAsistencia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TipoDocumento]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TipoDocumento](
	[idTipoDocumento] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[idTipoDocumentoTransaccional] [numeric](18, 0) NULL,
	[nombre] [varchar](50) NOT NULL,
	[descripcion] [varchar](250) NULL,
	[activo] [bit] NOT NULL,
 CONSTRAINT [PK_TiposDocumentos] PRIMARY KEY CLUSTERED 
(
	[idTipoDocumento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TipoEvento]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TipoEvento](
	[idTipoEvento] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](250) NOT NULL,
 CONSTRAINT [PK_TipoEvento] PRIMARY KEY CLUSTERED 
(
	[idTipoEvento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TipoEventoInstitucional]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TipoEventoInstitucional](
	[idTipoEventoInstitucional] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](150) NULL,
	[activo] [bit] NULL,
 CONSTRAINT [PK_Tipo_evento_institucional] PRIMARY KEY CLUSTERED 
(
	[idTipoEventoInstitucional] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TipoNovedadAulica]    Script Date: 09/10/2013 19:34:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TipoNovedadAulica](
	[idTipoNovedadAulica] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](50) NULL,
 CONSTRAINT [PK_TipoNovedad] PRIMARY KEY CLUSTERED 
(
	[idTipoNovedadAulica] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TipoPersona]    Script Date: 09/10/2013 19:34:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TipoPersona](
	[idTipoPersona] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[descripcion] [varchar](250) NULL,
	[activo] [bit] NOT NULL,
 CONSTRAINT [PK_TipoPersona] PRIMARY KEY CLUSTERED 
(
	[idTipoPersona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TipoPersonaRol]    Script Date: 09/10/2013 19:34:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TipoPersonaRol](
	[idTipoPersonaRol] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[idTipoPersona] [numeric](18, 0) NOT NULL,
	[nombreRol] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TipoPersonaRol] PRIMARY KEY CLUSTERED 
(
	[idTipoPersonaRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TipoRegistroClases]    Script Date: 09/10/2013 19:34:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TipoRegistroClases](
	[idTipoRegistroClases] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NULL,
 CONSTRAINT [PK_TipoRegistroClases] PRIMARY KEY CLUSTERED 
(
	[idTipoRegistroClases] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TipoSancion]    Script Date: 09/10/2013 19:34:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TipoSancion](
	[idTipoSancion] [int] IDENTITY(1,1) NOT NULL,
	[idTipoSancionTransaccional] [int] NOT NULL,
	[nombre] [varchar](50) NULL,
	[descripcion] [varchar](250) NULL,
 CONSTRAINT [PK_TipoSancion] PRIMARY KEY CLUSTERED 
(
	[idTipoSancion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TipoTutor]    Script Date: 09/10/2013 19:34:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TipoTutor](
	[idTipoTutor] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](50) NULL,
	[idTipoTutorTransaccional] [numeric](18, 0) NULL,
 CONSTRAINT [PK_TipoTutor] PRIMARY KEY CLUSTERED 
(
	[idTipoTutor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tutor]    Script Date: 09/10/2013 19:34:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tutor](
	[idTutor] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[idTutorTransaccional] [numeric](18, 0) NOT NULL,
	[idPersona] [numeric](18, 0) NULL,
	[telefonoTrabajo] [varchar](20) NULL,
	[idTipoTutor] [numeric](18, 0) NULL,
 CONSTRAINT [PK_Tutor] PRIMARY KEY CLUSTERED 
(
	[idTutor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TutorAlumno]    Script Date: 09/10/2013 19:34:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TutorAlumno](
	[idTutorAlumno] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[idAlumno] [numeric](18, 0) NOT NULL,
	[idTutor] [numeric](18, 0) NOT NULL,
 CONSTRAINT [PK_TutorAlumno] PRIMARY KEY CLUSTERED 
(
	[idTutorAlumno] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ValorEscalaPonderacion]    Script Date: 09/10/2013 19:34:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ValorEscalaPonderacion](
	[idValorEscalaPonderacion] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](250) NULL,
	[descripcion] [varchar](4000) NULL,
	[idEscalaPonderacion] [numeric](18, 0) NULL,
	[orden] [numeric](18, 0) NULL,
PRIMARY KEY CLUSTERED 
(
	[idValorEscalaPonderacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ValoresEscalaCalificacion]    Script Date: 09/10/2013 19:34:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ValoresEscalaCalificacion](
	[idValorEscalaCalificacion] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[idValorEscalaCalificacionTransaccional] [numeric](18, 0) NULL,
	[nombre] [varchar](50) NOT NULL,
	[descripcion] [varchar](250) NULL,
	[valor] [varchar](50) NOT NULL,
	[activo] [bit] NOT NULL,
	[aprobado] [bit] NOT NULL,
 CONSTRAINT [PK_ValorEscalaCalificacion] PRIMARY KEY CLUSTERED 
(
	[idValorEscalaCalificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[vw_CursosCicloLectivoActual]    Script Date: 09/10/2013 19:34:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vw_CursosCicloLectivoActual] AS 

SELECT

CCL.idCursoCicloLectivo, n.nombre + ' ' + c.nombre AS Curso, CCL.idPreceptor

FROM dbo.CursosCicloLectivo AS CCL 
	INNER JOIN dbo.CicloLectivo AS CL ON CCL.idCicloLectivo = CL.idCicloLectivo AND activo = 1
	INNER JOIN dbo.Curso AS C ON CCL.idCurso = C.idCurso
	INNER JOIN dbo.Nivel AS N ON C.idNivel = N.idNivel

--ORDER BY idCursoCicloLectivo


GO
/****** Object:  View [dbo].[vw_AsignaturaCurso]    Script Date: 09/10/2013 19:34:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[vw_AsignaturaCurso] as
select 
vw_CursosCicloLectivoActual.idCursoCicloLectivo
,Curso,idPreceptor,
AsignaturaCicloLectivo.idDocente,
AsignaturaCicloLectivo.idAsignaturaCicloLectivo,
Asignatura.nombre as 'asignatura'
 from 
vw_CursosCicloLectivoActual inner join 
AsignaturaCicloLectivo 
on vw_CursosCicloLectivoActual.idCursoCicloLectivo = AsignaturaCicloLectivo.idCursoCicloLectivo
inner join Asignatura on AsignaturaCicloLectivo.idAsignatura = Asignatura.idAsignatura
GO
/****** Object:  View [dbo].[vw_PersonaUsuarioRol]    Script Date: 09/10/2013 19:34:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_PersonaUsuarioRol] AS 
SELECT 

	Roles.RoleName, Usuario.UserName, Personas.nombre, Personas.apellido, Personas.idPersona, Membership.Password
FROM 

EDUAR_aspnet_services.dbo.aspnet_Roles AS Roles
	INNER JOIN
EDUAR_aspnet_services.dbo.aspnet_UsersInRoles AS RolUsuario
	ON Roles.RoleId = RolUsuario.RoleId	
	INNER JOIN
EDUAR_aspnet_services.dbo.aspnet_Membership AS Membership
	ON RolUsuario.UserId = Membership.UserId
	INNER JOIN
EDUAR_aspnet_services.dbo.aspnet_Users AS Usuario	
	ON Membership.UserId = Usuario.UserId
	INNER JOIN
EDUAR.dbo.Personas AS Personas
	ON Personas.username = Usuario.UserName

WHERE Personas.activo = 1	
--ORDER BY Roles.RoleName
GO
/****** Object:  View [dbo].[vw_AnalisisRespuestasTextuales]    Script Date: 09/10/2013 19:34:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_AnalisisRespuestasTextuales] AS 
SELECT E.idEncuesta, COUNT(e.idpersona) AS 'Cantidad', 'Encuestas Enviadas' AS 'Status', RE.rol as 'roleName'
FROM encuestausuario AS E
INNER JOIN RelEncuestaRol AS RE ON E.idEncuesta = RE.idEncuesta
WHERE E.idPersona IN (SELECT idpersona FROM vw_PersonaUsuarioRol WHERE rolename=RE.rol)
GROUP BY E.idEncuesta, RE.rol

UNION

SELECT E.idEncuesta, COUNT(E.idpersona) AS 'Cantidad', 'Encuestas Respondidas' AS 'Status', RE.rol as 'roleName'
FROM encuestausuario AS E
INNER JOIN RelEncuestaRol AS RE ON E.idEncuesta = RE.idEncuesta
WHERE E.fecharespuesta IS NOT NULL
AND E.idPersona IN (SELECT idpersona FROM vw_PersonaUsuarioRol WHERE rolename=RE.rol)
GROUP BY E.idEncuesta, RE.rol

UNION

SELECT RP.idEncuesta, COUNT(RP.idRespuesta) AS 'Cantidad', 'Respuestas Efectivas' AS 'Status', RP.roleName 
FROM respuestapregunta AS RP
INNER JOIN pregunta AS P ON RP.idpregunta = P.idPregunta AND P.idEscalaPonderacion = 3
GROUP BY RP.idEncuesta, RP.roleName
GO
/****** Object:  View [dbo].[vw_StatusEncuestas]    Script Date: 09/10/2013 19:34:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_StatusEncuestas] AS 
SELECT 'Enviadas' as 'Status',COUNT(EU.idEncuesta) AS 'Cantidad',E.idAmbito, E.idCursoCicloLectivo, E.idAsignaturaCicloLectivo, E.idEncuesta, PUR.RoleName AS 'Rol'
FROM Encuesta AS E
INNER JOIN EncuestaUsuario AS EU ON EU.idEncuesta=E.idEncuesta
INNER JOIN vw_PersonaUsuarioRol AS PUR ON PUR.idPersona = EU.idPersona
WHERE E.fechaLanzamiento IS NOT NULL
GROUP BY E.idAmbito, E.idCursoCicloLectivo, E.idAsignaturaCicloLectivo, E.idEncuesta, PUR.RoleName

UNION

SELECT 'Pendiente' as 'Status',COUNT(EU.idEncuesta) AS 'Cantidad',E.idAmbito, E.idCursoCicloLectivo, E.idAsignaturaCicloLectivo, E.idEncuesta, PUR.RoleName AS 'Rol'
FROM Encuesta AS E
INNER JOIN EncuestaUsuario AS EU ON EU.idEncuesta=E.idEncuesta
INNER JOIN vw_PersonaUsuarioRol AS PUR ON PUR.idPersona = EU.idPersona
WHERE EU.fechaRespuesta IS NULL
AND E.fechaLanzamiento IS NOT NULL
AND E.fechaLimite >= GETDATE()
GROUP BY E.idAmbito, E.idCursoCicloLectivo, E.idAsignaturaCicloLectivo, E.idEncuesta, PUR.RoleName

UNION

SELECT 'Respondida' as 'Status',COUNT(E.idEncuesta) AS 'Cantidad', E.idAmbito, E.idCursoCicloLectivo, E.idAsignaturaCicloLectivo, E.idEncuesta, PUR.RoleName AS 'Rol'
FROM Encuesta AS E
INNER JOIN EncuestaUsuario AS EU ON EU.idEncuesta=E.idEncuesta
INNER JOIN vw_PersonaUsuarioRol AS PUR ON PUR.idPersona = EU.idPersona
WHERE EU.fechaRespuesta IS NOT NULL
AND E.fechaLanzamiento IS NOT NULL
GROUP BY E.idAmbito, E.idCursoCicloLectivo, E.idAsignaturaCicloLectivo, E.idEncuesta, PUR.RoleName

UNION

SELECT 'Expirada' as 'Status',COUNT(E.idEncuesta) AS 'Cantidad', E.idAmbito, E.idCursoCicloLectivo, E.idAsignaturaCicloLectivo, E.idEncuesta, PUR.RoleName AS 'Rol'
FROM Encuesta AS E
INNER JOIN EncuestaUsuario AS EU ON EU.idEncuesta=E.idEncuesta
INNER JOIN vw_PersonaUsuarioRol AS PUR ON PUR.idPersona = EU.idPersona
WHERE E.fechaLimite < GETDATE()
AND EU.fechaRespuesta IS NULL
AND E.fechaLanzamiento IS NOT NULL
GROUP BY E.idAmbito, E.idCursoCicloLectivo, E.idAsignaturaCicloLectivo, E.idEncuesta, PUR.RoleName

UNION

SELECT 'No lanzadas' as 'Status',COUNT(E.idEncuesta) AS 'Cantidad', E.idAmbito, E.idCursoCicloLectivo, E.idAsignaturaCicloLectivo, E.idEncuesta, NULL AS 'Rol'
FROM Encuesta AS E
INNER JOIN AmbitoEncuesta AS AE ON AE.idAmbito=E.idAmbito
WHERE E.fechaLanzamiento IS NULL
GROUP BY E.idAmbito, E.idCursoCicloLectivo, E.idAsignaturaCicloLectivo, E.idEncuesta
GO
/****** Object:  View [dbo].[vw_AlumnosPorCurso_CicloLectivoActual]    Script Date: 09/10/2013 19:34:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_AlumnosPorCurso_CicloLectivoActual] AS 
SELECT P.apellido+', '+P.nombre 'Alumno'
,ISNULL(P.username, '-') 'Usuario'
,N.nombre+' '+C.nombre 'Curso'
FROM Alumnos AS A
INNER JOIN AlumnoCursoCicloLectivo AS ACCL ON ACCL.idAlumno = A.idAlumno
INNER JOIN Personas AS P ON P.idPersona = A.idPersona
INNER JOIN CursosCicloLectivo AS CCL ON CCL.idCursoCicloLectivo = ACCL.idCursoCicloLectivo
INNER JOIN Curso AS C ON C.idCurso = CCL.idCurso
INNER JOIN Nivel AS N ON N.idNivel = C.idNivel
INNER JOIN CicloLectivo AS CL ON CL.idCicloLectivo = CCL.idCicloLectivo AND CL.activo = 1
GO
/****** Object:  View [dbo].[vw_DocentesAsignaturaCurso]    Script Date: 09/10/2013 19:34:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_DocentesAsignaturaCurso] AS 
SELECT P.apellido+', '+P.nombre 'Docente'
,ISNULL(P.username, '-') 'Usuario'
,A.nombre 'Asignatura'
,N.nombre+' '+C.nombre 'Curso'
,P.idPersona 'ID'
FROM Personal AS PL
INNER JOIN Personas AS P ON P.idPersona = PL.idPersona
INNER JOIN AsignaturaCicloLectivo ACL ON ACL.idDocente = PL.idPersonal
INNER JOIN CursosCicloLectivo AS CCL ON CCL.idCursoCicloLectivo = ACL.idCursoCicloLectivo
INNER JOIN Curso AS C ON C.idCurso = CCL.idCurso
INNER JOIN Nivel AS N ON N.idNivel = C.idNivel
INNER JOIN Asignatura AS A ON A.idAsignatura = ACL.idAsignatura
INNER JOIN CicloLectivo AS CL ON CL.idCicloLectivo = CCL.idCicloLectivo AND CL.activo = 1
GO
/****** Object:  View [dbo].[vw_respuestas_encuesta]    Script Date: 09/10/2013 19:34:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_respuestas_encuesta] AS 
SELECT E.idEncuesta,RP.idPregunta,RP.valorRespuestaSeleccion AS 'Seleccion',RP.valorRespuestaTextual AS 'Respuesta',P.peso, RP.valorRespuestaSeleccion*P.peso AS 'Valor',RP.roleName
FROM RespuestaPregunta AS RP
INNER JOIN Pregunta AS P ON RP.idPregunta = P.idPregunta
INNER JOIN Encuesta AS E ON E.idEncuesta = P.idEncuesta
WHERE RP.valorRespuestaSeleccion <> 0

UNION ALL

SELECT E.idEncuesta,RP.idPregunta,RP.valorRespuestaSeleccion AS 'Seleccion',RP.valorRespuestaTextual AS 'Respuesta',P.peso, P.peso AS 'Valor',RP.roleName
FROM RespuestaPregunta AS RP
INNER JOIN Pregunta AS P ON RP.idPregunta = P.idPregunta
INNER JOIN Encuesta AS E ON E.idEncuesta = P.idEncuesta
WHERE RP.valorRespuestaTextual IS NOT NULL
AND RP.valorRespuestaSeleccion = 0
GO
/****** Object:  View [dbo].[vw_respuestasTextuales]    Script Date: 09/10/2013 19:34:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_respuestasTextuales] AS 
SELECT RP.idEncuesta,RP.idPregunta,RP.valorRespuestaTextual,RP.roleName
FROM RespuestaPregunta AS RP
WHERE RP.valorRespuestaTextual IS NOT NULL
GO
/****** Object:  View [dbo].[vw_TutoresPorCurso_CicloLectivo]    Script Date: 09/10/2013 19:34:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_TutoresPorCurso_CicloLectivo] AS 
SELECT DISTINCT P.apellido+', '+P.nombre 'Tutor'
,ISNULL(P.username, '-') 'Usuario'
,N.nombre+' '+C.nombre 'Curso'
FROM Alumnos AS A
INNER JOIN AlumnoCursoCicloLectivo AS ACCL ON ACCL.idAlumno = A.idAlumno
INNER JOIN TutorAlumno AS TA ON TA.idAlumno = A.idAlumno
INNER JOIN Tutor AS T ON T.idTutor = TA.idTutor
INNER JOIN Personas AS P ON P.idPersona = T.idPersona
INNER JOIN CursosCicloLectivo AS CCL ON CCL.idCursoCicloLectivo = ACCL.idCursoCicloLectivo
INNER JOIN Curso AS C ON C.idCurso = CCL.idCurso
INNER JOIN Nivel AS N ON N.idNivel = C.idNivel
INNER JOIN CicloLectivo AS CL ON CL.idCicloLectivo = CCL.idCicloLectivo AND CL.activo = 1

GO
ALTER TABLE [dbo].[Contenido] ADD  CONSTRAINT [DF_Contenido_activo]  DEFAULT ((1)) FOR [activo]
GO
ALTER TABLE [dbo].[Indicadores] ADD  CONSTRAINT [DF_Indicadores_invertirEscala]  DEFAULT ((0)) FOR [invertirEscala]
GO
ALTER TABLE [dbo].[PlanificacionAnual] ADD  CONSTRAINT [DF_PlanificacionAnual_solicitarAprobacion]  DEFAULT ((0)) FOR [solicitarAprobacion]
GO
ALTER TABLE [dbo].[TemaContenido] ADD  CONSTRAINT [DF_TemaContenido_activo]  DEFAULT ((1)) FOR [activo]
GO
ALTER TABLE [dbo].[TemaPlanificacionTemaContenido] ADD  CONSTRAINT [DF_TemaPlanificacionTemaContenido_obligatorio]  DEFAULT ((1)) FOR [obligatorio]
GO
ALTER TABLE [dbo].[Accesos]  WITH CHECK ADD  CONSTRAINT [FK_Accesos_Paginas] FOREIGN KEY([idPagina])
REFERENCES [dbo].[Paginas] ([idPagina])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Accesos] CHECK CONSTRAINT [FK_Accesos_Paginas]
GO
ALTER TABLE [dbo].[Asistencia]  WITH CHECK ADD  CONSTRAINT [FK_Asistencia_TipoAsistencia] FOREIGN KEY([idTipoAsistencia])
REFERENCES [dbo].[TipoAsistencia] ([idTipoAsistencia])
GO
ALTER TABLE [dbo].[Asistencia] CHECK CONSTRAINT [FK_Asistencia_TipoAsistencia]
GO
ALTER TABLE [dbo].[Citacion]  WITH CHECK ADD  CONSTRAINT [FK_Citacion_Curso] FOREIGN KEY([idCurso])
REFERENCES [dbo].[CursosCicloLectivo] ([idCursoCicloLectivo])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Citacion] CHECK CONSTRAINT [FK_Citacion_Curso]
GO
ALTER TABLE [dbo].[Citacion]  WITH CHECK ADD  CONSTRAINT [FK_Citacion_Motivo_Citacion] FOREIGN KEY([idMotivoCitacion])
REFERENCES [dbo].[MotivoCitacion] ([idMotivoCitacion])
GO
ALTER TABLE [dbo].[Citacion] CHECK CONSTRAINT [FK_Citacion_Motivo_Citacion]
GO
ALTER TABLE [dbo].[Citacion]  WITH CHECK ADD  CONSTRAINT [FK_Citacion_Personal] FOREIGN KEY([idOrganizador])
REFERENCES [dbo].[Personas] ([idPersona])
GO
ALTER TABLE [dbo].[Citacion] CHECK CONSTRAINT [FK_Citacion_Personal]
GO
ALTER TABLE [dbo].[Citacion]  WITH CHECK ADD  CONSTRAINT [FK_Citacion_Tutor] FOREIGN KEY([idTutor])
REFERENCES [dbo].[Tutor] ([idTutor])
GO
ALTER TABLE [dbo].[Citacion] CHECK CONSTRAINT [FK_Citacion_Tutor]
GO
ALTER TABLE [dbo].[Curso]  WITH CHECK ADD  CONSTRAINT [FK_Curso_Nivel] FOREIGN KEY([idNivel])
REFERENCES [dbo].[Nivel] ([idNivel])
GO
ALTER TABLE [dbo].[Curso] CHECK CONSTRAINT [FK_Curso_Nivel]
GO
ALTER TABLE [dbo].[CursosCicloLectivo]  WITH CHECK ADD  CONSTRAINT [FK_CursosCicloLectivo_Personal] FOREIGN KEY([idPreceptor])
REFERENCES [dbo].[Personal] ([idPersonal])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CursosCicloLectivo] CHECK CONSTRAINT [FK_CursosCicloLectivo_Personal]
GO
ALTER TABLE [dbo].[Evaluacion]  WITH CHECK ADD  CONSTRAINT [FK_Evaluacion_EventoAgenda] FOREIGN KEY([idEvento])
REFERENCES [dbo].[EventoAgenda] ([idEventoAgenda])
GO
ALTER TABLE [dbo].[Evaluacion] CHECK CONSTRAINT [FK_Evaluacion_EventoAgenda]
GO
ALTER TABLE [dbo].[EventoAgenda]  WITH CHECK ADD  CONSTRAINT [FK_EventoAgenda_AgendaActividades] FOREIGN KEY([idAgendaActividades])
REFERENCES [dbo].[AgendaActividades] ([idAgendaActividad])
GO
ALTER TABLE [dbo].[EventoAgenda] CHECK CONSTRAINT [FK_EventoAgenda_AgendaActividades]
GO
ALTER TABLE [dbo].[EventoAgenda]  WITH CHECK ADD  CONSTRAINT [FK_EventoAgenda_Personas] FOREIGN KEY([idOrganizador])
REFERENCES [dbo].[Personas] ([idPersona])
GO
ALTER TABLE [dbo].[EventoAgenda] CHECK CONSTRAINT [FK_EventoAgenda_Personas]
GO
ALTER TABLE [dbo].[EventoAgenda]  WITH CHECK ADD  CONSTRAINT [FK_EventoAgenda_TipoEvento] FOREIGN KEY([idTipoEvento])
REFERENCES [dbo].[TipoEvento] ([idTipoEvento])
GO
ALTER TABLE [dbo].[EventoAgenda] CHECK CONSTRAINT [FK_EventoAgenda_TipoEvento]
GO
ALTER TABLE [dbo].[EventoInstitucional]  WITH CHECK ADD  CONSTRAINT [FK_Evento_institucional_Tipo_evento_institucional] FOREIGN KEY([idTipoEvento])
REFERENCES [dbo].[TipoEventoInstitucional] ([idTipoEventoInstitucional])
GO
ALTER TABLE [dbo].[EventoInstitucional] CHECK CONSTRAINT [FK_Evento_institucional_Tipo_evento_institucional]
GO
ALTER TABLE [dbo].[EventoInstitucional]  WITH CHECK ADD  CONSTRAINT [FK_EventoInstitucional_Personas] FOREIGN KEY([idPersona])
REFERENCES [dbo].[Personas] ([idPersona])
GO
ALTER TABLE [dbo].[EventoInstitucional] CHECK CONSTRAINT [FK_EventoInstitucional_Personas]
GO
ALTER TABLE [dbo].[Excursion]  WITH CHECK ADD  CONSTRAINT [FK_Excursion_EventoAgenda] FOREIGN KEY([idEvento])
REFERENCES [dbo].[EventoAgenda] ([idEventoAgenda])
GO
ALTER TABLE [dbo].[Excursion] CHECK CONSTRAINT [FK_Excursion_EventoAgenda]
GO
ALTER TABLE [dbo].[Modulo]  WITH CHECK ADD  CONSTRAINT [FK_Modulo_DiaHorario] FOREIGN KEY([idDiaHorario])
REFERENCES [dbo].[DiaHorario] ([idDiaHorario])
GO
ALTER TABLE [dbo].[Modulo] CHECK CONSTRAINT [FK_Modulo_DiaHorario]
GO
ALTER TABLE [dbo].[Personal]  WITH CHECK ADD  CONSTRAINT [FK_Personal_CargosPersonal] FOREIGN KEY([idCargoPersonal])
REFERENCES [dbo].[CargosPersonal] ([idCargoPersonal])
GO
ALTER TABLE [dbo].[Personal] CHECK CONSTRAINT [FK_Personal_CargosPersonal]
GO
ALTER TABLE [dbo].[Personas]  WITH CHECK ADD  CONSTRAINT [FK_Personas_Localidades] FOREIGN KEY([idLocalidad])
REFERENCES [dbo].[Localidades] ([idLocalidad])
GO
ALTER TABLE [dbo].[Personas] CHECK CONSTRAINT [FK_Personas_Localidades]
GO
ALTER TABLE [dbo].[Personas]  WITH CHECK ADD  CONSTRAINT [FK_Personas_TipoPersona] FOREIGN KEY([idTipoPersona])
REFERENCES [dbo].[TipoPersona] ([idTipoPersona])
GO
ALTER TABLE [dbo].[Personas] CHECK CONSTRAINT [FK_Personas_TipoPersona]
GO
ALTER TABLE [dbo].[Personas]  WITH CHECK ADD  CONSTRAINT [FK_Personas_TiposDocumentos] FOREIGN KEY([idTipoDocumento])
REFERENCES [dbo].[TipoDocumento] ([idTipoDocumento])
GO
ALTER TABLE [dbo].[Personas] CHECK CONSTRAINT [FK_Personas_TiposDocumentos]
GO
ALTER TABLE [dbo].[ProcesosEjecutados]  WITH CHECK ADD  CONSTRAINT [FK_ProcesosEjecutados_ProcesosAutomaticos] FOREIGN KEY([idProcesoAutomatico])
REFERENCES [dbo].[ProcesosAutomaticos] ([idProcesoAutomatico])
GO
ALTER TABLE [dbo].[ProcesosEjecutados] CHECK CONSTRAINT [FK_ProcesosEjecutados_ProcesosAutomaticos]
GO
ALTER TABLE [dbo].[Provincias]  WITH CHECK ADD  CONSTRAINT [FK_Provincias_Paises] FOREIGN KEY([idPais])
REFERENCES [dbo].[Paises] ([idPais])
GO
ALTER TABLE [dbo].[Provincias] CHECK CONSTRAINT [FK_Provincias_Paises]
GO
ALTER TABLE [dbo].[Reunion]  WITH CHECK ADD  CONSTRAINT [FK_Reunion_EventoAgenda] FOREIGN KEY([idEvento])
REFERENCES [dbo].[EventoAgenda] ([idEventoAgenda])
GO
ALTER TABLE [dbo].[Reunion] CHECK CONSTRAINT [FK_Reunion_EventoAgenda]
GO
ALTER TABLE [dbo].[TemaContenido]  WITH CHECK ADD  CONSTRAINT [FK_TemaContenido_Contenido] FOREIGN KEY([idContenido])
REFERENCES [dbo].[Contenido] ([idContenido])
GO
ALTER TABLE [dbo].[TemaContenido] CHECK CONSTRAINT [FK_TemaContenido_Contenido]
GO
ALTER TABLE [dbo].[TemaPlanificacionAnual]  WITH CHECK ADD  CONSTRAINT [FK_TemaPlanificacionAnual_PlanificacionAnual] FOREIGN KEY([idPlanificacionAnual])
REFERENCES [dbo].[PlanificacionAnual] ([idPlanificacionAnual])
GO
ALTER TABLE [dbo].[TemaPlanificacionAnual] CHECK CONSTRAINT [FK_TemaPlanificacionAnual_PlanificacionAnual]
GO
ALTER TABLE [dbo].[TipoPersonaRol]  WITH CHECK ADD  CONSTRAINT [FK_TipoPersonaRol_TipoPersona] FOREIGN KEY([idTipoPersona])
REFERENCES [dbo].[TipoPersona] ([idTipoPersona])
GO
ALTER TABLE [dbo].[TipoPersonaRol] CHECK CONSTRAINT [FK_TipoPersonaRol_TipoPersona]
GO
ALTER TABLE [dbo].[Tutor]  WITH CHECK ADD  CONSTRAINT [FK_Tutor_TipoTutor] FOREIGN KEY([idTipoTutor])
REFERENCES [dbo].[TipoTutor] ([idTipoTutor])
GO
ALTER TABLE [dbo].[Tutor] CHECK CONSTRAINT [FK_Tutor_TipoTutor]
GO
ALTER TABLE [dbo].[TutorAlumno]  WITH CHECK ADD  CONSTRAINT [FK_TutorAlumno_Alumnos] FOREIGN KEY([idAlumno])
REFERENCES [dbo].[Alumnos] ([idAlumno])
GO
ALTER TABLE [dbo].[TutorAlumno] CHECK CONSTRAINT [FK_TutorAlumno_Alumnos]
GO
ALTER TABLE [dbo].[TutorAlumno]  WITH CHECK ADD  CONSTRAINT [FK_TutorAlumno_Tutor] FOREIGN KEY([idTutor])
REFERENCES [dbo].[Tutor] ([idTutor])
GO
ALTER TABLE [dbo].[TutorAlumno] CHECK CONSTRAINT [FK_TutorAlumno_Tutor]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_StatusEncuestas'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_StatusEncuestas'
GO
