using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DAReunion : DataAccesBase<Reunion>
    {
        #region --[Atributos]--
        private const string ClassName = "DAReunion";
        #endregion

        #region --[Constructor]--
        public DAReunion()
        {
        }

        public DAReunion(DATransaction objDATransaction)
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

        public override Reunion GetById(Reunion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Reunion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Reunion entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(Reunion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Reunion entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
