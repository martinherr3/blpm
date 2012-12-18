using System;
using EDUAR_Entities.Shared;
namespace EDUAR_Entities
{
    [Serializable]
    public class EscalaCalificacion: DTBase
    {
        public decimal idEscalaCalificacion { get; set; }
        public decimal idEscalaCalificacionTransaccional { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public Boolean activo { get; set; }
        public decimal idTipoEscalaCalificacion { get; set; }
    }
}
