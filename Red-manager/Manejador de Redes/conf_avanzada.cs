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
    /// <summary>
    /// La funcion de este formulario es poder tener un lugar donde se puedan manipular directamente
    /// el total de servidores DNS, WIN y/o Gateway sin importar la version (IPv6 es un poco mas estricto
    /// con los gateways ya que primero corrabora que efectivamente existan).
    /// El formulario debe ser inicializado y antes de desplegarlo se debe de indicar con cual version
    /// se trabajara. La mayor parte del trabajo lo hara el mismo formulario.
    /// </summary>
    public partial class conf_avanzada : Form
    {
        /// <summary>
        /// Esta constante esta para deshabilitar cualquier  cosa relacionada con WINS en IPv6
        /// Si en un futuro se debe de operar igual que con IPv4 solo hay necesidad de cambiar este valor.
        /// </summary>
        const bool bWINSv6 = false;

        const bool bWorking_IPv4 = true;
        const bool bWOrking_IPv6 = false;

        /// <summary>
        ///Indica que tipo de dirreción IP estamos utilizando ((IPv4, IPv6)) \n
        ///IPv4 = true \n
        ///IPv6 = false
        /// </summary>
        bool bVersionIP = false;

        /// <summary>
        /// Sirve para indicar que tipo de dirreción IP estamos esperando (IPv4, IPv6)
        /// </summary>
        public bool VersionIP
        {
            //get { return bVersionIP; }
            set { bVersionIP = value;
                //Originalmente, esto estaba cuando se inicializaba la clase, pero en ese momento
                //Se desconoce el valor de bVersion, asi que si cambia, se debe de repoblar las talbas
                PoblarTablas();
            }

        }

        //Esta variable es para una decision rapida acerca de  si se permite o no modificar 
        //algun dato si se tiene respuesta de un servidor DHCP. 
        // Lo pongo como variable constante para no tener que ir a buscar las lineas
        // Tentativa: Añadir la opcion directamente de permitirlo (Checbokx?)
        const Boolean bModificable_DHCP = false;


        //Toda la información que tenemos de la itnerfaz a configurar
        BoxInterfasesRedes nInterfaz;

        //Al cerrar el formulario para guardar cambios  usaremos estos bools 
        //para decidir si debe guardar o no los cambios
        private Boolean bModificado_dns = false;
        private Boolean bModificado_win = false;
        private Boolean bModificado_gtw = false;



        public conf_avanzada()
        {
            InitializeComponent();
            //nInterfaz = 
        }
        /// <summary>
        /// Este es el constructor que poblara de forma correcta el nuevo formulario
        /// </summary>
        /// <param name="Interfaz"></param>
        public conf_avanzada(BoxInterfasesRedes Interfaz)
        {
            InitializeComponent();
            nInterfaz = Interfaz;

            bModificado_dns = false;
            bModificado_win = false;
            bModificado_gtw = false;
            
            /// Esta linea fue omitida ya que  en estos momentos, no hay informacion 
            /// valida en bVersionIP para poblar adecuadamente las tablas
            /// Si se queda, se deber[ia de volver a ejecutar para estar seguros
            //PoblarTablas();

        }

        /// <summary>
        /// Se encarga de poblar el listbox correspondiente al DNS 
        /// </summary>
        /// <returns></returns>
        private int PoblarTabla_DNS()
        {
            IPAddressCollection ipDNS;

            if (bVersionIP == bWorking_IPv4)
                ipDNS = nInterfaz.getDNSv4Address();
            else
                ipDNS = nInterfaz.getDNSv6Address();
            
            list_dns.BeginUpdate();
            list_dns.Items.Clear();
            //SI, SOLO SI   la lista no esta vacia!
            if (ipDNS == null)
            {
                list_dns.Items.Add("No hay informacion");
                //Es peligroso si el usuario intenta anexar info 
                //ya que estamos ante un potecial riesgo de daño
                panel_dns.Enabled = false;
                return 1;
            }
            else
                for (int iC = 0; iC < ipDNS.Count; iC++)
                {
                    //Este conjunto de dirreciones pueden estar mezcladas en distintos 
                    //protoclos L3 (IPv4 e IPv6) solo queremos un tipo y solo ese tipo
                    if (nInterfaz.ConfirmarTipoIP(ipDNS[iC], bVersionIP))
                        list_dns.Items.Add(ipDNS[iC].ToString());
                }


            //Ahora, debemos decidir si el usuario puede o no editar esta informacion 
            //regla basica: Si hay ip fija, el usuario puede editar 
            if (bVersionIP == bWorking_IPv4)
                if (!(nInterfaz.getisDHCPv4Enabled())  || bModificable_DHCP)
                    panel_dns.Enabled = true;
                else
                    panel_dns.Enabled = false;
            else //IPv6
                if  (! (nInterfaz.getisDHCPv6Enabled())  || bModificable_DHCP)
                    panel_dns.Enabled = true;
                else
                    panel_dns.Enabled = false;


            list_dns.Update();
            list_dns.ClearSelected();
            list_dns.EndUpdate();

            return 1;
        }

        /// <summary>
        /// Se encarga de poblar el listbox correspondiente al WIN 
        /// </summary>
        /// <returns></returns>
        private int PoblarTabla_WIN()
        {
            IPAddressCollection ipWINS;

            //WINS por el momento solo jala en IPv4
            //si aqui detectamos que la cosa murio tons 
            // no hay que gastar mas recursos
            if (bVersionIP == bWorking_IPv4)
                ipWINS = nInterfaz.getWINv4Address();
            else if (bWINSv6)
                ipWINS = nInterfaz.getWINSv6Address();
            else
            {
                list_wins.Items.Add("No habilitado para IPv6");
                panel_wins.Enabled = false;
                return 1;
            }

            list_wins.BeginUpdate();

            list_wins.Items.Clear();
            //SI, SOLO SI   la lista no esta vacia!
            if (ipWINS == null)
            {
                list_wins.Items.Add("No hay informacion");
                //Es peligroso si el usuario intenta anexar info 
                //ya que estamos ante un potecial riesgo de daño
                panel_wins.Enabled = false;
                return 1;
            }
            else
                for (int iC = 0; iC < ipWINS.Count; iC++)
                {
                    //Este conjunto de dirreciones pueden estar mezcladas en distintos 
                    //protoclos L3 (IPv4 e IPv6) solo queremos un tipo y solo ese tipo
                    //NOTA: En este caso, no existe (aun) un servidor WINS para IPv6
                    //if (bWINSv6 && !bVersionIP)
                        if (nInterfaz.ConfirmarTipoIP(ipWINS[iC], bVersionIP))
                            list_wins.Items.Add(ipWINS[iC].ToString());
                }
            //Ahora, debemos decidir si el usuario puede o no editar esta informacion 
            //regla basica: Si hay ip fija, el usuario puede editar 
            if (bVersionIP==bWorking_IPv4)
                if (!(nInterfaz.getisDHCPv4Enabled())  || bModificable_DHCP)
                    panel_wins.Enabled = true;
                else
                    panel_wins.Enabled = false;
            else if (!(nInterfaz.getisDHCPv6Enabled()) || bModificable_DHCP)
                panel_wins.Enabled = true;
            else
                panel_wins.Enabled = false;

            list_wins.Update();
            list_wins.ClearSelected();
            list_wins.EndUpdate();

            return 1;
        }

        /// <summary>
        /// Se encarga de poblar el listbox correspondiente a los gateways 
        /// </summary>
        /// <returns></returns>
        private int PoblarTabla_Gateway()
        {
            List<String> ipGtws;

            if (bVersionIP == bWorking_IPv4)
                ipGtws = nInterfaz.getAllGatewaysv4();
            else
                ipGtws = nInterfaz.getAllGatewaysv6();

            list_gtw.BeginUpdate();
            list_gtw.Items.Clear();
            //SI, SOLO SI   la lista no esta vacia!
            if (ipGtws == null)
            {
                list_gtw.Items.Add("No hay informacion");
                //Es peligroso si el usuario intenta anexar info 
                //ya que estamos ante un potecial riesgo de daño
                panel_gtws.Enabled = false;
                return 1;
            }
            else
                for (int iC = 0; iC < ipGtws.Count; iC++)
                        list_gtw.Items.Add(ipGtws[iC]);
            //Ahora, debemos decidir si el usuario puede o no editar esta informacion 
            //regla basica: Si hay ip fija, el usuario puede editar 
            if (bVersionIP==bWorking_IPv4)
                if (!(nInterfaz.getisDHCPv4Enabled()) || bModificable_DHCP)
                    panel_gtws.Enabled = true;
                else
                    panel_gtws.Enabled = false;
            else if (!(nInterfaz.getisDHCPv6Enabled())  || bModificable_DHCP)
                panel_gtws.Enabled = true;
            else
                panel_gtws.Enabled = false;
            
                

            list_gtw.Update();
            list_gtw.ClearSelected();
            list_gtw.EndUpdate();

            return 1;
        }

        /// <summary>
        /// Se tiene 3 tablas DNS, WIN y Gateways, hay que llenarlas con la información que se tiene en nInterfaz
        /// </summary>
        /// <returns></returns>
        private int PoblarTablas()
        {

            int iDNS, iWINS, iGtws;

            //Actualizamos cada uno de los tabuladores
            iDNS = PoblarTabla_DNS();
            iWINS = PoblarTabla_WIN();
            iGtws = PoblarTabla_Gateway();

            //DNS tiene un detalle adicional, estando con IP dinamica, los servidores DNS pueden estar fijos o dinamicos
            //estando con IP estatica, los servidores DNS solo pueden ser fijos
            if ( (nInterfaz.getisDHCPv4Enabled() && bVersionIP) || (nInterfaz.getisDHCPv6Enabled() && !bVersionIP)){
                //dns_dinamico.Checked = nInterfaz.IsDynamicDnsEnabled(); //Por alguna razon siempre me regresa true aunque sea falso
                dns_dinamico.Checked = true;
                dns_dinamico.Enabled = true;}
            else
            {
                dns_dinamico.Checked = false;
                dns_dinamico.Enabled = false;
            }
            bModificado_dns = false; //Como el chequeo nos dispara un evento antes de tiempo reseteamos  esta variable 

            //Los botones generales tambien van... el de cancelar (cerrar) it's ok pero el de aceptar y guardar cambios
            //debemos habilitarlo solo si queremos que se puedan guardar cambios
            ///OBSERVACION: Debido a que DNS puede ser estatico o dinamico sin importar que DHCP este habilitado, las siguientes
            ///lines fueron  surpimidas.
            /*if (!nInterfaz.getisDHCPenabled() || bModificable_DHCP)
                bt_guardar.Enabled = true;
            else
                bt_guardar.Enabled = false;*/

            //Listo ...
            return 1;
        }

        /// <summary>
        /// Esta serie de eventos son relacionados para mover un item dentro de una tabla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_dns_up_Click(object sender, EventArgs e)
        {
            int iIndex = list_dns.SelectedIndex;
            String sAux;
            //El mas alto  no puede elevarse mas 
            if (iIndex <= 0)
                return;

            sAux = list_dns.Items[iIndex - 1].ToString();
            list_dns.Items[iIndex - 1] = list_dns.Items[iIndex];
            list_dns.Items[iIndex] = sAux;

            //Para facilitar las cosas... el nuevo item qeuda seleccionado
            list_dns.SelectedIndex = iIndex - 1;
            //list_dns.Select();

            bModificado_dns = true;
        }
        private void bt_dns_down_Click(object sender, EventArgs e)
        {
            int iIndex = list_dns.SelectedIndex;
            String sAux;
            //El mas alto  no puede elevarse mas 
            if (iIndex < 0)
                return;
            else if (iIndex >= list_dns.Items.Count - 1)
                return;

            sAux = list_dns.Items[iIndex + 1].ToString();
            list_dns.Items[iIndex + 1] = list_dns.Items[iIndex];
            list_dns.Items[iIndex] = sAux;

            //Para facilitar las cosas... el nuevo item qeuda seleccionado
            list_dns.SelectedIndex = iIndex + 1;

            bModificado_dns = true;
        }
        private void bt_win_up_Click(object sender, EventArgs e)
        {
            int iIndex = list_wins.SelectedIndex;
            String sAux;
            //El mas alto  no puede elevarse mas 
            if (iIndex <= 0)
                return;

            sAux = list_wins.Items[iIndex - 1].ToString();
            list_wins.Items[iIndex - 1] = list_wins.Items[iIndex];
            list_wins.Items[iIndex] = sAux;

            //Para facilitar las cosas... el nuevo item qeuda seleccionado
            list_wins.SelectedIndex = iIndex - 1;
            //list_dns.Select();

            bModificado_win = true;
        }
        private void bt_win_down_Click(object sender, EventArgs e)
        {
            int iIndex = list_wins.SelectedIndex;
            String sAux;
            //El mas alto  no puede elevarse mas 
            if (iIndex < 0)
                return;
            else if (iIndex >= list_wins.Items.Count - 1)
                return;

            sAux = list_wins.Items[iIndex + 1].ToString();
            list_wins.Items[iIndex + 1] = list_wins.Items[iIndex];
            list_wins.Items[iIndex] = sAux;

            //Para facilitar las cosas... el nuevo item qeuda seleccionado
            list_wins.SelectedIndex = iIndex + 1;

            bModificado_win = true;
        }
        private void bt_gtw_up_Click(object sender, EventArgs e)
        {
            int iIndex = list_gtw.SelectedIndex;
            String sAux;
            //El mas alto  no puede elevarse mas 
            if (iIndex <= 0)
                return;

            sAux = list_gtw.Items[iIndex - 1].ToString();
            list_gtw.Items[iIndex - 1] = list_gtw.Items[iIndex];
            list_gtw.Items[iIndex] = sAux;

            //Para facilitar las cosas... el nuevo item qeuda seleccionado
            list_gtw.SelectedIndex = iIndex - 1;
            //list_dns.Select();

            bModificado_gtw = true;
        }
        private void bt_gtw_down_Click(object sender, EventArgs e)
        {
            int iIndex = list_gtw.SelectedIndex;
            String sAux;
            //El mas alto  no puede elevarse mas 
            if (iIndex < 0)
                return;
            else if (iIndex >= list_gtw.Items.Count - 1)
                return;

            sAux = list_gtw.Items[iIndex + 1].ToString();
            list_gtw.Items[iIndex + 1] = list_gtw.Items[iIndex];
            list_gtw.Items[iIndex] = sAux;

            //Para facilitar las cosas... el nuevo item qeuda seleccionado
            list_gtw.SelectedIndex = iIndex + 1;

            bModificado_gtw = true;

        }

        /// <summary>
        /// Esta serie de eventos son para remover el item (Seleccionado) de la tabla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_dns_remove_Click(object sender, EventArgs e)
        {
            int iIndex = list_dns.SelectedIndex;

            //El mas alto  no puede elevarse mas 
            if (iIndex < 0)
                return;

            //NO USAR poblartablXXX() ya que toma una copia de las colecciones (Solo lectura)
            list_dns.BeginUpdate();
            list_dns.Items.RemoveAt(iIndex);
            list_dns.EndUpdate();

            bModificado_dns = true;
        }
        private void bt_win_remove_Click(object sender, EventArgs e)
        {
            int iIndex = list_wins.SelectedIndex;

            //El mas alto  no puede elevarse mas 
            if (iIndex < 0)
                return;

            //NO USAR poblartablXXX() ya que toma una copia de las colecciones (Solo lectura)
            list_wins.BeginUpdate();
            list_wins.Items.RemoveAt(iIndex);
            list_wins.EndUpdate();

            bModificado_win = true;
        }
        private void bt_gtw_remove_Click(object sender, EventArgs e)
        {
            int iIndex = list_gtw.SelectedIndex;

            //El mas alto  no puede elevarse mas 
            if (iIndex < 0)
                return;

            //NO USAR poblartablXXX() ya que toma una copia de las colecciones (Solo lectura)
            list_gtw.BeginUpdate();
            list_gtw.Items.RemoveAt(iIndex);
            list_gtw.EndUpdate();

            bModificado_gtw = true;
        }

        /// <summary>
        /// En este evento se abre un nuevo formulario  para que el usuario introduzca
        /// una dirrecion IP valida,  en caso que sea así la IP sera introducida a la lista correspondiente
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_dns_add_Click(object sender, EventArgs e)
        {
            form_ipaddress fIPadd = new form_ipaddress();
            fIPadd.ShowDialog();
            //Le pasamos el chisme de que tipo de dirrecion estamos manejando...
            fIPadd.VersionIP = bVersionIP;
            //¿Se introdujo una IP adecuada?
            if (fIPadd.Valido)
            {
                //Anexamos la IP a la lista
                list_dns.BeginUpdate();
                list_dns.Items.Add(fIPadd.IPAddress);
                list_dns.EndUpdate();
                bModificado_dns = true;
            }
            fIPadd.Dispose();
        }
        private void bt_win_add_Click(object sender, EventArgs e)
        {
            form_ipaddress fIPadd = new form_ipaddress();

            fIPadd.ShowDialog();

            //¿Se introdujo una IP adecuada?
            if (fIPadd.Valido)
            {
                //Anexamos la IP a la lista
                list_wins.BeginUpdate();
                list_wins.Items.Add(fIPadd.IPAddress);
                list_wins.EndUpdate();
                bModificado_win = true;
            }

            fIPadd.Dispose();
        }
        private void bt_gtw_add_Click(object sender, EventArgs e)
        {
            form_ipaddress fIPadd = new form_ipaddress();

            fIPadd.ShowDialog();

            //¿Se introdujo una IP adecuada?
            if (fIPadd.Valido)
            {
                //Anexamos la IP a la lista
                list_gtw.BeginUpdate();
                list_gtw.Items.Add(fIPadd.IPAddress);
                list_gtw.EndUpdate();
                bModificado_gtw = true;
            }

            fIPadd.Dispose();
        }

        /// <summary>
        /// Este evento es extremadamente sencillo, el formulario se cierra haciendo caso omiso de 
        /// cualquier cambio el usuario halla introducido.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_dontsave_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// En este evento, se mandara a ejecutar el netsh para guardar los cambios realizados 
        /// a los listados de DNS , WINS y Gateways que se requieran hacer. 
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_guardar_Click(object sender, EventArgs e)
        {
            ///Hay que validar  cuales seran modificados y si es IPv4 o IPv6 (El comando de netshl al parecer cambia)
            String[] sCol;
            if (bModificado_dns)
            {
                sCol = new String[list_dns.Items.Count];
                list_dns.Items.CopyTo(sCol, 0);
                //Si no es dinamico engase todo lo que tenemos 
                if (!dns_dinamico.Checked)
                    if (bVersionIP)
                        nInterfaz.setDNSv4Address(sCol);
                    else
                        nInterfaz.setDNSv6Address(sCol);

                /*else if ((nInterfaz.getisDHCPv4Enabled() && bVersionIP) || (nInterfaz.getisDHCPv6Enabled() && !bVersionIP))
                    if (bVersionIP)                     // Version4 o vervsion6?
                        nInterfaz.setDNSv4Address();
                    else
                        nInterfaz.setDNSv6Address();*/
                else
                    MessageBox.Show("Para poder actualizar de forma dinamica los servidores DNS debe estar habilitado el DHCP");
            }
            //Se supone que estos campos solo cuando son estaticos dejan qeu se escriban 
            if ((nInterfaz.getisDHCPv4Enabled() && bVersionIP) ) //IPv4
            {
                if (bModificado_win)
                {
                    sCol = new String[list_wins.Items.Count];
                    list_wins.Items.CopyTo(sCol, 0);

                    nInterfaz.setWINSv4Address(sCol);
                }

                if (bModificado_gtw)
                {
                    //GatewayIPAddressInformation[] Listas = new GatewayIPAddressInformation[list_gtw.Items.Count];
                    sCol = new String[list_gtw.Items.Count];
                    list_gtw.Items.CopyTo(sCol, 0);
                    nInterfaz.setGTWv4Address(sCol);
                }

            }
            else if ( (nInterfaz.getisDHCPv6Enabled() && !bVersionIP)) //IPv6
            {
                if (bModificado_win && bWINSv6)
                {
                    sCol = new String[list_wins.Items.Count];
                    list_wins.Items.CopyTo(sCol, 0);

                    nInterfaz.setWINSv6Address(sCol);
                }

                if (bModificado_gtw)
                {
                    //GatewayIPAddressInformation[] Listas = new GatewayIPAddressInformation[list_gtw.Items.Count];
                    sCol = new String[list_gtw.Items.Count];
                    list_gtw.Items.CopyTo(sCol, 0);
                    
                    nInterfaz.SetIPv6Gateways(sCol);
                }

            }
            //y al final... cerramos el  formulario ...
            this.Close();

        }

        ////
        /// <summary>
        /// En esta serie de evento, DNS y WIN   necesitamos  no permitirles editar si quieren un servidor dinamico 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dns_dinamico_CheckedChanged(object sender, EventArgs e)
        {
            panel_dns.Enabled = !dns_dinamico.Checked;
            bModificado_dns = true;
        }

    }
}
