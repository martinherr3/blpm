using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DAPlan : DataAccesBase<Plan>
    {
        #region --[Atributos]--
        private const string ClassName = "DAPlan";
        #endregion

        #region --[Constructor]--
        public DAPlan()
        {
        }

        public DAPlan(DATransaction objDATransaction)
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

        public override Plan GetById(Plan entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Plan entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Plan entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(Plan entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Plan entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
