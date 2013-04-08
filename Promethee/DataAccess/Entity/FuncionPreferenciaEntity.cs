using System;

namespace DataAccess.Entity
{
    [Serializable]
    public class FuncionPreferenciaEntity
    {
        public int idFuncionPreferencia { get; set; }
        public string nombre { get; set; }
        public string ayuda { get; set; }
    }
}
