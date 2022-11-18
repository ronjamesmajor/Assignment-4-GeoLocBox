namespace _270_GeoLocBox
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
            this.lblTime = new System.Windows.Forms.Label();
            this.lblLight = new System.Windows.Forms.Label();
            this.lblHumidity = new System.Windows.Forms.Label();
            this.lblTemp = new System.Windows.Forms.Label();
            this.lblLat = new System.Windows.Forms.Label();
            this.lblLong = new System.Windows.Forms.Label();
            this.lblAlt = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(17, 9);
            this.lblTime.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(112, 51);
            this.lblTime.TabIndex = 0;
            this.lblTime.Text = "Time:";
            // 
            // lblLight
            // 
            this.lblLight.AutoSize = true;
            this.lblLight.Location = new System.Drawing.Point(17, 65);
            this.lblLight.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.lblLight.Name = "lblLight";
            this.lblLight.Size = new System.Drawing.Size(114, 51);
            this.lblLight.TabIndex = 0;
            this.lblLight.Text = "Light:";
            // 
            // lblHumidity
            // 
            this.lblHumidity.AutoSize = true;
            this.lblHumidity.Location = new System.Drawing.Point(17, 116);
            this.lblHumidity.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.lblHumidity.Name = "lblHumidity";
            this.lblHumidity.Size = new System.Drawing.Size(183, 51);
            this.lblHumidity.TabIndex = 0;
            this.lblHumidity.Text = "Humidity:";
            // 
            // lblTemp
            // 
            this.lblTemp.AutoSize = true;
            this.lblTemp.Location = new System.Drawing.Point(17, 167);
            this.lblTemp.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.lblTemp.Name = "lblTemp";
            this.lblTemp.Size = new System.Drawing.Size(241, 51);
            this.lblTemp.TabIndex = 0;
            this.lblTemp.Text = "Temperature:";
            // 
            // lblLat
            // 
            this.lblLat.AutoSize = true;
            this.lblLat.Location = new System.Drawing.Point(541, 30);
            this.lblLat.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.lblLat.Name = "lblLat";
            this.lblLat.Size = new System.Drawing.Size(166, 51);
            this.lblLat.TabIndex = 0;
            this.lblLat.Text = "Latitude:";
            // 
            // lblLong
            // 
            this.lblLong.AutoSize = true;
            this.lblLong.Location = new System.Drawing.Point(541, 86);
            this.lblLong.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.lblLong.Name = "lblLong";
            this.lblLong.Size = new System.Drawing.Size(200, 51);
            this.lblLong.TabIndex = 0;
            this.lblLong.Text = "Longitude:";
            // 
            // lblAlt
            // 
            this.lblAlt.AutoSize = true;
            this.lblAlt.Location = new System.Drawing.Point(544, 137);
            this.lblAlt.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.lblAlt.Name = "lblAlt";
            this.lblAlt.Size = new System.Drawing.Size(163, 51);
            this.lblAlt.TabIndex = 0;
            this.lblAlt.Text = "Altitude:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(20F, 50F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(1095, 232);
            this.Controls.Add(this.lblAlt);
            this.Controls.Add(this.lblTemp);
            this.Controls.Add(this.lblLight);
            this.Controls.Add(this.lblLong);
            this.Controls.Add(this.lblLat);
            this.Controls.Add(this.lblHumidity);
            this.Controls.Add(this.lblTime);
            this.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(8, 10, 8, 10);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lblTime;
        private Label lblLight;
        private Label lblHumidity;
        private Label lblTemp;
        private Label lblLat;
        private Label lblLong;
        private Label lblAlt;
    }
}