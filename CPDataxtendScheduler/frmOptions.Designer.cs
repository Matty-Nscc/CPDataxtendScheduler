
namespace CPDataxtendScheduler
{
    partial class frmOptions
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpDatabase = new System.Windows.Forms.TabPage();
            this.lblDatabase = new System.Windows.Forms.Label();
            this.cbDatabase = new System.Windows.Forms.ComboBox();
            this.lblServer = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.mtxtPassword = new System.Windows.Forms.MaskedTextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.chkWindowsAuthentication = new System.Windows.Forms.CheckBox();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.tpDataxtend = new System.Windows.Forms.TabPage();
            this.lblPDName = new System.Windows.Forms.Label();
            this.txtPDName = new System.Windows.Forms.TextBox();
            this.lblDataxtendPort = new System.Windows.Forms.Label();
            this.txtDataxtendPort = new System.Windows.Forms.TextBox();
            this.txtDataxtendIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tpOptions = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.nudDefaultSites2Rep = new System.Windows.Forms.NumericUpDown();
            this.chkReplicatioonDetails = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tpDatabase.SuspendLayout();
            this.tpDataxtend.SuspendLayout();
            this.tpOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDefaultSites2Rep)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpDatabase);
            this.tabControl1.Controls.Add(this.tpDataxtend);
            this.tabControl1.Controls.Add(this.tpOptions);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(261, 326);
            this.tabControl1.TabIndex = 0;
            // 
            // tpDatabase
            // 
            this.tpDatabase.BackColor = System.Drawing.SystemColors.Control;
            this.tpDatabase.Controls.Add(this.lblDatabase);
            this.tpDatabase.Controls.Add(this.cbDatabase);
            this.tpDatabase.Controls.Add(this.lblServer);
            this.tpDatabase.Controls.Add(this.txtServer);
            this.tpDatabase.Controls.Add(this.mtxtPassword);
            this.tpDatabase.Controls.Add(this.lblPassword);
            this.tpDatabase.Controls.Add(this.txtUserName);
            this.tpDatabase.Controls.Add(this.lblUserName);
            this.tpDatabase.Controls.Add(this.chkWindowsAuthentication);
            this.tpDatabase.Controls.Add(this.btnTestConnection);
            this.tpDatabase.Location = new System.Drawing.Point(4, 22);
            this.tpDatabase.Margin = new System.Windows.Forms.Padding(2);
            this.tpDatabase.Name = "tpDatabase";
            this.tpDatabase.Size = new System.Drawing.Size(253, 300);
            this.tpDatabase.TabIndex = 0;
            this.tpDatabase.Text = "Database";
            // 
            // lblDatabase
            // 
            this.lblDatabase.AutoSize = true;
            this.lblDatabase.Location = new System.Drawing.Point(9, 102);
            this.lblDatabase.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.Size = new System.Drawing.Size(56, 13);
            this.lblDatabase.TabIndex = 9;
            this.lblDatabase.Text = "Database:";
            // 
            // cbDatabase
            // 
            this.cbDatabase.FormattingEnabled = true;
            this.cbDatabase.Location = new System.Drawing.Point(89, 100);
            this.cbDatabase.Margin = new System.Windows.Forms.Padding(2);
            this.cbDatabase.Name = "cbDatabase";
            this.cbDatabase.Size = new System.Drawing.Size(152, 21);
            this.cbDatabase.TabIndex = 4;
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(9, 12);
            this.lblServer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(41, 13);
            this.lblServer.TabIndex = 7;
            this.lblServer.Text = "Server:";
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(89, 10);
            this.txtServer.Margin = new System.Windows.Forms.Padding(2);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(152, 20);
            this.txtServer.TabIndex = 0;
            // 
            // mtxtPassword
            // 
            this.mtxtPassword.Location = new System.Drawing.Point(89, 55);
            this.mtxtPassword.Margin = new System.Windows.Forms.Padding(2);
            this.mtxtPassword.Name = "mtxtPassword";
            this.mtxtPassword.PasswordChar = '*';
            this.mtxtPassword.Size = new System.Drawing.Size(152, 20);
            this.mtxtPassword.TabIndex = 2;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(9, 58);
            this.lblPassword.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 13);
            this.lblPassword.TabIndex = 4;
            this.lblPassword.Text = "Password:";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(89, 32);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(2);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(152, 20);
            this.txtUserName.TabIndex = 1;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(9, 35);
            this.lblUserName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(61, 13);
            this.lblUserName.TabIndex = 2;
            this.lblUserName.Text = "User name:";
            // 
            // chkWindowsAuthentication
            // 
            this.chkWindowsAuthentication.AutoSize = true;
            this.chkWindowsAuthentication.Location = new System.Drawing.Point(89, 78);
            this.chkWindowsAuthentication.Margin = new System.Windows.Forms.Padding(2);
            this.chkWindowsAuthentication.Name = "chkWindowsAuthentication";
            this.chkWindowsAuthentication.Size = new System.Drawing.Size(141, 17);
            this.chkWindowsAuthentication.TabIndex = 3;
            this.chkWindowsAuthentication.Text = "Windows Authentication";
            this.chkWindowsAuthentication.UseVisualStyleBackColor = true;
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.Location = new System.Drawing.Point(89, 124);
            this.btnTestConnection.Margin = new System.Windows.Forms.Padding(2);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(152, 22);
            this.btnTestConnection.TabIndex = 5;
            this.btnTestConnection.Text = "Test Connection";
            this.btnTestConnection.UseVisualStyleBackColor = true;
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // tpDataxtend
            // 
            this.tpDataxtend.BackColor = System.Drawing.SystemColors.Control;
            this.tpDataxtend.Controls.Add(this.lblPDName);
            this.tpDataxtend.Controls.Add(this.txtPDName);
            this.tpDataxtend.Controls.Add(this.lblDataxtendPort);
            this.tpDataxtend.Controls.Add(this.txtDataxtendPort);
            this.tpDataxtend.Controls.Add(this.txtDataxtendIP);
            this.tpDataxtend.Controls.Add(this.label1);
            this.tpDataxtend.Location = new System.Drawing.Point(4, 22);
            this.tpDataxtend.Margin = new System.Windows.Forms.Padding(2);
            this.tpDataxtend.Name = "tpDataxtend";
            this.tpDataxtend.Size = new System.Drawing.Size(253, 300);
            this.tpDataxtend.TabIndex = 1;
            this.tpDataxtend.Text = "Dataxtend";
            // 
            // lblPDName
            // 
            this.lblPDName.AutoSize = true;
            this.lblPDName.Location = new System.Drawing.Point(9, 59);
            this.lblPDName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPDName.Name = "lblPDName";
            this.lblPDName.Size = new System.Drawing.Size(56, 13);
            this.lblPDName.TabIndex = 13;
            this.lblPDName.Text = "PD Name:";
            // 
            // txtPDName
            // 
            this.txtPDName.Location = new System.Drawing.Point(89, 56);
            this.txtPDName.Margin = new System.Windows.Forms.Padding(2);
            this.txtPDName.Name = "txtPDName";
            this.txtPDName.Size = new System.Drawing.Size(152, 20);
            this.txtPDName.TabIndex = 2;
            // 
            // lblDataxtendPort
            // 
            this.lblDataxtendPort.AutoSize = true;
            this.lblDataxtendPort.Location = new System.Drawing.Point(9, 35);
            this.lblDataxtendPort.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDataxtendPort.Name = "lblDataxtendPort";
            this.lblDataxtendPort.Size = new System.Drawing.Size(29, 13);
            this.lblDataxtendPort.TabIndex = 11;
            this.lblDataxtendPort.Text = "Port:";
            // 
            // txtDataxtendPort
            // 
            this.txtDataxtendPort.Location = new System.Drawing.Point(89, 32);
            this.txtDataxtendPort.Margin = new System.Windows.Forms.Padding(2);
            this.txtDataxtendPort.Name = "txtDataxtendPort";
            this.txtDataxtendPort.Size = new System.Drawing.Size(152, 20);
            this.txtDataxtendPort.TabIndex = 1;
            // 
            // txtDataxtendIP
            // 
            this.txtDataxtendIP.Location = new System.Drawing.Point(89, 10);
            this.txtDataxtendIP.Margin = new System.Windows.Forms.Padding(2);
            this.txtDataxtendIP.Name = "txtDataxtendIP";
            this.txtDataxtendIP.Size = new System.Drawing.Size(152, 20);
            this.txtDataxtendIP.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "IP:";
            // 
            // tpOptions
            // 
            this.tpOptions.BackColor = System.Drawing.SystemColors.Control;
            this.tpOptions.Controls.Add(this.chkReplicatioonDetails);
            this.tpOptions.Controls.Add(this.nudDefaultSites2Rep);
            this.tpOptions.Controls.Add(this.label2);
            this.tpOptions.Location = new System.Drawing.Point(4, 22);
            this.tpOptions.Margin = new System.Windows.Forms.Padding(2);
            this.tpOptions.Name = "tpOptions";
            this.tpOptions.Size = new System.Drawing.Size(253, 300);
            this.tpOptions.TabIndex = 2;
            this.tpOptions.Text = "Options";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnCancel);
            this.splitContainer1.Panel2.Controls.Add(this.btnOK);
            this.splitContainer1.Size = new System.Drawing.Size(261, 366);
            this.splitContainer1.SplitterDistance = 326;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(76, 2);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(69, 24);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(3, 2);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(69, 24);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Default Sites to Replicate";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // nudDefaultSites2Rep
            // 
            this.nudDefaultSites2Rep.Location = new System.Drawing.Point(142, 10);
            this.nudDefaultSites2Rep.Name = "nudDefaultSites2Rep";
            this.nudDefaultSites2Rep.Size = new System.Drawing.Size(99, 20);
            this.nudDefaultSites2Rep.TabIndex = 1;
            // 
            // chkReplicatioonDetails
            // 
            this.chkReplicatioonDetails.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chkReplicatioonDetails.AutoSize = true;
            this.chkReplicatioonDetails.Location = new System.Drawing.Point(27, 36);
            this.chkReplicatioonDetails.Name = "chkReplicatioonDetails";
            this.chkReplicatioonDetails.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkReplicatioonDetails.Size = new System.Drawing.Size(114, 17);
            this.chkReplicatioonDetails.TabIndex = 16;
            this.chkReplicatioonDetails.TabStop = false;
            this.chkReplicatioonDetails.Text = "Replication Details";
            this.chkReplicatioonDetails.UseVisualStyleBackColor = true;
            // 
            // frmOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(261, 366);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOptions";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Options";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmOptions_Load);
            this.tabControl1.ResumeLayout(false);
            this.tpDatabase.ResumeLayout(false);
            this.tpDatabase.PerformLayout();
            this.tpDataxtend.ResumeLayout(false);
            this.tpDataxtend.PerformLayout();
            this.tpOptions.ResumeLayout(false);
            this.tpOptions.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudDefaultSites2Rep)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpDatabase;
        private System.Windows.Forms.TabPage tpDataxtend;
        private System.Windows.Forms.TabPage tpOptions;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblDatabase;
        private System.Windows.Forms.ComboBox cbDatabase;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.MaskedTextBox mtxtPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.CheckBox chkWindowsAuthentication;
        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.Label lblDataxtendPort;
        private System.Windows.Forms.TextBox txtDataxtendPort;
        private System.Windows.Forms.TextBox txtDataxtendIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPDName;
        private System.Windows.Forms.TextBox txtPDName;
        private System.Windows.Forms.NumericUpDown nudDefaultSites2Rep;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkReplicatioonDetails;
    }
}