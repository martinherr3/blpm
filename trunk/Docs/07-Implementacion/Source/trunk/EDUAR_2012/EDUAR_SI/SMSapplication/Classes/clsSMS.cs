using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;

namespace SMSapplication
{
    public class clsSMS
    {
        enum EnumEstado
        {
            ENVIANDO_SMS,
            RECIVIENDO_SMS,
            DISPONIBLE, 
            DESCONECTADO,
            PAUSA_FALLIDOS
        }

        #region Private Variables

        SerialPort _comm;

        Stream _SafeBaseStream;

        private Boolean  _disponible;
        private String _imei;
        private String _simid;
        private String _causa;
        private String _numActual;
        private EnumEstado _estado;
        private DateTime _finPausaEnvio;
        private String _signal;
        private clsCtrlPausas _ctrlPausas = new clsCtrlPausas();

        //private static readonly log4net.ILog //_logger = log4net.LogManager.GetLogger("CLSSMS");
        #endregion

        #region Property
        public Boolean Disponible
        {
            get
            {
                return _disponible;
            }
            set
            {
                lock (this)
                {
                    _disponible = value;
                }

            }
        }

        public String IMEI
        {
            get { return _imei; }
            set { _imei = value; }
        }
        public String SIMID
        {
            get { return _simid; }
            set { _simid = value; }
        }

        public String Causa
        {
            get { return _causa; }
            set { _causa = value; }
        }

        public String NumActual
        {
            get { return _numActual; }
            set { _numActual = value; }
        }

        public String Status
        {
            get { return _estado.ToString(); }

        }

        public DateTime FinPausaEnvio
        {
            get { return _finPausaEnvio; }
            set { _finPausaEnvio = value; }
        }

        public String Signal
        {
            get { return _signal; }
            set { _signal = value; }
        }

        #endregion

        public clsSMS()
        {
            //XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4netSMSWS.config"));
            _estado = EnumEstado.DISPONIBLE;
           // //_logger.Debug("INICIANDO CLS");
        }
        #region Open and Close Ports
        //Open Port
        public Boolean TryOpenPort(string p_strPortName, int p_uBaudRate, int p_uDataBits, int p_uReadTimeout, int p_uWriteTimeout)
        {
            _comm = OpenPort( p_strPortName,  p_uBaudRate,  p_uDataBits,  p_uReadTimeout,  p_uWriteTimeout);
            

            if (_comm != null)
            {
                _SafeBaseStream = _comm.BaseStream;
                Inicializar();
                return true;
            }
            else 
            {
                return false;
            }
            
        }

        public SerialPort OpenPort(string p_strPortName, int p_uBaudRate, int p_uDataBits, int p_uReadTimeout, int p_uWriteTimeout)
        {
            receiveNow = new AutoResetEvent(false);
            SerialPort port = new SerialPort();

            try
            {           
                port.PortName = p_strPortName;                 //COM1
                port.BaudRate = p_uBaudRate;                   //9600
                port.DataBits = p_uDataBits;                   //8
                port.StopBits = StopBits.One;                  //1
                port.Parity = Parity.None;                     //None
                port.ReadTimeout = p_uReadTimeout;             //300
                port.WriteTimeout = p_uWriteTimeout;           //300
                port.Encoding = Encoding.GetEncoding("iso-8859-1");
                port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
                port.Open();
                port.DtrEnable = true;
                port.RtsEnable = true;
               
            }
            catch (Exception ex)
            {
                ////_logger.Error("OpenPort", ex);
                return null;
            }
            return port;
        }

        private void Inicializar()
        {
            try
            {
                String command = @"ATE1";
                ExecCommand(_comm, command, 300, "Failed to set ATE1");                

                command = @"AT+CSCS=""GSM""";
                ExecCommand(_comm, command, 300, "Failed to set GSM");                
                
            }
            catch (Exception ex)
            {
                ////_logger.Error("OpenPort" + _causa + " " + _comm.PortName, ex);
                Console.WriteLine(_causa + " " + _comm.PortName);
                _causa = ex.Message;                
            }
            
        }

