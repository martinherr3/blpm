using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DACurso : DataAccesBase<Curso>
    {
        #region --[Atributos]--
        private const string ClassName = "DACurso";
        #endregion

        #region --[Constructor]--
        public DACurso()
        {
        }

        public DACurso(DATransaction objDATransaction)
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

        public override Curso GetById(Curso entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Curso entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Curso entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(Curso entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Curso entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
