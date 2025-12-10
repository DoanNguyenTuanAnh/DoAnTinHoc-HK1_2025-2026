using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DoAnTinHoc
{
    public partial class QLAVL : Form
    {
        private TruyCap dataAccess;
        private AVLTree currentTree;
        // Tên file đã được kiểm tra và đồng bộ với Form1.cs
        private const string DATA_FILE_NAME = "data.csv";

        public QLAVL()
        {
            InitializeComponent();
            dataAccess = TruyCap.khoiTao();

            // 1. Tải dữ liệu từ file
            if (TruyCap.docFile(DATA_FILE_NAME))
            {
                // Nếu tải thành công, lấy cây đầu tiên (hoặc tạo mới nếu chưa có)
                if (dataAccess.getDanhSachCay().Any())
                {
                    currentTree = dataAccess.getDanhSachCay().First();
                }
                else
                {
                    currentTree = new AVLTree();
                    dataAccess.getDanhSachCay().Add(currentTree);
                }

            }
            else
            {
                // Nếu tải thất bại (file không tồn tại/lỗi), khởi tạo cây mới
                currentTree = new AVLTree();
                dataAccess.getDanhSachCay().Add(currentTree);

            }

            // 2. Cấu hình DataGridView
            SetupDataGridView();

            // 3. Hiển thị dữ liệu ban đầu
            LoadDataToGrid();
        }

        // Hàm hỗ trợ: Lấy danh sách CustomerRecord từ cây AVL bằng cách duyệt In-order
        private List<CustomerRecord> GetDataForDisplay(AVLNode node, List<CustomerRecord> list)
        {
            if (node != null)
            {
                GetDataForDisplay(node.Left, list);
                list.Add(node.Data);
                GetDataForDisplay(node.Right, list);
            }
            return list;
        }

        // Hàm hỗ trợ: Cấu hình các cột cho DataGridView
        private void SetupDataGridView()
        {
            // Tắt chế độ tự động tạo cột
            dgvAVL.AutoGenerateColumns = false;
            // Xóa các cột cũ
            dgvAVL.Columns.Clear();

            // ------------------- CÁC CỘT CHÍNH -------------------

            // 1. Customer ID (Mã KH) - Khóa chính, chỉ đọc
            dgvAVL.Columns.Add("colID", "Mã KH");
            dgvAVL.Columns["colID"].DataPropertyName = "ID";
            dgvAVL.Columns["colID"].ReadOnly = true;
            dgvAVL.Columns["colID"].Width = 60;

            // 2. Age (Tuổi)
            dgvAVL.Columns.Add("colAge", "Tuổi");
            dgvAVL.Columns["colAge"].DataPropertyName = "Age";
            dgvAVL.Columns["colAge"].Width = 50;

            // 3. Gender (Giới tính)
            dgvAVL.Columns.Add("colGender", "Giới tính");
            dgvAVL.Columns["colGender"].DataPropertyName = "Gender";
            dgvAVL.Columns["colGender"].Width = 80;

            // 4. Item Purchased (Sản phẩm)
            dgvAVL.Columns.Add("colItem", "Sản phẩm Mua");
            dgvAVL.Columns["colItem"].DataPropertyName = "ItemPurchased";

            // 5. Category (Danh mục)
            dgvAVL.Columns.Add("colCategory", "Danh mục");
            dgvAVL.Columns["colCategory"].DataPropertyName = "Category";

            // 6. Purchase Amount (Số tiền)
            dgvAVL.Columns.Add("colAmount", "Số tiền (USD)");
            dgvAVL.Columns["colAmount"].DataPropertyName = "PurchaseAmount";
            dgvAVL.Columns["colAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // 7. Location (Địa điểm)
            dgvAVL.Columns.Add("colLocation", "Địa điểm");
            dgvAVL.Columns["colLocation"].DataPropertyName = "Location";

            // 8. Size (Kích cỡ)
            dgvAVL.Columns.Add("colSize", "Kích cỡ");
            dgvAVL.Columns["colSize"].DataPropertyName = "Size";
            dgvAVL.Columns["colSize"].Width = 60;

            // 9. Color (Màu sắc)
            dgvAVL.Columns.Add("colColor", "Màu sắc");
            dgvAVL.Columns["colColor"].DataPropertyName = "Color";

            // 10. Season (Mùa)
            dgvAVL.Columns.Add("colSeason", "Mùa");
            dgvAVL.Columns["colSeason"].DataPropertyName = "Season";
            dgvAVL.Columns["colSeason"].Width = 80;

            // 11. Review Rating (Đánh giá)
            dgvAVL.Columns.Add("colRating", "Đánh giá");
            dgvAVL.Columns["colRating"].DataPropertyName = "ReviewRating";
            dgvAVL.Columns["colRating"].Width = 70;

            // 12. Subscription Status (Trạng thái đăng ký)
            dgvAVL.Columns.Add("colSubscription", "Đã Đăng ký");
            dgvAVL.Columns["colSubscription"].DataPropertyName = "SubscriptionStatus";
            dgvAVL.Columns["colSubscription"].Width = 80;

            // 13. Shipping Type (Loại vận chuyển)
            dgvAVL.Columns.Add("colShipping", "Vận chuyển");
            dgvAVL.Columns["colShipping"].DataPropertyName = "ShippingType";

            // 14. Discount Applied (Áp dụng KM)
            dgvAVL.Columns.Add("colDiscount", "Giảm giá");
            dgvAVL.Columns["colDiscount"].DataPropertyName = "DiscountApplied";
            dgvAVL.Columns["colDiscount"].Width = 60;

            // 15. Promo Code Used (Dùng mã KM)
            dgvAVL.Columns.Add("colPromo", "Mã KM");
            dgvAVL.Columns["colPromo"].DataPropertyName = "PromoCodeUsed";
            dgvAVL.Columns["colPromo"].Width = 60;

            // 16. Previous Purchases (Lần mua trước)
            dgvAVL.Columns.Add("colPrevPurchases", "SL Mua trước");
            dgvAVL.Columns["colPrevPurchases"].DataPropertyName = "PreviousPurchases";
            dgvAVL.Columns["colPrevPurchases"].Width = 80;

            // 17. Payment Method (Phương thức TT)
            dgvAVL.Columns.Add("colPayment", "Thanh toán");
            dgvAVL.Columns["colPayment"].DataPropertyName = "PaymentMethod";

            // 18. Frequency of Purchases (Tần suất mua)
            dgvAVL.Columns.Add("colFrequency", "Tần suất mua");
            dgvAVL.Columns["colFrequency"].DataPropertyName = "FrequencyOfPurchases";

            // ------------------- THIẾT LẬP HIỂN THỊ -------------------
            dgvAVL.Columns["colID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvAVL.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // Hàm chính: Tải dữ liệu từ cây AVL lên DataGridView
        private void LoadDataToGrid()
        {
            dgvAVL.DataSource = null;
            List<CustomerRecord> displayList = new List<CustomerRecord>();
            GetDataForDisplay(currentTree.Root, displayList);
            dgvAVL.DataSource = displayList;
            UpdateStats();

        }
        private void UpdateStats()
        {
            if (currentTree.DemNode() > 0)
            {
                int height = currentTree.DemChieuCao();
                int totalNodes = currentTree.DemNode();


                // Giả sử bạn có các Label: lblHeight, lblTotalNodes, lblLeafNodes
                lblChieuCao.Text = $"Chiều cao Cây AVL: {height}";
                lblSoNut.Text = $"Tổng số Nút: {totalNodes}";
            }
            else
            {
                // Xử lý trường hợp cây rỗng
                lblChieuCao.Text = "Chiều cao Cây AVL: 0";
                lblSoNut.Text = "Tổng số Nút: 0";
            }
        }
        private CustomerRecord CreateRecordFromInputs(bool isNewRecord)
        {
            if (!int.TryParse(txtID.Text, out int id))
            {
                MessageBox.Show("Mã KH phải là số nguyên hợp lệ.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            if (isNewRecord && id <= 0)
            {
                MessageBox.Show("Mã khách hàng phải là số nguyên dương.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            int age = 30;
            CGender gender = CGender.Male; // Ví dụ: Enum.TryParse(cmbGender.Text, out gender);
            string itemPurchased = "Item A"; // Ví dụ: txtItemPurchased.Text
            string category = "Category X";
            int purchaseAmount = 100; // Ví dụ: int.TryParse(txtAmount.Text, out purchaseAmount)
            string location = "Hanoi";
            string size = "M";
            string color = "White";
            string season = "All";
            float reviewRating = 4.5f;
            CSubscriptionStatus subStatus = CSubscriptionStatus.No;
            string shippingType = "Standard";
            CDiscountApplied discount = CDiscountApplied.No;
            CPromoCodeUsed promo = CPromoCodeUsed.No;
            int prevPurchases = 1;
            string paymentMethod = "Credit Card";
            string frequencyOfPurchases = "Quarterly";


            CustomerRecord record = new CustomerRecord(
              customerID: id,
              age: age,
              gender: gender,
              itemPurchased: itemPurchased,
              category: category,
              purchaseAmount: purchaseAmount,
              location: location,
              size: size,
              color: color,
              season: season,
              reviewRating: reviewRating,
              subscriptionStatus: subStatus,
              shippingType: shippingType,
              discountApplied: discount,
              promoCodeUsed: promo,
              previousPurchases: prevPurchases,
              paymentMethod: paymentMethod,
              frequencyOfPurchases: frequencyOfPurchases
            );

            return record;
        }



        // ------------------- Các sự kiện chính -------------------

        private void btnThem_Click(object sender, EventArgs e)
        {
            QLData dataForm = new QLData();
            if (dataForm.ShowDialog() == DialogResult.OK)
            {
                CustomerRecord newRecord = dataForm.NewCustomerRecord;
                if (newRecord != null)
                {
                    // SỬA LỖI: AVLTree.Insert đã xử lý cả chèn mới và chèn trùng lặp.
                    // Ta chỉ cần gọi Insert và cập nhật DataGridView, không cần kiểm tra Search.
                    try
                    {
                        currentTree.Insert(newRecord);
                        LoadDataToGrid(); // Cập nhật DataGridView sau khi thêm/chèn trùng lặp
                        MessageBox.Show("Thêm khách hàng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi thêm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtID.Text, out int idToDelete))
            {
                MessageBox.Show("Vui lòng nhập Mã KH hợp lệ để xóa.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (currentTree.Search(idToDelete) == null)
            {
                MessageBox.Show($"Không tìm thấy Mã KH **{idToDelete}** để xóa.", "Lỗi Xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa khách hàng có Mã KH **{idToDelete}**?", "Xác nhận Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    currentTree.Delete(idToDelete);
                    LoadDataToGrid();
                    MessageBox.Show("Xóa khách hàng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvAVL.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để sửa.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. Lấy ID của bản ghi được chọn
            if (!int.TryParse(dgvAVL.SelectedRows[0].Cells["colID"].Value.ToString(), out int idToEdit))
            {
                MessageBox.Show("Lỗi lấy ID bản ghi.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. TÌM AVLNode trong cây AVL bằng hàm FindNode MỚI (Fix Lỗi 1)
            AVLNode nodeToEdit = currentTree.FindNode(idToEdit);
            if (nodeToEdit == null)
            {
                MessageBox.Show($"Không tìm thấy Mã KH **{idToEdit}** trong cây.", "Lỗi Sửa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadDataToGrid();
                return;
            }

            // 3. Tạo một bản sao chép dữ liệu bằng hàm Clone() MỚI (Fix Lỗi 2)
            CustomerRecord recordToPass = (CustomerRecord)nodeToEdit.Data.Clone();

            // 4. Mở form QLData để chỉnh sửa
            QLData dataForm = new QLData(recordToPass);

            if (dataForm.ShowDialog() == DialogResult.OK)
            {
                // 5. Lấy CustomerRecord đã được cập nhật
                CustomerRecord updatedRecord = dataForm.NewCustomerRecord;

                if (updatedRecord != null && updatedRecord.ID == idToEdit)
                {
                    // 6. Cập nhật dữ liệu vào node.Data hiện tại (không cần chèn lại)
                    nodeToEdit.Data.Age = updatedRecord.Age;
                    nodeToEdit.Data.Gender = updatedRecord.Gender;
                    nodeToEdit.Data.ItemPurchased = updatedRecord.ItemPurchased;
                    nodeToEdit.Data.Category = updatedRecord.Category;
                    nodeToEdit.Data.PurchaseAmount = updatedRecord.PurchaseAmount;
                    nodeToEdit.Data.Location = updatedRecord.Location;
                    nodeToEdit.Data.Size = updatedRecord.Size;
                    nodeToEdit.Data.Color = updatedRecord.Color;
                    nodeToEdit.Data.Season = updatedRecord.Season;
                    nodeToEdit.Data.ReviewRating = updatedRecord.ReviewRating;
                    nodeToEdit.Data.SubscriptionStatus = updatedRecord.SubscriptionStatus;
                    nodeToEdit.Data.ShippingType = updatedRecord.ShippingType;
                    nodeToEdit.Data.DiscountApplied = updatedRecord.DiscountApplied;
                    nodeToEdit.Data.PromoCodeUsed = updatedRecord.PromoCodeUsed;
                    nodeToEdit.Data.PreviousPurchases = updatedRecord.PreviousPurchases;
                    nodeToEdit.Data.PaymentMethod = updatedRecord.PaymentMethod;
                    nodeToEdit.Data.FrequencyOfPurchases = updatedRecord.FrequencyOfPurchases;

                    // 7. Tải lại DataGridView
                    LoadDataToGrid();

                    dgvAVL.ClearSelection();

                    MessageBox.Show($"Cập nhật thông tin khách hàng Mã KH **{idToEdit}** thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Lỗi cập nhật: Dữ liệu trả về không hợp lệ.", "Lỗi Sửa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtID.Text, out int idToSearch))
            {
                MessageBox.Show("Vui lòng nhập Mã KH hợp lệ để tìm kiếm.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            CustomerRecord foundRecord = currentTree.Search(idToSearch);

            if (foundRecord != null)
            {
                foreach (DataGridViewRow row in dgvAVL.Rows)
                {
                    if (row.Cells["colID"].Value != null && (int)row.Cells["colID"].Value == idToSearch)
                    {
                        dgvAVL.ClearSelection();
                        row.Selected = true;
                        dgvAVL.FirstDisplayedScrollingRowIndex = row.Index;
                        MessageBox.Show($"Đã tìm thấy Khách hàng có Mã KH: {idToSearch}", "Tìm kiếm thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show($"Không tìm thấy Khách hàng có Mã KH: {idToSearch}", "Tìm kiếm thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnXuatTangK_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtTangK.Text, out int levelK) || levelK <= 0)
            {
                MessageBox.Show("Vui lòng nhập tầng K là một số nguyên dương.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string result = GetNodesAtLevel(currentTree.Root, levelK+1, 1);

            if (string.IsNullOrEmpty(result.Trim()))
            {
                MessageBox.Show($"Tầng **{levelK}** không tồn tại hoặc không có nút nào.", "Kết quả Xuất tầng K", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"Các Mã KH ở tầng **{levelK}** là: \n{result}", "Kết quả Xuất tầng K", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private string GetNodesAtLevel(AVLNode node, int targetLevel, int currentLevel)
        {
            if (node == null) return string.Empty;

            if (currentLevel == targetLevel)
            {
                return $"{node.Data.ID} ";
            }

            if (currentLevel < targetLevel)
            {
                string leftResult = GetNodesAtLevel(node.Left, targetLevel, currentLevel + 1);
                string rightResult = GetNodesAtLevel(node.Right, targetLevel, currentLevel + 1);

                return leftResult + rightResult;
            }

            return string.Empty;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        private void btnDanhSachIDBitrung_Click(object sender, EventArgs e)
        {
            if (currentTree == null || currentTree.Root == null)
            {
                MessageBox.Show("Cây AVL chưa có dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dgvList.DataSource = null;
                return;
            }

            try
            {
                // Lấy danh sách TẤT CẢ các bản ghi bị trùng lặp
                // Phương thức này chỉ trả về các bản ghi từ DuplicatesHead,
                // do đó đã loại trừ các ID không bị trùng và bản ghi gốc.
                List<CustomerRecord> duplicateRecords = currentTree.GetAllDuplicateRecords();

                if (duplicateRecords.Any())
                {
                    // Sắp xếp theo ID để dễ dàng quan sát
                    var sortedDuplicates = duplicateRecords.OrderBy(r => r.ID).ToList();

                    // Gán danh sách đã sắp xếp vào dgvList
                    dgvList.DataSource = sortedDuplicates;
                    dgvList.Refresh();

                    MessageBox.Show($"Đã tìm thấy **{sortedDuplicates.Count}** bản ghi trùng lặp ID. Kết quả được hiển thị trong DataGridview phía dưới.",
                                    "Danh Sách ID Bị Trùng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    dgvList.DataSource = null;
                    MessageBox.Show("Không tìm thấy bất kỳ bản ghi nào bị trùng lặp ID.",
                                    "Danh Sách ID Bị Trùng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            if (currentTree == null || currentTree.Root == null)
            {
                MessageBox.Show("Cây AVL chưa có dữ liệu.", "Lỗi Thống Kê", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // A. Đếm số lượng nút có ID bị trùng
            int duplicateNodeCount = currentTree.CountDuplicateNodes();

            // B. Tìm ID bị trùng nhiều nhất
            var result = currentTree.FindMostDuplicateID();

            // C. Hiển thị kết quả
            string message = $"Tổng số nút có ID bị trùng lặp: **{duplicateNodeCount}**\n";

            if (result.MaxCount > 1)
            {
                message += $"ID bị trùng lặp nhiều nhất là: **{result.ID}**\n";
                message += $"Số lượng bản ghi trùng lặp của ID này: **{result.MaxCount}**";
            }
            else
            {
                message += "Không có ID nào bị trùng lặp (hoặc trùng lặp chỉ 1 lần).";
            }

            MessageBox.Show(message, "Thống Kê ID Trùng Lặp", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        
    }
}