namespace DoAnTinHoc
{
    partial class QLAVL
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
            this.dgvAVL = new System.Windows.Forms.DataGridView();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnXuatTangK = new System.Windows.Forms.Button();
            this.txtTangK = new System.Windows.Forms.TextBox();
            this.lblTangK = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.btnTim = new System.Windows.Forms.Button();
            this.lblChieuCao = new System.Windows.Forms.Label();
            this.lblSoNut = new System.Windows.Forms.Label();
            this.btnDanhSachIDBitrung = new System.Windows.Forms.Button();
            this.btnThongKe = new System.Windows.Forms.Button();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAVL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAVL
            // 
            this.dgvAVL.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAVL.Location = new System.Drawing.Point(12, 214);
            this.dgvAVL.Name = "dgvAVL";
            this.dgvAVL.RowHeadersWidth = 51;
            this.dgvAVL.RowTemplate.Height = 24;
            this.dgvAVL.Size = new System.Drawing.Size(1577, 220);
            this.dgvAVL.TabIndex = 0;
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(12, 12);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(99, 39);
            this.btnThem.TabIndex = 1;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(12, 57);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(99, 39);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(12, 102);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(99, 39);
            this.btnSua.TabIndex = 3;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(12, 147);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(99, 39);
            this.btnThoat.TabIndex = 4;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnXuatTangK
            // 
            this.btnXuatTangK.Location = new System.Drawing.Point(205, 147);
            this.btnXuatTangK.Name = "btnXuatTangK";
            this.btnXuatTangK.Size = new System.Drawing.Size(99, 39);
            this.btnXuatTangK.TabIndex = 5;
            this.btnXuatTangK.Text = "Xuất Tầng K";
            this.btnXuatTangK.UseVisualStyleBackColor = true;
            this.btnXuatTangK.Click += new System.EventHandler(this.btnXuatTangK_Click);
            // 
            // txtTangK
            // 
            this.txtTangK.Location = new System.Drawing.Point(319, 155);
            this.txtTangK.Name = "txtTangK";
            this.txtTangK.Size = new System.Drawing.Size(100, 22);
            this.txtTangK.TabIndex = 6;
            // 
            // lblTangK
            // 
            this.lblTangK.AutoSize = true;
            this.lblTangK.Location = new System.Drawing.Point(448, 161);
            this.lblTangK.Name = "lblTangK";
            this.lblTangK.Size = new System.Drawing.Size(129, 16);
            this.lblTangK.TabIndex = 7;
            this.lblTangK.Text = "Các node trên tầng k";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(139, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Nhập ID:";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(205, 6);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(100, 22);
            this.txtID.TabIndex = 9;
            // 
            // btnTim
            // 
            this.btnTim.Location = new System.Drawing.Point(343, 6);
            this.btnTim.Name = "btnTim";
            this.btnTim.Size = new System.Drawing.Size(99, 39);
            this.btnTim.TabIndex = 10;
            this.btnTim.Text = "Tìm";
            this.btnTim.UseVisualStyleBackColor = true;
            this.btnTim.Click += new System.EventHandler(this.btnTim_Click);
            // 
            // lblChieuCao
            // 
            this.lblChieuCao.AutoSize = true;
            this.lblChieuCao.Location = new System.Drawing.Point(657, 6);
            this.lblChieuCao.Name = "lblChieuCao";
            this.lblChieuCao.Size = new System.Drawing.Size(69, 16);
            this.lblChieuCao.TabIndex = 12;
            this.lblChieuCao.Text = "Chiều Cao";
            // 
            // lblSoNut
            // 
            this.lblSoNut.AutoSize = true;
            this.lblSoNut.Location = new System.Drawing.Point(657, 35);
            this.lblSoNut.Name = "lblSoNut";
            this.lblSoNut.Size = new System.Drawing.Size(47, 16);
            this.lblSoNut.TabIndex = 13;
            this.lblSoNut.Text = "Số Nút";
            // 
            // btnDanhSachIDBitrung
            // 
            this.btnDanhSachIDBitrung.Location = new System.Drawing.Point(142, 57);
            this.btnDanhSachIDBitrung.Name = "btnDanhSachIDBitrung";
            this.btnDanhSachIDBitrung.Size = new System.Drawing.Size(121, 52);
            this.btnDanhSachIDBitrung.TabIndex = 16;
            this.btnDanhSachIDBitrung.Text = "Danh Sách ID Bị Trùng";
            this.btnDanhSachIDBitrung.UseVisualStyleBackColor = true;
            this.btnDanhSachIDBitrung.Click += new System.EventHandler(this.btnDanhSachIDBitrung_Click);
            // 
            // btnThongKe
            // 
            this.btnThongKe.Location = new System.Drawing.Point(343, 96);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Size = new System.Drawing.Size(99, 39);
            this.btnThongKe.TabIndex = 17;
            this.btnThongKe.Text = "Thống Kê";
            this.btnThongKe.UseVisualStyleBackColor = true;
            this.btnThongKe.Click += new System.EventHandler(this.btnThongKe_Click);
            // 
            // dgvList
            // 
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Location = new System.Drawing.Point(12, 452);
            this.dgvList.Name = "dgvList";
            this.dgvList.RowHeadersWidth = 51;
            this.dgvList.RowTemplate.Height = 24;
            this.dgvList.Size = new System.Drawing.Size(1577, 246);
            this.dgvList.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 195);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 16);
            this.label2.TabIndex = 19;
            this.label2.Text = "Dữ liệu không bị trùng";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 437);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 16);
            this.label3.TabIndex = 20;
            this.label3.Text = "Dữ liệu bị trùng";
            // 
            // QLAVL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1601, 710);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvList);
            this.Controls.Add(this.btnThongKe);
            this.Controls.Add(this.btnDanhSachIDBitrung);
            this.Controls.Add(this.lblSoNut);
            this.Controls.Add(this.lblChieuCao);
            this.Controls.Add(this.btnTim);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTangK);
            this.Controls.Add(this.txtTangK);
            this.Controls.Add(this.btnXuatTangK);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.dgvAVL);
            this.Name = "QLAVL";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.dgvAVL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAVL;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnXuatTangK;
        private System.Windows.Forms.TextBox txtTangK;
        private System.Windows.Forms.Label lblTangK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Button btnTim;
        private System.Windows.Forms.Label lblChieuCao;
        private System.Windows.Forms.Label lblSoNut;
        private System.Windows.Forms.Button btnDanhSachIDBitrung;
        private System.Windows.Forms.Button btnThongKe;
        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}