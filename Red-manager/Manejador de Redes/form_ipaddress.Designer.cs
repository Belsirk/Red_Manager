namespace Manejador_de_Redes
{
    partial class form_ipaddress
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
            this.lab_ip = new System.Windows.Forms.Label();
            this.text_ip = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lab_ip
            // 
            this.lab_ip.AutoSize = true;
            this.lab_ip.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_ip.Location = new System.Drawing.Point(3, 23);
            this.lab_ip.Name = "lab_ip";
            this.lab_ip.Size = new System.Drawing.Size(77, 16);
            this.lab_ip.TabIndex = 0;
            this.lab_ip.Text = "Dirreción IP";
            // 
            // text_ip
            // 
            this.text_ip.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_ip.Location = new System.Drawing.Point(97, 20);
            this.text_ip.Name = "text_ip";
            this.text_ip.Size = new System.Drawing.Size(187, 22);
            this.text_ip.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::Manejador_de_Redes.Properties.Resources.accept;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Location = new System.Drawing.Point(290, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(20, 20);
            this.button1.TabIndex = 2;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackgroundImage = global::Manejador_de_Redes.Properties.Resources.cancel;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(316, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(20, 20);
            this.button2.TabIndex = 3;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // form_ipaddress
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(344, 52);
            this.ControlBox = false;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.text_ip);
            this.Controls.Add(this.lab_ip);
            this.Name = "form_ipaddress";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Dirreción IP (Version 4 ó Version 6)";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lab_ip;
        private System.Windows.Forms.TextBox text_ip;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}