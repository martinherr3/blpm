using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DAContenido : DataAccesBase<Contenido>
    {
        #region --[Atributos]--
        private const string ClassName = "DAContenido";
        #endregion

        #region --[Constructor]--
        public DAContenido()
        {
        }

        public DAContenido(DATransaction objDATransaction)
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

        public override Contenido GetById(Contenido entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Contenido entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Contenido entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(Contenido entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Contenido entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
