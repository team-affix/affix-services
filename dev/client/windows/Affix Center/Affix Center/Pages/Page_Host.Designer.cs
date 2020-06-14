namespace Affix_Center.Pages
{
    partial class Page_Host
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Drawing.StringFormat stringFormat1 = new System.Drawing.StringFormat();
            System.Drawing.StringFormat stringFormat2 = new System.Drawing.StringFormat();
            System.Drawing.StringFormat stringFormat3 = new System.Drawing.StringFormat();
            this.hdr = new Affix_Center.Dialog_Header();
            this.flpConnections = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlConnections = new Affix_Center.CustomControls.RoundedPanel();
            this.pnlConnectionInfo = new Affix_Center.CustomControls.RoundedPanel();
            this.txtNumberConnected = new System.Windows.Forms.TextBox();
            this.lblNumberConnected = new System.Windows.Forms.Label();
            this.txtNumberMembers = new System.Windows.Forms.TextBox();
            this.lblNumberMembers = new System.Windows.Forms.Label();
            this.pnlInfo = new Affix_Center.CustomControls.RoundedPanel();
            this.pnlServerInfo2 = new Affix_Center.CustomControls.RoundedPanel();
            this.pnlToggleFileStorageLoading = new Affix_Center.CustomControls.RoundedPanel();
            this.btnToggleFileStorage = new Affix_Center.CustomControls.RoundedButton();
            this.pnlToggleMessagingLoading = new Affix_Center.CustomControls.RoundedPanel();
            this.btnToggleMessaging = new Affix_Center.CustomControls.RoundedButton();
            this.pnlServerInfo1 = new Affix_Center.CustomControls.RoundedPanel();
            this.txtCompatibility = new System.Windows.Forms.TextBox();
            this.lblCompatibility = new System.Windows.Forms.Label();
            this.txtNatType = new System.Windows.Forms.TextBox();
            this.lblNatType = new System.Windows.Forms.Label();
            this.txtIPv4 = new System.Windows.Forms.TextBox();
            this.lblIPv4 = new System.Windows.Forms.Label();
            this.pnlServerInfo0 = new Affix_Center.CustomControls.RoundedPanel();
            this.pnlToggleServerLoading = new Affix_Center.CustomControls.RoundedPanel();
            this.btnToggleServer = new Affix_Center.CustomControls.RoundedButton();
            this.pnlConnections.SuspendLayout();
            this.pnlConnectionInfo.SuspendLayout();
            this.pnlInfo.SuspendLayout();
            this.pnlServerInfo2.SuspendLayout();
            this.pnlServerInfo1.SuspendLayout();
            this.pnlServerInfo0.SuspendLayout();
            this.SuspendLayout();
            // 
            // hdr
            // 
            this.hdr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hdr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.hdr.Location = new System.Drawing.Point(0, 0);
            this.hdr.Name = "hdr";
            this.hdr.Size = new System.Drawing.Size(1186, 100);
            this.hdr.Subtitle = "Configure and host your personal server.";
            this.hdr.TabIndex = 0;
            this.hdr.Title = "Host";
            // 
            // flpConnections
            // 
            this.flpConnections.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpConnections.AutoScroll = true;
            this.flpConnections.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpConnections.Location = new System.Drawing.Point(10, 10);
            this.flpConnections.Margin = new System.Windows.Forms.Padding(10);
            this.flpConnections.Name = "flpConnections";
            this.flpConnections.Size = new System.Drawing.Size(289, 471);
            this.flpConnections.TabIndex = 1;
            // 
            // pnlConnections
            // 
            this.pnlConnections.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlConnections.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.pnlConnections.BorderWidth = 2;
            this.pnlConnections.Controls.Add(this.pnlConnectionInfo);
            this.pnlConnections.Controls.Add(this.flpConnections);
            this.pnlConnections.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.pnlConnections.Location = new System.Drawing.Point(293, 109);
            this.pnlConnections.Name = "pnlConnections";
            this.pnlConnections.Radius = 5;
            this.pnlConnections.Size = new System.Drawing.Size(309, 572);
            this.pnlConnections.TabIndex = 10;
            // 
            // pnlConnectionInfo
            // 
            this.pnlConnectionInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlConnectionInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.pnlConnectionInfo.BorderWidth = 2;
            this.pnlConnectionInfo.Controls.Add(this.txtNumberConnected);
            this.pnlConnectionInfo.Controls.Add(this.lblNumberConnected);
            this.pnlConnectionInfo.Controls.Add(this.txtNumberMembers);
            this.pnlConnectionInfo.Controls.Add(this.lblNumberMembers);
            this.pnlConnectionInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.pnlConnectionInfo.Location = new System.Drawing.Point(10, 487);
            this.pnlConnectionInfo.Margin = new System.Windows.Forms.Padding(10, 3, 10, 10);
            this.pnlConnectionInfo.Name = "pnlConnectionInfo";
            this.pnlConnectionInfo.Radius = 5;
            this.pnlConnectionInfo.Size = new System.Drawing.Size(290, 75);
            this.pnlConnectionInfo.TabIndex = 3;
            // 
            // txtNumberConnected
            // 
            this.txtNumberConnected.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtNumberConnected.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.txtNumberConnected.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNumberConnected.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.txtNumberConnected.ForeColor = System.Drawing.Color.DodgerBlue;
            this.txtNumberConnected.Location = new System.Drawing.Point(118, 38);
            this.txtNumberConnected.Margin = new System.Windows.Forms.Padding(3, 10, 10, 3);
            this.txtNumberConnected.Name = "txtNumberConnected";
            this.txtNumberConnected.ReadOnly = true;
            this.txtNumberConnected.Size = new System.Drawing.Size(162, 25);
            this.txtNumberConnected.TabIndex = 21;
            // 
            // lblNumberConnected
            // 
            this.lblNumberConnected.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblNumberConnected.Font = new System.Drawing.Font("Segoe UI Light", 11F);
            this.lblNumberConnected.ForeColor = System.Drawing.Color.White;
            this.lblNumberConnected.Location = new System.Drawing.Point(10, 38);
            this.lblNumberConnected.Margin = new System.Windows.Forms.Padding(10, 3, 3, 0);
            this.lblNumberConnected.Name = "lblNumberConnected";
            this.lblNumberConnected.Size = new System.Drawing.Size(102, 23);
            this.lblNumberConnected.TabIndex = 20;
            this.lblNumberConnected.Text = "Connected:";
            this.lblNumberConnected.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNumberMembers
            // 
            this.txtNumberMembers.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtNumberMembers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.txtNumberMembers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNumberMembers.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.txtNumberMembers.ForeColor = System.Drawing.Color.DodgerBlue;
            this.txtNumberMembers.Location = new System.Drawing.Point(118, 12);
            this.txtNumberMembers.Margin = new System.Windows.Forms.Padding(3, 10, 10, 3);
            this.txtNumberMembers.Name = "txtNumberMembers";
            this.txtNumberMembers.ReadOnly = true;
            this.txtNumberMembers.Size = new System.Drawing.Size(162, 25);
            this.txtNumberMembers.TabIndex = 19;
            // 
            // lblNumberMembers
            // 
            this.lblNumberMembers.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblNumberMembers.Font = new System.Drawing.Font("Segoe UI Light", 11F);
            this.lblNumberMembers.ForeColor = System.Drawing.Color.White;
            this.lblNumberMembers.Location = new System.Drawing.Point(10, 12);
            this.lblNumberMembers.Margin = new System.Windows.Forms.Padding(10, 10, 3, 0);
            this.lblNumberMembers.Name = "lblNumberMembers";
            this.lblNumberMembers.Size = new System.Drawing.Size(102, 23);
            this.lblNumberMembers.TabIndex = 18;
            this.lblNumberMembers.Text = "Members:";
            this.lblNumberMembers.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlInfo
            // 
            this.pnlInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.pnlInfo.BorderWidth = 2;
            this.pnlInfo.Controls.Add(this.pnlServerInfo2);
            this.pnlInfo.Controls.Add(this.pnlServerInfo1);
            this.pnlInfo.Controls.Add(this.pnlServerInfo0);
            this.pnlInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.pnlInfo.Location = new System.Drawing.Point(3, 106);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Radius = 5;
            this.pnlInfo.Size = new System.Drawing.Size(284, 572);
            this.pnlInfo.TabIndex = 11;
            // 
            // pnlServerInfo2
            // 
            this.pnlServerInfo2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlServerInfo2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.pnlServerInfo2.BorderWidth = 2;
            this.pnlServerInfo2.Controls.Add(this.pnlToggleFileStorageLoading);
            this.pnlServerInfo2.Controls.Add(this.btnToggleFileStorage);
            this.pnlServerInfo2.Controls.Add(this.pnlToggleMessagingLoading);
            this.pnlServerInfo2.Controls.Add(this.btnToggleMessaging);
            this.pnlServerInfo2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.pnlServerInfo2.Location = new System.Drawing.Point(10, 193);
            this.pnlServerInfo2.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.pnlServerInfo2.Name = "pnlServerInfo2";
            this.pnlServerInfo2.Radius = 5;
            this.pnlServerInfo2.Size = new System.Drawing.Size(264, 121);
            this.pnlServerInfo2.TabIndex = 3;
            // 
            // pnlToggleFileStorageLoading
            // 
            this.pnlToggleFileStorageLoading.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pnlToggleFileStorageLoading.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.pnlToggleFileStorageLoading.BorderWidth = 2;
            this.pnlToggleFileStorageLoading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.pnlToggleFileStorageLoading.Location = new System.Drawing.Point(17, 65);
            this.pnlToggleFileStorageLoading.Margin = new System.Windows.Forms.Padding(5);
            this.pnlToggleFileStorageLoading.Name = "pnlToggleFileStorageLoading";
            this.pnlToggleFileStorageLoading.Radius = 5;
            this.pnlToggleFileStorageLoading.Size = new System.Drawing.Size(37, 37);
            this.pnlToggleFileStorageLoading.TabIndex = 13;
            // 
            // btnToggleFileStorage
            // 
            this.btnToggleFileStorage.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnToggleFileStorage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnToggleFileStorage.BorderWidth = 2;
            this.btnToggleFileStorage.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnToggleFileStorage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnToggleFileStorage.Location = new System.Drawing.Point(60, 65);
            this.btnToggleFileStorage.Margin = new System.Windows.Forms.Padding(1);
            this.btnToggleFileStorage.Name = "btnToggleFileStorage";
            this.btnToggleFileStorage.Radius = 5;
            this.btnToggleFileStorage.Size = new System.Drawing.Size(187, 37);
            stringFormat1.Alignment = System.Drawing.StringAlignment.Center;
            stringFormat1.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
            stringFormat1.LineAlignment = System.Drawing.StringAlignment.Center;
            stringFormat1.Trimming = System.Drawing.StringTrimming.Character;
            this.btnToggleFileStorage.StringFormat = stringFormat1;
            this.btnToggleFileStorage.TabIndex = 12;
            this.btnToggleFileStorage.Text = "Enable File Storage";
            this.btnToggleFileStorage.TextColor = System.Drawing.Color.White;
            this.btnToggleFileStorage.UseVisualStyleBackColor = false;
            this.btnToggleFileStorage.Click += new System.EventHandler(this.btnToggleFileStorage_Click);
            this.btnToggleFileStorage.MouseEnter += new System.EventHandler(this.btn_MouseEnter);
            this.btnToggleFileStorage.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            // 
            // pnlToggleMessagingLoading
            // 
            this.pnlToggleMessagingLoading.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pnlToggleMessagingLoading.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.pnlToggleMessagingLoading.BorderWidth = 2;
            this.pnlToggleMessagingLoading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.pnlToggleMessagingLoading.Location = new System.Drawing.Point(17, 18);
            this.pnlToggleMessagingLoading.Margin = new System.Windows.Forms.Padding(5);
            this.pnlToggleMessagingLoading.Name = "pnlToggleMessagingLoading";
            this.pnlToggleMessagingLoading.Radius = 5;
            this.pnlToggleMessagingLoading.Size = new System.Drawing.Size(37, 37);
            this.pnlToggleMessagingLoading.TabIndex = 11;
            // 
            // btnToggleMessaging
            // 
            this.btnToggleMessaging.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnToggleMessaging.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnToggleMessaging.BorderWidth = 2;
            this.btnToggleMessaging.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnToggleMessaging.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnToggleMessaging.Location = new System.Drawing.Point(60, 18);
            this.btnToggleMessaging.Margin = new System.Windows.Forms.Padding(1);
            this.btnToggleMessaging.Name = "btnToggleMessaging";
            this.btnToggleMessaging.Radius = 5;
            this.btnToggleMessaging.Size = new System.Drawing.Size(187, 37);
            stringFormat2.Alignment = System.Drawing.StringAlignment.Center;
            stringFormat2.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
            stringFormat2.LineAlignment = System.Drawing.StringAlignment.Center;
            stringFormat2.Trimming = System.Drawing.StringTrimming.Character;
            this.btnToggleMessaging.StringFormat = stringFormat2;
            this.btnToggleMessaging.TabIndex = 10;
            this.btnToggleMessaging.Text = "Enable Messaging";
            this.btnToggleMessaging.TextColor = System.Drawing.Color.White;
            this.btnToggleMessaging.UseVisualStyleBackColor = false;
            this.btnToggleMessaging.Click += new System.EventHandler(this.btnToggleMessaging_Click);
            this.btnToggleMessaging.MouseEnter += new System.EventHandler(this.btn_MouseEnter);
            this.btnToggleMessaging.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            // 
            // pnlServerInfo1
            // 
            this.pnlServerInfo1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlServerInfo1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.pnlServerInfo1.BorderWidth = 2;
            this.pnlServerInfo1.Controls.Add(this.txtCompatibility);
            this.pnlServerInfo1.Controls.Add(this.lblCompatibility);
            this.pnlServerInfo1.Controls.Add(this.txtNatType);
            this.pnlServerInfo1.Controls.Add(this.lblNatType);
            this.pnlServerInfo1.Controls.Add(this.txtIPv4);
            this.pnlServerInfo1.Controls.Add(this.lblIPv4);
            this.pnlServerInfo1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.pnlServerInfo1.Location = new System.Drawing.Point(10, 91);
            this.pnlServerInfo1.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.pnlServerInfo1.Name = "pnlServerInfo1";
            this.pnlServerInfo1.Radius = 5;
            this.pnlServerInfo1.Size = new System.Drawing.Size(264, 96);
            this.pnlServerInfo1.TabIndex = 2;
            this.pnlServerInfo1.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlServerInfo1_Paint);
            // 
            // txtCompatibility
            // 
            this.txtCompatibility.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCompatibility.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.txtCompatibility.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCompatibility.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.txtCompatibility.ForeColor = System.Drawing.Color.DodgerBlue;
            this.txtCompatibility.Location = new System.Drawing.Point(105, 62);
            this.txtCompatibility.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.txtCompatibility.Name = "txtCompatibility";
            this.txtCompatibility.ReadOnly = true;
            this.txtCompatibility.Size = new System.Drawing.Size(149, 25);
            this.txtCompatibility.TabIndex = 17;
            // 
            // lblCompatibility
            // 
            this.lblCompatibility.Font = new System.Drawing.Font("Segoe UI Light", 11F);
            this.lblCompatibility.ForeColor = System.Drawing.Color.White;
            this.lblCompatibility.Location = new System.Drawing.Point(10, 62);
            this.lblCompatibility.Margin = new System.Windows.Forms.Padding(10, 3, 3, 10);
            this.lblCompatibility.Name = "lblCompatibility";
            this.lblCompatibility.Size = new System.Drawing.Size(89, 23);
            this.lblCompatibility.TabIndex = 16;
            this.lblCompatibility.Text = "Compatibility:";
            this.lblCompatibility.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNatType
            // 
            this.txtNatType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNatType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.txtNatType.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNatType.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.txtNatType.ForeColor = System.Drawing.Color.DodgerBlue;
            this.txtNatType.Location = new System.Drawing.Point(105, 36);
            this.txtNatType.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.txtNatType.Name = "txtNatType";
            this.txtNatType.ReadOnly = true;
            this.txtNatType.Size = new System.Drawing.Size(149, 25);
            this.txtNatType.TabIndex = 15;
            // 
            // lblNatType
            // 
            this.lblNatType.Font = new System.Drawing.Font("Segoe UI Light", 11F);
            this.lblNatType.ForeColor = System.Drawing.Color.White;
            this.lblNatType.Location = new System.Drawing.Point(17, 36);
            this.lblNatType.Margin = new System.Windows.Forms.Padding(10, 3, 3, 0);
            this.lblNatType.Name = "lblNatType";
            this.lblNatType.Size = new System.Drawing.Size(82, 23);
            this.lblNatType.TabIndex = 14;
            this.lblNatType.Text = "NAT Type:";
            this.lblNatType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIPv4
            // 
            this.txtIPv4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIPv4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.txtIPv4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtIPv4.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.txtIPv4.ForeColor = System.Drawing.Color.DodgerBlue;
            this.txtIPv4.Location = new System.Drawing.Point(105, 10);
            this.txtIPv4.Margin = new System.Windows.Forms.Padding(3, 10, 10, 3);
            this.txtIPv4.Name = "txtIPv4";
            this.txtIPv4.ReadOnly = true;
            this.txtIPv4.Size = new System.Drawing.Size(149, 25);
            this.txtIPv4.TabIndex = 13;
            // 
            // lblIPv4
            // 
            this.lblIPv4.Font = new System.Drawing.Font("Segoe UI Light", 11F);
            this.lblIPv4.ForeColor = System.Drawing.Color.White;
            this.lblIPv4.Location = new System.Drawing.Point(17, 10);
            this.lblIPv4.Margin = new System.Windows.Forms.Padding(10, 10, 3, 0);
            this.lblIPv4.Name = "lblIPv4";
            this.lblIPv4.Size = new System.Drawing.Size(82, 23);
            this.lblIPv4.TabIndex = 12;
            this.lblIPv4.Text = "IPv4:";
            this.lblIPv4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlServerInfo0
            // 
            this.pnlServerInfo0.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlServerInfo0.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.pnlServerInfo0.BorderWidth = 2;
            this.pnlServerInfo0.Controls.Add(this.pnlToggleServerLoading);
            this.pnlServerInfo0.Controls.Add(this.btnToggleServer);
            this.pnlServerInfo0.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.pnlServerInfo0.Location = new System.Drawing.Point(10, 10);
            this.pnlServerInfo0.Margin = new System.Windows.Forms.Padding(10, 10, 10, 3);
            this.pnlServerInfo0.Name = "pnlServerInfo0";
            this.pnlServerInfo0.Radius = 5;
            this.pnlServerInfo0.Size = new System.Drawing.Size(264, 75);
            this.pnlServerInfo0.TabIndex = 0;
            // 
            // pnlToggleServerLoading
            // 
            this.pnlToggleServerLoading.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlToggleServerLoading.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.pnlToggleServerLoading.BorderWidth = 2;
            this.pnlToggleServerLoading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.pnlToggleServerLoading.Location = new System.Drawing.Point(17, 19);
            this.pnlToggleServerLoading.Margin = new System.Windows.Forms.Padding(5);
            this.pnlToggleServerLoading.Name = "pnlToggleServerLoading";
            this.pnlToggleServerLoading.Radius = 5;
            this.pnlToggleServerLoading.Size = new System.Drawing.Size(37, 37);
            this.pnlToggleServerLoading.TabIndex = 9;
            // 
            // btnToggleServer
            // 
            this.btnToggleServer.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnToggleServer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnToggleServer.BorderWidth = 2;
            this.btnToggleServer.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnToggleServer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnToggleServer.Location = new System.Drawing.Point(60, 19);
            this.btnToggleServer.Margin = new System.Windows.Forms.Padding(1);
            this.btnToggleServer.Name = "btnToggleServer";
            this.btnToggleServer.Radius = 5;
            this.btnToggleServer.Size = new System.Drawing.Size(187, 37);
            stringFormat3.Alignment = System.Drawing.StringAlignment.Center;
            stringFormat3.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
            stringFormat3.LineAlignment = System.Drawing.StringAlignment.Center;
            stringFormat3.Trimming = System.Drawing.StringTrimming.Character;
            this.btnToggleServer.StringFormat = stringFormat3;
            this.btnToggleServer.TabIndex = 8;
            this.btnToggleServer.Text = "Enable Server";
            this.btnToggleServer.TextColor = System.Drawing.Color.White;
            this.btnToggleServer.UseVisualStyleBackColor = false;
            this.btnToggleServer.Click += new System.EventHandler(this.btnToggleServer_Click);
            this.btnToggleServer.MouseEnter += new System.EventHandler(this.btn_MouseEnter);
            this.btnToggleServer.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            // 
            // Page_Host
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Controls.Add(this.pnlInfo);
            this.Controls.Add(this.pnlConnections);
            this.Controls.Add(this.hdr);
            this.Name = "Page_Host";
            this.Size = new System.Drawing.Size(1186, 681);
            this.Load += new System.EventHandler(this.Page_Host_Load);
            this.pnlConnections.ResumeLayout(false);
            this.pnlConnectionInfo.ResumeLayout(false);
            this.pnlConnectionInfo.PerformLayout();
            this.pnlInfo.ResumeLayout(false);
            this.pnlServerInfo2.ResumeLayout(false);
            this.pnlServerInfo1.ResumeLayout(false);
            this.pnlServerInfo1.PerformLayout();
            this.pnlServerInfo0.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Dialog_Header hdr;
        private System.Windows.Forms.FlowLayoutPanel flpConnections;
        private CustomControls.RoundedPanel pnlConnections;
        private CustomControls.RoundedPanel pnlInfo;
        private CustomControls.RoundedPanel pnlServerInfo0;
        private CustomControls.RoundedPanel pnlServerInfo1;
        public CustomControls.RoundedPanel pnlToggleServerLoading;
        public CustomControls.RoundedButton btnToggleServer;
        private CustomControls.RoundedPanel pnlConnectionInfo;
        private CustomControls.RoundedPanel pnlServerInfo2;
        public CustomControls.RoundedPanel pnlToggleFileStorageLoading;
        public CustomControls.RoundedButton btnToggleFileStorage;
        public CustomControls.RoundedPanel pnlToggleMessagingLoading;
        public CustomControls.RoundedButton btnToggleMessaging;
        private System.Windows.Forms.TextBox txtIPv4;
        private System.Windows.Forms.Label lblIPv4;
        private System.Windows.Forms.TextBox txtNatType;
        private System.Windows.Forms.Label lblNatType;
        private System.Windows.Forms.TextBox txtCompatibility;
        private System.Windows.Forms.Label lblCompatibility;
        private System.Windows.Forms.TextBox txtNumberConnected;
        private System.Windows.Forms.Label lblNumberConnected;
        private System.Windows.Forms.TextBox txtNumberMembers;
        private System.Windows.Forms.Label lblNumberMembers;
    }
}
