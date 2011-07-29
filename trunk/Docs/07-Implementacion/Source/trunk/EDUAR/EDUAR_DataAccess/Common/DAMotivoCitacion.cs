using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DAMotivoCitacion : DataAccesBase<MotivoCitacion>
    {
        #region --[Atributos]--
        private const string ClassName = "DAMotivoCitacion";
        #endregion

        #region --[Constructor]--
        public DAMotivoCitacion()
        {
        }

        public DAMotivoCitacion(DATransaction objDATransaction)
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

        public override MotivoCitacion GetById(MotivoCitacion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(MotivoCitacion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(MotivoCitacion entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(MotivoCitacion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(MotivoCitacion entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
