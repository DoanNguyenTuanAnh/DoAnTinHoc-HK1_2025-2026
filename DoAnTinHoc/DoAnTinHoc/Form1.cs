using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;

namespace DoAnTinHoc
{
    [Serializable]
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void lưuFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool ketQuaGhiFile = TruyCap.ghiFile("data.csv");
            if (ketQuaGhiFile)
                MessageBox.Show("Đã ghi file", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Ghi file thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void BatDauChapAVLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QLAVL qLAVL = new QLAVL();
            qLAVL.Show();
        } 
        private void VeCayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TruyCap dataAccess = TruyCap.khoiTao();
            AVLTree currentTree = dataAccess.getDanhSachCay().FirstOrDefault();

            if (currentTree == null || currentTree.Root == null)
            {
                MessageBox.Show("Cây AVL chưa có dữ liệu.", "Lỗi Truy Cập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            VeCay veCay = new VeCay(currentTree);
            veCay.Show();
        }

        private void ChucNangToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}