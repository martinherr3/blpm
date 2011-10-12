using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DAInstanciaCalificacion : DataAccesBase<InstanciaCalificacion>
    {
        #region --[Atributos]--
        private const string ClassName = "DAInstanciaCalificacion";
        #endregion

        #region --[Constructor]--
        public DAInstanciaCalificacion()
        {
        }

        public DAInstanciaCalificacion(DATransaction objDATransaction)
            : base(objDATransaction)
        {

        }
        #endregion

        #region --[Implementación métodos heredados]--
        public override string FieldID
        {
            get { throw new NotImplementedException(); }
        }

        public override string FieldDescription
        {
            get { throw new NotImplementedException(); }
        }

        public override InstanciaCalificacion GetById(InstanciaCalificacion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(InstanciaCalificacion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(InstanciaCalificacion entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(InstanciaCalificacion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(InstanciaCalificacion entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
