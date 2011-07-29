using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DAExcursion : DataAccesBase<Excursion>
    {
        #region --[Atributos]--
        private const string ClassName = "DAExcursion";
        #endregion

        #region --[Constructor]--
        public DAExcursion()
        {
        }

        public DAExcursion(DATransaction objDATransaction)
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

        public override Excursion GetById(Excursion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Excursion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Excursion entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(Excursion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Excursion entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
