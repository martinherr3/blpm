Select  Distinct N.nombre as 'Nivel', C.nombre as 'Division', CL.nombre as 'Año Lectivo',
P.nombre as 'Nombre Alumno', P.apellido as 'Apellido Alumno'
from CursosCicloLectivo as CCL
Inner join Curso as C on CCL.idCurso=C.idCurso
inner join Nivel as N on N.idNivel=C.idnivel
inner join CicloLectivo as CL on CL.idCicloLectivo=CcL.idCicloLectivo
inner join AlumnoCursoCicloLectivo as ACCL on ACCL.idCursoCicloLectivo=CCL.idCursoCicloLectivo
inner Join Alumnos as A on A.idAlumno=ACCL.idAlumno
inner join Personas as P on P.idpersona=A.idpersona
inner join AsignaturaCicloLectivo as ACL on ACL.idCursoCicloLectivo=CCL.idCursoCicloLectivo
inner join Calificacion as Ca on Ca.idAlumnoCursoCicloLectivo=ACCL.idAlumnoCursoCicloLectivo
and Ca.idAsignaturaCicloLectivo=ACL.idAsignatura
inner join Asignatura as ASI on ASI.idAsignatura=ACL.idAsignatura
inner join ValoresEscalaCalificacion as VEC on VEC.idValorEscalaCalificacion=Ca.idValorCalificacion