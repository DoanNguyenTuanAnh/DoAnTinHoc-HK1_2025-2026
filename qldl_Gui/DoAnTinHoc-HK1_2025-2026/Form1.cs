using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Globalization;


namespace DoAnTinHoc_HK1_2025_2026
{
    public partial class Form1 : Form 
    {
        // Khai báo đối tượng AVLTree
        private AVLTree node;
        private readonly Dictionary<string, Func<CustomerRecord, int>> keySelectors = new Dictionary<string, Func<CustomerRecord, int>>()
        {
            {"ID", r => r.ID},
            {"Age", r => r.Age},
            {"Purchase Amount", r => r.PurchaseAmount},
            {"Previous Purchases", r => r.PreviousPurchases}
        };
        // --- CÁC HẰNG SỐ VẼ ---
        private const int NodeRadius = 25; // Bán kính nút (để nút là hình tròn)
        private const int HorizontalSpacing = 60; // Khoảng cách ngang giữa các nút con
        private const int VerticalSpacing = 80;   // Khoảng cách dọc giữa các tầng

        private const int StartX = 50; // Vị trí X bắt đầu vẽ cây
        private const int StartY = 30; // Vị trí Y bắt đầu vẽ cây

        private const int MaxNodesToDraw = 10; // Giới hạn số nút vẽ

        // --- CỜ VÀ ĐỐI TƯỢNG VẼ ---
        private Graphics treeGraphics; // Đối tượng Graphics để vẽ
        private Font nodeFont = new Font("Arial", 8, FontStyle.Bold);
        private Brush textBrush = Brushes.Black;
        private Pen nodePen = new Pen(Color.DarkBlue, 2);
        private Pen edgePen = new Pen(Color.Gray, 1);


        public Form1()
        {
            InitializeComponent();
            InitializeKeySelectorComboBox();
            if (cmbSortingKey.Items.Count > 0 && cmbSortingKey.SelectedIndex == -1)
            {
                cmbSortingKey.SelectedIndex = 0;
            }
            pnlTreeCanvas.Paint += pnlTreeCanvas_Paint;
        }

        // --- HÀM XỬ LÝ ĐỌC FILE CSV VÀ XÂY DỰNG CÂY ---

        private void LoadDataFromCsv(string filePath)
        {
            string defaultKey = "ID Khách hàng";
            string selectedKey = defaultKey;

          
            if (cmbSortingKey.SelectedItem != null)
            {
                selectedKey = cmbSortingKey.SelectedItem.ToString();
            }
           
            if (!keySelectors.ContainsKey(selectedKey))
            {
                selectedKey = defaultKey;
            }

            Func<CustomerRecord, int> currentKeySelector = keySelectors[selectedKey];

            try
            {
               
                node = new AVLTree(currentKeySelector);

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
                        CSubscriptionStatus subscriptionStatus = (values[i++].Trim().ToLower() == "yes") ? CSubscriptionStatus.Yes : CSubscriptionStatus.No;
                        string shippingType = values[i++].Trim();
                        CDiscountApplied discountApplied = (values[i++].Trim().ToLower() == "yes") ? CDiscountApplied.Yes : CDiscountApplied.No;
                        CPromoCodeUsed promoCodeUsed = (values[i++].Trim().ToLower() == "yes") ? CPromoCodeUsed.Yes : CPromoCodeUsed.No;
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
                        node.Chen(record);
                    }
                }

