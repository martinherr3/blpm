using EDUAR_Entities.Shared;

namespace EDUAR_Entities
{
    public class TipoDocumento : DTBase
    {
        private int _idTipoDocumento;
        public int idTipoDocumento
        {
            get { return _idTipoDocumento; }
            set { _idTipoDocumento = value; }
        }
        private int _idTipoDocumentoTransaccional;
        public int idTipoDocumentoTransaccional
        {
            get { return _idTipoDocumentoTransaccional; }
            set { _idTipoDocumentoTransaccional = value; }
        }
        private string _nombre;
        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        private string _descripcion;
        public string descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
        private bool? _activo;
        public bool? activo
        {
            get { return _activo; }
            set { _activo = value; }
        }

        public TipoDocumento()
        {
            activo = null;
        }

        ~TipoDocumento()
        {

        }
    }
}
