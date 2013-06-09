using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DAEscalaCalificacion : DataAccesBase<EscalaCalificacion>
    {
        #region --[Atributos]--
        private const string ClassName = "DAEscalaCalificacion";
        #endregion

        #region --[Constructor]--
        public DAEscalaCalificacion()
        {
        }

        public DAEscalaCalificacion(DATransaction objDATransaction)
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

        public override EscalaCalificacion GetById(EscalaCalificacion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(EscalaCalificacion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(EscalaCalificacion entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(EscalaCalificacion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(EscalaCalificacion entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
