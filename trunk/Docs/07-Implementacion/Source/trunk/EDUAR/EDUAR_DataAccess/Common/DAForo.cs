using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DAForo : DataAccesBase<Foro>
    {
        #region --[Atributos]--
        private const string ClassName = "DAForo";
        #endregion

        #region --[Constructor]--
        public DAForo()
        {
        }

        public DAForo(DATransaction objDATransaction)
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

        public override Foro GetById(Foro entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Foro entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Foro entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(Foro entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Foro entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
