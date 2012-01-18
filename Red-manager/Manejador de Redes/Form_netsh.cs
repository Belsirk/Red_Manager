using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.NetworkInformation; 
namespace Manejador_de_Redes
{
    public partial class Form_netsh : Form
    {
        #region Variables 
        /// <summary>
        /// La lista que limita los parametros que esta aplicacion puede realizar, se les limita para evitar 
        /// que devbiliten la seguridad 
        /// En un futuro se planea pasar a un archivo de configuracion para  faciltiar su deshabilitacion 
        /// </summary>
        private String[] Blacklist = { "winsock", "p2p", "nap", "netio", "ras", "rpc", "wcn", "wfp", "exec",
                                      "advfirewall"};
        const String Msg_Blacklist_found = "Lo sentimos uno o mas del os parametros que utilizo estan prohibidos";
        /// <summary>
        /// Un modo rapido de habilitar o deshabilitar el filtro de seguridad de los comandos de netsh.
        /// </summary>
        const Boolean HabilitarFiltro = false;

        List<String> Historial;

        #endregion

        #region Metodos relacionados al manejo de Netsh

        /// <summary>
        /// Revisa la lista negra que tenemos en busqueda de esas palabras 
        /// para dictaminar si el comando puede o no ejecutarse. 
        /// La intencion es sencilla, evitar que puedan alterar la seguridad del dispositivo al limitar
        /// los parametros ejecutables 
        /// </summary>
        /// <param name="sComando"> El comando que se va analizar</param>
        /// <returns> Si no se hallo ningun componente de la lista negra regresa verdadero</returns>
        private Boolean IsValidCommand(String sComando) {
            
            if (!HabilitarFiltro) //Debemos hacerlo?
                return true;

            foreach (String sWord in Blacklist)
                if (sComando.Contains(sWord))
                    return false;

            return true;
        }

        /// <summary>
        /// Executes a shell command synchronously.
        /// Fuente: http://www.codeproject.com/KB/cs/Execute_Command_in_CSharp.aspx
        /// </summary>
        /// <param name="command">string command</param>
        /// <returns>string, as output of the command.</returns>
        public String ExecuteCommandSync(object command)
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
                // Display the command output.
                //Console.WriteLine(result);
                
                return result; //Si la linea queda en blanco no es error , caso contrario it's !


            }
            catch (Exception objException)
            {
                // Log the exception
                return objException.Source;
            }


        }

        #endregion

        #region Eventos del formulario
        public Form_netsh()
        {
            InitializeComponent();
            Historial = new List<string>();


            List<NetworkInterface>  niInterfases = new List<NetworkInterface>(NetworkInterface.GetAllNetworkInterfaces());
            list_interfases.BeginUpdate();
            list_interfases.Items.Clear();

            list_mac.BeginUpdate();
            list_mac.Items.Clear();

            for (int c = 0; c < niInterfases.Count; c++)
            {
                /// El gran problema que se tiene  es que Win7 maneja demasiadas interfases especiales por 
                /// cada una que tengamos, asi que tenemos uqe filtrar hasta obtener las que si son
                /// interfases fisicas, despues estas las almacenaremos tal por cual dentro del Combobox 
                /// box_lista_interfases y utilizaremos la clase BoxInterfasesRedes para facilitarnos las cosas
                if ((niInterfases[c].NetworkInterfaceType != NetworkInterfaceType.Loopback) &&
                        (niInterfases[c].NetworkInterfaceType != NetworkInterfaceType.Tunnel) &&
                    (niInterfases[c].NetworkInterfaceType != NetworkInterfaceType.Unknown) /*&& 
                        (niInterfases[c].OperationalStatus == OperationalStatus.Up)*/)
                {

                    list_interfases.Items.Add(niInterfases[c].Name.ToString());
                    list_mac.Items.Add(niInterfases[c].GetPhysicalAddress().ToString());

                }

            }

            
            list_interfases.EndUpdate();
            list_mac.EndUpdate();
        }


        /// <summary>
        /// En este evento se envia lo que se tiene en text_comando 
        /// a la consola para ejecutarse Nesth + text_comando previo una 
        /// validacion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_ejecutar_Click(object sender, EventArgs e)
        {
            String sComm ="netsh " + text_command.Text;
            String sResultado;
            if (!this.IsValidCommand(sComm))
            {
                MessageBox.Show(Msg_Blacklist_found);
                return;
            }
            else if (this.text_command.Text.Equals(""))
                return;

            //Si paso el filtro entonces, sencillamente lo ejecutamos 
            sResultado = ExecuteCommandSync(sComm);
            //Y lo que sea el resultado lo desplegamos 
            Historial.Add(sResultado);
            text_pantalla.Lines = Historial.ToArray();

            //Para mayor comodidad, borramos el comand oque introdujo el usuario
            text_command.Text = "";

        }

        #endregion

        /// <summary>
        /// En este evento enviamos lo que se tenga seleccionado en la lista de interfaez
        /// al textbox del coamndo qeu se desea enviar .
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_pegar_interfases_Click(object sender, EventArgs e)
        {
            //Se tiene algo seleccionado ?
            int iC;
            iC = list_interfases.SelectedIndex;
            if (iC == -1)
                return;
            else 
                
            //Vilmente lo mandamos...
            text_command.Text += "\""+ list_interfases.Items[iC].ToString() + "\"";



            return;
        }

        /// <summary>
        /// En este evento enviamos lo que se tenga seleccionado en la lista de mac address
        /// al textbox del coamndo qeu se desea enviar .
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_pegar_mac_Click(object sender, EventArgs e)
        {
            //Se tiene algo seleccionado ?
            int iC;
            iC = list_mac.SelectedIndex;
            if (iC == -1)
                return;
            else

                //Vilmente lo mandamos...
                text_command.Text +=  list_mac.Items[iC].ToString();

            return;
        }

        /// <summary>
        /// Este evento borra el contenido del texto de la linea de comando
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_borrar_Click(object sender, EventArgs e)
        {
            text_command.Text = "";
        }
    }
}
