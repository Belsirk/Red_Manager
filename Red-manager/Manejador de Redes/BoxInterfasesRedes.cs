using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;


namespace Manejador_de_Redes
{
    public class BoxInterfasesRedes
    {
        NetworkInterface niInterfaz = null;



        /// <summary>
        /// Estas variables de error, deben ser identicas a las de Form-Principal
        /// mismo nombre, mismo valro
        /// </summary>
        const int Error_Tipo_Exito = 1;
        const int Error_Tipo_IPv4 = -1;
        const int Error_Tipo_Mask = -2;
        const int Error_Tipo_gtwv4 = -3;
        const int Error_Tipo_dnsv4 = -4;
        const int Error_Tipo_netsh = -5;
        const int Error_Tipo_dhcpv4 = -6;
        const int Error_Tipo_IPv6 = -7;
        const int Error_Tipogtwv6 = -8;
        const int Error_Tipo_dnsv6 = -9;
        const int Error_Tipo_Permisos = -20;
        const int Error_Tipo_Restart = -10;
        const int Error_Tipo_ComandoGenerico = -30;
        const int Error_Tipo_LinkAddress = -11;
        const String Log_RegistroIP = "RegistroIPs.txt";

        const bool Dirrecion_IPv4 = true;
        const bool Dirrecion_IPv6 = false;

        #region Constructor_Destructor

        public BoxInterfasesRedes()
        {

        }

        /// <summary>
        /// Constructor para manipular la interfaz que se desea
        /// </summary>
        /// <param name="niInt"></param>
        public BoxInterfasesRedes(NetworkInterface niInt)
        {
            niInterfaz = niInt;

        }


        ~BoxInterfasesRedes()
        {

            niInterfaz = null;

        }
        #endregion

        #region Metodos

        /// <summary>
        /// Executes a shell command synchronously.
        /// Fuente: http://www.codeproject.com/KB/cs/Execute_Command_in_CSharp.aspx
        /// </summary>
        /// <param name="command">string command</param>
        /// <returns>string, as output of the command.</returns>
        private int ExecuteCommandNetsh(object command)
        {
            try
            {

                ///UAC!... siempre no... proxima version tal veZ( el programa debe arrancar con 
                ///token de administrador

                // create the ProcessStartInfo using "cmd" as the program to be run,
                // and "/c " as the parameters.
                // Incidentally, /c tells cmd that we want it to execute the command that follows,
                // and then exit.
                System.Diagnostics.ProcessStartInfo procStartInfo =
                    new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);

                // The following commands are needed to redirect the standard output.
                // This means that it will be redirected to the Process.StandardOutput StreamReader.
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                // Do not create the black window.
                procStartInfo.CreateNoWindow = true;
                // Now we create a process, assign its ProcessStartInfo and start it
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();


                // Get the output into a string
                string result = proc.StandardOutput.ReadToEnd();

                //PAra facilitar debugeo, reigstramos la linea que introducimos a los comandos (Antes de la respuestA)
                LogError(command.ToString());

                // Display the command output.
                //Console.WriteLine(result);
                LogError(result); //Si la linea queda en blanco no es error , caso contrario it's !

                //Hay un error, en particular  que nos interesa y es facil que ocurra
                //Dejo la captura original en un Windows7 en Ingles y dos palabras claves por si cambia el S.O.
                if (result.Equals("The requested operation requires elevation (Run as administrator).") ||
                    result.Contains("administrator") || result.Contains("administrador"))
                {
                    return Error_Tipo_Permisos;
                }
                else if (result.Contains("mask")) //Invalid mask parameter
                    return Error_Tipo_Mask;
                else if (result.Contains("DHCP"))
                    return Error_Tipo_dhcpv4;
                else if (result.Contains("Restart"))
                    return Error_Tipo_Restart;
                else if  (result.Contains("Ok.\r\n\r\n") ) //Esta es una respuesta particular para DHCPv6
                    return Error_Tipo_Exito;                //En general, indica exito para los pasos previos al servicio
                else if (result.Equals("") || result.Equals("\r\n")) //Un mensa  je en blanco significa que logro ejecutarse adecuadamente
                    return Error_Tipo_Exito;                        //Si se habilita DHCP cuando ya esta habilitado tambien surge ese mensaje
                //DHCP is already enabled on this interface.
            }
            catch (Exception objException)
            {
                // Log the exception
                LogError(objException.Source);
                return Error_Tipo_netsh;
            }

            //En teoria no deberia de llegar hasta aqui, puede tratarse de una falsa alarma puede que no
            // en el registro se puede apreciar

            return Error_Tipo_netsh;
            // return Error_Tipo_Exito;   //Por ahora considerare uqe lo que llegue hasta aqui son falsos negativos
        }

        /// <summary>
        /// Este metodo es parecido al ExcecuteCommandSync excepto
        /// en que ejecuta solamente comandos para IPconfig y por lo tanto 
        /// omite totalmente el analisis de error. (Ademas de no requerir escalada de privilegios)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private int ExecuteCommand(object command)
        {
            try
            {

                // create the ProcessStartInfo using "cmd" as the program to be run,
                // and "/c " as the parameters.
                // Incidentally, /c tells cmd that we want it to execute the command that follows,
                // and then exit.
                System.Diagnostics.ProcessStartInfo procStartInfo =
                    new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);

                // The following commands are needed to redirect the standard output.
                // This means that it will be redirected to the Process.StandardOutput StreamReader.
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                // Do not create the black window.
                procStartInfo.CreateNoWindow = true;
                // Now we create a process, assign its ProcessStartInfo and start it
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();

                //PAra facilitar debugeo, reigstramos la linea que introducimos a los comandos (Antes de la respuestA)
                LogError(command.ToString());

                // Get the output into a string
                string result = proc.StandardOutput.ReadToEnd();
                // Display the command output.
                //Console.WriteLine(result);
                LogError(result); //Si la linea queda en blanco no es error , caso contrario it's !

                //Como no nos interesa el resultado simplemente damos luz verde
                return Error_Tipo_Exito;                        
                
            }
            catch (Exception objException)
            {
                // Log the exception
                LogError(objException.Source);
                return Error_Tipo_ComandoGenerico;
            }

