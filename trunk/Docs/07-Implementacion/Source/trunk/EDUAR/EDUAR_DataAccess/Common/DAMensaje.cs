using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DAMensaje : DataAccesBase<Mensaje>
    {
        #region --[Atributos]--
        private const string ClassName = "DAMensaje";
        #endregion

        #region --[Constructor]--
        public DAMensaje()
        {
        }

        public DAMensaje(DATransaction objDATransaction)
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

        public override Mensaje GetById(Mensaje entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Mensaje entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Mensaje entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(Mensaje entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Mensaje entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
