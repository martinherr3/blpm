using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DASancion : DataAccesBase<Sancion>
    {
        #region --[Atributos]--
        private const string ClassName = "DASancion";
        #endregion

        #region --[Constructor]--
        public DASancion()
        {
        }

        public DASancion(DATransaction objDATransaction)
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

        public override Sancion GetById(Sancion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Sancion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Sancion entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(Sancion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Sancion entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
