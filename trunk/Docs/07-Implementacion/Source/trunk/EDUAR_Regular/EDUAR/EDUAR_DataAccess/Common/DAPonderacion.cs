using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DAPonderacion : DataAccesBase<Ponderacion>
    {
        #region --[Atributos]--
        private const string ClassName = "DAPonderacion";
        #endregion

        #region --[Constructor]--
        public DAPonderacion()
        {
        }

        public DAPonderacion(DATransaction objDATransaction)
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

        public override Ponderacion GetById(Ponderacion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Ponderacion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Ponderacion entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(Ponderacion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Ponderacion entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
