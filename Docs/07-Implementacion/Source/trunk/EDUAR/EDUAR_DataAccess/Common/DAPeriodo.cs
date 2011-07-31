using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DAPeriodo : DataAccesBase<Periodo>
    {
        #region --[Atributos]--
        private const string ClassName = "DAPeriodo";
        #endregion

        #region --[Constructor]--
        public DAPeriodo()
        {
        }

        public DAPeriodo(DATransaction objDATransaction)
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

        public override Periodo GetById(Periodo entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Periodo entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Periodo entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(Periodo entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Periodo entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
