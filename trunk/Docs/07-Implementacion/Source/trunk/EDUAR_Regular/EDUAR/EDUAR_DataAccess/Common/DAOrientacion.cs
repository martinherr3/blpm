using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DAOrientacion : DataAccesBase<Orientacion>
    {
        #region --[Atributos]--
        private const string ClassName = "DAOrientacion";
        #endregion

        #region --[Constructor]--
        public DAOrientacion()
        {
        }

        public DAOrientacion(DATransaction objDATransaction)
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

        public override Orientacion GetById(Orientacion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Orientacion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Orientacion entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(Orientacion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Orientacion entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
