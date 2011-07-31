using System;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities;

namespace EDUAR_DataAccess.Common
{
    public class DATutor : DataAccesBase<Tutor>
    {
        #region --[Atributos]--
        private const string ClassName = "DATutor";
        #endregion

        #region --[Constructor]--
        public DATutor()
        {
        }

        public DATutor(DATransaction objDATransaction)
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

        public override Tutor GetById(Tutor entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Tutor entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Tutor entidad, out int identificador)
        {
            throw new NotImplementedException();
        }

        public override void Update(Tutor entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Tutor entidad)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --[Métodos Públicos]--
        #endregion
    }
}