                MessageBox.Show($"Đã tải và xây dựng cây AVL theo '{selectedKey}' với {node.DemNode()} bản ghi.", "Thành công");

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
        private void RefreshDataGrid()
        {
            if (node == null) return;
            List<CustomerRecord> allRecords = node.GetAllRecords();
            dgvCustomers.DataSource = null;
            dgvCustomers.DataSource = allRecords;
        }
        private void HighlightRecord(int searchID)
        {
           
            if (dgvCustomers.Rows.Count == 0 || dgvCustomers.Columns.Count == 0)
            {
                return;
            }
        
            int idColumnIndex = -1;
      
            if (dgvCustomers.Columns.Contains("CustomerID"))
            {
                idColumnIndex = dgvCustomers.Columns["CustomerID"].Index;
            }
            else
            {
            
                return;
            }

      
            foreach (DataGridViewRow row in dgvCustomers.Rows)
            {
                row.DefaultCellStyle.BackColor = Color.White; 
                row.Selected = false; // Bỏ chọn
            }

          
            bool found = false;
            foreach (DataGridViewRow row in dgvCustomers.Rows)
            {
                if (row.IsNewRow) continue;     
                if (row.Cells[idColumnIndex].Value != null &&
                    int.TryParse(row.Cells[idColumnIndex].Value.ToString(), out int currentID))
                {
                    if (currentID == searchID)
                    {                 
                        row.DefaultCellStyle.BackColor = Color.Yellow; 
                        row.Selected = true; // Chọn hàng đó
                        dgvCustomers.FirstDisplayedScrollingRowIndex = row.Index;
                        found = true;
                        break;
                    }
                }
            }
        }
        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            if (!int.TryParse(txtInputID.Text, out int searchID))
            {
                MessageBox.Show("Vui lòng nhập ID hợp lệ.", "Lỗi nhập liệu");
                return;
            }
            CustomerRecord foundRecord = node.TimKiem(searchID);
            if (foundRecord != null)
            {
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
            if (node == null)
            {
                MessageBox.Show("Vui lòng tải dữ liệu trước.", "Lỗi");
                return;
            }
            if (!int.TryParse(txtInputID.Text, out int primaryKey))
            {
                MessageBox.Show("Vui lòng nhập giá trị Khóa Chính (Key Value) hợp lệ.", "Lỗi nhập liệu");
                return;
            }
            CustomerRecord recordToDelete = node.TimKiem(primaryKey);
            if (recordToDelete == null)
            {
                MessageBox.Show($"Không tìm thấy bản ghi có Khóa Chính = {primaryKey} để xóa.", "Lỗi xóa");
                return;
            }
            int secondaryID = recordToDelete.ID; 
            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa khách hàng ID: {secondaryID} (Khóa Chính: {primaryKey}) không?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            node.Xoa(primaryKey, secondaryID);
            RefreshDataGrid();
            UpdateStats();
            MessageBox.Show($"Đã xóa thành công Khách hàng ID {secondaryID}.", "Xóa thành công");
        
        }
        private void UpdateStats()
        {
            if (node.DemNode() > 0)
            {
                int height = node.DemChieuCao();
                int totalNodes = node.DemNode();
                int leafNodes = node.DemNutLa();
                int n= node.DemSoNutTangK(2);
                int n2= node.DemSoButBenTrai();
               
                // Giả sử bạn có các Label: lblHeight, lblTotalNodes, lblLeafNodes
                lblHeight.Text = $"Chiều cao Cây AVL: {height}";
                lblTotalNodes.Text = $"Tổng số Nút: {totalNodes}";
                lblLeafNodes.Text = $"Số Nút Lá: {leafNodes}";
                lblNodeK.Text = $"Số Nút Tăng K (K=2): {n}";
                lblNutBenTrai.Text = $"Số Bút Bên Trái: {n2}";
               
            }
            else
            {
                // Xử lý trường hợp cây rỗng
                lblHeight.Text = "Chiều cao Cây AVL: 0";
                lblTotalNodes.Text = "Tổng số Nút: 0";
                lblLeafNodes.Text = "Số Nút Lá: 0";
            }
        }
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
           
            if (node != null && node.DemNode() > 0)
            {
              
                btnLoadAndStats_Click(sender, e);
            }
        }
        private class NodeLayoutInfo
        {
            public AVLNode Node;
            public int X;
            public int Y;
            public int SubtreeWidth; // Chiều rộng của cây con dưới nút này
        }

