using EDUAR_Entities.Shared;
using System;

namespace EDUAR_Entities
{
    [Serializable]
    public class CategoriaPregunta: DTBase
    {
        public int idCategoriaPregunta { get; set; }
        public AmbitoEncuesta ambito { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public bool disponible { get; set; }

        public CategoriaPregunta()
        {
            ambito = new AmbitoEncuesta();
        }

        ~CategoriaPregunta()
        {

        }

        public virtual void Dispose()
        {

        }
    }
}