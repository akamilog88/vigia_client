namespace HomeAlarm
{
    partial class SensorGroupCtrl
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

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar 
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.FL_sensorContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // FL_sensorContainer
            // 
            this.FL_sensorContainer.AutoScroll = true;
            this.FL_sensorContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FL_sensorContainer.Location = new System.Drawing.Point(0, 0);
            this.FL_sensorContainer.Name = "FL_sensorContainer";
            this.FL_sensorContainer.Size = new System.Drawing.Size(150, 150);
            this.FL_sensorContainer.TabIndex = 0;
            // 
            // SensorGroupCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.FL_sensorContainer);
            this.Name = "SensorGroupCtrl";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel FL_sensorContainer;
    }
}
