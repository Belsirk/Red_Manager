using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Manejador_de_Redes
{
    /// <summary>
    /// ESte formulario es meramente para  tener un lugar donde introducir una dirrecion de Red
    /// puede ser en cualquier protocolo (Por lo mismo se le debede proporcionar 
    /// </summary>
    public partial class form_ipaddress : Form
    {
        private Boolean bModificado;
        private String sIPAddress;

        private bool bVersionIP;
        public bool VersionIP
        { set { bVersionIP = value; } }
        /// <summary>
        /// Esta variable nos indica si el usuario al final si dio 
        /// una IP (v4 ó v6) adecuada.
        /// </summary>
        public Boolean Valido
        {
            get { return bModificado; }
            set { bModificado = value; }
        }

        public String IPAddress
        {
            get { return sIPAddress; }
        }

        /// <summary>
        /// 
        /// </summary>
        public form_ipaddress()
        {
            InitializeComponent();
            //Aseguramos que las variables importantes esten en blanco
            bModificado = false;
            sIPAddress = "";
        }

        /// <summary>
        /// Este es el evento para validar la dirreción IP 
        /// Para dejarlo sencillo, pasaremos un vil y simple Parse para validar el formato, en caso qeu sea erroneo...
        /// le notificaremos al usuario, en caso qeu sea valido, cerramos el formulario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            BoxInterfasesRedes Validador = new BoxInterfasesRedes();

            if (!Validador.GetIsValidIPAddress(text_ip.Text, bVersionIP))
            {
                MessageBox.Show("Error en el formato", "Campo invalido", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }
            //Como la IP es valida
            sIPAddress = text_ip.Text;
            bModificado = true;
            this.Close();
        }

        /// <summary>
        /// En este evento no se registra el cambio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            bModificado = false;
            this.Close();
        }
    }
}
