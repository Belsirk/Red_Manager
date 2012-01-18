namespace Manejador_de_Redes
{
    partial class Form_netsh
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_netsh));
            this.text_pantalla = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.text_command = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.list_interfases = new System.Windows.Forms.ListBox();
            this.bt_pegar_interfases = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.list_mac = new System.Windows.Forms.ListBox();
            this.bt_pegar_mac = new System.Windows.Forms.Button();
            this.bt_ejecutar = new System.Windows.Forms.Button();
            this.bt_borrar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // text_pantalla
            // 
            this.text_pantalla.Location = new System.Drawing.Point(16, 15);
            this.text_pantalla.Margin = new System.Windows.Forms.Padding(4);
            this.text_pantalla.Multiline = true;
            this.text_pantalla.Name = "text_pantalla";
            this.text_pantalla.ReadOnly = true;
            this.text_pantalla.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.text_pantalla.Size = new System.Drawing.Size(772, 549);
            this.text_pantalla.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 572);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Comando : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(94, 570);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "netsh";
            // 
            // text_command
            // 
            this.text_command.Location = new System.Drawing.Point(144, 569);
            this.text_command.Name = "text_command";
            this.text_command.Size = new System.Drawing.Size(644, 22);
            this.text_command.TabIndex = 3;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(795, 15);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox2.Size = new System.Drawing.Size(226, 80);
            this.textBox2.TabIndex = 4;
            this.textBox2.Text = resources.GetString("textBox2.Text");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(795, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(229, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "Nombre de interfases fisicas:";
            // 
            // list_interfases
            // 
            this.list_interfases.FormattingEnabled = true;
            this.list_interfases.ItemHeight = 16;
            this.list_interfases.Location = new System.Drawing.Point(798, 147);
            this.list_interfases.Name = "list_interfases";
            this.list_interfases.Size = new System.Drawing.Size(223, 84);
            this.list_interfases.TabIndex = 7;
            // 
            // bt_pegar_interfases
            // 
            this.bt_pegar_interfases.Location = new System.Drawing.Point(808, 237);
            this.bt_pegar_interfases.Name = "bt_pegar_interfases";
            this.bt_pegar_interfases.Size = new System.Drawing.Size(202, 26);
            this.bt_pegar_interfases.TabIndex = 8;
            this.bt_pegar_interfases.Text = "Insertar en linea de comando";
            this.bt_pegar_interfases.UseVisualStyleBackColor = true;
            this.bt_pegar_interfases.Click += new System.EventHandler(this.bt_pegar_interfases_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(795, 293);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(238, 18);
            this.label4.TabIndex = 9;
            this.label4.Text = "Dirreciones MAC de interfases";
            // 
            // list_mac
            // 
            this.list_mac.FormattingEnabled = true;
            this.list_mac.ItemHeight = 16;
            this.list_mac.Location = new System.Drawing.Point(798, 314);
            this.list_mac.Name = "list_mac";
            this.list_mac.Size = new System.Drawing.Size(223, 84);
            this.list_mac.TabIndex = 10;
            // 
            // bt_pegar_mac
            // 
            this.bt_pegar_mac.Location = new System.Drawing.Point(808, 404);
            this.bt_pegar_mac.Name = "bt_pegar_mac";
            this.bt_pegar_mac.Size = new System.Drawing.Size(202, 26);
            this.bt_pegar_mac.TabIndex = 11;
            this.bt_pegar_mac.Text = "Insertar en linea de comando";
            this.bt_pegar_mac.UseVisualStyleBackColor = true;
            this.bt_pegar_mac.Click += new System.EventHandler(this.bt_pegar_mac_Click);
            // 
            // bt_ejecutar
            // 
            this.bt_ejecutar.Location = new System.Drawing.Point(798, 567);
            this.bt_ejecutar.Name = "bt_ejecutar";
            this.bt_ejecutar.Size = new System.Drawing.Size(128, 26);
            this.bt_ejecutar.TabIndex = 12;
            this.bt_ejecutar.Text = "Ejecutar Comando";
            this.bt_ejecutar.UseVisualStyleBackColor = true;
            this.bt_ejecutar.Click += new System.EventHandler(this.bt_ejecutar_Click);
            // 
            // bt_borrar
            // 
            this.bt_borrar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bt_borrar.Location = new System.Drawing.Point(932, 567);
            this.bt_borrar.Name = "bt_borrar";
            this.bt_borrar.Size = new System.Drawing.Size(89, 26);
            this.bt_borrar.TabIndex = 13;
            this.bt_borrar.Text = "Borrar Cmd";
            this.bt_borrar.UseVisualStyleBackColor = true;
            this.bt_borrar.Click += new System.EventHandler(this.bt_borrar_Click);
            // 
            // Form_netsh
            // 
            this.AcceptButton = this.bt_ejecutar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.CancelButton = this.bt_borrar;
            this.ClientSize = new System.Drawing.Size(1033, 598);
            this.Controls.Add(this.bt_borrar);
            this.Controls.Add(this.bt_ejecutar);
            this.Controls.Add(this.bt_pegar_mac);
            this.Controls.Add(this.list_mac);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.bt_pegar_interfases);
            this.Controls.Add(this.list_interfases);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.text_command);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.text_pantalla);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_netsh";
            this.ShowIcon = false;
            this.Text = "Linea de commando NetSh (Privilegio administrativo)";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox text_pantalla;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox text_command;
        protected System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox list_interfases;
        private System.Windows.Forms.Button bt_pegar_interfases;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox list_mac;
        private System.Windows.Forms.Button bt_pegar_mac;
        private System.Windows.Forms.Button bt_ejecutar;
        private System.Windows.Forms.Button bt_borrar;
    }
}