using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DANivel : DataAccesBase<Nivel>
    {
        #region --[Atributos]--
        private const string ClassName = "DANivel";
        #endregion

        #region --[Constructor]--
        public DANivel()
        {
        }

        public DANivel(DATransaction objDATransaction)
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

        public override Nivel GetById(Nivel entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Nivel entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Nivel entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(Nivel entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Nivel entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
