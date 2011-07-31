using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DACicloLectivo : DataAccesBase<CicloLectivo>
    {
        #region --[Atributos]--
        private const string ClassName = "DACicloLectivo";
        #endregion

        #region --[Constructor]--
        public DACicloLectivo()
        {
        }

        public DACicloLectivo(DATransaction objDATransaction)
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

        public override CicloLectivo GetById(CicloLectivo entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(CicloLectivo entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(CicloLectivo entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(CicloLectivo entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(CicloLectivo entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
