using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DAPregunta : DataAccesBase<Pregunta>
    {
        #region --[Atributos]--
        private const string ClassName = "DAPregunta";
        #endregion

        #region --[Constructor]--
        public DAPregunta()
        {
        }

        public DAPregunta(DATransaction objDATransaction)
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

        public override Pregunta GetById(Pregunta entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Pregunta entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Pregunta entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(Pregunta entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Pregunta entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
