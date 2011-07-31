using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DAPersonal : DataAccesBase<Personal>
    {
        #region --[Atributos]--
        private const string ClassName = "DAPersonal";
        #endregion

        #region --[Constructor]--
        public DAPersonal()
        {
        }

        public DAPersonal(DATransaction objDATransaction)
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

        public override Personal GetById(Personal entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Personal entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Personal entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(Personal entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Personal entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
