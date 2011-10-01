using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DAMotivoAusencia : DataAccesBase<MotivoAusencia>
    {
        #region --[Atributos]--
        private const string ClassName = "DAMotivoAusencia";
        #endregion

        #region --[Constructor]--
        public DAMotivoAusencia()
        {
        }

        public DAMotivoAusencia(DATransaction objDATransaction)
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

        public override MotivoAusencia GetById(MotivoAusencia entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(MotivoAusencia entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(MotivoAusencia entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(MotivoAusencia entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(MotivoAusencia entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
