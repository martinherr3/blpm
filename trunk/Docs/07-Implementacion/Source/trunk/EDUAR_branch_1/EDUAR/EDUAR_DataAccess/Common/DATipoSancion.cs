using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DATipoSancion : DataAccesBase<TipoSancion>
    {
        #region --[Atributos]--
        private const string ClassName = "DATipoSancion";
        #endregion

        #region --[Constructor]--
        public DATipoSancion()
        {
        }

        public DATipoSancion(DATransaction objDATransaction)
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

        public override TipoSancion GetById(TipoSancion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(TipoSancion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(TipoSancion entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(TipoSancion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(TipoSancion entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
