using System;
using System.Collections.Generic;
using System.Text;


namespace SMSapplication
{
    class clsCtrlPausas
    {
        private Int32 _enviosExitosos;
		//private Int32 _exitososConsec;
        private Int32 _enviosFallidos;
        private Int32 _enviosFallidosConsec;
        
		//private Int32 _pausaActual;
		//private Int32 _intervaloEntreEnviosActual;


      

        public void IncrementarEnviosExitosos()
        {
            _enviosExitosos++;
            _enviosFallidosConsec++;
			//_exitososConsec = 0;
        }

        public void IncrementarEnviosFallidos()
        {
            _enviosFallidos++;
            _enviosFallidosConsec++;
			//_exitososConsec = 0;
        }

       
     
    }
}
