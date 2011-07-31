using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DAModulo : DataAccesBase<Modulo>
    {
        #region --[Atributos]--
        private const string ClassName = "DAModulo";
        #endregion

        #region --[Constructor]--
        public DAModulo()
        {
        }

        public DAModulo(DATransaction objDATransaction)
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

        public override Modulo GetById(Modulo entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Modulo entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Modulo entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(Modulo entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Modulo entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
