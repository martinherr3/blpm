using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DAEscala : DataAccesBase<Escala>
    {
        #region --[Atributos]--
        private const string ClassName = "DAEscala";
        #endregion

        #region --[Constructor]--
        public DAEscala()
        {
        }

        public DAEscala(DATransaction objDATransaction)
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

        public override Escala GetById(Escala entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Escala entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Escala entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(Escala entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Escala entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
