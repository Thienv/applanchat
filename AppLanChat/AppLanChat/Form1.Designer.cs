namespace AppLanChat
{
    partial class Form1
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
            this.panelChatClient = new System.Windows.Forms.Panel();
            this.txtFriendOnline = new System.Windows.Forms.TextBox();
            this.txtShowMessage = new System.Windows.Forms.TextBox();
            this.btnSent = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.panelChatClient.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelChatClient
            // 
            this.panelChatClient.Controls.Add(this.txtFriendOnline);
            this.panelChatClient.Controls.Add(this.txtShowMessage);
            this.panelChatClient.Controls.Add(this.btnSent);
            this.panelChatClient.Controls.Add(this.txtMessage);
            this.panelChatClient.Location = new System.Drawing.Point(12, 12);
            this.panelChatClient.Name = "panelChatClient";
            this.panelChatClient.Size = new System.Drawing.Size(776, 426);
            this.panelChatClient.TabIndex = 0;
            // 
            // txtFriendOnline
            // 
            this.txtFriendOnline.Location = new System.Drawing.Point(597, 25);
            this.txtFriendOnline.Multiline = true;
            this.txtFriendOnline.Name = "txtFriendOnline";
            this.txtFriendOnline.Size = new System.Drawing.Size(151, 323);
            this.txtFriendOnline.TabIndex = 7;
            // 
            // txtShowMessage
            // 
            this.txtShowMessage.Location = new System.Drawing.Point(3, 25);
            this.txtShowMessage.Multiline = true;
            this.txtShowMessage.Name = "txtShowMessage";
            this.txtShowMessage.Size = new System.Drawing.Size(562, 323);
            this.txtShowMessage.TabIndex = 6;
            // 
            // btnSent
            // 
            this.btnSent.Location = new System.Drawing.Point(673, 375);
            this.btnSent.Name = "btnSent";
            this.btnSent.Size = new System.Drawing.Size(75, 23);
            this.btnSent.TabIndex = 5;
            this.btnSent.Text = "Sent";
            this.btnSent.UseVisualStyleBackColor = true;
            this.btnSent.Click += new System.EventHandler(this.btnSent_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(3, 378);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(664, 20);
            this.txtMessage.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelChatClient);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panelChatClient.ResumeLayout(false);
            this.panelChatClient.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelChatClient;
        private System.Windows.Forms.TextBox txtFriendOnline;
        private System.Windows.Forms.TextBox txtShowMessage;
        private System.Windows.Forms.Button btnSent;
        private System.Windows.Forms.TextBox txtMessage;
    }
}

