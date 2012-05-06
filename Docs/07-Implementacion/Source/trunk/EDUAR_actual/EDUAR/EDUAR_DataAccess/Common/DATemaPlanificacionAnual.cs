using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DATemaPlanificacionAnual : DataAccesBase<TemaPlanificacionAnual>
    {
        #region --[Atributos]--
        private const string ClassName = "DATemasPlanificadosAsignatura";
        #endregion

        #region --[Constructor]--
        public DATemaPlanificacionAnual()
        {
        }

        public DATemaPlanificacionAnual(DATransaction objDATransaction)
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

        public override TemaPlanificacionAnual GetById(TemaPlanificacionAnual entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(TemaPlanificacionAnual entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(TemaPlanificacionAnual entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(TemaPlanificacionAnual entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(TemaPlanificacionAnual entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