        private void DrawGDIPlusTree(Func<CustomerRecord, int> visualKeySelector)
        {
          
            pnlTreeCanvas.Invalidate(); 
        }
        private void DrawNodesAndEdgesRecursive(AVLNode node, Dictionary<AVLNode, NodeLayoutInfo> layoutMap, Func<CustomerRecord, int> visualKeySelector)
        {
            if (node == null) return;

            NodeLayoutInfo info = layoutMap[node];
            int visualValue = visualKeySelector(node.Data);

         
            int nodeCenterX = info.X; 
            int nodeCenterY = info.Y;

            // Vẽ cạnh cho con trái
            if (node.Left != null)
            {
                NodeLayoutInfo leftChildInfo = layoutMap[node.Left];
                treeGraphics.DrawLine(edgePen, nodeCenterX, nodeCenterY + NodeRadius, leftChildInfo.X, leftChildInfo.Y - NodeRadius);
                DrawNodesAndEdgesRecursive(node.Left, layoutMap, visualKeySelector);
            }

            // Vẽ cạnh cho con phải
            if (node.Right != null)
            {
                NodeLayoutInfo rightChildInfo = layoutMap[node.Right];
                treeGraphics.DrawLine(edgePen, nodeCenterX, nodeCenterY + NodeRadius, rightChildInfo.X, rightChildInfo.Y - NodeRadius);
                DrawNodesAndEdgesRecursive(node.Right, layoutMap, visualKeySelector);
            }

            // Vẽ hình tròn cho nút
            Rectangle nodeRect = new Rectangle(nodeCenterX - NodeRadius, nodeCenterY - NodeRadius, NodeRadius * 2, NodeRadius * 2);
            treeGraphics.FillEllipse(Brushes.White, nodeRect);
            treeGraphics.DrawEllipse(nodePen, nodeRect);

            // Vẽ chữ trong nút
            string keyText = visualValue.ToString();
            string idText = "ID:" + node.Data.ID.ToString();

            SizeF keyTextSize = treeGraphics.MeasureString(keyText, nodeFont);
            SizeF idTextSize = treeGraphics.MeasureString(idText, nodeFont);

            // Căn giữa keyText
            PointF keyTextPoint = new PointF(
                nodeCenterX - keyTextSize.Width / 2,
                nodeCenterY - keyTextSize.Height / 2 - 5 // Dịch lên một chút
            );
            treeGraphics.DrawString(keyText, nodeFont, textBrush, keyTextPoint);

            // Căn giữa idText
            PointF idTextPoint = new PointF(
                nodeCenterX - idTextSize.Width / 2,
                nodeCenterY - idTextSize.Height / 2 + 10 // Dịch xuống một chút
            );
            treeGraphics.DrawString(idText, new Font("Arial", 7), textBrush, idTextPoint); // Font nhỏ hơn cho ID
        }