        #region SetPortNameValues
        public static List<String> GetPortNameValues()
        {
            List<String> lstPorts = new List<string>();

            foreach (String str in SerialPort.GetPortNames())
            {
                lstPorts.Add(str);
            }
            return lstPorts;
        }
        #endregion

        //Close Port
        public void ClosePort()
        {
            try
            {
                try
                {
                    Console.WriteLine("VA A CERRAR!!!!!");
                    Thread.Sleep(3000);
                    _comm.Close();
                    Console.WriteLine("VA A CERRAR1!!!!!");
                }
                catch (Exception ex)
                {
                    throw ex;////_logger.Error("ClosePort1", ex);
                    
                }
                Console.WriteLine("VA A CERRAR2!!!!!");
                _comm.DataReceived -= new SerialDataReceivedEventHandler(port_DataReceived);
                Console.WriteLine("VA A CERRAR3!!!!!");
                GC.SuppressFinalize(_SafeBaseStream);
                Console.WriteLine("VA A CERRAR4!!!!!");
                
                _comm = null;
                Console.WriteLine("VA A CERRAR5!!!!!");
                _SafeBaseStream = null;
                Console.WriteLine("VA A CERRAR6!!!!!");
            }
            catch (Exception ex)
            {
                ////_logger.Error("ClosePort", ex);
                _causa = ex.Message;
                Console.WriteLine("ClosePort " + ex.Message);
            }
        }

        #endregion

        //Execute AT Command
        public string ExecCommand(SerialPort port,string command, int responseTimeout, string errorMessage)
        {
            int idLinea = 0;
            try
            {
                
                port.DiscardOutBuffer();
                idLinea = 1;
                port.DiscardInBuffer();
                idLinea = 2;
                receiveNow.Reset();
                idLinea = 3;
                port.Write(command + "\r");
                idLinea = 4;
                ////_logger.Debug(String.Format("ExecCommand Puerto: {0} Comando: {1}", port.PortName, command));
                idLinea = 5;
                string input = ReadResponse(port, responseTimeout);
                idLinea = 6;
                if ((input.Length == 0) || ((!input.EndsWith("\r\n> ")) && (!input.EndsWith("\r\nOK\r\n"))))
                {                
                    idLinea = 7;
                    throw new ApplicationException("No success message was received. Comando:  "  + command + " leo: " + input);
                    //idLinea = 8;
                }
                return input;
            }
            catch (Exception ex)
            {
               // //_logger.Error("ExecCommand", ex);
                ////_logger.Error("ExecCommand linea: " + idLinea + " " + ex.StackTrace, ex);
                //Console.WriteLine("ExecCommand linea: " + idLinea  + " Ex: " + ex.Message);
                throw new ApplicationException(errorMessage, ex);
            }
        }   

