using EDUAR_Entities.Shared;

namespace EDUAR_Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class InstanciaCalificacion: DTBase
    {
        public int idInstanciaCalificacion { get; set; }
        public string nombre { get; set; }

        public InstanciaCalificacion()
        { }

        ~InstanciaCalificacion()
        {

        }

        public virtual void Dispose()
        {

        }

    }
}