            //En teoria no deberia de llegar hasta aqui, puede tratarse de una falsa alarma puede que no
            // en el registro se puede apreciar
     
        }

        /// <summary>
        /// El objetivo, es guardar el error que ocurrio en un lugar adecuado
        /// </summary>
        /// <param name="sError"></param>
        private void LogError(String sError)
        {
            // Para tener mayor compresion del log, le a.
            sError = DateTime.Now.ToString() + ": " + sError;

            try
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(Log_RegistroIP, true);
                file.WriteLine(sError);

                file.Close();
            }
            catch (Exception e)
            {
                //Donde avisar que hay un tremendo error a la hora de avisar del mismo ??
                System.Windows.Forms.MessageBox.Show("Notificar al instructor \n No se ha podido acceder al archivo de registro. \n" + e.Source);
            }

        }

        /// <summary>
        /// Regresa el estado de la interfaz, si esta levantado o caido
        /// NOTA: Cualquier lectura o escritura de un dato IP es preferible sol ohacerlo 
        /// cuando la interfaz esta levantada.
        /// http://msdn.microsoft.com/en-us/library/system.net.networkinformation.operationalstatus.aspx
        /// Existen 7 estados de operacion, nosotros nos enfocaremos solo en "UP" que indica que 
        /// esta en operacion. (Liga da mas informacion)
        /// </summary>
        /// <returns></returns>
        public Boolean getIntStatus()
        {

            if (niInterfaz.OperationalStatus == OperationalStatus.Up)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Este metodo solo valida que la dirreción en formato string sea correcta 
        /// El sistema le es indestitnto si se tratade una IPv4 o IPv6.
        /// </summary>
        /// <param name="sIPaddress"></param>
        /// <param name="bVersionIP"> IPv4 = True, IPv6=false</param>
        /// <returns></returns>
        public Boolean GetIsValidIPAddress(string sIPaddress, bool bVersionIP)
        {
            IPAddress ipAddress;

            try { ipAddress = IPAddress.Parse(sIPaddress); }
            catch (ArgumentNullException e) { LogError("Sintaxis IPv4" + e.Source); return false; }
            catch (FormatException e) { LogError("Sintaxis IPv4" + e.Source); return false; }
            catch (Exception e) { LogError("Sintaxis IPv4" + e.Source); return false; }

            if ((ipAddress.AddressFamily == AddressFamily.InterNetwork) && (bVersionIP))
                return true;
            else if ((ipAddress.AddressFamily == AddressFamily.InterNetworkV6) && (!bVersionIP))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Verifica si  los datos tomados de los servcidores DNS fueron adqueridos con DHCP  
        /// o fueron colocados manualmente.
        /// </summary>
        /// <returns></returns>
        public Boolean IsDynamicDnsEnabled() { return niInterfaz.GetIPProperties().IsDynamicDnsEnabled; }

        /// <summary>
        /// Este es el override generico ToString, el objetivo es que nos regrese 
        /// algo que tenga sentido para el que lea.
        /// </summary>
        /// <returns> Nombre de la interfaz </returns>
        public override String ToString()
        {

            //return niInterfaz.Name;
            return niInterfaz.Description;
        }

        /// <summary>
        /// Esta funcion se encarga de verificar si la dirrecion que recibio concuerda con el tipo de dirrecion
        /// que deseamos
        /// </summary>
        /// <param name="IPAdd"></param>
        /// <param name="VerionIP"> TRUE validara IPv4 y FALSE IPv6</param>
        /// <returns></returns>
        public bool ConfirmarTipoIP(IPAddress IPAdd, bool VerionIP)
        {
            if (VerionIP && IPAdd.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) //ES IPv4?
                return true;
            else if (!VerionIP && IPAdd.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6) //Es IPv6?
                return true;
            else
                return false; // Pues no lo es 
        }

        /// <summary>
        /// Detecta si actualmente esta un servidor DHCP proporcionado la dirrecion IP.
        /// Debido a que IPv6 tiene multiples dirreciones, revisamoos cada una de ellas. 
        /// </summary>
        /// <param name="bIPv4"> True Significa que buscara un DHCP para L3 IPv4 
        ///                     False significa que buscara un DHCP para L3 IPv6</param>
        /// <returns></returns>
        private Boolean getisDHCPenabled(bool bIPv4)
        {
            //No podemos confiarnos simplemente en la cantidad de dirreciones de servidores DHCP
            //ya que por lo menos recuerda el utlimo servidor (aunque se este en configuracion estatica)
            //Por lo tanto debemos estar seguro si esta o no esta con DHCP
            //http://msdn.microsoft.com/en-us/library/system.net.networkinformation.suffixorigin.aspx
            //Nos enfocaremos en el sufijo de las dirreciones para detectarlo.
            bool bValor = false;
            if (niInterfaz.GetIPProperties().UnicastAddresses.Count < 1)
                return false;

            foreach (UnicastIPAddressInformation Unis in niInterfaz.GetIPProperties().UnicastAddresses)
            {
                if (ConfirmarTipoIP(Unis.Address, bIPv4) && (Unis.SuffixOrigin == SuffixOrigin.OriginDhcp))
                    bValor =  true;
            }

            return bValor;
        }

        #region IPv4

        /// <summary>
        /// Indica si IPv6 esta habilitado.
        /// </summary>
        /// <returns></returns>
        public bool getisIPv4Enable()
        {
            return niInterfaz.Supports(NetworkInterfaceComponent.IPv4);
        }

        /// <summary>
        /// Indica si la dirrecion IPv4 de la interfaz fue adquerida por medio de 
        /// DHCP.
        /// </summary>
        /// <returns> True Si fue por DHCP, otro caso FALSE. </returns>
        public Boolean getisDHCPv4Enabled()
        {
            return getisDHCPenabled(Dirrecion_IPv4);
        }

        /// <summary>
        /// OBSOLETO - Curiosamente el sistema resguarda el utlimo servidor DHCP del 
        /// que tomo los datos, sin embargo no necesariamente es valido. 
        /// Es preferible asumir que si el usuario n
        /// </summary>
        /// <param name="bIPv4"> True Significa que buscara un DHCP para L3 IPv4 
        ///                     False significa que buscara un DHCP para L3 IPv6</param>
        /// <returns></returns>
        /*public Boolean getisDHCPenabled(bool bIPv4)
        {

            //Primero debemos averiguar si la uinterfaz tiene un DHCP asignado
            if (niInterfaz.GetIPProperties().DhcpServerAddresses.Count > 0)
            {
                //Ahora, como puede tener multiples dirreciones de capa 3, debemso ver si tiene del protocolo que queremos
                for (int iC = 0; iC < niInterfaz.GetIPProperties().DhcpServerAddresses.Count; iC++)
                    if ((niInterfaz.GetIPProperties().DhcpServerAddresses[0].AddressFamily == AddressFamily.InterNetwork) && (bIPv4))
                        return true;
                    else if ((niInterfaz.GetIPProperties().DhcpServerAddresses[0].AddressFamily == AddressFamily.InterNetworkV6) && (!bIPv4))
                        return true;
            }
            return false;
        }*/





        /// <summary>
        /// Activa DHCP automatico para la interfaz y el protocolo IP (4 ó 6)
        /// </summary>
        /// <param name="bIPv4"> Si es true , activara DHCP para IPv4, caso contrario sera para IPv6</param>
        /// <returns></returns>
        public int setNewDHCPClient(Boolean bIPv4)
        {

            //Podriamos utilizar directamente el servidor DHCP del Tec,el cual es 10.16.3.3
            //pero sería un peligro  si lo cambian ya que la aplicacion tardaría bastante en actualizarse
            //por lo mismo se queda en automatico
            int iCheck;
            String sCom = "netsh ";
            if (bIPv4)
            {
                sCom += "interface ipv4 set address name=" + "\"" + niInterfaz.Name + "\"" + " source=dhcp";
                LogError(sCom);
                iCheck = ExecuteCommandNetsh(sCom);
            }
            else
                iCheck = setIPv6_DHCPClient();
            

            //En este punto, ganar algo de tiempo para uqe el servidor DHCP pueda enviar respeusta
            //seria una buena idea

            return iCheck;
        }

        /// <summary>
        /// Regresa la primera IPv4 encontra que tiene la NIC en String
        /// NOTA: En caso que regrese 0.0.0.0 significa uqe ningun protocolo de capa red 
        /// es IPv4.
        /// </summary>
        /// <returns></returns>
        public String getIpv4Address()
        {

            IPInterfaceProperties Propiedades = niInterfaz.GetIPProperties();
            if (!getIntStatus())
                return "";
            else if (Propiedades.UnicastAddresses.Count == 0)
                return "";
            else
            {
                for (int ic = 0; ic < Propiedades.UnicastAddresses.Count; ic++)
                    if (Propiedades.UnicastAddresses[ic].Address.AddressFamily == AddressFamily.InterNetwork)
                        return Propiedades.UnicastAddresses[ic].Address.ToString();
            }

            return "0.0.0.0";
        }

        /// <summary>
        /// Regrsa la submascara de red que utiliza la NIC en IPv4 
        /// Obviamente, no existe un equivalente para IPv6
        /// </summary>
        /// <returns></returns>
        public String getNetmask()
        {

            IPInterfaceProperties Propiedades = niInterfaz.GetIPProperties();

            //La información solo es valida si la interfaz esta activa, de lo contrario
            //peude tener informacion vieja (incongruente) o valores "null"
            if (!getIntStatus())
                return "";
            else if (Propiedades.UnicastAddresses.Count == 0)
                return "";
            else
            {
                for (int ic = 0; ic < Propiedades.UnicastAddresses.Count; ic++)
                    if (Propiedades.UnicastAddresses[ic].Address.AddressFamily == AddressFamily.InterNetwork)
                        return Propiedades.UnicastAddresses[ic].IPv4Mask.ToString();

            }

            return "0.0.0.0";

        }

        /// <summary>
        /// Regresa el primer Gateway que halle  en IPv4
        /// </summary>
        /// <returns> Puede regresar 0.0.0.0 si no hallo ninguno o bien dejarlo en blanco si la interfaz
        /// esta apagada.
        /// </returns>
        public String getFirstIpv4Gateway()
        {

            IPInterfaceProperties Propiedades = niInterfaz.GetIPProperties();
            if (!getIntStatus())
                return "";
            else if (Propiedades.GatewayAddresses.Count == 0)
                return "";
            else
            {
                for (int ic = 0; ic < Propiedades.GatewayAddresses.Count; ic++)
                    if (Propiedades.GatewayAddresses[ic].Address.AddressFamily == AddressFamily.InterNetwork)
                        return Propiedades.GatewayAddresses[ic].Address.ToString();
            }
            return "0.0.0.0";
        }

        /// <summary>
        /// Utilizando el comando netsh se instala la nueva dirrecion IPv4 
        /// Esta configuracion solo incluye lo mas basico (una sola ip , su mascara y un solo gateway)
        /// </summary>
        /// <param name="IPv4"></param>
        /// <param name="Mask"></param>
        /// <param name="Gateway"></param>
        /// <returns></returns>
        public int setIPv4FullAddress(String IPv4, String Mask, String Gateway)
        {

            IPAddress ipIpv4, ipMask, ipGtwy = null;
            //Primero debemos validar una IPv4 Correcta
            try { ipIpv4 = IPAddress.Parse(IPv4); }
            catch (ArgumentNullException e) { LogError("IPv4" + e.Source); return Error_Tipo_IPv4; }
            catch (FormatException e) { LogError("IPv4" + e.Source); return Error_Tipo_IPv4; }
            catch (Exception e) { LogError("IPv4" + e.Source); return Error_Tipo_IPv4; }

            //La submascara debe ser respaldada
            try { ipMask = IPAddress.Parse(Mask); }
            catch (ArgumentNullException e) { LogError("Mask" + e.Source); return Error_Tipo_Mask; }
            catch (FormatException e) { LogError("Mask" + e.Source); return Error_Tipo_Mask; }
            catch (Exception e) { LogError("Mask" + e.Source); return Error_Tipo_Mask; }

            //La gateway tambien debe ser guarda adecuadamente 
            //Nota: Existe la posibilidad, que no se requiera gateway 
            if (!Gateway.Equals(""))
                try { ipGtwy = IPAddress.Parse(Gateway); }
                catch (ArgumentNullException e) { LogError("Gatewayv4" + e.Source); return Error_Tipo_gtwv4; }
                catch (FormatException e) { LogError("Gatewayv4" + e.Source); return Error_Tipo_gtwv4; }
                catch (Exception e) { LogError("Gatewayv4" + e.Source); return Error_Tipo_gtwv4; }

            ///Con todo bien... trabajamos ...pero hay 3 modos
            ///1 - Directamente a los registros de Windwos
            ///2 - Usando WMI y Manejador de Objetos (que viene trabajando sobre los registros)
            ///3 - Usando los comandos del comand-prompt (DOS)
            ///Los primeros dos exige saber bvien los valores a asignar a cada parametros el tercero
            ///nos permite utilizar el comando netsh que puede efectivamente ejecutar adecuadamente
            ///los cambiso de DHCP a ip fija y viceversa
            String sCom = "netsh ";
            sCom += "interface ipv4 set address name=" + "\"" + niInterfaz.Name + "\"" + " source=static";
            sCom += " address=" + ipIpv4.ToString() + " mask=" + ipMask.ToString();
            if (!Gateway.Equals(""))
                sCom += " gateway=" + ipGtwy.ToString();
            else
                sCom += " gateway=none";
            sCom += " store=active";

            //Con el comando listo a ejecutarse  lo guardamos en el log y lo ejecutamos
            LogError(sCom);
            return ExecuteCommandNetsh(sCom);
        }


        /// <summary>
        /// Regresa la coleccion de servidores 
        /// Observacion: Existe la posibilidad de que regrese NULL si la interfaz
        /// esta caida (o muerta), ademas de una lista vacia.
        /// </summary>
        /// <returns></returns>
        public IPAddressCollection getDNSv4Address()
        {
            //Si la interfaz esta down no se hace nada
            if (!getIntStatus())
                return null; //Una lista vacia por seguridad 
            IPInterfaceProperties Propiedades = niInterfaz.GetIPProperties();
            //Puede estar vacia, pero es informacion fideligna
            for (int ic = 0; ic < Propiedades.UnicastAddresses.Count; ic++)
                if (Propiedades.UnicastAddresses[ic].Address.AddressFamily == AddressFamily.InterNetwork)
                    return niInterfaz.GetIPProperties().DnsAddresses;
            //Si salio del for, significa que no tiene un protocolo IPv4 configurado, asi que tenemos que enviar un 
            //elemento vacio
            return null;
        }
        /// <summary>
        /// Regresa todo los servidores WINS
        /// NOTA: Al parecer Microsoft le da en la torre a los WINs en IPv6 ya que en el protocolo no aparece
        /// opcion alguna respecto a ellos.
        /// </summary>
        /// <returns></returns>
        public IPAddressCollection getWINv4Address()
        {

            //Si la interfaz esta down no se hace nada
            if (!getIntStatus())
                return null; //Una lista vacia por seguridad 

            IPInterfaceProperties Propiedades = niInterfaz.GetIPProperties();
            //Puede estar vacia, pero es informacion fideligna
            for (int ic = 0; ic < Propiedades.UnicastAddresses.Count; ic++)
                if (Propiedades.UnicastAddresses[ic].Address.AddressFamily == AddressFamily.InterNetwork)
                    return niInterfaz.GetIPProperties().WinsServersAddresses;

            return null;
        }


        /// <summary>
        /// Configura los servicios de DNS para IPv4 de forma estatica
        /// </summary>
        /// <param name="sIPAddresses"> Arreglo de strings que debe contener todo los servidores DNS estaticos</param>
        /// <returns></returns>
        public int setDNSv4Address(String[] sIPAddresses)
        {
            //Se supone de antemano toda las IP address estan validadas pero 
            //nos aseugraremos de ello 
            string sShell;
            int iC = 1;
            int iCheck;
            foreach (String Address in sIPAddresses)
            {
                //Solamente cuando la IP address es correcta 
                if (GetIsValidIPAddress(Address, true))
                {

                    //Antes de todo esto, DNS  no borra nada cuadno se anexan, asi que si queremos que este "limpio"
                    // hay que borrar lo que tenia previamente
                    /*sShell = "netsh interface ipv4 delete dnsserver name=\"" + niInterfaz.Name + "\" all";
                    LogError(sShell);
                    iCheck = ExecuteCommandNetsh(sShell);*/

                    if (iC == 1)
                    {
                        sShell = "netsh interface ipv4 set dnsserver name=\"" + niInterfaz.Name +
                             "\" source=static address=" + Address + " primary";

                    }
                    else
                    {
                        sShell = "netsh interface ipv4 add dnsserver name=\"" + niInterfaz.Name +
                              "\" address=" + Address + " index=" + iC.ToString();
                    }
                    iC++;
                    LogError(sShell);
                    iCheck = ExecuteCommandNetsh(sShell);
                    if (iCheck != Error_Tipo_Exito) //
                        return iCheck;
                }
                else return Error_Tipo_dnsv4;

            }

            return Error_Tipo_Exito;
        }

        /// <summary>
        /// Configura los servicios de DNS (Para IPv4) de forma dinamica
        /// </summary>
        /// <returns></returns>
        public int setDNSv4Address()
        {
            String sShell;

            sShell = "netsh interface ipv4 set dnsserver name=\"" + niInterfaz.Name + "\" source=dhcp register=both validate=yes";
            LogError(sShell);
            return ExecuteCommandNetsh(sShell);
        }

        /// <summary>
        /// Instala un listado de Gateways (Todos tendran la ruta 0.0.0.0/0 
        /// como defecto )
        /// </summary>
        /// <param name="sIPAddresses"></param>
        /// <returns></returns>
        public int setGTWv4Address(String[] sIPAddresses)
        {
            //Se supone de antemano toda las IP address estan validadas pero 
            //nos aseugraremos de ello 
            string sShell;
            int iC = 1;
            int iCheck;
            foreach (String Address in sIPAddresses)
            {
                //Solamente cuando la IP address es correcta 
                if (GetIsValidIPAddress(Address, true))
                {


                    if (iC == 1)
                    {
                        sShell = "netsh interface ipv4 set address name=\"" + niInterfaz.Name +
                             "\" gateway=" + Address + " gwmetric=0" + " store=persistent";

                    }
                    else
                    {
                        sShell = "netsh interface ipv4 add address name=\"" + niInterfaz.Name +
                              "\" gateway=" + Address + " gwmetric=" + (iC - 1).ToString() + " store=persistent";
                    }
                    iC++;
                    LogError(sShell);
                    iCheck = ExecuteCommandNetsh(sShell);
                    if (iCheck != Error_Tipo_Exito) //
                        return iCheck;
                }
                else return Error_Tipo_dnsv4;

            }

            return Error_Tipo_Exito;
        }

        /// <summary>
        /// Configura de forma estatica los servicios de WINS (solo IPv4)
        /// </summary>
        /// <param name="sIPAddresses"></param>
        /// <returns></returns>
        public int setWINSv4Address(String[] sIPAddresses)
        {
            //Se supone de antemano toda las IP address estan validadas pero 
            //nos aseugraremos de ello 
            string sShell;
            int iC = 1;
            int iCheck;
            foreach (String Address in sIPAddresses)
            {
                //Solamente cuando la IP address es correcta 
                if (GetIsValidIPAddress(Address, true))
                {

                    sShell = "netsh interface ipv4 set winsserver name=\"" + niInterfaz.Name +
                              "\" static address=" + Address + " index=" + iC++.ToString();

                    LogError(sShell);
                    iCheck = ExecuteCommandNetsh(sShell);
                    if (iCheck != Error_Tipo_Exito) //
                        return iCheck;
                }
                else return Error_Tipo_dnsv4;

            }

            return Error_Tipo_Exito;
        }

        /// <summary>
        /// Los gateway se manejan en su propio conjunto por lo tanto 
        /// los regresamos tal por cual (Haciendo un filtro para solo
        /// regresar el protocolo  de red debido).
        /// El Mayor incoveniente es que esta clase en particular no permite mas que lectura
        /// por lo mismo regresamos un listado de strings.
        /// </summary>
        /// <returns></returns>
        public List<String> getAllGatewaysv4()
        {
            List<String> Gateways = new List<String>();

            //Si la interfaz esta down no se hace nada
            if (!getIntStatus())
                return null; //Una lista vacia por seguridad 

            //Puede estar vacia, pero es informacion fideligna
            foreach (GatewayIPAddressInformation Gtw in niInterfaz.GetIPProperties().GatewayAddresses){
                if (Gtw.Address.AddressFamily == AddressFamily.InterNetwork)
                    Gateways.Add(Gtw.Address.ToString());
            }
            return Gateways;
        }

        #endregion


        #region IPv6

        /// <summary>
        /// Indica si IPv6 esta habilitado.
        /// </summary>
        /// <returns></returns>
        public bool getisIPv6Enable()
        {
            return niInterfaz.Supports(NetworkInterfaceComponent.IPv6);
        }

        /// <summary>
        /// Verifica si una dirrecion IPv6 es de cierto tipo basado en su sufijo.
        /// </summary>
        /// <param name="IPv6Address">La dirrecion IPv6 que se va a validar.</param>
        /// <param name="Tipo">El tipo de dirrecion que se espera es. </param>
        /// <returns></returns>
        public bool getOrigingIPv6Suffix(string IPv6Address, SuffixOrigin Tipo)
        {
            foreach (UnicastIPAddressInformation Dirres in niInterfaz.GetIPProperties().UnicastAddresses)
            {
                //ya que el LB que obtiene las dirreciones es una copia integra, sabemos que este
                //metodo es seguro, opcionalmente podriamos buscar un substring en vez de uno 100%
                //identico.
                if (  (IPv6Address.Equals( Dirres.Address.ToString() )) &&  ( Dirres.SuffixOrigin == Tipo) )
                   return true;
            }

            return false;
        }

        /// <summary>
        /// Indica si al menos una dirrecion IPv6 de la interfaz fue adquerida por medio de 
        /// DHCP.
        /// </summary>
        /// <returns> True Si fue por DHCP, otro caso FALSE. </returns>
        public Boolean getisDHCPv6Enabled()
        {
            return getisDHCPenabled(Dirrecion_IPv6);
        }

        /// <summary>
        /// Regresa en formato String la PRIMERA IPv6 address (sin prefijo)
        /// En caso que no este en funcionamiento la interfaz regresa espacio vacio 
        /// y en caso que no tenga el protocolo IPv6 regresa ::
        /// </summary>
        /// <returns></returns>
        public String getIpv6Address()
        {
            String sAux;
            int iAux = -1;

            IPInterfaceProperties Propiedades = niInterfaz.GetIPProperties();
            if (!getIntStatus())
                return "";
            else if (Propiedades.UnicastAddresses.Count == 0)
                return "";
            else
            {

                for (int ic = 0; ic < Propiedades.UnicastAddresses.Count; ic++)
                    if (Propiedades.UnicastAddresses[ic].Address.AddressFamily == AddressFamily.InterNetworkV6)
                    {
                        //El formato que nos da es el siguiente: IPv6Address%ScopeID  , queremos separarlos
                        sAux = Propiedades.UnicastAddresses[0].Address.ToString();
                        iAux = sAux.IndexOf("%");
                        if (iAux == (-1))
                            return sAux;
                        else
                            return sAux.Substring(0, iAux);
                    }
            }
            return "::";
        }

        /// <summary>
        /// Regresa toda las dirreciones IPv6 que la interfaz posee.
        /// NOTA: Cada dirrecion posee el formato Dirrecion%Prefijo
        /// </summary>
        /// <returns></returns>
        public List<String> getAllIpv6Address()
        {
            String sAux;
            List<String> lAux = new List<string>();
            lAux.Clear();
            

            IPInterfaceProperties Propiedades = niInterfaz.GetIPProperties();
            if (!getIntStatus())
                return lAux;
            else if (Propiedades.UnicastAddresses.Count == 0)
                return lAux;
            else
            {

                for (int ic = 0; ic < Propiedades.UnicastAddresses.Count; ic++)
                    if (Propiedades.UnicastAddresses[ic].Address.AddressFamily == AddressFamily.InterNetworkV6)
                    {
                        //El formato que nos da es el siguiente: IPv6Address%ScopeID  TAL POR CUAL SE VA
                        sAux = Propiedades.UnicastAddresses[ic].Address.ToString();
                        lAux.Add(sAux);
                    }
                return lAux;
            }
            //Este caso solo pasara si no existe una sola dirre IPv6 
            /*sAux = "::";
            lAux.Add(sAux);
            return lAux;*/
        }

        /// <summary>
        /// Regresa el Scope de la dirrecion IPv6
        /// (utilizado en una IPv6 global ) 
        /// </summary>
        /// <returns></returns>
        public String getIpv6ScopeID()
        {

            IPInterfaceProperties Propiedades = niInterfaz.GetIPProperties();
            if (!getIntStatus())
                return "";
            else if (Propiedades.UnicastAddresses.Count == 0)
                return "";
            else
            {
                for (int ic = 0; ic < Propiedades.UnicastAddresses.Count; ic++)
                    if (Propiedades.UnicastAddresses[ic].Address.AddressFamily == AddressFamily.InterNetworkV6)
                        return Propiedades.UnicastAddresses[ic].Address.ScopeId.ToString();
            }

            return "";
        }

        /// <summary>
        /// Regresa la primera gateway en IPv6 
        /// </summary>
        /// <returns></returns>
        public String getFirstIpv6Gateway()
        {

            IPInterfaceProperties Propiedades = niInterfaz.GetIPProperties();
            if (!getIntStatus())
                return "";
            else if (Propiedades.GatewayAddresses.Count == 0)
                return "";
            else
            {
                for (int ic = 0; ic < Propiedades.GatewayAddresses.Count; ic++)
                    if (Propiedades.GatewayAddresses[ic].Address.AddressFamily == AddressFamily.InterNetworkV6)
                        return Propiedades.GatewayAddresses[ic].Address.ToString();
            }
            return "0::0";
        }

        /// <summary>
        /// A diferencia de IPv4, netsh necesita de dos comandos distintos para configurar un gateway en IPv6 y 
        /// un host en IPv6.
        /// Este metodo es para el segundo caso.
        /// </summary>
        /// <param name="IPv6">Debe ser en el formato Address%Scope o bien sin el scope</param>
        /// <returns></returns>
        public int setIPv6Address(String IPv6)
        {

            IPAddress ipIpv6;
            //Primero debemos validar una IPv4 Correcta
            try { ipIpv6 = IPAddress.Parse(IPv6); }
            catch (ArgumentNullException e) { LogError("IPv6" + e.Source); return Error_Tipo_IPv6; }
            catch (FormatException e) { LogError("IPv6" + e.Source); return Error_Tipo_IPv6; }
            catch (Exception e) { LogError("IPv6" + e.Source); return Error_Tipo_IPv6; }


            String sCom = "netsh ";
            sCom += "interface ipv6 set address interface=" + "\"" + niInterfaz.Name + "\"";
            sCom += " address=" + ipIpv6.ToString() + " type=unicast store=active";

            //Con el comando listo a ejecutarse  lo guardamos en el log y lo ejecutamos
            LogError(sCom);
            return ExecuteCommandNetsh(sCom);
        }

        /// <summary>
        /// Recibe una lista de potenciales Gateways en formato IPv6 para ser añadidos.
        /// NOTA: Solo añade rutas ::/0 es decir, ultimo recurso, para algo mas especializado
        /// se requiere una funcion distinta (No creada para la 1.2)
        /// </summary>
        /// <param name="sGateways"></param>
        /// <returns></returns>
        public int SetIPv6Gateways(String[] sGateways)
        {

            //Se supone de antemano toda las IP address estan validadas pero 
            //nos aseugraremos de ello 
            string sShell;
            int iC = 1;
            int iCheck;
            foreach (String Address in sGateways)
            {
                //Solamente cuando la IP address es correcta 
                if (GetIsValidIPAddress(Address, Dirrecion_IPv6))
                {

                    //Antes de todo esto, DNS  no borra nada cuadno se anexan, asi que si queremos que este "limpio"
                    // hay que borrar lo que tenia previamente
                    /*sShell = "netsh interface ipv4 delete dnsserver name=\"" + niInterfaz.Name + "\" all";
                    LogError(sShell);
                    iCheck = ExecuteCommandNetsh(sShell);*/

                    if (iC == 1)
                    {
                        sShell = " netsh interface ipv6 set route prefix=::/0 interface=\"" + niInterfaz.Name +
                             "\"  nexthop=" + Address + " store=active";


                    }
                    else
                    {
                        sShell = "netsh interface ipv6 add route prefix=::/0 interface=\"" + niInterfaz.Name +
                              "\" nexthop=" + Address + " metric=" + iC.ToString() + " store=active";
                    }
                    iC++;
                    LogError(sShell);
                    iCheck = ExecuteCommandNetsh(sShell);
                    if (iCheck != Error_Tipo_Exito) //
                        return iCheck;
                }
                else return Error_Tipogtwv6;

            }

            return Error_Tipo_Exito;
        }

        /// <summary>
        /// Ejeceuta el comando para hacer que el DNS se obtenga via  DHCP 
        /// Nota: No esta probado el funcionamiento, pero es un comando proveido por NEtsh
        /// </summary>
        /// <returns></returns>
        public int setDNSv6Address()
        {
            String sShell;

            sShell = "netsh interface ipv6 set dnsserver name=\"" + niInterfaz.Name + "\" source=dhcp register=both validate=yes";
            LogError(sShell);
            return ExecuteCommandNetsh(sShell);
        }

        /// <summary>
        /// Se encarga de anexar uno o mas servidores DNS estaticos a la interfaz para el protocolo IPv6
        /// </summary>
        /// <param name="sIPAddresses"></param>
        /// <returns></returns>
        public int setDNSv6Address(String[] sIPAddresses)
        {
            //Se supone de antemano toda las IP address estan validadas pero 
            //nos aseugraremos de ello 
            string sShell;
            int iC = 1;
            int iCheck;
            foreach (String Address in sIPAddresses)
            {
                //Solamente cuando la IP address es correcta 
                if (GetIsValidIPAddress(Address, false))
                {

                    //Antes de todo esto, DNS  no borra nada cuadno se anexan, asi que si queremos que este "limpio"
                    // hay que borrar lo que tenia previamente
                    /*sShell = "netsh interface ipv4 delete dnsserver name=\"" + niInterfaz.Name + "\" all";
                    LogError(sShell);
                    iCheck = ExecuteCommandNetsh(sShell);*/

                    if (iC == 1)
                    {
                        sShell = "netsh interface ipv6 set dnsserver name=\"" + niInterfaz.Name +
                             "\" source=static address=" + Address + " primary";

                    }
                    else
                    {
                        sShell = "netsh interface ipv6 add dnsserver name=\"" + niInterfaz.Name +
                              "\" address=" + Address + " index=" + iC.ToString();
                    }
                    iC++;
                    LogError(sShell);
                    iCheck = ExecuteCommandNetsh(sShell);
                    if (iCheck != Error_Tipo_Exito) //
                        return iCheck;
                }
                else return Error_Tipo_dnsv6;

            }

            return Error_Tipo_Exito;
        }

        /// <summary>
        /// Esto ejecuta el comando RESET de la interfaz con IPv6
        /// En sintexis, borra toda las configuraciones y la deja con la de defecto.
        /// Observacion: Reinicio de la interfaz solicita REINICIO DE LA PC!
        /// </summary>
        /// <returns></returns>
        private int ResetIPv6Config()
        {
            String sCom = "netsh ";
            sCom += "interface ipv6 reset";

            //Con el comando listo a ejecutarse  lo guardamos en el log y lo ejecutamos
            LogError(sCom);
            return ExecuteCommandNetsh(sCom);    
        }

        /// <summary>
        /// Este metodo REINICIARA Toda las configuraciones de IPv6 borrando efectivamente
        /// todo los datos que tenia la interfaz. 
        /// Esto en teoria forzara al cliente de Win7 a volver a ejecutar NDP y por lo mismo
        /// se autoasignara su dirrecion IETf64|Random con o sin datos del gateway (Dependiendo si existe
        /// o no )
         /// </summary>
        /// <returns></returns>
        public int SetIPv6_PlugPlay()
        {
            return ResetIPv6Config(); 
        }

        /// <summary>
        /// Solicitar una dirrecion DHCP en IPv6 en un cliente Windows resulta ser tarea no facil 
        /// y eso se debe mas a que por defecto es "Plug & Play" debemos matar eso  y forzar
        /// a que pida la dirrecion  por DHCPv6. Por lo mismo se deben hacer primero unos pasos
        /// adicionales a los que normalmente se harian con IPv4.
        /// http://support.microsoft.com/kb/961433
        /// </summary>
        /// <returns></returns>
        private int setIPv6_DHCPClient()
        {
            int iSituacion;
            //Primer paso:  Disable Router Discovery.
            String sCom = "netsh interface ipv6  set interface" ;
            sCom += " interface=\"" + niInterfaz.Name + "\" routerdiscovery=disabled";

            //Lo ejecutamos (Si falla no lo intentamos mas)
           iSituacion = ExecuteCommandNetsh(sCom);
           if (!(iSituacion == Error_Tipo_Exito))
               return iSituacion;

            //Segundo Paso:  Enable Managed Address Configuration 
            //           OR Enable Other Stateful Configuration.
            // Esto es un problema porque en ambos se aconseja ejecutar
            // ipconfig /renew6 | /release6
 
           sCom = "netsh interface ipv6  set interface";
           sCom += " interface=\"" + niInterfaz.Name + "\" managedaddress=enabled";
           //Lo ejecutamos (Si falla no lo intentamos mas)
           iSituacion = ExecuteCommandNetsh(sCom);
           if (!(iSituacion == Error_Tipo_Exito))
               return iSituacion;

           sCom = "ipconfig /renew6  \"" + niInterfaz.Name + "\"";
           iSituacion = ExecuteCommand(sCom);

            //HAcemos una pequeña excepcion aqui, mandamos un mensaje de error 
            // pero sin retroalimentacion de origen del problema (A cambiar despues)
           if (!(iSituacion == Error_Tipo_Exito))
               return Error_Tipo_netsh;

            //En teoria... ya hicimos todo lo necesario... queda esperar... y esperar...
            return Error_Tipo_Exito;
        }

        /// <summary>
        /// Este metodo remueve la dirrecion IPv6 dada.
        /// </summary>
        /// <param name="sIPv6"></param>
        /// <returns></returns>
        public int RemoveIPv6Address(string IPv6)
        {

            IPAddress ipIpv6;
            IPv6 = IPv6.ToLower();
            //Primero debemos validar una IPv6 Correcta
            try { ipIpv6 = IPAddress.Parse(IPv6); }
            catch (ArgumentNullException e) { LogError("IPv6" + e.Source); return Error_Tipo_IPv6; }
            catch (FormatException e) { LogError("IPv6" + e.Source); return Error_Tipo_IPv6; }
            catch (Exception e) { LogError("IPv6" + e.Source); return Error_Tipo_IPv6; }


            String sCom = "netsh ";
            sCom += "interface ipv6 delete address interface=" + "\"" + niInterfaz.Name + "\"";
            sCom += " address=" + ipIpv6.ToString();

            //Netsh regresa un error algo ambiguo cuando se  trata de borrar una link-local 
            //el error es:  "It should be a valid IPv6 address." pero obviamente no es sintaxis
            //si no el hecho que lo que se desea borrar es una Link address (Fe80::/10)
            //por lo mismo es un caso especial que separaremos de ese error con su propio mensaje.
            if ( IPv6.Contains("fe80")  )
                return Error_Tipo_LinkAddress;

            //Con el comando listo a ejecutarse  lo guardamos en el log y lo ejecutamos
            LogError(sCom);
            return ExecuteCommandNetsh(sCom);
        }

        /// <summary>
        /// Recibe una listad e Gateways a ser removidos.
        /// NOTA: Los gateways no necesitan prefijo, ya que se entiende es de 128
        /// </summary>
        /// <param name="sGateways"></param>
        /// <returns></returns>
        public int RemoveIPv6Gateways(String[] sGateways)
        {

            //Se supone de antemano toda las IP address estan validadas pero 
            //nos aseugraremos de ello 
            string sShell;
            int iC = 1;
            int iCheck;
            foreach (String Address in sGateways)
            {
                //Solamente cuando la IP address es correcta 
                if (GetIsValidIPAddress(Address, Dirrecion_IPv6))
                {

                    if (iC == 1)
                    {
                        sShell = " netsh interface ipv6 delete route prefix=::/0 interface=\"" + niInterfaz.Name +
                             "\"  nexthop=" + Address;


                    }
                    else
                    {
                        sShell = "netsh interface ipv6 delete route prefix=::/0 interface=\"" + niInterfaz.Name +
                              "\" nexthop=" + Address + " metric=" + iC.ToString();
                    }
                    iC++;
                    LogError(sShell);
                    iCheck = ExecuteCommandNetsh(sShell);
                    if (iCheck != Error_Tipo_Exito) //
                        return iCheck;
                }
                else return Error_Tipogtwv6;

            }

            return Error_Tipo_Exito;
        }

        /// <summary>
        /// Regresa la coleccion de servidores 
        /// Observacion: Existe la posibilidad de que regrese NULL si la interfaz
        /// esta caida (o muerta), ademas de una lista vacia.
        /// </summary>
        /// <returns></returns>
        public IPAddressCollection getDNSv6Address()
        {
            //Si la interfaz esta down no se hace nada
            if (!getIntStatus())
                return null; //Una lista vacia por seguridad 
            IPInterfaceProperties Propiedades = niInterfaz.GetIPProperties();
            //Puede estar vacia, pero es informacion fideligna
            for (int ic = 0; ic < Propiedades.UnicastAddresses.Count; ic++)
                if (Propiedades.UnicastAddresses[ic].Address.AddressFamily == AddressFamily.InterNetworkV6)
                    return niInterfaz.GetIPProperties().DnsAddresses;
            //Si salio del for, significa que no tiene un protocolo IPv4 configurado, asi que tenemos que enviar un 
            //elemento vacio
            return null;
        }

        /// <summary>
        /// Actualmente regresa NULL (No existen servidores WINS para IPv6
        /// </summary>
        /// <returns></returns>
        public IPAddressCollection getWINSv6Address()
        {
            return null;
        }

        /// <summary>
        /// Por el momento es una funcion vacia
        /// </summary>
        /// <param name="WINServers"></param>
        /// <returns></returns>
        public int setWINSv6Address(String[] WINServers)
        {
            return 1;
        }

        /// <summary>
        /// Los gateway se manejan en su propio conjunto por lo tanto 
        /// los regresamos tal por cual (Haciendo un filtro para solo
        /// regresar el protocolo  de red debido).
        /// El Mayor incoveniente es que esta clase en particular no permite mas que lectura
        /// por lo mismo regresamos un listado de strings.
        /// </summary>
        /// <returns></returns>
        public List<String> getAllGatewaysv6()
        {
            List<String> Gateways = new List<String>();

            //Si la interfaz esta down no se hace nada
            if (!getIntStatus())
                return null; //Una lista vacia por seguridad 

            //Puede estar vacia, pero es informacion fideligna
            foreach (GatewayIPAddressInformation Gtw in niInterfaz.GetIPProperties().GatewayAddresses)
            {
                if (Gtw.Address.AddressFamily == AddressFamily.InterNetworkV6)
                    Gateways.Add(Gtw.Address.ToString());
            }
            return Gateways;
        }

        #endregion
        #endregion

    }
}
