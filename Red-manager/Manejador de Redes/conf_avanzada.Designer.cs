namespace Manejador_de_Redes
{
    partial class conf_avanzada
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(conf_avanzada));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dns_dinamico = new System.Windows.Forms.CheckBox();
            this.panel_dns = new System.Windows.Forms.Panel();
            this.bt_dns_up = new System.Windows.Forms.Button();
            this.bt_dns_remove = new System.Windows.Forms.Button();
            this.bt_dns_add = new System.Windows.Forms.Button();
            this.bt_dns_down = new System.Windows.Forms.Button();
            this.list_dns = new System.Windows.Forms.ListBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel_wins = new System.Windows.Forms.Panel();
            this.bt_win_up = new System.Windows.Forms.Button();
            this.bt_win_remove = new System.Windows.Forms.Button();
            this.bt_win_add = new System.Windows.Forms.Button();
            this.bt_win_down = new System.Windows.Forms.Button();
            this.list_wins = new System.Windows.Forms.ListBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.panel_gtws = new System.Windows.Forms.Panel();
            this.bt_gtw_up = new System.Windows.Forms.Button();
            this.bt_gtw_remove = new System.Windows.Forms.Button();
            this.bt_gtw_down = new System.Windows.Forms.Button();
            this.bt_gtw_add = new System.Windows.Forms.Button();
            this.list_gtw = new System.Windows.Forms.ListBox();
            this.bt_guardar = new System.Windows.Forms.Button();
            this.bt_dontsave = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel_dns.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel_wins.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel_gtws.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dns_dinamico);
            this.tabPage2.Controls.Add(this.panel_dns);
            this.tabPage2.Controls.Add(this.list_dns);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dns_dinamico
            // 
            resources.ApplyResources(this.dns_dinamico, "dns_dinamico");
            this.dns_dinamico.Name = "dns_dinamico";
            this.dns_dinamico.UseVisualStyleBackColor = true;
            this.dns_dinamico.CheckedChanged += new System.EventHandler(this.dns_dinamico_CheckedChanged);
            // 
            // panel_dns
            // 
            this.panel_dns.Controls.Add(this.bt_dns_up);
            this.panel_dns.Controls.Add(this.bt_dns_remove);
            this.panel_dns.Controls.Add(this.bt_dns_add);
            this.panel_dns.Controls.Add(this.bt_dns_down);
            resources.ApplyResources(this.panel_dns, "panel_dns");
            this.panel_dns.Name = "panel_dns";
            // 
            // bt_dns_up
            // 
            this.bt_dns_up.Image = global::Manejador_de_Redes.Properties.Resources.arrow_up;
            resources.ApplyResources(this.bt_dns_up, "bt_dns_up");
            this.bt_dns_up.Name = "bt_dns_up";
            this.bt_dns_up.UseVisualStyleBackColor = true;
            this.bt_dns_up.Click += new System.EventHandler(this.bt_dns_up_Click);
            // 
            // bt_dns_remove
            // 
            this.bt_dns_remove.Image = global::Manejador_de_Redes.Properties.Resources.cross;
            resources.ApplyResources(this.bt_dns_remove, "bt_dns_remove");
            this.bt_dns_remove.Name = "bt_dns_remove";
            this.bt_dns_remove.UseVisualStyleBackColor = true;
            this.bt_dns_remove.Click += new System.EventHandler(this.bt_dns_remove_Click);
            // 
            // bt_dns_add
            // 
            this.bt_dns_add.Image = global::Manejador_de_Redes.Properties.Resources.add;
            resources.ApplyResources(this.bt_dns_add, "bt_dns_add");
            this.bt_dns_add.Name = "bt_dns_add";
            this.bt_dns_add.UseVisualStyleBackColor = true;
            this.bt_dns_add.Click += new System.EventHandler(this.bt_dns_add_Click);
            // 
            // bt_dns_down
            // 
            this.bt_dns_down.Image = global::Manejador_de_Redes.Properties.Resources.arrow_down;
            resources.ApplyResources(this.bt_dns_down, "bt_dns_down");
            this.bt_dns_down.Name = "bt_dns_down";
            this.bt_dns_down.UseVisualStyleBackColor = true;
            this.bt_dns_down.Click += new System.EventHandler(this.bt_dns_down_Click);
            // 
            // list_dns
            // 
            resources.ApplyResources(this.list_dns, "list_dns");
            this.list_dns.FormattingEnabled = true;
            this.list_dns.Name = "list_dns";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel_wins);
            this.tabPage1.Controls.Add(this.list_wins);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel_wins
            // 
            this.panel_wins.Controls.Add(this.bt_win_up);
            this.panel_wins.Controls.Add(this.bt_win_remove);
            this.panel_wins.Controls.Add(this.bt_win_add);
            this.panel_wins.Controls.Add(this.bt_win_down);
            resources.ApplyResources(this.panel_wins, "panel_wins");
            this.panel_wins.Name = "panel_wins";
            // 
            // bt_win_up
            // 
            this.bt_win_up.Image = global::Manejador_de_Redes.Properties.Resources.arrow_up;
            resources.ApplyResources(this.bt_win_up, "bt_win_up");
            this.bt_win_up.Name = "bt_win_up";
            this.bt_win_up.UseVisualStyleBackColor = true;
            this.bt_win_up.Click += new System.EventHandler(this.bt_win_up_Click);
            // 
            // bt_win_remove
            // 
            this.bt_win_remove.Image = global::Manejador_de_Redes.Properties.Resources.cross;
            resources.ApplyResources(this.bt_win_remove, "bt_win_remove");
            this.bt_win_remove.Name = "bt_win_remove";
            this.bt_win_remove.UseVisualStyleBackColor = true;
            this.bt_win_remove.Click += new System.EventHandler(this.bt_win_remove_Click);
            // 
            // bt_win_add
            // 
            this.bt_win_add.Image = global::Manejador_de_Redes.Properties.Resources.add;
            resources.ApplyResources(this.bt_win_add, "bt_win_add");
            this.bt_win_add.Name = "bt_win_add";
            this.bt_win_add.UseVisualStyleBackColor = true;
            this.bt_win_add.Click += new System.EventHandler(this.bt_win_add_Click);
            // 
            // bt_win_down
            // 
            this.bt_win_down.Image = global::Manejador_de_Redes.Properties.Resources.arrow_down;
            resources.ApplyResources(this.bt_win_down, "bt_win_down");
            this.bt_win_down.Name = "bt_win_down";
            this.bt_win_down.UseVisualStyleBackColor = true;
            this.bt_win_down.Click += new System.EventHandler(this.bt_win_down_Click);
            // 
            // list_wins
            // 
            resources.ApplyResources(this.list_wins, "list_wins");
            this.list_wins.FormattingEnabled = true;
            this.list_wins.Name = "list_wins";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.panel_gtws);
            this.tabPage3.Controls.Add(this.list_gtw);
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // panel_gtws
            // 
            this.panel_gtws.Controls.Add(this.bt_gtw_up);
            this.panel_gtws.Controls.Add(this.bt_gtw_remove);
            this.panel_gtws.Controls.Add(this.bt_gtw_down);
            this.panel_gtws.Controls.Add(this.bt_gtw_add);
            resources.ApplyResources(this.panel_gtws, "panel_gtws");
            this.panel_gtws.Name = "panel_gtws";
            // 
            // bt_gtw_up
            // 
            this.bt_gtw_up.Image = global::Manejador_de_Redes.Properties.Resources.arrow_up;
            resources.ApplyResources(this.bt_gtw_up, "bt_gtw_up");
            this.bt_gtw_up.Name = "bt_gtw_up";
            this.bt_gtw_up.UseVisualStyleBackColor = true;
            this.bt_gtw_up.Click += new System.EventHandler(this.bt_gtw_up_Click);
            // 
            // bt_gtw_remove
            // 
            this.bt_gtw_remove.Image = global::Manejador_de_Redes.Properties.Resources.cross;
            resources.ApplyResources(this.bt_gtw_remove, "bt_gtw_remove");
            this.bt_gtw_remove.Name = "bt_gtw_remove";
            this.bt_gtw_remove.UseVisualStyleBackColor = true;
            this.bt_gtw_remove.Click += new System.EventHandler(this.bt_gtw_remove_Click);
            // 
            // bt_gtw_down
            // 
            this.bt_gtw_down.Image = global::Manejador_de_Redes.Properties.Resources.arrow_down;
            resources.ApplyResources(this.bt_gtw_down, "bt_gtw_down");
            this.bt_gtw_down.Name = "bt_gtw_down";
            this.bt_gtw_down.UseVisualStyleBackColor = true;
            this.bt_gtw_down.Click += new System.EventHandler(this.bt_gtw_down_Click);
            // 
            // bt_gtw_add
            // 
            this.bt_gtw_add.Image = global::Manejador_de_Redes.Properties.Resources.add;
            resources.ApplyResources(this.bt_gtw_add, "bt_gtw_add");
            this.bt_gtw_add.Name = "bt_gtw_add";
            this.bt_gtw_add.UseVisualStyleBackColor = true;
            this.bt_gtw_add.Click += new System.EventHandler(this.bt_gtw_add_Click);
            // 
            // list_gtw
            // 
            resources.ApplyResources(this.list_gtw, "list_gtw");
            this.list_gtw.FormattingEnabled = true;
            this.list_gtw.Name = "list_gtw";
            // 
            // bt_guardar
            // 
            this.bt_guardar.Image = global::Manejador_de_Redes.Properties.Resources.accept;
            resources.ApplyResources(this.bt_guardar, "bt_guardar");
            this.bt_guardar.Name = "bt_guardar";
            this.bt_guardar.UseVisualStyleBackColor = true;
            this.bt_guardar.Click += new System.EventHandler(this.bt_guardar_Click);
            // 
            // bt_dontsave
            // 
            this.bt_dontsave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bt_dontsave.Image = global::Manejador_de_Redes.Properties.Resources.cancel;
            resources.ApplyResources(this.bt_dontsave, "bt_dontsave");
            this.bt_dontsave.Name = "bt_dontsave";
            this.bt_dontsave.UseVisualStyleBackColor = true;
            this.bt_dontsave.Click += new System.EventHandler(this.bt_dontsave_Click);
            // 
            // conf_avanzada
            // 
            this.AcceptButton = this.bt_guardar;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.bt_dontsave;
            this.ControlBox = false;
            this.Controls.Add(this.bt_dontsave);
            this.Controls.Add(this.bt_guardar);
            this.Controls.Add(this.tabControl1);
            this.ForeColor = System.Drawing.SystemColors.InfoText;
            this.HelpButton = true;
            this.Name = "conf_avanzada";
            this.ShowInTaskbar = false;
            this.TopMost = true;
            //this.Load += new System.EventHandler(this.conf_avanzada_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel_dns.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel_wins.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.panel_gtws.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListBox list_dns;
        private System.Windows.Forms.Button bt_dns_add;
        private System.Windows.Forms.Button bt_dns_up;
        private System.Windows.Forms.Button bt_dns_remove;
        private System.Windows.Forms.Button bt_dns_down;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button bt_gtw_up;
        private System.Windows.Forms.Button bt_gtw_remove;
        private System.Windows.Forms.Button bt_gtw_down;
        private System.Windows.Forms.ListBox list_gtw;
        private System.Windows.Forms.Button bt_gtw_add;
        private System.Windows.Forms.Button bt_win_up;
        private System.Windows.Forms.Button bt_win_remove;
        private System.Windows.Forms.Button bt_win_down;
        private System.Windows.Forms.ListBox list_wins;
        private System.Windows.Forms.Button bt_win_add;
        private System.Windows.Forms.Button bt_guardar;
        private System.Windows.Forms.Button bt_dontsave;
        private System.Windows.Forms.Panel panel_dns;
        private System.Windows.Forms.Panel panel_wins;
        private System.Windows.Forms.Panel panel_gtws;
        private System.Windows.Forms.CheckBox dns_dinamico;
    }
}