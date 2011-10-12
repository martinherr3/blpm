using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DATipoTutor : DataAccesBase<TipoTutor>
    {
        #region --[Atributos]--
        private const string ClassName = "DATipoTutor";
        #endregion

        #region --[Constructor]--
        public DATipoTutor()
        {
        }

        public DATipoTutor(DATransaction objDATransaction)
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

        public override TipoTutor GetById(TipoTutor entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(TipoTutor entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(TipoTutor entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(TipoTutor entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(TipoTutor entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
