using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DAPermiso : DataAccesBase<Permiso>
    {
        #region --[Atributos]--
        private const string ClassName = "DAPermiso";
        #endregion

        #region --[Constructor]--
        public DAPermiso()
        {
        }

        public DAPermiso(DATransaction objDATransaction)
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

        public override Permiso GetById(Permiso entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Permiso entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Permiso entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(Permiso entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Permiso entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
