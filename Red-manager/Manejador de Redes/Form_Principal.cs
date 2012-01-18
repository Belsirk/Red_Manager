using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//Adicionales
using System.Net.NetworkInformation;



namespace Manejador_de_Redes
{



    /// <summary>
    /// Este es el formulario principal del proyecto, desde aqui se puede acceder a los "modulos"
    /// adicionales  pero principalmente se maneja los cambios de dirreciones IP. En version 4 es 
    /// relativamente simple: Sobre-escribir el ya existente.  Pero en IPv6 la cosa se complica
    /// ya que puede tener multiples dirreciones.
    /// </summary>
    public partial class Form_Principal : Form
    {

        #region Constantes

        const String Log_Registro =  "RegistroIPs.txt";

        /// <summary>
        /// Estas dos constantes es para el label lab_netmask
        /// ya que su texto puede variar
        /// </summary>
        const String Label_Netmaks = "Netmask: ";
        const String Label_ScopeID = "Scope ID: ";

        /// <summary>
        /// Son los valores de la dimension horizontal del textbox text_netmask
        /// </summary>
        const int NetmaskSize = 119;
        const int ScopeIDSize = 30;

        const String Msg_ModoAutomatico = "DHCP habilitado";
        /*static String Msg_TipModoAutomatico = "La red se configura de modo automatico,"+
                                                "se recomienda si se desea renovar la IP utilizar"+
                                                "Comand Prompt";*/
        const String Msg_ModoEstatico = "IP estatica";
        //static String Msg_TipModoEstatico = "DHCP No esta habilitado";

        const String Msg_bt_OpEstatica = "Modo estatico";
        const String Msg_bt_OpDHCP = "Modo DHCP";

        const String Msg_EstatusInterfazDown = "La interfaz se encuentra deshabilitada. \n" +
                                "se requiere del administrador del Sistema para levantar dicha interfaz";
        const String Msg_EstatusInterfazUP = "La interfaz se encuentra habilitada. ";

        /// <summary>
        /// Constnates  que se utilizan para identificar el tipo de error 
        /// </summary>
        const int Error_Tipo_Exito = 1;
        const int Error_Tipo_IPv4 = -1;
        const int Error_Tipo_Mask = -2;
        const int Error_Tipo_gtwv4 = -3;
        const int Error_Tipo_dnsv4 = -4;
        const int Error_Tipo_netsh = -5;
        const int Error_Tipo_IPv6 = -7;
        const int Error_Tipogtwv6 = -8;
        const int Error_Tipo_dnsv6 = -9;
        const int Error_Tipo_Permisos = -20;
        const int Error_Tipo_Restart = -10;
        const int Error_Tipo_LinkAddress = -11;
        const int Error_Tipo_ComandoGenerico = -30;
        #endregion


        /// <summary>
        /// Usaremos objetos de libreria NetworkingInformation para no batallar
        /// </summary>
        List<NetworkInterface> niInterfases = null;





        public Form_Principal()
        {
            InitializeComponent();
        }

        #region UAC Buttons!
        [System.Runtime.InteropServices.DllImport("user32")]
        public static extern UInt32 SendMessage
            (IntPtr hWnd, UInt32 msg, UInt32 wParam, UInt32 lParam);

        internal const int BCM_FIRST = 0x1600; //Normal button

        internal const int BCM_SETSHIELD = (BCM_FIRST + 0x000C); //Elevated button

        // Make the button display the UAC shield.
        [Browsable(false)]
        public static void AddShieldToButton(Button btn)
        {
            const Int32 BCM_SETSHIELD = 0x160C;

            // Give the button the flat style and make it display the UAC shield.
            btn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            SendMessage(btn.Handle, BCM_SETSHIELD, 0, 1);
        }

        #endregion

        #region Metodos_Ajenos_a_Eventos

        /// <summary>
        /// Con esta funcion guardaremos registro de elementos improtantes
        /// NOTA: La invocacion de NetSH dentro de este programa  tiene su propio log
        /// que no necesariamente es el mismo archivo.
        /// </summary>
        /// <param name="sRegistro"></param>
        private void LogRegistro(String sRegistro)
        {
            // Para tener mayor compresion del log, le a.
            sRegistro = DateTime.Now.ToString() + ": " + sRegistro;

            try
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(Log_Registro, true);
                file.WriteLine(sRegistro);

                file.Close();
            }
            catch (Exception e)
            {
                //Donde avisar que hay un tremendo error a la hora de avisar del mismo ??
                System.Windows.Forms.MessageBox.Show("Notificar al instructor \n No se ha podido acceder al archivo de registro.  \n Error:" + e.Source);
            }

        }


