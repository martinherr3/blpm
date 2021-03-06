﻿using System;
using System.Collections.Generic;
using EDUAR_BusinessLogic.Shared;
using EDUAR_DataAccess.Common;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using EDUAR_Entities.Shared;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;

namespace EDUAR_BusinessLogic.Common
{
    public class BLPlanificacionAnual : BusinessLogicBase<PlanificacionAnual, DAPlanificacionAnual>
    {
        #region --[Constante]--
        private const string ClassName = "BLPlanificacionAnual";
        #endregion

        #region --[Constructores]--
        /// <summary>
        /// Constructor con DTO como parámetro.
        /// </summary>
        public BLPlanificacionAnual(DTBase objPlanificacionAnual)
        {
            Data = (PlanificacionAnual)objPlanificacionAnual;
        }
        /// <summary>
        /// Constructor vacio
        /// </summary>
        public BLPlanificacionAnual()
        {
            Data = new PlanificacionAnual();
        }
        #endregion

        #region --[Propiedades Override]--
        protected override sealed DAPlanificacionAnual DataAcces
        {
            get { return dataAcces; }
            set { dataAcces = value; }
        }

        public override sealed PlanificacionAnual Data
        {
            get { return data; }
            set { data = value; }
        }

        public override string FieldId
        {
            get { return DataAcces.FieldID; }
        }

        public override string FieldDescription
        {
            get { return DataAcces.FieldDescription; }
        }

