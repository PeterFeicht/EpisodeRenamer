namespace EpisodeRenamer
{
	partial class ChangePrefixForm
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
			if(disposing && (components != null))
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.txtSeasonPrefix = new System.Windows.Forms.TextBox();
			this.txtEpisodePrefix = new System.Windows.Forms.TextBox();
			this.txtSeparator = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.lblExample = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.numDigits = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.numDigits)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(260, 56);
			this.label1.TabIndex = 0;
			this.label1.Text = "Here you can change the prefixes for season and episode numbers as well as the se" +
				"parator used to separate the name of the show, the numbers and the name.";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 65);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(74, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Season prefix:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(118, 65);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(76, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Episode prefix:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 143);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(50, 13);
			this.label4.TabIndex = 3;
			this.label4.Text = "Example:";
			// 
			// btnOK
			// 
			this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(58, 201);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 4;
			this.btnOK.Text = "&OK";
			this.btnOK.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(151, 201);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// txtSeasonPrefix
			// 
			this.txtSeasonPrefix.Location = new System.Drawing.Point(12, 81);
			this.txtSeasonPrefix.Name = "txtSeasonPrefix";
			this.txtSeasonPrefix.Size = new System.Drawing.Size(100, 20);
			this.txtSeasonPrefix.TabIndex = 0;
			this.txtSeasonPrefix.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
			// 
			// txtEpisodePrefix
			// 
			this.txtEpisodePrefix.Location = new System.Drawing.Point(118, 81);
			this.txtEpisodePrefix.Name = "txtEpisodePrefix";
			this.txtEpisodePrefix.Size = new System.Drawing.Size(100, 20);
			this.txtEpisodePrefix.TabIndex = 1;
			this.txtEpisodePrefix.Text = "x";
			this.txtEpisodePrefix.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
			// 
			// txtSeparator
			// 
			this.txtSeparator.Location = new System.Drawing.Point(12, 120);
			this.txtSeparator.Name = "txtSeparator";
			this.txtSeparator.Size = new System.Drawing.Size(100, 20);
			this.txtSeparator.TabIndex = 2;
			this.txtSeparator.Text = " - ";
			this.txtSeparator.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(12, 104);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(56, 13);
			this.label5.TabIndex = 9;
			this.label5.Text = "Separator:";
			// 
			// lblExample
			// 
			this.lblExample.AutoSize = true;
			this.lblExample.Location = new System.Drawing.Point(12, 162);
			this.lblExample.Name = "lblExample";
			this.lblExample.Size = new System.Drawing.Size(150, 13);
			this.lblExample.TabIndex = 10;
			this.lblExample.Text = "Series - 01x05 - Episode name";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(118, 104);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(86, 13);
			this.label6.TabIndex = 11;
			this.label6.Text = "Number of digits:";
			// 
			// numDigits
			// 
			this.numDigits.Location = new System.Drawing.Point(118, 121);
			this.numDigits.Maximum = new decimal(new int[ ] {
            4,
            0,
            0,
            0});
			this.numDigits.Minimum = new decimal(new int[ ] {
            1,
            0,
            0,
            0});
			this.numDigits.Name = "numDigits";
			this.numDigits.Size = new System.Drawing.Size(100, 20);
			this.numDigits.TabIndex = 3;
			this.numDigits.Value = new decimal(new int[ ] {
            2,
            0,
            0,
            0});
			this.numDigits.ValueChanged += new System.EventHandler(this.numDigits_ValueChanged);
			// 
			// ChangePrefixForm
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(284, 236);
			this.Controls.Add(this.numDigits);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.lblExample);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.txtSeparator);
			this.Controls.Add(this.txtEpisodePrefix);
			this.Controls.Add(this.txtSeasonPrefix);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "ChangePrefixForm";
			this.ShowInTaskbar = false;
			this.Text = "Change prefixes";
			this.Load += new System.EventHandler(this.ChangePrefixForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.numDigits)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TextBox txtSeasonPrefix;
		private System.Windows.Forms.TextBox txtEpisodePrefix;
		private System.Windows.Forms.TextBox txtSeparator;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label lblExample;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.NumericUpDown numDigits;
	}
}