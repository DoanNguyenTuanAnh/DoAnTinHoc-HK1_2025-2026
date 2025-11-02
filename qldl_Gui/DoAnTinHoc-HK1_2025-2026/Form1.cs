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
using System.Globalization;

namespace DoAnTinHoc_HK1_2025_2026
{
    public partial class Form1 : Form // Thay thế Form1 bằng tên Form của bạn
    {
        // Khai báo đối tượng AVLTree
        private AVLTree customerTree;
        private readonly Dictionary<string, Func<CustomerRecord, int>> keySelectors = new Dictionary<string, Func<CustomerRecord, int>>()
        {
            {"Customer ID", r => r.CustomerID},
            {"Age", r => r.Age},
            {"Purchase Amount", r => r.PurchaseAmount},
            {"Previous Purchases", r => r.PreviousPurchases}
            // Thêm các trường int khác nếu cần
        };
        public Form1()
        {
            InitializeComponent();
            InitializeKeySelectorComboBox();
            if (cmbSortingKey.Items.Count > 0 && cmbSortingKey.SelectedIndex == -1)
            {
                cmbSortingKey.SelectedIndex = 0;
            }

        }

        // --- HÀM XỬ LÝ ĐỌC FILE CSV VÀ XÂY DỰNG CÂY ---

        private void LoadDataFromCsv(string filePath)
        {
            // Lấy khóa mặc định là "ID Khách hàng"
            string defaultKey = "ID Khách hàng";
            string selectedKey = defaultKey;

            // 1. XÁC ĐỊNH KHÓA HIỆN TẠI VÀ THÊM KIỂM TRA NULL
            if (cmbSortingKey.SelectedItem != null)
            {
                selectedKey = cmbSortingKey.SelectedItem.ToString();
            }
            // Nếu SelectedItem là null, selectedKey vẫn là defaultKey

            // Đảm bảo khóa đã chọn tồn tại trong Dictionary
            if (!keySelectors.ContainsKey(selectedKey))
            {
                selectedKey = defaultKey;
            }

            Func<CustomerRecord, int> currentKeySelector = keySelectors[selectedKey];

            try
            {
                // 2. KHỞI TẠO CÂY AVL MỚI với KeySelector đã chọn
                customerTree = new AVLTree(currentKeySelector);

                // Đọc file CSV (Giữ nguyên logic cũ)
                // Cần đảm bảo file tồn tại trước khi đọc
                if (!File.Exists(filePath))
                {
                    MessageBox.Show($"Không tìm thấy file dữ liệu: {filePath}", "Lỗi File");
                    return;
                }

                string[] lines = File.ReadAllLines(filePath, Encoding.UTF8).Skip(1).ToArray();

                foreach (string line in lines)
                {
                    // ... (Logic đọc và phân tích dữ liệu CSV) ...

                    string[] values = line.Split(',');
                    if (values.Length >= 18)
                    {
                        int i = 0;
                        int customerID = int.Parse(values[i++].Trim());
                        int age = int.Parse(values[i++].Trim());
                        CGender gender = (values[i++].Trim().ToLower() == "male") ? CGender.Male : CGender.Female;
                        string itemPurchased = values[i++].Trim();
                        string category = values[i++].Trim();
                        int purchaseAmount = int.Parse(values[i++].Trim());
                        string location = values[i++].Trim();
                        string size = values[i++].Trim();
                        string color = values[i++].Trim();
                        string season = values[i++].Trim();
                        float reviewRating = float.Parse(values[i++].Trim(), CultureInfo.InvariantCulture);
                        string subscriptionStatus = values[i++].Trim();
                        string shippingType = values[i++].Trim();
                        string discountApplied = values[i++].Trim();
                        string promoCodeUsed = values[i++].Trim();
                        int previousPurchases = int.Parse(values[i++].Trim());
                        string paymentMethod = values[i++].Trim();
                        string frequencyOfPurchases = values[i].Trim();

                        CustomerRecord record = new CustomerRecord(
                            customerID, age, gender, itemPurchased, category,
                            purchaseAmount, location, size, color, season,
                            reviewRating, subscriptionStatus, shippingType,
                            discountApplied, promoCodeUsed, previousPurchases,
                            paymentMethod, frequencyOfPurchases
                        );

                        // 3. Chèn vào Cây AVL mới
                        customerTree.Insert(record);
                    }
                }

                MessageBox.Show($"Đã tải và xây dựng cây AVL theo '{selectedKey}' với {customerTree.CountNodes()} bản ghi.", "Thành công");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi Hệ thống khi tải dữ liệu: {ex.Message}", "Lỗi");
            }
        }

