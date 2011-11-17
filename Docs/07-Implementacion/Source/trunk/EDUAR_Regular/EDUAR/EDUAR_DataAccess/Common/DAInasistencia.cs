using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DAInasistencia : DataAccesBase<Inasistencia>
    {
        #region --[Atributos]--
        private const string ClassName = "DAInasistencia";
        #endregion

        #region --[Constructor]--
        public DAInasistencia()
        {
        }

        public DAInasistencia(DATransaction objDATransaction)
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

        public override Inasistencia GetById(Inasistencia entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Inasistencia entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Inasistencia entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(Inasistencia entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Inasistencia entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
