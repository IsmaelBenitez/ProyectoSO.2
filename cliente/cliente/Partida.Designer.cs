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
            this.components = new System.ComponentModel.Container();
            this.Chat = new System.Windows.Forms.Label();
            this.MensajeBox = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btn_chat = new System.Windows.Forms.Button();
            this.Dado_btn = new System.Windows.Forms.Button();
            this.itziar_btn = new System.Windows.Forms.Button();
            this.aza_button = new System.Windows.Forms.Button();
            this.pedro_btn = new System.Windows.Forms.Button();
            this.guillem_btn = new System.Windows.Forms.Button();
            this.victor_btn = new System.Windows.Forms.Button();
            this.ismael_btn = new System.Windows.Forms.Button();
            this.Sol_btn = new System.Windows.Forms.Button();
            this.turno_lbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Cartas_lbl = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Chat
            // 
            this.Chat.BackColor = System.Drawing.Color.Snow;
            this.Chat.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Chat.Location = new System.Drawing.Point(551, 548);
            this.Chat.Name = "Chat";
            this.Chat.Size = new System.Drawing.Size(244, 99);
            this.Chat.TabIndex = 26;
            this.Chat.Visible = false;
            // 
            // MensajeBox
            // 
            this.MensajeBox.Location = new System.Drawing.Point(551, 650);
            this.MensajeBox.Name = "MensajeBox";
            this.MensajeBox.Size = new System.Drawing.Size(172, 20);
            this.MensajeBox.TabIndex = 27;
            this.MensajeBox.Visible = false;
            this.MensajeBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MensajeBox_KeyPress);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // listBox2
            // 
            this.listBox2.Font = new System.Drawing.Font("Mongolian Baiti", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 16;
            this.listBox2.Items.AddRange(new object[] {
            "Barra metálica",
            "Cables",
            "Cuchillo",
            "Extintor",
            "Osciloscopio",
            "Termo",
            "Tijeras",
            "Veneno"});
            this.listBox2.Location = new System.Drawing.Point(890, 548);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(118, 132);
            this.listBox2.TabIndex = 39;
            this.listBox2.Visible = false;
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("Mongolian Baiti", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Items.AddRange(new object[] {
            "Azahara",
            "Guillem",
            "Ismael",
            "Itziar",
            "Pedro",
            "Victor"});
            this.listBox1.Location = new System.Drawing.Point(810, 548);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(74, 132);
            this.listBox1.TabIndex = 40;
            this.listBox1.Visible = false;
            // 
            // btn_chat
            // 
            this.btn_chat.Location = new System.Drawing.Point(729, 650);
            this.btn_chat.Name = "btn_chat";
            this.btn_chat.Size = new System.Drawing.Size(66, 20);
            this.btn_chat.TabIndex = 28;
            this.btn_chat.Text = "Enviar";
            this.btn_chat.UseVisualStyleBackColor = true;
            this.btn_chat.Visible = false;
            this.btn_chat.Click += new System.EventHandler(this.btn_chat_Click);
            // 
            // Dado_btn
            // 
            this.Dado_btn.BackgroundImage = global::cliente.Properties.Resources.d1;
            this.Dado_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Dado_btn.Location = new System.Drawing.Point(1038, 548);
            this.Dado_btn.Name = "Dado_btn";
            this.Dado_btn.Size = new System.Drawing.Size(114, 109);
            this.Dado_btn.TabIndex = 41;
            this.Dado_btn.UseVisualStyleBackColor = true;
            this.Dado_btn.Visible = false;
            this.Dado_btn.Click += new System.EventHandler(this.Dado_btn_Click);
            // 
            // itziar_btn
            // 
            this.itziar_btn.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.itziar_btn.BackgroundImage = global::cliente.Properties.Resources.itziar;
            this.itziar_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.itziar_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.itziar_btn.Location = new System.Drawing.Point(796, 279);
            this.itziar_btn.Name = "itziar_btn";
            this.itziar_btn.Size = new System.Drawing.Size(175, 250);
            this.itziar_btn.TabIndex = 34;
            this.itziar_btn.UseVisualStyleBackColor = false;
            this.itziar_btn.Click += new System.EventHandler(this.itziar_btn_Click);
            // 
            // aza_button
            // 
            this.aza_button.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.aza_button.BackgroundImage = global::cliente.Properties.Resources.azahara;
            this.aza_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.aza_button.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.aza_button.FlatAppearance.BorderSize = 0;
            this.aza_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.aza_button.Location = new System.Drawing.Point(796, 12);
            this.aza_button.Name = "aza_button";
            this.aza_button.Size = new System.Drawing.Size(175, 250);
            this.aza_button.TabIndex = 33;
            this.aza_button.UseVisualStyleBackColor = false;
            this.aza_button.Click += new System.EventHandler(this.aza_button_Click);
            // 
            // pedro_btn
            // 
            this.pedro_btn.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pedro_btn.BackgroundImage = global::cliente.Properties.Resources.pedro;
            this.pedro_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pedro_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pedro_btn.Location = new System.Drawing.Point(977, 279);
            this.pedro_btn.Name = "pedro_btn";
            this.pedro_btn.Size = new System.Drawing.Size(175, 250);
            this.pedro_btn.TabIndex = 32;
            this.pedro_btn.UseVisualStyleBackColor = false;
            this.pedro_btn.Click += new System.EventHandler(this.pedro_btn_Click);
            // 
            // guillem_btn
            // 
            this.guillem_btn.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.guillem_btn.BackgroundImage = global::cliente.Properties.Resources.guillem;
            this.guillem_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.guillem_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.guillem_btn.Location = new System.Drawing.Point(977, 12);
            this.guillem_btn.Name = "guillem_btn";
            this.guillem_btn.Size = new System.Drawing.Size(175, 250);
            this.guillem_btn.TabIndex = 31;
            this.guillem_btn.UseVisualStyleBackColor = false;
            this.guillem_btn.Click += new System.EventHandler(this.guillem_btn_Click);
            // 
            // victor_btn
            // 
            this.victor_btn.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.victor_btn.BackgroundImage = global::cliente.Properties.Resources.victor;
            this.victor_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.victor_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.victor_btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.victor_btn.Location = new System.Drawing.Point(1158, 279);
            this.victor_btn.Name = "victor_btn";
            this.victor_btn.Size = new System.Drawing.Size(175, 250);
            this.victor_btn.TabIndex = 30;
            this.victor_btn.UseVisualStyleBackColor = false;
            this.victor_btn.Click += new System.EventHandler(this.victor_btn_Click);
            // 
            // ismael_btn
            // 
            this.ismael_btn.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ismael_btn.BackgroundImage = global::cliente.Properties.Resources.ismael;
            this.ismael_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ismael_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ismael_btn.Location = new System.Drawing.Point(1158, 12);
            this.ismael_btn.Name = "ismael_btn";
            this.ismael_btn.Size = new System.Drawing.Size(175, 250);
            this.ismael_btn.TabIndex = 29;
            this.ismael_btn.UseVisualStyleBackColor = false;
            this.ismael_btn.Click += new System.EventHandler(this.ismael_btn_Click);
            // 
            // Sol_btn
            // 
            this.Sol_btn.Location = new System.Drawing.Point(1068, 657);
            this.Sol_btn.Name = "Sol_btn";
            this.Sol_btn.Size = new System.Drawing.Size(84, 23);
            this.Sol_btn.TabIndex = 42;
            this.Sol_btn.Text = "Solucionar";
            this.Sol_btn.UseVisualStyleBackColor = true;
            this.Sol_btn.Visible = false;
            this.Sol_btn.Click += new System.EventHandler(this.Sol_btn_Click);
            // 
            // turno_lbl
            // 
            this.turno_lbl.AutoSize = true;
            this.turno_lbl.BackColor = System.Drawing.Color.Transparent;
            this.turno_lbl.Font = new System.Drawing.Font("Mongolian Baiti", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.turno_lbl.Location = new System.Drawing.Point(1035, 532);
            this.turno_lbl.Name = "turno_lbl";
            this.turno_lbl.Size = new System.Drawing.Size(52, 16);
            this.turno_lbl.TabIndex = 43;
            this.turno_lbl.Text = "label1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Mongolian Baiti", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(851, 529);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 16);
            this.label1.TabIndex = 44;
            this.label1.Text = "Haz tu acusación:";
            this.label1.Visible = false;
            // 
            // Cartas_lbl
            // 
            this.Cartas_lbl.AutoSize = true;
            this.Cartas_lbl.BackColor = System.Drawing.Color.Transparent;
            this.Cartas_lbl.Font = new System.Drawing.Font("Mongolian Baiti", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cartas_lbl.Location = new System.Drawing.Point(781, 9);
            this.Cartas_lbl.Name = "Cartas_lbl";
            this.Cartas_lbl.Size = new System.Drawing.Size(87, 16);
            this.Cartas_lbl.TabIndex = 45;
            this.Cartas_lbl.Text = "Mis cartas:";
            this.Cartas_lbl.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Mongolian Baiti", 20F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(741, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(220, 29);
            this.label3.TabIndex = 47;
            this.label3.Text = "Cartas sobrantes:";
            this.label3.Visible = false;
            // 
            // Partida
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.BackgroundImage = global::cliente.Properties.Resources.fondo1;
            this.ClientSize = new System.Drawing.Size(1370, 710);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Cartas_lbl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.turno_lbl);
            this.Controls.Add(this.Sol_btn);
            this.Controls.Add(this.Dado_btn);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.listBox2);
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
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Partida_FormClosing);
            this.Load += new System.EventHandler(this.Partida_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Chat;
        private System.Windows.Forms.TextBox MensajeBox;
        private System.Windows.Forms.Button ismael_btn;
        private System.Windows.Forms.Button victor_btn;
        private System.Windows.Forms.Button pedro_btn;
        private System.Windows.Forms.Button guillem_btn;
        private System.Windows.Forms.Button itziar_btn;
        private System.Windows.Forms.Button aza_button;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button Dado_btn;
        private System.Windows.Forms.Button btn_chat;
        private System.Windows.Forms.Button Sol_btn;
        private System.Windows.Forms.Label turno_lbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Cartas_lbl;
        private System.Windows.Forms.Label label3;
    }
}