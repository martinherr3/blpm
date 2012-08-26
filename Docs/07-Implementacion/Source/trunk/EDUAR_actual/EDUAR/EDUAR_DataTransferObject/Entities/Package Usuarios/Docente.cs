
using System;
using EDUAR_Entities.Shared;
namespace EDUAR_Entities
{
    [Serializable]
    public class Docente : Personal
    {
        public int idDocente { get; set; }
        public int idDocenteTransaccional { get; set; }
    }
}