        /// <summary>
        /// Se encarga de  actualziar el ComboBox box_lista_interfases con el listado de las interfases
        ///  y sus configuraciones actuales. Usualmente llamado, cuando el programa se carga y se selecciona 
        ///  el boton de actualizar, pero tambien puede ser llamado cuando la configuracion de una interfas 
        ///  es modificada.
        /// </summary>
        /// <returns></returns>
        /// 
        [Browsable(false)]
        private int ActualizarListaInterfases()
        {
            int iC2 = 0;
            BoxInterfasesRedes bxAux;
            niInterfases = new List<NetworkInterface>(NetworkInterface.GetAllNetworkInterfaces());
            box_lista_interfases.BeginUpdate();
            box_lista_interfases.Items.Clear();



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

                    iC2++;

                    bxAux = new BoxInterfasesRedes(niInterfases[c]);
                    box_lista_interfases.Items.Add(bxAux);

                }

            }

            box_lista_interfases.Sorted = true;
            box_lista_interfases.EndUpdate();

            return 1;
        }

        

        /// <summary>
        /// Este metodo se  mandara a llamar cada vez que se desee actualizar la información en pantalla 
        /// de la interfaz seleccionada. 
        /// Se debe garantizar que  este seleccionado un item en el combobox antes de mandar a llarmarlo.
        /// </summary>
        /// <returns></returns>
        /// 
        private int Actualizar_Despliegue_Interfaz()
        {
            ///Lo primero, es obtener la Interfaz seleccionada
            BoxInterfasesRedes bxInterfas = (BoxInterfasesRedes)box_lista_interfases.SelectedItem;
            ipv6_lb_addresses.BeginUpdate();

            #region Limpiar_Componentes
            //Lo primero es limpiar los cuadros de texto, con el objetivo de que no se despliegue 
            //informacion anterior y por lo mismo erronea(Particularmente importante cuando 
            //el protocolo esta deshabilitado).
            ipv4_tb_address.Text = "";
            ipv4_tb_gtw.Text = "";
            ipv4_tb_mask.Text = "";
            ipv6_tb_gtw.Text = "";
            ipv6_lb_addresses.Items.Clear();
            #endregion

            #region Detectar_EstadoInterfaz
            //Vemos como esta la itnerfaz, para motivos practicos, mostramos DOWN  para cualquier 
            //estatus distinto a una  interfaz  Levantada.  
            // En caso de estar caida deshabilito los componentes y botones de esa hoja del tabulador.
            // Dejo el Try-Catch por  trabajar con archivos fisicos (las imagenes )
            try
            {

                if (bxInterfas.getIntStatus())
                {
                    //El boton de estatus de la interfaz
                    picture_updown.Image = global::Manejador_de_Redes.Properties.Resources.UP;
                    toolTip1.SetToolTip(picture_updown, Msg_EstatusInterfazUP);
                    
                    //ADemas del estado del a interfaz depende el protocolo habilitado.
                    
                    //Los componentes IPv4
                    if (bxInterfas.getisIPv4Enable())
                    {
                        panel_ipv4.Enabled = true;
                        bt_ipv4_form_avanzado.Enabled = true;
                        ipv4_groupbox_tipodirre.Enabled = true;
                    }
                    else
                    {
                        panel_ipv4.Enabled = false;
                        bt_ipv4_form_avanzado.Enabled = false;
                        ipv4_groupbox_tipodirre.Enabled = false;
                    }
                    //Los componentes IPv6
                    if (bxInterfas.getisIPv6Enable())
                    {
                        panel_ipv6.Enabled = true;
                        bt_ipv6_form_avanzado.Enabled = true;
                        ipv6_groupbox_tipodirre.Enabled = true;
                    }
                    else
                    {
                        panel_ipv6.Enabled = false;
                        bt_ipv6_form_avanzado.Enabled = false;
                        ipv6_groupbox_tipodirre.Enabled = false;
                    }
                }
                else //TODO MUERE
                {
                    //El boton de estatus de la interfaz
                    picture_updown.Image = global::Manejador_de_Redes.Properties.Resources.Down;
                    toolTip1.SetToolTip(picture_updown, Msg_EstatusInterfazDown);
                    //Los componentes IPv4
                    panel_ipv4.Enabled = false;
                    bt_ipv4_form_avanzado.Enabled = false;
                    ipv4_groupbox_tipodirre.Enabled = false;
                    //Los componentes IPv6
                    panel_ipv6.Enabled = false;
                    bt_ipv6_form_avanzado.Enabled = false;
                    ipv6_groupbox_tipodirre.Enabled = false;
                
                }
            }
            catch (SystemException error)
            {
                throw (error);    // Rethrowing exception e
            }//Catch*
            #endregion

            #region Actualizar_datos
            //Con las ventanas preparadas llenaremos los campos de IPv4 e IPv6 con 
            //datos obtenidos de la misma interfaz.
            ipv4_tb_address.Text = bxInterfas.getIpv4Address();
            ipv4_tb_gtw.Text = bxInterfas.getFirstIpv4Gateway();
            ipv4_tb_mask.Text = bxInterfas.getNetmask();

            ipv6_tb_gtw.Text = bxInterfas.getFirstIpv6Gateway();
            List<String> lAux;
            lAux = bxInterfas.getAllIpv6Address();
            //Cada dirrecion debe ser incluida en el ListBox
            //Por defecto la primera dirrecion sera selecionada
            foreach (String sIPv6 in lAux)
                ipv6_lb_addresses.Items.Add(sIPv6);
            if(ipv6_lb_addresses.Items.Count > 0)
                ipv6_lb_addresses.SelectedIndex = 0;

            ipv6_lb_addresses.EndUpdate();
            //Ahora viene detectar el modo de trabajo de IPv4
            //Relativamente facil al solo tener un tipo de dirrecionamiento.
            if (bxInterfas.getisDHCPv4Enabled())
                ipv4_dinamica.Checked = true;
            else
                ipv4_estatica.Checked = true;

            //IPv6 es otro boleto... al tener multiples dirreciones 
            //Potencial Error en el disenio de la aplicacion: las multiples posibles
            //dirreciones por interfaz ¿permiten tener DHCP y Estaticas simultaneamente?
            //¿Permite tener stateless y stateful simultaneamente? (Ya que el link-state siempre
            // sera stateless es un hecho que pueden haber fe80:: con stateful)
            //Por el momento la aplicacion forzara a la persona a solo tener
            // DHCP | Estatica | plug&play

            //La solucion sera manejar IPv6 de forma distinta a IPv4. Se indicara el tipo
            //de dirrecion seleccionada del List Box ipv6_lb_addresses 
            //Claro, esto si hay dirreciones posibles...
            if (ipv6_lb_addresses.Items.Count < 1)
                return 1;

            if ((bxInterfas.getOrigingIPv6Suffix(lAux[0], SuffixOrigin.LinkLayerAddress)) ||
                (bxInterfas.getOrigingIPv6Suffix(lAux[0], SuffixOrigin.Random)))
                rd_ipv6_plugplay.Checked = true;
            else if (bxInterfas.getOrigingIPv6Suffix(lAux[0], SuffixOrigin.OriginDhcp))
                rd_ipv6_dhcp.Checked = true;
            else
                rd_ipv6_static.Checked = true;

            #endregion

            return 1;

        }


        /// <summary>
        /// Los eventos cuando se seleccione una IPv6 de la lista de opciones.
        /// Cada vez que se selecciona una se indica su tipo (Plug&Play, DHCP, static)
        /// Observacion: Plug&Play  considera el formato IETF64 y el Random.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ipv6_lb_addresses_SelectedIndexChanged(object sender, EventArgs e)
        {
            BoxInterfasesRedes bxInterfas = (BoxInterfasesRedes)box_lista_interfases.SelectedItem;

            //Si no hay elementos no se ejecuta (En teoria no deberia llamarse este evento 
            //si no los hay... pero mejor prevengo...
            if (ipv6_lb_addresses.Items.Count < 1)
                return;

            String sAux = ipv6_lb_addresses.Items[0].ToString();

            if ((bxInterfas.getOrigingIPv6Suffix(sAux, SuffixOrigin.LinkLayerAddress)) ||
                (bxInterfas.getOrigingIPv6Suffix(sAux, SuffixOrigin.Random)))
                rd_ipv6_plugplay.Checked = true;
            else if (bxInterfas.getOrigingIPv6Suffix(sAux, SuffixOrigin.OriginDhcp))
                rd_ipv6_dhcp.Checked = true;
            else
                rd_ipv6_static.Checked = true;
        }

         

        /// <summary>
        /// Cada vez que se mande a llamar una operación de  cmabiar la configuracion de una interfaz
        /// el valor devuelto debera ser enviado a este metodo para manejar los diferentes tipos de erorres
        /// </summary>
        /// <param name="iCheck"></param>
        private void ValidarOperacionInterfas(int iCheck)
        {
            switch (iCheck)
            {
                case Error_Tipo_IPv4: System.Windows.Forms.MessageBox.Show("El formato de la dirreción IPv4 es incorrecto"); break;
                case Error_Tipo_Mask: System.Windows.Forms.MessageBox.Show("El formato de la Mascara es incorrecto"); break;
                case Error_Tipo_gtwv4: System.Windows.Forms.MessageBox.Show("Una o mas dirreciones IPv4 de servidores DNS son incorrectas"); break;
                case Error_Tipo_dnsv4: System.Windows.Forms.MessageBox.Show("El formato de la Gateway (IPv4) es incorrecto"); break;
                case Error_Tipo_dnsv6: System.Windows.Forms.MessageBox.Show("El formato de la Gateway (IPv6) es incorrecto"); break;
                case Error_Tipo_netsh: System.Windows.Forms.MessageBox.Show("Es posible que la accion no se haya realizado correctamente (Revisar con Netsh)"); break; //Este es malo
                case Error_Tipo_Permisos: System.Windows.Forms.MessageBox.Show("El manejador de redes no tiene privilegio"); break;
                case Error_Tipo_IPv6: System.Windows.Forms.MessageBox.Show("El formato de la dirrecion IPv6 es incorrecto"); break;
                case Error_Tipogtwv6: System.Windows.Forms.MessageBox.Show("El formato de la Gateway (IPv6) es incorrecto"); break;
                case Error_Tipo_Restart: System.Windows.Forms.MessageBox.Show("Es posible que deba de reiniciar el equipo para que los cambios tomen efecto."); break;
                case Error_Tipo_LinkAddress: System.Windows.Forms.MessageBox.Show("No se puede eliminar dirreciones de la FE80::/10"); break;
                case Error_Tipo_Exito: //Exito!!
                    //Es una buena idea actualizar la interfaz sin importar el tipo de error 
                    //Con la interfaz ya modificada debemos re-actualizar la aplicacion
                    int iAux = box_lista_interfases.SelectedIndex;
                    ActualizarListaInterfases();
                    if (iAux < 0)
                        box_lista_interfases.SelectedIndex = 0;
                    else if (box_lista_interfases.Items.Count < iAux)
                        box_lista_interfases.SelectedIndex = 0;
                    else
                        box_lista_interfases.SelectedIndex = iAux;



                    Actualizar_Despliegue_Interfaz();
                    //Y colorin colorado, la interfaz ya tiene IP Fija

                    break;

                default: System.Windows.Forms.MessageBox.Show("Posible falso negativo, puede obtener mas informacion en el archivo " + Log_Registro
                     + "\n Si actualizo una IP dinamica que estuviese previamente en dinamica haga caso omiso de este aviso."); break;


            }//End-Switch


        }

        #endregion

        #region Eventos

        /// <summary>
        /// Al iniciar la carga debemos obtener la información actual de nuestras tarjetas de red 
        /// No debemos asumir que no esta conectado desde un inicio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {

            ActualizarListaInterfases();
            //Lo dejamos afuera de la funcion ActualizarListaItnerfases para quitar la molestia de perder
            //de vista  lo que se estaba trabajando.
            box_lista_interfases.SelectedIndex = 0;

            //Botoenes bonitos!! *Bueno, UAC
            //OJO: ESto no hace uqe pida una escalada de privilegios, solo anexa el boton
            AddShieldToButton(bt_ipv4_form_avanzado);
            AddShieldToButton(bt_ipv6_form_avanzado);
            
            //Por el momento deshabilitado en estos botones por problemas de estetica (Esta para futuras versiones)
            //AddShieldToButton(ipv4_bt_dhcprenew);
            //AddShieldToButton(ipv4_bt_editardirre);
           
            
            //ipv4_bt_dhcprenew.BackgroundImage = 
            //Avisamos
            LogRegistro(" El manejador ha sido inicializado \n Usuario: " + 
                System.Security.Principal.WindowsIdentity.GetCurrent().Name +
                " IsSystem: " + System.Security.Principal.WindowsIdentity.GetCurrent().IsSystem.ToString());

        }


        /// <summary>
        /// Cada vez que el combobox sea actualizado debemos reflejar la info de la interfas de red
        /// seleccionada.
        /// NOTA: Este sera invocado  al inicio cuando se pueble el combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void box_lista_interfases_SelectedValueChanged(object sender, EventArgs e)
        {

            Actualizar_Despliegue_Interfaz();
        }

        /// <summary>
        /// En este evento, el objetivo es volver a actualizar la lista de interfases  , con lo cual 
        /// se vera reflejado los cambios en el listado.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_FreshInterfaces_Click(object sender, EventArgs e)
        {
            //Ates de actualizar debemos asegurarnos de saber que item esta seleccionado 
            int iAux = box_lista_interfases.SelectedIndex;
            ActualizarListaInterfases();

            //Una vez actualizada,  seleccionamos el mismo item, esto no disparara (Al pareceR) 
            //el evento de index changed pero si validara que se tenga un item valido
            //En caso contrario usaremos un catch por cualquier riesgo de falla (Un enable/disable
            // que altere el tamaño de la coleccion de interfases por ejemplo)
            if (box_lista_interfases.Items.Count < iAux)
                box_lista_interfases.SelectedIndex = 0;
            else //Un elemento adicional, seria verificar que fuese la misma interfaz... pero no por ahora...
                box_lista_interfases.SelectedIndex = iAux;


            //Para uqe el cambio se vea reflejado de forma inmediata para el usuario 
            //tenemos uqe cargar nuevamente la informacion
            Actualizar_Despliegue_Interfaz();
        }

        /// <summary>
        /// Al ejecutar este boton se abre una nueva forma ( con_avanzadas )
        /// con el cual se supone el usuario puede introducir una serie 
        /// de colecciones de servidores DNS y WIN ademas de Gateways
        /// Para facilitar las cosas, no enviaremos colecciones, si no el objeto 
        /// de la interfaz con esa misma info
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_form_avanzado_Click(object sender, EventArgs e)
        {
            //Empeznado con lo basico, hay que declarar tres  coleccioens o listas
            //A partir de la interfaz seleccionada 

            BoxInterfasesRedes bxInterfas = (BoxInterfasesRedes)box_lista_interfases.SelectedItem;
            conf_avanzada Formulario = new conf_avanzada(bxInterfas);

            

            Formulario.VersionIP = true;
            Formulario.ShowDialog();

            //Una vez hecho las modificaciones (klas cuales se ejecutan en el mismo formulario hijo )
            //reactualizamos las interfases
            Actualizar_Despliegue_Interfaz();


        }
        private void bt_ipv6_form_avanzado_Click(object sender, EventArgs e)
        {
            BoxInterfasesRedes bxInterfas = (BoxInterfasesRedes)box_lista_interfases.SelectedItem;
            conf_avanzada Formulario = new conf_avanzada(bxInterfas);



            Formulario.VersionIP = false;
            Formulario.ShowDialog();

            //Una vez hecho las modificaciones (klas cuales se ejecutan en el mismo formulario hijo )
            //reactualizamos las interfases
            Actualizar_Despliegue_Interfaz();

        }

        /// <summary>
        /// Despliega nuestro Formulario "AboutBox"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bot_about_Click(object sender, EventArgs e)
        {
            AboutBox1 Mensaje = new AboutBox1();
            Mensaje.Show();
        }

        /// <summary>
        /// Simplemente se encarga de abrir un formulario  on la ventana de netsh disponible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_open_netsh_Click(object sender, EventArgs e)
        {
            Form_netsh Hojita = new Form_netsh();
            Hojita.Show();
        }

        /// <summary>
        /// ESte evento es para  manejar la aplicaicon en el tray icon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_Principal_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                this.ShowInTaskbar = false;
            }
        }
        private void Notificacion_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }
        private void Form_Principal_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                this.Hide();
                this.ShowInTaskbar = false;
            }
        }

        /// <summary>
        /// Originalmente, esta aplicación se iba a ejecutar como un servicio de fondo
        /// por lo mismo no se iba a permitir que se cerrara. Esa idea fue descartada para la version
        /// 1.2 y es la razon de que las lineas de codigo estan omitidas.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_Principal_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*
            //En ambos casos maxima autoridad quieren cerrar el programa
            //no hay porque  fastidiar con un mensaje que de todas formas no se le prestara 
            //Atencion.
            if ( (e.CloseReason==CloseReason.TaskManagerClosing) || 
                (e.CloseReason==CloseReason.WindowsShutDown) )
                e.Cancel = false;
            else {
                e.Cancel = true;
                System.Text.StringBuilder messageBoxCS = new System.Text.StringBuilder();
                //messageBoxCS.AppendFormat("{0} = {1}", "CloseReason", e.CloseReason);
                //messageBoxCS.AppendLine();
                //messageBoxCS.AppendFormat("{0} = {1}", "Cancel", e.Cancel);
                //messageBoxCS.AppendLine();
                messageBoxCS.AppendFormat("{0} : {1}", "Lo sentimos pero el administrador de redes" +
                    "no puede ser terminado. \n Evento de cierro negado", e.CloseReason);
                MessageBox.Show(messageBoxCS.ToString(), "Solicitud negada");
            }
            */
            LogRegistro("El manejador ha sido cerrado \n\n" );
        }

        /// <summary>
        /// Cuando el usuario selecciona el tipo de dirrecion IPv4 DHCP significa
        /// que no puede editar la dirrecion. El boton para editar desaparece y el 
        /// boton para solicitar nuevas dirreciones DHCP aparece.
        /// Caso contrario ocurre cuando el usuario selecciona IP estatica.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ipv4_dinamica_CheckedChanged(object sender, EventArgs e)
        {
            ipv4_bt_editardirre.Visible = false;
            ipv4_bt_dhcprenew.Visible = true;

            ipv4_tb_address.Enabled = false;
            ipv4_tb_mask.Enabled = false;
            ipv4_tb_gtw.Enabled = false;

        }
        private void ipv4_estatica_CheckedChanged(object sender, EventArgs e)
        {
            ipv4_bt_editardirre.Visible = true;
            ipv4_bt_dhcprenew.Visible = false;

            ipv4_tb_address.Enabled = true;
            ipv4_tb_mask.Enabled = true;
            ipv4_tb_gtw.Enabled = true;

        }



        /// <summary>
        /// Cuando el boton ipv4_bt_editardirre es presionado se esta intentando
        /// fijar una IP estatica. Por lo tanto tomamos 
        /// los campos de IP (IP/Prefijo + Gtw) y los enviamos a NethSH
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ipv4_bt_editardirre_Click(object sender, EventArgs e)
        {

            BoxInterfasesRedes bxInterfas = (BoxInterfasesRedes)box_lista_interfases.SelectedItem;
            String sIP = ipv4_tb_address.Text;
            String sMk = ipv4_tb_mask.Text;
            String sGt = ipv4_tb_gtw.Text;

            //Aunque se supone que solo se puede dar  clic cuando el boton esta disponible
            //garantizamos que sea cuando el usuario esta trabajando con IPv4 estatica.
            if (!ipv4_estatica.Checked)
                return;

            //Ejecutamos el comando para fijar la IP.
            ValidarOperacionInterfas(bxInterfas.setIPv4FullAddress(sIP, sMk, sGt));

            return;
        }

        /// <summary>
        /// Cuando el usuario da clic en el boton ipv4_bt_dhcprenew el objetivo es renovar
        /// la IP con una nueva solicitud desde un cliente DHCP. Por medio de Netsh se hara tal cosa.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ipv4_bt_dhcprenew_Click(object sender, EventArgs e)
        {
            BoxInterfasesRedes bxInterfas = (BoxInterfasesRedes)box_lista_interfases.SelectedItem;
            
            //Aunque se supone que solo se puede dar  clic cuando el boton esta disponible
            //garantizamos que sea cuando el usuario esta trabajando con IPv4 dinamica.
            if ( !ipv4_dinamica.Checked)
                return;

           ValidarOperacionInterfas(  bxInterfas.setNewDHCPClient(true) );

            //NOTA: Aqui suelen morir los que solo programan Software... el comando se ejecuta
            // en nanosegundos, pero obviamente la respuesta del servidor llegara en milisegundos
            // DEBEMOS ESPERAR para obtener una respuesta satisfactoria.
            Contador.Tick += new EventHandler(Tick_RefreshInterface); // Everytime timer ticks, timer_Tick will be called
            //Contador.Interval = (1000) * (1);              // Timer will tick evert second
            Contador.Enabled = true;                       // Enable the timer
            Contador.Start();                              // Start the timer


        }


        /// <summary>
        /// Este evento de Timer generalmente sera invocado cuando se  acaba de enviar una 
        /// solicitud de renovacion de IP via DHCP. Lo unico que hace es apagar el timer
        /// y reflejar los cambios despues de un tiempo dado.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Tick_RefreshInterface(object sender, EventArgs e)
        {
            Contador.Stop();
            Contador.Enabled = false;
            Actualizar_Despliegue_Interfaz();
        }

        /// <summary>
        /// Estos eventos son muy parecidos a IPv4 pero la cosa cambia ya que 
        /// Plug & Play y DHCPv6 son distintos pero a la vez similar (Distinto en protocolo
        /// pero para el usuario es un efecto parecido ).
        /// Por lo mismo estos dos primeros solo deben de permitir volver a actualizar una interfaz
        /// y el ultimo es el que permite anexar o quitar dirreciones en particulares.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rd_ipv6_plugplay_CheckedChanged(object sender, EventArgs e)
        {
            ipv6_lb_addresses.Enabled = false;
            ipv6_tb_gtw.Enabled = false;

            bt_ipv6_dirrecion_actualizar.Visible = true;
            bt_ipv6_dirrecion_anexar.Visible = false;
            bt_ipv6_dirrecion_quitar.Visible = false;
        }
        private void rd_ipv6_dhcp_CheckedChanged(object sender, EventArgs e)
        {
            ipv6_lb_addresses.Enabled = false;
            ipv6_tb_gtw.Enabled = false;

            bt_ipv6_dirrecion_actualizar.Visible = true;
            bt_ipv6_dirrecion_anexar.Visible = false;
            bt_ipv6_dirrecion_quitar.Visible = false;
        }
        private void rd_ipv6_static_CheckedChanged(object sender, EventArgs e)
        {
            ipv6_lb_addresses.Enabled = true;
            ipv6_tb_gtw.Enabled = true;

            bt_ipv6_dirrecion_actualizar.Visible = false;
            bt_ipv6_dirrecion_anexar.Visible = true;
            bt_ipv6_dirrecion_quitar.Visible = true;
        }

        /// <summary>
        /// Cuando se manda a llamar este evento al presionar el boton bt_ipv6_dirrecion_actualizar
        /// se desea reactualizar las dirreciones sea con una nueva oferta de DHCP o con 
        /// una rneovacion a la dirrecion enlace local.
        /// NOTA: Como IPv6 puede causar mucho revuelo se ejecutara un RESET antes de estos comandos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_ipv6_dirrecion_actualizar_Click(object sender, EventArgs e)
        {
            BoxInterfasesRedes bxInterfas = (BoxInterfasesRedes)box_lista_interfases.SelectedItem;
 
            //En teoria solo debe de pasar con dos posibilidades, pero nos aseugramos
            //de que solo sea en esos dos casos.
            if (!((rd_ipv6_dhcp.Checked) || (rd_ipv6_plugplay.Checked)))
                return;

            if (rd_ipv6_dhcp.Checked)
                ValidarOperacionInterfas(bxInterfas.setNewDHCPClient(false));
            else if (rd_ipv6_plugplay.Checked)
                ValidarOperacionInterfas(bxInterfas.SetIPv6_PlugPlay());

            //En ambos casos se recomienda esperar algo de tiempo...
            //Plug&Play requiere los mensajes de NDP, DHCPv6 requiere respuesta del servidor
            Contador.Tick += new EventHandler(Tick_RefreshInterface); // Everytime timer ticks, timer_Tick will be called
            //Contador.Interval = (1000) * (1);              // Timer will tick evert second
            Contador.Enabled = true;                       // Enable the timer
            Contador.Start();                              // Start the timer




        }

        /// <summary>
        /// Cuando se llame este evento es para eliminar una dirrecion de red IPv6 
        /// con todo y gateway de las que estan en el listbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_ipv6_dirrecion_quitar_Click(object sender, EventArgs e)
        {
            BoxInterfasesRedes bxInterfas = (BoxInterfasesRedes)box_lista_interfases.SelectedItem;
            String sIPv6Add;
            String[] sGtw = new String[1];
            //Primero, solo debe de ocurrir cuando se este trabajando con estaticas
            if(!rd_ipv6_static.Checked)
                return;

            //Segundo, solo debe de pasar cuando el LB tiene mas de un objeto
            if (! (ipv6_lb_addresses.Items.Count > 0) )
                return;

            //Tercero, solo debe de pasar cuando hay una dirrecion selecciona
            sIPv6Add = (String)ipv6_lb_addresses.SelectedItem;
            if (sIPv6Add == null)
                return;
            else if (sIPv6Add.Equals("")) //String vacio no es aceptable (Antes debemos evitar null).
                return;

            sGtw[0] = ipv6_tb_gtw.Text;

            

            ValidarOperacionInterfas(bxInterfas.RemoveIPv6Address(sIPv6Add));
            //Gtw solo cmabia si no esta en blanco 
            if ( !(sGtw[0].Equals("::")) && !(sGtw[0].Equals("0::0")) && !(sGtw[0].Equals("")) )
                ValidarOperacionInterfas(bxInterfas.RemoveIPv6Gateways(sGtw));
            
        }

        /// <summary>
        /// Cuando este evento es invocado lo que se desea es introducir una nueva 
        /// dirrecion IPv6 a la lista de dirreciones (Con todo y potencial Gateway si es uno valido)
        /// La metodologia es invocar el formulario form_ipaddress  para que el usuario 
        /// pueda introducir la sintaxis.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_ipv6_dirrecion_anexar_Click(object sender, EventArgs e)
        {
            BoxInterfasesRedes bxInterfas = (BoxInterfasesRedes)box_lista_interfases.SelectedItem;
            form_ipaddress fIPadd = new form_ipaddress();
           

            //Solo debe de ocurrir cuando se este trabajando con estaticas
            if (!rd_ipv6_static.Checked)
                return;

            fIPadd.ShowDialog();
            //Le pasamos el chisme de que que es IPv6 lo que queremos 
            fIPadd.VersionIP = false;
            //¿Se introdujo una IP adecuada?
            if (fIPadd.Valido)
            {
                
                //Anexamos la IPv6 ADdress
                ValidarOperacionInterfas(bxInterfas.setIPv6Address(fIPadd.IPAddress));
                
                
            }
            fIPadd.Dispose();

        }   

        /// <summary>
        /// Este evento anexara el Gtw de IPv6 a la lista
        /// La razon porla que se maneja separdo a la dirrecion es que puede 
        /// ser mas dificil de manipular.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_ipv6_gt_anexar_Click(object sender, EventArgs e)
        {
            String[] sGtw = new String[1];
            BoxInterfasesRedes bxInterfas = (BoxInterfasesRedes)box_lista_interfases.SelectedItem;
            //Solo debe de ocurrir cuando se este trabajando con estaticas
            if (!rd_ipv6_static.Checked)
                return;
            sGtw[0] = ipv6_tb_gtw.Text;
            //Gtw solo cmabia si no esta en blanco 
            if (!(sGtw[0].Equals("::")) && !(sGtw[0].Equals("0::0")) && !(sGtw[0].Equals("")))
                ValidarOperacionInterfas(bxInterfas.SetIPv6Gateways(sGtw));
        }


        #endregion

       
        

        #region Pruebas



        #endregion

    }
}
