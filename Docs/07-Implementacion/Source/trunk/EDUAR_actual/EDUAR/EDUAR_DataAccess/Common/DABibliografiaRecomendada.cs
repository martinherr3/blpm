using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DABibliografiaRecomendada : DataAccesBase<BibliografiaRecomendada>
    {
        #region --[Atributos]--
        private const string ClassName = "DABibliografiaRecomendada";
        #endregion

        #region --[Constructor]--
        public DABibliografiaRecomendada()
        {
        }

        public DABibliografiaRecomendada(DATransaction objDATransaction)
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

        public override BibliografiaRecomendada GetById(BibliografiaRecomendada entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(BibliografiaRecomendada entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(BibliografiaRecomendada entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(BibliografiaRecomendada entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(BibliografiaRecomendada entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
