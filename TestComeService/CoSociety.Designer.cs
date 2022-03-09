using MaterialSkin;
using MaterialSkin.Controls;

namespace TestComeService
{
    partial class CoSociety
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txt_ClientConnection = new System.Windows.Forms.TextBox();
            this.txt_ChatUsers = new System.Windows.Forms.TextBox();
            this.txt_Client = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_CreateUser = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txt_ClientConnection
            // 
            this.txt_ClientConnection.Location = new System.Drawing.Point(0, 61);
            this.txt_ClientConnection.Multiline = true;
            this.txt_ClientConnection.Name = "txt_ClientConnection";
            this.txt_ClientConnection.Size = new System.Drawing.Size(926, 668);
            this.txt_ClientConnection.TabIndex = 0;
            // 
            // txt_ChatUsers
            // 
            this.txt_ChatUsers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_ChatUsers.Location = new System.Drawing.Point(932, 61);
            this.txt_ChatUsers.Multiline = true;
            this.txt_ChatUsers.Name = "txt_ChatUsers";
            this.txt_ChatUsers.Size = new System.Drawing.Size(217, 715);
            this.txt_ChatUsers.TabIndex = 1;
            // 
            // txt_Client
            // 
            this.txt_Client.Location = new System.Drawing.Point(603, 737);
            this.txt_Client.Name = "txt_Client";
            this.txt_Client.Size = new System.Drawing.Size(198, 27);
            this.txt_Client.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(508, 740);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "User Name: ";
            // 
            // btn_CreateUser
            // 
            this.btn_CreateUser.Location = new System.Drawing.Point(807, 735);
            this.btn_CreateUser.Name = "btn_CreateUser";
            this.btn_CreateUser.Size = new System.Drawing.Size(94, 29);
            this.btn_CreateUser.TabIndex = 4;
            this.btn_CreateUser.Text = "Add User";
            this.btn_CreateUser.UseVisualStyleBackColor = true;
            this.btn_CreateUser.Click += new System.EventHandler(this.btn_CreateUser_Click);
            // 
            // CoSociety
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1149, 776);
            this.Controls.Add(this.btn_CreateUser);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_Client);
            this.Controls.Add(this.txt_ClientConnection);
            this.Controls.Add(this.txt_ChatUsers);
            this.Name = "CoSociety";
            this.Text = "Co Society ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CoSociety_FormClosingAsync);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CoSociety_FormClosed);
            this.Load += new System.EventHandler(this.CoSociety_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox txt_ClientConnection;
        private TextBox txt_ChatUsers;
        private TextBox txt_Client;
        private Label label1;
        private Button btn_CreateUser;
    }
}