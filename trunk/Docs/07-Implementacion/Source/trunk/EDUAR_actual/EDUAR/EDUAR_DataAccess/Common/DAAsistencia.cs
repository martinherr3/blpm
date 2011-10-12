using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DAAsistencia : DataAccesBase<Asistencia>
    {
        #region --[Atributos]--
        private const string ClassName = "DAAsistencia";
        #endregion

        #region --[Constructor]--
        public DAAsistencia()
        {
        }

        public DAAsistencia(DATransaction objDATransaction)
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

        public override Asistencia GetById(Asistencia entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Asistencia entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Asistencia entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(Asistencia entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Asistencia entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
