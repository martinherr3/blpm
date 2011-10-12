using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DACalificacion : DataAccesBase<Calificacion>
    {
        #region --[Atributos]--
        private const string ClassName = "DACalificacion";
        #endregion

        #region --[Constructor]--
        public DACalificacion()
        {
        }

        public DACalificacion(DATransaction objDATransaction)
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

        public override Calificacion GetById(Calificacion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Calificacion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Calificacion entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(Calificacion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Calificacion entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