        // Sự kiện tải/tái xây dựng cây
        private void btnLoadAndStats_Click(object sender, EventArgs e)
        {
            // Tái tạo cây mỗi khi nút được nhấn (đặc biệt sau khi thay đổi khóa trong cmbSortingKey)
            string filePath = "data.csv";
            LoadDataFromCsv(filePath);
            RefreshDataGrid();
            UpdateStats();
        }

        // --- HÀM LÀM MỚI DATAGRIDVIEW ---

        private void RefreshDataGrid()
        {
            if (customerTree == null) return;
            List<CustomerRecord> allRecords = customerTree.GetAllRecords();
            dgvCustomers.DataSource = null;
            dgvCustomers.DataSource = allRecords;
        }

       
        private void HighlightRecord(int searchID)
        {
            // 1. Đảm bảo DataGridView đã có dữ liệu và có cột CustomerID
            if (dgvCustomers.Rows.Count == 0 || dgvCustomers.Columns.Count == 0)
            {
                return;
            }

            // 2. Tìm chỉ mục (index) của cột CustomerID
            int idColumnIndex = -1;
            // Tìm cột bằng tên thuộc tính (CustomerID) của CustomerRecord
            if (dgvCustomers.Columns.Contains("CustomerID"))
            {
                idColumnIndex = dgvCustomers.Columns["CustomerID"].Index;
            }
            else
            {
                // Nếu không tìm thấy cột tên "CustomerID", có thể thoát hoặc log lỗi
                return;
            }

            // 3. Đặt lại màu nền của tất cả các hàng
            foreach (DataGridViewRow row in dgvCustomers.Rows)
            {
                row.DefaultCellStyle.BackColor = Color.White; // Hoặc Color.Empty để dùng màu mặc định
                row.Selected = false; // Bỏ chọn
            }

            // 4. Duyệt qua từng hàng để tìm bản ghi khớp
            bool found = false;
            foreach (DataGridViewRow row in dgvCustomers.Rows)
            {
                // Tránh hàng trống cuối cùng nếu AllowUserToAddRows là true
                if (row.IsNewRow) continue;

                // Lấy giá trị ID từ ô tại cột CustomerID
                // Cần đảm bảo giá trị này không phải là null và có thể chuyển sang int
                if (row.Cells[idColumnIndex].Value != null &&
                    int.TryParse(row.Cells[idColumnIndex].Value.ToString(), out int currentID))
                {
                    if (currentID == searchID)
                    {
                        // Đánh dấu (highlight) hàng được tìm thấy
                        row.DefaultCellStyle.BackColor = Color.Yellow; // Hoặc một màu nổi bật khác
                        row.Selected = true; // Chọn hàng đó

                        // Cuộn DataGridView đến hàng này (scroll into view)
                        dgvCustomers.FirstDisplayedScrollingRowIndex = row.Index;

                        found = true;
                        break;
                    }
                }
            }

            // (Tùy chọn) Nếu không tìm thấy, có thể hiển thị thông báo.
            // Tuy nhiên, hàm tìm kiếm đã báo cáo, nên bước này có thể bỏ qua.
        }
        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            if (!int.TryParse(txtInputID.Text, out int searchID))
            {
                MessageBox.Show("Vui lòng nhập ID hợp lệ.", "Lỗi nhập liệu");
                return;
            }

            CustomerRecord foundRecord = customerTree.Search(searchID);

