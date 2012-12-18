
using EDUAR_Entities.Shared;
using System;
namespace EDUAR_Entities
{
    [Serializable]
    public class AmbitoEncuesta : DTBase
    {
        public int idAmbitoEncuesta { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }

        public AmbitoEncuesta()
        {

        }

        ~AmbitoEncuesta()
        {

        }

        public virtual void Dispose()
        {

        }
    }
}
