namespace DoAnTinHoc
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ChucNangToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lưuFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DuLieuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BatDauChapAVLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.VeCayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ChucNangToolStripMenuItem,
            this.DuLieuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ChucNangToolStripMenuItem
            // 
            this.ChucNangToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lưuFileToolStripMenuItem});
            this.ChucNangToolStripMenuItem.Name = "ChucNangToolStripMenuItem";
            this.ChucNangToolStripMenuItem.Size = new System.Drawing.Size(93, 24);
            this.ChucNangToolStripMenuItem.Text = "Chức năng";
            this.ChucNangToolStripMenuItem.Click += new System.EventHandler(this.ChucNangToolStripMenuItem_Click);
            // 
            // lưuFileToolStripMenuItem
            // 
            this.lưuFileToolStripMenuItem.Name = "lưuFileToolStripMenuItem";
            this.lưuFileToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.lưuFileToolStripMenuItem.Text = "Lưu File";
            this.lưuFileToolStripMenuItem.Click += new System.EventHandler(this.lưuFileToolStripMenuItem_Click);
            // 
            // DuLieuToolStripMenuItem
            // 
            this.DuLieuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BatDauChapAVLToolStripMenuItem,
            this.VeCayToolStripMenuItem});
            this.DuLieuToolStripMenuItem.Name = "DuLieuToolStripMenuItem";
            this.DuLieuToolStripMenuItem.Size = new System.Drawing.Size(74, 24);
            this.DuLieuToolStripMenuItem.Text = "Dữ Liệu";
            // 
            // BatDauChapAVLToolStripMenuItem
            // 
            this.BatDauChapAVLToolStripMenuItem.Name = "BatDauChapAVLToolStripMenuItem";
            this.BatDauChapAVLToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.BatDauChapAVLToolStripMenuItem.Text = "Bắt Đầu Chạy";
            this.BatDauChapAVLToolStripMenuItem.Click += new System.EventHandler(this.BatDauChapAVLToolStripMenuItem_Click);
            // 
            // VeCayToolStripMenuItem
            // 
            this.VeCayToolStripMenuItem.Name = "VeCayToolStripMenuItem";
            this.VeCayToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.VeCayToolStripMenuItem.Text = "Vẽ Cây";
            this.VeCayToolStripMenuItem.Click += new System.EventHandler(this.VeCayToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ChucNangToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lưuFileToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem DuLieuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BatDauChapAVLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem VeCayToolStripMenuItem;
    }
}

