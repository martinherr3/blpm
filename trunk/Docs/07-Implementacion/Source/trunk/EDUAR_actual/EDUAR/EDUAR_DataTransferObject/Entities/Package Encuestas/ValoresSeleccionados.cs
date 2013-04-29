using EDUAR_Entities.Shared;
using System;

namespace EDUAR_Entities
{
    [Serializable]
    public class ValoresSeleccionados : DTBase
    {
        public int idValorEscala { get; set; }
        public int cantidad { get; set; }

        public ValoresSeleccionados()
        {
        }

        ~ValoresSeleccionados()
        {
        }

        public virtual void Dispose()
        {
        }
    }
}