            if (foundRecord != null)
            {
                // Tùy chọn: Hiển thị thông tin tìm thấy lên các TextBox khác (ví dụ: txtAge, txtCategory)
                // txtInputAge.Text = foundRecord.Age.ToString();

                MessageBox.Show($"Tìm thấy khách hàng ID {searchID}: Category = {foundRecord.Category}.", "Tìm kiếm thành công");

                HighlightRecord(searchID);
            }
            else
            {
                MessageBox.Show($"Không tìm thấy khách hàng ID {searchID}.", "Không tìm thấy");
            }
        }
        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            if (customerTree == null)
            {
                MessageBox.Show("Vui lòng tải dữ liệu trước.", "Lỗi");
                return;
            }

            // 1. Lấy ID Phụ (CustomerID) và Khóa Chính (Age, PurchaseAmount,...)
            // Cần 2 TextBox hoặc cơ chế chọn hàng để lấy 2 giá trị này
            if (!int.TryParse(txtInputID.Text, out int primaryKey))
            {
                MessageBox.Show("Vui lòng nhập giá trị Khóa Chính (Key Value) hợp lệ.", "Lỗi nhập liệu");
                return;
            }

            // Lấy ID từ bản ghi đầu tiên có khóa trùng (Giả định đơn giản cho mục đích demo)
            CustomerRecord recordToDelete = customerTree.Search(primaryKey);
            if (recordToDelete == null)
            {
                MessageBox.Show($"Không tìm thấy bản ghi có Khóa Chính = {primaryKey} để xóa.", "Lỗi xóa");
                return;
            }

            int secondaryID = recordToDelete.CustomerID; // Lấy ID duy nhất của bản ghi đầu tiên

            // Hỏi xác nhận
            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa khách hàng ID: {secondaryID} (Khóa Chính: {primaryKey}) không?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            // 2. Thực hiện xóa trong Cây AVL (Cần cả 2 khóa)
            customerTree.Delete(primaryKey, secondaryID);

            // 3. Cập nhật giao diện
            RefreshDataGrid();
            UpdateStats();

            MessageBox.Show($"Đã xóa thành công Khách hàng ID {secondaryID}.", "Xóa thành công");
        
        }
        // Trong lớp Form1.cs

        private void UpdateStats()
        {
            if (customerTree.CountNodes() > 0)
            {
                int height = customerTree.GetTreeHeight();
                int totalNodes = customerTree.CountNodes();
                int leafNodes = customerTree.CountLeafNodes();

                // Giả sử bạn có các Label: lblHeight, lblTotalNodes, lblLeafNodes
                lblHeight.Text = $"Chiều cao Cây AVL: {height}";
                lblTotalNodes.Text = $"Tổng số Nút: {totalNodes}";
                lblLeafNodes.Text = $"Số Nút Lá: {leafNodes}";
            }
            else
            {
                // Xử lý trường hợp cây rỗng
                lblHeight.Text = "Chiều cao Cây AVL: 0";
                lblTotalNodes.Text = "Tổng số Nút: 0";
                lblLeafNodes.Text = "Số Nút Lá: 0";
            }
        }
        // Đặt đoạn này trong lớp Form1
        private void InitializeKeySelectorComboBox()
        {
            // 1. Nạp các tên khóa (Keys) từ Dictionary vào ComboBox
            cmbSortingKey.Items.Clear();
            cmbSortingKey.Items.AddRange(keySelectors.Keys.ToArray());

            // 2. Thiết lập mục đầu tiên làm mặc định (tránh lỗi null)
            if (cmbSortingKey.Items.Count > 0)
            {
                cmbSortingKey.SelectedIndex = 0; // Chọn "ID Khách hàng" (mục đầu tiên)
            }
        }
        private void cmbSortingKey_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Nếu cây đã có dữ liệu, ta tiến hành TÁI TẠO CÂY với khóa mới.
            if (customerTree != null && customerTree.CountNodes() > 0)
            {
                // Gọi lại hàm Tải (LoadAndStats). Hàm này sẽ đọc khóa mới, 
                // khởi tạo lại cây và cập nhật giao diện.
                btnLoadAndStats_Click(sender, e);
            }
        }

     
    }
}