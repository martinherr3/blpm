using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DAAlumnoCurso : DataAccesBase<AlumnoCurso>
    {
        #region --[Atributos]--
        private const string ClassName = "DAAlumnoCurso";
        #endregion

        #region --[Constructor]--
        public DAAlumnoCurso()
        {
        }

        public DAAlumnoCurso(DATransaction objDATransaction)
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

        public override AlumnoCurso GetById(AlumnoCurso entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(AlumnoCurso entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(AlumnoCurso entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(AlumnoCurso entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(AlumnoCurso entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
