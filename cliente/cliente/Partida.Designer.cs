namespace cliente
{
    partial class Partida
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
            this.Chat = new System.Windows.Forms.Label();
            this.MensajeBox = new System.Windows.Forms.TextBox();
            this.btn_chat = new System.Windows.Forms.Button();
            this.ismael_btn = new System.Windows.Forms.Button();
            this.victor_btn = new System.Windows.Forms.Button();
            this.pedro_btn = new System.Windows.Forms.Button();
            this.guillem_btn = new System.Windows.Forms.Button();
            this.itziar_btn = new System.Windows.Forms.Button();
            this.aza_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Chat
            // 
            this.Chat.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Chat.Location = new System.Drawing.Point(1111, 541);
            this.Chat.Name = "Chat";
            this.Chat.Size = new System.Drawing.Size(247, 99);
            this.Chat.TabIndex = 26;
            // 
            // MensajeBox
            // 
            this.MensajeBox.Location = new System.Drawing.Point(1186, 643);
            this.MensajeBox.Name = "MensajeBox";
            this.MensajeBox.Size = new System.Drawing.Size(172, 20);
            this.MensajeBox.TabIndex = 27;
            this.MensajeBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MensajeBox_KeyPress);
            // 
            // btn_chat
            // 
            this.btn_chat.Location = new System.Drawing.Point(1283, 669);
            this.btn_chat.Name = "btn_chat";
            this.btn_chat.Size = new System.Drawing.Size(75, 23);
            this.btn_chat.TabIndex = 28;
            this.btn_chat.Text = "Enviar";
            this.btn_chat.UseVisualStyleBackColor = true;
            this.btn_chat.Click += new System.EventHandler(this.btn_chat_Click);
            // 
            // ismael_btn
            // 
            this.ismael_btn.Location = new System.Drawing.Point(1158, 12);
            this.ismael_btn.Name = "ismael_btn";
            this.ismael_btn.Size = new System.Drawing.Size(175, 250);
            this.ismael_btn.TabIndex = 29;
            this.ismael_btn.UseVisualStyleBackColor = true;
            // 
            // victor_btn
            // 
            this.victor_btn.Location = new System.Drawing.Point(1158, 279);
            this.victor_btn.Name = "victor_btn";
            this.victor_btn.Size = new System.Drawing.Size(175, 250);
            this.victor_btn.TabIndex = 30;
            this.victor_btn.UseVisualStyleBackColor = true;
            // 
            // pedro_btn
            // 
            this.pedro_btn.Location = new System.Drawing.Point(977, 279);
            this.pedro_btn.Name = "pedro_btn";
            this.pedro_btn.Size = new System.Drawing.Size(175, 250);
            this.pedro_btn.TabIndex = 32;
            this.pedro_btn.UseVisualStyleBackColor = true;
            // 
            // guillem_btn
            // 
            this.guillem_btn.Location = new System.Drawing.Point(977, 12);
            this.guillem_btn.Name = "guillem_btn";
            this.guillem_btn.Size = new System.Drawing.Size(175, 250);
            this.guillem_btn.TabIndex = 31;
            this.guillem_btn.UseVisualStyleBackColor = true;
            // 
            // itziar_btn
            // 
            this.itziar_btn.Location = new System.Drawing.Point(796, 279);
            this.itziar_btn.Name = "itziar_btn";
            this.itziar_btn.Size = new System.Drawing.Size(175, 250);
            this.itziar_btn.TabIndex = 34;
            this.itziar_btn.UseVisualStyleBackColor = true;
            // 
            // aza_button
            // 
            this.aza_button.Location = new System.Drawing.Point(796, 12);
            this.aza_button.Name = "aza_button";
            this.aza_button.Size = new System.Drawing.Size(175, 250);
            this.aza_button.TabIndex = 33;
            this.aza_button.UseVisualStyleBackColor = true;
            this.aza_button.Click += new System.EventHandler(this.aza_button_Click);
            // 
            // Partida
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 710);
            this.Controls.Add(this.itziar_btn);
            this.Controls.Add(this.aza_button);
            this.Controls.Add(this.pedro_btn);
            this.Controls.Add(this.guillem_btn);
            this.Controls.Add(this.victor_btn);
            this.Controls.Add(this.ismael_btn);
            this.Controls.Add(this.btn_chat);
            this.Controls.Add(this.MensajeBox);
            this.Controls.Add(this.Chat);
            this.Name = "Partida";
            this.Text = "Partida";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Partida_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Chat;
        private System.Windows.Forms.TextBox MensajeBox;
        private System.Windows.Forms.Button btn_chat;
        private System.Windows.Forms.Button ismael_btn;
        private System.Windows.Forms.Button victor_btn;
        private System.Windows.Forms.Button pedro_btn;
        private System.Windows.Forms.Button guillem_btn;
        private System.Windows.Forms.Button itziar_btn;
        private System.Windows.Forms.Button aza_button;
    }
}