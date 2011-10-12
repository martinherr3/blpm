using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DAPerfil : DataAccesBase<Perfil>
    {
        #region --[Atributos]--
        private const string ClassName = "DAPerfil";
        #endregion

        #region --[Constructor]--
        public DAPerfil()
        {
        }

        public DAPerfil(DATransaction objDATransaction)
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

        public override Perfil GetById(Perfil entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Perfil entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Perfil entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(Perfil entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Perfil entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
