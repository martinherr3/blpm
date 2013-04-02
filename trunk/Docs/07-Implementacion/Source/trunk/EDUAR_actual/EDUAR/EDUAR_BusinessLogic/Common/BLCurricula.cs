using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDUAR_DataAccess.Common;
using EDUAR_Entities;
using EDUAR_BusinessLogic.Shared;
using EDUAR_Entities.Shared;
using EDUAR_Utility.Excepciones;
using EDUAR_Utility.Enumeraciones;
using EDUAR_DataAccess.Shared;

namespace EDUAR_BusinessLogic.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class BLCurricula : BusinessLogicBase<Curricula, DACurricula>
    {
        #region --[Constante]--
        private const string ClassName = "BLCurricula";
        #endregion

        #region --[Constructores]--
        /// <summary>
        /// Constructor con DTO como parámetro.
        /// </summary>
        public BLCurricula(DTBase objCurricula)
        {
            Data = (Curricula)objCurricula;
        }
        /// <summary>
        /// Constructor vacio
        /// </summary>
        public BLCurricula()
        {
            Data = new Curricula();
        }
        #endregion

        #region --[Propiedades Override]--
        protected override sealed DACurricula DataAcces
        {
            get { return dataAcces; }
            set { dataAcces = value; }
        }

        public override sealed Curricula Data
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
                int idCurricula = 0;

                if (Data.idCurricula == 0)
                    DataAcces.Create(Data, out idCurricula);
                else
                    DataAcces.Update(Data);

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
                DataAcces = new DACurricula(objDATransaction);
                int idCurricula = 0;
                if (Data.idCurricula == 0)
                {
                    DataAcces.Create(Data, out idCurricula);
                    Data.idCurricula = idCurricula;
                }
                else
                    DataAcces.Update(Data);
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
            try
            {
                DataAcces = new DACurricula();
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

        public override void Delete(DATransaction objDATransaction)
        {
            try
            {
                DataAcces = new DACurricula(objDATransaction);
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
        /// Gets the curriculas.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        /// <returns></returns>
        /// <exception cref="CustomizedException"></exception>
        public List<Curricula> GetCurriculas(Curricula entidad)
        {
            try
            {
                return DataAcces.GetCurriculas(entidad);
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetCurriculas", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }

        /// <summary>
        /// Gets the by asignatura ciclo lectivo.
        /// </summary>
        /// <param name="objFiltro">The obj filtro.</param>
        /// <returns></returns>
        public List<Curricula> GetByAsignaturaCicloLectivo(Curricula objFiltro)
        {
            try
            {
                return DataAcces.GetCurriculas(objFiltro);
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetByAsignaturaCicloLectivo", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }

        /// <summary>
        /// Gets the curricula asignatura nivel.
        /// </summary>
        /// <param name="objFiltro">The obj filtro.</param>
        /// <returns></returns>
        /// <exception cref="CustomizedException"></exception>
        public List<Contenido> GetCurriculaAsignaturaNivel(Curricula objFiltro)
        {
            try
            {
                Curricula miCurricula = new Curricula();
                miCurricula = DataAcces.GetByAsignaturaNivelOrientacion(objFiltro);
                if (miCurricula.idCurricula > 0)
                {
                    Contenido objContenido = new Contenido();
                    objContenido.idCurricula = miCurricula.idCurricula;
                    BLContenido objBLContenido = new BLContenido();
                    return objBLContenido.GetCurriculaAsignaturaNivel(objContenido);
                }
                return new List<Contenido>();
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetCurriculaAsignaturaNivel", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }

        /// <summary>
        /// Guardars the contenidos.
        /// </summary>
        /// <param name="curricula">The curricula.</param>
        /// <param name="nuevoContenido">The nuevo contenido.</param>
        /// <exception cref="CustomizedException"></exception>
        public void GuardarContenidos(Curricula curricula, Contenido nuevoContenido)
        {
            try
            {
                //busca si existe el id
                curricula.idCurricula = DataAcces.GetByAsignaturaNivelOrientacion(curricula).idCurricula;
                Data = curricula;

                //Abre la transaccion que se va a utilizar
                DataAcces.Transaction.OpenTransaction();
                this.Save(DataAcces.Transaction);
                nuevoContenido.idCurricula = Data.idCurricula;

                BLContenido objBLContenido = new BLContenido(nuevoContenido);
                objBLContenido.Save(DataAcces.Transaction);

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
                throw new CustomizedException(string.Format("Fallo en {0} - GuardarContenidos()", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }

        /// <summary>
        /// Gets the by asignatura nivel orientacion.
        /// </summary>
        /// <param name="objFiltro">The obj filtro.</param>
        /// <returns></returns>
        /// <exception cref="CustomizedException"></exception>
        public Curricula GetByAsignaturaNivelOrientacion(Curricula objFiltro)
        {
            try
            {
                Curricula objCurricula = new Curricula();
                objCurricula = DataAcces.GetByAsignaturaNivelOrientacion(objFiltro);
                return objCurricula;
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetByAsignaturaNivelOrientacion", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }

        /// <summary>
        /// Gets the temas contenido by curricula.
        /// </summary>
        /// <param name="objFiltro">The obj filtro.</param>
        /// <returns></returns>
        /// <exception cref="CustomizedException"></exception>
        public List<TemaContenido> GetTemasContenidoByCurricula(Curricula objFiltro)
        {
            try
            {
                Curricula miCurricula = new Curricula();
                miCurricula = DataAcces.GetByAsignaturaNivelOrientacion(objFiltro);
                if (miCurricula.idCurricula > 0)
                {
                    BLTemaContenido objBLContenido = new BLTemaContenido();
                    return objBLContenido.GetTemasByCursoAsignatura(miCurricula);
                }
                return new List<TemaContenido>();
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetCurriculaAsignaturaNivel", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }
        #endregion

    }
}
