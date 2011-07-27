using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DACitacion : DataAccesBase<Citacion>
    {
        #region --[Atributos]--
        private const string ClassName = "DACitacion";
        #endregion

        #region --[Constructor]--
        public DACitacion()
        {
        }

        public DACitacion(DATransaction objDATransaction)
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

        public override Citacion GetById(Citacion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Citacion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Citacion entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(Citacion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Citacion entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
