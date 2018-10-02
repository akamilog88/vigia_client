namespace HomeAlarm
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.LB_dtmfStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.LB_PPortStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.TAB_alarmGroup = new System.Windows.Forms.TabControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.TA_logArea = new System.Windows.Forms.RichTextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.bt_reset = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.bt_stopDetector = new System.Windows.Forms.Button();
            this.bt_startDetector = new System.Windows.Forms.Button();
            this.ls_detectors = new System.Windows.Forms.CheckedListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.GB_filterPanel = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TB_Filter = new System.Windows.Forms.TextBox();
            this.LB_status = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.GB_filterPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.statusStrip1);
            this.groupBox1.Controls.Add(this.TAB_alarmGroup);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Teal;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(865, 201);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Alarmas";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LB_dtmfStatus,
            this.LB_PPortStatus});
            this.statusStrip1.Location = new System.Drawing.Point(2, 177);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(861, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // LB_dtmfStatus
            // 
            this.LB_dtmfStatus.Name = "LB_dtmfStatus";
            this.LB_dtmfStatus.Size = new System.Drawing.Size(158, 17);
            this.LB_dtmfStatus.Text = "Sensores DTMF: no iniciados";
            // 
            // LB_PPortStatus
            // 
            this.LB_PPortStatus.Name = "LB_PPortStatus";
            this.LB_PPortStatus.Size = new System.Drawing.Size(206, 17);
            this.LB_PPortStatus.Text = "Sensores Puerto Paralelo: no iniciados";
            // 
            // TAB_alarmGroup
            // 
            this.TAB_alarmGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TAB_alarmGroup.Location = new System.Drawing.Point(2, 18);
            this.TAB_alarmGroup.Name = "TAB_alarmGroup";
            this.TAB_alarmGroup.SelectedIndex = 0;
            this.TAB_alarmGroup.Size = new System.Drawing.Size(861, 181);
            this.TAB_alarmGroup.TabIndex = 1;
            this.TAB_alarmGroup.SelectedIndexChanged += new System.EventHandler(this.TAB_alarmGroup_SelectedIndexChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.TA_logArea, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.59309F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48.40691F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(871, 462);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // TA_logArea
            // 
            this.TA_logArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TA_logArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TA_logArea.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.TA_logArea.Location = new System.Drawing.Point(3, 270);
            this.TA_logArea.Name = "TA_logArea";
            this.TA_logArea.ReadOnly = true;
            this.TA_logArea.Size = new System.Drawing.Size(865, 189);
            this.TA_logArea.TabIndex = 1;
            this.TA_logArea.Text = "";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.bt_reset);
            this.flowLayoutPanel1.Controls.Add(this.panel1);
            this.flowLayoutPanel1.Controls.Add(this.button1);
            this.flowLayoutPanel1.Controls.Add(this.GB_filterPanel);
            this.flowLayoutPanel1.Controls.Add(this.LB_status);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 210);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(865, 54);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // bt_reset
            // 
            this.bt_reset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.bt_reset.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_reset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.bt_reset.Location = new System.Drawing.Point(0, 0);
            this.bt_reset.Margin = new System.Windows.Forms.Padding(0);
            this.bt_reset.Name = "bt_reset";
            this.bt_reset.Size = new System.Drawing.Size(107, 55);
            this.bt_reset.TabIndex = 0;
            this.bt_reset.Tag = "Some Button";
            this.bt_reset.Text = "Comenzar";
            this.bt_reset.UseVisualStyleBackColor = false;
            this.bt_reset.Click += new System.EventHandler(this.bt_reset_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Location = new System.Drawing.Point(110, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(143, 60);
            this.panel1.TabIndex = 5;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.65035F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.34965F));
            this.tableLayoutPanel2.Controls.Add(this.bt_stopDetector, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.bt_startDetector, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.ls_detectors, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(143, 60);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // bt_stopDetector
            // 
            this.bt_stopDetector.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.bt_stopDetector.Location = new System.Drawing.Point(70, 27);
            this.bt_stopDetector.Margin = new System.Windows.Forms.Padding(0);
            this.bt_stopDetector.Name = "bt_stopDetector";
            this.bt_stopDetector.Size = new System.Drawing.Size(73, 23);
            this.bt_stopDetector.TabIndex = 1;
            this.bt_stopDetector.Text = "DETENER";
            this.bt_stopDetector.UseVisualStyleBackColor = false;
            this.bt_stopDetector.Click += new System.EventHandler(this.bt_stopDetector_Click);
            // 
            // bt_startDetector
            // 
            this.bt_startDetector.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.bt_startDetector.Location = new System.Drawing.Point(0, 27);
            this.bt_startDetector.Margin = new System.Windows.Forms.Padding(0);
            this.bt_startDetector.Name = "bt_startDetector";
            this.bt_startDetector.Size = new System.Drawing.Size(70, 23);
            this.bt_startDetector.TabIndex = 0;
            this.bt_startDetector.Text = "INICIAR";
            this.bt_startDetector.UseVisualStyleBackColor = false;
            this.bt_startDetector.Click += new System.EventHandler(this.bt_startDetector_Click);
            // 
            // ls_detectors
            // 
            this.ls_detectors.CheckOnClick = true;
            this.tableLayoutPanel2.SetColumnSpan(this.ls_detectors, 2);
            this.ls_detectors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ls_detectors.FormattingEnabled = true;
            this.ls_detectors.Items.AddRange(new object[] {
            "DTMF",
            "PPORT"});
            this.ls_detectors.Location = new System.Drawing.Point(0, 0);
            this.ls_detectors.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.ls_detectors.Name = "ls_detectors";
            this.ls_detectors.Size = new System.Drawing.Size(143, 24);
            this.ls_detectors.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Navy;
            this.button1.Location = new System.Drawing.Point(259, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 34);
            this.button1.TabIndex = 1;
            this.button1.Text = "HISTORIAL";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // GB_filterPanel
            // 
            this.GB_filterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.GB_filterPanel.Controls.Add(this.button2);
            this.GB_filterPanel.Controls.Add(this.label1);
            this.GB_filterPanel.Controls.Add(this.TB_Filter);
            this.GB_filterPanel.Location = new System.Drawing.Point(347, 3);
            this.GB_filterPanel.Name = "GB_filterPanel";
            this.GB_filterPanel.Size = new System.Drawing.Size(245, 34);
            this.GB_filterPanel.TabIndex = 4;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(174, 7);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(63, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Refrescar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "FILTRAR";
            // 
            // TB_Filter
            // 
            this.TB_Filter.Location = new System.Drawing.Point(68, 7);
            this.TB_Filter.Name = "TB_Filter";
            this.TB_Filter.Size = new System.Drawing.Size(100, 20);
            this.TB_Filter.TabIndex = 3;
            // 
            // LB_status
            // 
            this.LB_status.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolTip;
            this.LB_status.Dock = System.Windows.Forms.DockStyle.Left;
            this.LB_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LB_status.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LB_status.Location = new System.Drawing.Point(598, 0);
            this.LB_status.Name = "LB_status";
            this.LB_status.Size = new System.Drawing.Size(253, 66);
            this.LB_status.TabIndex = 2;
            this.LB_status.Tag = "xdzfgvcfxgcfxgcfxgdxfg";
            this.LB_status.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(871, 462);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(750, 380);
            this.Name = "Form1";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VIGIA";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.GB_filterPanel.ResumeLayout(false);
            this.GB_filterPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RichTextBox TA_logArea;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button bt_reset;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label LB_status;
        private System.Windows.Forms.TabControl TAB_alarmGroup;
        private System.Windows.Forms.Panel GB_filterPanel;
        private System.Windows.Forms.TextBox TB_Filter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel LB_dtmfStatus;
        private System.Windows.Forms.ToolStripStatusLabel LB_PPortStatus;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button bt_stopDetector;
        private System.Windows.Forms.Button bt_startDetector;
        private System.Windows.Forms.CheckedListBox ls_detectors;
    }
}

