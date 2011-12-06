using System;

namespace EpisodeRenamer
{
	partial class MainForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.txtNameFile = new System.Windows.Forms.TextBox();
			this.txtEpisodeFolder = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.lblChooseFirst = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.lblEditWrong = new System.Windows.Forms.Label();
			this.btnChooseNameFile = new System.Windows.Forms.Button();
			this.btnChooseFolder = new System.Windows.Forms.Button();
			this.btnPasteNames = new System.Windows.Forms.Button();
			this.btnReadFiles = new System.Windows.Forms.Button();
			this.btnReadNames = new System.Windows.Forms.Button();
			this.chkMonitorClipboard = new System.Windows.Forms.CheckBox();
			this.openNameFile = new System.Windows.Forms.OpenFileDialog();
			this.dataGridView = new System.Windows.Forms.DataGridView();
			this.btnRename = new System.Windows.Forms.Button();
			this.btnSetPrefix = new System.Windows.Forms.Button();
			this.openFolder = new System.Windows.Forms.FolderBrowserDialog();
			this.btnEditClipboardData = new System.Windows.Forms.Button();
			this.btnSaveClipboardData = new System.Windows.Forms.Button();
			this.saveClipboardData = new System.Windows.Forms.SaveFileDialog();
			this.chkUseFolderName = new System.Windows.Forms.CheckBox();
			this.grpReplace = new System.Windows.Forms.GroupBox();
			this.chkIgnoreCase = new System.Windows.Forms.CheckBox();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtReplace = new System.Windows.Forms.TextBox();
			this.txtSearch = new System.Windows.Forms.TextBox();
			this.chkPostReplace = new System.Windows.Forms.CheckBox();
			this.enabledDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.oldFilenameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.newFilenameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.newNameStringDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.episodes = new System.Windows.Forms.BindingSource(this.components);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
			this.grpReplace.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.episodes)).BeginInit();
			this.SuspendLayout();
			// 
			// txtNameFile
			// 
			this.txtNameFile.AllowDrop = true;
			this.txtNameFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtNameFile.Location = new System.Drawing.Point(12, 64);
			this.txtNameFile.Name = "txtNameFile";
			this.txtNameFile.Size = new System.Drawing.Size(779, 20);
			this.txtNameFile.TabIndex = 2;
			this.txtNameFile.TextChanged += new System.EventHandler(this.txtNameFile_TextChanged);
			this.txtNameFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.txt_DragDrop);
			this.txtNameFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.txt_DragEnter);
			this.txtNameFile.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNameFile_KeyDown);
			// 
			// txtEpisodeFolder
			// 
			this.txtEpisodeFolder.AllowDrop = true;
			this.txtEpisodeFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtEpisodeFolder.Location = new System.Drawing.Point(12, 25);
			this.txtEpisodeFolder.Name = "txtEpisodeFolder";
			this.txtEpisodeFolder.Size = new System.Drawing.Size(779, 20);
			this.txtEpisodeFolder.TabIndex = 0;
			this.txtEpisodeFolder.TextChanged += new System.EventHandler(this.txtEpisodeFolder_TextChanged);
			this.txtEpisodeFolder.DragDrop += new System.Windows.Forms.DragEventHandler(this.txt_DragDrop);
			this.txtEpisodeFolder.DragEnter += new System.Windows.Forms.DragEventHandler(this.txt_DragEnter);
			this.txtEpisodeFolder.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEpisodeFolder_KeyDown);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 48);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(152, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "File containing episode names:";
			// 
			// lblChooseFirst
			// 
			this.lblChooseFirst.AutoSize = true;
			this.lblChooseFirst.Cursor = System.Windows.Forms.Cursors.Help;
			this.lblChooseFirst.Location = new System.Drawing.Point(12, 125);
			this.lblChooseFirst.Name = "lblChooseFirst";
			this.lblChooseFirst.Size = new System.Drawing.Size(200, 13);
			this.lblChooseFirst.TabIndex = 3;
			this.lblChooseFirst.Text = "Choose episode folder and name file first.";
			this.lblChooseFirst.Click += new System.EventHandler(this.lblChooseFirst_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(152, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Folder containing episode files:";
			// 
			// lblEditWrong
			// 
			this.lblEditWrong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblEditWrong.AutoSize = true;
			this.lblEditWrong.Cursor = System.Windows.Forms.Cursors.Help;
			this.lblEditWrong.Location = new System.Drawing.Point(648, 125);
			this.lblEditWrong.Name = "lblEditWrong";
			this.lblEditWrong.Size = new System.Drawing.Size(224, 13);
			this.lblEditWrong.TabIndex = 5;
			this.lblEditWrong.Text = "Edit wrong names, then click Rename to start.";
			this.lblEditWrong.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.lblEditWrong.Visible = false;
			this.lblEditWrong.Click += new System.EventHandler(this.lblEditWrong_Click);
			// 
			// btnChooseNameFile
			// 
			this.btnChooseNameFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnChooseNameFile.Location = new System.Drawing.Point(797, 62);
			this.btnChooseNameFile.Name = "btnChooseNameFile";
			this.btnChooseNameFile.Size = new System.Drawing.Size(75, 23);
			this.btnChooseNameFile.TabIndex = 3;
			this.btnChooseNameFile.Text = "Choose...";
			this.btnChooseNameFile.UseVisualStyleBackColor = true;
			this.btnChooseNameFile.Click += new System.EventHandler(this.btnChooseNameFile_Click);
			// 
			// btnChooseFolder
			// 
			this.btnChooseFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnChooseFolder.Location = new System.Drawing.Point(797, 23);
			this.btnChooseFolder.Name = "btnChooseFolder";
			this.btnChooseFolder.Size = new System.Drawing.Size(75, 23);
			this.btnChooseFolder.TabIndex = 1;
			this.btnChooseFolder.Text = "Choose...";
			this.btnChooseFolder.UseVisualStyleBackColor = true;
			this.btnChooseFolder.Click += new System.EventHandler(this.btnChooseFolder_Click);
			// 
			// btnPasteNames
			// 
			this.btnPasteNames.Location = new System.Drawing.Point(12, 90);
			this.btnPasteNames.Name = "btnPasteNames";
			this.btnPasteNames.Size = new System.Drawing.Size(160, 23);
			this.btnPasteNames.TabIndex = 4;
			this.btnPasteNames.Text = "Paste names from clipboard...";
			this.btnPasteNames.UseVisualStyleBackColor = true;
			this.btnPasteNames.Click += new System.EventHandler(this.btnPasteNames_Click);
			// 
			// btnReadFiles
			// 
			this.btnReadFiles.Location = new System.Drawing.Point(12, 145);
			this.btnReadFiles.Name = "btnReadFiles";
			this.btnReadFiles.Size = new System.Drawing.Size(140, 23);
			this.btnReadFiles.TabIndex = 6;
			this.btnReadFiles.Text = "Read original filenames";
			this.btnReadFiles.UseVisualStyleBackColor = true;
			this.btnReadFiles.Click += new System.EventHandler(this.btnReadFiles_Click);
			// 
			// btnReadNames
			// 
			this.btnReadNames.Enabled = false;
			this.btnReadNames.Location = new System.Drawing.Point(331, 145);
			this.btnReadNames.Name = "btnReadNames";
			this.btnReadNames.Size = new System.Drawing.Size(140, 23);
			this.btnReadNames.TabIndex = 8;
			this.btnReadNames.Text = "Read episode names";
			this.btnReadNames.UseVisualStyleBackColor = true;
			this.btnReadNames.Click += new System.EventHandler(this.btnReadNames_Click);
			// 
			// chkMonitorClipboard
			// 
			this.chkMonitorClipboard.AutoSize = true;
			this.chkMonitorClipboard.Location = new System.Drawing.Point(304, 94);
			this.chkMonitorClipboard.Name = "chkMonitorClipboard";
			this.chkMonitorClipboard.Size = new System.Drawing.Size(196, 17);
			this.chkMonitorClipboard.TabIndex = 5;
			this.chkMonitorClipboard.Text = "Monitor clipboard for episode names";
			this.chkMonitorClipboard.UseVisualStyleBackColor = true;
			this.chkMonitorClipboard.CheckedChanged += new System.EventHandler(this.chkMonitorClipboard_CheckedChanged);
			// 
			// openNameFile
			// 
			this.openNameFile.AddExtension = false;
			this.openNameFile.Filter = "Text Files (*.txt)|*.txt|All Files|*.*";
			this.openNameFile.SupportMultiDottedExtensions = true;
			this.openNameFile.Title = "Select episode name file";
			// 
			// dataGridView
			// 
			this.dataGridView.AllowUserToAddRows = false;
			this.dataGridView.AllowUserToResizeRows = false;
			this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView.AutoGenerateColumns = false;
			this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.enabledDataGridViewCheckBoxColumn,
            this.oldFilenameDataGridViewTextBoxColumn,
            this.newFilenameDataGridViewTextBoxColumn,
            this.newNameStringDataGridViewTextBoxColumn});
			this.dataGridView.DataSource = this.episodes;
			this.dataGridView.Location = new System.Drawing.Point(12, 277);
			this.dataGridView.MultiSelect = false;
			this.dataGridView.Name = "dataGridView";
			this.dataGridView.RowHeadersVisible = false;
			this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView.ShowEditingIcon = false;
			this.dataGridView.Size = new System.Drawing.Size(860, 263);
			this.dataGridView.TabIndex = 9;
			this.dataGridView.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridView_CurrentCellDirtyStateChanged);
			this.dataGridView.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView_RowPostPaint);
			this.dataGridView.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dataGridView_RowPrePaint);
			// 
			// btnRename
			// 
			this.btnRename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRename.Enabled = false;
			this.btnRename.Location = new System.Drawing.Point(797, 145);
			this.btnRename.Name = "btnRename";
			this.btnRename.Size = new System.Drawing.Size(75, 23);
			this.btnRename.TabIndex = 12;
			this.btnRename.Text = "Rename";
			this.btnRename.UseVisualStyleBackColor = true;
			this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
			// 
			// btnSetPrefix
			// 
			this.btnSetPrefix.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSetPrefix.Location = new System.Drawing.Point(573, 145);
			this.btnSetPrefix.Name = "btnSetPrefix";
			this.btnSetPrefix.Size = new System.Drawing.Size(92, 23);
			this.btnSetPrefix.TabIndex = 10;
			this.btnSetPrefix.Text = "Set prefixes...";
			this.btnSetPrefix.UseVisualStyleBackColor = true;
			this.btnSetPrefix.Click += new System.EventHandler(this.btnSetPrefix_Click);
			// 
			// openFolder
			// 
			this.openFolder.Description = "Select the folder containing the episode files.";
			this.openFolder.RootFolder = System.Environment.SpecialFolder.MyComputer;
			this.openFolder.ShowNewFolderButton = false;
			// 
			// btnEditClipboardData
			// 
			this.btnEditClipboardData.Enabled = false;
			this.btnEditClipboardData.Location = new System.Drawing.Point(178, 90);
			this.btnEditClipboardData.Name = "btnEditClipboardData";
			this.btnEditClipboardData.Size = new System.Drawing.Size(120, 23);
			this.btnEditClipboardData.TabIndex = 14;
			this.btnEditClipboardData.Text = "Edit clipboard data...";
			this.btnEditClipboardData.UseVisualStyleBackColor = true;
			this.btnEditClipboardData.Click += new System.EventHandler(this.btnEditClipboardData_Click);
			// 
			// btnSaveClipboardData
			// 
			this.btnSaveClipboardData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSaveClipboardData.Enabled = false;
			this.btnSaveClipboardData.Location = new System.Drawing.Point(671, 145);
			this.btnSaveClipboardData.Name = "btnSaveClipboardData";
			this.btnSaveClipboardData.Size = new System.Drawing.Size(120, 23);
			this.btnSaveClipboardData.TabIndex = 11;
			this.btnSaveClipboardData.Text = "Save clipboard data...";
			this.btnSaveClipboardData.UseVisualStyleBackColor = true;
			this.btnSaveClipboardData.Click += new System.EventHandler(this.btnSaveClipboardData_Click);
			// 
			// saveClipboardData
			// 
			this.saveClipboardData.DefaultExt = "txt";
			this.saveClipboardData.FileName = "Episode Names.txt";
			this.saveClipboardData.Filter = "Text files (*.txt)|*.txt|All files|*.*";
			this.saveClipboardData.SupportMultiDottedExtensions = true;
			this.saveClipboardData.Title = "Save clipboard data";
			// 
			// chkUseFolderName
			// 
			this.chkUseFolderName.AutoSize = true;
			this.chkUseFolderName.Location = new System.Drawing.Point(158, 149);
			this.chkUseFolderName.Name = "chkUseFolderName";
			this.chkUseFolderName.Size = new System.Drawing.Size(167, 17);
			this.chkUseFolderName.TabIndex = 7;
			this.chkUseFolderName.Text = "Use folder name for series title";
			this.chkUseFolderName.UseVisualStyleBackColor = true;
			this.chkUseFolderName.CheckedChanged += new System.EventHandler(this.chkUseFolderName_CheckedChanged);
			// 
			// grpReplace
			// 
			this.grpReplace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpReplace.Controls.Add(this.chkIgnoreCase);
			this.grpReplace.Controls.Add(this.linkLabel1);
			this.grpReplace.Controls.Add(this.label5);
			this.grpReplace.Controls.Add(this.label4);
			this.grpReplace.Controls.Add(this.label2);
			this.grpReplace.Controls.Add(this.txtReplace);
			this.grpReplace.Controls.Add(this.txtSearch);
			this.grpReplace.Location = new System.Drawing.Point(12, 174);
			this.grpReplace.Name = "grpReplace";
			this.grpReplace.Size = new System.Drawing.Size(860, 97);
			this.grpReplace.TabIndex = 13;
			this.grpReplace.TabStop = false;
			this.grpReplace.Text = "Post rename replace";
			// 
			// chkIgnoreCase
			// 
			this.chkIgnoreCase.AutoSize = true;
			this.chkIgnoreCase.Checked = true;
			this.chkIgnoreCase.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkIgnoreCase.Location = new System.Drawing.Point(377, 34);
			this.chkIgnoreCase.Name = "chkIgnoreCase";
			this.chkIgnoreCase.Size = new System.Drawing.Size(82, 17);
			this.chkIgnoreCase.TabIndex = 5;
			this.chkIgnoreCase.Text = "Ignore case";
			this.chkIgnoreCase.UseVisualStyleBackColor = true;
			// 
			// linkLabel1
			// 
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.LinkArea = new System.Windows.Forms.LinkArea(4, 4);
			this.linkLabel1.Location = new System.Drawing.Point(213, 12);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(246, 17);
			this.linkLabel1.TabIndex = 2;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "See here for information on regular expressions.";
			this.linkLabel1.UseCompatibleTextRendering = true;
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(465, 12);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(298, 78);
			this.label5.TabIndex = 4;
			this.label5.Text = resources.GetString("label5.Text");
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 55);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(64, 13);
			this.label4.TabIndex = 3;
			this.label4.Text = "Replace by:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(59, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Search for:";
			// 
			// txtReplace
			// 
			this.txtReplace.Location = new System.Drawing.Point(6, 71);
			this.txtReplace.Name = "txtReplace";
			this.txtReplace.Size = new System.Drawing.Size(453, 20);
			this.txtReplace.TabIndex = 1;
			this.txtReplace.TextChanged += new System.EventHandler(this.txtRegex_TextChanged);
			// 
			// txtSearch
			// 
			this.txtSearch.Location = new System.Drawing.Point(6, 32);
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.Size = new System.Drawing.Size(365, 20);
			this.txtSearch.TabIndex = 0;
			this.txtSearch.TextChanged += new System.EventHandler(this.txtRegex_TextChanged);
			// 
			// chkPostReplace
			// 
			this.chkPostReplace.AutoSize = true;
			this.chkPostReplace.Location = new System.Drawing.Point(331, 124);
			this.chkPostReplace.Name = "chkPostReplace";
			this.chkPostReplace.Size = new System.Drawing.Size(158, 17);
			this.chkPostReplace.TabIndex = 9;
			this.chkPostReplace.Text = "Enable post rename replace";
			this.chkPostReplace.UseVisualStyleBackColor = true;
			this.chkPostReplace.CheckedChanged += new System.EventHandler(this.chkPostReplace_CheckedChanged);
			// 
			// enabledDataGridViewCheckBoxColumn
			// 
			this.enabledDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.enabledDataGridViewCheckBoxColumn.DataPropertyName = "Enabled";
			this.enabledDataGridViewCheckBoxColumn.FalseValue = "false";
			this.enabledDataGridViewCheckBoxColumn.Frozen = true;
			this.enabledDataGridViewCheckBoxColumn.HeaderText = "Enabled";
			this.enabledDataGridViewCheckBoxColumn.MinimumWidth = 60;
			this.enabledDataGridViewCheckBoxColumn.Name = "enabledDataGridViewCheckBoxColumn";
			this.enabledDataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.enabledDataGridViewCheckBoxColumn.TrueValue = "true";
			this.enabledDataGridViewCheckBoxColumn.Width = 60;
			// 
			// oldFilenameDataGridViewTextBoxColumn
			// 
			this.oldFilenameDataGridViewTextBoxColumn.DataPropertyName = "OldFilename";
			this.oldFilenameDataGridViewTextBoxColumn.Frozen = true;
			this.oldFilenameDataGridViewTextBoxColumn.HeaderText = "Original Filename";
			this.oldFilenameDataGridViewTextBoxColumn.MinimumWidth = 110;
			this.oldFilenameDataGridViewTextBoxColumn.Name = "oldFilenameDataGridViewTextBoxColumn";
			this.oldFilenameDataGridViewTextBoxColumn.ReadOnly = true;
			this.oldFilenameDataGridViewTextBoxColumn.Width = 170;
			// 
			// newFilenameDataGridViewTextBoxColumn
			// 
			this.newFilenameDataGridViewTextBoxColumn.DataPropertyName = "NewFilename";
			this.newFilenameDataGridViewTextBoxColumn.Frozen = true;
			this.newFilenameDataGridViewTextBoxColumn.HeaderText = "New Filename";
			this.newFilenameDataGridViewTextBoxColumn.MinimumWidth = 100;
			this.newFilenameDataGridViewTextBoxColumn.Name = "newFilenameDataGridViewTextBoxColumn";
			this.newFilenameDataGridViewTextBoxColumn.Width = 300;
			// 
			// newNameStringDataGridViewTextBoxColumn
			// 
			this.newNameStringDataGridViewTextBoxColumn.DataPropertyName = "NewNameString";
			this.newNameStringDataGridViewTextBoxColumn.Frozen = true;
			this.newNameStringDataGridViewTextBoxColumn.HeaderText = "Data";
			this.newNameStringDataGridViewTextBoxColumn.MinimumWidth = 100;
			this.newNameStringDataGridViewTextBoxColumn.Name = "newNameStringDataGridViewTextBoxColumn";
			this.newNameStringDataGridViewTextBoxColumn.ReadOnly = true;
			this.newNameStringDataGridViewTextBoxColumn.ToolTipText = "The line of data from which the episode name was taken.";
			this.newNameStringDataGridViewTextBoxColumn.Width = 250;
			// 
			// episodes
			// 
			this.episodes.DataSource = typeof(EpisodeRenamer.EpisodeEntry);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(884, 552);
			this.Controls.Add(this.chkPostReplace);
			this.Controls.Add(this.grpReplace);
			this.Controls.Add(this.chkUseFolderName);
			this.Controls.Add(this.btnSaveClipboardData);
			this.Controls.Add(this.btnEditClipboardData);
			this.Controls.Add(this.btnSetPrefix);
			this.Controls.Add(this.btnRename);
			this.Controls.Add(this.dataGridView);
			this.Controls.Add(this.chkMonitorClipboard);
			this.Controls.Add(this.btnReadNames);
			this.Controls.Add(this.btnReadFiles);
			this.Controls.Add(this.btnPasteNames);
			this.Controls.Add(this.btnChooseFolder);
			this.Controls.Add(this.btnChooseNameFile);
			this.Controls.Add(this.lblEditWrong);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.lblChooseFirst);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtEpisodeFolder);
			this.Controls.Add(this.txtNameFile);
			this.MinimumSize = new System.Drawing.Size(850, 500);
			this.Name = "MainForm";
			this.Text = "EpisodeRenamer by Pezo";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
			this.grpReplace.ResumeLayout(false);
			this.grpReplace.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.episodes)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtNameFile;
		private System.Windows.Forms.TextBox txtEpisodeFolder;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblChooseFirst;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label lblEditWrong;
		private System.Windows.Forms.Button btnChooseNameFile;
		private System.Windows.Forms.Button btnChooseFolder;
		private System.Windows.Forms.Button btnPasteNames;
		private System.Windows.Forms.Button btnReadFiles;
		private System.Windows.Forms.Button btnReadNames;
		private System.Windows.Forms.CheckBox chkMonitorClipboard;
		private System.Windows.Forms.OpenFileDialog openNameFile;
		private System.Windows.Forms.DataGridView dataGridView;
		private System.Windows.Forms.Button btnRename;
		private System.Windows.Forms.Button btnSetPrefix;
		private System.Windows.Forms.FolderBrowserDialog openFolder;
		private System.Windows.Forms.Button btnEditClipboardData;
		private System.Windows.Forms.BindingSource episodes;
		private System.Windows.Forms.DataGridViewCheckBoxColumn enabledDataGridViewCheckBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn oldFilenameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn newFilenameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn newNameStringDataGridViewTextBoxColumn;
		private System.Windows.Forms.Button btnSaveClipboardData;
		private System.Windows.Forms.SaveFileDialog saveClipboardData;
		private System.Windows.Forms.CheckBox chkUseFolderName;
		private System.Windows.Forms.GroupBox grpReplace;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtReplace;
		private System.Windows.Forms.TextBox txtSearch;
		private System.Windows.Forms.CheckBox chkPostReplace;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.CheckBox chkIgnoreCase;
	}
}

