
namespace CPDataxtendScheduler
{
    partial class frmMain
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvReplicationProgress = new System.Windows.Forms.DataGridView();
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.dataxtendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbNextSite = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nudMaxSitesToReplicate = new System.Windows.Forms.NumericUpDown();
            this.txtTelnet = new System.Windows.Forms.TextBox();
            this.dataGridViewProgressColumn1 = new CPDataxtendScheduler.DataGridViewProgressColumn();
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.lblStats = new System.Windows.Forms.Label();
            this.chkPriorityReplications = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.chkReplicationDetails = new System.Windows.Forms.CheckBox();
            this.tReplicateNewSite = new System.Windows.Forms.Timer(this.components);
            this.tPriority = new System.Windows.Forms.Timer(this.components);
            this.tReconnect = new System.Windows.Forms.Timer(this.components);
            this.CSessionID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSite = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CElapsedTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CHost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CPercent = new CPDataxtendScheduler.DataGridViewProgressColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReplicationProgress)).BeginInit();
            this.msMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxSitesToReplicate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvReplicationProgress
            // 
            this.dgvReplicationProgress.AllowUserToAddRows = false;
            this.dgvReplicationProgress.AllowUserToDeleteRows = false;
            this.dgvReplicationProgress.AllowUserToResizeColumns = false;
            this.dgvReplicationProgress.AllowUserToResizeRows = false;
            this.dgvReplicationProgress.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReplicationProgress.BackgroundColor = System.Drawing.Color.White;
            this.dgvReplicationProgress.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReplicationProgress.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CSessionID,
            this.cSite,
            this.CElapsedTime,
            this.CHost,
            this.CPercent});
            this.dgvReplicationProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReplicationProgress.Location = new System.Drawing.Point(0, 0);
            this.dgvReplicationProgress.Margin = new System.Windows.Forms.Padding(2);
            this.dgvReplicationProgress.MultiSelect = false;
            this.dgvReplicationProgress.Name = "dgvReplicationProgress";
            this.dgvReplicationProgress.ReadOnly = true;
            this.dgvReplicationProgress.RowHeadersVisible = false;
            this.dgvReplicationProgress.RowHeadersWidth = 51;
            this.dgvReplicationProgress.RowTemplate.Height = 24;
            this.dgvReplicationProgress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvReplicationProgress.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReplicationProgress.ShowCellErrors = false;
            this.dgvReplicationProgress.ShowCellToolTips = false;
            this.dgvReplicationProgress.ShowEditingIcon = false;
            this.dgvReplicationProgress.ShowRowErrors = false;
            this.dgvReplicationProgress.Size = new System.Drawing.Size(984, 250);
            this.dgvReplicationProgress.TabIndex = 2;
            this.dgvReplicationProgress.TabStop = false;
            this.dgvReplicationProgress.SelectionChanged += new System.EventHandler(this.dgvReplicationProgress_SelectionChanged);
            // 
            // msMain
            // 
            this.msMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile,
            this.dataxtendToolStripMenuItem});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.msMain.Size = new System.Drawing.Size(984, 24);
            this.msMain.TabIndex = 3;
            this.msMain.Text = "menuStrip1";
            // 
            // tsmiFile
            // 
            this.tsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOptions,
            this.tsmiExit});
            this.tsmiFile.Name = "tsmiFile";
            this.tsmiFile.Size = new System.Drawing.Size(37, 20);
            this.tsmiFile.Text = "&File";
            // 
            // tsmiOptions
            // 
            this.tsmiOptions.Name = "tsmiOptions";
            this.tsmiOptions.Size = new System.Drawing.Size(116, 22);
            this.tsmiOptions.Text = "Options";
            this.tsmiOptions.Click += new System.EventHandler(this.tsmiOptions_Click);
            // 
            // tsmiExit
            // 
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.Size = new System.Drawing.Size(116, 22);
            this.tsmiExit.Text = "Exit";
            this.tsmiExit.Click += new System.EventHandler(this.tsmiExit_Click);
            // 
            // dataxtendToolStripMenuItem
            // 
            this.dataxtendToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem});
            this.dataxtendToolStripMenuItem.Name = "dataxtendToolStripMenuItem";
            this.dataxtendToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.dataxtendToolStripMenuItem.Text = "Dataxtend";
            this.dataxtendToolStripMenuItem.Click += new System.EventHandler(this.dataxtendToolStripMenuItem_Click);
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Enabled = false;
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Enabled = false;
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // lbNextSite
            // 
            this.tlpMain.SetColumnSpan(this.lbNextSite, 2);
            this.lbNextSite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbNextSite.FormattingEnabled = true;
            this.lbNextSite.Location = new System.Drawing.Point(22, 28);
            this.lbNextSite.Margin = new System.Windows.Forms.Padding(2);
            this.lbNextSite.Name = "lbNextSite";
            this.lbNextSite.Size = new System.Drawing.Size(311, 190);
            this.lbNextSite.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(357, 6);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Sites to Replicate:";
            // 
            // nudMaxSitesToReplicate
            // 
            this.nudMaxSitesToReplicate.Location = new System.Drawing.Point(455, 3);
            this.nudMaxSitesToReplicate.Name = "nudMaxSitesToReplicate";
            this.nudMaxSitesToReplicate.Size = new System.Drawing.Size(120, 20);
            this.nudMaxSitesToReplicate.TabIndex = 9;
            this.nudMaxSitesToReplicate.TabStop = false;
            this.nudMaxSitesToReplicate.ValueChanged += new System.EventHandler(this.nudMaxSitesToReplicate_ValueChanged);
            // 
            // txtTelnet
            // 
            this.tlpMain.SetColumnSpan(this.txtTelnet, 4);
            this.txtTelnet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTelnet.Location = new System.Drawing.Point(358, 29);
            this.txtTelnet.Multiline = true;
            this.txtTelnet.Name = "txtTelnet";
            this.txtTelnet.ReadOnly = true;
            this.txtTelnet.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTelnet.Size = new System.Drawing.Size(603, 188);
            this.txtTelnet.TabIndex = 10;
            // 
            // dataGridViewProgressColumn1
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Red;
            this.dataGridViewProgressColumn1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewProgressColumn1.HeaderText = "Progress [%]";
            this.dataGridViewProgressColumn1.Name = "dataGridViewProgressColumn1";
            this.dataGridViewProgressColumn1.ProgressBarColor = System.Drawing.Color.Black;
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.Location = new System.Drawing.Point(0, 24);
            this.scMain.Name = "scMain";
            this.scMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.dgvReplicationProgress);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.tlpMain);
            this.scMain.Size = new System.Drawing.Size(984, 504);
            this.scMain.SplitterDistance = 250;
            this.scMain.TabIndex = 11;
            this.scMain.TabStop = false;
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 10;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.Controls.Add(this.txtTelnet, 5, 1);
            this.tlpMain.Controls.Add(this.lbNextSite, 1, 1);
            this.tlpMain.Controls.Add(this.nudMaxSitesToReplicate, 6, 0);
            this.tlpMain.Controls.Add(this.label2, 5, 0);
            this.tlpMain.Controls.Add(this.lblStats, 7, 0);
            this.tlpMain.Controls.Add(this.chkPriorityReplications, 8, 0);
            this.tlpMain.Controls.Add(this.label1, 1, 0);
            this.tlpMain.Controls.Add(this.btnClear, 5, 2);
            this.tlpMain.Controls.Add(this.chkReplicationDetails, 6, 2);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 3;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpMain.Size = new System.Drawing.Size(984, 250);
            this.tlpMain.TabIndex = 11;
            // 
            // lblStats
            // 
            this.lblStats.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblStats.AutoSize = true;
            this.lblStats.Location = new System.Drawing.Point(581, 6);
            this.lblStats.Name = "lblStats";
            this.lblStats.Size = new System.Drawing.Size(175, 13);
            this.lblStats.TabIndex = 11;
            this.lblStats.Text = "Sites Replicating: 0 Sites Waiting: 0";
            // 
            // chkPriorityReplications
            // 
            this.chkPriorityReplications.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.chkPriorityReplications.AutoSize = true;
            this.chkPriorityReplications.Location = new System.Drawing.Point(762, 3);
            this.chkPriorityReplications.Name = "chkPriorityReplications";
            this.chkPriorityReplications.Size = new System.Drawing.Size(155, 20);
            this.chkPriorityReplications.TabIndex = 13;
            this.chkPriorityReplications.TabStop = false;
            this.chkPriorityReplications.Text = "Accept Priority Replications";
            this.chkPriorityReplications.UseVisualStyleBackColor = true;
            this.chkPriorityReplications.CheckedChanged += new System.EventHandler(this.chkPriorityReplications_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.tlpMain.SetColumnSpan(this.label1, 2);
            this.label1.Location = new System.Drawing.Point(23, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(309, 26);
            this.label1.TabIndex = 14;
            this.label1.Text = "Site Priority List";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClear
            // 
            this.btnClear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClear.Location = new System.Drawing.Point(358, 223);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(91, 24);
            this.btnClear.TabIndex = 12;
            this.btnClear.TabStop = false;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // chkReplicationDetails
            // 
            this.chkReplicationDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.chkReplicationDetails.AutoSize = true;
            this.tlpMain.SetColumnSpan(this.chkReplicationDetails, 2);
            this.chkReplicationDetails.Location = new System.Drawing.Point(455, 223);
            this.chkReplicationDetails.Name = "chkReplicationDetails";
            this.chkReplicationDetails.Size = new System.Drawing.Size(114, 24);
            this.chkReplicationDetails.TabIndex = 15;
            this.chkReplicationDetails.TabStop = false;
            this.chkReplicationDetails.Text = "Replication Details";
            this.chkReplicationDetails.UseVisualStyleBackColor = true;
            // 
            // tReplicateNewSite
            // 
            this.tReplicateNewSite.Interval = 5000;
            this.tReplicateNewSite.Tick += new System.EventHandler(this.tReplicateNewSite_Tick);
            // 
            // tPriority
            // 
            this.tPriority.Tick += new System.EventHandler(this.tPriority_Tick);
            // 
            // tReconnect
            // 
            this.tReconnect.Interval = 10000;
            this.tReconnect.Tick += new System.EventHandler(this.tReconnect_Tick);
            // 
            // CSessionID
            // 
            this.CSessionID.FillWeight = 27.91878F;
            this.CSessionID.HeaderText = "SessionID";
            this.CSessionID.Name = "CSessionID";
            this.CSessionID.ReadOnly = true;
            this.CSessionID.Visible = false;
            // 
            // cSite
            // 
            this.cSite.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cSite.FillWeight = 49.24873F;
            this.cSite.HeaderText = "Partner Site";
            this.cSite.MinimumWidth = 6;
            this.cSite.Name = "cSite";
            this.cSite.ReadOnly = true;
            // 
            // CElapsedTime
            // 
            this.CElapsedTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CElapsedTime.HeaderText = "Elapsed Time";
            this.CElapsedTime.MinimumWidth = 50;
            this.CElapsedTime.Name = "CElapsedTime";
            this.CElapsedTime.ReadOnly = true;
            this.CElapsedTime.Width = 96;
            // 
            // CHost
            // 
            this.CHost.HeaderText = "Host";
            this.CHost.Name = "CHost";
            this.CHost.ReadOnly = true;
            this.CHost.Visible = false;
            // 
            // CPercent
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Red;
            this.CPercent.DefaultCellStyle = dataGridViewCellStyle1;
            this.CPercent.FillWeight = 32.83249F;
            this.CPercent.HeaderText = "";
            this.CPercent.Name = "CPercent";
            this.CPercent.ProgressBarColor = System.Drawing.Color.Black;
            this.CPercent.ReadOnly = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 528);
            this.Controls.Add(this.scMain);
            this.Controls.Add(this.msMain);
            this.MainMenuStrip = this.msMain;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmMain";
            this.Text = "CP Dataxtend Scheduler";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReplicationProgress)).EndInit();
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxSitesToReplicate)).EndInit();
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvReplicationProgress;
        private System.Windows.Forms.MenuStrip msMain;
        private System.Windows.Forms.ToolStripMenuItem tsmiFile;
        private System.Windows.Forms.ListBox lbNextSite;
        private System.Windows.Forms.ToolStripMenuItem tsmiOptions;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        private System.Windows.Forms.ToolStripMenuItem dataxtendToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudMaxSitesToReplicate;
        private System.Windows.Forms.TextBox txtTelnet;
        private DataGridViewProgressColumn dataGridViewProgressColumn1;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.SplitContainer scMain;
        private System.Windows.Forms.Label lblStats;
        private System.Windows.Forms.Timer tReplicateNewSite;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Timer tPriority;
        private System.Windows.Forms.Timer tReconnect;
        private System.Windows.Forms.CheckBox chkPriorityReplications;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkReplicationDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn CSessionID;
        private System.Windows.Forms.DataGridViewTextBoxColumn cSite;
        private System.Windows.Forms.DataGridViewTextBoxColumn CElapsedTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn CHost;
        private DataGridViewProgressColumn CPercent;
    }
}