        /// <summary>
        /// Gets the by id.
        /// </summary>
        public override void GetById()
        {
            try
            {
                Data = DataAcces.GetById(Data);
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetById", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }

        /// <summary>
        /// Método que guarda el registro actualmente cargado en memoria. No importa si se trata de una alta o modificación.
        /// </summary>
        public override void Save()
        {
            try
            {
                //Abre la transaccion que se va a utilizar
                DataAcces.Transaction.OpenTransaction();
                int idPlanificacionAnual = 0;

                if (Data.idPlanificacionAnual == 0)
                    DataAcces.Create(Data, out idPlanificacionAnual);
                else
                    DataAcces.Update(Data);

                if (Data.listaTemasPlanificacion.Count > 0)
                {
                    BLTemaPlanificacionAnual objBLPlanificacion;
                    foreach (TemaPlanificacionAnual item in Data.listaTemasPlanificacion)
                    {
                        item.idPlanificacionAnual = idPlanificacionAnual > 0 ? idPlanificacionAnual : Data.idPlanificacionAnual;
                        objBLPlanificacion = new BLTemaPlanificacionAnual(item);
                        objBLPlanificacion.Save(DataAcces.Transaction);
                    }
                }
                //Se da el OK para la transaccion.
                DataAcces.Transaction.CommitTransaction();
            }
            catch (CustomizedException ex)
            {
                if (DataAcces != null && DataAcces.Transaction != null)
                    DataAcces.Transaction.RollbackTransaction();
                throw ex;
            }
            catch (Exception ex)
            {
                if (DataAcces != null && DataAcces.Transaction != null)
                    DataAcces.Transaction.RollbackTransaction();
                throw new CustomizedException(string.Format("Fallo en {0} - Save()", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }

        /// <summary>
        /// Método que guarda el registro actualmente cargado en memoria. No importa si se trata de una alta o modificación.
        /// </summary>
        public override void Save(DATransaction objDATransaction)
        {
            try
            {
                //Si no viene el Id es porque se esta creando la entidad
                DataAcces = new DAPlanificacionAnual(objDATransaction);
                int idPlanificacionAnual = 0;
                if (Data.idPlanificacionAnual == 0)
                {
                    DataAcces.Create(Data, out idPlanificacionAnual);
                    Data.idPlanificacionAnual = idPlanificacionAnual;
                }
                else
                    DataAcces.Update(Data);

                if (Data.listaTemasPlanificacion.Count > 0)
                {
                    BLTemaPlanificacionAnual objBLPlanificacion;
                    foreach (TemaPlanificacionAnual item in Data.listaTemasPlanificacion)
                    {
                        item.idPlanificacionAnual = idPlanificacionAnual > 0 ? idPlanificacionAnual : Data.idPlanificacionAnual;
                        objBLPlanificacion = new BLTemaPlanificacionAnual(item);
                        objBLPlanificacion.Save(DataAcces.Transaction);
                    }
                }

                GrabarCursos(idPlanificacionAnual);
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - Save()", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }

        public override void Delete()
        {
            throw new NotImplementedException();
        }

        public override void Delete(DATransaction objDATransaction)
        {
            try
            {
                DataAcces = new DAPlanificacionAnual(objDATransaction);
                DataAcces.Delete(Data);
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - Delete()", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }
        #endregion

        #region --[Métodos publicos]--
        /// <summary>
        /// Gets the planificacion.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        public List<PlanificacionAnual> GetPlanificacion(PlanificacionAnual entidad)
        {
            try
            {
                List<PlanificacionAnual> lista = DataAcces.GetPlanificacion(entidad);
                BLTemaPlanificacionAnual objBLTemas = new BLTemaPlanificacionAnual();
                foreach (PlanificacionAnual item in lista)
                {
                    item.listaTemasPlanificacion = objBLTemas.GetTemasPlanificacionAnualCalc(item);
                    item.listaCursos = DataAcces.GetCursos(item);
                }
                return lista;
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetPlanificacion", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }

        /// <summary>
        /// Gets the planificacion.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        public PlanificacionAnual GetPlanificacionByAsignatura(AsignaturaNivel entidad, CicloLectivo cicloLectivo)
        {
            try
            {
                Curricula objCurricula = new Curricula();
                objCurricula.asignatura = entidad.asignatura;
                objCurricula.nivel = entidad.nivel;
                objCurricula.orientacion = entidad.orientacion;
                BLCurricula objBLCurricula = new BLCurricula();

                PlanificacionAnual objPlanificacion = new PlanificacionAnual();
                objPlanificacion.curricula = objBLCurricula.GetByAsignaturaNivelOrientacion(objCurricula);
                objPlanificacion.cicloLectivo = cicloLectivo;
                if (objPlanificacion.curricula.idCurricula > 0)
                {
                    List<PlanificacionAnual> listaPlanificaciones = DataAcces.GetPlanificacion(objPlanificacion);

                    if (listaPlanificaciones != null && listaPlanificaciones.Count > 0)
                    {
                        objPlanificacion = listaPlanificaciones[0];
                        BLTemaPlanificacionAnual objBLTemas = new BLTemaPlanificacionAnual();
                        objPlanificacion.listaTemasPlanificacion = objBLTemas.GetTemasPlanificacionAnual(objPlanificacion);
                        objPlanificacion.listaCursos = DataAcces.GetCursos(objPlanificacion);
                    }
                    return objPlanificacion;
                }
                else
                    throw new CustomizedException("Aún no se ha generado la Currícula para la asignatura seleccionada", new Exception(),
                                                enuExceptionType.ValidationException);
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetPlanificacionByAsignatura", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }

        /// <summary>
        /// Gets the planificacion.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        public PlanificacionAnual GetPlanificacionByAsignaturaAtrasados(int idCursoCicloLectivo, int idAsignaturaCicloLectivo)
        {
            try
            {
                PlanificacionAnual objPlanificacion = new PlanificacionAnual();
                BLTemaPlanificacionAnual objBLTemas = new BLTemaPlanificacionAnual();
                objPlanificacion.listaTemasPlanificacion = objBLTemas.GetTemasPlanificacionAnualAtrasados(idCursoCicloLectivo, idAsignaturaCicloLectivo);
                return objPlanificacion;
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetPlanificacionByAsignatura", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }

        /// <summary>
        /// Gets the contenidos no asignados.
        /// </summary>
        /// <param name="idCursoCicloLectivo">The id curso ciclo lectivo.</param>
        /// <param name="idAsignaturaCicloLectivo">The id asignatura ciclo lectivo.</param>
        /// <returns></returns>
        public List<TemaPlanificacionAnual> GetContenidosNoAsignados(int idCursoCicloLectivo, int idAsignaturaCicloLectivo)
        {
            try
            {
                return (this.GetPlanificacionByAsignaturaAtrasados(idCursoCicloLectivo, idAsignaturaCicloLectivo)).listaTemasPlanificacion;
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetAsignaturas", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }

        /// <summary>
        /// Grabars the planificacion.
        /// </summary>
        /// <exception cref="CustomizedException"></exception>
        public void GrabarPlanificacion()
        {
            try
            {
                BLCurricula objBLCurricula = new BLCurricula(Data.curricula);

                //busca si existe el id
                Data.curricula = objBLCurricula.GetByAsignaturaNivelOrientacion(Data.curricula);

                //Abre la transaccion que se va a utilizar
                DataAcces.Transaction.OpenTransaction();
                if (Data.curricula.idCurricula == 0)
                    objBLCurricula.Save(DataAcces.Transaction);
                //nuevoContenido.idCurricula = Data.idCurricula;

                this.Save(DataAcces.Transaction);

                //Se da el OK para la transaccion.
                DataAcces.Transaction.CommitTransaction();
            }
            catch (CustomizedException ex)
            {
                if (DataAcces != null && DataAcces.Transaction != null)
                    DataAcces.Transaction.RollbackTransaction();
                throw ex;
            }
            catch (Exception ex)
            {
                if (DataAcces != null && DataAcces.Transaction != null)
                    DataAcces.Transaction.RollbackTransaction();
                throw new CustomizedException(string.Format("Fallo en {0} - GrabarPlanificacion()", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }

        /// <summary>
        /// Grabars the cursos.
        /// </summary>
        /// <param name="idPlanificacionAnual">The id planificacion anual.</param>
        /// <exception cref="CustomizedException"></exception>
        private void GrabarCursos(int idPlanificacionAnual)
        {
            try
            {
                if (Data.idPlanificacionAnual > 0) DataAcces.DeleteCursosPlanificacion(Data.idPlanificacionAnual);
                if (Data.listaCursos.Count > 0)
                    foreach (CursoCicloLectivo item in Data.listaCursos)
                        DataAcces.SaveCursos(idPlanificacionAnual > 0 ? idPlanificacionAnual : Data.idPlanificacionAnual, item.idCursoCicloLectivo);
            }
            catch (CustomizedException ex)
            {
                if (DataAcces != null && DataAcces.Transaction != null)
                    DataAcces.Transaction.RollbackTransaction();
                throw ex;
            }
            catch (Exception ex)
            {
                if (DataAcces != null && DataAcces.Transaction != null)
                    DataAcces.Transaction.RollbackTransaction();
                throw new CustomizedException(string.Format("Fallo en {0} - GrabarCursos()", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }

        /// <summary>
        /// Calculars the Cobertura Planificado vs Programado.
        /// </summary>
        /// <param name="idPlanificacionAnual">The id planificacion anual.</param>
        /// <exception cref="CustomizedException"></exception>
        public void CalcularCobertura(List<PlanificacionAnual> listaPlanificaciones)
        {
            BLContenido contenidoBL = new BLContenido();
            List<Contenido> ContenidosDeCurricula = new List<Contenido>();
            decimal temasContenidosCubiertos = 0;
            List<TemaContenido> ListaTemasContenidosCurricula = new List<TemaContenido>();

            foreach (PlanificacionAnual unaPlanificacion in listaPlanificaciones)
            {
                ContenidosDeCurricula = contenidoBL.GetCurriculaAsignaturaNivel(new Contenido(unaPlanificacion.curricula.idCurricula));

                foreach (Contenido unContenidoCurricula in ContenidosDeCurricula)
                    foreach (TemaContenido unTemaContenidoCurricula in unContenidoCurricula.listaContenidos)
                        ListaTemasContenidosCurricula.Add(unTemaContenidoCurricula);

                foreach (TemaPlanificacionAnual unTemaPlanificacionAnual in unaPlanificacion.listaTemasPlanificacion)
                    foreach (TemaContenido unTemaContenidoPlanificado in unTemaPlanificacionAnual.listaContenidos)
                        foreach (TemaContenido unTemaContenidoCurricula in ListaTemasContenidosCurricula)
                            if (unTemaContenidoPlanificado.idTemaContenido == unTemaContenidoCurricula.idTemaContenido)
                                temasContenidosCubiertos++;

                if (ListaTemasContenidosCurricula.Count > 0)
                    unaPlanificacion.porcentajeCobertura = Math.Round((temasContenidosCubiertos / ListaTemasContenidosCurricula.Count) * 100, 2);
                else
                    unaPlanificacion.porcentajeCobertura = 0;

                ListaTemasContenidosCurricula = new List<TemaContenido>();
                temasContenidosCubiertos = 0;
            }
        }
        #endregion
    }
}
