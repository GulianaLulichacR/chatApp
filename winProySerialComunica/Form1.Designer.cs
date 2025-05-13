namespace winProySerialComunica
{
    partial class FormChatApp
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormChatApp));
            this.btnEnviaMensaje = new System.Windows.Forms.Button();
            this.rtxBox = new System.Windows.Forms.RichTextBox();
            this.rtxMensajeEnvia = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnEnviaMensaje
            // 
            this.btnEnviaMensaje.BackColor = System.Drawing.Color.Transparent;
            this.btnEnviaMensaje.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEnviaMensaje.BackgroundImage")));
            this.btnEnviaMensaje.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnEnviaMensaje.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnviaMensaje.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(72)))), ((int)(((byte)(74)))));
            this.btnEnviaMensaje.Location = new System.Drawing.Point(538, 606);
            this.btnEnviaMensaje.Name = "btnEnviaMensaje";
            this.btnEnviaMensaje.Size = new System.Drawing.Size(45, 45);
            this.btnEnviaMensaje.TabIndex = 1;
            this.btnEnviaMensaje.UseVisualStyleBackColor = false;
            this.btnEnviaMensaje.Click += new System.EventHandler(this.button2_Click);
            // 
            // rtxBox
            // 
            this.rtxBox.BackColor = System.Drawing.Color.White;
            this.rtxBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtxBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.rtxBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.rtxBox.HideSelection = false;
            this.rtxBox.Location = new System.Drawing.Point(35, 27);
            this.rtxBox.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.rtxBox.Name = "rtxBox";
            this.rtxBox.ReadOnly = true;
            this.rtxBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtxBox.Size = new System.Drawing.Size(562, 570);
            this.rtxBox.TabIndex = 4;
            this.rtxBox.Text = "";
            this.rtxBox.TextChanged += new System.EventHandler(this.rtxBox_TextChanged);
            // 
            // rtxMensajeEnvia
            // 
            this.rtxMensajeEnvia.BackColor = System.Drawing.Color.White;
            this.rtxMensajeEnvia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtxMensajeEnvia.Location = new System.Drawing.Point(35, 596);
            this.rtxMensajeEnvia.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.rtxMensajeEnvia.Name = "rtxMensajeEnvia";
            this.rtxMensajeEnvia.Size = new System.Drawing.Size(493, 68);
            this.rtxMensajeEnvia.TabIndex = 5;
            this.rtxMensajeEnvia.Text = "";
            this.rtxMensajeEnvia.TextChanged += new System.EventHandler(this.rtxMensajeEnvia_TextChanged);
            // 
            // FormChatApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(72)))), ((int)(((byte)(74)))));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(640, 663);
            this.Controls.Add(this.rtxMensajeEnvia);
            this.Controls.Add(this.rtxBox);
            this.Controls.Add(this.btnEnviaMensaje);
            this.Font = new System.Drawing.Font("Segoe UI Emoji", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Name = "FormChatApp";
            this.Text = "ChatApp";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnEnviaMensaje;
        private System.Windows.Forms.RichTextBox rtxBox;
        private System.Windows.Forms.RichTextBox rtxMensajeEnvia;
    }
}