        //Receive data from port
        public void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (e.EventType == SerialData.Chars)
                    receiveNow.Set();
            }
            catch (Exception ex)
            {
               // //_logger.Error("port_DataRecieved", ex);
                throw ex;
            }
        }

        public string ReadResponse(SerialPort port,int timeout)
        {
            string buffer = string.Empty;
            try
            {    
                do
                {
                    if (receiveNow.WaitOne(timeout, false))
                    {
                        string t = port.ReadExisting();
                        buffer += t;
                    }
                    else
                    {
                        if (buffer.Length > 0)
                            throw new ApplicationException("Response received is incomplete.");
                        else
                            throw new ApplicationException("No data received from phone.");
                    }
                }
                while (!buffer.EndsWith("\r\nOK\r\n") && !buffer.EndsWith("\r\n> ") && !buffer.EndsWith("\r\nERROR\r\n") && !buffer.EndsWith("+CMS ERROR: 500\r\n"));
            }
            catch (Exception ex)
            {
                ////_logger.Error("ReadResponse", ex);
                _causa = ex.Message;
                //Console.WriteLine("ReadResponse " + ex.Message);
            }
            ////_logger.Debug(String.Format("ReadResponse: {0}", buffer));
            //Console.WriteLine(String.Format("ReadResponse: {0}", buffer));
            return buffer;
        }

        #region Count SMS
        public int CountSMSmessages()
        {
            int CountTotalMessages = 0;
            try
            {

                #region Execute Command

                string recievedData = ExecCommand(_comm, "AT", 300, "No phone connected at ");                
                recievedData = ExecCommand(_comm, "AT+CMGF=1", 300, "Failed to set message format.");
                String command = "AT+CPMS?";
                recievedData = ExecCommand(_comm, command, 1000, "Failed to count SMS message");
                int uReceivedDataLength = recievedData.Length;
                
                #endregion
               // //_logger.Debug("___________________________recievedData.Length: " + recievedData.Length);
                ////_logger.Debug("___________________________recievedData.StartsWith: " + recievedData.StartsWith("AT+CPMS?").ToString());
                #region If command is executed successfully
                if ((recievedData.Length >= 45) && (recievedData.StartsWith("AT+CPMS?")))
                {
                    #region Parsing SMS
                    string[] strSplit = recievedData.Split(',');
                    string strMessageStorageArea1 = strSplit[0];     //SM
                    string strMessageExist1 = strSplit[1];           //Msgs exist in SM
                    #endregion

                    #region Count Total Number of SMS In SIM
                    CountTotalMessages = Convert.ToInt32(strMessageExist1);
                    #endregion

                }                
                    
                #endregion

                #region If command is not executed successfully
                else if (recievedData.Contains("ERROR"))
                {
                    #region Error in Counting total number of SMS
                    string recievedError = recievedData;
                    recievedError = recievedError.Trim();
                    recievedData = "Following error occured while counting the message" + recievedError;
                    //Console.WriteLine("CountSMSmessages: " + recievedData);
                    //_logger.Error("Following error occured while counting the message" + recievedError);
                    #endregion

                }
                #endregion

                return CountTotalMessages;

            }
            catch (Exception ex)
            {
                //_logger.Error("CountSMSmessages", ex);
                _causa = ex.ToString();
                Console.WriteLine("CountSMSmessages: " + ex.ToString());
                return 0;

            }
           
        }
        #endregion

        #region Read SMS

        public AutoResetEvent receiveNow;

        public ShortMessageCollection ReadSMS()
        {
            _numActual = "";
            _estado = EnumEstado.RECIVIENDO_SMS;
            // Set up the phone and read the messages
            ShortMessageCollection messages = null;
            try
            {

                #region Execute Command
                // Check connection
                ExecCommand(_comm,"AT", 300, "No phone connected");
                // Use message format "Text mode"
                ExecCommand(_comm, "AT+CMGF=1", 300, "Failed to set message format.");
                // Use character set "PCCP437"
                //ExecCommand(port,"AT+CSCS=\"PCCP437\"", 300, "Failed to set character set.");
                // Select SIM storage
                ExecCommand(_comm, "AT+CPMS=\"SM\"", 300, "Failed to select message storage.");
                // Read the messages
                string input = ExecCommand(_comm, "AT+CMGL=\"ALL\"", 5000, "Failed to read the messages.");
                #endregion

                #region Parse messages
                messages = ParseMessages(input);
                #endregion

            }
            catch (Exception ex)
            {
                ////_logger.Error("ReadSMS", ex);
                _causa = ex.Message;
                Console.WriteLine("ReadSMS " + ex.Message);
            }
            _estado = EnumEstado.DISPONIBLE;
            if (messages != null)
                return messages;
            else
                return null;
        
        }
        public ShortMessageCollection ParseMessages(string input)
        {
            ShortMessageCollection messages = new ShortMessageCollection();
            try
            {     
                //Regex r = new Regex(@"\+CMGL: (\d+),""(.+)"",""(.+)"",(.*),""(.+)""\r\n(.+)\r\n");

                String inputAux = input;
                inputAux = inputAux.Replace("\r\n", @"\x");
                inputAux = inputAux.Replace("\n", "");
                inputAux = inputAux.Replace(@"\x", "\r\n");
                Regex r = new Regex(@"\+CMGL: (\d+),""(.+)"",""(.+)"",(.*),""(.+)""\r\n(.+)\r\n"); ;
                Match m = r.Match(inputAux);
                while (m.Success)
                {
                    ShortMessage msg = new ShortMessage();
                    //msg.Index = int.Parse(m.Groups[1].Value);
                    msg.Index = m.Groups[1].Value;
                    msg.Status = m.Groups[2].Value;
                    msg.Sender = m.Groups[3].Value;
                    msg.Alphabet = m.Groups[4].Value;
                    msg.Sent = m.Groups[5].Value;
                    msg.Message = m.Groups[6].Value;
                    messages.Add(msg);

                    m = m.NextMatch();
                }

            }
            catch (Exception ex)
            {
                //_logger.Error("ParseMessages", ex);
                _causa = ex.Message;
                Console.WriteLine("ParseMessages " + ex.Message);
            }
            return messages;
        }

        #endregion

        #region Send SMS
       
        static AutoResetEvent readNow = new AutoResetEvent(false);

        public bool sendMsg(string PhoneNo, string Message)
        {
            bool isSend = false;
            _numActual = PhoneNo;
            _estado = EnumEstado.ENVIANDO_SMS;
            try
            {
                //tiro el AT dos veces por las dudas
                try
                {
                    string rData = ExecCommand(_comm, "AT", 300, "No phone connected");

                }
                catch (Exception ex)
                {

                    Console.WriteLine("PRIMER AT FALLA" + ex.ToString());
                    Thread.Sleep(3000);
                }
                //

                string recievedData = ExecCommand(_comm, "AT", 300, "No phone connected");
                recievedData = ExecCommand(_comm, "AT+CMGF=1", 300, "Failed to set message format.");
                String command = "AT+CMGS=\"" + PhoneNo + "\"";
                recievedData = ExecCommand(_comm, command, 300, "Failed to accept phoneNo");
                command = Message + char.ConvertFromUtf32(26) + "\r";
                recievedData = ExecCommand(_comm, command, 30000, "Failed to send message"); //5 seconds
                if (recievedData.EndsWith("\r\nOK\r\n"))
                {
                    isSend = true;
                    Console.WriteLine("ENVIO EXITOSO MODEM: " + _imei.ToString());
                }
                else if (recievedData.Contains("ERROR"))
                {
                    _causa = "Sent denegated";
                    isSend = false;
                    //Console.WriteLine("ENVIO FALLIDO MODEM: " + _imei.ToString());
                }
                _estado = EnumEstado.DISPONIBLE;
                return isSend;

            }
            catch (Exception ex)
            {
                //_logger.Error("sendMSG", ex);
                _causa = ex.Message;
                if (ex.Message == "No phone connected")
                {
                    _estado = EnumEstado.DESCONECTADO;
                    Console.WriteLine("ENVIO FALLIDO DESCONECTADO MODEM: " + _imei.ToString());
                }
                else
                {
                    _estado = EnumEstado.DISPONIBLE;
                    Console.WriteLine("ENVIO FALLIDO ERROR MODEM: " + _imei.ToString());
                    //_ctrlPausas.IncrementarEnviosFallidos();

                }
                //EstablecerPausaEnvio();
                return false;
                
            }
            finally             
            {
                if (isSend)
                {
                    _ctrlPausas.IncrementarEnviosExitosos();
                   // _finPausaEnvio = DateTime.Now.AddSeconds(_ctrlPausas.GetIntervaloEnvio());

                }
                else
                {
                    _ctrlPausas.IncrementarEnviosFallidos();
                    EstablecerPausaEnvio();
                }
                
            
            }

        }

        private void EstablecerPausaEnvio()
        {
            if (_estado != EnumEstado.DESCONECTADO )
            {
                _estado = EnumEstado.PAUSA_FALLIDOS;
               // _finPausaEnvio =  DateTime.Now.AddSeconds(_ctrlPausas.CalcularPausaEnvio());            
            }
        }
       
        static void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (e.EventType == SerialData.Chars)
                    readNow.Set();
            }
            catch (Exception ex)
            {
                //_logger.Error("DataReceived", ex);
                throw ex;
            }
        }
        #endregion

        #region Delete SMS
        public bool DeleteMsg(string pIndex)
        {
            bool isDeleted = false;
            try
            {

                #region Execute Command
                string recievedData = ExecCommand(_comm ,"AT", 300, "No phone connected");
                recievedData = ExecCommand(_comm, "AT+CMGF=1", 300, "Failed to set message format.");
                String command = "AT+CMGD=" + pIndex;
                recievedData = ExecCommand(_comm,command, 5000, "Failed to delete message");
                #endregion

                if (recievedData.EndsWith("\r\nOK\r\n"))
                {
                    isDeleted = true;
                }
                if (recievedData.Contains("ERROR"))
                {
                    isDeleted = false;
                }
                return isDeleted;
            }
            catch (Exception ex)
            {
                //_logger.Error("DeleteMsg", ex);
                //throw new Exception(ex.Message);
                _causa = ex.ToString();
                Console.WriteLine("DeleteMsg" + ex.ToString());
                return false;
            }
            
        }  
        #endregion

        #region Read IMEI
        public String ReadIMEI()
        {            
            try
            {
                
                #region Execute Command
                string recievedData = ExecCommand(_comm , "AT", 300, "No phone connected");
                recievedData = ExecCommand(_comm, "AT+CMGF=1", 300, "Failed to set message format.");
                recievedData = ExecCommand(_comm, "AT+CGSN", 300, "Failed to read IMEI.");
                #endregion

                if (recievedData.EndsWith("\r\nOK\r\n"))
                {
                    String Aux = recievedData;
                    Aux = Aux.Replace("AT+CGSN", "");
                    Aux = Aux.Replace("\r\nOK\r\n", "");
                    Aux = Aux.Replace("\r", "");
                    Aux = Aux.Replace("\n", "");

                    _imei = Aux;
                }
                if (recievedData.Contains("ERROR"))
                {
                    _imei = "";
                }
                return _imei;
            }
            catch (Exception ex)
            {
                //_logger.Error("ReadIMEI", ex);
                _causa = ex.Message;
                //Console.WriteLine("ReadIMEI: " + ex.ToString());
                return "";

            }
        }

        #endregion

        #region Read SIMID
        public String ReadSIMID()
        {
            try
            {

                #region Execute Command
                string recievedData = ExecCommand(_comm, "AT", 300, "No phone connected");
                recievedData = ExecCommand(_comm, "AT+CMGF=1", 300, "Failed to set message format.");
                recievedData = ExecCommand(_comm, "AT+CIMI", 300, "Failed to read SIMID.");
                #endregion

                if (recievedData.EndsWith("\r\nOK\r\n"))
                {
                    String Aux = recievedData;
                    Aux = Aux.Replace("AT+CIMI", "");
                    Aux = Aux.Replace("\r\nOK\r\n", "");
                    Aux = Aux.Replace("\r", "");
                    Aux = Aux.Replace("\n", "");

                    _simid  = Aux;
                }
                if (recievedData.Contains("ERROR"))
                {
                    _simid = "";
                }
                return _simid ;
            }
            catch (Exception ex)
            {
                //_logger.Error("ReadSIMID", ex);
                _causa = ex.Message;
               // Console.WriteLine("ReadSIMID " + ex.Message);
                return "";
            }
        }

        #endregion

        #region Read Signal
        public String ReadSignal()
        {
            try
            {

                #region Execute Command
                string recievedData = ExecCommand(_comm, "AT", 300, "No phone connected");
                recievedData = ExecCommand(_comm, "AT+CMGF=1", 300, "Failed to set message format.");
                recievedData = ExecCommand(_comm, "AT+CSQ", 300, "Failed to read Signal.");
                #endregion

                if (recievedData.EndsWith("\r\nOK\r\n"))
                {
                    String Aux = recievedData;
                    Aux = Aux.Replace("AT+CSQ", "");
                    Aux = Aux.Replace("+CSQ:", "");
                    Aux = Aux.Replace("\r\nOK\r\n", "");
                    Aux = Aux.Replace("\r", "");
                    Aux = Aux.Replace("\n", "");


                    _signal = Aux;
                }
                if (recievedData.Contains("ERROR"))
                {
                    _signal = "";
                }
                return _signal;
            }
            catch (Exception ex)
            {
                //_logger.Error("ReadSignal", ex);
                _causa = ex.Message;
                //Console.WriteLine("ReadSignal: " + ex.ToString());
                return "";

            }
        }

        #endregion

    }
}
