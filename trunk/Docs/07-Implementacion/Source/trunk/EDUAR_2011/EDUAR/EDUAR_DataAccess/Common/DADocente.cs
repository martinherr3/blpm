using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DADocente : DataAccesBase<Docente>
    {
        #region --[Atributos]--
        private const string ClassName = "DADocente";
        #endregion

        #region --[Constructor]--
        public DADocente()
        {
        }

        public DADocente(DATransaction objDATransaction)
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

        public override Docente GetById(Docente entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Docente entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Docente entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(Docente entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Docente entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
