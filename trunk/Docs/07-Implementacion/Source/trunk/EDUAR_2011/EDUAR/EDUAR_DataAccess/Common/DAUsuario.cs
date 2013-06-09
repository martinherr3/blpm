using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DAUsuario : DataAccesBase<Usuario>
    {
        #region --[Atributos]--
        private const string ClassName = "DAUsuario";
        #endregion

        #region --[Constructor]--
        public DAUsuario()
        {
        }

        public DAUsuario(DATransaction objDATransaction)
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

        public override Usuario GetById(Usuario entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Usuario entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Usuario entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(Usuario entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Usuario entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
