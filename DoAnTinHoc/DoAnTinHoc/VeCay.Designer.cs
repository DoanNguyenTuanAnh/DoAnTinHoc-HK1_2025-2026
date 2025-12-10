namespace DoAnTinHoc
{
    partial class VeCay
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
            this.btnVeCay = new System.Windows.Forms.Button();
            this.txtVeCay = new System.Windows.Forms.TextBox();
            this.plVeCay = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnVeCay
            // 
            this.btnVeCay.Location = new System.Drawing.Point(12, 12);
            this.btnVeCay.Name = "btnVeCay";
            this.btnVeCay.Size = new System.Drawing.Size(75, 23);
            this.btnVeCay.TabIndex = 0;
            this.btnVeCay.Text = "Vẽ Cây";
            this.btnVeCay.UseVisualStyleBackColor = true;
            this.btnVeCay.Click += new System.EventHandler(this.btnVeCay_Click);
            // 
            // txtVeCay
            // 
            this.txtVeCay.Location = new System.Drawing.Point(107, 13);
            this.txtVeCay.Name = "txtVeCay";
            this.txtVeCay.Size = new System.Drawing.Size(100, 22);
            this.txtVeCay.TabIndex = 1;
            this.txtVeCay.Text = "100";
            // 
            // plVeCay
            // 
            this.plVeCay.AutoScroll = true;
            this.plVeCay.Location = new System.Drawing.Point(12, 56);
            this.plVeCay.Name = "plVeCay";
            this.plVeCay.Size = new System.Drawing.Size(1733, 686);
            this.plVeCay.TabIndex = 2;
            this.plVeCay.Paint += new System.Windows.Forms.PaintEventHandler(this.plVeCay_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(245, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(275, 32);
            this.label1.TabIndex = 3;
            this.label1.Text = "Cây chỉ vẽ được khi dữ liệu không vượt quá 14\r\n\r\n";
            // 
            // VeCay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1757, 754);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.plVeCay);
            this.Controls.Add(this.txtVeCay);
            this.Controls.Add(this.btnVeCay);
            this.Name = "VeCay";
            this.Text = "VeCay";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnVeCay;
        private System.Windows.Forms.TextBox txtVeCay;
        private System.Windows.Forms.Panel plVeCay;
        private System.Windows.Forms.Label label1;
    }
}