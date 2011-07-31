using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DADiasHorarios : DataAccesBase<DiasHorarios>
    {
        #region --[Atributos]--
        private const string ClassName = "DADiasHorarios";
        #endregion

        #region --[Constructor]--
        public DADiasHorarios()
        {
        }

        public DADiasHorarios(DATransaction objDATransaction)
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

        public override DiasHorarios GetById(DiasHorarios entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(DiasHorarios entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(DiasHorarios entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(DiasHorarios entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(DiasHorarios entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
