select  DISTINCT CL.nombre as 'Ciclo Lectivo',N.nombre as 'Curso', C.nombre as 'Division', A.nombre as 'Asignatura', PE.apellido as 'Apellido Docente', PE.nombre as 'Nombre Docente'
from DiaHorario as DH
inner join AsignaturaCicloLectivo as ACL on ACL.idAsignaturaCicloLectivo=DH.idAsignaturaCicloLectivo
inner join Personal as P on P.idPersonal=ACL.iddocente
inner join Personas as PE on P.idPersona=PE.idPersona
inner join CursosCicloLectivo as CCL on CCL.idCursoCicloLectivo=ACL.idCursoCicloLectivo
inner join Curso as C on C.idCurso=CCL.idCurso
inner join Nivel as N on N.idNivel =C.idNivel
inner join CicloLectivo as CL on CL.idCicloLectivo=CCL.idCicloLectivo
inner join Asignatura as A on A.idAsignatura=ACL.idAsignatura
order by CL.nombre desc

------------problemas con plan!
select *
from Orientacion as O
inner join Plan as P on O.idOrientacion=P.idOrientacion
