using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DANovedad : DataAccesBase<Novedad>
    {
        #region --[Atributos]--
        private const string ClassName = "DANovedad";
        #endregion

        #region --[Constructor]--
        public DANovedad()
        {
        }

        public DANovedad(DATransaction objDATransaction)
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

        public override Novedad GetById(Novedad entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Novedad entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Novedad entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(Novedad entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Novedad entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
