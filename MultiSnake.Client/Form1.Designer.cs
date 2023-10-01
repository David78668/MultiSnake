namespace MultiSnake.Client
{
    partial class Form1
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
			this.messageBox = new System.Windows.Forms.TextBox();
			this.usernameBox = new System.Windows.Forms.TextBox();
			this.chatBox = new System.Windows.Forms.ListBox();
			this.sendButton = new System.Windows.Forms.Button();
			this.scoreBoardLB = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// messageBox
			// 
			this.messageBox.Location = new System.Drawing.Point(745, 398);
			this.messageBox.Name = "messageBox";
			this.messageBox.Size = new System.Drawing.Size(248, 27);
			this.messageBox.TabIndex = 0;
			// 
			// usernameBox
			// 
			this.usernameBox.Location = new System.Drawing.Point(745, 13);
			this.usernameBox.Name = "usernameBox";
			this.usernameBox.Size = new System.Drawing.Size(248, 27);
			this.usernameBox.TabIndex = 1;
			// 
			// chatBox
			// 
			this.chatBox.FormattingEnabled = true;
			this.chatBox.ItemHeight = 20;
			this.chatBox.Location = new System.Drawing.Point(745, 46);
			this.chatBox.Name = "chatBox";
			this.chatBox.Size = new System.Drawing.Size(248, 344);
			this.chatBox.TabIndex = 2;
			// 
			// sendButton
			// 
			this.sendButton.Location = new System.Drawing.Point(745, 431);
			this.sendButton.Name = "sendButton";
			this.sendButton.Size = new System.Drawing.Size(248, 29);
			this.sendButton.TabIndex = 3;
			this.sendButton.Text = "Send";
			this.sendButton.UseVisualStyleBackColor = true;
			this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
			// 
			// scoreBoardLB
			// 
			this.scoreBoardLB.AutoSize = true;
			this.scoreBoardLB.Location = new System.Drawing.Point(12, 485);
			this.scoreBoardLB.Name = "scoreBoardLB";
			this.scoreBoardLB.Size = new System.Drawing.Size(104, 20);
			this.scoreBoardLB.TabIndex = 4;
			this.scoreBoardLB.Text = "SCOREBOARD";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1007, 514);
			this.Controls.Add(this.scoreBoardLB);
			this.Controls.Add(this.sendButton);
			this.Controls.Add(this.chatBox);
			this.Controls.Add(this.usernameBox);
			this.Controls.Add(this.messageBox);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "Form1";
			this.Text = "Form1";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

		#endregion

		private TextBox messageBox;
		private TextBox usernameBox;
		private ListBox chatBox;
		private Button sendButton;
		private Label scoreBoardLB;
	}
}