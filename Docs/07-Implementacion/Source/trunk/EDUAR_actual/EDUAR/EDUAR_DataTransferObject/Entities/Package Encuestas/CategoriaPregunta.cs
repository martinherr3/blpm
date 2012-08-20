using EDUAR_Entities.Shared;

namespace EDUAR_Entities
{
    public class CategoriaPregunta: DTBase
    {
        public int idCategoriaPregunta { get; set; }
        public AmbitoEncuesta ambito { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }

        public CategoriaPregunta()
        {

        }

        ~CategoriaPregunta()
        {

        }

        public virtual void Dispose()
        {

        }
    }
}