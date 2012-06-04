
---- falta preceptor!!!!

Select N.nombre as 'Nivel', C.nombre as 'Division', CL.nombre as 'Año Lectivo' , CCL.idPreceptor
from CursosCicloLectivo as CCL
Inner join Curso as C on CCL.idCurso=C.idCurso
inner join Nivel as N on N.idNivel=C.idnivel
inner join CicloLectivo as CL on CL.idCicloLectivo=CcL.idCicloLectivo

