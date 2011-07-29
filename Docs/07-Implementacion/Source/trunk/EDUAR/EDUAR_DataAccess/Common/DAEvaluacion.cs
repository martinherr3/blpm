using EDUAR_DataAccess.Shared;
using EDUAR_Entities;
using System;
using System.Data;
using System.Data.SqlClient;
using EDUAR_Utility.Excepciones;
using EDUAR_Utility.Enumeraciones;
using System.Collections.Generic;

namespace EDUAR_DataAccess.Common
{
    public class DAEvaluacion : DataAccesBase<Evaluacion>
    {
        #region --[Atributos]--
        private const string ClassName = "DAEvaluacion";
        #endregion

        #region --[Constructor]--
        public DAEvaluacion()
        {
        }

        public DAEvaluacion(DATransaction objDATransaction)
            : base(objDATransaction)
        {

        }
        #endregion

        #region --[Métodos Públicos]--
        
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

        public override Evaluacion GetById(Evaluacion entidad)
        {
            throw new NotImplementedException();
           
        }

        public override void Create(Evaluacion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Create(Evaluacion entidad, out int identificador)
        {
            throw new NotImplementedException();          
        }

        public override void Update(Evaluacion entidad)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Evaluacion entidad)
        {
            throw new NotImplementedException();
            
        }
        #endregion
    }
}
