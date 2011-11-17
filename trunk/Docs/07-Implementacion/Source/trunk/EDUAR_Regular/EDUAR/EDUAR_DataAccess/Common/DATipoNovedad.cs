using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DATipoNovedad : DataAccesBase<TipoNovedad>
    {
        #region --[Atributos]--
        private const string ClassName = "DATipoNovedad";
        #endregion

        #region --[Constructor]--
        public DATipoNovedad()
        {
        }

        public DATipoNovedad(DATransaction objDATransaction)
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

        public override TipoNovedad GetById(TipoNovedad entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(TipoNovedad entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(TipoNovedad entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(TipoNovedad entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(TipoNovedad entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
