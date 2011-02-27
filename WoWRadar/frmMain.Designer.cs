namespace WoWRadar
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
            this.RadarBox = new System.Windows.Forms.PictureBox();
            this.RadarTimer = new System.Windows.Forms.Timer(this.components);
            this.tmrWaypoint = new System.Windows.Forms.Timer(this.components);
            this.grpPlayer = new System.Windows.Forms.GroupBox();
            this.lblPlayerPower = new System.Windows.Forms.Label();
            this.lblPlayerHealth = new System.Windows.Forms.Label();
            this.lblPlayerRotationD = new System.Windows.Forms.Label();
            this.lblPlayerRotationR = new System.Windows.Forms.Label();
            this.lblPlayerGuid = new System.Windows.Forms.Label();
            this.lblPlayerZPosition = new System.Windows.Forms.Label();
            this.lblPlayerYPosition = new System.Windows.Forms.Label();
            this.lblPlayerXPosition = new System.Windows.Forms.Label();
            this.lblPlayerName = new System.Windows.Forms.Label();
            this.lblPlayerLevel = new System.Windows.Forms.Label();
            this.grpTarget = new System.Windows.Forms.GroupBox();
            this.lblTargetPower = new System.Windows.Forms.Label();
            this.lblTargetSummonedBy = new System.Windows.Forms.Label();
            this.lblTargetHealth = new System.Windows.Forms.Label();
            this.lblTargetGuid = new System.Windows.Forms.Label();
            this.lblTargetZPosition = new System.Windows.Forms.Label();
            this.lblTargetYPosition = new System.Windows.Forms.Label();
            this.lblTargetXPosition = new System.Windows.Forms.Label();
            this.lblTargetName = new System.Windows.Forms.Label();
            this.lblTargetLevel = new System.Windows.Forms.Label();
            this.grpProcess = new System.Windows.Forms.GroupBox();
            this.lblTotalWowObjects = new System.Windows.Forms.Label();
            this.lblFirstObject = new System.Windows.Forms.Label();
            this.lblObjectManager = new System.Windows.Forms.Label();
            this.lblClientConnection = new System.Windows.Forms.Label();
            this.grpConfig = new System.Windows.Forms.GroupBox();
            this.btnReload = new System.Windows.Forms.Button();
            this.chkWaypoint = new System.Windows.Forms.CheckBox();
            this.chkRadius = new System.Windows.Forms.CheckBox();
            this.chkMinimap = new System.Windows.Forms.CheckBox();
            this.chkGrid = new System.Windows.Forms.CheckBox();
            this.lblRadarZoom = new System.Windows.Forms.Label();
            this.trkZoom = new System.Windows.Forms.TrackBar();
            this.lblRefreshRate = new System.Windows.Forms.Label();
            this.tkbRefreshRate = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.RadarBox)).BeginInit();
            this.grpPlayer.SuspendLayout();
            this.grpTarget.SuspendLayout();
            this.grpProcess.SuspendLayout();
            this.grpConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkZoom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tkbRefreshRate)).BeginInit();
            this.SuspendLayout();
            // 
            // RadarBox
            // 
            this.RadarBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RadarBox.Location = new System.Drawing.Point(12, 41);
            this.RadarBox.Name = "RadarBox";
            this.RadarBox.Size = new System.Drawing.Size(542, 542);
            this.RadarBox.TabIndex = 0;
            this.RadarBox.TabStop = false;
            // 
            // RadarTimer
            // 
            this.RadarTimer.Enabled = true;
            this.RadarTimer.Tick += new System.EventHandler(this.RadarTimer_Tick);
            // 
            // tmrWaypoint
            // 
            this.tmrWaypoint.Interval = 5000;
            this.tmrWaypoint.Tick += new System.EventHandler(this.tmrWaypoint_Tick);
            // 
            // grpPlayer
            // 
            this.grpPlayer.Controls.Add(this.lblPlayerPower);
            this.grpPlayer.Controls.Add(this.lblPlayerHealth);
            this.grpPlayer.Controls.Add(this.lblPlayerRotationD);
            this.grpPlayer.Controls.Add(this.lblPlayerRotationR);
            this.grpPlayer.Controls.Add(this.lblPlayerGuid);
            this.grpPlayer.Controls.Add(this.lblPlayerZPosition);
            this.grpPlayer.Controls.Add(this.lblPlayerYPosition);
            this.grpPlayer.Controls.Add(this.lblPlayerXPosition);
            this.grpPlayer.Controls.Add(this.lblPlayerName);
            this.grpPlayer.Controls.Add(this.lblPlayerLevel);
            this.grpPlayer.Location = new System.Drawing.Point(565, 12);
            this.grpPlayer.Name = "grpPlayer";
            this.grpPlayer.Size = new System.Drawing.Size(206, 160);
            this.grpPlayer.TabIndex = 38;
            this.grpPlayer.TabStop = false;
            this.grpPlayer.Text = "Player Information";
            // 
            // lblPlayerPower
            // 
            this.lblPlayerPower.AutoSize = true;
            this.lblPlayerPower.Location = new System.Drawing.Point(12, 55);
            this.lblPlayerPower.Name = "lblPlayerPower";
            this.lblPlayerPower.Size = new System.Drawing.Size(72, 13);
            this.lblPlayerPower.TabIndex = 36;
            this.lblPlayerPower.Text = "Player Power:";
            // 
            // lblPlayerHealth
            // 
            this.lblPlayerHealth.AutoSize = true;
            this.lblPlayerHealth.Location = new System.Drawing.Point(12, 42);
            this.lblPlayerHealth.Name = "lblPlayerHealth";
            this.lblPlayerHealth.Size = new System.Drawing.Size(73, 13);
            this.lblPlayerHealth.TabIndex = 35;
            this.lblPlayerHealth.Text = "Player Health:";
            // 
            // lblPlayerRotationD
            // 
            this.lblPlayerRotationD.AutoSize = true;
            this.lblPlayerRotationD.Location = new System.Drawing.Point(12, 134);
            this.lblPlayerRotationD.Name = "lblPlayerRotationD";
            this.lblPlayerRotationD.Size = new System.Drawing.Size(82, 13);
            this.lblPlayerRotationD.TabIndex = 34;
            this.lblPlayerRotationD.Text = "Player Rotation:";
            // 
            // lblPlayerRotationR
            // 
            this.lblPlayerRotationR.AutoSize = true;
            this.lblPlayerRotationR.Location = new System.Drawing.Point(12, 120);
            this.lblPlayerRotationR.Name = "lblPlayerRotationR";
            this.lblPlayerRotationR.Size = new System.Drawing.Size(82, 13);
            this.lblPlayerRotationR.TabIndex = 33;
            this.lblPlayerRotationR.Text = "Player Rotation:";
            // 
            // lblPlayerGuid
            // 
            this.lblPlayerGuid.AutoSize = true;
            this.lblPlayerGuid.Location = new System.Drawing.Point(12, 107);
            this.lblPlayerGuid.Name = "lblPlayerGuid";
            this.lblPlayerGuid.Size = new System.Drawing.Size(64, 13);
            this.lblPlayerGuid.TabIndex = 32;
            this.lblPlayerGuid.Text = "Player Guid:";
            // 
            // lblPlayerZPosition
            // 
            this.lblPlayerZPosition.AutoSize = true;
            this.lblPlayerZPosition.Location = new System.Drawing.Point(12, 94);
            this.lblPlayerZPosition.Name = "lblPlayerZPosition";
            this.lblPlayerZPosition.Size = new System.Drawing.Size(70, 13);
            this.lblPlayerZPosition.TabIndex = 31;
            this.lblPlayerZPosition.Text = "Player Z-Pos:";
            // 
            // lblPlayerYPosition
            // 
            this.lblPlayerYPosition.AutoSize = true;
            this.lblPlayerYPosition.Location = new System.Drawing.Point(12, 81);
            this.lblPlayerYPosition.Name = "lblPlayerYPosition";
            this.lblPlayerYPosition.Size = new System.Drawing.Size(70, 13);
            this.lblPlayerYPosition.TabIndex = 30;
            this.lblPlayerYPosition.Text = "Player Y-Pos:";
            // 
            // lblPlayerXPosition
            // 
            this.lblPlayerXPosition.AutoSize = true;
            this.lblPlayerXPosition.Location = new System.Drawing.Point(12, 68);
            this.lblPlayerXPosition.Name = "lblPlayerXPosition";
            this.lblPlayerXPosition.Size = new System.Drawing.Size(70, 13);
            this.lblPlayerXPosition.TabIndex = 29;
            this.lblPlayerXPosition.Text = "Player X-Pos:";
            // 
            // lblPlayerName
            // 
            this.lblPlayerName.AutoSize = true;
            this.lblPlayerName.Location = new System.Drawing.Point(12, 16);
            this.lblPlayerName.Name = "lblPlayerName";
            this.lblPlayerName.Size = new System.Drawing.Size(70, 13);
            this.lblPlayerName.TabIndex = 28;
            this.lblPlayerName.Text = "Player Name:";
            // 
            // lblPlayerLevel
            // 
            this.lblPlayerLevel.AutoSize = true;
            this.lblPlayerLevel.Location = new System.Drawing.Point(12, 29);
            this.lblPlayerLevel.Name = "lblPlayerLevel";
            this.lblPlayerLevel.Size = new System.Drawing.Size(68, 13);
            this.lblPlayerLevel.TabIndex = 27;
            this.lblPlayerLevel.Text = "Player Level:";
            // 
            // grpTarget
            // 
            this.grpTarget.Controls.Add(this.lblTargetPower);
            this.grpTarget.Controls.Add(this.lblTargetSummonedBy);
            this.grpTarget.Controls.Add(this.lblTargetHealth);
            this.grpTarget.Controls.Add(this.lblTargetGuid);
            this.grpTarget.Controls.Add(this.lblTargetZPosition);
            this.grpTarget.Controls.Add(this.lblTargetYPosition);
            this.grpTarget.Controls.Add(this.lblTargetXPosition);
            this.grpTarget.Controls.Add(this.lblTargetName);
            this.grpTarget.Controls.Add(this.lblTargetLevel);
            this.grpTarget.Location = new System.Drawing.Point(565, 178);
            this.grpTarget.Name = "grpTarget";
            this.grpTarget.Size = new System.Drawing.Size(206, 143);
            this.grpTarget.TabIndex = 39;
            this.grpTarget.TabStop = false;
            this.grpTarget.Text = "Target Information";
            // 
            // lblTargetPower
            // 
            this.lblTargetPower.AutoSize = true;
            this.lblTargetPower.Location = new System.Drawing.Point(12, 55);
            this.lblTargetPower.Name = "lblTargetPower";
            this.lblTargetPower.Size = new System.Drawing.Size(74, 13);
            this.lblTargetPower.TabIndex = 38;
            this.lblTargetPower.Text = "Target Power:";
            // 
            // lblTargetSummonedBy
            // 
            this.lblTargetSummonedBy.AutoSize = true;
            this.lblTargetSummonedBy.Location = new System.Drawing.Point(12, 120);
            this.lblTargetSummonedBy.Name = "lblTargetSummonedBy";
            this.lblTargetSummonedBy.Size = new System.Drawing.Size(78, 13);
            this.lblTargetSummonedBy.TabIndex = 37;
            this.lblTargetSummonedBy.Text = "Summoned By:";
            // 
            // lblTargetHealth
            // 
            this.lblTargetHealth.AutoSize = true;
            this.lblTargetHealth.Location = new System.Drawing.Point(12, 42);
            this.lblTargetHealth.Name = "lblTargetHealth";
            this.lblTargetHealth.Size = new System.Drawing.Size(75, 13);
            this.lblTargetHealth.TabIndex = 36;
            this.lblTargetHealth.Text = "Target Health:";
            // 
            // lblTargetGuid
            // 
            this.lblTargetGuid.AutoSize = true;
            this.lblTargetGuid.Location = new System.Drawing.Point(12, 107);
            this.lblTargetGuid.Name = "lblTargetGuid";
            this.lblTargetGuid.Size = new System.Drawing.Size(66, 13);
            this.lblTargetGuid.TabIndex = 35;
            this.lblTargetGuid.Text = "Target Guid:";
            // 
            // lblTargetZPosition
            // 
            this.lblTargetZPosition.AutoSize = true;
            this.lblTargetZPosition.Location = new System.Drawing.Point(12, 94);
            this.lblTargetZPosition.Name = "lblTargetZPosition";
            this.lblTargetZPosition.Size = new System.Drawing.Size(72, 13);
            this.lblTargetZPosition.TabIndex = 34;
            this.lblTargetZPosition.Text = "Target Z-Pos:";
            // 
            // lblTargetYPosition
            // 
            this.lblTargetYPosition.AutoSize = true;
            this.lblTargetYPosition.Location = new System.Drawing.Point(12, 81);
            this.lblTargetYPosition.Name = "lblTargetYPosition";
            this.lblTargetYPosition.Size = new System.Drawing.Size(72, 13);
            this.lblTargetYPosition.TabIndex = 33;
            this.lblTargetYPosition.Text = "Target Y-Pos:";
            // 
            // lblTargetXPosition
            // 
            this.lblTargetXPosition.AutoSize = true;
            this.lblTargetXPosition.Location = new System.Drawing.Point(12, 68);
            this.lblTargetXPosition.Name = "lblTargetXPosition";
            this.lblTargetXPosition.Size = new System.Drawing.Size(72, 13);
            this.lblTargetXPosition.TabIndex = 32;
            this.lblTargetXPosition.Text = "Target X-Pos:";
            // 
            // lblTargetName
            // 
            this.lblTargetName.AutoSize = true;
            this.lblTargetName.Location = new System.Drawing.Point(12, 16);
            this.lblTargetName.Name = "lblTargetName";
            this.lblTargetName.Size = new System.Drawing.Size(72, 13);
            this.lblTargetName.TabIndex = 31;
            this.lblTargetName.Text = "Target Name:";
            // 
            // lblTargetLevel
            // 
            this.lblTargetLevel.AutoSize = true;
            this.lblTargetLevel.Location = new System.Drawing.Point(12, 29);
            this.lblTargetLevel.Name = "lblTargetLevel";
            this.lblTargetLevel.Size = new System.Drawing.Size(70, 13);
            this.lblTargetLevel.TabIndex = 30;
            this.lblTargetLevel.Text = "Target Level:";
            // 
            // grpProcess
            // 
            this.grpProcess.Controls.Add(this.lblTotalWowObjects);
            this.grpProcess.Controls.Add(this.lblFirstObject);
            this.grpProcess.Controls.Add(this.lblObjectManager);
            this.grpProcess.Controls.Add(this.lblClientConnection);
            this.grpProcess.Location = new System.Drawing.Point(565, 327);
            this.grpProcess.Name = "grpProcess";
            this.grpProcess.Size = new System.Drawing.Size(206, 88);
            this.grpProcess.TabIndex = 40;
            this.grpProcess.TabStop = false;
            this.grpProcess.Text = "Process Information";
            // 
            // lblTotalWowObjects
            // 
            this.lblTotalWowObjects.AutoSize = true;
            this.lblTotalWowObjects.Location = new System.Drawing.Point(6, 61);
            this.lblTotalWowObjects.Name = "lblTotalWowObjects";
            this.lblTotalWowObjects.Size = new System.Drawing.Size(130, 13);
            this.lblTotalWowObjects.TabIndex = 29;
            this.lblTotalWowObjects.Text = "Total Objects In Manager:";
            // 
            // lblFirstObject
            // 
            this.lblFirstObject.AutoSize = true;
            this.lblFirstObject.Location = new System.Drawing.Point(6, 48);
            this.lblFirstObject.Name = "lblFirstObject";
            this.lblFirstObject.Size = new System.Drawing.Size(63, 13);
            this.lblFirstObject.TabIndex = 20;
            this.lblFirstObject.Text = "First Object:";
            // 
            // lblObjectManager
            // 
            this.lblObjectManager.AutoSize = true;
            this.lblObjectManager.Location = new System.Drawing.Point(6, 35);
            this.lblObjectManager.Name = "lblObjectManager";
            this.lblObjectManager.Size = new System.Drawing.Size(86, 13);
            this.lblObjectManager.TabIndex = 19;
            this.lblObjectManager.Text = "Object Manager:";
            // 
            // lblClientConnection
            // 
            this.lblClientConnection.AutoSize = true;
            this.lblClientConnection.Location = new System.Drawing.Point(6, 22);
            this.lblClientConnection.Name = "lblClientConnection";
            this.lblClientConnection.Size = new System.Drawing.Size(93, 13);
            this.lblClientConnection.TabIndex = 18;
            this.lblClientConnection.Text = "Client Connection:";
            // 
            // grpConfig
            // 
            this.grpConfig.Controls.Add(this.btnReload);
            this.grpConfig.Controls.Add(this.chkWaypoint);
            this.grpConfig.Controls.Add(this.chkRadius);
            this.grpConfig.Controls.Add(this.chkMinimap);
            this.grpConfig.Controls.Add(this.chkGrid);
            this.grpConfig.Controls.Add(this.lblRadarZoom);
            this.grpConfig.Controls.Add(this.trkZoom);
            this.grpConfig.Controls.Add(this.lblRefreshRate);
            this.grpConfig.Controls.Add(this.tkbRefreshRate);
            this.grpConfig.Location = new System.Drawing.Point(565, 421);
            this.grpConfig.Name = "grpConfig";
            this.grpConfig.Size = new System.Drawing.Size(206, 208);
            this.grpConfig.TabIndex = 41;
            this.grpConfig.TabStop = false;
            this.grpConfig.Text = "Settings";
            // 
            // btnReload
            // 
            this.btnReload.Location = new System.Drawing.Point(125, 178);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(75, 23);
            this.btnReload.TabIndex = 47;
            this.btnReload.Text = "Reload";
            this.btnReload.UseVisualStyleBackColor = false;
            // 
            // chkWaypoint
            // 
            this.chkWaypoint.AutoSize = true;
            this.chkWaypoint.Location = new System.Drawing.Point(9, 185);
            this.chkWaypoint.Name = "chkWaypoint";
            this.chkWaypoint.Size = new System.Drawing.Size(76, 17);
            this.chkWaypoint.TabIndex = 46;
            this.chkWaypoint.Text = "Waypoints";
            this.chkWaypoint.UseVisualStyleBackColor = true;
            this.chkWaypoint.CheckedChanged += new System.EventHandler(this.chkWaypoint_CheckedChanged);
            // 
            // chkRadius
            // 
            this.chkRadius.AutoSize = true;
            this.chkRadius.Location = new System.Drawing.Point(9, 162);
            this.chkRadius.Name = "chkRadius";
            this.chkRadius.Size = new System.Drawing.Size(116, 17);
            this.chkRadius.TabIndex = 45;
            this.chkRadius.Text = "Discovered Radius";
            this.chkRadius.UseVisualStyleBackColor = true;
            // 
            // chkMinimap
            // 
            this.chkMinimap.AutoSize = true;
            this.chkMinimap.Location = new System.Drawing.Point(9, 139);
            this.chkMinimap.Name = "chkMinimap";
            this.chkMinimap.Size = new System.Drawing.Size(104, 17);
            this.chkMinimap.TabIndex = 44;
            this.chkMinimap.Text = "Minimap Overlay";
            this.chkMinimap.UseVisualStyleBackColor = true;
            // 
            // chkGrid
            // 
            this.chkGrid.AutoSize = true;
            this.chkGrid.Location = new System.Drawing.Point(9, 116);
            this.chkGrid.Name = "chkGrid";
            this.chkGrid.Size = new System.Drawing.Size(84, 17);
            this.chkGrid.TabIndex = 43;
            this.chkGrid.Text = "Grid Overlay";
            this.chkGrid.UseVisualStyleBackColor = true;
            // 
            // lblRadarZoom
            // 
            this.lblRadarZoom.AutoSize = true;
            this.lblRadarZoom.Location = new System.Drawing.Point(43, 95);
            this.lblRadarZoom.Name = "lblRadarZoom";
            this.lblRadarZoom.Size = new System.Drawing.Size(93, 13);
            this.lblRadarZoom.TabIndex = 42;
            this.lblRadarZoom.Text = "Zoom Factor: 1.0x";
            // 
            // trkZoom
            // 
            this.trkZoom.LargeChange = 10;
            this.trkZoom.Location = new System.Drawing.Point(4, 63);
            this.trkZoom.Maximum = 50;
            this.trkZoom.Minimum = 10;
            this.trkZoom.Name = "trkZoom";
            this.trkZoom.Size = new System.Drawing.Size(196, 45);
            this.trkZoom.SmallChange = 10;
            this.trkZoom.TabIndex = 32;
            this.trkZoom.TickFrequency = 10;
            this.trkZoom.Value = 10;
            // 
            // lblRefreshRate
            // 
            this.lblRefreshRate.AutoSize = true;
            this.lblRefreshRate.Location = new System.Drawing.Point(43, 47);
            this.lblRefreshRate.Name = "lblRefreshRate";
            this.lblRefreshRate.Size = new System.Drawing.Size(107, 13);
            this.lblRefreshRate.TabIndex = 24;
            this.lblRefreshRate.Text = "Refresh Rate: 100ms";
            // 
            // tkbRefreshRate
            // 
            this.tkbRefreshRate.LargeChange = 100;
            this.tkbRefreshRate.Location = new System.Drawing.Point(4, 19);
            this.tkbRefreshRate.Maximum = 1000;
            this.tkbRefreshRate.Minimum = 10;
            this.tkbRefreshRate.Name = "tkbRefreshRate";
            this.tkbRefreshRate.Size = new System.Drawing.Size(196, 45);
            this.tkbRefreshRate.SmallChange = 10;
            this.tkbRefreshRate.TabIndex = 23;
            this.tkbRefreshRate.TickFrequency = 10;
            this.tkbRefreshRate.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tkbRefreshRate.Value = 100;
            this.tkbRefreshRate.Scroll += new System.EventHandler(this.tkbRefreshRate_Scroll);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 634);
            this.Controls.Add(this.grpConfig);
            this.Controls.Add(this.grpProcess);
            this.Controls.Add(this.grpTarget);
            this.Controls.Add(this.grpPlayer);
            this.Controls.Add(this.RadarBox);
            this.Name = "frmMain";
            this.Text = "WoW Radar";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.frmMain_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.RadarBox)).EndInit();
            this.grpPlayer.ResumeLayout(false);
            this.grpPlayer.PerformLayout();
            this.grpTarget.ResumeLayout(false);
            this.grpTarget.PerformLayout();
            this.grpProcess.ResumeLayout(false);
            this.grpProcess.PerformLayout();
            this.grpConfig.ResumeLayout(false);
            this.grpConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkZoom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tkbRefreshRate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox RadarBox;
        private System.Windows.Forms.Timer RadarTimer;
        private System.Windows.Forms.Timer tmrWaypoint;
        private System.Windows.Forms.GroupBox grpPlayer;
        private System.Windows.Forms.Label lblPlayerPower;
        private System.Windows.Forms.Label lblPlayerHealth;
        private System.Windows.Forms.Label lblPlayerRotationD;
        private System.Windows.Forms.Label lblPlayerRotationR;
        private System.Windows.Forms.Label lblPlayerGuid;
        private System.Windows.Forms.Label lblPlayerZPosition;
        private System.Windows.Forms.Label lblPlayerYPosition;
        private System.Windows.Forms.Label lblPlayerXPosition;
        private System.Windows.Forms.Label lblPlayerName;
        private System.Windows.Forms.Label lblPlayerLevel;
        private System.Windows.Forms.GroupBox grpTarget;
        private System.Windows.Forms.Label lblTargetPower;
        private System.Windows.Forms.Label lblTargetSummonedBy;
        private System.Windows.Forms.Label lblTargetHealth;
        private System.Windows.Forms.Label lblTargetGuid;
        private System.Windows.Forms.Label lblTargetZPosition;
        private System.Windows.Forms.Label lblTargetYPosition;
        private System.Windows.Forms.Label lblTargetXPosition;
        private System.Windows.Forms.Label lblTargetName;
        private System.Windows.Forms.Label lblTargetLevel;
        private System.Windows.Forms.GroupBox grpProcess;
        private System.Windows.Forms.Label lblTotalWowObjects;
        private System.Windows.Forms.Label lblFirstObject;
        private System.Windows.Forms.Label lblObjectManager;
        private System.Windows.Forms.Label lblClientConnection;
        private System.Windows.Forms.GroupBox grpConfig;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.CheckBox chkWaypoint;
        private System.Windows.Forms.CheckBox chkRadius;
        private System.Windows.Forms.CheckBox chkMinimap;
        private System.Windows.Forms.CheckBox chkGrid;
        private System.Windows.Forms.Label lblRadarZoom;
        public System.Windows.Forms.TrackBar trkZoom;
        private System.Windows.Forms.Label lblRefreshRate;
        private System.Windows.Forms.TrackBar tkbRefreshRate;
    }
}

