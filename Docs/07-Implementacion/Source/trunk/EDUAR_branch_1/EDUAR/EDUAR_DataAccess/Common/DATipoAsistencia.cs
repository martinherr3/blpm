using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DATipoAsistencia : DataAccesBase<TipoAsistencia>
    {
        #region --[Atributos]--
        private const string ClassName = "DATipoAsistencia";
        #endregion

        #region --[Constructor]--
        public DATipoAsistencia()
        {
        }

        public DATipoAsistencia(DATransaction objDATransaction)
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

        public override TipoAsistencia GetById(TipoAsistencia entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(TipoAsistencia entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(TipoAsistencia entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(TipoAsistencia entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(TipoAsistencia entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
