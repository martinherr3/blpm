using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DAValoresEscalaCalificacion : DataAccesBase<ValoresEscalaCalificacion>
    {
        #region --[Atributos]--
        private const string ClassName = "DAValoresEscalaCalificacion";
        #endregion

        #region --[Constructor]--
        public DAValoresEscalaCalificacion()
        {
        }

        public DAValoresEscalaCalificacion(DATransaction objDATransaction)
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

        public override ValoresEscalaCalificacion GetById(ValoresEscalaCalificacion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(ValoresEscalaCalificacion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(ValoresEscalaCalificacion entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(ValoresEscalaCalificacion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(ValoresEscalaCalificacion entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
