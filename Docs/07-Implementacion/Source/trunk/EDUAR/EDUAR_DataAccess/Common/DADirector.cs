using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DADirector : DataAccesBase<Director>
    {
        #region --[Atributos]--
        private const string ClassName = "DADirector";
        #endregion

        #region --[Constructor]--
        public DADirector()
        {
        }

        public DADirector(DATransaction objDATransaction)
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

        public override Director GetById(Director entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Director entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Director entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(Director entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Director entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
