using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DATipoEventoInstitucional : DataAccesBase<TipoEventoInstitucional>
    {
        #region --[Atributos]--
        private const string ClassName = "DATipoEventoInstitucional";
        #endregion

        #region --[Constructor]--
        public DATipoEventoInstitucional()
        {
        }

        public DATipoEventoInstitucional(DATransaction objDATransaction)
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

        public override TipoEventoInstitucional GetById(TipoEventoInstitucional entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(TipoEventoInstitucional entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(TipoEventoInstitucional entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(TipoEventoInstitucional entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(TipoEventoInstitucional entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
