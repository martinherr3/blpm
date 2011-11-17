using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DAPsicopedagogo : DataAccesBase<Psicopedagogo>
    {
        #region --[Atributos]--
        private const string ClassName = "DAPsicopedagogo";
        #endregion

        #region --[Constructor]--
        public DAPsicopedagogo()
        {
        }

        public DAPsicopedagogo(DATransaction objDATransaction)
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

        public override Psicopedagogo GetById(Psicopedagogo entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Psicopedagogo entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Psicopedagogo entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(Psicopedagogo entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Psicopedagogo entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
