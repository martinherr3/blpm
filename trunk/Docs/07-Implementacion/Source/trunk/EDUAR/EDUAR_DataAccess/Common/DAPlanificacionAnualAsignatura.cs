using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DAPlanificacionAnualAsignatura : DataAccesBase<PlanificacionAnualAsignatura>
    {
        #region --[Atributos]--
        private const string ClassName = "DAPlanificacionAnualAsignatura";
        #endregion

        #region --[Constructor]--
        public DAPlanificacionAnualAsignatura()
        {
        }

        public DAPlanificacionAnualAsignatura(DATransaction objDATransaction)
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

        public override PlanificacionAnualAsignatura GetById(PlanificacionAnualAsignatura entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(PlanificacionAnualAsignatura entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(PlanificacionAnualAsignatura entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(PlanificacionAnualAsignatura entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(PlanificacionAnualAsignatura entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
