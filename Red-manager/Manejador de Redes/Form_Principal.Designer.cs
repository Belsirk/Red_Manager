using System.Drawing;
namespace Manejador_de_Redes
{
    partial class Form_Principal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Principal));
            this.bot_about = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.box_lista_interfases = new System.Windows.Forms.ComboBox();
            this.bt_FreshInterfaces = new System.Windows.Forms.Button();
            this.bt_ipv4_form_avanzado = new System.Windows.Forms.Button();
            this.bt_open_netsh = new System.Windows.Forms.Button();
            this.bt_ipv6_form_avanzado = new System.Windows.Forms.Button();
            this.rd_ipv6_static = new System.Windows.Forms.RadioButton();
            this.rd_ipv6_dhcp = new System.Windows.Forms.RadioButton();
            this.rd_ipv6_plugplay = new System.Windows.Forms.RadioButton();
            this.ipv6_tb_gtw = new System.Windows.Forms.TextBox();
            this.tabL3Protocol = new System.Windows.Forms.TabControl();
            this.tabIPv4 = new System.Windows.Forms.TabPage();
            this.ipv4_groupbox_tipodirre = new System.Windows.Forms.GroupBox();
            this.ipv4_estatica = new System.Windows.Forms.RadioButton();
            this.ipv4_dinamica = new System.Windows.Forms.RadioButton();
            this.panel_ipv4 = new System.Windows.Forms.Panel();
            this.ipv4_bt_dhcprenew = new System.Windows.Forms.Button();
            this.ipv4_bt_editardirre = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.ipv4_tb_gtw = new System.Windows.Forms.TextBox();
            this.ipv4_tb_mask = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ipv4_tb_address = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabIPv6 = new System.Windows.Forms.TabPage();
            this.ipv6_groupbox_tipodirre = new System.Windows.Forms.GroupBox();
            this.panel_ipv6 = new System.Windows.Forms.Panel();
            this.bt_ipv6_gt_anexar = new System.Windows.Forms.Button();
            this.bt_ipv6_dirrecion_actualizar = new System.Windows.Forms.Button();
            this.bt_ipv6_dirrecion_quitar = new System.Windows.Forms.Button();
            this.ipv6_lb_addresses = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.bt_ipv6_dirrecion_anexar = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.picture_updown = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.text_dhcpaddress = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Notificacion = new System.Windows.Forms.NotifyIcon(this.components);
            this.Contador = new System.Windows.Forms.Timer(this.components);
            this.tabL3Protocol.SuspendLayout();
            this.tabIPv4.SuspendLayout();
            this.ipv4_groupbox_tipodirre.SuspendLayout();
            this.panel_ipv4.SuspendLayout();
            this.tabIPv6.SuspendLayout();
            this.ipv6_groupbox_tipodirre.SuspendLayout();
            this.panel_ipv6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picture_updown)).BeginInit();
            this.SuspendLayout();
            // 
            // bot_about
            // 
            this.bot_about.BackgroundImage = global::Manejador_de_Redes.Properties.Resources.symbol_help;
            this.bot_about.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bot_about.Location = new System.Drawing.Point(436, 1);
            this.bot_about.Name = "bot_about";
            this.bot_about.Size = new System.Drawing.Size(38, 39);
            this.bot_about.TabIndex = 6;
            this.toolTip1.SetToolTip(this.bot_about, "Manejador de Redes  - V 1.0\r\nAutor: Ing. Raúl Fuentes");
            this.bot_about.UseVisualStyleBackColor = true;
            this.bot_about.Click += new System.EventHandler(this.bot_about_Click);
            // 
            // box_lista_interfases
            // 
            this.box_lista_interfases.FormattingEnabled = true;
            this.box_lista_interfases.Location = new System.Drawing.Point(60, 19);
            this.box_lista_interfases.Name = "box_lista_interfases";
            this.box_lista_interfases.Size = new System.Drawing.Size(214, 21);
            this.box_lista_interfases.TabIndex = 1;
            this.toolTip1.SetToolTip(this.box_lista_interfases, "1.\tInterfaces físicas de la computadora");
            this.box_lista_interfases.SelectedValueChanged += new System.EventHandler(this.box_lista_interfases_SelectedValueChanged);
            // 
            // bt_FreshInterfaces
            // 
            this.bt_FreshInterfaces.BackColor = System.Drawing.Color.Transparent;
            this.bt_FreshInterfaces.BackgroundImage = global::Manejador_de_Redes.Properties.Resources.icon_update;
            this.bt_FreshInterfaces.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bt_FreshInterfaces.Location = new System.Drawing.Point(278, 6);
            this.bt_FreshInterfaces.Name = "bt_FreshInterfaces";
            this.bt_FreshInterfaces.Size = new System.Drawing.Size(37, 38);
            this.bt_FreshInterfaces.TabIndex = 2;
            this.toolTip1.SetToolTip(this.bt_FreshInterfaces, "Si se ha conectado o desconectado de alguna interfaz es aconsejable que primero a" +
        "ctualice la lista de interfases antes de cualquier otro cambio.");
            this.bt_FreshInterfaces.UseVisualStyleBackColor = false;
            this.bt_FreshInterfaces.Click += new System.EventHandler(this.bt_FreshInterfaces_Click);
            // 
            // bt_ipv4_form_avanzado
            // 
            this.bt_ipv4_form_avanzado.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.bt_ipv4_form_avanzado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_ipv4_form_avanzado.Location = new System.Drawing.Point(0, 161);
            this.bt_ipv4_form_avanzado.Name = "bt_ipv4_form_avanzado";
            this.bt_ipv4_form_avanzado.Size = new System.Drawing.Size(72, 23);
            this.bt_ipv4_form_avanzado.TabIndex = 0;
            this.bt_ipv4_form_avanzado.Text = "Avanzado";
            this.toolTip1.SetToolTip(this.bt_ipv4_form_avanzado, "Seleccione esta opción si es necesario configurar DNS,WIN o  se requieren mas de " +
        "un gateway.");
            this.bt_ipv4_form_avanzado.UseVisualStyleBackColor = true;
            this.bt_ipv4_form_avanzado.Click += new System.EventHandler(this.bt_form_avanzado_Click);
            // 
            // bt_open_netsh
            // 
            this.bt_open_netsh.Image = global::Manejador_de_Redes.Properties.Resources.application_xp_terminal;
            this.bt_open_netsh.Location = new System.Drawing.Point(360, 72);
            this.bt_open_netsh.Name = "bt_open_netsh";
            this.bt_open_netsh.Size = new System.Drawing.Size(25, 27);
            this.bt_open_netsh.TabIndex = 5;
            this.toolTip1.SetToolTip(this.bt_open_netsh, "Abre una ventana dodne se puede ejecutar  comnandos de NEtsh con privilegio de ad" +
        "ministrador. PErmitiendo utilizar  dichos comandos para la ocnfiguraci[on de las" +
        " interfases.");
            this.bt_open_netsh.UseVisualStyleBackColor = true;
            this.bt_open_netsh.Click += new System.EventHandler(this.bt_open_netsh_Click);
            // 
            // bt_ipv6_form_avanzado
            // 
            this.bt_ipv6_form_avanzado.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.bt_ipv6_form_avanzado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_ipv6_form_avanzado.Location = new System.Drawing.Point(0, 161);
            this.bt_ipv6_form_avanzado.Name = "bt_ipv6_form_avanzado";
            this.bt_ipv6_form_avanzado.Size = new System.Drawing.Size(72, 23);
            this.bt_ipv6_form_avanzado.TabIndex = 0;
            this.bt_ipv6_form_avanzado.Text = "Avanzado";
            this.toolTip1.SetToolTip(this.bt_ipv6_form_avanzado, "Seleccione esta opción si es necesario configurar DNS o Gateways de forma indepen" +
        "diente a la dirreción.");
            this.bt_ipv6_form_avanzado.UseVisualStyleBackColor = true;
            this.bt_ipv6_form_avanzado.Click += new System.EventHandler(this.bt_ipv6_form_avanzado_Click);
            // 
            // rd_ipv6_static
            // 
            this.rd_ipv6_static.AutoSize = true;
            this.rd_ipv6_static.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd_ipv6_static.Location = new System.Drawing.Point(6, 51);
            this.rd_ipv6_static.Name = "rd_ipv6_static";
            this.rd_ipv6_static.Size = new System.Drawing.Size(119, 19);
            this.rd_ipv6_static.TabIndex = 2;
            this.rd_ipv6_static.TabStop = true;
            this.rd_ipv6_static.Text = "Stateful (manual)";
            this.toolTip1.SetToolTip(this.rd_ipv6_static, resources.GetString("rd_ipv6_static.ToolTip"));
            this.rd_ipv6_static.UseVisualStyleBackColor = true;
            this.rd_ipv6_static.CheckedChanged += new System.EventHandler(this.rd_ipv6_static_CheckedChanged);
            // 
            // rd_ipv6_dhcp
            // 
            this.rd_ipv6_dhcp.AutoSize = true;
            this.rd_ipv6_dhcp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd_ipv6_dhcp.Location = new System.Drawing.Point(6, 35);
            this.rd_ipv6_dhcp.Name = "rd_ipv6_dhcp";
            this.rd_ipv6_dhcp.Size = new System.Drawing.Size(111, 19);
            this.rd_ipv6_dhcp.TabIndex = 1;
            this.rd_ipv6_dhcp.TabStop = true;
            this.rd_ipv6_dhcp.Text = "Stateful (DHCP)";
            this.toolTip1.SetToolTip(this.rd_ipv6_dhcp, "Si selecciona esta opción se enviara una solicitud de dirreción via DHCP. ");
            this.rd_ipv6_dhcp.UseVisualStyleBackColor = true;
            this.rd_ipv6_dhcp.CheckedChanged += new System.EventHandler(this.rd_ipv6_dhcp_CheckedChanged);
            // 
            // rd_ipv6_plugplay
            // 
            this.rd_ipv6_plugplay.AutoSize = true;
            this.rd_ipv6_plugplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd_ipv6_plugplay.Location = new System.Drawing.Point(6, 19);
            this.rd_ipv6_plugplay.Name = "rd_ipv6_plugplay";
            this.rd_ipv6_plugplay.Size = new System.Drawing.Size(140, 19);
            this.rd_ipv6_plugplay.TabIndex = 0;
            this.rd_ipv6_plugplay.TabStop = true;
            this.rd_ipv6_plugplay.Text = "Stateless (Plug & Play)";
            this.toolTip1.SetToolTip(this.rd_ipv6_plugplay, resources.GetString("rd_ipv6_plugplay.ToolTip"));
            this.rd_ipv6_plugplay.UseVisualStyleBackColor = true;
            this.rd_ipv6_plugplay.CheckedChanged += new System.EventHandler(this.rd_ipv6_plugplay_CheckedChanged);
            // 
            // ipv6_tb_gtw
            // 
            this.ipv6_tb_gtw.Location = new System.Drawing.Point(72, 84);
            this.ipv6_tb_gtw.Name = "ipv6_tb_gtw";
            this.ipv6_tb_gtw.Size = new System.Drawing.Size(178, 20);
            this.ipv6_tb_gtw.TabIndex = 1;
            this.toolTip1.SetToolTip(this.ipv6_tb_gtw, "NDP Se encarga de asignar el gateway.");
            // 
            // tabL3Protocol
            // 
            this.tabL3Protocol.Controls.Add(this.tabIPv4);
            this.tabL3Protocol.Controls.Add(this.tabIPv6);
            this.tabL3Protocol.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.tabL3Protocol.Location = new System.Drawing.Point(15, 50);
            this.tabL3Protocol.Name = "tabL3Protocol";
            this.tabL3Protocol.SelectedIndex = 0;
            this.tabL3Protocol.ShowToolTips = true;
            this.tabL3Protocol.Size = new System.Drawing.Size(327, 210);
            this.tabL3Protocol.TabIndex = 3;
            this.toolTip1.SetToolTip(this.tabL3Protocol, resources.GetString("tabL3Protocol.ToolTip"));
            // 
            // tabIPv4
            // 
            this.tabIPv4.BackgroundImage = global::Manejador_de_Redes.Properties.Resources.background_01_subsection;
            this.tabIPv4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tabIPv4.Controls.Add(this.bt_ipv4_form_avanzado);
            this.tabIPv4.Controls.Add(this.ipv4_groupbox_tipodirre);
            this.tabIPv4.Controls.Add(this.panel_ipv4);
            this.tabIPv4.Location = new System.Drawing.Point(4, 22);
            this.tabIPv4.Name = "tabIPv4";
            this.tabIPv4.Padding = new System.Windows.Forms.Padding(3);
            this.tabIPv4.Size = new System.Drawing.Size(319, 184);
            this.tabIPv4.TabIndex = 0;
            this.tabIPv4.Text = "IPv4";
            this.tabIPv4.UseVisualStyleBackColor = true;
            // 
            // ipv4_groupbox_tipodirre
            // 
            this.ipv4_groupbox_tipodirre.BackColor = System.Drawing.Color.Transparent;
            this.ipv4_groupbox_tipodirre.Controls.Add(this.ipv4_estatica);
            this.ipv4_groupbox_tipodirre.Controls.Add(this.ipv4_dinamica);
            this.ipv4_groupbox_tipodirre.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ipv4_groupbox_tipodirre.Location = new System.Drawing.Point(174, 110);
            this.ipv4_groupbox_tipodirre.Name = "ipv4_groupbox_tipodirre";
            this.ipv4_groupbox_tipodirre.Size = new System.Drawing.Size(145, 70);
            this.ipv4_groupbox_tipodirre.TabIndex = 18;
            this.ipv4_groupbox_tipodirre.TabStop = false;
            this.ipv4_groupbox_tipodirre.Text = "Tipo de Dirreción";
            // 
            // ipv4_estatica
            // 
            this.ipv4_estatica.AutoSize = true;
            this.ipv4_estatica.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ipv4_estatica.Location = new System.Drawing.Point(6, 40);
            this.ipv4_estatica.Name = "ipv4_estatica";
            this.ipv4_estatica.Size = new System.Drawing.Size(68, 19);
            this.ipv4_estatica.TabIndex = 1;
            this.ipv4_estatica.TabStop = true;
            this.ipv4_estatica.Text = "Estática";
            this.ipv4_estatica.UseVisualStyleBackColor = true;
            this.ipv4_estatica.CheckedChanged += new System.EventHandler(this.ipv4_estatica_CheckedChanged);
            // 
            // ipv4_dinamica
            // 
            this.ipv4_dinamica.AutoSize = true;
            this.ipv4_dinamica.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ipv4_dinamica.Location = new System.Drawing.Point(6, 17);
            this.ipv4_dinamica.Name = "ipv4_dinamica";
            this.ipv4_dinamica.Size = new System.Drawing.Size(123, 19);
            this.ipv4_dinamica.TabIndex = 0;
            this.ipv4_dinamica.TabStop = true;
            this.ipv4_dinamica.Text = "Dinamica (DHCP)";
            this.ipv4_dinamica.UseVisualStyleBackColor = true;
            this.ipv4_dinamica.CheckedChanged += new System.EventHandler(this.ipv4_dinamica_CheckedChanged);
            // 
            // panel_ipv4
            // 
            this.panel_ipv4.Controls.Add(this.ipv4_bt_dhcprenew);
            this.panel_ipv4.Controls.Add(this.ipv4_bt_editardirre);
            this.panel_ipv4.Controls.Add(this.label5);
            this.panel_ipv4.Controls.Add(this.ipv4_tb_gtw);
            this.panel_ipv4.Controls.Add(this.ipv4_tb_mask);
            this.panel_ipv4.Controls.Add(this.label4);
            this.panel_ipv4.Controls.Add(this.ipv4_tb_address);
            this.panel_ipv4.Controls.Add(this.label2);
            this.panel_ipv4.Location = new System.Drawing.Point(3, 3);
            this.panel_ipv4.Name = "panel_ipv4";
            this.panel_ipv4.Size = new System.Drawing.Size(293, 101);
            this.panel_ipv4.TabIndex = 6;
            // 
            // ipv4_bt_dhcprenew
            // 
            this.ipv4_bt_dhcprenew.BackgroundImage = global::Manejador_de_Redes.Properties.Resources.arrow_refresh;
            this.ipv4_bt_dhcprenew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ipv4_bt_dhcprenew.Location = new System.Drawing.Point(256, 63);
            this.ipv4_bt_dhcprenew.Name = "ipv4_bt_dhcprenew";
            this.ipv4_bt_dhcprenew.Size = new System.Drawing.Size(25, 22);
            this.ipv4_bt_dhcprenew.TabIndex = 4;
            this.toolTip1.SetToolTip(this.ipv4_bt_dhcprenew, "Si da click aqui se volvera a solicitar una dirreción DHCP. Efectivamente, este b" +
        "oton operara de forma similar al comando  \"IPconfig renew\" ");
            this.ipv4_bt_dhcprenew.UseVisualStyleBackColor = true;
            this.ipv4_bt_dhcprenew.Click += new System.EventHandler(this.ipv4_bt_dhcprenew_Click);
            // 
            // ipv4_bt_editardirre
            // 
            this.ipv4_bt_editardirre.BackgroundImage = global::Manejador_de_Redes.Properties.Resources.pencil;
            this.ipv4_bt_editardirre.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ipv4_bt_editardirre.Location = new System.Drawing.Point(256, 7);
            this.ipv4_bt_editardirre.Name = "ipv4_bt_editardirre";
            this.ipv4_bt_editardirre.Size = new System.Drawing.Size(25, 22);
            this.ipv4_bt_editardirre.TabIndex = 3;
            this.ipv4_bt_editardirre.UseVisualStyleBackColor = true;
            this.ipv4_bt_editardirre.Click += new System.EventHandler(this.ipv4_bt_editardirre_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Gateway:";
            // 
            // ipv4_tb_gtw
            // 
            this.ipv4_tb_gtw.Location = new System.Drawing.Point(72, 69);
            this.ipv4_tb_gtw.Name = "ipv4_tb_gtw";
            this.ipv4_tb_gtw.Size = new System.Drawing.Size(155, 20);
            this.ipv4_tb_gtw.TabIndex = 2;
            // 
            // ipv4_tb_mask
            // 
            this.ipv4_tb_mask.Location = new System.Drawing.Point(72, 32);
            this.ipv4_tb_mask.Name = "ipv4_tb_mask";
            this.ipv4_tb_mask.Size = new System.Drawing.Size(155, 20);
            this.ipv4_tb_mask.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Submascara:";
            // 
            // ipv4_tb_address
            // 
            this.ipv4_tb_address.Location = new System.Drawing.Point(72, 0);
            this.ipv4_tb_address.Name = "ipv4_tb_address";
            this.ipv4_tb_address.Size = new System.Drawing.Size(155, 20);
            this.ipv4_tb_address.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Dirreción:";
            // 
            // tabIPv6
            // 
            this.tabIPv6.BackgroundImage = global::Manejador_de_Redes.Properties.Resources.background_01_subsection;
            this.tabIPv6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tabIPv6.Controls.Add(this.bt_ipv6_form_avanzado);
            this.tabIPv6.Controls.Add(this.ipv6_groupbox_tipodirre);
            this.tabIPv6.Controls.Add(this.panel_ipv6);
            this.tabIPv6.Location = new System.Drawing.Point(4, 22);
            this.tabIPv6.Name = "tabIPv6";
            this.tabIPv6.Padding = new System.Windows.Forms.Padding(3);
            this.tabIPv6.Size = new System.Drawing.Size(319, 184);
            this.tabIPv6.TabIndex = 1;
            this.tabIPv6.Text = "IPv6";
            this.tabIPv6.UseVisualStyleBackColor = true;
            // 
            // ipv6_groupbox_tipodirre
            // 
            this.ipv6_groupbox_tipodirre.Controls.Add(this.rd_ipv6_static);
            this.ipv6_groupbox_tipodirre.Controls.Add(this.rd_ipv6_dhcp);
            this.ipv6_groupbox_tipodirre.Controls.Add(this.rd_ipv6_plugplay);
            this.ipv6_groupbox_tipodirre.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ipv6_groupbox_tipodirre.Location = new System.Drawing.Point(170, 110);
            this.ipv6_groupbox_tipodirre.Name = "ipv6_groupbox_tipodirre";
            this.ipv6_groupbox_tipodirre.Size = new System.Drawing.Size(145, 70);
            this.ipv6_groupbox_tipodirre.TabIndex = 8;
            this.ipv6_groupbox_tipodirre.TabStop = false;
            this.ipv6_groupbox_tipodirre.Text = "Tipo de dirreción";
            // 
            // panel_ipv6
            // 
            this.panel_ipv6.Controls.Add(this.bt_ipv6_gt_anexar);
            this.panel_ipv6.Controls.Add(this.bt_ipv6_dirrecion_actualizar);
            this.panel_ipv6.Controls.Add(this.bt_ipv6_dirrecion_quitar);
            this.panel_ipv6.Controls.Add(this.ipv6_lb_addresses);
            this.panel_ipv6.Controls.Add(this.label6);
            this.panel_ipv6.Controls.Add(this.bt_ipv6_dirrecion_anexar);
            this.panel_ipv6.Controls.Add(this.ipv6_tb_gtw);
            this.panel_ipv6.Controls.Add(this.label8);
            this.panel_ipv6.Location = new System.Drawing.Point(3, 3);
            this.panel_ipv6.Name = "panel_ipv6";
            this.panel_ipv6.Size = new System.Drawing.Size(284, 108);
            this.panel_ipv6.TabIndex = 7;
            // 
            // bt_ipv6_gt_anexar
            // 
            this.bt_ipv6_gt_anexar.BackgroundImage = global::Manejador_de_Redes.Properties.Resources.pencil;
            this.bt_ipv6_gt_anexar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bt_ipv6_gt_anexar.Location = new System.Drawing.Point(256, 82);
            this.bt_ipv6_gt_anexar.Name = "bt_ipv6_gt_anexar";
            this.bt_ipv6_gt_anexar.Size = new System.Drawing.Size(25, 22);
            this.bt_ipv6_gt_anexar.TabIndex = 5;
            this.toolTip1.SetToolTip(this.bt_ipv6_gt_anexar, "Utilice este boton cuando deseeagregar un nuevo gateway para IPv6.\r\nRecuerde que " +
        "es fundamental que el gateway exista de lo contrario no se anexara a la lista.");
            this.bt_ipv6_gt_anexar.UseVisualStyleBackColor = true;
            this.bt_ipv6_gt_anexar.Click += new System.EventHandler(this.bt_ipv6_gt_anexar_Click);
            // 
            // bt_ipv6_dirrecion_actualizar
            // 
            this.bt_ipv6_dirrecion_actualizar.BackgroundImage = global::Manejador_de_Redes.Properties.Resources.arrow_refresh;
            this.bt_ipv6_dirrecion_actualizar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bt_ipv6_dirrecion_actualizar.Location = new System.Drawing.Point(256, 3);
            this.bt_ipv6_dirrecion_actualizar.Name = "bt_ipv6_dirrecion_actualizar";
            this.bt_ipv6_dirrecion_actualizar.Size = new System.Drawing.Size(25, 22);
            this.bt_ipv6_dirrecion_actualizar.TabIndex = 2;
            this.toolTip1.SetToolTip(this.bt_ipv6_dirrecion_actualizar, "Utilice este boton cuando desee renovar la dirreción IP.\r\nTome en cuenta que depe" +
        "ndiendo si estamos utilizando el modo \"Plug & Play\" o DHCPv6 es como se actualiz" +
        "ara la dirreción.");
            this.bt_ipv6_dirrecion_actualizar.UseVisualStyleBackColor = true;
            this.bt_ipv6_dirrecion_actualizar.Click += new System.EventHandler(this.bt_ipv6_dirrecion_actualizar_Click);
            // 
            // bt_ipv6_dirrecion_quitar
            // 
            this.bt_ipv6_dirrecion_quitar.BackgroundImage = global::Manejador_de_Redes.Properties.Resources.cancel;
            this.bt_ipv6_dirrecion_quitar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bt_ipv6_dirrecion_quitar.Location = new System.Drawing.Point(256, 54);
            this.bt_ipv6_dirrecion_quitar.Name = "bt_ipv6_dirrecion_quitar";
            this.bt_ipv6_dirrecion_quitar.Size = new System.Drawing.Size(25, 22);
            this.bt_ipv6_dirrecion_quitar.TabIndex = 4;
            this.toolTip1.SetToolTip(this.bt_ipv6_dirrecion_quitar, resources.GetString("bt_ipv6_dirrecion_quitar.ToolTip"));
            this.bt_ipv6_dirrecion_quitar.UseVisualStyleBackColor = true;
            this.bt_ipv6_dirrecion_quitar.Click += new System.EventHandler(this.bt_ipv6_dirrecion_quitar_Click);
            // 
            // ipv6_lb_addresses
            // 
            this.ipv6_lb_addresses.FormattingEnabled = true;
            this.ipv6_lb_addresses.Location = new System.Drawing.Point(72, 7);
            this.ipv6_lb_addresses.Name = "ipv6_lb_addresses";
            this.ipv6_lb_addresses.Size = new System.Drawing.Size(178, 69);
            this.ipv6_lb_addresses.TabIndex = 0;
            this.ipv6_lb_addresses.SelectedIndexChanged += new System.EventHandler(this.ipv6_lb_addresses_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Gateway:";
            // 
            // bt_ipv6_dirrecion_anexar
            // 
            this.bt_ipv6_dirrecion_anexar.BackgroundImage = global::Manejador_de_Redes.Properties.Resources.add;
            this.bt_ipv6_dirrecion_anexar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bt_ipv6_dirrecion_anexar.Location = new System.Drawing.Point(256, 26);
            this.bt_ipv6_dirrecion_anexar.Name = "bt_ipv6_dirrecion_anexar";
            this.bt_ipv6_dirrecion_anexar.Size = new System.Drawing.Size(25, 22);
            this.bt_ipv6_dirrecion_anexar.TabIndex = 3;
            this.toolTip1.SetToolTip(this.bt_ipv6_dirrecion_anexar, "Al hacer click aqui se anexara una nueva dirrecion IPv6 a la lista. Stateful debe" +
        " estar seleccionado previamente para que esto ocurra.");
            this.bt_ipv6_dirrecion_anexar.UseVisualStyleBackColor = true;
            this.bt_ipv6_dirrecion_anexar.Click += new System.EventHandler(this.bt_ipv6_dirrecion_anexar_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Dirreción/Px:";
            // 
            // picture_updown
            // 
            this.picture_updown.Image = global::Manejador_de_Redes.Properties.Resources.Down;
            this.picture_updown.Location = new System.Drawing.Point(321, 6);
            this.picture_updown.Name = "picture_updown";
            this.picture_updown.Size = new System.Drawing.Size(36, 38);
            this.picture_updown.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picture_updown.TabIndex = 10;
            this.picture_updown.TabStop = false;
            this.toolTip1.SetToolTip(this.picture_updown, "Status de la interfaz seleccionada.");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Interfaz";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 263);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(474, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // text_dhcpaddress
            // 
            this.text_dhcpaddress.Enabled = false;
            this.text_dhcpaddress.Location = new System.Drawing.Point(360, 263);
            this.text_dhcpaddress.Name = "text_dhcpaddress";
            this.text_dhcpaddress.Size = new System.Drawing.Size(120, 20);
            this.text_dhcpaddress.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(362, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Módulos Adicionales";
            // 
            // Notificacion
            // 
            this.Notificacion.BalloonTipText = "Este es el manejador de redes proveido para los laboratorios de redes de Itesm Ca" +
    "mpus Monterrey";
            this.Notificacion.BalloonTipTitle = "Manejador de redes - ITESM";
            this.Notificacion.Icon = ((System.Drawing.Icon)(resources.GetObject("Notificacion.Icon")));
            this.Notificacion.Text = "notifyIManejador de redes - ITESMcon1";
            this.Notificacion.Visible = true;
            this.Notificacion.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Notificacion_MouseDoubleClick);
            // 
            // Contador
            // 
            this.Contador.Interval = 2000;
            // 
            // Form_Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImage = global::Manejador_de_Redes.Properties.Resources.background_01;
            this.ClientSize = new System.Drawing.Size(474, 285);
            this.Controls.Add(this.picture_updown);
            this.Controls.Add(this.tabL3Protocol);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.bt_open_netsh);
            this.Controls.Add(this.text_dhcpaddress);
            this.Controls.Add(this.bt_FreshInterfaces);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.bot_about);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.box_lista_interfases);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Principal";
            this.Text = " Manejador de redes  (Open Source)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Principal_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Form_Principal_MouseDoubleClick);
            this.Resize += new System.EventHandler(this.Form_Principal_Resize);
            this.tabL3Protocol.ResumeLayout(false);
            this.tabIPv4.ResumeLayout(false);
            this.ipv4_groupbox_tipodirre.ResumeLayout(false);
            this.ipv4_groupbox_tipodirre.PerformLayout();
            this.panel_ipv4.ResumeLayout(false);
            this.panel_ipv4.PerformLayout();
            this.tabIPv6.ResumeLayout(false);
            this.ipv6_groupbox_tipodirre.ResumeLayout(false);
            this.ipv6_groupbox_tipodirre.PerformLayout();
            this.panel_ipv6.ResumeLayout(false);
            this.panel_ipv6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picture_updown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bot_about;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox box_lista_interfases;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button bt_FreshInterfaces;
        private System.Windows.Forms.TextBox text_dhcpaddress;
        private System.Windows.Forms.Button bt_ipv4_form_avanzado;
        private System.Windows.Forms.Button bt_open_netsh;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NotifyIcon Notificacion;
        private System.Windows.Forms.TabPage tabIPv6;
        private System.Windows.Forms.TabPage tabIPv4;
        private System.Windows.Forms.TabControl tabL3Protocol;
        private System.Windows.Forms.Panel panel_ipv4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox ipv4_tb_gtw;
        private System.Windows.Forms.TextBox ipv4_tb_mask;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ipv4_tb_address;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel_ipv6;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox ipv6_tb_gtw;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox ipv4_groupbox_tipodirre;
        private System.Windows.Forms.RadioButton ipv4_estatica;
        private System.Windows.Forms.RadioButton ipv4_dinamica;
        private System.Windows.Forms.Button bt_ipv6_form_avanzado;
        private System.Windows.Forms.GroupBox ipv6_groupbox_tipodirre;
        private System.Windows.Forms.RadioButton rd_ipv6_static;
        private System.Windows.Forms.RadioButton rd_ipv6_dhcp;
        private System.Windows.Forms.RadioButton rd_ipv6_plugplay;
        private System.Windows.Forms.ListBox ipv6_lb_addresses;
        private System.Windows.Forms.PictureBox picture_updown;
        private System.Windows.Forms.Button ipv4_bt_editardirre;
        private System.Windows.Forms.Button bt_ipv6_dirrecion_quitar;
        private System.Windows.Forms.Button bt_ipv6_dirrecion_anexar;
        private System.Windows.Forms.Button ipv4_bt_dhcprenew;
        private System.Windows.Forms.Timer Contador;
        private System.Windows.Forms.Button bt_ipv6_dirrecion_actualizar;
        private System.Windows.Forms.Button bt_ipv6_gt_anexar;
    }
}