        private Func<CustomerRecord, int> _currentVisualKeySelector;
        private void btnDrawGDIPlus_Click(object sender, EventArgs e)
        {
            if (node == null || node.DemNode() == 0)
            {
                MessageBox.Show("Vui lòng tải dữ liệu trước.", "Lỗi");
                return;
            }

            int totalNodes = node.DemNode();

            // Kiểm tra giới hạn nút
            if (totalNodes > MaxNodesToDraw)
            {
                MessageBox.Show($"Tổng số nút là {totalNodes}. Vượt quá giới hạn tối đa cho phép vẽ cây đồ họa ({MaxNodesToDraw} nút).", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

         
            string selectedKey = cmbSortingKey.SelectedItem?.ToString() ?? "Customer ID";
            if (!keySelectors.ContainsKey(selectedKey)) selectedKey = "Customer ID";
            Func<CustomerRecord, int> visualKeySelector = keySelectors[selectedKey];

          
            _currentVisualKeySelector = visualKeySelector;

            // Kích hoạt Panel vẽ lại
            pnlTreeCanvas.Invalidate();
            MessageBox.Show($"Đã vẽ cây AVL ({totalNodes} nút) sử dụng GDI+.", "Thành công");
        }
    
        private void pnlTreeCanvas_Paint(object sender, PaintEventArgs e)
        {
        
            if (node == null || node.Root == null || _currentVisualKeySelector == null) return;

            treeGraphics = e.Graphics; // Lấy đối tượng Graphics từ sự kiện Paint
            treeGraphics.SmoothingMode = SmoothingMode.AntiAlias; // Làm mịn đường vẽ


            Dictionary<AVLNode, NodeLayoutInfo> layoutMap = new Dictionary<AVLNode, NodeLayoutInfo>();

          
            int CalculateNodePositions(AVLNode node, int currentX, int depth)
            {
                if (node == null) return 0;

                NodeLayoutInfo info = new NodeLayoutInfo { Node = node, Y = StartY + depth * VerticalSpacing };
                layoutMap[node] = info;

                int leftChildWidth = CalculateNodePositions(node.Left, currentX, depth + 1);
                currentX += leftChildWidth; // Cập nhật X sau khi vẽ cây con trái

                // Vị trí X của nút hiện tại là giữa cây con trái và phải (hoặc ngay sau cây con trái nếu không có con phải)
                info.X = currentX + (NodeRadius * 2 + HorizontalSpacing) / 2; // Tạm thời đặt giữa
                currentX += (NodeRadius * 2 + HorizontalSpacing); // Khoảng cách cho nút hiện tại

                int rightChildWidth = CalculateNodePositions(node.Right, currentX, depth + 1);
                currentX += rightChildWidth; // Cập nhật X sau khi vẽ cây con phải

                info.SubtreeWidth = currentX - (info.X - (NodeRadius * 2 + HorizontalSpacing) / 2); // Tổng chiều rộng từ vị trí bắt đầu của subtree

                // Điều chỉnh vị trí X của nút cha để nó nằm giữa con trái và con phải
                if (node.Left != null && node.Right != null)
                {
                    NodeLayoutInfo leftInfo = layoutMap[node.Left];
                    NodeLayoutInfo rightInfo = layoutMap[node.Right];
                    info.X = (leftInfo.X + rightInfo.X) / 2;
                }
                else if (node.Left != null) // Chỉ có con trái
                {
                    NodeLayoutInfo leftInfo = layoutMap[node.Left];
                    // Đảm bảo nút cha không quá sát con trái
                    int nodeHalfWidth = NodeRadius + HorizontalSpacing / 2;
                    if (info.X < leftInfo.X + nodeHalfWidth)
                    {
                        info.X = leftInfo.X + nodeHalfWidth;
                    }

                }
                else if (node.Right != null) // Chỉ có con phải
                {
                    NodeLayoutInfo rightInfo = layoutMap[node.Right];
                    // Đảm bảo nút cha không quá sát con phải
                    int nodeHalfWidth = NodeRadius + HorizontalSpacing / 2;
                    if (info.X > rightInfo.X - nodeHalfWidth)
                    {
                        info.X = rightInfo.X - nodeHalfWidth;
                    }
                }

                return leftChildWidth + (NodeRadius * 2 + HorizontalSpacing) + rightChildWidth;
            }
            CalculateNodePositions(node.Root, StartX, 0);
            DrawNodesAndEdgesRecursive(node.Root, layoutMap, _currentVisualKeySelector);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra cây AVL đã được tải chưa
            if (node == null)
            {
                MessageBox.Show("Vui lòng tải dữ liệu CSV trước khi thêm bản ghi.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Mở Form2 để nhập dữ liệu
            // Lưu ý: Form2 cần phải có một thuộc tính (Property) để trả về CustomerRecord mới.
            using (Form2 addForm = new Form2())
            {
                // Hiển thị Form2 dưới dạng Dialog (Form1 sẽ tạm dừng)
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    // Lấy bản ghi mới từ Form2 (giả định Form2 có thuộc tính NewRecord)
                    CustomerRecord newRecord = addForm.NewRecord;

                    if (newRecord != null)
                    {
                        // **Kiểm tra ID Duy nhất:** Đảm bảo ID Khách hàng không bị trùng
                        // (ID là khóa chính phụ nên cần kiểm tra tất cả bản ghi)
                        if (node.GetAllRecords().Any(r => r.ID == newRecord.ID))
                        {
                            MessageBox.Show($"Lỗi: ID Khách hàng ({newRecord.ID}) đã tồn tại trong bộ dữ liệu. Không thể thêm.", "Lỗi trùng ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // 3. Tiến hành chèn vào cây AVL
                        try
                        {
                            // Chèn bản ghi mới. Cây sẽ sử dụng KeySelector hiện tại (ID, Age, Purchase...)
                            node.Chen(newRecord);
                            MessageBox.Show($"Đã thêm thành công Khách hàng ID: {newRecord.ID} (Khóa Sắp xếp: {node.KeySelector(newRecord)}).", "Thành công");

                            // 4. Cập nhật giao diện
                            RefreshDataGrid(); // Cập nhật DataGridView
                            UpdateStats();     // Cập nhật thống kê cây

                            // Chỉ vẽ lại cây nếu số nút vẫn trong giới hạn
                            if (node.DemNode() <= MaxNodesToDraw)
                            {
                                pnlTreeCanvas.Invalidate();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Lỗi khi chèn vào cây: {ex.Message}", "Lỗi Chèn");
                        }
                    }
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra Cây AVL và DataGridView
            if (node == null || node.DemNode() == 0)
            {
                MessageBox.Show("Vui lòng tải dữ liệu trước khi chỉnh sửa.", "Lỗi");
                return;
            }

            if (dgvCustomers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để chỉnh sửa.", "Lỗi");
                return;
            }

            // 2. Lấy ID Khách hàng từ dòng được chọn
            DataGridViewRow selectedRow = dgvCustomers.SelectedRows[0];
            int idColumnIndex = dgvCustomers.Columns.Contains("ID") ? dgvCustomers.Columns["ID"].Index : 0;

            if (selectedRow.Cells[idColumnIndex].Value == null ||
                !int.TryParse(selectedRow.Cells[idColumnIndex].Value.ToString(), out int customerIDToEdit))
            {
                MessageBox.Show("Không thể đọc Customer ID của bản ghi đã chọn.", "Lỗi");
                return;
            }

            // 3. Tìm kiếm bản ghi gốc trong Cây AVL
            // Do cây AVL sắp xếp theo keySelector (ID, Age, Purchase...), ta cần hàm tìm kiếm theo ID thực (khóa phụ)
            // Nếu lớp AVLTree của bạn chưa có hàm TimKiemTheoID, ta dùng cách thủ công:
            CustomerRecord originalRecord = node.GetAllRecords().FirstOrDefault(r => r.ID == customerIDToEdit);

            if (originalRecord == null)
            {
                MessageBox.Show($"Không tìm thấy Khách hàng ID: {customerIDToEdit} trong bộ dữ liệu.", "Lỗi tìm kiếm");
                return;
            }

            // 4. Mở Form2 ở chế độ Chỉnh sửa
            using (Form2 editForm = new Form2(originalRecord))
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    CustomerRecord updatedRecord = editForm.NewRecord;

                    if (updatedRecord != null)
                    {
                        // 5. CẬP NHẬT TRONG CÂY: Xóa bản ghi cũ, sau đó chèn bản ghi mới
                        try
                        {
                            int oldPrimaryKey = node.KeySelector(originalRecord);

                            // Xóa bản ghi cũ
                            node.Xoa(oldPrimaryKey, originalRecord.ID);

                            // Chèn bản ghi mới (sẽ tự động được sắp xếp lại nếu Primary Key thay đổi)
                            node.Chen(updatedRecord);

                            MessageBox.Show($"Đã cập nhật thành công Khách hàng ID: {updatedRecord.ID}.", "Thành công");

                            // 6. Cập nhật giao diện
                            RefreshDataGrid();
                            UpdateStats();
                            HighlightRecord(updatedRecord.ID); // Tô sáng bản ghi vừa sửa
                            if (node.DemNode() <= MaxNodesToDraw)
                            {
                                pnlTreeCanvas.Invalidate();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Lỗi khi cập nhật cây: {ex.Message}", "Lỗi Cập nhật");
                        }
                    }
                }
            }
        }
        private void GhiFileCSV(string filePath)
        {
            if (node == null || node.DemNode() == 0)
            {
                MessageBox.Show("Cây AVL đang rỗng. Không có dữ liệu để lưu.", "Lưu File", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                List<CustomerRecord> allRecords = node.GetAllRecords();            
                string header = "Customer ID,Age,Gender,Item Purchased,Category,Purchase Amount (USD),Location,Size,Color,Season,Review Rating,Subscription Status,Shipping Type,Discount Applied,Promo Code Used,Previous Purchases,Payment Method,Frequency of Purchases";   
                List<string> lines = new List<string> { header };
                foreach (var record in allRecords)
                {
                    lines.Add(record.ToCsvString());
                }
                File.WriteAllLines(filePath, lines, Encoding.UTF8);

                MessageBox.Show($"Đã lưu thành công {allRecords.Count} bản ghi vào file: {filePath}", "Lưu File Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi ghi file CSV: {ex.Message}", "Lỗi Lưu File", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string filePath = "data.csv"; // Tên file mặc định
            GhiFileCSV(filePath);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                                                    "Bạn có chắc chắn muốn thoát chương trình không? Nhớ lưu dữ liệu.",
                                                    "Xác nhận Thoát",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Question
                                                    );
            if (result == DialogResult.Yes)
            {
                Close();
            }
        }
    }
}