﻿using System;
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
    public class BLContenido : BusinessLogicBase<Contenido, DAContenido>
    {
        #region --[Constante]--
        private const string ClassName = "BLContenido";
        #endregion

        #region --[Constructores]--
        /// <summary>
        /// Constructor con DTO como parámetro.
        /// </summary>
        public BLContenido(DTBase objContenido)
        {
            Data = (Contenido)objContenido;
        }
        /// <summary>
        /// Constructor vacio
        /// </summary>
        public BLContenido()
        {
            Data = new Contenido();
        }
        #endregion

        #region --[Propiedades Override]--
        protected override sealed DAContenido DataAcces
        {
            get { return dataAcces; }
            set { dataAcces = value; }
        }

        public override sealed Contenido Data
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
                int idContenido = 0;

                if (Data.idContenido == 0)
                    DataAcces.Create(Data, out idContenido);
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
                DataAcces = new DAContenido(objDATransaction);
                if (Data.idContenido == 0)
                    DataAcces.Create(Data);
                else
                {
                    DataAcces.Update(Data);
                }
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

        /// <summary>
        /// Deletes this instance.
        /// </summary>
        /// <exception cref="CustomizedException"></exception>
        public override void Delete()
        {
            try
            {
                DataAcces = new DAContenido();
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

        /// <summary>
        /// Deletes the specified obj DA transaction.
        /// </summary>
        /// <param name="objDATransaction">The obj DA transaction.</param>
        /// <exception cref="CustomizedException"></exception>
        public override void Delete(DATransaction objDATransaction)
        {
            try
            {
                DataAcces = new DAContenido(objDATransaction);
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
        /// Gets the by asignatura ciclo lectivo.
        /// </summary>
        /// <param name="objFiltro">The obj filtro.</param>
        /// <returns></returns>
        public List<Contenido> GetByAsignaturaCicloLectivo(Contenido objFiltro)
        {
            try
            {
                return DataAcces.GetContenidos(objFiltro);
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
        public List<Contenido> GetCurriculaAsignaturaNivel(Contenido objFiltro)
        {
            BLTemaContenido TemaContenidoBL = new BLTemaContenido();

            List<Contenido> listaContenidos = new List<Contenido>();
            try
            {
                listaContenidos = DataAcces.GetContenidosCalc(objFiltro);

                foreach (Contenido unContenido in listaContenidos)
                {
                    unContenido.listaContenidos = TemaContenidoBL.GetTemasByContenidoCalc(unContenido);
                }
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

            return (listaContenidos);
        }

        /// <summary>
        /// Gets the curricula asignatura nivel.
        /// </summary>
        /// <param name="objFiltro">The obj filtro.</param>
        /// <returns></returns>
        /// <exception cref="CustomizedException"></exception>
        public List<Contenido> GetCurriculaFullAsignaturaNivel(Contenido objFiltro)
        {
            BLTemaContenido TemaContenidoBL = new BLTemaContenido();

            List<Contenido> listaContenidos = new List<Contenido>();
            try
            {
                listaContenidos = DataAcces.GetContenidos(objFiltro);

                foreach (Contenido unContenido in listaContenidos)
                {
                    unContenido.listaContenidos = TemaContenidoBL.GetTemasByContenidoCalc(unContenido);
                }
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetCurriculaFullAsignaturaNivel", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }

            return (listaContenidos);
        }


        public void EliminarContenidos()
        {
            try
            {
                //Abre la transaccion que se va a utilizar
                DataAcces.Transaction.OpenTransaction();
                BLTemaContenido objBLTemaContenido = new BLTemaContenido(new TemaContenido() { idContenido = Data.idContenido , usuarioBaja = Data.usuarioBaja});

                objBLTemaContenido.Delete(DataAcces.Transaction);

                this.Delete(DataAcces.Transaction);

                DataAcces.Transaction.CommitTransaction();
            }
            catch (CustomizedException ex)
            {
                DataAcces.Transaction.RollbackTransaction();
                throw ex;
            }
            catch (Exception ex)
            {
                DataAcces.Transaction.RollbackTransaction();
                throw new CustomizedException(string.Format("Fallo en {0} - Save()", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }
        #endregion

    }
}
