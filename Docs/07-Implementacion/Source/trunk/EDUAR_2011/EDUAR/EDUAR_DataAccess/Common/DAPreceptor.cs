using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DAPreceptor : DataAccesBase<Preceptor>
    {
        #region --[Atributos]--
        private const string ClassName = "DAPreceptor";
        #endregion

        #region --[Constructor]--
        public DAPreceptor()
        {
        }

        public DAPreceptor(DATransaction objDATransaction)
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

        public override Preceptor GetById(Preceptor entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Preceptor entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Preceptor entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(Preceptor entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Preceptor entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
