using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DAFeriadosYFechasEspeciales : DataAccesBase<FeriadosYFechasEspeciales>
    {
        #region --[Atributos]--
        private const string ClassName = "DAFeriadosYFechasEspeciales";
        #endregion

        #region --[Constructor]--
        public DAFeriadosYFechasEspeciales()
        {
        }

        public DAFeriadosYFechasEspeciales(DATransaction objDATransaction)
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

        public override FeriadosYFechasEspeciales GetById(FeriadosYFechasEspeciales entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(FeriadosYFechasEspeciales entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(FeriadosYFechasEspeciales entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(FeriadosYFechasEspeciales entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(FeriadosYFechasEspeciales entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
