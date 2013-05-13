using EDUAR_Entities.Shared;

namespace EDUAR_Entities
{
    public class TipoDocumento : DTBase
    {
        public int idTipoDocumento { get; set; }
        public int idTipoDocumentoTransaccional { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public bool? activo { get; set; }

        public TipoDocumento()
        {
            activo = null;
        }

        ~TipoDocumento()
        {

        }
    }
}
