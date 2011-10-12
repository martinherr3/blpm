using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DACargoPersonal : DataAccesBase<CargoPersonal>
    {
        #region --[Atributos]--
        private const string ClassName = "DACargoPersonal";
        #endregion

        #region --[Constructor]--
        public DACargoPersonal()
        {
        }

        public DACargoPersonal(DATransaction objDATransaction)
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

        public override CargoPersonal GetById(CargoPersonal entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(CargoPersonal entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(CargoPersonal entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(CargoPersonal entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(CargoPersonal entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
