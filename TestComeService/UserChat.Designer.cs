using MaterialSkin;
using MaterialSkin.Controls;

namespace TestComeService
{
    partial class UserChat
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
            this.txtMessageHistory = new System.Windows.Forms.TextBox();
            this.txt_SendText = new System.Windows.Forms.TextBox();
            this.btnSentText = new MaterialSkin.Controls.MaterialRaisedButton();
            this.SuspendLayout();
            // 
            // txtMessageHistory
            // 
            this.txtMessageHistory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessageHistory.Location = new System.Drawing.Point(12, 93);
            this.txtMessageHistory.Multiline = true;
            this.txtMessageHistory.Name = "txtMessageHistory";
            this.txtMessageHistory.Size = new System.Drawing.Size(1004, 499);
            this.txtMessageHistory.TabIndex = 0;
            // 
            // txt_SendText
            // 
            this.txt_SendText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txt_SendText.Location = new System.Drawing.Point(12, 628);
            this.txt_SendText.Multiline = true;
            this.txt_SendText.Name = "txt_SendText";
            this.txt_SendText.Size = new System.Drawing.Size(894, 57);
            this.txt_SendText.TabIndex = 1;
            // 
            // btnSentText
            // 
            this.btnSentText.Depth = 0;
            this.btnSentText.Location = new System.Drawing.Point(912, 628);
            this.btnSentText.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnSentText.Name = "btnSentText";
            this.btnSentText.Primary = true;
            this.btnSentText.Size = new System.Drawing.Size(94, 57);
            this.btnSentText.TabIndex = 2;
            this.btnSentText.Text = "Send";
            this.btnSentText.UseVisualStyleBackColor = true;
            this.btnSentText.Click += new System.EventHandler(this.btnSentText_Click);
            // 
            // UserChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 697);
            this.Controls.Add(this.btnSentText);
            this.Controls.Add(this.txt_SendText);
            this.Controls.Add(this.txtMessageHistory);
            this.Name = "UserChat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ClientName";
            this.Load += new System.EventHandler(this.UserChat_LoadAsync);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox txtMessageHistory;
        private TextBox txt_SendText;
        private MaterialRaisedButton btnSentText;
    }
}