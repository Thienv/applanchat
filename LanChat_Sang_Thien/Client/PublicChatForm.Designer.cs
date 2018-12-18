namespace Client
{
    partial class PublicChatForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtReceive = new System.Windows.Forms.RichTextBox();
            this.menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.privateChat = new System.Windows.Forms.ToolStripMenuItem();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.userList = new System.Windows.Forms.ListBox();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(583, 406);
            this.panel1.TabIndex = 0;
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.Location = new System.Drawing.Point(336, 343);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(91, 44);
            this.btnSend.TabIndex = 7;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
           // this.btnSend.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtInput_PreviewKeyDown);
            // 
            // txtReceive
            // 
            this.txtReceive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReceive.BackColor = System.Drawing.Color.White;
            this.txtReceive.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReceive.HideSelection = false;
            this.txtReceive.Location = new System.Drawing.Point(44, 44);
            this.txtReceive.Name = "txtReceive";
            this.txtReceive.ReadOnly = true;
            this.txtReceive.Size = new System.Drawing.Size(383, 293);
            this.txtReceive.TabIndex = 5;
            this.txtReceive.Text = "";
            this.txtReceive.TextChanged += new System.EventHandler(this.txtReceive_TextChanged);
           // this.txtReceive.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtInput_PreviewKeyDown);
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.privateChat});
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(139, 26);
            this.menu.Opening += new System.ComponentModel.CancelEventHandler(this.menu_Opening);
            //this.menu.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtInput_PreviewKeyDown);
            // 
            // privateChat
            // 
            this.privateChat.Name = "privateChat";
            this.privateChat.Size = new System.Drawing.Size(138, 22);
            this.privateChat.Text = "Private Chat";
            // 
            // txtInput
            // 
            this.txtInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInput.Location = new System.Drawing.Point(44, 343);
            this.txtInput.Multiline = true;
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(286, 44);
            this.txtInput.TabIndex = 6;
            this.txtInput.TextChanged += new System.EventHandler(this.txtInput_TextChanged);
            this.txtInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInput_KeyDown);
           // this.txtInput.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtInput_PreviewKeyDown);
            // 
            // userList
            // 
            this.userList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.userList.ContextMenuStrip = this.menu;
            this.userList.FormattingEnabled = true;
            this.userList.Location = new System.Drawing.Point(433, 44);
            this.userList.Name = "userList";
            this.userList.Size = new System.Drawing.Size(129, 342);
            this.userList.TabIndex = 4;
            this.userList.SelectedIndexChanged += new System.EventHandler(this.userList_SelectedIndexChanged);
           // this.userList.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtInput_PreviewKeyDown);
            // 
            // PublicChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 430);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtReceive);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.userList);
            this.Controls.Add(this.panel1);
            this.Name = "PublicChatForm";
            this.Text = "PublicChatForm";
            this.menu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.RichTextBox txtReceive;
        private System.Windows.Forms.ContextMenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem privateChat;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.ListBox userList;
    }
}