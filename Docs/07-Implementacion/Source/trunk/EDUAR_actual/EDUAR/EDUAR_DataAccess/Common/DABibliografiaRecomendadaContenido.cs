using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DABibliografiaRecomendadaContenido : DataAccesBase<BibliografiaRecomendadaContenido>
    {
        #region --[Atributos]--
        private const string ClassName = "DABibliografiaRecomendadaContenido";
        #endregion

        #region --[Constructor]--
        public DABibliografiaRecomendadaContenido()
        {
        }

        public DABibliografiaRecomendadaContenido(DATransaction objDATransaction)
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

        public override BibliografiaRecomendadaContenido GetById(BibliografiaRecomendadaContenido entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(BibliografiaRecomendadaContenido entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(BibliografiaRecomendadaContenido entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(BibliografiaRecomendadaContenido entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(BibliografiaRecomendadaContenido entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
