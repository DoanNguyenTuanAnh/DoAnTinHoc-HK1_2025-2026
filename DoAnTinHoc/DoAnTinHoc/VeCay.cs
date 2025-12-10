using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DoAnTinHoc
{
    public partial class VeCay : Form
    {
        private AVLTree currentTree;
        private int nodeLimit = 0; // Giới hạn số lượng node vẽ dựa trên ID

        // Cấu hình vẽ (Constants)
        private const int NODE_RADIUS = 20;    // Bán kính nút (Nút sẽ có kích thước 40x40)
        private const int HORIZONTAL_GAP = 50; // Khoảng cách giữa các cây con ngang
        private const int VERTICAL_GAP = 80;   // Khoảng cách giữa các tầng dọc

        // Map lưu trữ vị trí đã tính toán của mỗi node
        private Dictionary<AVLNode, Point> nodePositions = new Dictionary<AVLNode, Point>();

        // Constructor: Nhận đối tượng AVLTree
        public VeCay(AVLTree tree)
        {
            InitializeComponent();
            this.currentTree = tree;
            this.DoubleBuffered = true; // Giúp việc vẽ mượt mà hơn

            // Designer đã tự động gắn plVeCay_Paint vào Panel, KHÔNG cần dòng this.Paint += ... nữa.
        }

        // Cần thêm hàm này nếu Designer có sự kiện TextChanged
        private void txtVeCay_TextChanged(object sender, EventArgs e) { }

        private void btnVeCay_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra input hợp lệ
            if (!int.TryParse(txtVeCay.Text, out int limit) || limit <= 0)
            {
                MessageBox.Show("Vui lòng nhập ID tối đa muốn vẽ là một số nguyên dương.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. Kiểm tra cây có dữ liệu không
            if (currentTree == null || currentTree.Root == null)
            {
                MessageBox.Show("Cây AVL hiện đang rỗng. Không thể vẽ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 3. Gán giới hạn và yêu cầu Panel vẽ lại
            this.nodeLimit = limit;
            this.plVeCay.Invalidate(); // <<< QUAN TRỌNG: Gọi vẽ lại Panel
        }
        // PHƯƠNG THỨC 1: Tính toán vị trí (Layout)
        // Trả về tọa độ X bắt đầu tiếp theo cần dùng sau khi cây con hiện tại đã được bố cục.
        private float CalculateNodePositions(AVLNode node, int depth, float currentX)
        {
            // Nếu nút rỗng hoặc ID vượt quá giới hạn, trả về vị trí X hiện tại (không cần dời).
            if (node == null || node.Data.ID > nodeLimit)
                return currentX;

            // 1. Bố cục cây con trái
            // nextX là vị trí X mới, sau khi đã vẽ hết cây con trái
            float nextX = CalculateNodePositions(node.Left, depth + 1, currentX);

            // 2. Định vị nút hiện tại (Center X)
            // Nút hiện tại được đặt ngay sau cây con trái (nextX) + 1 bán kính.
            float nodeCenterX = nextX + NODE_RADIUS;
            float nodeY = 50 + depth * VERTICAL_GAP; // Y dựa trên độ sâu (tầng)

            // Lưu vị trí (Tâm) của nút
            nodePositions[node] = new Point((int)nodeCenterX, (int)nodeY);

            // 3. Chuẩn bị vị trí X bắt đầu cho cây con phải
            float rightStartX = nodeCenterX + NODE_RADIUS + HORIZONTAL_GAP;

            // 4. Bố cục cây con phải
            float finalX = CalculateNodePositions(node.Right, depth + 1, rightStartX);

            // finalX là vị trí X cuối cùng đã được sử dụng cho cả cây con này
            return finalX;
        }
        // PHƯƠNG THỨC 2: Vẽ (Render)
        // Dùng đệ quy để vẽ các cạnh, nút và ID.
        private void DrawTreeRecursive(Graphics g, AVLNode node, float offsetX, float offsetY)
        {
            if (node == null || !nodePositions.ContainsKey(node)) return;

            // Lấy vị trí đã tính toán và ÁP DỤNG OFFSET để căn giữa
            Point center = nodePositions[node];
            center.X = (int)(center.X + offsetX);
            center.Y = (int)(center.Y + offsetY);

            // 1. Vẽ cạnh (Edges)
            using (Pen p = new Pen(Color.Gray, 2))
            {
                // Vẽ cạnh trái
                if (node.Left != null && nodePositions.ContainsKey(node.Left))
                {
                    Point childCenter = nodePositions[node.Left];
                    childCenter.X = (int)(childCenter.X + offsetX);
                    childCenter.Y = (int)(childCenter.Y + offsetY);
                    // Nối từ đáy node cha đến đỉnh node con
                    g.DrawLine(p, center.X, center.Y + NODE_RADIUS, childCenter.X, childCenter.Y - NODE_RADIUS);
                }

                // Vẽ cạnh phải
                if (node.Right != null && nodePositions.ContainsKey(node.Right))
                {
                    Point childCenter = nodePositions[node.Right];
                    childCenter.X = (int)(childCenter.X + offsetX);
                    childCenter.Y = (int)(childCenter.Y + offsetY);
                    g.DrawLine(p, center.X, center.Y + NODE_RADIUS, childCenter.X, childCenter.Y - NODE_RADIUS);
                }
            }

            // 2. Vẽ nút (Node - hình tròn)
            Rectangle nodeBounds = new Rectangle(center.X - NODE_RADIUS, center.Y - NODE_RADIUS, 2 * NODE_RADIUS, 2 * NODE_RADIUS);
            g.FillEllipse(Brushes.LightBlue, nodeBounds);
            g.DrawEllipse(Pens.DarkBlue, nodeBounds);

            // 3. Vẽ văn bản (Text - ID)
            using (StringFormat sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                g.DrawString(
                    node.Data.ID.ToString(),
                    this.Font,
                    Brushes.Black,
                    nodeBounds,
                    sf
                );
            }

            // Đệ quy cho cây con
            DrawTreeRecursive(g, node.Left, offsetX, offsetY);
            DrawTreeRecursive(g, node.Right, offsetX, offsetY);
        }
        private void plVeCay_Paint(object sender, PaintEventArgs e)
        {
            if (currentTree == null || currentTree.Root == null || nodeLimit <= 0) return;

            Graphics g = e.Graphics;
            g.Clear(this.plVeCay.BackColor); // Xóa nền Panel

            // 1. Tính toán vị trí tất cả các node (Layout)
            nodePositions.Clear();
            // totalEndPosition không được dùng, nhưng vẫn gọi để gán vị trí vào nodePositions
            CalculateNodePositions(currentTree.Root, 0, 0);

            // 2. Tính toán Offset để căn cây vào giữa Panel
            if (nodePositions.Count == 0) return;

            // Tìm tọa độ X Center nhỏ nhất và lớn nhất
            float minX = nodePositions.Values.Min(p => p.X);
            float maxX = nodePositions.Values.Max(p => p.X);

            // Chiều rộng Panel
            int canvasWidth = this.plVeCay.ClientSize.Width;

            // Tính toán độ dời (Offset)
            float treeSpanWidth = maxX - minX;
            float targetCenterX = canvasWidth / 2f;
            float currentTreeCenterX = minX + (treeSpanWidth / 2f);
            float offsetX = targetCenterX - currentTreeCenterX;

            // 3. Tiến hành vẽ cây và các cạnh
            DrawTreeRecursive(g, currentTree.Root, offsetX, 0);
        }
    }
}