using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DAMotivoSancion : DataAccesBase<MotivoSancion>
    {
        #region --[Atributos]--
        private const string ClassName = "DAMotivoSancion";
        #endregion

        #region --[Constructor]--
        public DAMotivoSancion()
        {
        }

        public DAMotivoSancion(DATransaction objDATransaction)
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

        public override MotivoSancion GetById(MotivoSancion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(MotivoSancion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(MotivoSancion entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(MotivoSancion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(MotivoSancion entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
