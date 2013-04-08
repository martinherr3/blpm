using System;
using System.Collections.Generic;

namespace DataAccess.Entity
{
    [Serializable]
    public class IndicadorEntity
    {
        public int idIndicador { get; set; }
        public string nombre { get; set; }
        public decimal pesoDefault { get; set; }
        public string escala { get; set; }
        public decimal pesoMinimo { get; set; }
        public decimal pesoMaximo { get; set; }
        public bool maximiza { get; set; }
        public List<ConfigFuncionPreferenciaEntity> listaConfig { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="IndicadorEntity"/> class.
        /// </summary>
        public IndicadorEntity()
        {
            listaConfig = new List<ConfigFuncionPreferenciaEntity>();
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="IndicadorEntity"/> class.
        /// </summary>
        ~IndicadorEntity()
        {

        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        public virtual void Dispose()
        {
        }
    }
}
