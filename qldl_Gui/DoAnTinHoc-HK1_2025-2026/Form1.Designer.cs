namespace DoAnTinHoc_HK1_2025_2026
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
            this.btnLoadAndStats = new System.Windows.Forms.Button();
            this.dgvCustomers = new System.Windows.Forms.DataGridView();
            this.lblHeight = new System.Windows.Forms.Label();
            this.lblTotalNodes = new System.Windows.Forms.Label();
            this.lblLeafNodes = new System.Windows.Forms.Label();
            this.txtInputID = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbSortingKey = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoadAndStats
            // 
            this.btnLoadAndStats.Location = new System.Drawing.Point(12, 12);
            this.btnLoadAndStats.Name = "btnLoadAndStats";
            this.btnLoadAndStats.Size = new System.Drawing.Size(75, 23);
            this.btnLoadAndStats.TabIndex = 0;
            this.btnLoadAndStats.Text = "Stats";
            this.btnLoadAndStats.UseVisualStyleBackColor = true;
            this.btnLoadAndStats.Click += new System.EventHandler(this.btnLoadAndStats_Click);
            // 
            // dgvCustomers
            // 
            this.dgvCustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCustomers.Location = new System.Drawing.Point(12, 128);
            this.dgvCustomers.Name = "dgvCustomers";
            this.dgvCustomers.RowHeadersWidth = 51;
            this.dgvCustomers.RowTemplate.Height = 24;
            this.dgvCustomers.Size = new System.Drawing.Size(776, 310);
            this.dgvCustomers.TabIndex = 1;
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(12, 38);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(60, 16);
            this.lblHeight.TabIndex = 2;
            this.lblHeight.Text = "lblHeight";
            // 
            // lblTotalNodes
            // 
            this.lblTotalNodes.AutoSize = true;
            this.lblTotalNodes.Location = new System.Drawing.Point(12, 63);
            this.lblTotalNodes.Name = "lblTotalNodes";
            this.lblTotalNodes.Size = new System.Drawing.Size(93, 16);
            this.lblTotalNodes.TabIndex = 3;
            this.lblTotalNodes.Text = "lblTotalNodes";
            // 
            // lblLeafNodes
            // 
            this.lblLeafNodes.AutoSize = true;
            this.lblLeafNodes.Location = new System.Drawing.Point(12, 89);
            this.lblLeafNodes.Name = "lblLeafNodes";
            this.lblLeafNodes.Size = new System.Drawing.Size(88, 16);
            this.lblLeafNodes.TabIndex = 4;
            this.lblLeafNodes.Text = "lblLeafNodes";
            // 
            // txtInputID
            // 
            this.txtInputID.Location = new System.Drawing.Point(688, 13);
            this.txtInputID.Name = "txtInputID";
            this.txtInputID.Size = new System.Drawing.Size(100, 22);
            this.txtInputID.TabIndex = 5;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(713, 41);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 7;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click_1);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(713, 70);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(605, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "CustomerID";
            // 
            // cmbSortingKey
            // 
            this.cmbSortingKey.FormattingEnabled = true;
            this.cmbSortingKey.Location = new System.Drawing.Point(252, 8);
            this.cmbSortingKey.Name = "cmbSortingKey";
            this.cmbSortingKey.Size = new System.Drawing.Size(121, 24);
            this.cmbSortingKey.TabIndex = 10;
            this.cmbSortingKey.SelectedIndexChanged += new System.EventHandler(this.cmbSortingKey_SelectedIndexChanged);
         
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(148, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 16);
            this.label2.TabIndex = 11;
            this.label2.Text = "hoose Key Sort";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbSortingKey);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtInputID);
            this.Controls.Add(this.lblLeafNodes);
            this.Controls.Add(this.lblTotalNodes);
            this.Controls.Add(this.lblHeight);
            this.Controls.Add(this.dgvCustomers);
            this.Controls.Add(this.btnLoadAndStats);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoadAndStats;
        private System.Windows.Forms.DataGridView dgvCustomers;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.Label lblTotalNodes;
        private System.Windows.Forms.Label lblLeafNodes;
        private System.Windows.Forms.TextBox txtInputID;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbSortingKey;
        private System.Windows.Forms.Label label2;
    }
}

