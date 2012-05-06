using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DAPlanificacionAnual : DataAccesBase<PlanificacionAnual>
    {
        #region --[Atributos]--
        private const string ClassName = "DAPlanificacionAnual";
        #endregion

        #region --[Constructor]--
        public DAPlanificacionAnual()
        {
        }

        public DAPlanificacionAnual(DATransaction objDATransaction)
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

        public override PlanificacionAnual GetById(PlanificacionAnual entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(PlanificacionAnual entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(PlanificacionAnual entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(PlanificacionAnual entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(PlanificacionAnual entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
