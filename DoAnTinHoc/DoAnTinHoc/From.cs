using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DoAnTinHoc
{
    public partial class From : Form
    {
        public From()
        {
            InitializeComponent();
            this.Load += From_Load;
        }

        private void From_Load(object sender, EventArgs e)
        {
            // Đặt file Data.csv vào cùng thư mục chạy (bin\Debug hoặc bin\Release)
            string csvPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data.csv");

            try
            {
                DataTable table = CsvReader.ReadCsvToDataTable(csvPath);
                dataGridView1.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể đọc file CSV: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}