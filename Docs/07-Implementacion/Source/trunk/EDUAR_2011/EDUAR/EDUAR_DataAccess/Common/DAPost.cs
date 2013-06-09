using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DAPost : DataAccesBase<Post>
    {
        #region --[Atributos]--
        private const string ClassName = "DAPost";
        #endregion

        #region --[Constructor]--
        public DAPost()
        {
        }

        public DAPost(DATransaction objDATransaction)
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

        public override Post GetById(Post entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Post entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Post entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(Post entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Post entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
