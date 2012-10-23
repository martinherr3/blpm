﻿using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;
using SMSapplication;
using System.Collections.Generic;

namespace EDUAR_Utility.Utilidades
{
    [Serializable]
    public class EDUARSMS
    {
        #region --[Atributos]--
        /// <summary>
        /// nombre clase
        /// </summary>
        private const string ClassName = "EDUARSMS";

        private clsSMS _clsSms;

        private List<String> _puertos;

        #endregion

        #region --[Constructor]--

        /// <summary>
        /// Constructor.
        /// </summary>
        public EDUARSMS()
        {
            _clsSms = new SMSapplication.clsSMS();

        }

        #endregion

        #region --[Métodos Publicos]--


        public void EnviarSMS(string nroDestino, string cuerpo)
        {
            try
            {
               // conectarse();
                _clsSms.sendMsg(nroDestino, cuerpo);
               // _clsSms.ClosePort();
                
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - EnviarMail()", ClassName),
                                                        ex, enuExceptionType.Exception);
            }

        }

        public void Desconectarse()
        {
            try
            {
                _clsSms.ClosePort();

            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - EnviarSMS()", ClassName),
                                                        ex, enuExceptionType.Exception);
            }
        }
        #endregion

        #region --[Métodos Privados]--


        public bool Conectarse()
        {
            bool retValue = false;

            try
            {
                _puertos = clsSMS.GetPortNameValues();
                foreach (String puerto in _puertos)
                {
                    clsSMS LineaAux = new clsSMS();
                    if (_clsSms.TryOpenPort(puerto, 230400, 8, 300, 300))
                    {
                        _clsSms.ReadIMEI();
                        _clsSms.ReadSIMID();
                        retValue = true;
                        //MessageBox.Show("Abrio Puerto " + puerto);
                    }
                }


            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - EnviarMail()", ClassName),
                                                        ex, enuExceptionType.Exception);
            }

            return (retValue);
        }
        #endregion
    }
}