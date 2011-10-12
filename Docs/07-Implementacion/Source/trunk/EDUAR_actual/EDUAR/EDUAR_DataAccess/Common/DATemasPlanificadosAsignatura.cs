using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DATemasPlanificadosAsignatura : DataAccesBase<TemasPlanificadosAsignatura>
    {
        #region --[Atributos]--
        private const string ClassName = "DATemasPlanificadosAsignatura";
        #endregion

        #region --[Constructor]--
        public DATemasPlanificadosAsignatura()
        {
        }

        public DATemasPlanificadosAsignatura(DATransaction objDATransaction)
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

        public override TemasPlanificadosAsignatura GetById(TemasPlanificadosAsignatura entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(TemasPlanificadosAsignatura entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(TemasPlanificadosAsignatura entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(TemasPlanificadosAsignatura entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(TemasPlanificadosAsignatura entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
