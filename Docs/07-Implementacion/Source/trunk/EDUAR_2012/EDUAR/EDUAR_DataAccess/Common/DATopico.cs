using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DATopico : DataAccesBase<Topico>
    {
        #region --[Atributos]--
        private const string ClassName = "DATopico";
        #endregion

        #region --[Constructor]--
        public DATopico()
        {
        }

        public DATopico(DATransaction objDATransaction)
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

        public override Topico GetById(Topico entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Topico entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Topico entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(Topico entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Topico entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
