using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DASeccion : DataAccesBase<Seccion>
    {
        #region --[Atributos]--
        private const string ClassName = "DASeccion";
        #endregion

        #region --[Constructor]--
        public DASeccion()
        {
        }

        public DASeccion(DATransaction objDATransaction)
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

        public override Seccion GetById(Seccion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Seccion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Seccion entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(Seccion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Seccion entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
